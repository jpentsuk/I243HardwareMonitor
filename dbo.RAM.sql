CREATE TABLE [dbo].[RAM] (
    [Id]      INT        IDENTITY (1, 1) NOT NULL,
    [UsersID] INT        NOT NULL,
    [InUse]   FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

