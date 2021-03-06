USE [LighterDB]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 2015/7/20 21:50:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Code] [varchar](6) NOT NULL,
	[Name] [varchar](16) NOT NULL,
	[Password] [varchar](16) NOT NULL,
	[Modules] [varchar](128) NULL,
	[ShortName] [varchar](16) NULL,
	[DepartCode] [varchar](10) NULL,
	[gwcode] [varchar](50) NULL,
	[khxh] [int] NULL,
	[loadflag] [varchar](1) NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Account_IsDeleted]  DEFAULT ((0)),
	[LastDate] [datetime] NULL,
	[IsLogin] [bit] NOT NULL CONSTRAINT [DF_Account_IsLogin]  DEFAULT ((0)),
 CONSTRAINT [PK_Account] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Account_DepartCode]    Script Date: 2015/7/20 21:50:19 ******/
CREATE CLUSTERED INDEX [IX_Account_DepartCode] ON [dbo].[Account]
(
	[DepartCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2015/7/20 21:50:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[Description] [varchar](128) NULL,
	[loadflag] [varchar](1) NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastDate] [datetime] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Account_Code]    Script Date: 2015/7/20 21:50:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Account_Code] ON [dbo].[Account]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Account_Name]    Script Date: 2015/7/20 21:50:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Account_Name] ON [dbo].[Account]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Department_code]    Script Date: 2015/7/20 21:50:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Department_code] ON [dbo].[Department]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Department_Name]    Script Date: 2015/7/20 21:50:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Department_Name] ON [dbo].[Department]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [DF_Department_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Department] FOREIGN KEY([DepartCode])
REFERENCES [dbo].[Department] ([Code])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Department]
GO
USE [master]
GO
ALTER DATABASE [LighterDB] SET  READ_WRITE 
GO
