--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-28',N'01 hrt not birth contol tweak.sql')
GO
--end HRA script header

IF NOT EXISTS (SELECT * FROM lkpSurveyMetadata where screenID=950)
	BEGIN
		INSERT INTO dbo.lkpSurveyMetaData
		  VALUES (950, N'BottomMessage', N'913')
	END

IF NOT EXISTS (SELECT * FROM lkpTranslation WHERE translationID=913)
	BEGIN
		INSERT INTO dbo.lkpTranslation
		  VALUES (913, N'caveat for hrt', N'(other than birth control)', N'(autre que le contrôle des naissances)', NULL, NULL, N'(aparte de control de la natalidad)', N'(aparte de control de la natalidad)', NULL, NULL, NULL, N'(que não seja o controle da natalidade)', N'(que não seja o controle da natalidade)', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
	END