CREATE TABLE [dbo].[GPU] (
    [Id]         INT NOT NULL IDENTITY,
    [UsersID]    INT        NOT NULL,
    [TotalClock] FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

