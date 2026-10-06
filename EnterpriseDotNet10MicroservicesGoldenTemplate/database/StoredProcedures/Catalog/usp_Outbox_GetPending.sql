CREATE OR ALTER PROCEDURE dbo.usp_Outbox_GetPending
    @BatchSize INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (@BatchSize) Id,EventType,Payload,OccurredAtUtc
    FROM dbo.OutboxMessage WITH (READPAST, UPDLOCK)
    WHERE PublishedAtUtc IS NULL
    ORDER BY OccurredAtUtc,Id;
END;
GO
