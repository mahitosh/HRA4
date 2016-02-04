--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-19',N'02 alter lkpInterfaceDefinitions.sql')
GO
--end HRA script header

--------------------------------------------------
-- modify lkpInterfaceDefinitions
-- Add some more blank parameters for future use.
-- yes, your next question is why don't these columns have better names?
-- because these will mean different things for different interface types.  
--------------------------------------------------
DECLARE @present int

-- add some string params

SET @present = (SELECT COUNT (sysobjects.name)
FROM sysobjects 
JOIN syscolumns ON sysobjects.id = syscolumns.id
JOIN systypes ON syscolumns.xtype=systypes.xtype
WHERE sysobjects.xtype='U' AND syscolumns.name='stringParam4' AND sysobjects.name='lkpInterfaceDefinitions')

IF (@present = 0)
BEGIN
ALTER TABLE lkpInterfaceDefinitions
ADD stringParam4 nvarchar(max);
END

SET @present = (SELECT COUNT (sysobjects.name)
FROM sysobjects 
JOIN syscolumns ON sysobjects.id = syscolumns.id
JOIN systypes ON syscolumns.xtype=systypes.xtype
WHERE sysobjects.xtype='U' AND syscolumns.name='stringParam5' AND sysobjects.name='lkpInterfaceDefinitions')

IF (@present = 0)
BEGIN
ALTER TABLE lkpInterfaceDefinitions
ADD stringParam5 nvarchar(max);
END

SET @present = (SELECT COUNT (sysobjects.name)
FROM sysobjects 
JOIN syscolumns ON sysobjects.id = syscolumns.id
JOIN systypes ON syscolumns.xtype=systypes.xtype
WHERE sysobjects.xtype='U' AND syscolumns.name='stringParam6' AND sysobjects.name='lkpInterfaceDefinitions')

IF (@present = 0)
BEGIN
ALTER TABLE lkpInterfaceDefinitions
ADD stringParam6 nvarchar(max);
END

-- now some ints

SET @present = (SELECT COUNT (sysobjects.name)
FROM sysobjects 
JOIN syscolumns ON sysobjects.id = syscolumns.id
JOIN systypes ON syscolumns.xtype=systypes.xtype
WHERE sysobjects.xtype='U' AND syscolumns.name='intParam3' AND sysobjects.name='lkpInterfaceDefinitions')

IF (@present = 0)
BEGIN
ALTER TABLE lkpInterfaceDefinitions
ADD intParam3 int;
END


SET @present = (SELECT COUNT (sysobjects.name)
FROM sysobjects 
JOIN syscolumns ON sysobjects.id = syscolumns.id
JOIN systypes ON syscolumns.xtype=systypes.xtype
WHERE sysobjects.xtype='U' AND syscolumns.name='intParam4' AND sysobjects.name='lkpInterfaceDefinitions')

IF (@present = 0)
BEGIN
ALTER TABLE lkpInterfaceDefinitions
ADD intParam4 int;
END


SET @present = (SELECT COUNT (sysobjects.name)
FROM sysobjects 
JOIN syscolumns ON sysobjects.id = syscolumns.id
JOIN systypes ON syscolumns.xtype=systypes.xtype
WHERE sysobjects.xtype='U' AND syscolumns.name='intParam5' AND sysobjects.name='lkpInterfaceDefinitions')

IF (@present = 0)
BEGIN
ALTER TABLE lkpInterfaceDefinitions
ADD intParam5 int;
END

SET @present = (SELECT COUNT (sysobjects.name)
FROM sysobjects 
JOIN syscolumns ON sysobjects.id = syscolumns.id
JOIN systypes ON syscolumns.xtype=systypes.xtype
WHERE sysobjects.xtype='U' AND syscolumns.name='intParam6' AND sysobjects.name='lkpInterfaceDefinitions')

IF (@present = 0)
BEGIN
ALTER TABLE lkpInterfaceDefinitions
ADD intParam6 int;
END