IF (EXISTS (SELECT * FROM SYS.OBJECTS WHERE [TYPE] = 'U' AND [Name] = 'Roles'))
	DROP TABLE [Roles]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 03/31/2014 00:38:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_LastModifiedOn]  DEFAULT (getdate()) FOR [LastModifiedOn]
GO


