/* RIDA Golden Template - REFERENCE SQL CONTRACTS. Replace with approved corporate schema before production. */
IF DB_ID(N'RidaOrder') IS NULL CREATE DATABASE RidaOrder;
GO
USE RidaOrder;
GO
IF OBJECT_ID(N'dbo.[Order]','U') IS NULL CREATE TABLE dbo.[Order](Id uniqueidentifier NOT NULL PRIMARY KEY,CustomerReference nvarchar(100) NOT NULL,CreatedAtUtc datetime2(7) NOT NULL);
IF OBJECT_ID(N'dbo.OrderItem','U') IS NULL CREATE TABLE dbo.OrderItem(Id uniqueidentifier NOT NULL PRIMARY KEY,OrderId uniqueidentifier NOT NULL,ProductId uniqueidentifier NOT NULL,Quantity int NOT NULL,UnitPrice decimal(18,2) NOT NULL,CONSTRAINT FK_OrderItem_Order FOREIGN KEY(OrderId) REFERENCES dbo.[Order](Id));
IF OBJECT_ID(N'dbo.OutboxMessage','U') IS NULL CREATE TABLE dbo.OutboxMessage(Id uniqueidentifier NOT NULL PRIMARY KEY,Type nvarchar(200) NOT NULL,Payload nvarchar(max) NOT NULL,OccurredAtUtc datetime2(7) NOT NULL,PublishedAtUtc datetime2(7) NULL);
GO
CREATE OR ALTER PROCEDURE dbo.usp_Order_Create @Id uniqueidentifier,@CustomerReference nvarchar(100),@CreatedAtUtc datetime2(7) AS BEGIN SET NOCOUNT ON;INSERT dbo.[Order](Id,CustomerReference,CreatedAtUtc) VALUES(@Id,@CustomerReference,@CreatedAtUtc);END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_OrderItem_Create @Id uniqueidentifier,@OrderId uniqueidentifier,@ProductId uniqueidentifier,@Quantity int,@UnitPrice decimal(18,2) AS BEGIN SET NOCOUNT ON;INSERT dbo.OrderItem(Id,OrderId,ProductId,Quantity,UnitPrice) VALUES(@Id,@OrderId,@ProductId,@Quantity,@UnitPrice);END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_Outbox_Add @Id uniqueidentifier,@Type nvarchar(200),@Payload nvarchar(max),@OccurredAtUtc datetime2(7) AS BEGIN SET NOCOUNT ON;INSERT dbo.OutboxMessage(Id,Type,Payload,OccurredAtUtc) VALUES(@Id,@Type,@Payload,@OccurredAtUtc);END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_Outbox_GetPending @BatchSize int AS BEGIN SET NOCOUNT ON;SELECT TOP(@BatchSize) Id,Type,Payload,OccurredAtUtc FROM dbo.OutboxMessage WITH(READPAST,UPDLOCK,ROWLOCK) WHERE PublishedAtUtc IS NULL ORDER BY OccurredAtUtc,Id;END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_Outbox_MarkPublished @Id uniqueidentifier,@PublishedAtUtc datetime2(7) AS BEGIN SET NOCOUNT ON;UPDATE dbo.OutboxMessage SET PublishedAtUtc=@PublishedAtUtc WHERE Id=@Id AND PublishedAtUtc IS NULL;END;
GO
