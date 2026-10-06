# Production Readiness Gate

This repository is a production-oriented reference implementation. Production approval remains an explicit gate and cannot be inferred from source code alone.

## Architecture

- [x] Catalog Service uses Clean Architecture boundaries.
- [x] Order Service uses Hexagonal Architecture / Ports & Adapters.
- [x] Domain has no infrastructure framework dependency.
- [x] Order local transaction uses UnitOfWork.
- [x] Cross-service database access is prohibited.

## Persistence

- [x] Dapper is isolated in Infrastructure.
- [x] SQL persistence is represented by Stored Procedures.
- [x] Order + Outbox are persisted in the same local transaction.
- [x] Inbox uses a unique message identifier for idempotency.
- [x] Integration test exists for database/schema reachability.

## Messaging

- [x] RabbitMQ is the selected asynchronous broker for this starter.
- [x] Durable topic exchange.
- [x] Publisher confirms enabled for Outbox publishing.
- [x] At-least-once publication semantics documented.
- [x] Consumer idempotency via Inbox.
- [x] Retry queue with TTL.
- [x] Dead-letter queue.
- [x] Retry limit configurable.
- [x] RabbitMQ integration smoke test exists.

## API and security

- [x] FluentValidation pipeline behavior.
- [x] RFC 7807-style ProblemDetails responses.
- [x] Correlation ID propagation.
- [x] Rate limiting baseline.
- [x] JWT/OIDC configuration point.
- [x] Production startup fails when authentication is not configured.
- [ ] Corporate IdP, scopes, claims and authorization policies approved.
- [ ] Production secrets supplied through the approved secret manager.
- [ ] Production TLS/certificate policy approved.

## Observability

- [x] Structured logging baseline.
- [x] Correlation ID.
- [x] Liveness endpoint.
- [x] Readiness endpoint for SQL Server and RabbitMQ.
- [ ] Platform-approved OpenTelemetry collector/exporter configuration.
- [ ] Production dashboards and alert thresholds approved.

## Testing

- [x] Unit tests for success and failure paths.
- [x] Event contract test.
- [x] HTTP client contract test.
- [x] Integration test harness for SQL Server.
- [x] Integration test harness for RabbitMQ.
- [ ] Integration tests executed against a real environment.
- [ ] Full end-to-end test executed.
- [ ] Performance/load test completed.
- [ ] Security penetration test completed.

## Delivery

- [x] CI restore/build/test pipeline.
- [x] Architecture preflight.
- [x] Dependency/security scanning baseline.
- [x] Container build and vulnerability scan workflow.
- [x] Non-root container runtime.
- [ ] Corporate registry, signing and provenance policy approved.
- [ ] Deployment platform/IaC approved.
- [ ] Rollback executed successfully in a controlled environment.
- [ ] Disaster recovery and backup/restore test approved.

## Spec-Driven Development

- [x] Constitution.
- [x] Specification.
- [x] Plan.
- [x] Tasks.
- [x] Contract artifact.
- [ ] `analyze` executed by the configured Spec Kit agent and clean result recorded.
- [ ] `implement` executed in the target development environment.
- [ ] `converge` executed until no remaining work is reported.

The remaining unchecked items are environment/governance gates, not missing architectural components in this starter.
