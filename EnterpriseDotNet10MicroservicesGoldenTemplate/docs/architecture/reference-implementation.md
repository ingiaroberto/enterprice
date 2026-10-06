# RIDA Backend Reference Implementation

## Boundary rules
1. Domain has no infrastructure dependencies.
2. Application owns use cases and ports.
3. Infrastructure implements persistence, HTTP adapters and messaging.
4. API is composition and transport only.
5. Each service owns its database.
6. UnitOfWork is local to a service transaction.
7. Outbox is committed in the same transaction as the business write.
8. Cross-service consistency is asynchronous; distributed transactions are not used.

## Order traceability
`POST /api/orders` → `CreateOrderCommand` → `CreateOrderHandler` → `ICatalogPort`/`IOrderRepository`/`IOutboxRepository` → `CatalogClient`/`OrderRepository`/`OutboxRepository` → Dapper → stored procedures.
