CREATE TABLE [dbo].[CPUCores] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [UsersID]   INT        NOT NULL,
    [Core1Load] FLOAT (53) NOT NULL,
    [Core2Load] FLOAT (53) NOT NULL,
    [Core3Load] FLOAT (53) NOT NULL,
    [Core4Load] FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_CPUCores_ToUsers] FOREIGN KEY (UsersID) REFERENCES Users(UsersID)
);

