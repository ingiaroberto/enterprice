# ADR-0001 — Inter-service communication
Status: Proposed

Use HTTP/REST or gRPC for synchronous interactions and asynchronous events for decoupled workflows. All HTTP clients use IHttpClientFactory; typed clients are preferred. Outbox and idempotent consumers are required for reliable state-change events. Concrete broker selection remains an infrastructure decision.
