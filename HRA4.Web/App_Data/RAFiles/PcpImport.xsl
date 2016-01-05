<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:output method="xml" indent="yes"/>
<!-- This is an identity copy template; it performs a deep copy, recursively -->
 <xsl:template match="@*|node()">
 <xsl:copy>
 <xsl:apply-templates select="@*|node()"/>
 </xsl:copy>
 </xsl:template>

<xsl:template match="Person_Facility">

		<title>
			<xsl:text>Dr.</xsl:text>
		</title>

		<firstName>
			<xsl:value-of select="Person/first-name"/>
		</firstName>

		<middleName>
			<xsl:value-of select="Person/middle-name"/>
		</middleName>

		<lastName>
			<xsl:value-of select="Person/last-name"/>
		</lastName>

		<degree>
			<xsl:value-of select="Person/degree"/>
		</degree>

		<institution>
			<xsl:value-of select="Facilities/Facility/facility-id"/>
		</institution>

		<address1>
			<xsl:value-of select="Facilities/Facility/office-address-1"/>
		</address1>

		<address2>
			<xsl:value-of select="Facilities/Facility/office-address-2"/>
		</address2>

		<city>
			<xsl:value-of select="Facilities/Facility/office-city"/>
		</city>

		<state>
			<xsl:value-of select="Facilities/Facility/office-state"/>
		</state>

		<zipcode>
			<xsl:value-of select="Facilities/Facility/office-zip"/>
		</zipcode>

		<country></country>

		<phone>
			<xsl:value-of select="Facilities/Facility/provider-phoneext."/>
		</phone>

		<fax>
			<xsl:value-of select="Facilities/Facility/provider-fax"/>
		</fax>

		<email>
			<xsl:value-of select="Facilities/Facility/email-address"/>
		</email>

		<nationalProviderID>
			<xsl:value-of select="Person/npi-"/>
		</nationalProviderID>

		<UPIN>
			<xsl:value-of select="Person/upin-"/>
		</UPIN>

		<fullName>
			<xsl:text>Dr. </xsl:text><xsl:value-of select="Person/first-name"/><xsl:text> </xsl:text><xsl:value-of select="Person/middle-name"/><xsl:text> </xsl:text><xsl:value-of select="Person/last-name"/>
		</fullName>

		<riskClinic></riskClinic>

		<dataSource>
			<xsl:value-of select="Person/site-localids/site-localid/site"/>
		</dataSource>

		<localProviderID>
			<xsl:value-of select="Person/site-localids/site-localid/value"/>
		</localProviderID>

		<displayName>
			<xsl:text>Dr. </xsl:text><xsl:value-of select="Person/first-name"/><xsl:text> </xsl:text><xsl:value-of select="Person/middle-name"/><xsl:text> </xsl:text><xsl:value-of select="Person/last-name"/> 
		</displayName>

		<networkID>
			<xsl:value-of select="Person/nt-login"/>
		</networkID>


</xsl:template>

</xsl:stylesheet> 


