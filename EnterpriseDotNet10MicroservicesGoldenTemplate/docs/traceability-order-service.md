# Order Service Traceability

| Requirement | Spec | Application | Persistence | Event | Tests | Evidence |
|---|---|---|---|---|---|---|
| Validate CategoryId | `002-create-order/spec.md` | `CreateOrderValidator` | N/A | N/A | Unit | CI |
| Category exists/active | `002-create-order/spec.md` | `CreateOrderHandler` + `ICatalogPort` | Catalog API | N/A | Unit + HTTP contract | CI |
| Persist order atomically | `002-create-order/plan.md` | `IUnitOfWork` | `usp_Order_Create` | `order.created.v1` | Integration | Environment |
| Persist event atomically | `002-create-order/plan.md` | `IOutboxRepository` | `usp_Outbox_Add` | `order.created.v1` | Unit + integration | Environment |
| Publish asynchronously | `002-create-order/spec.md` | `OutboxPublisher` | Outbox | RabbitMQ | Integration | Environment |
| Idempotent consumption | `002-create-order/spec.md` | `IInboxStore` | `Inbox contract (consumer implementation pending)` | Consumer | Integration | Environment |
| Retry/DLQ | `002-create-order/spec.md` | Consumer | RabbitMQ topology | DLQ | Integration | Environment |
