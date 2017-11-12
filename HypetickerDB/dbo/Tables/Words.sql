CREATE TABLE [dbo].[Words] (
    [Word]   NVARCHAR (50) NOT NULL,
    [Public] DATETIME      DEFAULT (getutcdate()) NOT NULL,
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Word] ASC)
);

