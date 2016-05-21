CREATE TABLE [dbo].[Vendors]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [location] NVARCHAR(250) NULL, 
    [email] NVARCHAR(50) NULL
)
