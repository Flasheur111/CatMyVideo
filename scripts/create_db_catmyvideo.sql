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

CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](256) NOT NULL)
GO


INSERT INTO [dbo].[AspNetRoles]([Id], [Name])
VALUES ('c8f3cc2a-c021-4e7e-8a97-5e2f53b10ddf', 'Admin');


CREATE TABLE [dbo].[T_Users](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[nickname] [nvarchar](50) NOT NULL,
	[description] [nvarchar](144) NULL,
	[AspNetUsersId] [nvarchar](128) NOT NULL)
GO

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL PRIMARY KEY,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[T_UserId] [int] NOT NULL,
	CONSTRAINT [FK_T_Users] FOREIGN KEY([T_UserId])
	REFERENCES [dbo].[T_Users] ([id]))
GO

INSERT INTO [dbo].[T_Users] ([nickname], [description], [AspNetUsersId])
VALUES ('CatMyVideo', 'Official channel', '88b66e20cecb');

INSERT INTO [dbo].[AspNetUsers] ([Id],[Email],[EmailConfirmed],[PasswordHash], [SecurityStamp], [PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnabled],[AccessFailedCount],[UserName],[T_UserId])
VALUES ('88b66e20cecb', 'ceo@catmyvideo.com', 1, 'AEP3XIoC3L9dGvtyT8kHbyyDMliY1H/Dy3YkY97AG7IfHC5bisAo03jv5XafahTYYw==', '310db2fa-f2dd-403c-807c-271352c6679e', 0, 0, 1, 0, 'CatMyVideo', 1)
GO

ALTER TABLE [dbo].[T_Users]  WITH CHECK ADD  CONSTRAINT [FK_T_Users_AspNetUsers] FOREIGN KEY([AspNetUsersId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[T_Users] CHECK CONSTRAINT [FK_T_Users_AspNetUsers]
GO

CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	CONSTRAINT [FK_AspNetUsers] FOREIGN KEY([UserId])
	REFERENCES [dbo].[AspNetUsers] ([id]),
	CONSTRAINT [FK_AspNetRole] FOREIGN KEY([RoleId])
	REFERENCES [dbo].[AspNetRoles] ([id]),
	CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[AspNetUserRoles]([UserId], [RoleId])
VALUES ('88b66e20cecb', 'c8f3cc2a-c021-4e7e-8a97-5e2f53b10ddf');

CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	CONSTRAINT [FK_AspNetUsers_Logins] FOREIGN KEY([UserId])
	REFERENCES [dbo].[AspNetUsers] ([id]),
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[T_Videos](
	[id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[title] [varchar](50) NOT NULL,
	[description] [varchar](144) NOT NULL,
	[upload_date] [datetime] NOT NULL,
	[view_count] [bigint] NOT NULL,
	[uploader] INT NOT NULL,
	CONSTRAINT [FK_Videos_Users] FOREIGN KEY([uploader])
	REFERENCES [dbo].[T_Users] ([id]))
GO

CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	CONSTRAINT [FK_AspNetUsers_Claims] FOREIGN KEY([UserId])
	REFERENCES [dbo].[AspNetUsers] ([id]))
GO


CREATE TABLE [dbo].[T_Encode](
	[id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[video] INT NOT NULL,
	[is_base] [bit] NOT NULL,
	[is_encoded] [bit] NOT NULL,
	[quality][int] NOT NULL,
	CONSTRAINT [FK_Encode_Videos] FOREIGN KEY([video])
	REFERENCES [dbo].[T_Videos] ([id]))
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
