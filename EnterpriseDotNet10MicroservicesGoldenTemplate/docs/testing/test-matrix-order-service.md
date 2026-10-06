# Order Service Test Matrix

| Layer | Scenario | Type | Artifact | Gate |
|---|---|---|---|---|
| Domain | Valid order creation | Unit | `Order` | Required |
| Application | Active category | Unit | `CreateOrderHandlerTests` | Required |
| Application | Missing category | Unit | `CreateOrderFailureTests` | Required |
| Application | Inactive category | Unit | `CreateOrderFailureTests` | Required |
| Application | Validation | Unit | `CreateOrderValidator` | Required |
| Contract | `order.created.v1` shape | Contract | `OrderCreatedContractTests` | Required |
| Contract | Catalog HTTP response | Contract | `CatalogClientHttpContractTests` | Required |
| Persistence | SQL schema reachability | Integration | `DatabaseSmokeTests` | Environment gate |
| Messaging | RabbitMQ connectivity | Integration | `RabbitMqSmokeTests` | Environment gate |
| Messaging | Retry/DLQ behavior | Integration | RabbitMQ environment test | Environment gate |
| End-to-end | Create order → Outbox → RabbitMQ | E2E | Deployment test | Environment gate |
| Security | Authenticated request | API/Security | Deployment test | Corporate gate |
| Performance | Sustained order creation | Load | k6/JMeter or corporate tool | Corporate gate |
