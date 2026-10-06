# ADR-0003 — Repository and Git Governance

## Status
Accepted as starter baseline; adapt to corporate GitHub policy before production adoption.

## Context
The platform contains multiple .NET 10 microservices and must provide traceability from requirements through production releases.

## Decision
Start with a monorepo. Enforce protected branches, pull requests, CODEOWNERS, automated CI/security checks, conventional commits and semantic release tags. Keep microservice boundaries explicit within the monorepo.

## Consequences
The team gets centralized governance and easier cross-service changes. Repository size and CI duration must be monitored. If organizational scale later requires polyrepo, the migration must be documented through an ADR.
