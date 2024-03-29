IF( EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE [TYPE] = 'P' AND [Name] = 'GetLeadById'))
	DROP PROCEDURE GetLeadById;
	
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
CREATE PROCEDURE GetLeadById
	@leadId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT Leads.Id, Leads.FirstName, Leads.MiddleName, Leads.LastName, Leads.Email, Leads.Phone, Leads.ImageUrl
		FROM  Leads
		WHERE Leads.Id = @leadId	
END
GO
