--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-09',N'02 v_RelativeDetails.sql')
GO
--end HRA script header

/****** Object:  View [dbo].[v_RelativeDetails]    Script Date: 12/18/2015 16:19:03 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_RelativeDetails]'))
DROP VIEW [dbo].[v_RelativeDetails]
GO



/****** Object:  View [dbo].[v_RelativeDetails]    Script Date: 12/18/2015 16:19:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[v_RelativeDetails]
AS
SELECT     [dbo].fn_getRelativeRelationship(relationship, bloodLine) AS fullRelationship, [dbo].fn_getRelativeDiseases(apptID, relativeID) AS diseaseList, apptID, 
		[dbo].fn_getRelativeDiseasesMammo(apptID, relativeID) AS diseaseListMammo,
		[dbo].fn_getRelativeDiseasesForRiskModels(apptID, relativeID) AS diseaseListRiskModels
FROM         [dbo].tblRiskDataRelatives


GO

