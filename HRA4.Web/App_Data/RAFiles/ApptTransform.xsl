<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<HraImportAttemp>
<xsl:for-each select="//RecentExams">
	<appointment>
		<patientname>
			<xsl:value-of select="PatientName"/>
		</patientname>
		<mrn>
			<xsl:value-of select="MRN"/>
		</mrn>
		<apptdate>
			<xsl:value-of select="substring(Exam_Date,6,2)"/>/<xsl:value-of select="substring(Exam_Date,9,2)"/>/<xsl:value-of select="substring(Exam_Date,1,4)"/>
		</apptdate>
		<appttime>
			<xsl:value-of select="Exam_Time"/>
		</appttime>
		<gender>
			<xsl:value-of select="Gender"/>
		</gender>
		<dob>
			<xsl:value-of select="substring(DOB,6,2)"/>/<xsl:value-of select="substring(DOB,9,2)"/>/<xsl:value-of select="substring(DOB,1,4)"/>
		</dob>
		<pcpname>
			<xsl:value-of select="PhysicianName"/>
		</pcpname>
		<!-- <EMPI>
			<xsl:value-of select="EMPI"/>
		</EMPI> -->
		<clinicID>1</clinicID>
		<address1>
			<xsl:value-of select="ptAddrStreet1"/>
		</address1>	
		<city>
			<xsl:value-of select="ptAddrCity"/>
		</city>
		<state>
			<xsl:value-of select="ptAddrState"/>
		</state>
		<zip>
			<xsl:value-of select="ptAddrZipCode"/>
		</zip>
		<pcpaddress1>
			<xsl:value-of select="PhyStreet1"/>
		</pcpaddress1>
		<pcpaddress2>
			<xsl:value-of select="PhyStreet2"/>
		</pcpaddress2>
		<pcpcity>
			<xsl:value-of select="PhyCity"/>
		</pcpcity>
		<pcpstate>
			<xsl:value-of select="PhyState"/>
		</pcpstate>
		<pcpzip>
			<xsl:value-of select="PhyZipcode"/>
		</pcpzip>
		<pcpphone>
			<xsl:value-of select="PhyPhone"/>
		</pcpphone>
		<appttype>
			<xsl:value-of select="TypeTitle"/>
		</appttype>
		<clinic>
			<xsl:value-of select="LOCATION"/>
		</clinic>
		
	</appointment>
<xsl:text>
</xsl:text>
</xsl:for-each>
</HraImportAttemp>
</xsl:template>
</xsl:stylesheet>