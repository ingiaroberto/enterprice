# ADR-003 — Inter-Service Communication

## Status
Accepted for the starter baseline; production hosting remains subject to corporate platform governance.

## Decision
Use synchronous HTTP/REST through typed `IHttpClientFactory` clients with .NET resilience handlers when an immediate response is required. Use RabbitMQ for asynchronous integration events through an Outbox/Inbox-capable messaging design.

## Rules
- No direct cross-service database access.
- No distributed transactions.
- Retries must be safe for the operation.
- Consumers must be idempotent.
- Business data and its Outbox record must commit atomically.
- RabbitMQ exchanges and queues must be durable for production workloads.
- Message contracts are versioned and use stable routing keys.
- Production credentials must come from the approved secret-management mechanism.
- Production HA, networking, retention, monitoring and operational ownership are infrastructure decisions.
