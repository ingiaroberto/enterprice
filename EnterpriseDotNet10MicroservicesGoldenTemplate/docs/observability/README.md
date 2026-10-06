# Observability Baseline

Every service must expose logs, metrics and traces. CorrelationId is propagated across HTTP and represented in messaging metadata; TraceId is preferred for distributed tracing. Logs must include service/environment/correlation identifiers and never secrets.

Health endpoints:
- `/health/live`: process liveness.
- `/health/ready`: readiness according to deployment policy.

The concrete telemetry backend remains a corporate platform decision.
