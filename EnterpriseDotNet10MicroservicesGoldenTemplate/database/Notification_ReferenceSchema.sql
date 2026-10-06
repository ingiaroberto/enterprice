/* RIDA Golden Template - REFERENCE SQL CONTRACT. Replace with approved corporate schema before production. */
IF DB_ID(N'RidaNotification') IS NULL CREATE DATABASE RidaNotification;
GO
USE RidaNotification;
GO
IF OBJECT_ID(N'dbo.Notification','U') IS NULL CREATE TABLE dbo.Notification(Id uniqueidentifier NOT NULL PRIMARY KEY,Recipient nvarchar(320) NOT NULL,Message nvarchar(4000) NOT NULL,CreatedAtUtc datetime2(7) NOT NULL);
GO
CREATE OR ALTER PROCEDURE dbo.usp_Notification_Notification_Create
    @Id uniqueidentifier,@Recipient nvarchar(320),@Message nvarchar(4000),@CreatedAtUtc datetime2(7)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.Notification (Id,Recipient,Message,CreatedAtUtc)
    VALUES (@Id,@Recipient,@Message,@CreatedAtUtc);
END;
GO
