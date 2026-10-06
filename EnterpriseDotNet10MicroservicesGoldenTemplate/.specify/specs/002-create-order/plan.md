# Create Order Plan

1. Validate command through FluentValidation.
2. Resolve Catalog through `ICatalogPort`.
3. Create Domain Order.
4. Begin local UnitOfWork.
5. Execute `usp_Order_Create`.
6. Execute Outbox insert Stored Procedure.
7. Commit.
8. Publish pending Outbox records asynchronously through RabbitMQ.
9. Verify idempotent consumption through Inbox.
10. Validate unit/API/integration/contract tests.
