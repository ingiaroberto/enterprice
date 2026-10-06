#!/usr/bin/env bash
set -euo pipefail

fail() { echo "ARCHITECTURE CHECK FAILED: $1" >&2; exit 1; }

if grep -R -nE 'PackageReference Include="(Dapper|Microsoft.Data.SqlClient|RabbitMQ.Client)"|using (Dapper|RabbitMQ\.Client|Microsoft\.Data\.SqlClient)' src/*Service.Domain src/*Service.Application 2>/dev/null; then
  fail "Domain/Application must not reference infrastructure frameworks or persistence/messaging implementations."
fi

test -f RIDA.Enterprise.sln || fail "RIDA.Enterprise.sln missing"
test -f .specify/memory/constitution.md || fail "Spec Kit constitution missing"
test -f docs/operations/production-readiness.md || fail "Production readiness gate missing"

for proc in usp_Order_Create usp_Outbox_Add usp_Outbox_GetPending usp_Outbox_MarkPublished usp_Catalog_Category_Create usp_Catalog_Category_GetById; do
  grep -R -q "CREATE OR ALTER PROCEDURE dbo.${proc}" database || fail "Missing stored procedure contract: ${proc}"
done

echo "Architecture preflight: PASS"
