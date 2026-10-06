# Repository Standards

1. The repository is production-oriented; defaults must be secure.
2. No direct push to `main`.
3. No secrets in Git.
4. No cross-microservice database access.
5. No production code without a traceable specification.
6. No new public contract without compatibility review.
7. No architecture-changing implementation without an ADR.
8. No database object may be invented by an implementation agent.
9. CI is a merge gate, not a post-merge activity.
10. Every production change must be reproducible from Git and its release artifact.
