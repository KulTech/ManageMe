CREATE TABLE [dbo].[SubTypes]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [SubTypeName] NVARCHAR(50) NOT NULL, 
    [ETypeId] INT NOT NULL, 
    CONSTRAINT [FK_SubTypes_ETypes] FOREIGN KEY ([ETypeId]) REFERENCES [ExpenseType]([id])
)
