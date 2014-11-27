IF (EXISTS (SELECT * FROM SYS.OBJECTS WHERE [TYPE] = 'U' AND [Name] = 'Users'))
	DROP TABLE [Users]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 04/07/2014 23:20:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Avatar] [varchar](100) NULL,
	[Phone] [bigint] NULL,
	[UniquePhoneId] [varchar](100) NULL,
	[DeviceId] [varchar](100) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_LastModifiedOn]  DEFAULT (getdate()) FOR [LastModifiedOn]
GO


