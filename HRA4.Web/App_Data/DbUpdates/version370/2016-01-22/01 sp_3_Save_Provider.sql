--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-22',N'01 sp_3_Save_Provider.sql')
GO
--end HRA script header
/****** Object:  StoredProcedure [dbo].[sp_3_LoadProviderList]    Script Date: 11/13/2012 14:40:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_3_Save_Provider]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_3_Save_Provider]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_3_Save_Provider] 
	@user nvarchar(100) = null,
	@providerID int output,
	@title nvarchar(10) = null,
	@firstName nvarchar(50) = null,
	@middleName nvarchar(50) = null,
	@lastName nvarchar(50) = null,
	@degree nvarchar(70) = null,
	@institution nvarchar(200) = null,
	@address1 nvarchar(100) = null,
	@address2 nvarchar(100) = null,
	@city nvarchar(50) = null,
	@state nvarchar(2) = null,
	@zipcode nvarchar(13) = null,
	@country nvarchar(50) = null,
	@phone nvarchar(70) = null,
	@fax nvarchar(20) = null,
	@email nvarchar(100) = null,
	@nationalProviderID nvarchar(50) = null,
	@UPIN nvarchar(50) = null,
	@defaultRole nvarchar(50) = null,
	@fullName nvarchar(150) = null,
	@riskClinic int = null,
	@dataSource nvarchar(100) = null,
	@localProviderID nvarchar(100) = null,
	@displayName nvarchar(100) = null,
	@isApptProvider nvarchar(10) = null,
	@uploadPath nvarchar(255) = null,
	@letterName nvarchar(500) = null,
	@letterTitle nvarchar(500) = null,
	@networkID nvarchar(50) = null,
	@photoPath nvarchar(500) = null,
	@cellphone nvarchar(20) = null,
	@pager nvarchar(20) = null,
	@comments nvarchar(max) = null,
	@suffixName nvarchar(50) = null,
	@webURL nvarchar(100) = null,
	@GT1 nvarchar(50) = null,
	@GT2 nvarchar(50) = null,
	@GT3 nvarchar(50) = null,
	@GT4 nvarchar(50) = null,
	@professionalTitle nvarchar(500) = null,
	@associatedNurses nvarchar(500) = null,
	@academicTitle nvarchar(500) = null,
	@footerText nvarchar(500) = null,
	@clinic nvarchar(500) = null,
	@academicSite nvarchar(500) = null,
	@professionalSite nvarchar(500) = null,
	@showEmail bit = null,
	@isDuplicate nvarchar(1) = null,
	@documentStoragePath nvarchar(500) = null,
	@webSite nvarchar(500) = null,
	
	@apptid int = null,
	@PCP bit = null,
	@refPhys bit = null,
	@delete bit = 0
	
AS
BEGIN
	begin transaction

		IF (@delete = 1)
		BEGIN
			DELETE FROM tblApptProviders WHERE apptID = @apptid and providerID = @providerID
		END
		IF(@delete = 0)
		BEGIN
			if (@providerID <= 0)
			BEGIN

				SELECT @providerID=ISNULL(MAX(providerID),0)  FROM lkpProviders 
				SET @providerID=@providerID+1
											
				INSERT INTO lkpProviders (providerID) VALUES     (@providerID)

			END

			IF (NOT(@city IS NULL)) BEGIN UPDATE lkpProviders SET city = @city WHERE providerID = @providerID END
			IF (NOT(@title  IS NULL)) BEGIN UPDATE lkpProviders SET title  = @title  WHERE providerID = @providerID END
			IF (NOT(@firstName  IS NULL)) BEGIN UPDATE lkpProviders SET firstName  = @firstName  WHERE providerID = @providerID END
			IF (NOT(@middleName  IS NULL)) BEGIN UPDATE lkpProviders SET middleName  = @middleName  WHERE providerID = @providerID END
			IF (NOT(@lastName  IS NULL)) BEGIN UPDATE lkpProviders SET lastName  = @lastName  WHERE providerID = @providerID END
			IF (NOT(@degree  IS NULL)) BEGIN UPDATE lkpProviders SET degree  = @degree  WHERE providerID = @providerID END
			IF (NOT(@institution  IS NULL)) BEGIN UPDATE lkpProviders SET institution  = @institution  WHERE providerID = @providerID END
			IF (NOT(@address1  IS NULL)) BEGIN UPDATE lkpProviders SET address1  = @address1  WHERE providerID = @providerID END
			IF (NOT(@address2  IS NULL)) BEGIN UPDATE lkpProviders SET address2  = @address2  WHERE providerID = @providerID END
			IF (NOT(@city  IS NULL)) BEGIN UPDATE lkpProviders SET city  = @city  WHERE providerID = @providerID END
			IF (NOT(@state  IS NULL)) BEGIN UPDATE lkpProviders SET state  = @state  WHERE providerID = @providerID END
			IF (NOT(@zipcode  IS NULL)) BEGIN UPDATE lkpProviders SET zipcode  = @zipcode  WHERE providerID = @providerID END
			IF (NOT(@country  IS NULL)) BEGIN UPDATE lkpProviders SET country  = @country  WHERE providerID = @providerID END
			IF (NOT(@phone  IS NULL)) BEGIN UPDATE lkpProviders SET phone  = @phone  WHERE providerID = @providerID END
			IF (NOT(@fax  IS NULL)) BEGIN UPDATE lkpProviders SET fax  = @fax  WHERE providerID = @providerID END
			IF (NOT(@email  IS NULL)) BEGIN UPDATE lkpProviders SET email  = @email  WHERE providerID = @providerID END
			IF (NOT(@nationalProviderID  IS NULL)) BEGIN UPDATE lkpProviders SET nationalProviderID  = @nationalProviderID  WHERE providerID = @providerID END
			IF (NOT(@UPIN  IS NULL)) BEGIN UPDATE lkpProviders SET UPIN  = @UPIN  WHERE providerID = @providerID END
			IF (NOT(@defaultRole  IS NULL)) BEGIN UPDATE lkpProviders SET defaultRole  = @defaultRole  WHERE providerID = @providerID END
			IF (NOT(@fullName  IS NULL)) BEGIN UPDATE lkpProviders SET fullName  = @fullName  WHERE providerID = @providerID END
			IF (NOT(@riskClinic  IS NULL)) BEGIN UPDATE lkpProviders SET riskClinic  = @riskClinic  WHERE providerID = @providerID END
			IF (NOT(@dataSource  IS NULL)) BEGIN UPDATE lkpProviders SET dataSource  = @dataSource  WHERE providerID = @providerID END
			IF (NOT(@localProviderID  IS NULL)) BEGIN UPDATE lkpProviders SET localProviderID  = @localProviderID  WHERE providerID = @providerID END
			IF (NOT(@displayName  IS NULL)) BEGIN UPDATE lkpProviders SET displayName  = @displayName  WHERE providerID = @providerID END
			IF (NOT(@isApptProvider  IS NULL)) BEGIN UPDATE lkpProviders SET isApptProvider  = @isApptProvider  WHERE providerID = @providerID END
			IF (NOT(@uploadPath  IS NULL)) BEGIN UPDATE lkpProviders SET uploadPath  = @uploadPath  WHERE providerID = @providerID END
			IF (NOT(@letterName  IS NULL)) BEGIN UPDATE lkpProviders SET letterName  = @letterName  WHERE providerID = @providerID END
			IF (NOT(@letterTitle  IS NULL)) BEGIN UPDATE lkpProviders SET letterTitle  = @letterTitle  WHERE providerID = @providerID END
			IF (NOT(@networkID  IS NULL)) BEGIN UPDATE lkpProviders SET networkID  = @networkID  WHERE providerID = @providerID END
			IF (NOT(@photoPath  IS NULL)) BEGIN UPDATE lkpProviders SET photoPath  = @photoPath  WHERE providerID = @providerID END
			IF (NOT(@cellphone  IS NULL)) BEGIN UPDATE lkpProviders SET cellphone  = @cellphone  WHERE providerID = @providerID END
			IF (NOT(@pager  IS NULL)) BEGIN UPDATE lkpProviders SET pager  = @pager  WHERE providerID = @providerID END
			IF (NOT(@comments  IS NULL)) BEGIN UPDATE lkpProviders SET comments  = @comments  WHERE providerID = @providerID END
			IF (NOT(@suffixName  IS NULL)) BEGIN UPDATE lkpProviders SET suffixName  = @suffixName  WHERE providerID = @providerID END
			IF (NOT(@webURL  IS NULL)) BEGIN UPDATE lkpProviders SET webURL  = @webURL  WHERE providerID = @providerID END
			IF (NOT(@GT1  IS NULL)) BEGIN UPDATE lkpProviders SET GT1  = @GT1  WHERE providerID = @providerID END
			IF (NOT(@GT2  IS NULL)) BEGIN UPDATE lkpProviders SET GT2  = @GT2  WHERE providerID = @providerID END
			IF (NOT(@GT3  IS NULL)) BEGIN UPDATE lkpProviders SET GT3  = @GT3  WHERE providerID = @providerID END
			IF (NOT(@GT4  IS NULL)) BEGIN UPDATE lkpProviders SET GT4  = @GT4  WHERE providerID = @providerID END
			IF (NOT(@professionalTitle  IS NULL)) BEGIN UPDATE lkpProviders SET professionalTitle  = @professionalTitle  WHERE providerID = @providerID END
			IF (NOT(@associatedNurses  IS NULL)) BEGIN UPDATE lkpProviders SET associatedNurses  = @associatedNurses  WHERE providerID = @providerID END
			IF (NOT(@academicTitle  IS NULL)) BEGIN UPDATE lkpProviders SET academicTitle  = @academicTitle  WHERE providerID = @providerID END
			IF (NOT(@footerText  IS NULL)) BEGIN UPDATE lkpProviders SET footerText  = @footerText  WHERE providerID = @providerID END
			IF (NOT(@clinic  IS NULL)) BEGIN UPDATE lkpProviders SET clinic  = @clinic  WHERE providerID = @providerID END
			IF (NOT(@academicSite  IS NULL)) BEGIN UPDATE lkpProviders SET academicSite  = @academicSite  WHERE providerID = @providerID END
			IF (NOT(@professionalSite  IS NULL)) BEGIN UPDATE lkpProviders SET professionalSite  = @professionalSite  WHERE providerID = @providerID END
			IF (NOT(@showEmail  IS NULL)) BEGIN UPDATE lkpProviders SET showEmail  = @showEmail  WHERE providerID = @providerID END
			IF (NOT(@isDuplicate  IS NULL)) BEGIN UPDATE lkpProviders SET isDuplicate  = @isDuplicate  WHERE providerID = @providerID END
			IF (NOT(@documentStoragePath  IS NULL)) BEGIN UPDATE lkpProviders SET documentStoragePath  = @documentStoragePath  WHERE providerID = @providerID END
			IF (NOT(@webSite  IS NULL)) BEGIN UPDATE lkpProviders SET webSite  = @webSite  WHERE providerID = @providerID END

			IF (@apptid > 0)
			BEGIN
				DELETE FROM tblApptProviders where apptID = @apptid and providerID = @providerID
				INSERT INTO tblApptProviders (apptID, providerID, role, refPhys, PCP) values (@apptID, @providerID, @defaultRole, @refPhys, @PCP)
			END
		END		
	commit

	SELECT @providerID
END





