/* RIDA Golden Template - REFERENCE SQL CONTRACT. Replace with approved corporate schema before production. */
IF DB_ID(N'RidaCatalog') IS NULL CREATE DATABASE RidaCatalog;
GO
USE RidaCatalog;
GO
IF OBJECT_ID(N'dbo.Category','U') IS NULL
CREATE TABLE dbo.Category
(
    Id uniqueidentifier NOT NULL CONSTRAINT PK_Category PRIMARY KEY,
    Name nvarchar(150) NOT NULL,
    Active bit NOT NULL,
    CreatedAtUtc datetime2(7) NOT NULL
);
GO
CREATE OR ALTER PROCEDURE dbo.usp_Catalog_Category_Create
    @Id uniqueidentifier,
    @Name nvarchar(150),
    @Active bit,
    @CreatedAtUtc datetime2(7)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.Category (Id, Name, Active, CreatedAtUtc)
    VALUES (@Id, @Name, @Active, @CreatedAtUtc);
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_Catalog_Category_GetById
    @CategoryId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Active, CreatedAtUtc
    FROM dbo.Category
    WHERE Id = @CategoryId;
END;
GO
