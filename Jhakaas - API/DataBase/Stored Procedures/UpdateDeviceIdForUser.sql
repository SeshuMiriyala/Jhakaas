IF( EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE [TYPE] = 'P' AND [Name] = 'UpdateDeviceIdForUser'))
	DROP PROCEDURE UpdateDeviceIdForUser;
	
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
CREATE PROCEDURE UpdateDeviceIdForUser
	@uniquePhoneId varchar(100),
	@deviceId varchar(MAX)
AS
BEGIN
	IF(EXISTS (SELECT 1 FROM [NotificationSettings] WHERE [UniqueId] = @uniquePhoneId))
		UPDATE [NotificationSettings] SET [DeviceId] = @deviceId WHERE [UniqueId] = @uniquePhoneId
	ELSE
		INSERT INTO [NotificationSettings] ([UniqueID], [DeviceId]) VALUES (@uniquePhoneId, @deviceId)
END
GO
