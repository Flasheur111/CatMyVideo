USE master
GO

IF EXISTS(select * from sys.databases where name='CatMyVideo')
DROP DATABASE [CatMyVideo]
GO

CREATE DATABASE [CatMyVideo]
GO

USE [CatMyVideo]
GO

CREATE TABLE [dbo].[T_Tags](
	[name] [nvarchar](20) PRIMARY KEY NOT NULL)
GO

CREATE TABLE [dbo].[T_Users](
	[id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[nickname] [nvarchar](50) NOT NULL,
	[mail] [nvarchar](50) NOT NULL,
	[pass] [nvarchar](max) NOT NULL,
	[description] [nvarchar](144) NULL,
	[type] [int] NOT NULL)
GO

CREATE TABLE [dbo].[T_Videos](
	[id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[base_video] INT NOT NULL,
	[title] [varchar](50) NOT NULL,
	[description] [varchar](144) NOT NULL,
	[upload_date] [datetime] NOT NULL,
	[view_count] [bigint] NOT NULL,
	[quality] [int] NOT NULL,
	[is_encoded] [bit] NOT NULL,
	[uploader] INT NOT NULL,
	CONSTRAINT [FK_Videos_Users] FOREIGN KEY([uploader])
	REFERENCES [dbo].[T_Users] ([id]))
GO

CREATE TABLE [dbo].[T_Comments](
	[id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[message] [varchar](144) NOT NULL,
	[post_date] [datetime] NOT NULL,
	[author] INT NOT NULL,
	[video] INT NOT NULL,
	CONSTRAINT [FK_Comment_Users] FOREIGN KEY([author])
	REFERENCES [dbo].[T_Users] ([id]),
	CONSTRAINT [FK_Comment_Video] FOREIGN KEY([video])
	REFERENCES [dbo].[T_Videos] ([id])
	)
GO

CREATE TABLE [dbo].[T_VideosTags](
	[video] INT NOT NULL,
	[tag] [nvarchar](20) NOT NULL,
	CONSTRAINT [FK_VideosTags_Tags] FOREIGN KEY([tag])
	REFERENCES [dbo].[T_Tags] ([name]),
	CONSTRAINT [FK_VideosTags_Videos] FOREIGN KEY([video])
	REFERENCES [dbo].[T_Videos] ([id]))
GO
