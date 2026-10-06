# Create Order Tasks

## Implementation

- [x] Command and validator.
- [x] FluentValidation MediatR pipeline behavior.
- [x] Domain Order aggregate.
- [x] Catalog output port and HTTP adapter.
- [x] Order repository adapter.
- [x] UnitOfWork.
- [x] Outbox adapter.
- [x] RabbitMQ publisher with publisher confirms.
- [x] RabbitMQ durable topology.
- [x] Retry queue with bounded retries.
- [x] Dead-letter queue.
- [x] Inbox idempotency.
- [x] ProblemDetails error contract.
- [x] Rate limiting.
- [x] Liveness/readiness health checks.

## Tests

- [x] Unit success path.
- [x] Unit missing/inactive category paths.
- [x] Event contract test.
- [x] HTTP client contract test.
- [x] SQL integration smoke test harness.
- [x] RabbitMQ integration smoke test harness.
- [ ] Execute integration tests against real infrastructure.
- [ ] Execute end-to-end CreateOrder → Outbox → RabbitMQ → Consumer flow.
- [ ] Execute performance test.
- [ ] Execute security test.

## Spec Kit quality gates

- [ ] `speckit-clarify` executed against the corporate requirements.
- [ ] `speckit-analyze` executed and clean result recorded.
- [ ] `speckit-implement` executed in the target development environment.
- [ ] `speckit-converge` executed until Converged.

The unchecked items require a configured development/CI environment or corporate requirements; they are intentionally not fabricated as completed evidence.
