CREATE TABLE [dbo].[Shares] (
    [WordId]   INT NOT NULL,
    [TraderId] INT NOT NULL,
    [Quantity] INT NOT NULL,
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TraderId]) REFERENCES [dbo].[Traders] ([Id]),
    FOREIGN KEY ([WordId]) REFERENCES [dbo].[Words] ([Id]),
    CONSTRAINT [UC_Word_Trader] UNIQUE NONCLUSTERED ([WordId] ASC, [TraderId] ASC)
);

