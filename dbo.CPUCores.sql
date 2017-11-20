CREATE TABLE [dbo].[CPUCores]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UsersID] INT NOT NULL, 
    [Core1Load] FLOAT NOT NULL, 
    [Core2Load] FLOAT NOT NULL, 
    [Core3Load] FLOAT NOT NULL, 
    [Core4Load] FLOAT NOT NULL
)
