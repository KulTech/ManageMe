CREATE TABLE [dbo].[Documents]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATE NOT NULL, 
    [Notes] NVARCHAR(500) NULL, 
	[PropertyId] int NOT NULL,
    [fileContent] VARBINARY(MAX) NULL, 
    [amount] MONEY NOT NULL, 
    [typeid] INT NOT NULL, 
    [updateDate] DATETIME NOT NULL DEFAULT getdate(), 
    [VendorId] INT NULL, 
    CONSTRAINT [FK_Documents_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Properties](Id), 
    CONSTRAINT [FK_Documents_SubTypes_SubTypeId] FOREIGN KEY ([typeid]) REFERENCES [SubTypes]([Id]), 
    CONSTRAINT [FK_Documents_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendors]([Id])
)

GO

--CREATE unique INDEX [IX_Documents_unique_vendor_date_amount] ON [dbo].[Documents] ([Date],amount,VendorId)
