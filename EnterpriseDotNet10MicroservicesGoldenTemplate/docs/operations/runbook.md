# Production Runbook

Health: `/health/live`, `/health/ready`.

First checks: deployment, readiness, correlation/trace ID, dependency health, database/SP failures, Outbox backlog where applicable.

Rollback: use the approved deployment artifact and enterprise release process. Destructive database changes require a forward-compatible migration strategy.
