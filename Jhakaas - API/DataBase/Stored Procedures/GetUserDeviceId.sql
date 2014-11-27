IF( EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE [TYPE] = 'P' AND [Name] = 'GetUserDeviceId'))
	DROP PROCEDURE GetUserDeviceId;
	
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Seshu Miriyala
-- Create date: 04/02/2014
-- Description:	Stored Procedure to get the User Device Id 
--				to send the notifications on User Id
-- =============================================
CREATE PROCEDURE GetUserDeviceId
	@userId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT DeviceId
		FROM  Users
		WHERE (Users.Id = @userId OR @userId IS NULL)
END
GO
