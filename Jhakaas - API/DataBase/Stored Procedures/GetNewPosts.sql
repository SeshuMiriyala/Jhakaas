IF( EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE [TYPE] = 'P' AND [Name] = 'GetNewPosts'))
	DROP PROCEDURE GetNewPosts;
	
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Seshu Miriyala
-- Create date: 03/30/2014
-- Description:	Stored procedure to get the new posts
-- =============================================
CREATE PROCEDURE GetNewPosts 
	@lastPostCreatedOn DateTime = NULL,
	@maximumPostsCount INT = -1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF (@maximumPostsCount != -1)
		SET ROWCOUNT @maximumPostsCount;

    SELECT	Posts.Id AS PostId, 
			Activities.Id AS ActivityId, 
			Activities.ActivityName, 
			Activities.ActivityImageUrl,
			Activities.IsNotificationEnabled, 
			Users.Id AS UserId, 
			Users.FirstName AS UserFirstName, 
			Users.MiddleName AS UserMiddleName, 
			Users.LastName AS UserLastName, 
            Roles.RoleName AS UserRole, 
            Users.Avatar AS UserAvatarUrl, 
            Leads.Id AS LeadId, 
            Leads.FirstName AS LeadFirstName, 
            Leads.MiddleName AS LeadMiddleName, 
            Leads.LastName AS LeadLastName, 
            Leads.ImageUrl AS LeadAvatarUrl, 
            Leads.Email AS LeadEmail, 
            Leads.Phone AS LeadPhone, 
			Posts.IsRead,
            DATEADD(hour, 12.5, Posts.[CreatedOn]) AS PostCreatedOn
		FROM   Posts INNER JOIN
               Activities ON Posts.ActivityId = Activities.Id INNER JOIN
               Leads ON Posts.LeadId = Leads.Id INNER JOIN
               Users ON Posts.UserId = Users.Id INNER JOIN
               Roles ON Users.RoleId = Roles.Id
		WHERE	(DATEDIFF(ms,@lastPostCreatedOn, Posts.CreatedOn) > 0 OR @lastPostCreatedOn IS NULL)
		ORDER BY	PostCreatedOn Desc
END
GO
