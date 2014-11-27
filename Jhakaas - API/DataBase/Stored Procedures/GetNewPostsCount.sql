IF( EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE [TYPE] = 'P' AND [Name] = 'GetNewPostsCount'))
	DROP PROCEDURE GetNewPostsCount;
	
GO
SET ANSI_NULLS ON
GO


SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Seshu Miriyala
-- Create date: 03/30/2014
-- Description:	Stored Procedure to get the count of new posts
-- =============================================
CREATE PROCEDURE GetNewPostsCount 
	@lastPostCreatedOn DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT COUNT(Id) FROM  Posts WHERE DATEDIFF(ms,@lastPostCreatedOn, CreatedOn) > 0
END
GO
