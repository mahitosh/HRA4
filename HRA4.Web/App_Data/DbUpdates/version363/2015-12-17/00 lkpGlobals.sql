--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2015-12-17',N'00 lkpGlobals')
GO
--end HRA script header--


update lkpGlobals set appVer = '3.63'
