--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-15',N'01 lkpFamilyDegrees')
GO
--end HRA script header



DELETE FROM lkpFamilyDegrees WHERE Relationship=N'Nephew'
DELETE FROM lkpFamilyDegrees WHERE Relationship=N'Niece'
go


set identity_insert lkpFamilyDegrees on
go

INSERT INTO lkpFamilyDegrees
  (ID, Degrees, Relationship)
  VALUES (38, N'Second', N'Nephew')
INSERT INTO lkpFamilyDegrees
  (ID, Degrees, Relationship)
  VALUES (37, N'Second', N'Niece')
go

set identity_insert lkpFamilyDegrees off
go

