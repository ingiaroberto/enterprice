# RIDA Enterprise .NET 10 Microservices Golden Template

## Version 3.0.0 — FINAL REBUILT

This package is the audited final reconstruction of the backend template.

- Order Service is the Golden Path.
- Five independently deployable services are included.
- Clean/Hexagonal boundaries are enforced.
- CQRS + MediatR + FluentValidation are implemented.
- Dapper + SQL Server + Stored Procedures are isolated in Infrastructure.
- Local UnitOfWork transactions are implemented.
- Order Outbox + RabbitMQ publisher baseline is included.
- Security, observability, health checks, rate limiting and ProblemDetails baseline are included.
- Docker and CI/CD definitions are included.
- Static architecture/code/SQL consistency audits pass.

Runtime build/test certification must be executed on a machine or CI runner with the .NET 10 SDK.
