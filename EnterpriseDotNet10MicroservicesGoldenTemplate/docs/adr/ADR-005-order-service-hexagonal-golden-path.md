# ADR-005 — Order Service Hexagonal Golden Path

## Status
Accepted for the starter/reference implementation.

## Context

Order Service has multiple external concerns: Catalog HTTP calls, SQL Server/Dapper persistence and RabbitMQ messaging. The service must remain testable and independent from those technologies.

## Decision

Use Hexagonal Architecture / Ports & Adapters for Order Service.

- REST controller is an inbound adapter.
- MediatR use cases form the application boundary.
- `ICatalogPort`, `IOrderRepository`, `IOutboxPort` and `IUnitOfWork` are application ports.
- HTTP, Dapper/SQL Server and RabbitMQ implementations are infrastructure adapters.
- RabbitMQ is used asynchronously; HTTP/REST is used for synchronous request/response.
- Outbox + UnitOfWork provide atomic local persistence.
- Inbox provides consumer idempotency.
- RabbitMQ retry/DLQ is bounded; infinite requeue is prohibited.

## Consequences

Positive:
- Business logic is isolated from frameworks.
- Adapters can be replaced or tested independently.
- RabbitMQ and SQL Server do not leak into the domain/application core.
- The same pattern can be reused by future microservices.

Trade-offs:
- More interfaces and indirection.
- Additional operational components: Outbox, Inbox, retry and DLQ.
- Production topology and platform integration remain external decisions.
