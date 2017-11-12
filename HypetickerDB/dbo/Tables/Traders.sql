CREATE TABLE [dbo].[Traders] (
    [Email]   VARCHAR (200) NOT NULL,
    [Name]    VARCHAR (200) NOT NULL,
    [Created] DATETIME      DEFAULT (getutcdate()) NOT NULL,
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Cash]    INT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [UC_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);

