IF (EXISTS (SELECT * FROM SYS.OBJECTS WHERE [TYPE] = 'U' AND [Name] = 'Activities'))
	DROP TABLE [Activities]
GO

/****** Object:  Table [dbo].[Activities]    Script Date: 04/02/2014 14:41:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Activities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityName] [varchar](50) NOT NULL,
	[ActivityCode] [varchar](50) NOT NULL,
	[ActivityImageUrl] [varchar](50) NULL,
	[IsNotificationEnabled] [bit] NOT NULL,
	[NotificationMessage] [varchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Activities] ADD  CONSTRAINT [DF_Activities_IsNotificationEnabled]  DEFAULT ((0)) FOR [IsNotificationEnabled]
GO

ALTER TABLE [dbo].[Activities] ADD  CONSTRAINT [DF_Activities_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Activities] ADD  CONSTRAINT [DF_Activities_LastModifiedOn]  DEFAULT (getdate()) FOR [LastModifiedOn]
GO





