/* RIDA Golden Template - REFERENCE SQL CONTRACT. Replace with approved corporate schema before production. */
IF DB_ID(N'RidaPayment') IS NULL CREATE DATABASE RidaPayment;
GO
USE RidaPayment;
GO
IF OBJECT_ID(N'dbo.Payment','U') IS NULL CREATE TABLE dbo.Payment(Id uniqueidentifier NOT NULL PRIMARY KEY,OrderId uniqueidentifier NOT NULL,Amount decimal(18,2) NOT NULL,CreatedAtUtc datetime2(7) NOT NULL);
GO
CREATE OR ALTER PROCEDURE dbo.usp_Payment_Payment_Create
    @Id uniqueidentifier,@OrderId uniqueidentifier,@Amount decimal(18,2),@CreatedAtUtc datetime2(7)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.Payment (Id,OrderId,Amount,CreatedAtUtc)
    VALUES (@Id,@OrderId,@Amount,@CreatedAtUtc);
END;
GO
