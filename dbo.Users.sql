﻿CREATE TABLE [dbo].[Users] (
    [UsersID] INT          NOT NULL IDENTITY,
    [CPU]     VARCHAR (50) NOT NULL,
    [GPU]     VARCHAR (50) NOT NULL,
    [HDD]     VARCHAR (50) NOT NULL,
    [RAM]     VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([UsersID] ASC)
);
