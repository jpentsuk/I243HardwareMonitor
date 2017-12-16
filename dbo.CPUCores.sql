CREATE TABLE [dbo].[CPUCores] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [Core1Load] FLOAT (53) NULL,
    [Core2Load] FLOAT (53) NULL,
    [Core3Load] FLOAT (53) NULL,
    [Core4Load] FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

