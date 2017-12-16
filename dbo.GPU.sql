CREATE TABLE [dbo].[GPU] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [TotalLoad] FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

