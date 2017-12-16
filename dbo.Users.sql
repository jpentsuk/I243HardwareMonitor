CREATE TABLE [dbo].[Users] (
    [UsersID] INT          IDENTITY (1, 1) NOT NULL,
    [CPU]     VARCHAR (50) NULL,
    [GPU]     VARCHAR (50) NULL,
    [HDD]     VARCHAR (50) NULL,
    [RAM]     VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([UsersID] ASC)
);

