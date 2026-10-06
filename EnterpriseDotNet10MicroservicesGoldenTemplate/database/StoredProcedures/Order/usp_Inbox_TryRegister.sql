CREATE OR ALTER PROCEDURE dbo.usp_Inbox_TryRegister
    @MessageId UNIQUEIDENTIFIER,
    @MessageType NVARCHAR(200),
    @ReceivedAtUtc DATETIME2(7)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.InboxMessage(MessageId,MessageType,ReceivedAtUtc)
        VALUES(@MessageId,@MessageType,@ReceivedAtUtc);
        SELECT 1;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() IN (2601,2627)
            SELECT 0;
        ELSE
            THROW;
    END CATCH;
END;
GO
