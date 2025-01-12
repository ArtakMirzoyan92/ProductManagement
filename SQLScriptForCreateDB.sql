 --    SQL Script

 --1) Create DB TestDb
CREATE DATABASE TestDb;
GO

USE TestDb
GO

--2) Table Product
CREATE TABLE Product 
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [Name] NVARCHAR(255) NOT NULL UNIQUE,
    [Description] NVARCHAR(MAX)
);
GO

-- Index for field Name
CREATE NONCLUSTERED INDEX IX_Product_Name ON Product ([Name])
WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
GO

--3) Table ProductVersion
CREATE TABLE ProductVersion 
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ProductId UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX),
    CreatingDate DATETIME DEFAULT GETDATE() NOT NULL,
    Width FLOAT NOT NULL,
    Height FLOAT NOT NULL,
    [Length] FLOAT NOT NULL,
    CONSTRAINT FK_ProductVersion_Product 
        FOREIGN KEY (ProductId) REFERENCES Product(Id)
        ON DELETE CASCADE
);
GO

-- Indexes for fields Name, CreatingDate, Width, Height ? Length
CREATE NONCLUSTERED INDEX IX_ProductVersion_Name ON ProductVersion ([Name])
WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);

CREATE NONCLUSTERED INDEX IX_ProductVersion_CreatingDate ON ProductVersion (CreatingDate)
WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);

CREATE NONCLUSTERED INDEX IX_ProductVersion_Width ON ProductVersion (Width)
WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);

CREATE NONCLUSTERED INDEX IX_ProductVersion_Height ON ProductVersion (Height)
WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);

CREATE NONCLUSTERED INDEX IX_ProductVersion_Length ON ProductVersion ([Length])
WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
GO

--4) Table EventLog
CREATE TABLE EventLog 
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EventDate DATETIME DEFAULT GETDATE() NOT NULL,
    [Description] NVARCHAR(MAX)
);
GO

-- Index for field EventDate
CREATE NONCLUSTERED INDEX IX_EventLog_EventDate ON EventLog(EventDate) 
WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON);
GO

--5) Trigger Product
--      Insert
CREATE TRIGGER Tr_Insert_Product
ON Product
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        INSERT INTO EventLog ([Description])
        SELECT 'Trigger for Product INSERT: Id - ' + TRY_CAST(Id as NVARCHAR(36)) + 'Name - ' + [Name]
		FROM Inserted
    END
END;
GO

--     Update
CREATE TRIGGER Tr_Update_Product
ON Product
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        INSERT INTO EventLog ([Description])
        SELECT 'Trigger for Product UPDATE: Id - ' + TRY_CAST(Id as NVARCHAR(36)) + '; Name - ' + [Name]
		FROM Inserted
    END
END;
GO

--     Delete
CREATE TRIGGER Tr_Delete_Product
ON Product
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

		IF EXISTS (SELECT * FROM deleted)
    BEGIN
        INSERT INTO EventLog ([Description])
        SELECT 'Trigger for Product DELETE: Id - ' + TRY_CAST(Id as NVARCHAR(36)) + '; Name - ' + [Name]
		FROM deleted
    END
END;
GO

--   Trigger ProductVersion

--     Insert   
CREATE TRIGGER Tr_Insert_ProductVersion
ON ProductVersion
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

	     IF EXISTS (SELECT * FROM inserted)
    BEGIN
        INSERT INTO EventLog ([Description])
        SELECT 'Trigger for ProductVersion INSERT: Id - ' + TRY_CAST(Id as NVARCHAR(36)) + '; Name - ' + [Name]
		FROM inserted
    END		     
END
GO

--     Update
CREATE TRIGGER Tr_Update_ProductVersion
ON ProductVersion
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	
	    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        INSERT INTO EventLog ([Description])
        SELECT 'Trigger for ProductVersion UPDATE: Id - ' + TRY_CAST(Id as NVARCHAR(36)) + '; Name - ' + [Name]
		FROM inserted
    END
END
GO

--     Delete
CREATE TRIGGER Tr_Delete_ProductVersion
ON ProductVersion
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
	
	    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        INSERT INTO EventLog ([Description])
        SELECT 'Trigger for ProductVersion DELETE: Id - ' + TRY_CAST(Id as NVARCHAR(36)) + '; Name - ' + [Name]
		FROM deleted
    END
END
GO

-- 6) Function

CREATE FUNCTION fn_GetProductVersions
(
    @ProductName NVARCHAR(255),
    @ProductVersionName NVARCHAR(255),
    @MinVolume FLOAT,
    @MaxVolume FLOAT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        pv.ID AS VersionID,
        p.[Name] AS ProductName,
        pv.[Name] AS VersionName,
        pv.Width,
        pv.Height,
        pv.[Length]
    FROM Product p
    INNER JOIN 
        ProductVersion pv ON p.ID = pv.ProductID
    WHERE 
        p.[Name] LIKE '%' + @ProductName + '%' AND
        pv.[Name] LIKE '%' + @ProductVersionName + '%' AND
        (pv.Width * pv.Height * pv.[Length]) BETWEEN @MinVolume AND @MaxVolume
);
GO

--    Test data

INSERT INTO Product (Name, Description)
VALUES ('Product 1', 'Description for Product 1'),
       ('Product 2', 'Description for Product 2'),
       ('Product 3', 'Description for Product 3');


INSERT INTO ProductVersion (ProductId, Name, Description, Width, Height, Length)
VALUES
    
    ((SELECT Id FROM Product WHERE Name = 'Product 1'), 'Version 1', 'Description for Version 1', 10.5, 20.5, 30.5),
    ((SELECT Id FROM Product WHERE Name = 'Product 1'), 'Version 2', 'Description for Version 2', 15.2, 25.3, 35.7),
   
    ((SELECT Id FROM Product WHERE Name = 'Product 2'), 'Version 1', 'Description for Version 1', 8.2, 18.4, 28.6),
    ((SELECT Id FROM Product WHERE Name = 'Product 2'), 'Version 2', 'Description for Version 2', 12.9, 22.6, 32.8),
   
    ((SELECT Id FROM Product WHERE Name = 'Product 3'), 'Version 1', 'Description for Version 1', 7.1, 17.3, 27.5),
    ((SELECT Id FROM Product WHERE Name = 'Product 3'), 'Version 2', 'Description for Version 2', 9.8, 19.6, 29.4);

GO

--   Table User
CREATE TABLE Users
(

	Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	UserName NVARCHAR(30) NOT NULL,
	FirstName NVARCHAR(100) NOT NULL,
	Email NVARCHAR(30) NOT NULL,
	City NVARCHAR(100) NULL,
	PasswordHash NVARCHAR(200) NOT NULL,
)

CREATE UNIQUE INDEX IX_Users_UserName ON dbo.Users (UserName);
CREATE UNIQUE INDEX IX_Users_Email ON dbo.Users (Email);

GO

