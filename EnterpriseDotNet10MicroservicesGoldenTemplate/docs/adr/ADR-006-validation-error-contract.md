# ADR-006 — Validation and Error Contract

## Status
Accepted.

## Decision

Application validation runs through a MediatR pipeline behavior using FluentValidation. API exceptions are translated to ProblemDetails with a stable application `code` and `correlationId`.

Known business/application errors use explicit codes such as `CATEGORY_NOT_FOUND`, `CATEGORY_INACTIVE` and `CATEGORY_ALREADY_EXISTS`.

Unexpected exceptions return `INTERNAL_ERROR` without exposing implementation details.

## Consequences

- Clients receive predictable HTTP error shapes.
- Validation is centralized instead of repeated in controllers.
- Sensitive exception details are not exposed to clients.
