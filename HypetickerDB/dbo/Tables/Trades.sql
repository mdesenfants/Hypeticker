CREATE TABLE [dbo].[Trades] (
    [Id]       INT      IDENTITY (1, 1) NOT NULL,
    [TraderId] INT      NULL,
    [WordId]   INT      NOT NULL,
    [Created]  DATETIME DEFAULT (getutcdate()) NOT NULL,
    [Expires]  DATETIME DEFAULT (dateadd(hour,(1),getutcdate())) NOT NULL,
    [Type]     INT      NOT NULL,
    [Price]    INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TraderId]) REFERENCES [dbo].[Traders] ([Id]),
    FOREIGN KEY ([WordId]) REFERENCES [dbo].[Traders] ([Id])
);

