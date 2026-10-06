# ADR-004: Production Security and Observability Baseline

## Status
Accepted as starter baseline; provider-specific details remain deployment decisions.

## Decision
All microservices expose health endpoints, propagate a correlation identifier, use standardized Problem Details, and support OAuth2/OIDC configuration through environment-specific settings. Security and dependency scanning are enforced in CI.

## Consequences
The starter is production-oriented without coupling the business code to a specific identity provider or observability vendor.
