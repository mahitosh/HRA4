<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<HraImportAttemp>
<xsl:for-each select="Medication-List/Medication">
	<medication>
		<medication>
			<xsl:value-of select="Med-Name"/>
		</medication>
		<dose> 
			<xsl:value-of select="Dose"/><xsl:text> </xsl:text><xsl:value-of select="Units"/>
		</dose>
		<reason>
		 	<xsl:value-of select="PRNreason"/>
		</reason>
		<comments>
			<xsl:value-of select="Med-string"/><xsl:text> </xsl:text><xsl:value-of select="Directions"/>
		</comments>
		<startDate>
		 	<xsl:value-of select="Start-date"/>
		</startDate>
		<stopDate>
		 	<xsl:value-of select="End-date"/>
		</stopDate>
		<frequency>
		 	<xsl:value-of select="Med-Freq"/>
		</frequency>
		<route>
		 	<xsl:value-of select="Med-Route"/>
		</route>
		<prn>
		 	<xsl:value-of select="PRN"/><xsl:text> </xsl:text><xsl:value-of select="PRNreason"/>
		</prn>
	</medication>
<xsl:text>
</xsl:text>
</xsl:for-each>
</HraImportAttemp>
</xsl:template>
</xsl:stylesheet> 
