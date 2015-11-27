CREATE TABLE [dbo].[Documents]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(250) NULL, 
    [Date] DATE NULL, 
    [Notes] NVARCHAR(500) NULL, 
	[PropertyId] int NULL,
    [fileContent] VARBINARY(MAX) NULL
)
