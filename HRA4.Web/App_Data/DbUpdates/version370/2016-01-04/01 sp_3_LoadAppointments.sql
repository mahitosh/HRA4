--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-04',N'01 sp_3_LoadAppointmentList.sql')
GO
--end HRA script header

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_3_LoadAppointmentList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_3_LoadAppointmentList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_3_LoadAppointmentList](
	@Date as nvarchar(50) = null,
	@NameOrMrn as nvarchar(128) = null,
	@groupName as nvarchar(128) = null,
	@userLogin nvarchar(75) = '',
	@clinicId int = -1
) 
AS
BEGIN

if (@Date is not null)
	BEGIN

SELECT     tblAppointments.apptid, tblAppointments.unitnum, tblAppointments.patientname, tblBigQueue.Diseases, tblAppointments.dob, tblAppointments.apptdate, tblAppointments.appttime, 
                      tblAppointments.apptdatetime, tblAppointments.apptphysname, tblAppointments.familyNumber, tblAppointments.clinicID, dbo.fn_3_GetGoldenAppt(tblAppointments.unitnum) 
                      AS GoldenAppt, dbo.fn_3_GetApptDateTimeFromApptID(dbo.fn_3_GetGoldenAppt(tblAppointments.unitnum)) AS GoldenApptTime, tblAppointments.riskdatacompleted, lkpSurveys.surveyName as surveyType,
                      clinicName, tblAppointments.clinic, tblAppointments.gender, tblAppointments.language, tblAppointments.nationality, tblAppointments.race
FROM         tblAppointments LEFT OUTER JOIN
                      lkpSurveys ON tblAppointments.surveyID = lkpSurveys.surveyID LEFT OUTER JOIN
                      tblBigQueue ON tblAppointments.unitnum = tblBigQueue.unitnum LEFT OUTER JOIN
                      lkpClinics ON tblAppointments.clinicID = lkpClinics.clinicID
		WHERE 
		(@Date is null OR Convert(datetime,apptdate) = Convert(datetime,@Date)) and 
		(tblAppointments.clinicID = @clinicID OR  @clinicID = -1)  and
					
		(@NameOrMrn IS NULL OR 
		tblAppointments.unitnum = @NameOrMrn OR 
		tblAppointments.unitnum like '%' + @NameOrMrn + '%' OR 
		tblAppointments.patientname like '%' + @NameOrMrn + '%')
	END
else
	Begin

SELECT     tblAppointments.apptid, tblAppointments.unitnum, tblAppointments.patientname, tblBigQueue.Diseases, tblAppointments.dob, tblAppointments.apptdate, tblAppointments.appttime, 
                      tblAppointments.apptdatetime, tblAppointments.apptphysname, tblAppointments.familyNumber, tblAppointments.clinicID, dbo.fn_3_GetGoldenAppt(tblAppointments.unitnum) 
                      AS GoldenAppt, dbo.fn_3_GetApptDateTimeFromApptID(dbo.fn_3_GetGoldenAppt(tblAppointments.unitnum)) AS GoldenApptTime, tblAppointments.riskdatacompleted, lkpSurveys.surveyName as surveyType,
                      clinicName, tblAppointments.clinic, tblAppointments.gender, tblAppointments.language, tblAppointments.nationality, tblAppointments.race
FROM         tblAppointments LEFT OUTER JOIN
                      lkpSurveys ON tblAppointments.surveyID = lkpSurveys.surveyID LEFT OUTER JOIN
                      tblBigQueue ON tblAppointments.unitnum = tblBigQueue.unitnum LEFT OUTER JOIN
                      lkpClinics ON tblAppointments.clinicID = lkpClinics.clinicID
		WHERE 
		(tblAppointments.clinicID = @clinicID OR  @clinicID = -1)  and
		(@NameOrMrn IS NULL OR 
		tblAppointments.unitnum = @NameOrMrn OR 
		tblAppointments.unitnum like '%' + @NameOrMrn + '%' OR 
		tblAppointments.patientname like '%' + @NameOrMrn + '%')


	END

END
