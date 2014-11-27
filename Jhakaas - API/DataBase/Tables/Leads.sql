IF (EXISTS (SELECT * FROM SYS.OBJECTS WHERE [TYPE] = 'U' AND [Name] = 'Leads'))
	DROP TABLE [Leads]
GO

/****** Object:  Table [dbo].[Leads]    Script Date: 04/08/2014 03:14:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Leads](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NOT NULL,
	[ImageUrl] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [bigint] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedBy] [int] NOT NULL,
	[LastModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Leads] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Leads] ADD  CONSTRAINT [DF_Leads_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Leads] ADD  CONSTRAINT [DF_Leads_LastModifiedOn]  DEFAULT (getdate()) FOR [LastModifiedOn]
GO





