CREATE TABLE [dbo].[CPUCores]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UsersID] INT NOT NULL, 
    [Core1Load] FLOAT NULL, 
    [Core2Load] FLOAT NULL, 
    [Core3Load] FLOAT NULL, 
    [Core4Load] FLOAT NULL, 
    CONSTRAINT [FK_CPUCores_ToUsers] FOREIGN KEY (UsersID) REFERENCES Users(UsersID)
)
