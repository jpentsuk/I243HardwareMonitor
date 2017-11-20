CREATE TABLE [dbo].[HDD] (
    [Id]      INT        IDENTITY (1, 1) NOT NULL,
    [UsersID] INT        NOT NULL,
    [Load]    FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_HDD_ToUsers] FOREIGN KEY (UsersID) REFERENCES Users(UsersID)
);

