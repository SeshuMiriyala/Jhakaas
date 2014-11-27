IF( EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE [TYPE] = 'P' AND [Name] = 'GetUserById'))
	DROP PROCEDURE GetUserById;
	
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Seshu Miriyala
-- Create date: 03/30/2014
-- Description:	Stored Procedure to get the User based on User Id
-- =============================================
CREATE PROCEDURE GetUserById
	@userId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT Users.Id, Users.FirstName, Users.MiddleName, Users.LastName, Users.Email, Users.Avatar, Roles.RoleName
		FROM  Users INNER JOIN
           Roles ON Users.RoleId = Roles.Id
		WHERE Users.Id = @userId	
END
GO
