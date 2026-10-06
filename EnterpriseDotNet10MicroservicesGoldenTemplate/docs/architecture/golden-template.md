# Enterprise .NET 10 Microservices Golden Template

## Standard
- .NET 10
- Microservices with bounded contexts
- Clean Architecture for Catalog
- Hexagonal Architecture / Ports & Adapters for Order, Payment and Inventory
- Event-driven Hexagonal boundaries for Notification
- SQL Server owned per service
- Dapper + Stored Procedures
- CQRS + MediatR
- Repository + UnitOfWork for local transactions
- REST/gRPC only where synchronous interaction is justified
- RabbitMQ for asynchronous integration events
- Transactional Outbox + Inbox/idempotent consumers
- Retry + DLQ/DLX with bounded attempts
- Correlation/Trace IDs, health checks and structured observability
- Security-by-design and externalized secrets
- Unit, contract and integration testing
- CI/CD, dependency/security scanning and container scanning

## Non-negotiable
No service accesses another service's database. No business logic in repositories/controllers. No inline SQL in Application. No distributed transaction as a default integration strategy. Corporate infrastructure decisions are documented through ADRs rather than invented in the template.
