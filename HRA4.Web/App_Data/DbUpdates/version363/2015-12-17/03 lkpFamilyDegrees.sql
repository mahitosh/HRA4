--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2015-12-17',N'03 lkpFamilyDegrees')
GO
--end HRA script header

DELETE FROM dbo.lkpFamilyDegrees WHERE Relationship=N'Niece'
DELETE FROM dbo.lkpFamilyDegrees WHERE Relationship=N'Nephew'
go



set identity_insert dbo.lkpFamilyDegrees on
go

INSERT INTO dbo.lkpFamilyDegrees
  (ID, Degrees, Relationship)
  VALUES (37, N'Second', N'Niece')
INSERT INTO dbo.lkpFamilyDegrees
  (ID, Degrees, Relationship)
  VALUES (38, N'Second', N'Nephew')
go

set identity_insert dbo.lkpFamilyDegrees off
go

