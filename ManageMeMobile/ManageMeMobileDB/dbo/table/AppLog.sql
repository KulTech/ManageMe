﻿CREATE TABLE [dbo].[AppLog]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LogDate] DATETIME NOT NULL, 
    [msg] VARCHAR(2500) NULL
)
