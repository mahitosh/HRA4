<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:hra="http://www.hughesriskapps.com"
    exclude-result-prefixes="xs hra"
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
    
    <xsl:function name="hra:universal-date-from-slashdate" as="xs:string">
        <!-- Allow for input as MM/DD/YYYY or YYYY/MM/DD -->
        <xsl:param name="dateString"/>
        <xsl:variable name="pattern1" select="'^([0-9]{2})/([0-9]{2})/([0-9]{4}).*'"/>
        <xsl:variable name="pattern2" select="'^([0-9]{4})/?([0-9]{2})?/?([0-9]{2})?.*'"/>
        <xsl:variable name="passedYear"
            select="if (matches($dateString,$pattern1)) (: only two digits in first section of / separated date :)
            then replace($dateString, $pattern1, '$3')
            else if (matches($dateString,$pattern2)) then replace($dateString, $pattern2, '$1')
            else ()"/>
        <xsl:variable name="passedMonth"
            select="if (matches($dateString,$pattern1))
            then replace($dateString, $pattern1, '$1')
            else if (matches($dateString,$pattern2)) then replace($dateString, $pattern2, '$2')
            else ()"/>
        <xsl:variable name="passedDay"
            select="if (matches($dateString,$pattern1))
            then replace($dateString, $pattern1, '$2')
            else if (matches($dateString,$pattern2)) then replace($dateString, $pattern2, '$3')
            else ()"/>
        <xsl:sequence
            select="xs:string(concat($passedYear, ($passedMonth, xs:string(month-from-date(current-date())))[1], ($passedDay, xs:string(day-from-date(current-date())))[1]))"/>
    </xsl:function>
    
    <!-- This is an identity copy template; it performs a deep copy, recursively  -->
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    <!-- Match the FamilyHistory element, copy it and its attrs, then rearrange it's children per the HL7 spec -->
    <xsl:template match="FamilyHistory">
        <xsl:copy>
            <xsl:apply-templates select="@*"/>
            <xsl:apply-templates select="id"/>
            <xsl:apply-templates select="code"/>
            <xsl:if test="not(exists(code))">
                <code code="10157-6" codeSystemName="LOINC" displayName="HISTORY OF FAMILY MEMBER DISEASE"/>
            </xsl:if>
            <xsl:apply-templates select="text"/>
            <xsl:apply-templates select="statusCode"/>
            <xsl:apply-templates select="effectiveTime"/>
            <xsl:apply-templates select="confidentialityCode"/>
            <xsl:apply-templates select="uncertaintyCode"/>
            <xsl:apply-templates select="languageCode"/>
            <xsl:apply-templates select="methodCode"/>
            <xsl:apply-templates select="subject"/>
            <xsl:apply-templates select="informant"/>
            <xsl:apply-templates select="risk"/>
            <xsl:apply-templates select="component"/>
        </xsl:copy>
    </xsl:template>
    
    <!-- Match the patient element, copy it and its attrs, then rearrange it's children per the HL7 spec -->
    <xsl:template match="patient">
        <xsl:copy>
            <xsl:apply-templates select="@*"/>
            <xsl:apply-templates select="id"/>
            <xsl:apply-templates select="patientPerson"/>
            <xsl:apply-templates select="providerOrganization"/>
            <xsl:apply-templates select="subjectOf1"/>
            <xsl:if test="not(exists(subjectOf1))">
                <xsl:if test="exists(patientPerson/subjectOf1)">
                    <xsl:apply-templates select="patientPerson/subjectOf1"/>
                </xsl:if>
            </xsl:if>
            <xsl:apply-templates select="subjectOf2"/>
            <xsl:if test="not(exists(subjectOf2))">
                <xsl:if test="exists(patientPerson/subjectOf2)">
                    <xsl:apply-templates select="patientPerson/subjectOf2"/>
                </xsl:if>
            </xsl:if>
        </xsl:copy>
    </xsl:template>

    <!-- provides an exception to the identity template above
        whereby the surgeon general misspelled 'extention' becomes 'extension'  -->
    <xsl:template match="@extention">
        <xsl:attribute name="extension" select="."/>
    </xsl:template>

    <!-- Fix SG occasional use of 'SNOMED COMPLETE', rather than the correct 'SNOMED_CT' -->
    <xsl:template match="@codeSystemName['SNOMED COMPLETE']">
        <xsl:attribute name="codeSystemName" select="'SNOMED_CT'"/>
    </xsl:template>
    
    <!-- Fix SG effectiveTime format, removing slashes  -->
    <xsl:template match="effectiveTime">
        <effectiveTime value="{hra:universal-date-from-slashdate(@value)}"/>
    </xsl:template>

    <!-- Fix SG birthTime format, removing slashes  -->
    <xsl:template match="birthTime">
        <birthTime value="{hra:universal-date-from-slashdate(@value)}"/>
    </xsl:template>

    <!-- if the patient id has a zero-length string extension attribute, due to de-identification,
        for example, remove the extension attribute altogether to make HL7 valid -->
    <xsl:template match="patient/id/@extension[. eq '']">
    </xsl:template>
    
    <xsl:template match="patientPerson">
        <xsl:copy>
            <xsl:apply-templates select="@* except (@classCode|@determinerCode)"/>
            <xsl:attribute name="classCode" select="'PSN'"/>
            <xsl:attribute name="determinerCode" select="'INSTANCE'"/>
            <xsl:apply-templates select="id"/>
            <xsl:apply-templates select="name"/>
            <xsl:apply-templates select="telecom"/>
            <xsl:apply-templates select="administrativeGenderCode"/>
            <xsl:apply-templates select="birthTime"/>
            <xsl:apply-templates select="deceasedIndCode"/>
            <xsl:apply-templates select="deceasedTime"/>
            <xsl:apply-templates select="raceCode"/>
            <xsl:apply-templates select="ethnicGroupCode"/>
            <xsl:apply-templates select="relative"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="relationshipHolder">
        <xsl:copy>
            <xsl:apply-templates select="@* except (@classCode|@determinerCode)"/>
            <xsl:attribute name="classCode" select="'PSN'"/>
            <xsl:attribute name="determinerCode" select="'INSTANCE'"/>
            <xsl:apply-templates select="id"/>
            <xsl:apply-templates select="name"/>
            <xsl:apply-templates select="telecom"/>
            <xsl:apply-templates select="administrativeGenderCode"/>
            <xsl:apply-templates select="birthTime"/>
            <xsl:apply-templates select="deceasedIndCode"/>
            <xsl:apply-templates select="deceasedTime"/>
            <xsl:apply-templates select="raceCode"/>
            <xsl:apply-templates select="ethnicGroupCode"/>
            <xsl:apply-templates select="relative"/>
        </xsl:copy>
    </xsl:template>

    <!-- While the following code would make the names true HL7, HRA cannot read this in, so we'll leave it as is -->
    <!--    <xsl:template match="name">
                <name>
                    <xsl:apply-templates select="(@* except @formatted)|*"/>
                    <xsl:value-of select="@formatted"/>
                </name>
            </xsl:template>
    -->
    
    <!-- first and last and middle name attributes are not HL7 compatible; normally remove them  -->
    <!-- but HRA can read these in, so this is ok for XML import -->
    <!--    <xsl:template match="name/@first | name/@last | name/@middle | name/@formatted"/> -->
    
    <!-- SG uses non-HL7 element name; convert to proper HL7 element; must be true or false, if UNKNOWN, leave off -->
    <xsl:template match="deceasedIndCode">
        <xsl:if test="(lower-case(@value) eq 'true') or (lower-case(@value) eq 'false')">
            <deceasedInd value="{@value}"/>
        </xsl:if>
    </xsl:template>

    <xsl:template match="raceCode">
        <raceCode>
            <xsl:attribute name="code" select="(if (@code ne '') then @code else (), 'Unknown')[1]"/>
            <xsl:attribute name="codeSystemName" select="(if (@codeSystemName ne '') then @codeSystemName else (), 'HL7')[1]"/>
            <xsl:attribute name="displayName" select="(if (@displayName ne '') then @displayName else (), 'Unknown')[1]"/>
            <!-- ignore any other attributes -->
            <xsl:apply-templates select="*"/>
        </raceCode>
    </xsl:template>

    <xsl:template match="ethnicGroupCode">
        <ethnicGroupCode>
            <xsl:attribute name="code" select="(if (@code ne '') then @code else (), 'Unknown')[1]"/>
            <xsl:attribute name="codeSystemName" select="(if (@codeSystemName ne '') then @codeSystemName else (), 'HL7')[1]"/>
            <xsl:attribute name="displayName" select="(if (@displayName ne '') then @displayName else (), 'Unknown')[1]"/>
            <!-- ignore any other attributes -->
            <xsl:apply-templates select="*"/>
        </ethnicGroupCode>
    </xsl:template>

    <xsl:template match="relative">
        <xsl:copy>
            <xsl:apply-templates select="@* except @classCode"/>
            <xsl:attribute name="classCode" select="'PRS'"/>
            <xsl:apply-templates select="code"/>
            <xsl:apply-templates select="relationshipHolder"/>
            <xsl:apply-templates select="subjectOf1"/>
            <xsl:if test="not(exists(subjectOf1))">
                <xsl:if test="exists(relationshipHolder/subjectOf1)">
                    <xsl:apply-templates select="relationshipHolder/subjectOf1"/>
                </xsl:if>
            </xsl:if>
            <xsl:apply-templates select="subjectOf2"/>
            <xsl:if test="not(exists(subjectOf2))">
                <xsl:if test="exists(relationshipHolder/subjectOf2)">
                    <xsl:apply-templates select="relationshipHolder/subjectOf2"/>
                </xsl:if>
            </xsl:if>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="code">
        <code>
            <xsl:choose>
                <xsl:when test="string-length(@code) eq 0">
                    <xsl:attribute name="code" select="'NotAvailable'"/>
                    <xsl:apply-templates select="@* except @code"/>
                    <xsl:apply-templates />
                </xsl:when>
                <xsl:otherwise>
                    <xsl:apply-templates select="@*"/>
                    <xsl:apply-templates />
                </xsl:otherwise>
            </xsl:choose>
        </code>
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

    <!-- only include subjectOf1 nodes with positive numeric ages; otherwise skip entirely -->
    <xsl:template match="subjectOf1[livingEstimatedAge/value]" priority="2">
        <xsl:if test="((number(./livingEstimatedAge/value/low/@value)) ge 0) or ((number(./livingEstimatedAge/value/high/@value)) ge 0) or ((number(./livingEstimatedAge/value/@value)) ge 0)">
            <subjectOf1 typeCode="SBJ">
                <xsl:apply-templates select="@*|*"/>
            </subjectOf1>
        </xsl:if>
    </xsl:template>
    
    <xsl:template match="subjectOf1[deceasedEstimatedAge/value]" priority="2">
        <xsl:if test="((number(./deceasedEstimatedAge/value/low/@value)) ge 0) or ((number(./deceasedEstimatedAge/value/high/@value)) ge 0) or ((number(./deceasedEstimatedAge/value/@value)) ge 0)">
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

    <xsl:template match="livingEstimatedAge/value | deceasedEstimatedAge/value | dataEstimatedAge/value">
        <value>
            <xsl:if test="@value">
                <xsl:attribute name="value" select="@value"/>
            </xsl:if>
            <!-- ignore any other attributes, esp. 'unit', except that if unit is specified and is not 'year', re-scale the data to years -->
            <xsl:if test="low/@value">
                <low>
                    <xsl:attribute name="value" select="
                        if(@unit eq 'day')
                            then string(round(number(low/@value) div 365))
                        else if (@unit eq 'week')
                            then string(round(number(low/@value) div 52))
                        else if (@unit eq 'month')
                            then string(round(number(low/@value) div 12))
                        else low/@value"/>
                </low>
            </xsl:if>
            <xsl:if test="high/@value">
                <high>
                    <xsl:attribute name="value" select="
                        if(@unit = 'day')
                            then string(round(number(high/@value) div 365))
                        else if (@unit eq 'week')
                            then string(round(number(high/@value) div 52))
                        else if (@unit = 'month')
                            then string(round(number(high/@value) div 12))
                        else high/@value"/>
                </high>
            </xsl:if>
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
        <xsl:for-each select="clinicalObservation">
            <subjectOf2 typeCode="SBJ">
                <clinicalObservation classCode="OBS" moodCode="EVN">
                    <xsl:variable name="SGHeight" select="
                        if (code[lower-case(@displayName)='height'] and (code/@codeSystemName ne 'UML'))
                        then 1
                        else 0"/>
                    <xsl:variable name="SGWeight" select="
                        if (code[lower-case(@displayName)='weight'] and (code/@codeSystemName ne 'UML'))
                            then 1
                            else 0"/>
                    <code>
                        <xsl:choose>
                            <!-- Are we treating DCIS as cancer AND this is DCIS? -->
                            <xsl:when test="($dcisAsCancer eq '1') and code[contains(lower-case(@displayName),'dcis') or contains(lower-case(@displayName),'ductal carcinoma in situ of the breast')]">
                                <!-- hard code DCIS as cancer -->
                                <xsl:attribute name="code" select="'140'"/>
                                <xsl:attribute name="codeSystemName" select="'UML'"/>
                                <xsl:attribute name="displayName" select="'Breast Cancer (DCIS) but considered Invasive Breast Cancer for BRCAPro'"/>
                            </xsl:when>
                            <!-- check for surgeon general style height clinicalObservation and modify to HL7 -->
                            <xsl:when test="$SGHeight eq 1">
                                <xsl:attribute name="code" select="'132'"/>
                                <xsl:attribute name="codeSystemName" select="'UML'"/>
                                <xsl:attribute name="displayName" select="'Height'"/>
                            </xsl:when>
                            <!-- check for surgeon general style weight clinicalObservation and modify to HL7 -->
                            <xsl:when test="$SGWeight eq 1">
                                <xsl:attribute name="code" select="'133'"/>
                                <xsl:attribute name="codeSystemName" select="'UML'"/>
                                <xsl:attribute name="displayName" select="'Weight'"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <!-- code, codeSystemName, and displayName must not be 0-length strings -->
                                <xsl:attribute name="code" select="(if (code/@code ne '') then code/@code else (), 'Unknown')[1]"/>
                                <xsl:attribute name="codeSystemName" select="(if (code/@codeSystemName ne '') then code/@codeSystemName else (), 'HL7')[1]"/>
                                <xsl:attribute name="displayName" select="(if (code/@displayName ne '') then code/@displayName else (), 'Unknown')[1]"/>
                            </xsl:otherwise>
                            <!-- don't preserve any other attributes of the clinicalObservation/code element here -->
                        </xsl:choose>
                        <xsl:if test="starts-with(lower-case(code/@displayName), 'number of')">
                            <qualifier>
                                <value code="{code/@value}"/>
                            </qualifier>
                         </xsl:if>
                        <xsl:if test="(code/@displayName eq 'Height') and ($SGHeight eq 0)">
                            <!-- Height (in meters, to nearest cm i.e. 1.65) -->
                            <qualifier>
                                <value code="{code/@value}"/>
                            </qualifier>
                        </xsl:if>
                        <xsl:if test="(code/@displayName eq 'Weight') and ($SGWeight eq 0)">
                            <!-- Weight (in kilograms) -->
                            <qualifier>
                                <value code="{code/@value}"/>
                            </qualifier>
                        </xsl:if>
                        <xsl:if test="$SGHeight eq 1">
                            <!-- SurgeonGeneral format; convert Height to meters if in inches -->
                            <xsl:variable name="mHt" select="if (value/@unit='inches') then format-number(.0254*xs:double(value/@value),'##0.0#') else value/@value"/>
                            <qualifier>
                                <value code="{$mHt}"/>
                            </qualifier>
                        </xsl:if>
                        <xsl:if test="$SGWeight eq 1">
                            <!-- SurgeonGeneral format; convert Weight to kilograms if in pounds -->
                            <xsl:variable name="kgWt" select="if (value/@unit='pound') then format-number(.45359237*xs:double(value/@value),'##0.0#') else value/@value"/>
                            <qualifier>
                                <value code="{$kgWt}"/>
                            </qualifier>
                        </xsl:if>
                        <xsl:if test="code/@displayName eq 'HRT Length Past'">
                            <!-- Number of years, decimal allowed: i.e. 18 months should be entered as 1.5 years. -->
                            <!-- 0 = not relevant, missing or unknown (equivalent to not including this clinical observation) -->
                            <qualifier>
                                <value code="{code/@value}"/>
                            </qualifier>
                        </xsl:if>
                        <xsl:if test="code/@displayName eq 'HRT Length Intent'">
                            <!-- Length of time woman intends to use HRT in the future (if current user) -->
                            <!-- Number of years, decimal allowed: i.e. 18 months should be entered as 1.5 years. -->
                            <!-- 0 = not relevant, missing or unknown (equivalent to not including this clinical observation) -->
                            <qualifier>
                                <value code="{code/@value}"/>
                            </qualifier>
                        </xsl:if>
                        <xsl:if test="code/@displayName eq 'HRT Last Use'">
                            <!-- Time since HRT last used (only relevant if previous HRT user) -->
                            <!-- Number of years, decimal allowed: i.e. 18 months should be entered as 1.5 years. -->
                            <!-- 0 = not relevant, missing or unknown (equivalent to not including this clinical observation) -->
                            <qualifier>
                                <value code="{code/@value}"/>
                            </qualifier>
                        </xsl:if>
                        <xsl:if test="code/@displayName eq 'Identical twin (person)'">
                            <qualifier>
                                <value code="{if (string-length(code/@value) gt 0) then code/@value else 'NotAvailable'}"/>
                            </qualifier>
                        </xsl:if>
                    </code>
                    <xsl:if test="starts-with(lower-case(code/@displayName), 'age at')">
                        <xsl:call-template name="addAgeData">
                            <xsl:with-param name="value" select="code/@value"></xsl:with-param>
                        </xsl:call-template>
                    </xsl:if>
                    <xsl:if test="code/@displayName eq 'Menopausal Status'">
                        <!-- 0=pre-menopausal; 1=peri-menopausal; 2=post-menopausal; 3=Unknown -->
                        <statusCode code="{code/@value}"/>
                    </xsl:if>
                    <xsl:if test="code/@displayName eq 'Menopause Age'">
                        <!-- Age in Years (integer) for post-menopausal -->
                        <xsl:call-template name="addAgeData">
                            <xsl:with-param name="value" select="code/@value"></xsl:with-param>
                        </xsl:call-template>
                    </xsl:if>
                    <xsl:if test="code/@displayName eq 'HRT Use'">
                        <!-- 0=Never; 1=Previous user (more than 5 years ago); 2=Previous user (less than 5 years ago); 3=Current user -->
                        <!-- 0 = not relevant, missing or unknown (equivalent to not including this clinical observation) -->
                        <statusCode code="{code/@value}"/>
                    </xsl:if>
                    <xsl:if test="code/@displayName eq 'HRT Type'">
                        <!-- 1=estrogen; 2=combined; 0 = not relevant, missing or unknown (equivalent to not including this clinical observation) -->
                        <!-- Note these codes differ slightly from the Tyrer-Cusick model input parameter codes, but are more consistently defined with other related codes (using 0 for unknown) and provide more information -->
                        <statusCode code="{code/@value}"/>
                    </xsl:if>
                    <xsl:choose>
                        <xsl:when test="($SGWeight eq 1) or ($SGHeight eq 1)">
                            <xsl:apply-templates select="* except (code|value)"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:apply-templates select="* except code"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </clinicalObservation>
            </subjectOf2>
            <xsl:if test="string-length(code/@ERStatus) gt 0">
                <subjectOf2 typeCode="SBJ">
                    <clinicalObservation classCode="OBS" moodCode="EVN">
                        <code>
                            <xsl:attribute name="code" select="translate(code/@ERStatus,' ','_')"/>    
                            <xsl:attribute name="codeSystemName" select="'ERStatus'"/>
                        </code>                    
                        <xsl:apply-templates select="* except code"/>
                    </clinicalObservation>
                </subjectOf2>
            </xsl:if>
            <xsl:if test="string-length(code/@PRStatus) gt 0">
                <subjectOf2 typeCode="SBJ">
                    <clinicalObservation classCode="OBS" moodCode="EVN">
                        <code>
                            <xsl:attribute name="code" select="translate(code/@PRStatus,' ','_')"/>    
                            <xsl:attribute name="codeSystemName" select="'PRStatus'"/>
                        </code>                    
                        <xsl:apply-templates select="* except code"/>
                    </clinicalObservation>
                </subjectOf2>
            </xsl:if>
            <xsl:if test="string-length(code/@msiResults) gt 0">
                <subjectOf2 typeCode="SBJ">
                    <clinicalObservation classCode="OBS" moodCode="EVN">
                        <code>
                            <xsl:attribute name="code" select="translate(code/@msiResults,' ','_')"/>    
                            <xsl:attribute name="codeSystemName" select="'msiResults'"/>
                        </code>                    
                        <xsl:apply-templates select="* except code"/>
                    </clinicalObservation>
                </subjectOf2>
            </xsl:if>
            <xsl:if test="string-length(code/@location) gt 0">
                <subjectOf2 typeCode="SBJ">
                    <clinicalObservation classCode="OBS" moodCode="EVN">
                        <code>
                            <xsl:attribute name="code" select="translate(code/@location,' ','_')"/>    
                            <xsl:attribute name="codeSystemName" select="'location'"/>
                        </code>                    
                        <xsl:apply-templates select="* except code"/>
                    </clinicalObservation>
                </subjectOf2>
            </xsl:if>
            <xsl:if test="string-length(code/@immunohistochemistry) gt 0">
                <subjectOf2 typeCode="SBJ">
                    <clinicalObservation classCode="OBS" moodCode="EVN">
                        <code>
                            <xsl:attribute name="code" select="translate(code/@immunohistochemistry,' ','_')"/>    
                            <xsl:attribute name="codeSystemName" select="'immunohistochemistry'"/>
                        </code>                    
                        <xsl:apply-templates select="* except code"/>
                    </clinicalObservation>
                </subjectOf2>
            </xsl:if>
            <xsl:if test="string-length(code/@HER2NeuIHC) gt 0">
                <subjectOf2 typeCode="SBJ">
                    <clinicalObservation classCode="OBS" moodCode="EVN">
                        <code>
                            <xsl:attribute name="code" select="translate(code/@HER2NeuIHC,' ','_')"/>    
                            <xsl:attribute name="codeSystemName" select="'HER2NeuIHC'"/>
                        </code>                    
                        <xsl:apply-templates select="* except code"/>
                    </clinicalObservation>
                </subjectOf2>
            </xsl:if>
            <xsl:if test="string-length(code/@HER2NeuFISH) gt 0">
                <subjectOf2 typeCode="SBJ">
                    <clinicalObservation classCode="OBS" moodCode="EVN">
                        <code>
                            <xsl:attribute name="code" select="translate(code/@HER2NeuFISH,' ','_')"/>    
                            <xsl:attribute name="codeSystemName" select="'HER2NeuFISH'"/>
                        </code>                    
                        <xsl:apply-templates select="* except code"/>
                    </clinicalObservation>
                </subjectOf2>
            </xsl:if>
        </xsl:for-each>
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
            <component3 typeCode="COMP">
                <sequenceVariation classCode="SEQVAR" moodCode="EVN">
                    <!-- TODO: add mutation details; not needed for BayesMendel -->
                    <interpretationCode code="{if (empty($result) or (string-length($result) = 0)) then 'NotAvailable' else $result}"/>
                </sequenceVariation>
            </component3>
        </geneticLocus>
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
    
    <xsl:template match="sourceOf">
        <sourceOf typeCode="CAUS">
            <clinicalObservation classCode="OBS" moodCode="EVN">
                <xsl:apply-templates/>
            </clinicalObservation>
        </sourceOf>
    </xsl:template>

</xsl:stylesheet>
