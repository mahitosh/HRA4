--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-05',N'05 sp_3_Save_Person.sql')
GO
--end HRA script header

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_3_Save_Person]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_3_Save_Person]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_3_Save_Person] 
	@patientUnitnum nvarchar(50),
    @relativeID int,
    @user nvarchar(100),

	@delete bit= 0,

	@name nvarchar(100) = NULL,
	@lastName nvarchar(100) = NULL,
	@firstName nvarchar(100) = NULL,
	@middleName nvarchar(100) = NULL,
	@maidenName nvarchar(100) = NULL,
	@address1 nvarchar(100) = NULL,
	@address2 nvarchar(100) = NULL,
	@city nvarchar(50) = NULL,
	@state nvarchar(2) = NULL,
	@zip nvarchar(13) = NULL,
	@country nvarchar(100) = NULL,
	@homephone nvarchar(50) = NULL,
	@workphone nvarchar(50) = NULL,
	@cellphone nvarchar(50) = NULL,
	@dob nvarchar(10) = NULL,
	@maritalstatus nvarchar(50) = NULL,
	@religion nvarchar(100) = NULL,
	@contactname nvarchar(50) = NULL,
	@contacthomephone nvarchar(20) = NULL,
	@contactworkphone nvarchar(20) = NULL,
	@contactcellphone nvarchar(20) = NULL,
	@occupation nvarchar(50) = NULL,
	@emailAddress nvarchar(50) = NULL,
	@educationLevel nvarchar(50) = NULL,

	@motherID int = NULL,
	@fatherID int = NULL,
	@gender nvarchar(50) = NULL,
	@relationship nvarchar(50) = NULL,
	@relationshipOther nvarchar(50) = NULL,
	@bloodline nvarchar(10) = NULL,
	@age nvarchar(20) = NULL,
	@vitalStatus nvarchar(10) = NULL,
	@comment nvarchar(250) = NULL,

	@title nvarchar(50) = NULL,
	@suffix nvarchar(50) = NULL,
	@twinID int = NULL,
	@twinType nvarchar(50) = NULL,
	@adopted nvarchar(20) = NULL,
	@dobConfidence nvarchar(50) = NULL,
	@dateOfDeath nvarchar(10) = NULL,
	@causeOfDeath nvarchar(100) = NULL,
	@dateOfDeathConfidence nvarchar(50) = NULL,

	@x_position float = NULL,
	@y_position float = NULL,
	@x_norm int = NULL,
	@y_norm int = NULL,
	@pedigreeGroup int = NULL,
	
	@isAshkenazi nvarchar(100) = NULL,
	@isHispanic nvarchar(100) = NULL,
	
	@family_comment nvarchar(max) = null,

	
	@consanguineousSpouseID int = NULL,
	@adoptedFhxKnown nvarchar(50) = NULL, --
	@apptid int = null,
	
	@geneticTesting nvarchar(50) = null,
	@geneticTestingResult nvarchar(100) = null,
	@riskFactorsConfirmed int = null
	
AS
BEGIN
begin transaction
IF (@apptid is null or @apptid <= 0)
BEGIN
	SET @apptid = dbo.fn_3_GetGoldenAppt(@patientUnitnum)
END

