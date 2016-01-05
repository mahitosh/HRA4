<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema" exclude-result-prefixes="xs"
    xmlns:xd="http://www.oxygenxml.com/ns/doc/xsl"
    xmlns:hra="http://www.hughesriskapps.com" version="2.0">
    <xd:doc scope="stylesheet">
        <xd:desc>
            <xd:p><xd:b>Created on:</xd:b> August 6, 2014</xd:p>
            <xd:p><xd:b>Author:</xd:b> pb</xd:p>
            <xd:p></xd:p>
            <xd:p><xd:b>Added patient LCIS bool on:</xd:b> June 2, 2015</xd:p>
            <xd:p><xd:b>Author:</xd:b> pb</xd:p>
            <xd:p><xd:b>Though not a direct input to Gail, Gail is inapplicable if pt. had/has LCIS</xd:b> pb</xd:p>
            <xd:p></xd:p>
            <xd:p>GetRace portion updated to work with codes or displayName and ensure it isn't 'not/non hispanic' (for Epic compatibility)</xd:p>
        </xd:desc>
    </xd:doc>
    
    <xsl:output method="text"/>
    <xsl:strip-space elements="*"/>
    
    <!-- Following can be overidden by transformer invocation with parameter set -->
    <xsl:param name="localBaseUri" select="xs:string('file:/d:/vbshare/')"/>
    
    <!-- Constants -->
    <xsl:variable name="ASHKENAZI_SNOMED" select="'81706006'"/>
    <!-- in lieu of including RaceCodes.xml, we need only these three codes for Gail: -->
    <xsl:variable name="WHITE_RACE_HL7_CODE" select="'2106-3'"/>
    <xsl:variable name="BLACK_RACE_HL7_CODE" select="'2054-5'"/>
    <xsl:variable name="HISPANIC_RACE_HL7_CODE" select="'2135-2'"/>
    
    <!-- Functions -->
    <xsl:function name="hra:date-from-yyyymmdd" as="xs:date">
        <!-- Allow for input as 4 digits (YYYY) or 6 (YYYYMM) or 8 (YYYYMMDD)
            Use today's month and/or day if not provided
            YYYY MM DD are optionally separated by / chars    -->
        <xsl:param name="dateString"/>
        <xsl:variable name="pattern1" select="'^([0-9]{2})/([0-9]{2})/([0-9]{4})*'"/>
        <xsl:variable name="pattern2" select="'^([0-9]{4})/?([0-9]{2})?/?([0-9]{2})?[0-9]*'"/>
        <xsl:variable name="passedYear"
            select="if (matches($dateString,'^[0-9]{2}/')) (: only two digits in first section of / separated date :)
            then replace($dateString, $pattern1, '$3')
            else replace($dateString, $pattern2, '$1')"/>
        <xsl:variable name="passedMonth"
            select="if (matches($dateString,'^[0-9]{2}/'))
            then replace($dateString, $pattern1, '$1')
            else replace($dateString, $pattern2, '$2')"/>
        <xsl:variable name="passedDay"
            select="if (matches($dateString,'^[0-9]{2}/'))
            then replace($dateString, $pattern1, '$2')
            else replace($dateString, $pattern2, '$3')"/>
        <xsl:sequence
            select="xs:date(concat($passedYear, '-', format-number((if ($passedMonth ne '') then ($passedMonth) else (month-from-date(current-date()))) cast as xs:integer, '00'), '-',
            format-number((if ($passedDay ne '') then ($passedDay) else (day-from-date(current-date()))) cast as xs:integer, '00')))"/>
    </xsl:function>

    <xsl:variable name="effectiveTime" as="xs:date"
        select="if (exists(/FamilyHistory/effectiveTime/@value))
        then hra:date-from-yyyymmdd(/FamilyHistory/effectiveTime/@value) else current-date()">
    </xsl:variable>
    
    <xsl:function name="hra:compute-age-from-birthTime" as="xs:string">
        <!-- Rounds to the nearest year  -->
        <xsl:param name="dateString"/>
        <xsl:variable name="passedDate" select="hra:date-from-yyyymmdd($dateString)"/>
        <xsl:sequence select="xs:string(round((($effectiveTime - $passedDate) div xs:dayTimeDuration('P1D')) div 365.25))"/>
    </xsl:function>
    
    <xsl:function name="hra:avgValue">
        <!--expects a single value node, which may or may not have low and high sub-nodes, returns the average value, or empty if no value attributes provided -->
        <xsl:param name="val"/>
        <xsl:choose>
            <xsl:when test="exists($val/@value)">
                <xsl:sequence select="xs:integer(round($val/@value))"/>
            </xsl:when>
            <xsl:when test="exists($val/low) or exists($val/high)">
                <xsl:sequence select="xs:integer(round(avg(($val/low[1]/@value, $val/high[1]/@value))))"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:sequence select="()"></xsl:sequence>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:function>
    
    <xsl:function name="hra:avgPosValue">
        <!-- wraps hra:avgValue, ensuring result is positive, otherwise empty sequence is returned -->
        <xsl:param name="val"/>
        <xsl:variable name="avgVal" select="hra:avgValue($val)"/>
        <xsl:sequence select="if (number($avgVal) ge 0) then $avgVal else ()"/>
    </xsl:function>
    
    <!-- function to return a risk meaning given a code and codeSystem -->
    <!-- returns the empty sequence if not found -->
    <xsl:function name="hra:meaning">
        <xsl:param name="code"/>
        <xsl:param name="codeSystemIn"/>
        <xsl:variable name="codeSystem" select="if (starts-with(upper-case($codeSystemIn), 'SNOMED')) then 'SNOMED_CT' else $codeSystemIn"></xsl:variable>
        <xsl:sequence select="$riskMeanings/root/row[(code eq $code) and (codeSystem eq $codeSystem)][1]/meaning"/>
    </xsl:function>
    
    <!-- function which returns passed string as xs:integer if possible; otherwise returns 0 -->
    <xsl:function name="hra:asIntegerOrZero">
        <xsl:param name="value"/>
        <xsl:sequence select="if ($value castable as xs:integer) then xs:integer($value) else 0"/>
    </xsl:function>
    
    
    <!-- Variables -->
    <xsl:variable name="riskMeanings" select="doc(resolve-uri('riskMeanings.xml', $localBaseUri))"/> 


    <!-- Let clinObs be the node set of clinical observations for the patient -->
    <xsl:variable name="clinObs" select="/FamilyHistory/subject/patient/subjectOf2/clinicalObservation"/>
    
    <!-- patientAgeFromBirthTime is not normally used, as patient estimated age should be in HL7.  When no birthtime, return empty sequence -->
    <xsl:variable name="patientAgeFromBirthTime"
        select="if (exists(/FamilyHistory/subject/patient/patientPerson/birthTime/@value))
        then hra:compute-age-from-birthTime(/FamilyHistory/subject/patient/patientPerson/birthTime/@value)
        else ()">
    </xsl:variable>

    <!-- Begin Main template -->
    <xsl:template match="/">
        <!-- Get Gail specific patient values; other Gail data is derived from BayesMendel input matrix  -->
        <xsl:text>currentAge menarcheAge firstLiveBirthAge hadBiopsy numBiopsy hyperPlasia race LCIS&#xa;</xsl:text>
        <xsl:call-template name="getPatientAge"/>
        <xsl:call-template name="getAgeMenarche"/>
        <xsl:call-template name="getAgeAtFirstBirth"/>
        <xsl:call-template name="getNumBreastBiopsies"/>
        <xsl:call-template name="getAH"/>
        <xsl:call-template name="getRace"/>
        <xsl:call-template name="getLCIS"/>
    </xsl:template>

    <xsl:template name="getPatientAge">
        <!-- patient age is the first available of this sequence -->
        <xsl:variable name="ageTemp" select="
            (hra:avgPosValue(/FamilyHistory/subject[1]/patient[1]/subjectOf1[1]/livingEstimatedAge[1]/value[1]), $patientAgeFromBirthTime, 'UNKNOWN')[1]"/>
        <xsl:value-of select="xs:string($ageTemp)"/>
        <xsl:text> </xsl:text>
    </xsl:template>
    
    <xsl:template name="getAgeMenarche">
        <xsl:variable name="menarcheAge" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Age at Menarche'][1]/subject/dataEstimatedAge/value[1]">
        </xsl:variable>
        <xsl:variable name="ageTemp" select="(hra:avgPosValue($menarcheAge), 'UNKNOWN')[1]"/>
        <xsl:value-of select="xs:string($ageTemp)"/>
        <xsl:text> </xsl:text>
    </xsl:template>
    
    <xsl:template name="getAgeAtFirstBirth">
        <xsl:variable name="ageAtFirstBirthTemp" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Age at First Live Birth'][1]/subject/dataEstimatedAge/value[1]"/>
        <xsl:variable name="ageAtFirstBirth" select="(hra:avgPosValue($ageAtFirstBirthTemp), 'UNKNOWN')[1]"/>
        <xsl:value-of select="xs:string($ageAtFirstBirth)"/>
        <xsl:text> </xsl:text>
    </xsl:template>
    
    <xsl:template name="getNumBreastBiopsies">
        <xsl:variable name="numBreastBiopsiesClassicCount" select="($clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Number of breast biopsies'][not(code/qualifier/value/@codeSystemName)][1]/code/qualifier/value[1]/@code, '0')[1]"/>
        <xsl:variable name="numBreastBiopsiesSnomedCount" select="($clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Biopsy of breast'][not(code/qualifier/value/@codeSystemName)][1]/code/qualifier/value[1]/@code, '0')[1]"/>
        <xsl:variable name="numBreastBiopsiesSnomedCountEncoded" select="($clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Biopsy of breast'][1]/code/qualifier/value[starts-with(upper-case(@codeSystemName), 'SNOMED')][1]/@code, '0')[1]"/>
        <xsl:variable name="numBreastBiopsiesSnomedCountDecoded" select="if ($numBreastBiopsiesSnomedCountEncoded eq '0') then '0' else (hra:meaning($numBreastBiopsiesSnomedCountEncoded, 'SNOMED_CT'), '0')[1]"/>
        <xsl:variable name="numBreastBiopsies" select=
            "max((hra:asIntegerOrZero($numBreastBiopsiesClassicCount), hra:asIntegerOrZero($numBreastBiopsiesSnomedCount), hra:asIntegerOrZero($numBreastBiopsiesSnomedCountDecoded)))"/>
        <!-- Gail model doesn't take UNKNOWN as possible value for number of breast biopsies, so count of 0 is same as no information provided in FH -->
        <xsl:variable name="hadBreastBiopsies" select="
            if ($numBreastBiopsies castable as xs:integer)
                then if (xs:integer($numBreastBiopsies) gt 0) then '1' else '0'
                else '0'"></xsl:variable>
        <xsl:value-of select="xs:string($hadBreastBiopsies)"/>
        <xsl:text> </xsl:text>
        <xsl:value-of select="if ($hadBreastBiopsies eq '1') then xs:string($numBreastBiopsies) else '0'"/>
        <xsl:text> </xsl:text>
    </xsl:template>

    <xsl:template name="getAH">
        <xsl:variable name="atypia" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Atypia']"/>
        <xsl:value-of select="
            if (exists($atypia))
                then '1'
                else '0'"/>
        <xsl:text> </xsl:text>
    </xsl:template>
    
    <xsl:template name="getRace">
        <!-- possibly empty sequence list of raceCode elements -->
        <xsl:variable name="individual" select="/FamilyHistory/subject/patient/patientPerson"/>
        
        <xsl:variable name="black0" select="($individual/raceCode[@code eq $BLACK_RACE_HL7_CODE], $individual/ethnicGroupCode[@code eq $BLACK_RACE_HL7_CODE])"/>
        <xsl:variable name="hispanic0" select="($individual/raceCode[@code eq $HISPANIC_RACE_HL7_CODE], $individual/ethnicGroupCode[@code eq $HISPANIC_RACE_HL7_CODE])"/>
        
        <xsl:variable name="black1" select="$individual/raceCode[contains(lower-case(@displayName),'african')]"/>
        <xsl:variable name="black2" select="$individual/ethnicGroupCode[contains(lower-case(@displayName),'african')]"/>
        <xsl:variable name="hispanic1" select="$individual/raceCode[contains(lower-case(@displayName),'hispanic') and not(matches(lower-case(@displayName), 'not|non'))]"/>
        <xsl:variable name="hispanic2" select="$individual/ethnicGroupCode[contains(lower-case(@displayName),'hispanic') and not(matches(lower-case(@displayName), 'not|non'))]"/>
        
        <xsl:value-of select="
            if (exists(($black0, $black1, $black2))) then 'black'
            else if (exists(($hispanic0, $hispanic1, $hispanic2))) then 'hispanic'
            else 'white'"/>
        <xsl:text> </xsl:text>
    </xsl:template>
    
    <xsl:template name="getLCIS">
        <xsl:variable name="lcis" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'LCIS']">
        </xsl:variable>
        <xsl:value-of select="
            if (exists($lcis))
            then '1'
            else '0'"/>
    </xsl:template>
    
</xsl:stylesheet>
