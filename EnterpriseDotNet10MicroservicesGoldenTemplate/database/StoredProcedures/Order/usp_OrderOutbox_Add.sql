CREATE OR ALTER PROCEDURE dbo.usp_OrderOutbox_Add
    @Id UNIQUEIDENTIFIER,
    @EventType NVARCHAR(200),
    @Payload NVARCHAR(MAX),
    @OccurredAtUtc DATETIME2(7)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.OrderOutboxMessage(Id, EventType, Payload, OccurredAtUtc)
    VALUES (@Id, @EventType, @Payload, @OccurredAtUtc);
END;
GO