DECLARE @rowCount int
IF ((@apptid > 0) and (@relativeID > 0))
BEGIN 

	IF(@delete=1)
		BEGIN
			IF(@relativeID > 7)
			BEGIN
				DELETE FROM tblRelativeDetails WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblRiskDataRelatives WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblPedigreePositions WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblRelativeBackground WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblRiskDataDisease WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblRiskGeneticTest WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblRiskGeneticTestTest WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblRiskAnalysis WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblRiskAnalysis2 WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblObPregnancies WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblCustomFields WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblConditions WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblColonCancerDetails WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblBreastHistology WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblBreastCancerDetails WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblSymptoms WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblSystemIDs WHERE (apptid = @apptid) and relativeID = @relativeID
				DELETE FROM tblTempRelatives WHERE (apptid = @apptid) and relativeID = @relativeID
				
				update tblriskdatarelatives set motherid = 0 WHERE (apptid = @apptid) and motherid = @relativeID
				update tblriskdatarelatives set fatherid = 0 WHERE (apptid = @apptid) and fatherid = @relativeID
			END
		END
	ELSE
	BEGIN

			------------------------------------------------
			------------ tblRiskData ------------
			------------------------------------------------
		IF (@relativeID = 1)
			BEGIN
				BEGIN TRANSACTION TRD
					SET @rowCount = (SELECT count(apptid)
									 FROM         tblRiskData	
									 WHERE apptID = @apptid)
					-- check to see if we need a new row
					IF (@rowCount = 0)
					BEGIN
						INSERT INTO tblRiskData (riskID, apptID, createdBy, created)
							VALUES     (@apptid,@apptid,@user,GETDATE())
					END
					
					IF (NOT(@geneticTesting IS NULL)) BEGIN UPDATE tblRiskData SET geneticTesting = @geneticTesting, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) END
					IF (NOT(@geneticTestingResult IS NULL)) BEGIN UPDATE tblRiskData SET geneticTestingResult = @geneticTestingResult, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) END
					IF (NOT(@riskFactorsConfirmed IS NULL)) BEGIN UPDATE tblRiskData SET riskFactorsConfirmed = @riskFactorsConfirmed, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) END

				COMMIT TRANSACTION TRD
			END

		------------------------------------------------
		------------ relatives details table  ------------
		------------------------------------------------
		SET @rowCount = (SELECT count(apptid)
						 FROM         tblRelativeDetails	
						 WHERE apptID = @apptid AND relativeID = @relativeID)
		-- check to see if we need a new row
		IF (@rowCount = 0)
		BEGIN
			INSERT INTO tblRelativeDetails (apptID, relativeID, createdBy, created)
					VALUES     (@apptid,@relativeID,@user,GETDATE())
		END
		IF (NOT(@city IS NULL)) BEGIN UPDATE tblRelativeDetails SET city = @city, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@state IS NULL)) BEGIN UPDATE tblRelativeDetails SET state = @state, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@zip IS NULL)) BEGIN UPDATE tblRelativeDetails SET zipcode = @zip, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END

		------------------------------------------------
		------------    relatives table     ------------
		------------------------------------------------
		SET @rowCount = (SELECT count(apptid)
						 FROM         tblRiskDataRelatives	
						 WHERE apptID = @apptid AND relativeID = @relativeID)
		-- check to see if we need a new row
		IF (@rowCount = 0)
		BEGIN
			INSERT INTO tblRiskDataRelatives (apptID, relativeID, createdBy, created)
					VALUES     (@apptid,@relativeID, @user, GETDATE())
		END

		IF (NOT(@name IS NULL)) 
		BEGIN 
			UPDATE tblRiskDataRelatives 
			SET name = @name, modified = GETDATE(), modifiedBy = @user 
			WHERE (apptid = @apptid) and relativeID = @relativeID 
			
			IF (@relativeID = 1)
			BEGIN
				UPDATE tblAppointments 
				SET patientname = @name
				WHERE (apptid = @apptid) 
			END
		END
		
		
		IF (NOT(@lastName IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET lastName = @lastName, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@firstName IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET firstName = @firstName, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@middleName IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET middleName = @middleName, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@maidenName IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET maidenName = @maidenName, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@dob IS NULL)) 
		BEGIN 
			UPDATE tblRiskDataRelatives 
			SET dob = @dob, modified = GETDATE(), modifiedBy = @user 
			WHERE (apptid = @apptid) and relativeID = @relativeID
			
			IF (@relativeID = 1)
			BEGIN
				UPDATE tblAppointments 
				SET dob = CONVERT(VARCHAR(10),CONVERT(datetime,@dob),101)
				WHERE (apptid = @apptid) 
			END
		END
		
		IF ((@relativeID = 1) AND NOT(@emailAddress IS NULL)) 
		BEGIN 
			UPDATE tblRiskData SET emailAddress = @emailAddress, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid)
		END
		-------------------------------------------------------------------
		---Person : HraObject---
		-------------------------------------------------------------------
		
		-- check to see if we need a new row
		--IF (@rowCount = 0)
		--BEGIN
		--	INSERT INTO tblPatients (apptID, relativeID, createdBy, created)
		--			VALUES     (@apptid,@relativeID, @user, GETDATE())
		--END
		
		IF(@address1 IS NOT NULL) BEGIN 
			UPDATE tblPatients SET address1 = @address1 WHERE unitnum = @patientUnitnum
			UPDATE tblAppointments SET address1 = @address1 WHERE unitnum = @patientUnitnum 
		END
		IF(@address2 IS NOT NULL) BEGIN 
			UPDATE tblPatients SET address2 = @address2 WHERE unitnum = @patientUnitnum 
			UPDATE tblAppointments SET address2 = @address2 WHERE unitnum = @patientUnitnum
		END		
		
		IF(@country IS NOT NULL) BEGIN
			UPDATE tblPatients SET country = @country WHERE unitnum = @patientUnitnum
			UPDATE tblAppointments SET country = @country WHERE unitnum = @patientUnitnum
		END
		
		IF(@homephone IS NOT NULL) BEGIN
			UPDATE tblPatients SET homephone = @homephone WHERE unitnum = @patientUnitnum
			UPDATE tblAppointments SET homephone = @homephone WHERE unitnum = @patientUnitnum
		END
				
		IF(@workphone IS NOT NULL) BEGIN
			UPDATE tblPatients SET workphone = @workphone WHERE unitnum = @patientUnitnum
			UPDATE tblAppointments SET workphone = @workphone WHERE unitnum = @patientUnitnum
		END
		
		IF(@cellphone IS NOT NULL) BEGIN
			UPDATE tblPatients SET cellphone = @cellphone WHERE unitnum = @patientUnitnum
			UPDATE tblAppointments SET cellphone = @cellphone WHERE unitnum = @patientUnitnum
		END
		
		-----------------------------------------
		-----------------------------------------
		
		IF(@relativeID = 1)
		BEGIN
			BEGIN
				BEGIN TRANSACTION trdExt2
				
					SET @rowCount = (SELECT count(apptid) FROM tblRiskDataExt2 WHERE apptID = @apptid)
					
					if(@rowCount = 0)
					BEGIN
						INSERT INTO tblRiskDataExt2 (apptID, createdBy, created)
							VALUES     (@apptid,@user,GETDATE())
					END
				
					IF (@maritalstatus IS NOT NULL) 
					BEGIN 
						UPDATE tblRiskDataExt2 SET maritalStatus = @maritalstatus, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid)
					END
					
					IF (@educationLevel IS NOT NULL) 
					BEGIN 
						UPDATE tblRiskDataExt2 SET educationLevel = @educationLevel, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid)
					END
					
					IF (@occupation IS NOT NULL) 
					BEGIN 
						UPDATE tblRiskDataExt2 SET occupation = @occupation, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid)
					END
				COMMIT TRANSACTION trdExt2
			END
		END

	---------------------------------------------------------------------------------
		IF (NOT (@motherID IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET motherID = @motherID, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT (@fatherID IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET fatherID = @fatherID, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@gender IS NULL)) 
		BEGIN 
			UPDATE tblRiskDataRelatives 
			SET gender = @gender, modified = GETDATE(), modifiedBy = @user 
			WHERE (apptid = @apptid) and relativeID = @relativeID 
			
			IF (@relativeID = 1)
			BEGIN
				UPDATE tblAppointments 
				SET gender = UPPER(SUBSTRING(@gender,1,1))
				WHERE (apptid = @apptid) 
			END
		END
		IF (NOT(@relationship IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET relationship = @relationship, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@relationshipOther IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET relationshipOther = @relationshipOther, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@bloodline IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET bloodline = @bloodline, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@age IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET age = @age, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@vitalStatus IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET ageStatus = @vitalStatus, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@comment IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET comment = @comment, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END

		IF (NOT(@title IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET title = @title, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@suffix IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET suffix = @suffix, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF NOT (@twinID IS  NULL) 
		 BEGIN 
		  IF (@twinID <=0)
		  BEGIN
		   UPDATE tblRiskDataRelatives SET twinID = null, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID 
		   UPDATE tblRiskDataRelatives SET @twinType = null, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID 
		  END
		  ELSE
		  BEGIN
		   UPDATE tblRiskDataRelatives SET twinID = @twinID, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID 
		  END
		 END
		IF (NOT(@twinType IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET twinType = @twinType, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@adopted IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET adopted = @adopted, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@adoptedFhxKnown IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET adoptedFhxKnown = @adoptedFhxKnown, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END --
		IF (NOT(@dobConfidence IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET dobConfidence = @dobConfidence, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@causeOfDeath IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET causeOfDeath = @causeOfDeath, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@dateOfDeath IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET dateOfDeath = @dateOfDeath, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@dateOfDeathConfidence IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET dateOfDeathConfidence = @dateOfDeathConfidence, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (NOT(@consanguineousSpouseID IS NULL)) BEGIN UPDATE tblRiskDataRelatives SET consanguineousSpouseID = @consanguineousSpouseID, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END
		IF (@consanguineousSpouseID = 0) BEGIN UPDATE tblRiskDataRelatives SET consanguineousSpouseID = NULL, modified = GETDATE(), modifiedBy = @user WHERE (apptid = @apptid) and relativeID = @relativeID END

		------------------------------------------------
		------------pedigree positions table------------
		------------------------------------------------
		SET @rowCount = (SELECT count(apptid)
						FROM         tblPedigreePositions	
						WHERE apptID = @apptid AND relativeID = @relativeID)
		--if we have data

		IF (@rowCount > 0)
		BEGIN
			IF (@x_position is not null)
			BEGIN
				UPDATE    tblPedigreePositions
				SET       x = @x_position
				WHERE     (apptid = @apptid) and relativeID = @relativeID	
			END
			IF (@y_position is not null)
			BEGIN
				UPDATE    tblPedigreePositions
				SET       y = @y_position
				WHERE     (apptid = @apptid) and relativeID = @relativeID	
			END
			IF (@x_norm is not null)
			BEGIN
				UPDATE    tblPedigreePositions
				SET       x_norm = @x_norm
				WHERE     (apptid = @apptid) and relativeID = @relativeID	
			END
			IF (@y_norm is not null)
			BEGIN
				UPDATE    tblPedigreePositions
				SET       y_norm = @y_norm
				WHERE     (apptid = @apptid) and relativeID = @relativeID	
			END
			IF (@pedigreeGroup is not null)
			BEGIN
				UPDATE    tblPedigreePositions
				SET       pedigreeGroup = @pedigreeGroup
				WHERE     (apptid = @apptid) and relativeID = @relativeID	
			END
		END
		ELSE
		BEGIN
			INSERT INTO tblPedigreePositions (x, y, x_norm, y_norm, apptID, relativeID,pedigreeGroup)
				VALUES     (@x_position, @y_position, @x_norm, @y_norm, @apptid, @relativeID,@pedigreeGroup)
		END
				
		------------------------------------------------
		------------Relative Background table------------
		------------------------------------------------
		if (@isAshkenazi is not null)
		BEGIN
			DELETE FROM tblRelativeBackground where apptid = @apptid and relativeID = @relativeID and tag='isAshkenazi'
			INSERT INTO tblRelativeBackground (apptID, relativeID, tag, value)
				VALUES  (@apptid,@relativeID, N'isAshkenazi',@isAshkenazi)
			if (@relativeID = 1)
			BEGIN	
				update tblRiskData set isAshkenazi = @isAshkenazi where apptID = @apptid
			END
		END
		if (@isHispanic is not null)
		BEGIN
			DELETE FROM tblRelativeBackground where apptid = @apptid and relativeID = @relativeID and tag='isHispanic'
			INSERT INTO tblRelativeBackground (apptID, relativeID, tag, value)
				VALUES  (@apptid,@relativeID, N'isHispanic',@isHispanic)
				
			if (@relativeID = 1)
			BEGIN	
				update tblRiskData set isHispanic = @isHispanic where apptID = @apptid
			END
		END

		------------------------------------------------
		------------Patient Comment------------
		------------------------------------------------
		if (@family_comment is not null)
		BEGIN
			SET @rowCount = (SELECT count(apptid)
				 FROM        tblRiskDataComments	
				 WHERE apptID = @apptid)
			-- check to see if we need a new row
			IF (@rowCount = 0)
			BEGIN
				INSERT INTO tblRiskDataComments (apptID, createdBy, created, comments1)
						VALUES     (@apptid, @user, GETDATE(),@family_comment)
			END
			ELSE
			BEGIN
				UPDATE tblRiskDataComments SET modified = GETDATE(), modifiedby = @user, comments1 = @family_comment WHERE apptid = @apptid
			END
		END
		
	END
	
END
	
commit

END


