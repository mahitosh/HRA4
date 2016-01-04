<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    version="2.0">

    <xsl:output method="xml" indent="yes"/>
    <xsl:preserve-space elements="*"/>
    
    <!-- Following can be overidden by transformer invocation with parameter set -->
    <xsl:param name="localBaseUri" select="'file:/C:/Program%20Files/riskAppsV2/tools/'"/>
    <xsl:param name="dcisAsCancer" select="'0'"/>  <!-- BayesMendel default is that DCIS is not considered cancer -->
    
    <xsl:variable name="geneCodes" select="doc(resolve-uri('GeneCodes.xml', $localBaseUri))"/>
    <!--Subset of GeneCodes which use NCBI Entrez-->
    <xsl:variable name="NCBItable">
        <xsl:copy-of select="$geneCodes/root/Gene[codeSystemName='NCBI Entrez']"/>
    </xsl:variable>
    <!--Subset of GeneCodes which use HGNC-->
    <xsl:variable name="HGNCtable">
        <xsl:copy-of select="$geneCodes/root/Gene[codeSystem='HGNC']"/>
    </xsl:variable>
    

    <!-- This is an identity copy template; it performs a deep copy, recursively  -->
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    <!-- if the patient id has a zero-length string extension attribute, due to de-identification,
        for example, remove the extension attribute altogether to make HL7 valid -->
    <xsl:template match="patient/id/@extension[. eq '']">
    </xsl:template>
    
    <xsl:template match="patientPerson">
        <patientPerson classCode="PSN" determinerCode="INSTANCE">
            <!-- copy over the rest of patientPerson's attributes and all xml children  -->
            <xsl:apply-templates select="@*|*"/>
        </patientPerson>
    </xsl:template>
    
    <!-- Don't remove names, even though they're not in HL7 format, so HRA can process on import -->
    <!-- For output to Service, HRA will have deidentified the names already -->
    <!--
        <xsl:template match="name">
        </xsl:template>
    -->
    
    <xsl:template match="deceasedInd">
        <deceasedInd>
            <xsl:attribute name="value" select="@code"/>
            <xsl:apply-templates select="(@* except @code)|*"/>
        </deceasedInd>
    </xsl:template>

    <xsl:template match="raceCode">
        <raceCode>
            <xsl:attribute name="code" select="(if (@code ne '') then @code else (), 'Unknown')[1]"/>
            <xsl:attribute name="codeSystemName" select="(if (@codeSystemName ne '') then @codeSystemName else (), 'HL7')[1]"/>
            <xsl:attribute name="displayName" select="(if (@displayName ne '') then @displayName else if (@originalText ne '') then @originalText else (), 'Unknown')[1]"/>
            <!-- ignore any other attributes -->
            <xsl:apply-templates select="*"/>
        </raceCode>
    </xsl:template>

    <xsl:template match="ethnicGroupCode">
        <ethnicGroupCode>
            <xsl:attribute name="code" select="(if (@code ne '') then @code else (), 'Unknown')[1]"/>
            <xsl:attribute name="codeSystemName" select="(if (@codeSystemName ne '') then @codeSystemName else (), 'HL7')[1]"/>
            <xsl:attribute name="displayName" select="(if (@displayName ne '') then @displayName else if (@originalText ne '') then @originalText else (), 'Unknown')[1]"/>
            <!-- ignore any other attributes -->
            <xsl:apply-templates select="*"/>
        </ethnicGroupCode>
    </xsl:template>

    <xsl:template match="relative">
        <relative classCode="PRS">
            <xsl:if test="not(string-length(code/@code) gt 0)">
                <code code="NotAvailable"/>
            </xsl:if>
            <xsl:apply-templates select="@*|*"/>
        </relative>
    </xsl:template>

    <xsl:template match="relationshipHolder">
        <relationshipHolder classCode="PSN" determinerCode="INSTANCE">
            <xsl:apply-templates select="@*|*"/>
        </relationshipHolder>
    </xsl:template>

    <xsl:template match="livingEstimatedAge">
        <livingEstimatedAge classCode="OBS" moodCode="EVN">
            <xsl:apply-templates select="*"/>
        </livingEstimatedAge>
    </xsl:template>

    <xsl:template match="deceasedEstimatedAge">
        <deceasedEstimatedAge classCode="OBS" moodCode="EVN">
            <xsl:apply-templates select="*"/>
        </deceasedEstimatedAge>
    </xsl:template>

    <xsl:template match="dataEstimatedAge">
        <dataEstimatedAge classCode="OBS" moodCode="EVN">
            <xsl:apply-templates select="*"/>
        </dataEstimatedAge>
    </xsl:template>

    <!-- only include subjectOf1 nodes with  positive numeric ages; otherwise skip entirely -->
    <xsl:template match="subjectOf1[livingEstimatedAge/value]" priority="2">
        <xsl:if test="number(./livingEstimatedAge/value/@value) ge 0">
            <subjectOf1 typeCode="SBJ">
                <xsl:apply-templates select="@*|*"/>
            </subjectOf1>
        </xsl:if>
    </xsl:template>
    
    <xsl:template match="subjectOf1[deceasedEstimatedAge/value]" priority="2">
        <xsl:if test="number(./deceasedEstimatedAge/value/@value) ge 0">
            <subjectOf1 typeCode="SBJ">
                <xsl:apply-templates select="@*|*"/>
            </subjectOf1>
        </xsl:if>
    </xsl:template>

    <!-- only include subjectOf nodes with positive numeric dataEstimatedAges; otherwise skip entirely -->
    <xsl:template match="subject[dataEstimatedAge/value]" priority="2">
        <xsl:if test="((number(./dataEstimatedAge/value/low/@value)) ge 0) or ((number(./dataEstimatedAge/value/high/@value)) ge 0) or ((number(./dataEstimatedAge/value/@value)) ge 0)">
            <subject typeCode="SUBJ">
                <xsl:apply-templates select="@*|*"/>
            </subject>
        </xsl:if>
    </xsl:template>

    <xsl:template match="livingEstimatedAge/value | deceasedEstimatedAge/value">
        <value>
            <xsl:attribute name="value" select="@value"/>
            <!-- ignore any other attributes, esp. 'unit' -->
            <xsl:apply-templates select="*"/>
        </value>
    </xsl:template>

    <xsl:template match="subject">
        <subject typeCode="SUBJ">
            <xsl:apply-templates select="@*|*"/>
        </subject>
    </xsl:template>

    <xsl:template match="subjectOf1">
        <subjectOf1 typeCode="SBJ">
            <xsl:apply-templates select="@*|*"/>
        </subjectOf1>
    </xsl:template>

    <xsl:template match="subjectOf2">
        <subjectOf2 typeCode="SBJ">
            <xsl:apply-templates select="@*|*"/>
        </subjectOf2>
    </xsl:template>

    <xsl:template match="subjectOf2[clinicalObservation]">
        <subjectOf2 typeCode="SBJ">
            <clinicalObservation classCode="OBS" moodCode="EVN">
                <code>
                    <xsl:choose>
                        <!-- Are we treating DCIS as cancer AND this is DCIS? -->
                        <xsl:when test="($dcisAsCancer eq '1') and not (clinicalObservation/code[contains(lower-case(@displayName),'borderline dcis')]) and clinicalObservation/code[contains(lower-case(@displayName),'dcis') or contains(lower-case(@displayName),'ductal carcinoma in situ of the breast')]">
                            <!-- hard code DCIS as cancer -->
                            <xsl:attribute name="code" select="'140'"/>
                            <xsl:attribute name="codeSystemName" select="'UML'"/>
                            <xsl:attribute name="displayName" select="'Breast Cancer (DCIS) but considered Invasive Breast Cancer for BRCAPro'"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <!-- code, codeSystemName, and displayName must not be 0-length strings -->
                            <xsl:attribute name="code" select="(if (clinicalObservation/code/@code ne '') then clinicalObservation/code/@code else (), 'Unknown')[1]"/>
                            <xsl:attribute name="codeSystemName" select="(if (clinicalObservation/code/@codeSystemName ne '') then clinicalObservation/code/@codeSystemName else (), 'HL7')[1]"/>
                            <xsl:attribute name="displayName" select="(if (clinicalObservation/code/@displayName ne '') then clinicalObservation/code/@displayName else (), 'Unknown')[1]"/>
                        </xsl:otherwise>
                        <!-- don't preserve any other attributes of the clinicalObservation/code element here -->
                    </xsl:choose>
                    <xsl:choose>
                        <xsl:when test="starts-with(lower-case(clinicalObservation/code/@displayName), 'number of')
                                        and (string-length(clinicalObservation/code/@value) gt 0)">
                            <qualifier>
                                <value code="{clinicalObservation/code/@value}"/>
                            </qualifier>
                         </xsl:when>
                        <xsl:when test="(clinicalObservation/code/@displayName = ('Height', 'Weight', 'HRT Length Past', 'HRT Length Intent', 'HRT Last Use',
                            'Identical twin (person)', 'Cigarette Smoking years', 'Cigarettes per day', 'Vegetable servings per day'))
                                        and (string-length(clinicalObservation/code/@value) gt 0)">
                            <qualifier>
                                <value code="{clinicalObservation/code/@value}"/>
                            </qualifier>
                        </xsl:when>
                        <!-- if HRA already included an HL7 compatible qualifier element, use it as is -->
                        <xsl:when test="exists(clinicalObservation/code/qualifier/value[string-length(@code) gt 0])">
                            <qualifier>
                                <value code="{clinicalObservation/code/qualifier/value/@code}"/>
                            </qualifier>
                        </xsl:when>
                    </xsl:choose>
                </code>
                <xsl:if test="starts-with(lower-case(clinicalObservation/code/@displayName), 'age at')
                                and (string-length(clinicalObservation/code/@value) gt 0)">
                    <xsl:call-template name="addAgeData">
                        <xsl:with-param name="value" select="clinicalObservation/code/@value"></xsl:with-param>
                    </xsl:call-template>
                </xsl:if>
                <xsl:if test="(clinicalObservation/code/@displayName eq 'Menopausal Status')
                                and (string-length(clinicalObservation/code/@value) gt 0)">
                    <!-- 0=pre-menopausal; 1=peri-menopausal; 2=post-menopausal; 3=Unknown -->
                    <statusCode code="{clinicalObservation/code/@value}"/>
                </xsl:if>
                <xsl:if test="(clinicalObservation/code/@displayName eq 'Menopause Age')
                                and (string-length(clinicalObservation/code/@value) gt 0)">
                    <!-- Age in Years (integer) for post-menopausal -->
                    <xsl:call-template name="addAgeData">
                        <xsl:with-param name="value" select="clinicalObservation/code/@value"></xsl:with-param>
                    </xsl:call-template>
                </xsl:if>
                <xsl:if test="(clinicalObservation/code/@displayName eq 'HRT Use')
                                and (string-length(clinicalObservation/code/@value) gt 0)">
                    <!-- 0=Never; 1=Previous user (more than 5 years ago); 2=Previous user (less than 5 years ago); 3=Current user -->
                    <!-- 0 = not relevant, missing or unknown (equivalent to not including this clinical observation) -->
                    <statusCode code="{clinicalObservation/code/@value}"/>
                </xsl:if>
                <xsl:if test="(clinicalObservation/code/@displayName eq 'HRT Type')
                                and (string-length(clinicalObservation/code/@value) gt 0)">
                    <!-- 1=estrogen; 2=combined; 0 = not relevant, missing or unknown (equivalent to not including this clinical observation) -->
                    <!-- Note these codes differ slightly from the Tyrer-Cusick model input parameter codes, but are more consistently defined with other related codes (using 0 for unknown) and provide more information -->
                    <statusCode code="{clinicalObservation/code/@value}"/>
                </xsl:if>
                <xsl:apply-templates select="clinicalObservation/* except clinicalObservation/code"/>
            </clinicalObservation>
        </subjectOf2>
        <xsl:if test="string-length(clinicalObservation/code/@ERStatus) gt 0">
            <subjectOf2 typeCode="SBJ">
                <clinicalObservation classCode="OBS" moodCode="EVN">
                    <code>
                        <xsl:attribute name="code" select="translate(clinicalObservation/code/@ERStatus,' ','_')"/>    
                        <xsl:attribute name="codeSystemName" select="'ERStatus'"/>
                    </code>                    
                    <xsl:apply-templates select="clinicalObservation/* except clinicalObservation/code"/>
                </clinicalObservation>
            </subjectOf2>
        </xsl:if>
        <xsl:if test="string-length(clinicalObservation/code/@PRStatus) gt 0">
            <subjectOf2 typeCode="SBJ">
                <clinicalObservation classCode="OBS" moodCode="EVN">
                    <code>
                        <xsl:attribute name="code" select="translate(clinicalObservation/code/@PRStatus,' ','_')"/>    
                        <xsl:attribute name="codeSystemName" select="'PRStatus'"/>
                    </code>                    
                    <xsl:apply-templates select="clinicalObservation/* except clinicalObservation/code"/>
                </clinicalObservation>
            </subjectOf2>
        </xsl:if>
        <xsl:if test="string-length(clinicalObservation/code/@msiResults) gt 0">
            <subjectOf2 typeCode="SBJ">
                <clinicalObservation classCode="OBS" moodCode="EVN">
                    <code>
                        <xsl:attribute name="code" select="translate(clinicalObservation/code/@msiResults,' ','_')"/>    
                        <xsl:attribute name="codeSystemName" select="'msiResults'"/>
                    </code>                    
                    <xsl:apply-templates select="clinicalObservation/* except clinicalObservation/code"/>
                </clinicalObservation>
            </subjectOf2>
        </xsl:if>
        <xsl:if test="string-length(clinicalObservation/code/@location) gt 0">
            <subjectOf2 typeCode="SBJ">
                <clinicalObservation classCode="OBS" moodCode="EVN">
                    <code>
                        <xsl:attribute name="code" select="translate(clinicalObservation/code/@location,' ','_')"/>    
                        <xsl:attribute name="codeSystemName" select="'location'"/>
                    </code>                    
                    <xsl:apply-templates select="clinicalObservation/* except clinicalObservation/code"/>
                </clinicalObservation>
            </subjectOf2>
        </xsl:if>
        <xsl:if test="string-length(clinicalObservation/code/@immunohistochemistry) gt 0">
            <subjectOf2 typeCode="SBJ">
                <clinicalObservation classCode="OBS" moodCode="EVN">
                    <code>
                        <xsl:attribute name="code" select="translate(clinicalObservation/code/@immunohistochemistry,' ','_')"/>    
                        <xsl:attribute name="codeSystemName" select="'immunohistochemistry'"/>
                    </code>                    
                    <xsl:apply-templates select="clinicalObservation/* except clinicalObservation/code"/>
                </clinicalObservation>
            </subjectOf2>
        </xsl:if>
        <xsl:if test="string-length(clinicalObservation/code/@HER2NeuIHC) gt 0">
            <subjectOf2 typeCode="SBJ">
                <clinicalObservation classCode="OBS" moodCode="EVN">
                    <code>
                        <xsl:attribute name="code" select="translate(clinicalObservation/code/@HER2NeuIHC,' ','_')"/>    
                        <xsl:attribute name="codeSystemName" select="'HER2NeuIHC'"/>
                    </code>                    
                    <xsl:apply-templates select="clinicalObservation/* except clinicalObservation/code"/>
                </clinicalObservation>
            </subjectOf2>
        </xsl:if>
        <xsl:if test="string-length(clinicalObservation/code/@HER2NeuFISH) gt 0">
            <subjectOf2 typeCode="SBJ">
                <clinicalObservation classCode="OBS" moodCode="EVN">
                    <code>
                        <xsl:attribute name="code" select="translate(clinicalObservation/code/@HER2NeuFISH,' ','_')"/>    
                        <xsl:attribute name="codeSystemName" select="'HER2NeuFISH'"/>
                    </code>                    
                    <xsl:apply-templates select="clinicalObservation/* except clinicalObservation/code"/>
                </clinicalObservation>
            </subjectOf2>
        </xsl:if>
    </xsl:template>

    <xsl:template match="GeneticLocus">
        <xsl:variable name="geneName" select="value/@code"/>
        <xsl:variable name="result" select="translate(component3/sequenceVariation/interpretationCode/@code,' ','_')"/>
        <geneticLocus classCode="LOC" moodCode="EVN">
            <text><xsl:value-of select="$NCBItable/Gene[displayName=$geneName]/text"/></text>
            <xsl:if test="string-length(effectiveTime/@value) gt 0">
                <effectiveTime value="{effectiveTime/@value}"/>
            </xsl:if>
            <value code="{($NCBItable/Gene[displayName=$geneName]/code,'NA')[1]}" displayName="{$geneName}" codeSystemName="{($NCBItable/Gene[displayName=$geneName]/codeSystemName,'NA')[1]}">
                <translation code="{($HGNCtable/Gene[displayName=$geneName]/code,'NA')[1]}" displayName="{$geneName}" codeSystem="HGNC"/>
            </value>
            <xsl:apply-templates select="methodCode"/>
            <xsl:apply-templates select="derivedFrom1"/>
            <xsl:apply-templates select="component2"/>
            <component3 typeCode="COMP">
                <sequenceVariation classCode="SEQVAR" moodCode="EVN">
                    <interpretationCode code="{if (empty($result) or (string-length($result) = 0)) then 'NotAvailable' else $result}"/>
                </sequenceVariation>
            </component3>
        </geneticLocus>
    </xsl:template>
    
    <xsl:template match="derivedFrom1">
        <derivedFrom1 typeCode="DRIV">
            <xsl:apply-templates select="*|@*"/>
        </derivedFrom1>
    </xsl:template>
    
    <xsl:template name="addAgeData">
        <xsl:param name="value"></xsl:param>
        <subject typeCode="SUBJ">
            <dataEstimatedAge classCode="OBS" moodCode="EVN">
                <code code="397659008" displayName="Age" codeSystemName="SNOMED_CT"/>
                <value>
                    <low value="{$value}"/>
                    <high value="{$value}"/>
                </value>
            </dataEstimatedAge>
        </subject>
    </xsl:template>

    <!-- leave in the PHI below, as HRA deidentify code will have removed it already -->
    <!-- Remove birthTime; removes private health information; age element will be present, so don't need birthtime  -->
<!--<xsl:template match="birthTime">
    </xsl:template>
-->    
    <!-- Remove any deceasedTime attributes other than value  -->
<!--<xsl:template match="deceasedTime">
        <deceasedTime value="{@value}"/>
    </xsl:template>-->
    
    <!-- Remove phone numbers -->
<!--<xsl:template match="telecom">
    </xsl:template>
-->
</xsl:stylesheet>
