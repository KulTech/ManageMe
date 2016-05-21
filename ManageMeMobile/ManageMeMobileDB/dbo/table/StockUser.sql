CREATE TABLE [dbo].[StockUser]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserName] NVARCHAR(50) NULL, 
    [ticker] NVARCHAR(50) NULL, 
    [rdate] DATE NULL
)
