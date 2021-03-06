--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-04',N'01 sp_3_LoadUserClinicList.sql')
GO
--end HRA script header

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_3_LoadUserClinicList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_3_LoadUserClinicList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_3_LoadUserClinicList]
	@user_login nvarchar(50)
AS
BEGIN

SELECT     clinicID, clinicName, lkpClinics.defaultAssessmentType
FROM         tblUserProfiles INNER JOIN
                      tblUsers ON tblUserProfiles.userLogin = tblUsers.userLogin INNER JOIN
                      lkpClinics ON tblUserProfiles.userClinicID = lkpClinics.clinicID
WHERE tblUsers.userLogin = @user_login
END




