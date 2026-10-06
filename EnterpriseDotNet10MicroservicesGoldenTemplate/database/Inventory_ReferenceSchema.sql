/* RIDA Golden Template - REFERENCE SQL CONTRACT. Replace with approved corporate schema before production. */
IF DB_ID(N'RidaInventory') IS NULL CREATE DATABASE RidaInventory;
GO
USE RidaInventory;
GO
IF OBJECT_ID(N'dbo.InventoryItem','U') IS NULL CREATE TABLE dbo.InventoryItem(Id uniqueidentifier NOT NULL PRIMARY KEY,ProductId uniqueidentifier NOT NULL,Quantity int NOT NULL,UpdatedAtUtc datetime2(7) NOT NULL);
GO
CREATE OR ALTER PROCEDURE dbo.usp_Inventory_InventoryItem_Create
    @Id uniqueidentifier,@ProductId uniqueidentifier,@Quantity int,@UpdatedAtUtc datetime2(7)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.InventoryItem (Id,ProductId,Quantity,UpdatedAtUtc)
    VALUES (@Id,@ProductId,@Quantity,@UpdatedAtUtc);
END;
GO
