<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<HraImportAttemp>
<xsl:for-each select="Problem-List/Problem">
	<problem>
		<Problem>
			<xsl:choose>
			     <xsl:when test="Result-Text != null">
			         <xsl:value-of select="Result-Text" />
			     </xsl:when>
			     <xsl:otherwise>
			         <xsl:value-of select="Description" />       
			     </xsl:otherwise>
			</xsl:choose>
		</Problem>
		<Description> 
			<xsl:value-of select="Description"/>
		</Description>
		<Comment>
		 	<xsl:value-of select="Comment-Text"/>
		</Comment>
		<Onset>
			<xsl:value-of select="Onset"/>
		</Onset>
		<Acuity>
		 	<xsl:value-of select="Acuity"/>
		</Acuity>
		<Severity>
		 	<xsl:value-of select="Severity"/>
		</Severity>
		<Condition>
		 	<xsl:value-of select="Condition"/>
		</Condition>
		<Location>
		 	<xsl:value-of select="Location"/>
		</Location>
		<Modifiers>
		 	<xsl:value-of select="Modifiers"/>
		</Modifiers>
		<ServiceDate>
		 	<xsl:value-of select="Service-Date"/>
		</ServiceDate>
		<ResolutionDate>
		 	<xsl:value-of select="Resolution-Date"/>
		</ResolutionDate>
		<ProcedureDate>
		 	<xsl:value-of select="Procedure-Date"/>
		</ProcedureDate>
		<ICD9Code>
		 	<xsl:value-of select="ICD9-Code"/>
		</ICD9Code>
		<ConceptID>
		 	<xsl:value-of select="Concept-ID"/>
		</ConceptID>
		<RecID>
		 	<xsl:value-of select="Rec-ID"/>
		</RecID>
		<MPIID>
		 	<xsl:value-of select="MPI-ID"/>
		</MPIID>
		<SensitivityFlag>
		 	<xsl:value-of select="Sensitivity-Flag"/>
		</SensitivityFlag>
		<Type>
		 	<xsl:value-of select="Type"/>
		</Type>
	</problem>
<xsl:text>
</xsl:text>
</xsl:for-each>
</HraImportAttemp>
</xsl:template>
</xsl:stylesheet> 
