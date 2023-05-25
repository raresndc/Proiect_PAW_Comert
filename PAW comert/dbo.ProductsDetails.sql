CREATE TABLE [dbo].[Desfaceri] (
    [Id]          INT          NOT NULL,
    [ProductName] VARCHAR (50) NULL,
    [Amount]      INT          NULL,
    [Price]       INT          NULL,
    [Section]     VARCHAR (50) NULL,
    [Market]      VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

