USE [MasteringEFCoreDbFirst]
GO

/****** Object: Table [dbo].[Post] Script Date: 26-04-2017 08:28:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Post] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [BlogId]            INT            NOT NULL,
    [Content]           NVARCHAR (MAX) NULL,
    [PublishedDateTime] DATETIME  NOT NULL,
    [Title]             NVARCHAR (MAX) NOT NULL
);
GO

CREATE NONCLUSTERED INDEX [IX_Post_BlogId]
    ON [dbo].[Post]([BlogId] ASC);
GO

ALTER TABLE [dbo].[Post]
    ADD CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Post]
    ADD CONSTRAINT [FK_Post_Blog_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blog] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [Post] ([BlogId], [Title], [Content], [PublishedDateTime]) VALUES
(1, 'Dotnet 4.7 Released', 'Dotnet 4.7 Released Contents', '20170424'),
(2, '.NET Core 1.1 Released', '.NET Core 1.1 Released Contents', '20170424'),
(2, 'EF Core 1.1 Released', 'EF Core 1.1 Released Contents', '20170424')
GO