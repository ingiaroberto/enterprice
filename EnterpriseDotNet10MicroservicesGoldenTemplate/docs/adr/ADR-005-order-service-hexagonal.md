# ADR-005 — Order Service Hexagonal Architecture

## Status
Accepted for the starter baseline; subject to normal architecture governance.

## Context

Order Service has synchronous dependencies on Catalog and asynchronous dependencies on the enterprise event broker. It also requires persistence through Dapper and Stored Procedures. We need strong isolation from those technologies while keeping the service independently deployable.

## Decision

Use Hexagonal Architecture for Order Service. Application exposes ports and Infrastructure provides adapters.

- HTTP is a driving adapter.
- Catalog HTTP client is a driven adapter.
- Dapper/Stored Procedure repositories are driven adapters.
- RabbitMQ publishing and consumption are messaging adapters.
- UnitOfWork remains a local transaction boundary.

## Consequences

### Positive

- Business use cases are testable without infrastructure.
- Infrastructure can be replaced without changing use cases.
- External dependencies are explicit through ports.
- RabbitMQ and HTTP concerns remain outside the domain/application business rules.

### Trade-offs

- More interfaces and files than a simple layered service.
- Adapter contracts require governance and versioning.
- Some mapping and composition code is necessary.
