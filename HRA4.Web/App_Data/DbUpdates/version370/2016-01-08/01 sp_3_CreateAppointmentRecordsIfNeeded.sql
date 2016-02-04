--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-08',N'01 sp_3_CreateAppointmentRecordsIfNeeded.sql')
GO
--end HRA script header

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_3_CreateAppointmentRecordsIfNeeded]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_3_CreateAppointmentRecordsIfNeeded]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- borrowed RiskAppCore.ApptUtils.addAppointmentRecordsIfNeeded() in v2

CREATE PROCEDURE [dbo].[sp_3_CreateAppointmentRecordsIfNeeded] 

	@apptId int,
	@riskId nvarchar(100),
	@patientUnitnum nvarchar(50)
    
AS
BEGIN
BEGIN TRANSACTION

	DECLARE @rowCount int;
	
	---------------------------------------------------------------------------
	-- tblRiskClinicData
	SET @rowCount = (SELECT COUNT(apptID) FROM tblRiskClinicData WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN 
		INSERT INTO tblRiskClinicData(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP') 
	END
	
	---------------------------------------------------------------------------
	-- tblRiskData
	SET @rowCount = (SELECT COUNT(apptID) FROM tblRiskData WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblRiskData(apptID,created,createdby,riskID,printed,exported,converted) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP',@riskId,0,0,0)
	END
	
	---------------------------------------------------------------------------
	-- tblRiskDataExt1
	SET @rowCount = (SELECT COUNT(created) FROM tblRiskDataExt1 WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblRiskDataExt1(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP')
	END
	
	---------------------------------------------------------------------------
	-- tblRiskDataExt2
	SET @rowCount = (SELECT COUNT(created) FROM tblRiskDataExt2 WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblRiskDataExt2(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP')
	END
	
	---------------------------------------------------------------------------
	-- tblLymphSurveyResponses1
	SET @rowCount = (SELECT COUNT(apptID) FROM tblLymphSurveyResponses1 WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblLymphSurveyResponses1(apptID) VALUES(@apptId)
	END
	
	---------------------------------------------------------------------------
	-- tblLymphSurveyResponses2
	SET @rowCOunt = (SELECT COUNT(apptID) FROM tblLymphSurveyResponses2 WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblLymphSurveyResponses2 (apptID) VALUES(@apptId)
	END
	
	---------------------------------------------------------------------------
	-- tblRiskDataDepression
	SET @rowCount = (SELECT COUNT(created) FROM tblRiskDataDepression WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblRiskDataDepression(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP')
	END
		
	---------------------------------------------------------------------------
	-- tblRiskDataPain
	SET @rowCount = (SELECT COUNT(created) FROM tblRiskDataPain WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblRiskDataPain(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP')
	END
	
	---------------------------------------------------------------------------
	-- tblRiskDataCustom
	
	SET @rowCount = (SELECT COUNT(created) FROM tblRiskDataCustom WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblRiskDataCustom(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP')
	END
	
	---------------------------------------------------------------------------
	-- tblRiskDataFlags
	
	SET @rowCount = (SELECT COUNT(created) FROM tblRiskDataFlags WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblRiskDataFlags(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP')
	END
	
	---------------------------------------------------------------------------
	-- tblRiskDataComments
	
	SET @rowCount = (SELECT COUNT(created) FROM tblRiskDataComments WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblRiskDataComments(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP')
	END
	
	---------------------------------------------------------------------------
	-- tblPatients
	
	SET @rowCount = (SELECT COUNT(unitnum) FROM tblPatients WHERE unitnum=@patientUnitnum)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblPatients (unitnum) VALUES(@patientUnitnum)
	END	
	
	---------------------------------------------------------------------------
	-- tblPATA
	SET @rowCount = (SELECT COUNT(created) FROM tblPATA WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblPATA(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP')
	END
	
	---------------------------------------------------------------------------
	-- tblRiskDataOrtho
	SET @rowCount= (SELECT COUNT(created) FROM tblRiskDataOrtho WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblRiskDataOrtho(apptID,created,createdby) VALUES(@apptId,GETDATE(),N'RISKCLINICAPP')
	END
	
	---------------------------------------------------------------------------
	-- tblSurgicalClinic
	SET @rowCount = (SELECT COUNT(*) FROM tblSurgicalClinic WHERE apptID=@apptId)
	IF(@rowCount = 0)
	BEGIN
		INSERT INTO tblSurgicalClinic(apptID) VALUES(@apptId)
	END	
	
COMMIT
END


