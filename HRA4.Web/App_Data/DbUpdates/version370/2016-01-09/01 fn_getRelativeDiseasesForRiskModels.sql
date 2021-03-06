--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-09',N'01 fn_getRelativeDiseasesForRiskModels.sql')
GO
--end HRA script header

/****** Object:  UserDefinedFunction [dbo].[fn_getRelativeDiseasesForRiskModels]    Script Date: 12/18/2015 16:20:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_getRelativeDiseasesForRiskModels]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_getRelativeDiseasesForRiskModels]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_getRelativeDiseasesForRiskModels]    Script Date: 12/18/2015 16:20:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


create FUNCTION [dbo].[fn_getRelativeDiseasesForRiskModels] 
(
	@apptID int,
	@relativeID int
)
RETURNS nvarchar(1000) 
AS
BEGIN

	DECLARE @desc VARCHAR(1000)
	
	SET @desc='';


	SELECT @desc =(CASE ISNULL(ageDiagnosis , '') WHEN '' THEN @desc + ', ' + disease ELSE @desc + ', ' + disease + ' age '+ageDiagnosis END)

	FROM
	tblRiskDataDisease
	WHERE
	relativeID= @relativeID AND apptID= @apptID
	AND disease in (
		'Atypical Ductal Hyperplasia',
		'Atypical Hyperplasia',
		'Atypical Lobular Hyperplasia',
		'Breast Cancer',
		'Breast Cancer (DCIS with microinvasion)',
		'Breast Cancer (DCIS with suspicion of invasion)',
		'Breast Cancer (DCIS)',
		'Breast Cancer (Invasive Ductal)',
		'Breast Cancer (Invasive)',
		'Breast Cancer (Lobular)',
		'Colon or Rectal Cancer',
		'LCIS',
		'Ovarian Cancer',
		'Severe ADH/Borderline DCIS',
		'Uterine Cancer',
		'Breast Cancer (Medullary)',
		'Colon Cancer',
		'Ovarian (Borderline)',
		'Rectal Cancer',
		'Ovarian (Germ cell)',
		'Ovarian Cancer (Epithelial)',
		'Bilateral Oophorectomy',
		'Bilateral Mastectomy'
	)
	SET @desc=substring(@desc,3,len(@desc))

	RETURN @desc;
END

GO

