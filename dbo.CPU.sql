﻿CREATE TABLE [dbo].[CPU] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [UsersID]    INT        NOT NULL,
    [TotalClock] FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
