CREATE TABLE [dbo].[Properties]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [Name] NVARCHAR(50) NULL, 
    [Address1] NVARCHAR(250) NULL, 
    [City] NVARCHAR(50) NULL, 
    [State] NVARCHAR(50) NULL
)
