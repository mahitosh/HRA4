--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2015-12-18',N'01 sp_formatProviderAddress')
GO
--end HRA script header--
/****** Object:  StoredProcedure [dbo].[sp_formatProviderAddress]    Script Date: 12/18/2015 11:43:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_formatProviderAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_formatProviderAddress]
GO

/****** Object:  StoredProcedure [dbo].[sp_formatProviderAddress]    Script Date: 12/18/2015 11:43:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[sp_formatProviderAddress] 
	@providerID	int
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @value nvarchar(max)
	DECLARE @result nvarchar(max)
	DECLARE @output TABLE 
	(
		MyValue nvarchar(max)
	)
	SET @result = ''

	SELECT @value = (select lkpProviders.degree from lkpProviders WHERE providerID=@providerID)

	IF((@value NOT LIKE '%M.D.%') AND (@value NOT LIKE '%MD%') AND (@value NOT LIKE '%PhD%') AND (@value NOT LIKE '%DO%') AND (@value NOT LIKE '%D.O.%'))  -- Do not include title if it's an M.D.
		BEGIN

				SELECT @value = (select lkpProviders.title from lkpProviders WHERE providerID=@providerID)
				
				IF((@value IS NOT NULL) AND (@value <> ''))
			BEGIN
				SET @result=@value+' '
			END
		END
	ELSE
		BEGIN
			SET @result=''
		END

	SELECT @value = (select dbo.fn_ToProperCase(lkpProviders.firstName) from lkpProviders WHERE providerID=@providerID)
	
	IF((@value IS NOT NULL) AND (@value <> ''))
BEGIN
	SET @result=@result+@value+' '
END

	SELECT @value = (select dbo.fn_ToProperCase(lkpProviders.middleName) from lkpProviders WHERE providerID=@providerID)

	IF((@value IS NOT NULL) AND (@value <> ''))
BEGIN	
	SET @result=@result+@value+' '
END

	SELECT @value = (select dbo.fn_ToProperCase(lkpProviders.lastName) from lkpProviders WHERE providerID=@providerID)

	IF((@value IS NOT NULL) AND (@value <> ''))
BEGIN	
	SET @result=@result+@value
END

	SELECT @value = (select dbo.fn_ToProperCase(lkpProviders.suffixName) from lkpProviders WHERE providerID=@providerID)

	IF((@value IS NOT NULL) AND (@value <> ''))
BEGIN	
	SET @result=@result+', '+@value
END

	SELECT @value = (select lkpProviders.degree from lkpProviders WHERE providerID=@providerID)

	IF((@value IS NOT NULL) AND (@value <> ''))
BEGIN	
	SET @result=@result+', '+@value
END

INSERT INTO @output (MyValue) SELECT @result

	-- ADDRESS 1
	SELECT @value = (SELECT ISNULL(address1,'')
		FROM         lkpProviders 
		WHERE    providerID = @providerID)
	IF (@value IS NOT NULL) AND (LEN(@value) > 0)
		BEGIN
			INSERT INTO @output (MyValue) SELECT @value
		END

	-- ADDRESS 2
	SELECT @value = (SELECT ISNULL(address2,'')
		FROM         lkpProviders 
		WHERE    providerID = @providerID)
	IF (@value IS NOT NULL) AND (LEN(@value) > 0)
		BEGIN
			INSERT INTO @output (MyValue) SELECT @value
		END

	-- CITY, STATE, ZIP
	SET @value=''
	SELECT @value =(CASE ISNULL(city , '') WHEN '' THEN @value ELSE @value+city END) FROM lkpProviders WHERE providerID=@providerID;
	SELECT @value =(CASE ISNULL(state , '') WHEN '' THEN @value ELSE @value+', '+ state END) FROM lkpProviders WHERE providerID=@providerID;
	SELECT @value =(CASE ISNULL(zipcode , '') WHEN '' THEN @value ELSE @value+' '+ zipcode END) FROM lkpProviders WHERE providerID=@providerID;
	INSERT INTO @output (MyValue) SELECT @value
	
	--COUNTRY
	SELECT @value = (SELECT ISNULL(country,'')
		FROM         lkpProviders 
		WHERE    providerID = @providerID)
	IF (@value IS NOT NULL) AND (LEN(@value) > 0) AND @value!='UNITED STATES'  AND @value!='USA' 
		BEGIN
			INSERT INTO @output (MyValue) SELECT @value
		END

	SELECT * FROM @output

END


GO


