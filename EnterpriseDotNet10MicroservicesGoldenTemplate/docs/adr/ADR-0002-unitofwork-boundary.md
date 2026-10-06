# ADR-0002 — UnitOfWork boundary
Status: Accepted

UnitOfWork is local to one microservice/database transaction. Repositories participating in one atomic use case share the same connection and transaction. Cross-service consistency uses events/Outbox rather than distributed SQL transactions.
