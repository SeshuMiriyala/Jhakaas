IF (EXISTS (SELECT * FROM SYS.OBJECTS WHERE [TYPE] = 'U' AND [Name] = 'Posts'))
	DROP TABLE [Posts]
GO


/****** Object:  Table [dbo].[Posts]    Script Date: 04/09/2014 23:36:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[LeadId] [int] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Leads_Users] FOREIGN KEY([LeadId])
REFERENCES [dbo].[Leads] ([Id])
GO

ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Leads_Users]
GO

ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Activities] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activities] ([Id])
GO

ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Activities]
GO

ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Users]
GO

ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [DF_Posts_IsRead]  DEFAULT ((0)) FOR [IsRead]
GO

ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [DF_Posts_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO


