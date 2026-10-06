# ADR-005 — Order Service Hexagonal Golden Path

## Status
Accepted for the starter baseline.

## Context
Order Service integrates HTTP, SQL Server/Dapper and RabbitMQ. Direct framework dependencies in use cases would make the service difficult to test and evolve.

## Decision
Use Hexagonal Architecture with explicit ports and infrastructure adapters. The Application layer owns use cases and depends on abstractions. Infrastructure implements SQL, HTTP and RabbitMQ adapters. Business state and its Outbox record are committed in one local UnitOfWork transaction. RabbitMQ is used for asynchronous integration; HTTP is used for synchronous request/response.

## Consequences
- Higher testability and replaceable adapters.
- Clear service boundaries.
- Additional abstractions and integration-test requirements.
- Eventual consistency is explicit.
