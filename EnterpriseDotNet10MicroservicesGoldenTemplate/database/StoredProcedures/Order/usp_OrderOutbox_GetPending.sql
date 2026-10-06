CREATE OR ALTER PROCEDURE dbo.usp_OrderOutbox_GetPending
    @BatchSize INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@BatchSize)
        Id,
        EventType,
        Payload,
        OccurredAtUtc
    FROM dbo.OrderOutboxMessage
    WHERE PublishedAtUtc IS NULL
    ORDER BY OccurredAtUtc;
END;
GO
