# Inter-service communication

Synchronous: HTTP/REST or gRPC when an immediate response is required. HTTP clients use IHttpClientFactory; typed clients are preferred. Cancellation, authentication, tracing, bounded timeouts and operation-aware resilience are required.

Asynchronous: message broker for decoupled workflows. Business state and Outbox record are committed in one local transaction. Consumers use Inbox/idempotency.

Prohibited: direct cross-service DB access, default distributed transactions, `new HttpClient()` in application code, blind retries for non-idempotent operations.
