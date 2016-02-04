--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2015-12-17',N'01 sp_deleteWebAppointment')
GO
--end HRA script header--

/****** Object:  StoredProcedure [dbo].[sp_deleteWebAppointment]    Script Date: 01/11/2016 13:43:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deleteWebAppointment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deleteWebAppointment]
GO



/****** Object:  StoredProcedure [dbo].[sp_deleteWebAppointment]    Script Date: 01/11/2016 13:43:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Hughes, Sherwood
-- Create date: 10/22/2009
-- Description:	If a user wants to delete all
-- data they have submitted do it.
--
-- Modified by: Friendlich, Mark
-- Modification Date: 05/29/2014
-- Description: Delete all data defined to date.
-- =============================================
CREATE PROCEDURE [dbo].[sp_deleteWebAppointment] 
	-- Add the parameters for the stored procedure here
	@apptID	int,
	@keepApptRecord bit = 0,
	@purgeAuditData bit = 0,
	@userLoginID nvarchar(50) = 'not specified',
	@machineNameID nvarchar(100) = 'default',
	@applicationID nvarchar(100) = 'HraWebTablet',	-- web tablet calls this sp with defaults... the others don't
	@visitnum nvarchar(100) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- in the flat file appt cancel routine, fetch the appt id from the visitnum, if possible....
	IF (@apptID = 0)
	BEGIN
		SELECT @apptID = apptid from tblAppointments where visitnum = @visitnum;
	END
	
	--IF ((NOT EXISTS (SELECT * FROM tblAppointments WHERE ((apptid = @apptID) AND (riskdatacompleted IS NULL)))) AND (@applicationID != 'RiskApps3CloudService'))
	--BEGIN
	--	RETURN -1
	--END
	
	EXEC [dbo].[sp_3_AuditUserActivity]	-- jdg 12/10/15 log that appt was deleted... and don't delete that table
		@application = @applicationID,
		@userLogin = @userLoginID,
		@machineName = @machineNameID,
		@message = N'Deleted Appointment',
		@apptID = @apptID
	
	DECLARE @unitnum nvarchar(50);
	SELECT @unitnum = '';
	SELECT @unitnum = unitnum FROM tblAppointments WHERE apptid = @apptID;
		
	DECLARE @patientID INT;
	SELECT @patientID = 0;
	
	IF @unitnum != ''
	BEGIN
		SET @patientID = (SELECT TOP(1) patientID FROM tblPatients WHERE @unitnum = unitnum);  -- CHANGE
	END
	BEGIN TRANSACTION
	
    -- Delete all data submitted so far by the user
    if (@keepApptRecord = 0)
    BEGIN
		DELETE FROM tblAppointments WHERE apptid = @apptID;
	END
	
	
	--DELETE FROM tblApptWeb WHERE apptid = @apptID;
	IF @purgeAuditData = 1 -- jdg 12/10/15
	BEGIN
		DELETE FROM tblAuditLog WHERE parentid = @apptID;
		DELETE FROM tblAuditLog_3 where apptid = @apptID;
	END
	
	DELETE FROM tblDocuments WHERE apptid = @apptID;
	--DECLARE @queueID INT;
	--SELECT @queueID = 0;
	--SELECT @queueID = queueID FROM tblQueue WHERE apptid = @apptID;
	--IF @queueID <> 0
	--BEGIN
		DELETE FROM tblQueue WHERE apptid = @apptID;
	--	DELETE FROM tblQueueLog WHERE queueid = @queueID
	--END
	DELETE FROM tblRiskData WHERE apptid = @apptID;
	DELETE FROM tblRiskAnalysis WHERE apptid = @apptID;
	DELETE FROM tblRiskAnalysis WHERE apptid = @apptID;
	DELETE FROM tblRiskDataCustom WHERE apptid = @apptID;
	DELETE FROM tblRiskDataComments WHERE apptid = @apptID;
	DELETE FROM tblRiskDataDisease WHERE apptid = @apptID;
	DELETE FROM tblRiskDataExt1 WHERE apptid = @apptID;
	DELETE FROM tblRiskDataExt2 WHERE apptid = @apptID;
	DELETE FROM tblRiskDataFlags WHERE apptid = @apptID;
	DELETE FROM tblRiskDataRelatives WHERE apptid = @apptID;
	DELETE FROM tblROS WHERE apptid = @apptID;
	--
	DELETE FROM tblAllergies WHERE			apptID = @apptID;
	DELETE FROM tblApptProviders WHERE		apptID = @apptID;
	DELETE FROM tblArchiveAppointments WHERE 	apptid = @apptID;
	DELETE FROM tblBrcaHazardRates WHERE		apptID = @apptID;
	DELETE FROM tblBreastCancerDetails WHERE	apptID = @apptID;
	DELETE FROM tblBreastExam WHERE			apptID = @apptID;
	DELETE FROM tblBreastHistology WHERE		apptID = @apptID;
	DELETE FROM tblColonCancerDetails WHERE		apptID = @apptID;
	DELETE FROM tblConditions WHERE			apptID = @apptID;
	DELETE FROM tblCustomFields WHERE		apptID = @apptID;
	DELETE FROM tblDocumentSections WHERE		apptID = @apptID;
	DELETE FROM tblLymphSurveyResponses WHERE	apptID = @apptID;
	DELETE FROM tblLymphSurveyResponses1 WHERE	apptID = @apptID;
	DELETE FROM tblLymphSurveyResponses2 WHERE	apptID = @apptID;
	DELETE FROM tblMasterItems WHERE		apptID = @apptID;
	DELETE FROM tblMedication WHERE			apptID = @apptID;
	DELETE FROM tblMmrHazardRates WHERE		apptID = @apptID;
	DELETE FROM tblObPregnancies WHERE		apptId = @apptID;
	DELETE FROM tblPATA WHERE			apptID = @apptID;
	DELETE FROM tblPedigreePositions WHERE		apptID = @apptID;
	DELETE FROM tblProblems WHERE			apptID = @apptID;
	DELETE FROM tblRelativeDetails WHERE		apptID = @apptID;
	DELETE FROM tblRheumJoints WHERE		apptID = @apptID;
	DELETE FROM tblRiskClinicData WHERE		apptID = @apptID;
	DELETE FROM tblRiskDataDepression WHERE		apptID = @apptID;
	DELETE FROM tblRiskDataDisease WHERE		apptID = @apptID;
	DELETE FROM tblRiskDataExt2 WHERE		apptID = @apptID;
	DELETE FROM tblRiskDataOrtho WHERE		apptID = @apptID;
	DELETE FROM tblRiskDataPain WHERE		apptID = @apptID;
	DELETE FROM tblRiskDataRelatives WHERE		apptID = @apptID;
	DELETE FROM tblRiskGeneticTest WHERE		apptID = @apptID;
	DELETE FROM tblRiskGeneticTestTest WHERE	apptID = @apptID;
	DELETE FROM tblRiskParagraphs WHERE		apptID = @apptID;
	DELETE FROM tblRiskRecommendations WHERE	apptID = @apptID;
	DELETE FROM tblROS WHERE			apptID = @apptID;
	DELETE FROM tblScreeningGuidelines WHERE	apptid = @apptID;
	DELETE FROM tblSurgicalClinicComplaints WHERE	apptID = @apptID;
	DELETE FROM tblSurgicalClinicImpressions WHERE	apptID = @apptID;
	DELETE FROM tblSurveyCompletions WHERE		apptid = @apptID;
	DELETE FROM tblSurveyHistory WHERE		apptID = @apptID;
	DELETE FROM tblSurveyResponses WHERE		apptID = @apptID;
	DELETE FROM tblSurveyState WHERE		apptID = @apptID;
	DELETE FROM tblSymptoms WHERE			apptID = @apptID;
	DELETE FROM tblSystemIDs WHERE			apptID = @apptID;
	DELETE FROM tblTempRelatives WHERE		apptID = @apptID;
	DELETE FROM tblTreatments WHERE			apptID = @apptID;
	DELETE FROM tblWebGuidWithApptId WHERE		apptID = @apptID;
	
	-- Rollback the transaction if there were any errors
	IF @@ERROR <> 0
	 BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN -1
	 END
	 
	COMMIT
	
	IF NOT EXISTS (SELECT apptid FROM tblAppointments WHERE unitnum = @unitnum AND apptid <> @apptID)
	BEGIN
		IF @unitnum <> ''
		BEGIN 
			DELETE FROM tblBreastImaging WHERE		unitnum = @unitnum
			DELETE FROM tblChemotherapy WHERE		unitnum = @unitnum
			DELETE FROM tblClinicalFeatures WHERE		unitnum = @unitnum
			DELETE FROM tblClinicalTests WHERE		unitnum = @unitnum
			DELETE FROM tblCombinedProblemList WHERE	unitnum = @unitnum
			DELETE FROM tblEpisodes WHERE			unitnum = @unitnum
			DELETE FROM tblGUIPreferences WHERE		unitnum = @unitnum
			DELETE FROM tblLabResults WHERE			unitNum = @unitNum
			DELETE FROM tblOrders WHERE			unitnum = @unitnum
			DELETE FROM tblPedigreeAnnotations WHERE	unitnum = @unitnum
			DELETE FROM tblRadiationTherapy WHERE		unitnum = @unitnum
			DELETE FROM tblRadiologyReports WHERE		unitnum = @unitnum
			DELETE FROM tblTransvaginalImaging WHERE	unitnum = @unitnum
		END
		
		IF @patientID <> 0
		BEGIN
			DELETE FROM tblPatients WHERE			patientID = @patientID
			DELETE FROM web_tblDisease WHERE		patientid = @patientid 
			DELETE FROM web_tblPatients WHERE		patientID = @patientID 
			DELETE FROM web_tblRelatives WHERE		patientID = @patientID 
		END
	END
END







GO


