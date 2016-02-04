--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-20',N'04 sp_createApptFromSchedService.sql')
GO
--end HRA script header
/****** Object:  StoredProcedure [dbo].[sp_createApptFromSchedService]    Script Date: 01/20/2016 16:26:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_createApptFromSchedService]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_createApptFromSchedService]
GO

/****** Object:  StoredProcedure [dbo].[sp_createApptFromSchedService]    Script Date: 01/20/2016 16:26:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_createApptFromSchedService] 
	-- Add the parameters for the stored procedure here
	@apptdate nvarchar(max),
	@appttime nvarchar(max),
	@firstname nvarchar(max),
	@lastname nvarchar(max),
	@gender nvarchar(max),
	@dob nvarchar(max),
	@email nvarchar(max) = '',
	@address1 nvarchar(max) = '',
	@city nvarchar(max) = '',
	@state nvarchar(max) = '',
	@zip nvarchar(max) = '',
	@phone nvarchar(max) = '',
	@emailaddress nvarchar(max) = '',
	@sendByEmail int = '',
	@machineName nvarchar(max) = '',
	@preferredInstitutionName nvarchar(max) = '',
	@clinicID int = 1,
    @unitnum nvarchar(50) = '',
    @createdBy nvarchar(1024) = 'Schedule Service',
	@insertApptWebRecord bit = 0,
	@surveyid int = 160,
	@defaultScreenType nvarchar(50) = '',
	@visitnum nvarchar(50) = '',
	@apptphysname nvarchar(75) = NULL,
	@pcpName nvarchar(75) = '',
	@referral nvarchar(50) = '',
	@pcpLocalID int = 0,
	@refPhysLocalID int = 0

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @latestApptId int

	SELECT @latestApptId = (SELECT     MAX(apptid) 
						FROM         tblAppointments
						GROUP BY unitnum, apptdate, clinicID
						HAVING      (unitnum = @unitnum) AND (clinicID = @clinicID) AND (apptdate = @apptdate))

	IF (@latestApptId > 0)
		BEGIN
			return @latestApptId	 -- do not keep re-adding appointments
		END
	
	begin transaction
		
		SET NOCOUNT ON;
		
		DECLARE @now datetime
		DECLARE @strRiskID nvarchar(50)
		DECLARE @strSurveyType nvarchar(50)
		SELECT @now = GETDATE()

		DECLARE @name nvarchar(max)
		IF @lastname = NULL AND @firstname = NULL
		BEGIN 
			SELECT @name = NULL
		END
		ELSE SELECT @name = @lastname + ', ' + @firstname

		-- creat the appointment record and then get the apptid value
		
		IF (LEN(@defaultScreenType) = 0)
		BEGIN
			SELECT @defaultScreenType =(select defaultAssessmentType from lkpclinics where clinicID = @clinicID)
		END
		
		select @strSurveyType = (select surveyName from lkpSurveys where surveyID = @surveyid)
		
		INSERT INTO [tblAppointments] (apptdate,	appttime,	patientname,	address1,	city,	state,	zip,	homephone,	gender,		dob,	surveyType,		clinicID,	surveyID,	unitnum,	clinic,				visitnum,	apptphysname,	pcpname,	referral)
		VALUES (					   @apptdate,	@appttime,	@name,	        @address1,	@city,	@state, @zip,	@phone,		@gender,	@dob,	@strSurveyType, @clinicID,	@surveyid,	@unitnum,	@defaultScreenType, @visitnum,	@apptphysname,	@pcpName,	@referral)
			
		DECLARE @newApptId	int
		SELECT @newApptId = (SELECT SCOPE_IDENTITY())
		
	commit
	
	
		--INSERT INTO [tblapptProviders] (apptID, providerID, role,refPhys,PCP) VALUES (@newApptid,9532,'Internal Medicine',1,1)
		--INSERT INTO tblApptProviders(apptID, providerID, role,refPhys,PCP) VALUES(@newApptId,10847,'Radiation Oncologist',0,0)
		--INSERT INTO tblApptProviders(apptID, providerID, role,refPhys,PCP) VALUES(@newApptId,10849,'Plastic Surgeon',0,0)
		--INSERT INTO tblApptProviders(apptID, providerID, role,refPhys,PCP) VALUES(@newApptId,9672,'Medical Oncologist',0,0)
		
	-- now update tblApptProviders if we can jdg 1/5/16
	declare @pcpID int = 0
	IF (@newApptId > 0)		-- sanity check
	 BEGIN
		IF (@refPhysLocalID > 0)
			BEGIN
				declare @refID int = 0
				select @refID = (select providerID from lkpProviders where localProviderID = @refPhysLocalID)
				if (@refID > 0)
					BEGIN
						INSERT INTO tblApptProviders(apptID, providerID, role,refPhys,PCP) VALUES(@newApptId,@refID,'Referring Provider',1,0)
						if (@pcpLocalID > 0)
						BEGIN
							select @pcpID = (select providerID from lkpProviders where localProviderID = @pcpLocalID)
							IF (@pcpID > 0)
							BEGIN
								IF (@pcpID = @refID)
									BEGIN
										UPDATE tblApptProviders SET PCP = 1 where apptID = @newApptId
									END
								ELSE
									BEGIN
										INSERT INTO tblApptProviders(apptID, providerID, role,refPhys,PCP) VALUES(@newApptId,@pcpID,'Primary Care Provider',0,1)
									END
							END
						END
					END
				ELSE
					BEGIN
						IF (@pcpLocalID > 0)
						BEGIN
							select @pcpID = (select providerID from lkpProviders where localProviderID = @pcpLocalID)
							IF (@pcpID > 0)
							BEGIN
								INSERT INTO tblApptProviders(apptID, providerID, role,refPhys,PCP) VALUES(@newApptId,@pcpID,'Primary Care Provider',0,1)
							END
						END
					END
			END
		ELSE
			BEGIN
				IF (@pcpLocalID > 0)
					BEGIN
						select @pcpID = (select providerID from lkpProviders where localProviderID = @pcpLocalID)
						IF (@pcpID > 0)
						BEGIN
							INSERT INTO tblApptProviders(apptID, providerID, role,refPhys,PCP) VALUES(@newApptId,@pcpID,'Primary Care Provider',0,1)
						END
					END
			END
	 END
	--	-- now create the tblApptWeb record
	if @insertApptWebRecord = 1
		BEGIN
			INSERT INTO tblApptWeb (apptID, machineName, submissionDate, accepted, deleted, sendLetterByEmail, preferredInstitutionName)
			VALUES (@newApptId, @machineName, @now, 0, 0, @sendByEmail,@preferredInstitutionName)
		END   

	RETURN @newApptId
END












GO


