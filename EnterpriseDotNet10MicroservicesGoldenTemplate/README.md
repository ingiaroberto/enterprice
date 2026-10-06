# RIDA Enterprise .NET 10 Microservices Golden Template — Rebuilt

## What changed
This release is a **code-first rebuild** of the backend reference implementation. The previous scaffold contained placeholder services and compressed source files; this version establishes a consistent enterprise implementation pattern.

## Golden path
**Order Service** is the reference implementation:

`HTTP API → MediatR CQRS → FluentValidation → Domain → Application Port → Infrastructure Adapter → Dapper → Stored Procedure → local SQL transaction`

For asynchronous integration:

`Domain/application event → Outbox → Publisher → RabbitMQ → Consumer boundary → idempotent processing`

## Services
- Catalog — Clean Architecture reference slice
- Orders — Hexagonal / Ports & Adapters golden path
- Payments — Hexagonal-compatible service boundary
- Inventory — Hexagonal-compatible service boundary
- Notification — event-oriented service boundary

## Important SQL rule
The SQL scripts in `database/` are **reference template contracts** created solely to keep the code-to-SQL examples coherent. They are not a statement of RIDA's corporate database schema. Before production, replace them with approved corporate objects without changing the architectural boundary.

## Production validation
The repository is prepared for .NET 10. This environment does not include the .NET SDK, so a runtime `dotnet build/test` was not falsely claimed. Static consistency checks are included in `scripts/verify-backend.py`.
