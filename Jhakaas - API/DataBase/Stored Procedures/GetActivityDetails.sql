IF( EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE [TYPE] = 'P' AND [Name] = 'GetActivityDetails'))
	DROP PROCEDURE GetActivityDetails;
	
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Seshu Miriyala
-- Create date: 04/02/2014
-- Description:	Stored Procedure to get the Activity Name and notification message
-- =============================================
CREATE PROCEDURE GetActivityDetails
	@activityCode varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT ActivityName + '#' + NotificationMessage + '#' + CASE IsNotificationEnabled WHEN 1  THEN 'true' ELSE 'false' END
		FROM  dbo.Activities
		WHERE ActivityCode = @activityCode
END
GO
