# Security Baseline

- HTTPS redirection enabled.
- HSTS enabled outside Development.
- Security response headers enabled.
- JWT/OIDC configuration supported through `Authentication:Authority` and `Authentication:Audience`.
- API endpoints require authentication outside Development once the corporate IdP configuration is present.
- Production startup fails when authentication configuration is absent.
- Secrets must be supplied through environment/secret-management infrastructure and never committed.
- Rate limiting is enabled on API controllers.
- ProblemDetails responses do not expose stack traces or database details.
- Dependency and container security scans run in CI.

Corporate authorization policies, scopes, claims, TLS termination, secret manager and network controls remain environment-specific and require approved ADRs.
