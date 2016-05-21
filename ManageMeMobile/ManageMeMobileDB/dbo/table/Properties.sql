CREATE TABLE [dbo].[Properties]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Address1] NVARCHAR(250) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [State] NVARCHAR(50) NOT NULL
)
