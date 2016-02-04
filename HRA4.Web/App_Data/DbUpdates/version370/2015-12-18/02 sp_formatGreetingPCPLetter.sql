--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2015-12-18',N'02 sp_formatGreetingPCPLetter')
GO
--end HRA script header--
/****** Object:  StoredProcedure [dbo].[sp_formatGreetingPCPLetter]    Script Date: 12/18/2015 10:02:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_formatGreetingPCPLetter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_formatGreetingPCPLetter]
GO

/****** Object:  StoredProcedure [dbo].[sp_formatGreetingPCPLetter]    Script Date: 12/18/2015 10:02:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[sp_formatGreetingPCPLetter]    Script Date: 05/31/2010 15:50:30 ******/

CREATE PROCEDURE [dbo].[sp_formatGreetingPCPLetter] 
	@apptID	int
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @value nvarchar(max)

	DECLARE @provider1 int
	SET @provider1 = NULL

	DECLARE @output TABLE 
	(
		MyValue nvarchar(max)
	)

SELECT     @provider1=lkpProviders.providerID
FROM         tblAppointments INNER JOIN
                      lkpProviders ON tblAppointments.apptphysname = lkpProviders.displayName
WHERE     (tblAppointments.apptid = @apptID)

	--first look for RefPhys
	SELECT @value = (SELECT providerID
		FROM         tblApptProviders 
		WHERE    apptID = @apptID AND refPhys=1)

	IF (@value IS NOT NULL) AND (@value > 0)
		BEGIN
			IF(@provider1 IS NOT NULL)
			BEGIN
				INSERT INTO @output (MyValue) exec sp_formatProviderGreetingMoniker @provider1, @value
			END
			ELSE
			BEGIN
				INSERT INTO @output (MyValue) exec sp_formatProviderGreeting @value
			END
		END
	ELSE
		BEGIN
			--now try PCP
			SELECT @value = (SELECT providerID
				FROM         tblApptProviders 
				WHERE    apptID = @apptID AND PCP=1)

			IF (@value IS NOT NULL) AND (@value > 0)
				BEGIN
					IF(@provider1 IS NOT NULL)
					BEGIN
						INSERT INTO @output (MyValue) exec sp_formatProviderGreetingMoniker @provider1, @value
					END
					ELSE
					BEGIN
						INSERT INTO @output (MyValue) exec sp_formatProviderGreeting @value
					END
				END
			ELSE
				BEGIN
					--no refPhys and no PCP, so just take the first provider
					SELECT @value = (SELECT TOP (1)  providerID
						FROM         tblApptProviders 
						WHERE    apptID = @apptID)
					IF (@value IS NOT NULL) AND (@value > 0)
						BEGIN
							IF(@provider1 IS NOT NULL)
							BEGIN
								INSERT INTO @output (MyValue) exec sp_formatProviderGreetingMoniker @provider1, @value
							END
							ELSE
							BEGIN
								INSERT INTO @output (MyValue) exec sp_formatProviderGreeting @value
							END

						END
				END
		END

		SELECT dbo.fn_ToProperCase(MyValue) FROM @output
END


GO


