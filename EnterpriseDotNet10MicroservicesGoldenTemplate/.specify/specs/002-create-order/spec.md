# Feature Specification: Create Order

## Goal
Create an order only when the referenced Catalog category exists and is active.

## Functional requirements
- Validate `CategoryId`.
- Query Catalog through the application port.
- Reject missing category.
- Reject inactive category.
- Persist Order and Outbox message atomically in the Order database.
- Return the created order identifier and category identifier.
- Return standardized ProblemDetails for validation and known application errors.

## Integration requirements
- Publish `order.created.v1` through RabbitMQ after the local transaction commits.
- Publisher uses at-least-once semantics and publisher confirms.
- Consumers must be idempotent through Inbox.
- Consumer failures use bounded retry and DLQ; infinite requeue is prohibited.
- HTTP dependency calls use the typed client and cancellation.

## Non-functional requirements
- Structured logging and correlation.
- Authentication/authorization according to environment policy.
- Production startup must fail when authentication is not configured.
- Rate limiting.
- Bounded resilience for synchronous dependency calls.
- Liveness and readiness health endpoints.
- No sensitive credentials in source control for production.

## Out of scope
- Selection of a corporate IdP.
- Selection of a cloud/Kubernetes platform.
- Production RabbitMQ hosting topology.
- Production secret-management platform.
- Production observability exporter/backend.
