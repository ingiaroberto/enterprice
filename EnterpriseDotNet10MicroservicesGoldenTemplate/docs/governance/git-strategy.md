# Git & GitHub Enterprise Governance

## Repository model

The platform starts as a monorepo. Microservices retain strict boundaries and own their code, database scripts, tests, documentation and deployment assets.

## Branches

- `main`: production-ready code. Protected; no direct pushes.
- `develop`: integration branch when the corporate release model requires it.
- `feature/<WORKITEM>-<short-description>`: new functionality.
- `bugfix/<WORKITEM>-<short-description>`: non-production defect fixes.
- `hotfix/<WORKITEM>-<short-description>`: urgent production fixes.
- `release/<version>`: release stabilization when required.

## Pull requests

All changes enter protected branches through pull requests. Required checks should include build, tests, security and applicable architecture/specification gates.

## Commits

Use Conventional Commits:

`feat(scope): description`
`fix(scope): description`
`refactor(scope): description`
`test(scope): description`
`docs(scope): description`
`chore(scope): description`
`security(scope): description`
`ci(scope): description`

Include the corporate work-item ID where required, for example:

`feat(PUI-125): add report creation`

## Tags

Production releases use semantic version tags such as `v1.0.0`, subject to corporate release policy.

## Protected branches

Recommended GitHub rules for `main`:

- Pull request required.
- At least two approvals for production-sensitive repositories, subject to corporate policy.
- CODEOWNERS review required.
- Required CI checks.
- Conversations resolved.
- Force pushes disabled.
- Branch deletion disabled.
- Administrators subject to the same controls where feasible.

## Environments

Use GitHub Environments for `dev`, `qa`, `uat` and `prod`. Production should require explicit approval and use environment-scoped secrets.

## Secrets

Never commit credentials, private keys, tokens, connection strings or certificates. Use the corporate secret-management mechanism and GitHub Environment/Repository secrets only where approved.

## Database changes

Database scripts are versioned with the service that owns the database. Cross-service database access is prohibited.

## Specification traceability

A pull request must identify the corresponding specification and work item. The relationship is:

`Work Item → Specification → Plan → Tasks → Code → Tests → Pull Request → Release`
