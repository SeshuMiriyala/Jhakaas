IF( EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE [TYPE] = 'P' AND [Name] = 'AddNewPost'))
	DROP PROCEDURE AddNewPost;
	
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Seshu Miriyala
-- Create date: 04/02/2014
-- Description:	Stored Procedure to add new posts
-- =============================================
CREATE PROCEDURE AddNewPost
	@userId int,
	@leadId int,
	@activityCode varchar(50)
AS
BEGIN
	
	INSERT INTO Posts([ActivityId], [UserId], [LeadId]) 
	SELECT Activities.Id, @userId, @leadId
		FROM Activities
		WHERE ActivityCode = @activityCode
END
GO
