USE [MasteringEFCoreBlog]
GO

/****** Object: Table [dbo].[Blog] Script Date: 26-04-2017 08:28:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Blog] (
    [Id]  INT            IDENTITY (1, 1) NOT NULL,
    [Url] NVARCHAR (MAX) NULL
);
GO

INSERT INTO [Blog] (Url) VALUES
('http://blogs.packtpub.com/dotnet'),
('http://blogs.packtpub.com/dotnetcore'),
('http://blogs.packtpub.com/signalr')
GO