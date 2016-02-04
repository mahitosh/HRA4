--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2015-12-17',N'02 sp_formatDemographics1')
GO
--end HRA script header--


/****** Object:  StoredProcedure [dbo].[sp_formatDemographics1]    Script Date: 01/11/2016 13:46:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_formatDemographics1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_formatDemographics1]
GO

/****** Object:  StoredProcedure [dbo].[sp_formatDemographics1]    Script Date: 01/11/2016 13:46:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
CREATE PROCEDURE [dbo].[sp_formatDemographics1] 
	-- Add the parameters for the stored procedure here
	@apptID	int
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @value nvarchar(max)
	DECLARE @displayValue nvarchar(max)
	DECLARE @output TABLE 
	(
		Operation nvarchar(max),
		Date nvarchar(max)
	)


	SELECT @value = (SELECT dbo.fn_calculateAge(dob, apptdate) FROM tblAppointments WHERE (apptid = @apptID))
	IF (@value IS NOT NULL)
	BEGIN
		IF(LEN(@value) > 0)
		BEGIN
			INSERT INTO @output (Operation,Date) SELECT 'Age:', @value
		END
	END


	SELECT @value = (SELECT heightFeetInches FROM tblRiskData WHERE apptID = @apptID)
	IF (@value IS NOT NULL)
	BEGIN
		IF(LEN(@value) > 0)
		BEGIN
			INSERT INTO @output (Operation,Date) SELECT 'Height:', @value
		END
	END

	SELECT @value = (SELECT weightPounds FROM tblRiskData WHERE apptID = @apptID)
	IF (@value IS NOT NULL)
	BEGIN
		IF(LEN(@value) > 0)
		BEGIN
			INSERT INTO @output (Operation,Date) SELECT 'Weight:', @value
		END
	END

	SELECT @value = BMI  FROM tblRiskDataExt2 WHERE apptID = @apptID
	IF (@value IS NOT NULL)
	BEGIN
		IF(LEN(@value) > 0)
		BEGIN
			--IF (@displayValue IS NOT NULL)
			--	BEGIN
			--		INSERT INTO @output (Operation,Date) SELECT 'BMI:', @value+' '+@displayValue
			--	END
			--ELSE
			--	BEGIN
					INSERT INTO @output (Operation,Date) SELECT 'BMI:', @value
				--END
		END
	END

	SELECT @value = (SELECT gender FROM tblAppointments WHERE apptid = @apptID)
	IF (@value IS NOT NULL)
	BEGIN
		IF(@value Like 'M%')
		BEGIN
			INSERT INTO @output (Operation,Date) SELECT 'Gender:', 'Male'
		END
		ELSE 
		BEGIN
			INSERT INTO @output (Operation,Date) SELECT 'Gender:', 'Female'
		END
	END

	SELECT * FROM @output


END



GO


