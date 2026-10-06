# Enterprise .NET 10 Microservices Constitution

1. **Architecture** — .NET 10, Clean Architecture and/or Hexagonal Architecture at service level, DDD/Bounded Contexts, independently deployable microservices. The selected style MUST preserve inward dependency direction and ports/adapters isolation.
2. **Spec-Driven** — specify → clarify → plan → checklist → tasks → analyze → implement → converge. Ambiguities affecting business, security, data or contracts require clarification.
3. **Data ownership** — each service owns its persistence; no cross-service database access.
4. **Application** — CQRS/MediatR, FluentValidation, dependency inversion; handlers do not create connections or contain SQL.
5. **Persistence** — Dapper + SQL Server + approved Stored Procedures. No inline SQL in Application. Never invent DB objects.
6. **Repository** — persistence only; uses the UnitOfWork connection/transaction.
7. **UnitOfWork** — local transaction boundary only; owns connection, transaction, begin/commit/rollback/disposal.
8. **Transactions** — local atomic operations share one UnitOfWork. Business state and its Outbox record commit atomically.
9. **Communication** — HTTP/REST or gRPC for immediate responses; IHttpClientFactory for HTTP; async broker/events for decoupled workflows.
10. **Outbox/Inbox** — reliable state-change publication and idempotent consumers.
11. **Resilience** — cancellation, bounded timeout and operation-aware retry/circuit/rate limiting where appropriate.
12. **Security** — authentication, authorization, TLS, secrets management, validation, least privilege and secure headers.
13. **Observability** — structured logs, correlation/trace IDs, metrics/tracing, liveness/readiness.
14. **Testing** — unit, integration, API and transaction tests according to risk.
15. **Contracts** — API/event contracts are versioned and breaking changes require impact analysis.
16. **Documentation** — C4/UML, ADRs and production runbooks.
17. **CI/CD** — build, test, quality/security gates, artifact and controlled deployment.
18. **AI governance** — AI must not invent requirements, DB objects, contracts or architecture decisions.
19. **Production readiness** — feature is Done only when specification, implementation, tests, security, observability, documentation and convergence are satisfied.

## Production Baseline

All services MUST implement standardized error handling, correlation propagation, health endpoints, security configuration, automated tests, dependency/security scanning, and a documented rollback path before production approval.

## Service Communication

Synchronous service-to-service calls MUST use IHttpClientFactory through typed clients. Asynchronous state-change integration MUST use an Outbox and idempotent consumers. Services MUST NOT access another service's database directly.

## Infrastructure Decisions

Identity provider, message broker, API gateway, secret manager, container platform, and observability backend MUST be selected through explicit architecture decisions when the corporate platform is known. The starter MUST NOT invent production infrastructure.
