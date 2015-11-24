CREATE TABLE [dbo].[Documents]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(250) NULL, 
    [Path] NVARCHAR(250) NULL, 
    [Date] DATE NULL, 
    [Notes] NVARCHAR(500) NULL
)
