USE [LighterDB]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 07/23/2015 17:17:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [varchar](16) NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[Description] [varchar](128) NULL,
	[Authority] [varchar](1024) NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastDate] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Module]    Script Date: 07/23/2015 17:17:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Module](
	[Id] [varchar](16) NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[Catalog] [varchar](32) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastDate] [datetime] NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Department]    Script Date: 07/23/2015 17:17:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [varchar](16) NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[Description] [varchar](128) NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastDate] [datetime] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Account]    Script Date: 07/23/2015 17:17:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [varchar](16) NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[Password] [varchar](16) NOT NULL,
	[Authority] [varchar](1024) NULL,
	[ShortName] [varchar](16) NULL,
	[DepartId] [varchar](16) NULL,
	[RoleId] [varchar](16) NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastDate] [datetime] NULL,
	[IsLogin] [bit] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_Role_IsDeleted]    Script Date: 07/23/2015 17:17:22 ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Module_IsDeleted]    Script Date: 07/23/2015 17:17:22 ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Department_IsDeleted]    Script Date: 07/23/2015 17:17:22 ******/
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [DF_Department_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Account_IsDeleted]    Script Date: 07/23/2015 17:17:22 ******/
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Account_IsLogin]    Script Date: 07/23/2015 17:17:22 ******/
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_IsLogin]  DEFAULT ((0)) FOR [IsLogin]
GO
/****** Object:  ForeignKey [FK_Account_Department]    Script Date: 07/23/2015 17:17:22 ******/
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Department] FOREIGN KEY([DepartId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Department]
GO
/****** Object:  ForeignKey [FK_Account_Role]    Script Date: 07/23/2015 17:17:22 ******/
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
