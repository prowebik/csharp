﻿CREATE TABLE[dbo].[People] (
[Id] INT IDENTITY(1, 1) NOT NULL,
[FIO] NVARCHAR(MAX) NULL,
[Department] NVARCHAR(MAX) NULL,
CONSTRAINT[PK_dbo.People] PRIMARY KEY
CLUSTERED([Id] ASC)
);
