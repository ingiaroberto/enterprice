# Order Service Runbook

## Health

- `/health/live`: process/liveness only.
- `/health/ready`: SQL Server and RabbitMQ dependencies.

## Outbox backlog

1. Inspect pending rows in `OrderOutboxMessage`.
2. Verify RabbitMQ connectivity.
3. Verify exchange and routing key topology.
4. Inspect publisher logs using `CorrelationId`/`MessageId`.
5. Do not delete pending messages as a first response.

## DLQ

1. Inspect the DLQ message and failure reason.
2. Determine whether the failure is transient or poison data.
3. Fix the consumer/application issue before replay.
4. Replay only through the approved operational procedure.
5. Preserve the original message identifier for traceability.

## Database incident

1. Check `/health/ready`.
2. Verify SQL connectivity and connection pool saturation.
3. Review stored procedure errors and lock/blocking conditions.
4. Check whether transactions are being rolled back.
5. Escalate using the corporate DBA process.
