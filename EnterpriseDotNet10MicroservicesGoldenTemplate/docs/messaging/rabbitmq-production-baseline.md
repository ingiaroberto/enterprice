# RabbitMQ Production Baseline

RabbitMQ is the selected asynchronous broker for this starter.

## Delivery semantics

The system uses **at-least-once** delivery. The Outbox is the source of truth until a message is marked published. A successful broker publish followed by a database update failure can result in a duplicate publication; consumers therefore use Inbox/idempotency.

## Topology

- Topic exchange: `enterprise.events`
- Retry exchange: `enterprise.events.retry`
- Dead-letter exchange: `enterprise.events.dlx`
- Durable consumer queues
- Retry queue with TTL and dead-letter routing back to the main exchange
- Dedicated DLQ for poison messages

## Operational rules

1. Do not use infinite `requeue=true` loops.
2. Consumer retry count is bounded by configuration.
3. Failed messages beyond the retry limit go to DLQ.
4. DLQ messages require operational inspection and replay procedures.
5. Production credentials must not be stored in source control.
6. Production topology, quorum/classic queue policy, HA, TLS, network placement and retention are platform decisions and must be recorded in an ADR before deployment.
