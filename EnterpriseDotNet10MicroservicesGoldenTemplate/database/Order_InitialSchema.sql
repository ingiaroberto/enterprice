IF OBJECT_ID('dbo.[Order]','U') IS NULL
BEGIN
    CREATE TABLE dbo.[Order]
    (
        Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Order PRIMARY KEY,
        CategoryId UNIQUEIDENTIFIER NOT NULL,
        CreatedAtUtc DATETIME2(7) NOT NULL
    );
END;
GO

IF OBJECT_ID('dbo.OrderOutboxMessage','U') IS NULL
BEGIN
    CREATE TABLE dbo.OrderOutboxMessage
    (
        Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_OrderOutboxMessage PRIMARY KEY,
        EventType NVARCHAR(200) NOT NULL,
        Payload NVARCHAR(MAX) NOT NULL,
        OccurredAtUtc DATETIME2(7) NOT NULL,
        PublishedAtUtc DATETIME2(7) NULL
    );
    CREATE INDEX IX_OrderOutboxMessage_Pending ON dbo.OrderOutboxMessage(PublishedAtUtc, OccurredAtUtc);
END;
GO

IF OBJECT_ID('dbo.InboxMessage','U') IS NULL
BEGIN
    CREATE TABLE dbo.InboxMessage
    (
        MessageId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_InboxMessage PRIMARY KEY,
        MessageType NVARCHAR(200) NOT NULL,
        ReceivedAtUtc DATETIME2(7) NOT NULL
    );
END;
GO
