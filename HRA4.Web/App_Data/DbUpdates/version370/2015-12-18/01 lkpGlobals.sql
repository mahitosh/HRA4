--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2015-12-24',N'01 lkpGlobals.sql')
GO
--end HRA script header


UPDATE dbo.lkpGlobals SET 
  appVer=N'3.70'
  WHERE ID=1


go
