<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:hra="http://www.hughesriskapps.com"
  xmlns:xd="http://www.oxygenxml.com/ns/doc/xsl">
  
  <xd:doc scope="stylesheet">
    <xd:desc>
      <xd:p><xd:b>Created on:</xd:b> February 1, 2012</xd:p>
      <xd:p><xd:b>Author:</xd:b> Phil Bosinoff, Deep Code Consulting</xd:p>
      <xd:p>Modified: May 8, 2012</xd:p>
      <xd:p>Removed computation of livingEstimatedAge from Birthtime; livingEstimatedAge must be present</xd:p>
      <xd:p>PHI: Remove birthtime, names, telcom, if present</xd:p>
      <xd:p>Modified: June 25, 2012</xd:p>
      <xd:p>Fixed HER2Neu to work if multiple ClinObs for same relative: lower-case(must not be sequence) </xd:p>
      <xd:p>Fixed ER,PR, HER2 to use Neg, if multiple CO and any are Neg (Neg trumps Pos, as is worse)</xd:p>
      <xd:p>Modified: February 5, 2013</xd:p>
      <xd:p>Added ethnicity and mastectomy columns for brcapro 2.0-8 </xd:p>
      <xd:p>Modified: Sept 28, 2013</xd:p>
      <xd:p>Cleaner functional approach to brca test results and test dates</xd:p>
      <xd:p>If not pos, and not neg - looks for valid neg result strings, is not tested (which may therefore include VUS and unknown)</xd:p>
      <xd:p>Modified: Sept 30, 2013</xd:p>
      <xd:p>brcaLocus set to last brca1 test and last brca2 test</xd:p>
      <xd:p>Modified: Aug. 28, 2014</xd:p>
      <xd:p>Added NA inputs for AgeBreast, AgeOvary, AgeBreastContralateral when appropriate for BayesMendel 2.1</xd:p>
      <xd:p>Modified: Sept. 4, 2014</xd:p>
      <xd:p>Added param to output origIds; used by wcfRiskService</xd:p>
      <xd:p>Modified: Sept. 17, 2014</xd:p>
      <xd:p>Changed AffectedColon to count of colon cancers for use by PREMM (doesn't break MMRpro)</xd:p>
      <xd:p>Added ECC values for PREMM.</xd:p>
      <xd:p>Modified: May 8, 2015</xd:p>
      <xd:p>When age of ooph is missing, use ageOvary (as before), but not if that is NA; BcraPro doesn't like that input even though it is allowed for ageOvary; instead send 1; brcapro will impute</xd:p>
      <xd:p>When age of mastectomy is missing, use ageBreast as a number; makes sense regardless of whether had BC since with BC, use that age, whereas w/o BC, use the "ageBreast" as well (using 1 not NA) since that is the age</xd:p>
      <xd:p>Modified: June 18, 2015</xd:p>
      <xd:p>If original Proband Id is '0' change to 'ZERO' everywhere needed (e.g. for proband's daughter's mother's id)</xd:p>
      <xd:p>In this case, 'ZERO' will show even when returning origIds (useOrigIds true) which is used when calling from Aggregator</xd:p>
      <xd:p>Modified: July 1, 2015</xd:p>
      <xd:p>gene test possible result of 'favor polymorphism' changed to 'favor_polymorphism' since original wasn't HL7 valid</xd:p>
      <xd:p>Modified: August 31, 2015</xd:p>
      <xd:p>Optionally add provided HL7 Relationship Code as last column in bayesmendel super matrix</xd:p>
      <xd:p>Modified: November 17, 2015</xd:p>
      <xd:p>Changed prior change to "addCustomColumns"; when 1, adds HL7 Relationship Code, AffectedDCIS, and AgeDCIS to BayesMendel Super Matrix</xd:p>
      <xd:p>Modified: November 20, 2015</xd:p>
      <xd:p>When "addCustomColumns" true, don't use UML 140 as BC, but as DCIS, for riskmeaning</xd:p>
    </xd:desc>
  </xd:doc>
  
  <xsl:output method="text"/>
  <xsl:strip-space elements="*"/>

  <!-- Following can be overidden by transformer invocation with parameter set -->
  <xsl:param name="localBaseUri" select="xs:string('file:/./App_Data/RAFiles/')"/>
  <!-- BayesMendel default is that DCIS is not considered cancer, but with this flag we can go either way -->
  <xsl:param name="dcisAsCancer" select="'0'"/>  
  <!-- Default is Risk Service need to use altered (sequential) ids, but wcfRiskService needs original ids bayesmendel super matrix -->
  <xsl:param name="useOrigIds" select="'0'"/>  
  <!-- Optionally add provided HL7 Relationship code, AffectedDCIS, and AgeDCIS as last columns in bayesmendel super matrix -->
  <xsl:param name="addCustomColumns" select="'0'"/>  
  
  <!-- Constants -->
  <xsl:variable name="FEMALE_SNOMED" select="'248152002'"/>
  <xsl:variable name="MALE_SNOMED" select="'248153007'"/>
  <xsl:variable name="ASHKENAZI_SNOMED" select="'81706006'"/>
  
  <xsl:variable name="geneCodesDoc" select="doc(resolve-uri('GeneCodes.xml', $localBaseUri))"/>
  <xsl:variable name="riskMeanings" select="doc(resolve-uri('riskMeanings.xml', $localBaseUri))"/> 
  
  <xsl:variable name="probandIsZero" select="//patientPerson[1]/id[1]/@extension eq '0'"/>
  
  <!-- After some fixup in "copy" mode, create the input document to be processed -->
  <xsl:variable name="in">
    <xsl:apply-templates mode="copy"></xsl:apply-templates>
  </xsl:variable>
 
  <!-- This is identity copy template; it performs a deep copy, recursively  -->
  <xsl:template match="@*|node()" mode="copy">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" mode="copy"/>
    </xsl:copy>
  </xsl:template>

  <!-- Remove Private Health Information, if present -->
  <!-- Remove birthTime; age element should be present, so don't need birthtime  -->
  <xsl:template match="birthTime" mode="copy">
  </xsl:template>
  
  <!-- remove all names -->
  <xsl:template match="name" mode="copy">
  </xsl:template>
  
  <!-- Remove phone numbers -->
  <xsl:template match="telecom" mode="copy">
  </xsl:template>

  <!-- provides an exception to the identity template above
    whereby the surgeon general misspelled 'extention' becomes 'extension'  -->
  <xsl:template match="@extention" mode="copy">
    <xsl:attribute name="extension" select=".">
    </xsl:attribute>
  </xsl:template>

  <!-- if the patientPerson has no id at all, such as with Surgeon General,
    we give her an id with extension=-1  -->
  <!-- Original proband id of 0 is stored and later returned as 'ZERO' to distinguish from other BrcaPro Matrix 0s-->
  <xsl:template match="patientPerson" mode="copy">
    <patientPerson>
      <xsl:apply-templates select="@*" mode="copy"/>
      <xsl:if test="not(exists(id))">
        <id extension="-1" />
      </xsl:if>
      <xsl:if test="exists(id) and $probandIsZero">
        <id extension="ZERO" />
      </xsl:if>
      <!-- copy over the rest of patientPerson's children  -->
      <xsl:apply-templates select="node()" mode="copy"/>
    </patientPerson>  
  </xsl:template>
  
  <!-- optionally alter parent ids which refer to the proband when proband is ZERO-->
  <xsl:template match="patientPerson[1]/relative/relationshipHolder/relative[code/@code = ('NMTH', 'NFTH')]/relationshipHolder/id" mode="copy">
    <id extension="{if ((./@extension eq '0') and $probandIsZero) then 'ZERO' else ./@extension}"/>
  </xsl:template>
  
  <!-- if any relative's gender is not expressly stated as Female or Male, remove that individual altogether -->
  <xsl:template match="patient/patientPerson/relative[not(exists(relationshipHolder/administrativeGenderCode))
                          or not (some $x in ('F', 'M', $FEMALE_SNOMED, $MALE_SNOMED)
                          satisfies relationshipHolder/administrativeGenderCode/@code eq $x)]" mode="copy">
    <xsl:message>Since Relative <xsl:value-of select="relationshipHolder/id/@extension"/> has unknown gender, this relative is not used in risk calculations</xsl:message>
  </xsl:template>
  
  <!-- if any relative's living age is > 105, reduce it to 105 and leave a message -->
  <xsl:template match="subjectOf1/livingEstimatedAge/value[xs:double(@value) gt 105]" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../(patientPerson|relationshipHolder)/id/@extension"/> has age <xsl:value-of select="@value"/> which exceeds 105; using age 105</xsl:message>
    <value value="105"/>
  </xsl:template>
  <xsl:template match="subjectOf1/livingEstimatedAge/value/low[xs:double(@value) gt 105]" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../../(patientPerson|relationshipHolder)/id/@extension"/> has low age <xsl:value-of select="@value"/> which exceeds 105; using age 105</xsl:message>
    <low value="105"/>
  </xsl:template>
  <xsl:template match="subjectOf1/livingEstimatedAge/value/high[xs:double(@value) gt 105]" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../../(patientPerson|relationshipHolder)/id/@extension"/> has high age <xsl:value-of select="@value"/> which exceeds 105; using age 105</xsl:message>
    <high value="105"/>
  </xsl:template>
  
  <!-- if any relative's deceased age is > 105, reduce it to 105 and leave a message -->
  <xsl:template match="subjectOf1/deceasedEstimatedAge/value[xs:double(@value) gt 105]" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../(patientPerson|relationshipHolder)/id/@extension"/> has age <xsl:value-of select="@value"/> which exceeds 105; using age 105</xsl:message>
    <value value="105"/>
  </xsl:template>
  <xsl:template match="subjectOf1/deceasedEstimatedAge/value/low[xs:double(@value) gt 105]" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../../(patientPerson|relationshipHolder)/id/@extension"/> has low age <xsl:value-of select="@value"/> which exceeds 105; using age 105</xsl:message>
    <low value="105"/>
  </xsl:template>
  <xsl:template match="subjectOf1/deceasedEstimatedAge/value/high[xs:double(@value) gt 105]" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../../(patientPerson|relationshipHolder)/id/@extension"/> has high age <xsl:value-of select="@value"/> which exceeds 105; using age 105</xsl:message>
    <high value="105"/>
  </xsl:template>
  
  
  <!-- if any relative's clinical observation age is > 105, reduce it to 105 and leave a message -->
  <xsl:template match="dataEstimatedAge/value[xs:double(@value) gt 105]" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../../../(patientPerson|relationshipHolder)/id/@extension"/> has a clinical observation with low age <xsl:value-of select="@value"/> which exceeds 105; using age 105</xsl:message>
    <value value="105"/>
  </xsl:template>
  
  <!-- if any relative's clinical observation low age is > 105, reduce it to 105 and leave a message -->
  <xsl:template match="dataEstimatedAge/value/low[xs:double(@value) gt 105]" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../../../../(patientPerson|relationshipHolder)/id/@extension"/> has a clinical observation with low age <xsl:value-of select="@value"/> which exceeds 105; using age 105</xsl:message>
    <low value="105"/>
  </xsl:template>
  
  <!-- if any relative's clinical observation high age is > 105, reduce it to 105 and leave a message -->
  <xsl:template match="dataEstimatedAge/value/high[xs:double(@value) gt 105]" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../../../../(patientPerson|relationshipHolder)/id/@extension"/> has a clinical observation with high age <xsl:value-of select="@value"/> which exceeds 105; using age 105</xsl:message>
    <high value="105"/>
  </xsl:template>

  <!-- if any relative's bayesmendel cancer clinical observation has an age of onset of 0, change it to unknown/unspecified and leave a message -->
  <xsl:template match="clinicalObservation[hra:meaning(code/@code, code/@codeSystemName) = (if ($dcisAsCancer ne '0') then 'DCIS' else (), 'Breast Cancer', 'Ovarian Cancer', 'Colon Cancer', 'Uterine Cancer', 'Pancreatic Cancer', 'Melanoma')][hra:avgAge(.) eq 0]/subject" mode="copy">
    <xsl:message>Relative <xsl:value-of select="../../../(patientPerson|relationshipHolder)/id/@extension"/> has cancer with age of onset of 0; used unknown/unspecified age of onset</xsl:message>
    <!-- nothing here; removes this subject node altogether, which removes the 0 age of onset-->
  </xsl:template>
  

  <!-- Modifies Hughes Risk Apps gender codeSystem -->
  <xsl:template match="administrativeGenderCode" mode="copy">
    <administrativeGenderCode>
      <xsl:choose>
        <xsl:when test="@code='F'">
          <xsl:attribute name="code" select="$FEMALE_SNOMED"/>
          <xsl:attribute name="codeSystemName" select="'SNOMED_CT'"/>
          <xsl:attribute name="displayName" select="'female'"/></xsl:when>
        <xsl:when test="@code='M'">
          <xsl:attribute name="code" select="'$MALE_SNOMED'"/>
          <xsl:attribute name="codeSystemName" select="'SNOMED_CT'"/>
          <xsl:attribute name="displayName" select="'male'"/></xsl:when>
        <xsl:otherwise>
          <xsl:apply-templates select="@*|*" mode="copy"/> <!-- copies attributes as they are -->
        </xsl:otherwise>
      </xsl:choose>
    </administrativeGenderCode>
  </xsl:template>
  
  <xsl:template match="patient" mode="copy">
    <patient>
      <!-- copy over all of patient's attributes and xml children -->
      <xsl:apply-templates select="@*|*" mode="copy"/>
      <xsl:if test="not(exists(subjectOf1/livingEstimatedAge))">
        <xsl:message>UNRECOVERABLE ERROR: There is no ESTIMATED AGE provided for the proband. END_OF_UNRECOVERABLE_ERROR</xsl:message>
      </xsl:if>
    </patient>
  </xsl:template>

  
  <!-- Functions -->
  <xsl:function name="hra:genderCode" as="xs:string">
    <xsl:param name="alphaGender"/>
    <xsl:sequence select="if ($alphaGender/@code eq $FEMALE_SNOMED) then '0' else '1' "/>
  </xsl:function>

  <xsl:function name="hra:ethnicity">
    <xsl:param name="individual"/>
    <!-- possibly empty sequence list of raceCode elements that are Ashkenazi -->
    <!-- we use the originalText attribute because there is no unique code for Ashkenazi in the HRA race table, and this is the only distinguishing feature -->
    <xsl:variable name="aj1" select="$individual/raceCode[contains(lower-case(@displayName),'ashkenazi')]"/>
    <xsl:variable name="aj2" select="$individual/ethnicGroupCode[contains(lower-case(@displayName),'ashkenazi')]"/>
    
    <!-- or Ashkenazi SNOMED present -->
    <xsl:variable name="ajSnomed1" select="$individual/raceCode[(@code eq $ASHKENAZI_SNOMED) and starts-with(lower-case(@codeSystemName), 'snomed')]"/>
    <xsl:variable name="ajSnomed2" select="$individual/ethnicGroupCode[(@code eq $ASHKENAZI_SNOMED) and starts-with(lower-case(@codeSystemName), 'snomed')]"/>
    
    <!-- Italian specified -->
    <xsl:variable name="italian" select="
      if (exists($individual/raceCode[contains(lower-case(@displayName),'italian')])
          or exists($individual/ethnicGroupCode[contains(lower-case(@displayName),'italian')]))
          then 'Italian'
          else ()"/>

    <!-- nonAJ specified -->
    <xsl:variable name="nonaj" select="
      if (exists($individual/raceCode[contains(lower-case(@displayName),'nonaj')])
          or exists($individual/raceCode[contains(lower-case(@displayName),'nonaj')])
          or exists($individual/ethnicGroupCode[contains(lower-case(@displayName),'nonashkenazi')])
          or exists($individual/ethnicGroupCode[contains(lower-case(@displayName),'nonashkenazi')]))
          then 'nonAJ'
          else ()"/>

    <xsl:sequence select="
      if (exists(($aj1, $aj2, $ajSnomed1, $ajSnomed2)))
        then 'AJ'
      else if (exists($italian)) then $italian
      else if (exists($nonaj)) then $nonaj
      else ()"/>
    <!-- BrcaPro actually accepts these race types: "AJ", "nonAJ", "Italian", or "other"; "other" is not supported by the web service -->
  </xsl:function>

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

  <xsl:function name="hra:isBRCA1" as="xs:boolean">
    <!-- Lookup in GeneCodes to see if this is BRCA1  -->
    <xsl:param name="code"/>
    <xsl:param name="system"/>
    <xsl:sequence select="xs:boolean(exists($geneCodesDoc//Gene[(code = $code) and (codeSystemName = $system)][contains(lower-case(displayName),'brca1')]))"/>
  </xsl:function>

  <xsl:function name="hra:isBRCA2" as="xs:boolean">
    <!-- Lookup in GeneCodes to see if this is BRCA2  -->
    <xsl:param name="code"/>
    <xsl:param name="system"/>
    <xsl:sequence select="xs:boolean(exists($geneCodesDoc//Gene[(code = $code) and (codeSystemName = $system)][contains(lower-case(displayName),'brca2')]))"/>
  </xsl:function>

  <xsl:function name="hra:isMLH1" as="xs:boolean">
    <!-- Lookup in GeneCodes to see if this is MLH1  -->
    <xsl:param name="code"/>
    <xsl:param name="system"/>
    <xsl:sequence select="xs:boolean(exists($geneCodesDoc//Gene[(code = $code) and (codeSystemName = $system)][contains(lower-case(displayName),'mlh1')]))"/>
  </xsl:function>
  
  <xsl:function name="hra:isMSH2" as="xs:boolean">
    <!-- Lookup in GeneCodes to see if this is MSH2  -->
    <xsl:param name="code"/>
    <xsl:param name="system"/>
    <xsl:sequence select="xs:boolean(exists($geneCodesDoc//Gene[(code = $code) and (codeSystemName = $system)][contains(lower-case(displayName),'msh2')]))"/>
  </xsl:function>

  <xsl:function name="hra:isMSH6" as="xs:boolean">
    <!-- Lookup in GeneCodes to see if this is MSH6  -->
    <xsl:param name="code"/>
    <xsl:param name="system"/>
    <xsl:sequence select="xs:boolean(exists($geneCodesDoc//Gene[(code = $code) and (codeSystemName = $system)][contains(lower-case(displayName),'msh6')]))"/>
  </xsl:function>
  
  <xsl:function name="hra:isP16" as="xs:boolean">
    <!-- Lookup in GeneCodes to see if this is P16  -->
    <xsl:param name="code"/>
    <xsl:param name="system"/>
    <xsl:sequence select="xs:boolean(exists($geneCodesDoc//Gene[(code = $code) and (codeSystemName = $system)][contains(lower-case(displayName),'p16')]))"/>
  </xsl:function>
  
  <xsl:function name="hra:isDeleterious" as="xs:boolean">
    <!-- is the string considered deleterious -->
    <xsl:param name="codeString"/>
    <xsl:sequence select="xs:boolean(contains(lower-case($codeString),'deleterious') or contains(lower-case($codeString),'pathogenic') or contains(lower-case($codeString),'pos'))"/>
  </xsl:function>
  
  <xsl:function name="hra:isNegative" as="xs:boolean">
    <!-- is the string considered Negative -->
    <xsl:param name="codeString"/>
    <xsl:sequence select="xs:boolean(contains(lower-case($codeString),'neg') or contains(lower-case($codeString),'benign') or contains(lower-case($codeString),'favor_polymorphism'))"/>
  </xsl:function>

  <xsl:function name="hra:brcaMatrixGenTestCode" as="xs:string">
    <!-- 1 for deleterious, 2 for neg, 0 for not tested -->
    <xsl:param name="brcaTestResult"/>
    <xsl:sequence select="
      if (hra:isDeleterious($brcaTestResult)) then '1'
      else if (hra:isNegative($brcaTestResult)) then '2'
      else '0'"/>
  </xsl:function>

  <xsl:function name="hra:brcaLocusCodedResult" as="xs:string">
    <!-- 1 for deleterious, 2 for neg, 0 for not tested -->
    <!-- input is a brcaLocus section of the XML -->
    <xsl:param name="brcaLocus"/>
    <xsl:sequence select="
        hra:brcaMatrixGenTestCode($brcaLocus/component3/sequenceVariation/interpretationCode/@code)
        "/>
  </xsl:function>

  <xsl:function name="hra:brcaLocusDate" as="xs:string">
    <!-- returns the effective Date of the panel test, or today's date when not otherwise available -->
    <!-- input is a brcaLocus section of the XML -->
    <xsl:param name="brcaLocus"/>
    <xsl:sequence select="
      ($brcaLocus/effectiveTime/@value , format-date(current-date(),'[Y][M,2][D,2]'))[1]
      "/>
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
  
  <xsl:function name="hra:avgAge">
    <!--expects a single clinical observation, returns the average age, or empty if no ages provided -->
    <xsl:param name="clinObs"/>
    <xsl:sequence select="hra:avgPosValue($clinObs/subject[1]/dataEstimatedAge[1]/value[1])"/>
  </xsl:function>

  <!-- function to return a risk meaning given a code and codeSystem -->
  <!-- returns the empty sequence if not found -->
  <!-- modified output to return multiple meanings, if present -->
  <xsl:function name="hra:meaning">
    <xsl:param name="code"/>
    <xsl:param name="codeSystemIn"/>
    <xsl:variable name="codeSystem" select="if (starts-with(upper-case($codeSystemIn), 'SNOMED')) then 'SNOMED_CT' else $codeSystemIn"></xsl:variable>
    <!-- In the special case of optionally outputing extra columns, which therefore include DCIS columns, we always classify DCIS as such, and BC as BC -->
    <!-- This won't affect BrcaPro input matrix because extra columns are not used for that; in that case we honor the UML 140 if present, which classifies DCIS as BC -->
    <xsl:choose >
      <xsl:when test="($addCustomColumns ne '0') and ($code eq '140') and ($codeSystem = 'UML')">
        <xsl:sequence select="'DCIS'"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:sequence select="$riskMeanings/root/row[(code eq $code) and (codeSystem eq $codeSystem)]/meaning"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:function>
  
  <!-- Variables -->
  
  <!-- Create idMapping "hashMap"; patient is id 1, other relatives' ids are sequential in document order -->
  <xsl:variable name="idMapping">
    <xsl:element name="relID">
      <xsl:attribute name="orig">
        <xsl:value-of select="$in//patientPerson[1]/id[1]/@extension"/>
      </xsl:attribute>
      <xsl:value-of select="1"></xsl:value-of>
    </xsl:element>    
    <xsl:for-each select="distinct-values($in//patientPerson/relative/relationshipHolder/id/@extension)">
      <xsl:element name="relID">
        <xsl:attribute name="orig">
          <xsl:value-of select="."/>
        </xsl:attribute>
          <xsl:value-of select="position()+1"></xsl:value-of>
      </xsl:element>    
    </xsl:for-each>
  </xsl:variable>
  
  <xsl:key name="idMap" match="relID" use="@orig"></xsl:key>
  <xsl:key name="relHolder" match="/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative/relationshipHolder" use="id/@extension"></xsl:key>
  <xsl:key name="relative" match="/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative" use="relationshipHolder/id/@extension"></xsl:key>
  
  <xsl:variable name="probandID" select="$in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/id[1]/@extension"></xsl:variable>
  
  <xsl:variable name="probandEthnicity" select="(hra:ethnicity($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]), 'nonAJ')[1]"/>
  
  
  <!-- Begin Main template -->
  <xsl:template match="/">
    <xsl:if test="$addCustomColumns eq '0'">
      <xsl:text>ID Gender FatherID MotherID AffectedBreast AffectedOvary AgeBreast AgeOvary AgeBreastContralateral Twins ethnic Oophorectomy AgeOophorectomy Mastectomy AgeMastectomy ER CK14 CK5.6 PR HER2 BRCA1 BRCA2 TestDate AffectedColon AffectedEndometrium AgeColon AgeEndometrium MSI location MLH1 MSH2 MSH6 MMRTestDate ECC AffectedPancreas AgePancreas AffectedSkin AgeSkin P16 P16TestDate&#xa;</xsl:text>
    </xsl:if>
    <xsl:if test="$addCustomColumns ne '0'">
      <xsl:text>ID Gender FatherID MotherID AffectedBreast AffectedOvary AgeBreast AgeOvary AgeBreastContralateral Twins ethnic Oophorectomy AgeOophorectomy Mastectomy AgeMastectomy ER CK14 CK5.6 PR HER2 BRCA1 BRCA2 TestDate AffectedColon AffectedEndometrium AgeColon AgeEndometrium MSI location MLH1 MSH2 MSH6 MMRTestDate ECC AffectedPancreas AgePancreas AffectedSkin AgeSkin P16 P16TestDate ProvidedHL7RelCode AffectedDCIS AgeDCIS&#xa;</xsl:text>
    </xsl:if>
    <xsl:for-each select="$idMapping/relID">
      <xsl:value-of select="if ($useOrigIds eq '0') then . else ./@orig" /><xsl:text> </xsl:text>
      <xsl:call-template name="getGender"/>
      <xsl:call-template name="getParents"/>
      <xsl:call-template name="getCancersAndAgesAndOophsAndERPRs"/>
      <xsl:call-template name="getGermline"/>

      <!-- begin MMR specific -->
      <xsl:call-template name="getColonCancersAndAgesAndMarkers"/>
      <xsl:call-template name="getMMRGermline"/>
      <xsl:call-template name="getECC"/>
      
      <!-- begin PancPRO specific -->
      <xsl:call-template name="getPancreaticCancersAndAges"/>

      <!-- begin MelaPRO specific -->
      <xsl:call-template name="getSkinCancersAndAges"/>
      <xsl:call-template name="getSkinGermline"/>
      
      <!-- begin optional HL7 Relationship Code and DCIS Info -->
      <xsl:if test="$addCustomColumns ne '0'">
        <xsl:call-template name="getProvidedHL7RelCode"/>
        <xsl:call-template name="getDCISInfo"/>
      </xsl:if>
      
      <xsl:text>&#xa;</xsl:text>
    </xsl:for-each>
    <!-- <xsl:call-template name="debug"/> -->
  </xsl:template>

  <xsl:template name="getGender">
    <xsl:param name="origID" select="./@orig"/>
    <xsl:value-of select="if ($origID=$probandID)
      then hra:genderCode($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/administrativeGenderCode)
      else hra:genderCode(key('relHolder',$origID,$in)/administrativeGenderCode)"></xsl:value-of>
    <xsl:text> </xsl:text>
  </xsl:template> 

  <xsl:template name="getParents">
    <xsl:param name="origID" select="./@orig"/>
    <xsl:variable name="relHolder" select="if ($origID=$probandID) then $in//patientPerson else $in//patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/..[1]"></xsl:variable>
    <xsl:variable name="fatherOrigID" select="$relHolder/relative/code[@code='NFTH']/../relationshipHolder/id/@extension"></xsl:variable>
    <xsl:variable name="motherOrigID" select="$relHolder/relative/code[@code='NMTH']/../relationshipHolder/id/@extension"></xsl:variable>
    <!-- Find Fathers, if any, for this person   -->
    <xsl:value-of select="if (exists($fatherOrigID)) then (if ($useOrigIds eq '0') then $idMapping/relID[@orig=$fatherOrigID] else $fatherOrigID,'0')[1] else '0'"/>
    <xsl:text> </xsl:text>
    
    <!-- msg about father id when not in pedigree -->
    <xsl:if test="exists($fatherOrigID) and not(exists($idMapping/relID[@orig=$fatherOrigID]))">
      <xsl:message>Relative <xsl:value-of select="$origID"/> father id <xsl:value-of select="$fatherOrigID"/> not in pedigree; used 0</xsl:message>
    </xsl:if>

    <!-- Find Mother, if any, for this person   -->
    <xsl:value-of select="if (exists($motherOrigID)) then (if ($useOrigIds eq '0') then $idMapping/relID[@orig=$motherOrigID] else $motherOrigID,'0')[1] else '0'"/>
    <xsl:text> </xsl:text>
    
    <!-- msg about mother id when not in pedigree -->
    <xsl:if test="exists($motherOrigID) and not(exists($idMapping/relID[@orig=$motherOrigID]))">
      <xsl:message>Relative <xsl:value-of select="$origID"/> mother id <xsl:value-of select="$motherOrigID"/> not in pedigree; used 0</xsl:message>
    </xsl:if>
    
  </xsl:template> 

  <xsl:template name="getCancersAndAgesAndOophsAndERPRs">
    <xsl:param name="origID" as="xs:string" select="./@orig"/>
    
    <!-- Get AJness of individual -->
    <xsl:variable name="localIndividual" select="if ($origID=$probandID) then $in//patientPerson else $in//patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/..[1]"/>
    <!-- If the individual's own ethnicity is not provided, use the proband's -->
    <xsl:variable name="ethnic" select="(hra:ethnicity($localIndividual), $probandEthnicity)[1]"/>

    <!-- Let clinObs be the node set of clinical observations for the current person; there could be more than one -->
    <xsl:variable name="clinObs" select="if ($origID=$probandID) then ($in/FamilyHistory/subject/patient/subjectOf2/clinicalObservation)
      else ($in//*/relative/relationshipHolder/id[@extension=$origID]/../../*/clinicalObservation)">
    </xsl:variable>
    
    <!-- Find the maximum observed age of onset of all diseases for this person; useful because person's age, if otherwise unknown, must be greater or equal to maxClinObsAge -->
    <xsl:variable name="maxClinObsAge" select="max((hra:avgPosValue($clinObs/subject/dataEstimatedAge/value)))">
    </xsl:variable>

    <!-- Get the clinical observations that are breast cancer - more than one if bilateral -->
    <!-- includes all diseases containing 'breast cancer' string and not containing 'dcis' string  -->
    <!-- Modified to consider UML 140 as invasive breast cancer.  This code, produced by riskApps, means it is DCIS, but to be considered cancer  -->
    <!-- If dcisAsCancer not '0', then consider anything with meaning of Breast Cancer or DCIS as cancer -->
    <xsl:variable name="clinObsBreastAll" select="
      if ($dcisAsCancer eq '0')
        then $clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Breast Cancer']
        else $clinObs[hra:meaning(code/@code, code/@codeSystemName) = ('Breast Cancer', 'DCIS')]"/>
    
    <!-- Sort the clin observations with BC for this individual in age ascending order, with missing age clin observations at end -->
    <xsl:variable name="clinObsBreastAllSorted" as="element(clinicalObservation)*">
      <xsl:perform-sort select="$clinObsBreastAll">
        <xsl:sort select="xs:double((hra:avgAge(.), 99999)[1])"/>
      </xsl:perform-sort>
    </xsl:variable>

    <xsl:variable name="livingEstimatedAge" select="
      if ($origID=$probandID)
      then hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/subjectOf1[1]/livingEstimatedAge[1]/value[1])
      else hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/../../subjectOf1[1]/livingEstimatedAge[1]/value[1])">
    </xsl:variable>
    <xsl:variable name="deceasedEstimatedAge" select="
      if ($origID=$probandID)
      then hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/subjectOf1[1]/deceasedEstimatedAge[1]/value[1])
      else hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/../../subjectOf1[1]/deceasedEstimatedAge[1]/value[1])">
    </xsl:variable>
    
    <!-- Get clinical observations that are ovarian cancer (could be 0, 1, or more nodes in this sequence) -->
    <xsl:variable name="clinObsOvarian" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Ovarian Cancer']"/>
    
    <xsl:variable name="breastCancer" select="
      if (empty($clinObsBreastAll))
        then '0'
        else xs:string(min((count($clinObsBreastAll/code), 2)))"/>
    <xsl:variable name="ovarianCancer" select="
      if (empty($clinObsOvarian))
        then '0'
        else '1'"/>
    
    <!-- Get first two breast cancers in age order; we will assume earlier age one is breast cancer, other is contralateral -->
    <xsl:variable name="clinObsBreastFirst" select="$clinObsBreastAllSorted[1]"/>
    <xsl:variable name="clinObsBreastSecond" select="$clinObsBreastAllSorted[2]"/>  <!-- other side, if any -->

    <!-- Get ages of breast cancers, if known -->
    <xsl:variable name="clinObsBreastFirstAge" select="hra:avgPosValue($clinObsBreastFirst/subject/dataEstimatedAge[1]/value[1])"/>
    <xsl:variable name="clinObsBreastSecondAge" select="hra:avgPosValue($clinObsBreastSecond/subject/dataEstimatedAge[1]/value[1])"/>
    
    <!-- For cancer, get the dataEstimatedAge average of low and high values for the appropriate clinicalObservation  -->
    <!--   However, if age of onset unknown, use NA since version 2.1+, instead of older min of livingAge, deceasedAge, or 50 -->
    <!-- For no cancer, get first non-empty value in sequence of livingEstimatedAge, deceasedEstimatedAge, maxClinObsAge, and 1 (or NA since v2.1+) -->
    <xsl:variable name="ageBreast" select="
      if ($breastCancer eq '0')
        then ($livingEstimatedAge, $deceasedEstimatedAge, $maxClinObsAge, 'NA')[1]
      else if ($breastCancer eq '1')
        then ($clinObsBreastFirstAge, 'NA')[1]
      else (: bilat breast cancer :) (
        if (exists($clinObsBreastFirstAge))
          then xs:double($clinObsBreastFirstAge)
          else 'NA'
        )"/>      

    <!-- For Contralateral breast cancer, use the age of onset for the second breast; if not available, for first breast  -->
    <!--   which could be 'NA' -->
    <xsl:variable name="ageBreastContralateral" select="
      if ($breastCancer ne '2')
        then '0'
        else (: bilat breast cancer :) (
          if (exists($clinObsBreastSecondAge))
            then xs:double($clinObsBreastSecondAge)
            else $ageBreast
        )"/>      

    <!-- If the relative has one or more ovarian cancers, use the minimum age of all ovarian cancers provided; if none available, use NA (since v2.1+) --> 
    <!-- If the relative has no ovarian cancers, use the first available of livingAge, deceasedAge, maxClinObsAge, NA (since v2.1+) --> 
    <xsl:variable name="ageOvary" select="
      if ($ovarianCancer ne '0')
        then (min((for $i in $clinObsOvarian return hra:avgAge($i))), 'NA')[1]
        else ($livingEstimatedAge, $deceasedEstimatedAge, $maxClinObsAge, 'NA')[1]"/>
    
    <!-- Get twin clinical observations, if any -->
    <xsl:variable name="clinObsTwins" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Identical twin'][1]">
    </xsl:variable>
    
    <!-- Get twinId -->
    <xsl:variable name="twinId" select="
      ($clinObsTwins/code/qualifier/value/@code, 0)[1]">
    </xsl:variable>
    
    <!-- Get the clinical observations that are oophorectomy, if any -->
    <xsl:variable name="clinObsOoph" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Oophorectomy']"/>
    <xsl:variable name="ooph" select="if (empty($clinObsOoph)) then '0' else '1'"/>
    
    <!-- For Oophorectomy, get the dataEstimatedAge average of low and high values for the appropriate clinicalObservation  -->
    <!--   However, if age of onset unknown, use ageOvary numeric value (since v2.1+) -->
    <!-- For no Oophorectomy, get first non-empty value in sequence of livingEstimatedAge, deceasedEstimatedAge, maxClinObsAge, and 1 -->
    <xsl:variable name="ageOophProvided" select="if ($ooph ne '0') then hra:avgAge($clinObsOoph[1]) else ()"/>
    <xsl:variable name="ageOvaryNumeric" select="if (string(number($ageOvary)) != 'NaN') then $ageOvary else '1'"/>
    <xsl:variable name="ageOoph" select="
      if ($ooph ne '0')
      then if (exists ($ageOophProvided))
            then min(($ageOophProvided,$ageOvaryNumeric))
            else $ageOvaryNumeric
      else ($livingEstimatedAge, $deceasedEstimatedAge, $maxClinObsAge, '1')[1]"/>
    
    <!-- Get all the clinical observations that are (uni) mastectomy, if any -->
    <xsl:variable name="clinObsMastAll" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Mastectomy']"/>
    
    <!-- Sort the clin observations with Mastectomy for this individual in age ascending order, with missing age clin observations at end -->
    <xsl:variable name="clinObsMastAllSorted" as="element(clinicalObservation)*">
      <xsl:perform-sort select="$clinObsMastAll">
        <xsl:sort select="xs:double((hra:avgAge(.), 99999)[1])"/>
      </xsl:perform-sort>
    </xsl:variable>

    <!-- Get first mastectomy in age order, if any -->
    <xsl:variable name="clinObsMastFirst" select="$clinObsMastAllSorted[1]"/>

    <!-- Get age of first mastectomy, if known -->
    <xsl:variable name="clinObsMastFirstAge" select="hra:avgPosValue($clinObsMastFirst/subject/dataEstimatedAge[1]/value[1])"/>

    <!-- Get second mastectomy in age order, if any -->
    <xsl:variable name="clinObsMastSecond" select="$clinObsMastAllSorted[2]"/>

    <!-- Get age of second mastectomy, if known -->
    <xsl:variable name="clinObsMastSecondAge" select="hra:avgPosValue($clinObsMastSecond/subject/dataEstimatedAge[1]/value[1])"/>

    <!-- Get all the clinical observations that are bilateral mastectomy, if any -->
    <xsl:variable name="clinObsBilatMast" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Bilateral Mastectomy']"/>

    <!-- Get provided age of (first) bilateral mastectomy, if any -->
    <xsl:variable name="clinObsBilatMastAge" select="hra:avgAge($clinObsBilatMast[1])"/>
    
    <xsl:variable name="bilatMast" select="if (exists($clinObsBilatMast) or exists($clinObsMastSecond)) then '1' else '0'"/>

    <xsl:variable name="ageBilatMastProvided" select="
      if (exists($clinObsBilatMastAge)) then $clinObsBilatMastAge
      else if (exists($clinObsMastSecondAge)) then $clinObsMastSecondAge
      else if (exists($clinObsMastSecond) and exists($clinObsMastFirstAge)) then $clinObsMastFirstAge  (: use age of first Mast, when known there were two, but don't have age for second one :)
      else ()"/>

    <!-- For BilatMastectomy, get the dataEstimatedAge average of low and high values for the appropriate clinicalObservation  -->
    <!--   However, if age of onset unknown, use breast cancer age as a number -->
    <!-- For no BilatMastectomy, get first non-empty value in sequence of livingEstimatedAge, deceasedEstimatedAge, maxClinObsAge, and 1 -->
    <xsl:variable name="ageBreastNumeric" select="if (string(number($ageBreast)) != 'NaN') then $ageBreast else '1'"/>
    <xsl:variable name="ageBilatMast" select="
      if ($bilatMast eq '1')
      then (($ageBilatMastProvided, $ageBreastNumeric)[1])
      else ($livingEstimatedAge, $deceasedEstimatedAge, $maxClinObsAge, '1')[1]"/>

    <!-- Get ER/PR status, if breast cancer  -->
    <xsl:variable name="ERStatus" select="
      (: Get (all) clinical observations that have an ERstatus;
      future: BayesMendel algorithm needs to change to allow different ERstatus for each disease :)
      if ($breastCancer ne '0')
        then $clinObs/code[@codeSystemName='ERStatus']
      else ()  (: the empty sequence :)
      "></xsl:variable>

    <xsl:variable name="ERStatusPositiveSnomedEncoded" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Estrogen receptor positive tumor']"/>
    <xsl:variable name="ERStatusNegativeSnomedEncoded" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Estrogen receptor negative neoplasm']"/>
    
    <xsl:variable name="PRStatus" select="
      (: Get (all) clinical observations that have a PRstatus;
      future: BayesMendel algorithm needs to change to allow different PRstatus for each disease :)
      if ($breastCancer ne '0')
        then $clinObs/code[@codeSystemName='PRStatus']
      else ()  (: the empty sequence :)
      "></xsl:variable>

    <xsl:variable name="PRStatusPositiveSnomedEncoded" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Progesterone receptor positive tumor']"/>
    <xsl:variable name="PRStatusNegativeSnomedEncoded" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'Progesterone receptor negative neoplasm']"/>

    <xsl:variable name="ERPR"  select="
      (: if any of the ERStatus' are negative, gets 2 (conservative, as this is worse), if any positive or faint, then gets 1, otherwise 0 :)
      (: same for PRStatus :)
      if ($breastCancer ne '0')
         then concat(
         if (exists($ERStatus[contains(lower-case(@code),'negative')]) or exists($ERStatusNegativeSnomedEncoded)) then '2'
         else if (exists($ERStatus[contains(lower-case(@code),'positive') or contains(lower-case(@code),'faint')]) or exists($ERStatusPositiveSnomedEncoded)) then '1'
            else '0',
            ' 0 0 ',
            if (exists($PRStatus[contains(lower-case(@code),'negative')]) or exists($PRStatusNegativeSnomedEncoded)) then '2'
            else if (exists($PRStatus[contains(lower-case(@code),'positive') or contains(lower-case(@code),'faint')]) or exists($PRStatusPositiveSnomedEncoded)) then '1'
            else '0')
      else '0 0 0 0'
      "></xsl:variable>
    
    <!-- HER2NeuFISH from SNOMED codes -->
    <xsl:variable name="HER2NeuFISHSnomedEncoded" select="($clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'HER2NeuFISH'][1]/code/qualifier/value[starts-with(upper-case(@codeSystemName), 'SNOMED')][1]/@code, '0')[1]"/>
    <xsl:variable name="HER2NeuFISHSnomedDecoded" select="if ($HER2NeuFISHSnomedEncoded eq '0') then '0' else (hra:meaning($HER2NeuFISHSnomedEncoded, 'SNOMED_CT'), '0')[1]"/>
    <xsl:variable name="HER2NeuIHCSnomedEncoded" select="($clinObs[hra:meaning(code/@code, code/@codeSystemName) eq 'HER2NeuIHC'][1]/code/qualifier/value[starts-with(upper-case(@codeSystemName), 'SNOMED')][1]/@code, '0')[1]"/>
    <xsl:variable name="HER2NeuIHCSnomedDecoded" select="if ($HER2NeuIHCSnomedEncoded eq '0') then '0' else (hra:meaning($HER2NeuIHCSnomedEncoded, 'SNOMED_CT'), '0')[1]"/>
    
    <!-- For HER2, (similar to ER/PR logic) if multiple COs, negative trumps positive, as it is worse -->
    <xsl:variable name="HER2NeuFISHClassic" select="
      if (exists($clinObs/code[@codeSystemName='HER2NeuFISH']))
        then if ($clinObs/code[@codeSystemName='HER2NeuFISH']/lower-case(@code) = ('not amplified', 'not_amplified'))
            then '2'
        else (if ($clinObs/code[@codeSystemName='HER2NeuFISH']/lower-case(@code) = ('amplified'))
            then '1' 
            else '0')
      else '0'">
    </xsl:variable>
    
    <xsl:variable name="HER2NeuFISH" select="
      if (($HER2NeuFISHClassic eq '2') or ($HER2NeuFISHSnomedDecoded eq 'Negative'))
        then '2'
        else (if (($HER2NeuFISHClassic eq '1') or ($HER2NeuFISHSnomedDecoded eq 'Positive'))
              then '1'
              else '0')">
    </xsl:variable>

    <!-- HER2NeuFISH trumps HER2NeuIHC when amplified or not amplified, otherwise use IHC -->
    <xsl:variable name="HER2" select="
      if ($HER2NeuFISH ne '0')
        then $HER2NeuFISH
        else if (($clinObs/code[@codeSystemName='HER2NeuIHC']/lower-case(@code) = ('0', '1+', '2+', 'negative')) or ($HER2NeuIHCSnomedDecoded eq 'Negative'))
                then '2'
                else
                if (($clinObs/code[@codeSystemName='HER2NeuIHC']/lower-case(@code) = ('3+', '4+', 'positive')) or ($HER2NeuIHCSnomedDecoded eq 'Positive'))
                    then '1'
                    else '0'">
    </xsl:variable>
    
    <xsl:value-of select="$breastCancer"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ovarianCancer"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ageBreast"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ageOvary"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ageBreastContralateral"/><xsl:text> </xsl:text>
    <xsl:value-of select="$twinId"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ethnic"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ooph"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ageOoph"/><xsl:text> </xsl:text>
    <xsl:value-of select="$bilatMast"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ageBilatMast"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ERPR"/><xsl:text> </xsl:text>
    <xsl:value-of select="$HER2"/><xsl:text> </xsl:text>
    
    <!-- message(s) when an age has been estimated -->
    <!-- no longer needed as any relevant messages or imputations done by BayesMendel itself -->
    
    <!-- msg about relative age (only need one of these since applies across all models) -->
<!--    <xsl:if test="not(exists(($livingEstimatedAge, $deceasedEstimatedAge)[1]))">
      <xsl:message>Relative <xsl:value-of select="$origID"/> age unknown; used <xsl:value-of select="($livingEstimatedAge, $deceasedEstimatedAge, $maxClinObsAge, '1')[1]"/></xsl:message>
    </xsl:if>

    <!-\- msg about breast cancer age of onset -\->
    <xsl:if test="$breastCancer eq '1'">
      <xsl:if test="not(exists($clinObsBreastFirstAge))">
        <xsl:message>Relative <xsl:value-of select="$origID"/> breast cancer age of onset unknown; used <xsl:value-of select="$ageBreast"/></xsl:message>
      </xsl:if>
    </xsl:if>
    <xsl:if test="$breastCancer eq '2'">
      <xsl:if test="not(exists($clinObsBreastFirstAge)) or not(exists($clinObsBreastSecondAge))">
        <xsl:message>Relative <xsl:value-of select="$origID"/> contralateral breast cancer age of onset unknown; used <xsl:value-of select="$ageBreastContralateral"/></xsl:message>
      </xsl:if>
    </xsl:if>
    <!-\- msg about ovarian cancer age of onset -\->
    <xsl:if test="$ovarianCancer ne '0'">
      <xsl:if test="not(exists($clinObsOvarian/subject/dataEstimatedAge/value))">
        <xsl:message>Relative <xsl:value-of select="$origID"/> ovarian cancer age of onset unknown; used <xsl:value-of select="$ageOvary"/></xsl:message>
      </xsl:if>
    </xsl:if>
-->    <!-- msg about oophorectomy age -->
    <xsl:if test="$ooph ne '0'">
      <xsl:if test="not(exists($clinObsOoph/subject/dataEstimatedAge/value))">
        <xsl:message>Relative <xsl:value-of select="$origID"/> oophorectomy age unknown; used imputed value</xsl:message>
      </xsl:if>
      <xsl:if test="exists($ageOophProvided) and ($ageOophProvided > $ageOvaryNumeric)">
        <xsl:message>Relative <xsl:value-of select="$origID"/> provided oophorectomy age of <xsl:value-of select="$ageOophProvided"/> has been adjusted to same value as Ovarian Cancer age of <xsl:value-of select="$ageOvary"/> due to BRCAPRO only handling oophorectomy occurring before ovarian cancer, not after</xsl:message>
      </xsl:if>
    </xsl:if>
    <!-- msg about bilateral mastectomy age -->
    <xsl:if test="$bilatMast ne '0'">
      <xsl:if test="not(exists($clinObsBilatMastAge)) and not(exists($clinObsMastSecondAge))">
        <xsl:message>Relative <xsl:value-of select="$origID"/> bilateral mastectomy age unknown; used <xsl:value-of select="$ageBilatMast"/></xsl:message>
      </xsl:if>
    </xsl:if>

  </xsl:template> 

  <xsl:template name="getGermline">
    <xsl:param name="origID" select="./@orig"/>
    <!-- Possibly empty sequence of geneticLocus nodes for BRCA1 genetic test results  -->
    <xsl:variable name="brca1Locus" select="
      if ($origID=$probandID)
        then ($in/FamilyHistory/subject/patient/subjectOf2/geneticLocus[hra:isBRCA1(value/@code, value/@codeSystemName)])[last()]
        else ($in/FamilyHistory/subject/patient/patientPerson/relative[relationshipHolder/id/@extension = $origID]/subjectOf2/geneticLocus[hra:isBRCA1(value/@code, value/@codeSystemName)])[last()]">
    </xsl:variable>
    <!-- Possibly empty sequence of geneticLocus nodes for BRCA2 genetic test results  -->
    <xsl:variable name="brca2Locus" select="
      if ($origID=$probandID)
      then ($in/FamilyHistory/subject/patient/subjectOf2/geneticLocus[hra:isBRCA2(value/@code, value/@codeSystemName)])[last()]
      else ($in/FamilyHistory/subject/patient/patientPerson/relative[relationshipHolder/id/@extension = $origID]/subjectOf2/geneticLocus[hra:isBRCA2(value/@code, value/@codeSystemName)])[last()]">
    </xsl:variable>
    
    <xsl:variable name="brca1Test" select="
        hra:brcaLocusCodedResult($brca1Locus)
        "/>
    <xsl:value-of select="$brca1Test"/>
    <xsl:text> </xsl:text>
   
    <xsl:variable name="brca2Test" select="
        hra:brcaLocusCodedResult($brca2Locus)
        "/>
    <xsl:value-of select="$brca2Test"/>
    <xsl:text> </xsl:text>
    
    <!-- insert proxy for Test Order; using this date info, will fix to true test order in R later.
         Use earliest date of positive BRCA1 and positive BRCA2 tests
         or date of the positive BRCA1 or BRCA2 test if only one of them is positive
         or earliest date of non-positive BRCA1 and non-positive BRCA2
         or date of only test if one is non-positive and other non-existent
         or 0 if no tests;
         Use today's date if the resulting effectiveDate is not in the HL7    -->

    <xsl:value-of select="format-number(xs:double(
      if (($brca1Test eq '1') and ($brca2Test eq '1'))
      then min((hra:brcaLocusDate($brca1Locus), hra:brcaLocusDate($brca2Locus)))
      else if (($brca1Test eq '1') and ($brca2Test ne '1')) then hra:brcaLocusDate($brca1Locus)
      else if (($brca2Test eq '1') and ($brca1Test ne '1')) then hra:brcaLocusDate($brca2Locus)
      else if (($brca1Test eq '2') and ($brca2Test eq '2')) then min((hra:brcaLocusDate($brca1Locus),hra:brcaLocusDate($brca2Locus)))
      else if (($brca1Test eq '2') and ($brca2Test eq '0')) then hra:brcaLocusDate($brca1Locus)
      else if (($brca2Test eq '2') and ($brca1Test eq '0')) then hra:brcaLocusDate($brca2Locus)
      else '0'), '########')
      "/>
    <xsl:text> </xsl:text>
  </xsl:template> 

  <xsl:template name="getColonCancersAndAgesAndMarkers">
    <xsl:param name="origID" as="xs:string" select="./@orig"/>
    
    <!-- Let clinObs be the node set of clinical observations for the current person; there could be more than one -->
    <xsl:variable name="clinObs" select="if ($origID=$probandID) then ($in/FamilyHistory/subject/patient/subjectOf2/clinicalObservation)
      else ($in//*/relative/relationshipHolder/id[@extension=$origID]/../../*/clinicalObservation)">
    </xsl:variable>

    <!-- Find the maximum observed age of onset of all diseases for this person; useful because person's age, if otherwise unknown, must be greater or equal to maxClinObsAge -->
    <xsl:variable name="maxClinObsAge" select="max((hra:avgPosValue($clinObs/subject/dataEstimatedAge/value)))"/>
    
    <!-- Get the clinical observations that are colon cancer - could be more than one -->
    <xsl:variable name="clinObsColonAll" select="
      $clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Colon Cancer']"/>

    <!-- Sort the clin observations with CC for this individual in age ascending order, with missing age clin observations at end -->
    <xsl:variable name="clinObsColonAllSorted" as="element(clinicalObservation)*">
      <xsl:perform-sort select="$clinObsColonAll">
        <xsl:sort select="xs:double((hra:avgAge(.), 99999)[1])"/>
      </xsl:perform-sort>
    </xsl:variable>

    <!-- Get the (first, agewise) clinical observation that is colon cancer for this person, if any -->
    <!-- includes all diseases containing 'colon cancer, rectal cancer, or colorectal, or colon or rectal cancer' string -->
    <xsl:variable name="clinObsColon" select="$clinObsColonAllSorted[1]"/>
        
    <!-- Get the first (if any) of these clinical observations that is endometrial (uterine) cancer -->
    <xsl:variable name="clinObsEndometrial" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Uterine Cancer'][1]">
    </xsl:variable>
    
    <xsl:variable name="colonCancer" select="if (empty($clinObsColon)) then '0' else string(count($clinObsColonAll))"/>
    <xsl:variable name="endometrialCancer" select="if (empty($clinObsEndometrial)) then '0' else '1'"/>
    
    <xsl:variable name="livingEstimatedAge" select="
      if ($origID=$probandID)
      then hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/subjectOf1[1]/livingEstimatedAge[1]/value[1])
      else hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/../../subjectOf1[1]/livingEstimatedAge[1]/value[1])">
    </xsl:variable>
    <xsl:variable name="deceasedEstimatedAge" select="
      if ($origID=$probandID)
      then hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/subjectOf1[1]/deceasedEstimatedAge[1]/value[1])
      else hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/../../subjectOf1[1]/deceasedEstimatedAge[1]/value[1])">
    </xsl:variable>

    <!-- For cancer, get the dataEstimatedAge average of low and high values for the appropriate clinicalObservation  -->
    <!--   However, if age of onset unknown, use min of livingAge, deceasedAge, or 50 -->
    <!-- For no cancer, get first non-empty value in sequence of livingEstimatedAge, deceasedEstimatedAge, maxClinObsAge, and 1 -->
    <xsl:variable name="ageColon" select="
      if ($colonCancer ne '0')
      then (hra:avgPosValue($clinObsColon/subject/dataEstimatedAge[1]/value[1]), min((xs:double($livingEstimatedAge), xs:double($deceasedEstimatedAge), 50)))[1]
      else ($livingEstimatedAge, $deceasedEstimatedAge, $maxClinObsAge, '1')[1]"/>
    
    <xsl:variable name="ageEndometrium" select="
      if ($endometrialCancer ne '0')
      then (hra:avgPosValue($clinObsEndometrial/subject/dataEstimatedAge[1]/value[1]), min((xs:double($livingEstimatedAge), xs:double($deceasedEstimatedAge), 50)))[1]
      else ($livingEstimatedAge, $deceasedEstimatedAge, $maxClinObsAge, '1')[1]"/>
    
    <!-- Get marker data, if colorectal cancer  -->
    <!-- Values are (0, 1, or 2) for msi and for location -->
    <xsl:variable name="MSILocation"  select="
      if (($colonCancer ne '0') or ($endometrialCancer eq '1'))
        then concat(
            (: msi value :)
            if (exists($clinObs/code[(@codeSystemName eq 'msiResults') and (lower-case(@code) eq 'msi-high')])) then '1'
            else if (exists($clinObs/code[(@codeSystemName eq 'immunohistochemistry') and (lower-case(@code) eq 'abnormal')])) then '1'
            else if (exists($clinObs/code[(@codeSystemName eq 'immunohistochemistry') and (lower-case(@code) eq 'normal')])) then '2'
            else if (exists($clinObs/code[(@codeSystemName eq 'msiResults') and (lower-case(@code) eq 'msi-low')])) then '2'
            else '0',
            ' ',
      
            (: location value; Note that even though location doesn't apply to endometrial cancer, MMRpro will process it :)
            if (exists($clinObs/code[(@codeSystemName eq 'location') and (contains(lower-case(@code),'proximal'))])) then '1'
            else if (exists($clinObs/code[(@codeSystemName eq 'location') and (contains(lower-case(@code),'distal'))])) then '2'
            else '0'
          )
      else '0 0'
      "></xsl:variable>
    
    <xsl:value-of select="$colonCancer"/><xsl:text> </xsl:text>
    <xsl:value-of select="$endometrialCancer"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ageColon"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ageEndometrium"/><xsl:text> </xsl:text>
    <xsl:value-of select="$MSILocation"/><xsl:text> </xsl:text>
    
    <!-- message(s) when an age has been estimated -->
    <!-- msg about colon cancer age of onset -->
    <xsl:if test="$colonCancer ne '0'">
      <xsl:if test="not(exists($clinObsColon/subject/dataEstimatedAge/value))">
        <xsl:message>Relative <xsl:value-of select="$origID"/> colon cancer age of onset unknown; used <xsl:value-of select="$ageColon"/></xsl:message>
      </xsl:if>
    </xsl:if>
    <!-- msg about endometrial cancer age of onset -->
    <xsl:if test="$endometrialCancer ne '0'">
      <xsl:if test="not(exists($clinObsEndometrial/subject/dataEstimatedAge/value))">
        <xsl:message>Relative <xsl:value-of select="$origID"/> endometrial cancer age of onset unknown; used <xsl:value-of select="$ageEndometrium"/></xsl:message>
      </xsl:if>
    </xsl:if>
  </xsl:template> 
  
  <xsl:template name="getMMRGermline">
    <xsl:param name="origID" select="./@orig"/>
    <!-- Possibly empty sequence of geneticLocus nodes for MLH1 genetic test results  -->
    <xsl:variable name="mlh1Locus" select="
      if ($origID=$probandID)
      then $in/FamilyHistory/subject/patient/subjectOf2/geneticLocus[hra:isMLH1(value/@code, value/@codeSystemName)]
      else $in/FamilyHistory/subject/patient/patientPerson/relative[relationshipHolder/id/@extension = $origID]/subjectOf2/geneticLocus[hra:isMLH1(value/@code, value/@codeSystemName)]">
    </xsl:variable>
    <!-- Possibly empty sequence of geneticLocus nodes for MSH2 genetic test results  -->
    <xsl:variable name="msh2Locus" select="
      if ($origID=$probandID)
      then $in/FamilyHistory/subject/patient/subjectOf2/geneticLocus[hra:isMSH2(value/@code, value/@codeSystemName)]
      else $in/FamilyHistory/subject/patient/patientPerson/relative[relationshipHolder/id/@extension = $origID]/subjectOf2/geneticLocus[hra:isMSH2(value/@code, value/@codeSystemName)]">
    </xsl:variable>
    <!-- Possibly empty sequence of geneticLocus nodes for MSH6 genetic test results  -->
    <xsl:variable name="msh6Locus" select="
      if ($origID=$probandID)
      then $in/FamilyHistory/subject/patient/subjectOf2/geneticLocus[hra:isMSH6(value/@code, value/@codeSystemName)]
      else $in/FamilyHistory/subject/patient/patientPerson/relative[relationshipHolder/id/@extension = $origID]/subjectOf2/geneticLocus[hra:isMSH6(value/@code, value/@codeSystemName)]">
    </xsl:variable>
    
    <xsl:variable name="mlh1Test" select="
      if (exists($mlh1Locus/component3/sequenceVariation/interpretationCode[hra:isDeleterious(@code)])) then '1'
      else if (count($mlh1Locus) ge 1) then '2'
      else '0' "/>
    <xsl:value-of select="$mlh1Test"/>
    <xsl:text> </xsl:text>
    
    <xsl:variable name="msh2Test" select="
      if (exists($msh2Locus/component3/sequenceVariation/interpretationCode[hra:isDeleterious(@code)])) then '1'
      else if (count($msh2Locus) ge 1) then '2'
      else '0' "/>
    <xsl:value-of select="$msh2Test"/>
    <xsl:text> </xsl:text>
    
    <xsl:variable name="msh6Test" select="
      if (exists($msh6Locus/component3/sequenceVariation/interpretationCode[hra:isDeleterious(@code)])) then '1'
      else if (count($msh6Locus) ge 1) then '2'
      else '0' "/>
    <xsl:value-of select="$msh6Test"/>
    <xsl:text> </xsl:text>

    <!-- insert proxy for Test Order; using this date info, will fix to true test order in R later.
      Use earliest date of positive MLH1, MSH2, MSH6 tests
      or earliest date of non-positive MLH1, MSH2, MSH6 tests
      or 0 if no tests;
      Use today's date if the resulting effectiveDate is not in the HL7    -->
    
    <xsl:value-of select="format-number(xs:double(
      if (($mlh1Test eq '1') or ($msh2Test eq '1') or ($msh6Test eq '1'))
      then (xs:integer(min(($mlh1Locus[component3/sequenceVariation/interpretationCode[hra:isDeleterious(@code)]]/effectiveTime/@value,$msh2Locus[component3/sequenceVariation/interpretationCode[hra:isDeleterious(@code)]]/effectiveTime/@value,$msh6Locus[component3/sequenceVariation/interpretationCode[hra:isDeleterious(@code)]]/effectiveTime/@value))) , format-date(current-date(),'[Y][M,2][D,2]'))[1]
      else if (($mlh1Test eq '2') or ($msh2Test eq '2') or ($msh6Test eq '2'))
      then (xs:integer(min(($mlh1Locus[component3/sequenceVariation/interpretationCode[not(hra:isDeleterious(@code))]]/effectiveTime/@value,$msh2Locus[component3/sequenceVariation/interpretationCode[not(hra:isDeleterious(@code))]]/effectiveTime/@value,$msh6Locus[component3/sequenceVariation/interpretationCode[not(hra:isDeleterious(@code))]]/effectiveTime/@value))) , format-date(current-date(),'[Y][M,2][D,2]'))[1]
      else '0'), '########')
      "/>
    <xsl:text> </xsl:text>
  </xsl:template> 
  
  <xsl:template name="getECC">
    <!-- Extra-colonic cancers -->
    <!-- This column ignored for MMRpro, used only by PREMM  -->
    <xsl:param name="origID" as="xs:string" select="./@orig"/>
    
    <!-- Let clinObs be the node set of clinical observations for the current person; there could be more than one -->
    <xsl:variable name="clinObs" select="if ($origID=$probandID) then ($in/FamilyHistory/subject/patient/subjectOf2/clinicalObservation)
      else ($in//*/relative/relationshipHolder/id[@extension=$origID]/../../*/clinicalObservation)">
    </xsl:variable>
    
    <!-- Get clinical observations that qualify as Extra-colonic cancers for this person, if any -->
    <xsl:variable name="clinObsECC" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = ('Brain Cancer', 'Ovarian Cancer', 'Pancreatic Cancer', 'Stomach Cancer', 'Biliary Tract Cancer', 'Renal Pelvis/Ureter Cancer', 'Small Intestine Cancer', 'Glioblastoma', 'Sebaceous Adenoma')]"/>
    
    <xsl:value-of select="if (empty($clinObsECC)) then '0' else '1'"/><xsl:text> </xsl:text>
  </xsl:template>

  <xsl:template name="getPancreaticCancersAndAges">
    <xsl:param name="origID" as="xs:string" select="./@orig"/>
    
    <!-- Let clinObs be the node set of clinical observations for the current person; there could be more than one -->
    <xsl:variable name="clinObs" select="if ($origID=$probandID) then ($in/FamilyHistory/subject/patient/subjectOf2/clinicalObservation)
      else ($in//*/relative/relationshipHolder/id[@extension=$origID]/../../*/clinicalObservation)">
    </xsl:variable>
    
    <!-- Find the maximum observed age of onset of all diseases for this person; useful because person's age, if otherwise unknown, must be greater or equal to maxClinObsAge -->
    <xsl:variable name="maxClinObsAge" select="max((hra:avgPosValue($clinObs/subject/dataEstimatedAge/value)))"/>
      
    <!-- Get the (first) clinical observation that is pancreatic cancer for this person, if any -->
    <!-- includes all diseases containing 'pancreatic cancer' string -->
    <xsl:variable name="clinObsPancreatic" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Pancreatic Cancer'][1]">
    </xsl:variable>
    
    <xsl:variable name="pancreaticCancer" select="if (empty($clinObsPancreatic)) then '0' else '1'"/>
    
    <xsl:variable name="livingEstimatedAge" select="
      if ($origID=$probandID)
      then hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/subjectOf1[1]/livingEstimatedAge[1]/value[1])
      else hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/../../subjectOf1[1]/livingEstimatedAge[1]/value[1])">
    </xsl:variable>
    <xsl:variable name="deceasedEstimatedAge" select="
      if ($origID=$probandID)
      then hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/subjectOf1[1]/deceasedEstimatedAge[1]/value[1])
      else hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/../../subjectOf1[1]/deceasedEstimatedAge[1]/value[1])">
    </xsl:variable>
    
    <!-- For cancer, get the dataEstimatedAge average of low and high values for the appropriate clinicalObservation  -->
    <!--   However, if age of onset unknown, use min of livingAge, deceasedAge, or 50 -->
    <!-- For no cancer, get first non-empty value in sequence of livingEstimatedAge, deceasedEstimatedAge, maxClinObsAge, and 1 -->
    <xsl:variable name="agePancreatic" select="
      if ($pancreaticCancer ne '0')
      then (hra:avgPosValue($clinObsPancreatic/subject/dataEstimatedAge[1]/value[1]), min((xs:double($livingEstimatedAge), xs:double($deceasedEstimatedAge), 50)))[1]
      else ($livingEstimatedAge, $deceasedEstimatedAge, $maxClinObsAge, '1')[1]"/>
    
    <xsl:value-of select="$pancreaticCancer"/><xsl:text> </xsl:text>
    <xsl:value-of select="$agePancreatic"/><xsl:text> </xsl:text>

    <!-- message(s) when an age has been estimated -->
    <!-- msg about pancreatic cancer age of onset -->
    <xsl:if test="$pancreaticCancer ne '0'">
      <xsl:if test="not(exists($clinObsPancreatic/subject/dataEstimatedAge/value))">
        <xsl:message>Relative <xsl:value-of select="$origID"/> pancreatic cancer age of onset unknown; used <xsl:value-of select="$agePancreatic"/></xsl:message>
      </xsl:if>
    </xsl:if>
  </xsl:template> 
  
  <xsl:template name="getSkinCancersAndAges">
    <xsl:param name="origID" as="xs:string" select="./@orig"/>
    
    <!-- Let clinObs be the node set of clinical observations for the current person; there could be more than one -->
    <xsl:variable name="clinObs" select="if ($origID=$probandID) then ($in/FamilyHistory/subject/patient/subjectOf2/clinicalObservation)
      else ($in//*/relative/relationshipHolder/id[@extension=$origID]/../../*/clinicalObservation)">
    </xsl:variable>
    
    <!-- Find the maximum observed age of onset of all diseases for this person; useful because person's age, if otherwise unknown, must be greater or equal to maxClinObsAge -->
    <xsl:variable name="maxClinObsAge" select="max((hra:avgPosValue($clinObs/subject/dataEstimatedAge/value)))"/>
    
    <!-- Get the (first) clinical observation that is Melanoma for this person, if any -->
    <!-- includes all diseases that starts with 'melanoma' string (does not include ocular melanoma)-->
    <xsl:variable name="clinObsMelanoma" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'Melanoma'][1]"/>
        
    <xsl:variable name="melanoma" select="if (empty($clinObsMelanoma)) then '0' else '1'"/>
    
    <xsl:variable name="livingEstimatedAge" select="
      if ($origID=$probandID)
      then hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/subjectOf1[1]/livingEstimatedAge[1]/value[1])
      else hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/../../subjectOf1[1]/livingEstimatedAge[1]/value[1])">
    </xsl:variable>
    <xsl:variable name="deceasedEstimatedAge" select="
      if ($origID=$probandID)
      then hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/subjectOf1[1]/deceasedEstimatedAge[1]/value[1])
      else hra:avgPosValue($in/FamilyHistory/subject[1]/patient[1]/patientPerson[1]/relative/relationshipHolder/id[@extension=$origID]/../../subjectOf1[1]/deceasedEstimatedAge[1]/value[1])">
    </xsl:variable>
    
    <!-- For cancer, get the dataEstimatedAge average of low and high values for the appropriate clinicalObservation  -->
    <!--   However, if age of onset unknown, use min of livingAge, deceasedAge, or 50 -->
    <!-- For no cancer, get first non-empty value in sequence of livingEstimatedAge, deceasedEstimatedAge, maxClinObsAge, and 1 -->
    <xsl:variable name="ageMelanoma" select="
      if ($melanoma ne '0')
      then (hra:avgPosValue($clinObsMelanoma/subject/dataEstimatedAge[1]/value[1]), min((xs:double($livingEstimatedAge), xs:double($deceasedEstimatedAge), 50)))[1]
      else ($livingEstimatedAge, $deceasedEstimatedAge, $maxClinObsAge, '1')[1]"/>
    
    <xsl:value-of select="$melanoma"/><xsl:text> </xsl:text>
    <xsl:value-of select="$ageMelanoma"/><xsl:text> </xsl:text>

    <!-- message(s) when an age has been estimated -->
    <!-- msg about melanoma age of onset -->
    <xsl:if test="$melanoma ne '0'">
      <xsl:if test="not(exists($clinObsMelanoma/subject/dataEstimatedAge/value))">
        <xsl:message>Relative <xsl:value-of select="$origID"/> melanoma age of onset unknown; used <xsl:value-of select="$ageMelanoma"/></xsl:message>
      </xsl:if>
    </xsl:if>
  </xsl:template> 

  <xsl:template name="getSkinGermline">
    <xsl:param name="origID" select="./@orig"/>
    <!-- Possibly empty sequence of geneticLocus nodes for P16 genetic test results  -->
    <xsl:variable name="p16Locus" select="
      if ($origID=$probandID)
      then $in/FamilyHistory/subject/patient/subjectOf2/geneticLocus[hra:isP16(value/@code, value/@codeSystemName)]
      else $in/FamilyHistory/subject/patient/patientPerson/relative[relationshipHolder/id/@extension = $origID]/subjectOf2/geneticLocus[hra:isP16(value/@code, value/@codeSystemName)]">
    </xsl:variable>
    
    <xsl:variable name="p16Test" select="
      if (exists($p16Locus/component3/sequenceVariation/interpretationCode[hra:isDeleterious(@code)])) then '1'
      else if (count($p16Locus) ge 1) then '2'
      else '0' "/>
    <xsl:value-of select="$p16Test"/>
    <xsl:text> </xsl:text>
    
    <!-- insert proxy for Test Order; using this date info, will fix to true test order in R later.
      Use earliest date of positive P16 tests
      or earliest date of non-positive P16 tests
      or 0 if no tests;
      Use today's date if the resulting effectiveDate is not in the HL7    -->
    
    <xsl:value-of select="format-number(xs:double(
      if ($p16Test eq '1')
      then (xs:integer($p16Locus[component3/sequenceVariation/interpretationCode[hra:isDeleterious(@code)]]/effectiveTime/@value), format-date(current-date(),'[Y][M,2][D,2]'))[1]
      else if ($p16Test eq '2')
      then (xs:integer($p16Locus[component3/sequenceVariation/interpretationCode[not(hra:isDeleterious(@code))]]/effectiveTime/@value) , format-date(current-date(),'[Y][M,2][D,2]'))[1]
      else '0'), '########')
      "/>
  </xsl:template> 
  
  <xsl:template name="getProvidedHL7RelCode">
    <xsl:param name="origID" select="./@orig"/>
    <xsl:text> </xsl:text>
    <xsl:value-of select="if ($origID=$probandID)
      then 'NA'
      else (key('relative',$origID,$in)/code/@code, 'NA')[1]"></xsl:value-of>
  </xsl:template> 
  
  <xsl:template name="getDCISInfo">
    <xsl:param name="origID" as="xs:string" select="./@orig"/>

    <!-- Let clinObs be the node set of clinical observations for the current person; there could be more than one -->
    <xsl:variable name="clinObs" select="if ($origID=$probandID) then ($in/FamilyHistory/subject/patient/subjectOf2/clinicalObservation)
      else ($in//*/relative/relationshipHolder/id[@extension=$origID]/../../*/clinicalObservation)">
    </xsl:variable>
    
    <!-- Get clinical observations that are DCIS (could be 0, 1, or more nodes in this sequence) -->
    <!-- $dcis shows count -->
    <xsl:variable name="clinObsDCIS" select="$clinObs[hra:meaning(code/@code, code/@codeSystemName) = 'DCIS']"/>
    <xsl:variable name="dcis" select="
      if (empty($clinObsDCIS))
      then '0'
      else xs:string(count($clinObsDCIS))"/>

    <!-- If the relative has one or more DCIS, use the minimum age of all DCIS provided; if none available, use NA --> 
    <!-- If the relative has no DCIS, use NA --> 
    <xsl:variable name="ageDCIS" select="
      if ($dcis ne '0')
      then (min((for $i in $clinObsDCIS return hra:avgAge($i))), 'NA')[1]
      else 'NA'"/>

    <xsl:text> </xsl:text>
    <xsl:value-of select="$dcis"/>
    <xsl:text> </xsl:text>
    <xsl:value-of select="$ageDCIS"/>
  </xsl:template> 
  
  <!-- Debug message -->
  <xsl:template name="debug">
    <xsl:message>This s/b male: <xsl:value-of select="(hra:meaning('248153007', 'SNOMED_CT'),'emptySeq')[1]"/></xsl:message>
  </xsl:template>

</xsl:stylesheet>
