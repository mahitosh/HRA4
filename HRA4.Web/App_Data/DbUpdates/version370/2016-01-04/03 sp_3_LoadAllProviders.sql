--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-04',N'01 sp_3_LoadAllProviders.sql')
GO
--end HRA script header

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_3_LoadAllProviders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_3_LoadAllProviders]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_3_LoadAllProviders]

AS
BEGIN
	SELECT     lkpProviders.*
	FROM         lkpProviders
END




