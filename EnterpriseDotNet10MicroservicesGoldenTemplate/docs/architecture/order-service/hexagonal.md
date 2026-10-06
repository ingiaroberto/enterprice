# Order Service — Hexagonal Architecture

## Decision

Order Service uses Hexagonal Architecture (Ports and Adapters) while remaining aligned with the enterprise architectural constitution.

Hexagonal Architecture is a service-level implementation choice. The enterprise rules remain unchanged: dependency inversion, bounded-context ownership, DDD, CQRS/MediatR, Dapper + Stored Procedures, local UnitOfWork, Outbox/Inbox, synchronous HTTP through typed clients, and asynchronous RabbitMQ messaging.

## Layers / responsibilities

| Area | Responsibility |
|---|---|
| Domain | Order aggregate and business invariants; no framework or infrastructure dependencies |
| Application | Use cases, commands, validation, input/output ports, orchestration |
| Driving adapters | HTTP controllers; translate external requests into application commands |
| Driven adapters | SQL/Dapper repository, Catalog HTTP adapter, RabbitMQ publisher/consumer |
| Infrastructure | Concrete adapters, connection management, messaging topology, persistence |

## Dependency rule

```text
HTTP Adapter ───────┐
                    ▼
              Application
              /         \
       Input Ports    Output Ports
                         │
          ┌──────────────┼──────────────┐
          ▼              ▼              ▼
     SQL Adapter   Catalog Adapter  RabbitMQ Adapter
          │              │              │
      SQL Server       HTTP           RabbitMQ
```

Application never references Dapper, SQL Server, RabbitMQ.Client or ASP.NET Core.

## Create Order flow

1. `OrdersController` receives the request.
2. MediatR dispatches `CreateOrderCommand`.
3. `CreateOrderHandler` calls `ICatalogPort` to validate the category.
4. The handler creates the domain `Order`.
5. `IUnitOfWork` begins a local SQL transaction.
6. `IOrderRepository` persists the order through `dbo.usp_Order_Create`.
7. `IOutboxRepository` persists `order.created.v1` through `dbo.usp_Outbox_Add`.
8. UnitOfWork commits both records atomically.
9. `OutboxPublisher` publishes the event to RabbitMQ.
10. A failure after publish and before marking published may cause redelivery; consumers must therefore be idempotent.

## Ports

- `ICatalogPort`: synchronous service-to-service dependency.
- `IOrderRepository`: persistence output port.
- `IOutboxRepository`: reliable event publication output port.
- `IUnitOfWork`: local transaction boundary.

## Adapters

- `CatalogClient`: typed `HttpClient` adapter implementing `ICatalogPort`.
- `OrderRepository`: Dapper adapter implementing `IOrderRepository`.
- `OutboxRepository`: Dapper adapter implementing `IOutboxRepository`.
- `OutboxPublisher`: RabbitMQ adapter for asynchronous publication.
- `CategoryCreatedConsumer`: RabbitMQ inbound adapter with Inbox idempotency.

## Production note

The SQL objects included under `database/` are starter reference objects for this repository, not a declaration of an existing corporate database. They must be reconciled with the approved database contract before deployment.
