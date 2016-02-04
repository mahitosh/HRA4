--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-19',N'01 sp_getInterfaceDefinitionForApptMWL')
GO
--end HRA script header--
/****** Object:  StoredProcedure [dbo].[sp_getInterfaceDefinitionForApptMWL]    Script Date: 01/19/2016 14:35:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getInterfaceDefinitionForApptMWL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getInterfaceDefinitionForApptMWL]
GO

/****** Object:  StoredProcedure [dbo].[sp_getInterfaceDefinitionForApptMWL]    Script Date: 01/19/2016 14:35:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_getInterfaceDefinitionForApptMWL] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT     InterfaceID, InterfaceType, ipAddress, port, stringParam1, stringParam2, stringParam3, intParam1, intParam2, stringParam4, stringParam5
FROM         lkpInterfaceDefinitions
WHERE     InterfaceType = 'ApptMWL'
END


GO


