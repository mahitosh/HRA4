--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2015-11-23',N'07 fn_getRelativeDiseases')
GO
/****** Object:  UserDefinedFunction [dbo].[fn_getRelativeDiseases]    Script Date: 11/09/2015 16:12:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_getRelativeDiseasesMammo]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_getRelativeDiseasesMammo]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_getRelativeDiseases]    Script Date: 11/09/2015 16:12:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE FUNCTION [dbo].[fn_getRelativeDiseasesMammo] 
(
	@apptID int,
	@relativeID int
)
RETURNS nvarchar(1000) 
AS
BEGIN
	--
	-- Declare the return variable here	
	DECLARE @desc VARCHAR(1000)
	
	DECLARE @results TABLE
	(
		instanceID int,
		side nvarchar(10)
	)
	
	SET @desc='';

	--SELECT @desc =@desc + ', ' + disease
	
	INSERT INTO @results SELECT instanceID, side FROM tblBreastCancerDetails WHERE apptID = @apptID AND relativeID = 1
	
	IF ((SELECT COUNT(instanceID) FROM @results)> = 1 AND @relativeID = 1)
	BEGIN
			SELECT @desc =(CASE ISNULL(ageDiagnosis , '') WHEN '' THEN @desc + ', ' + disease + ' ' + ISNULL(CONVERT(nvarchar(10),side),'') ELSE @desc + ', ' + disease + ' Age '+ageDiagnosis + ' ' + ISNULL(CONVERT(nvarchar(10),side),'') END)
			FROM
			tblRiskDataDisease INNER JOIN @results AS T1 ON tblRiskDataDisease.instanceID = T1.instanceID
			WHERE
			tblriskdatadisease.relativeID= @relativeID AND tblriskdatadisease.apptID= @apptID 
			
			SELECT @desc =(CASE ISNULL(ageDiagnosis , '') WHEN '' THEN @desc + ', ' + disease ELSE @desc + ', ' + disease + ' Age '+ageDiagnosis END)
			FROM
			tblRiskDataDisease
			WHERE
			relativeID= @relativeID AND apptID= @apptID AND tblRiskDataDisease.instanceID NOT IN (Select instanceID FROM @results)
			
			SET @desc=substring(@desc,3,len(@desc))
	END
	ELSE
	BEGIN
			SELECT @desc =(CASE ISNULL(ageDiagnosis , '') WHEN '' THEN @desc + ', ' + disease ELSE @desc + ', ' + disease + ' Age '+ageDiagnosis END)

			FROM
			tblRiskDataDisease
			WHERE
			relativeID= @relativeID AND apptID= @apptID
			SET @desc=substring(@desc,3,len(@desc))
	END

	RETURN @desc;
END

GO


