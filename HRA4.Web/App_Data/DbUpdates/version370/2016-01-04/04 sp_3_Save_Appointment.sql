--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-04',N'04 sp_3_Save_Appointment.sql')
GO
--end HRA script header

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_3_Save_Appointment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_3_Save_Appointment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_3_Save_Appointment] (
		@user nvarchar (100) = null,
        @apptID int,
        @patientname nvarchar(100) = null,
        @dob nvarchar(10) = null,
        @gender nvarchar(10) = null,
        @apptdate nvarchar(10) = null,
        @appttime nvarchar(10) = null,
        @apptphysname nvarchar(75) = null,
        @familyNumber int = null,
        @clinic nvarchar(100) = null,
        @clinicID int = null,
        @clinicName nvarchar(100) = null,
        @language nvarchar(100) = null,
        @race nvarchar(100) = null,
        @nationality nvarchar(100) = null,
        @surveyType nvarchar(100) = null
)
AS BEGIN

	IF(@apptId is not null)
	BEGIN
	
		BEGIN TRANSACTION
		
			IF (NOT(@patientname IS NULL)) BEGIN UPDATE tblAppointments SET patientname = @patientname WHERE apptid = @apptID END
			IF (NOT(@dob IS NULL)) BEGIN UPDATE tblAppointments SET dob = @dob WHERE apptid = @apptID END
			IF (NOT(@gender IS NULL)) BEGIN UPDATE tblAppointments SET gender = @gender WHERE apptid = @apptID END
			IF (NOT(@apptdate IS NULL)) BEGIN UPDATE tblAppointments SET apptdate = @apptdate WHERE apptid = @apptID END
			IF (NOT(@appttime IS NULL)) BEGIN UPDATE tblAppointments SET appttime = @appttime WHERE apptid = @apptID END
			IF (NOT(@apptphysname IS NULL)) BEGIN UPDATE tblAppointments SET apptphysname = @apptphysname WHERE apptid = @apptID END
			IF (NOT(@familyNumber IS NULL)) BEGIN UPDATE tblAppointments SET familyNumber = @familyNumber WHERE apptid = @apptID END
			IF (NOT(@clinic IS NULL)) BEGIN UPDATE tblAppointments SET clinic = @clinic WHERE apptid = @apptID END
			IF (NOT(@clinicID IS NULL)) BEGIN UPDATE tblAppointments SET clinicID = @clinicID WHERE apptid = @apptID END
			/*IF (NOT(@clinicName IS NULL)) BEGIN UPDATE tblAppointments SET clinicName = @clinicName WHERE apptid = @apptID END*/
			IF (NOT(@language IS NULL)) BEGIN UPDATE tblAppointments SET [language] = @language WHERE apptid = @apptID END
			IF (NOT(@race IS NULL)) BEGIN UPDATE tblAppointments SET race = @race WHERE apptid = @apptID END
			IF (NOT(@nationality IS NULL)) BEGIN UPDATE tblAppointments SET nationality = @nationality WHERE apptid = @apptID END
			IF (NOT(@surveyType IS NULL)) BEGIN UPDATE tblAppointments SET surveyType = @surveyType WHERE apptid = @apptID END
		
		COMMIT
	
	END

END