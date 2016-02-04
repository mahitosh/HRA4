--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-19',N'03 sp_getInterfaceDefinitionFromTemplateID')
GO
--end HRA script header--
/****** Object:  StoredProcedure [dbo].[sp_getInterfaceDefinitionFromTemplateID]    Script Date: 01/19/2016 16:16:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getInterfaceDefinitionFromTemplateID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getInterfaceDefinitionFromTemplateID]
GO


/****** Object:  StoredProcedure [dbo].[sp_getInterfaceDefinitionFromTemplateID]    Script Date: 01/19/2016 16:16:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_getInterfaceDefinitionFromTemplateID] 
	-- Add the parameters for the stored procedure here
	@documentTemplateID	int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT     lkp_AutomationDocuments.InterfaceId, lkpInterfaceDefinitions.InterfaceType, lkpInterfaceDefinitions.ipAddress, lkpInterfaceDefinitions.port, 
                      lkpInterfaceDefinitions.stringParam1, lkpInterfaceDefinitions.stringParam2, lkpInterfaceDefinitions.stringParam3, lkpInterfaceDefinitions.intParam1, 
                      lkpInterfaceDefinitions.intParam2, lkpInterfaceDefinitions.stringParam4, lkpInterfaceDefinitions.stringParam5, lkpInterfaceDefinitions.stringParam6,lkpInterfaceDefinitions.intParam3, 
                      lkpInterfaceDefinitions.intParam4, lkpInterfaceDefinitions.intParam5, lkpInterfaceDefinitions.intParam6
FROM         lkp_AutomationDocuments INNER JOIN
                      lkpInterfaceDefinitions ON lkp_AutomationDocuments.InterfaceId = lkpInterfaceDefinitions.InterfaceId
WHERE     (lkp_AutomationDocuments.documentTemplateID = @documentTemplateID)
END

GO


