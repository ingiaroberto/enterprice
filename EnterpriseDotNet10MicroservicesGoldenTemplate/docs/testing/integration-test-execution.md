# Integration Test Execution

Integration tests are deliberately separated from unit and contract tests because they require real infrastructure.

## Order Service

1. Start local infrastructure:

```bash
docker compose -f deploy/docker/docker-compose.local.yml up -d
```

2. Apply the database scripts in this order:

```text
database/Order_InitialSchema.sql
database/Order_ReferenceSchema.sql
database/StoredProcedures/Order/usp_Outbox_Add.sql
database/StoredProcedures/Order/usp_Outbox_GetPending.sql
database/StoredProcedures/Order/usp_Outbox_MarkPublished.sql
database/StoredProcedures/Order/Inbox contract (consumer implementation pending).sql
```

3. Set `ORDER_TEST_CONNECTION_STRING`.
4. Execute:

```bash
dotnet test RIDA.Enterprise.sln --filter Category=Integration
```

The integration test fails when the required environment variable is absent. This prevents CI from silently reporting a skipped infrastructure test as success.
