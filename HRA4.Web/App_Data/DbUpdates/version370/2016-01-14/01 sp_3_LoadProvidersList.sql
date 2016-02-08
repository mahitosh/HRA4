--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-14',N'01 sp_3_LoadProvidersList.sql')
GO
--end HRA script header
/****** Object:  StoredProcedure [dbo].[sp_3_LoadProviderList]    Script Date: 11/13/2012 14:40:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_3_LoadProviderList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_3_LoadProviderList]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_3_LoadProviderList](
	@unitnum as nvarchar(50),
	@apptid int = null
) 
AS
BEGIN

IF (@apptid is null or @apptid <= 0)
BEGIN
	SET @apptid = dbo.fn_3_GetGoldenAppt(@unitnum)
END
	

SELECT     tblApptProviders.role, tblApptProviders.refPhys, lkpProviders.providerID, lkpProviders.title, lkpProviders.firstName, lkpProviders.middleName, 
                      lkpProviders.lastName, lkpProviders.degree, lkpProviders.institution, lkpProviders.address1, lkpProviders.address2, lkpProviders.city, 
                      lkpProviders.state, lkpProviders.zipcode, lkpProviders.country, lkpProviders.phone, lkpProviders.fax, lkpProviders.email, 
                      lkpProviders.nationalProviderID, lkpProviders.UPIN, lkpProviders.defaultRole, lkpProviders.fullName, lkpProviders.riskClinic, lkpProviders.dataSource, 
                      lkpProviders.localProviderID, lkpProviders.displayName, lkpProviders.isApptProvider, lkpProviders.uploadPath, lkpProviders.letterName, 
                      lkpProviders.letterTitle, lkpProviders.networkID, lkpProviders.photoPath, tblApptProviders.PCP,
                      providerPhotosPath
FROM         tblApptProviders INNER JOIN
                      lkpProviders ON tblApptProviders.providerID = lkpProviders.providerID CROSS JOIN
                      lkpGlobals
WHERE     (tblApptProviders.apptID = @apptid)

END




