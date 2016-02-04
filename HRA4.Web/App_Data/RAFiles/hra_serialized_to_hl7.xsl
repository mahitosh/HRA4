<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    version="2.0"
    xmlns:hra="http://www.hughesriskapps.com"
    exclude-result-prefixes="hra">

  <xsl:output method="xml" indent="yes"/>
  <xsl:preserve-space elements="*"/>

  <!-- Following can be overidden by transformer invocation with parameter set -->
  <xsl:param name="localBaseUri" select="'file:/./App_Data/RAFiles/'"/>
  <!-- BayesMendel default is that DCIS is not considered cancer, but with this flag we can go either way; leave as default if want hl7 to reflect accurate object model -->
  <xsl:param name="dcisAsCancer" select="'0'"/>  
  <!-- Optionally don't remove PHI; use '0' to leave PHI in -->
  <xsl:param name="deIdentify" select="'1'"/>  
  
  <xsl:variable name="riskMeanings" select="doc(resolve-uri('riskMeanings.xml', $localBaseUri))"/> 
  <xsl:variable name="geneCodes" select="doc(resolve-uri('GeneCodes.xml', $localBaseUri))"/>
  <!--Subset of GeneCodes which use NCBI Entrez-->
  <xsl:variable name="NCBItable">
    <xsl:copy-of select="$geneCodes/root/Gene[codeSystemName='NCBI Entrez']"/>
  </xsl:variable>
  <!--Subset of GeneCodes which use HGNC-->
  <xsl:variable name="HGNCtable">
    <xsl:copy-of select="$geneCodes/root/Gene[codeSystem='HGNC']"/>
  </xsl:variable>

  <xsl:variable name="HL7Relationships" select="doc(resolve-uri('HL7Relationships.xml', $localBaseUri))"/>
  
  <xsl:variable name="patientAge" select="number(//HraObject[@type='Patient'][1]/age)"/>
  
  <!-- Functions -->
  
  <!-- function to return a risk meaning given a code and codeSystem -->
  <!-- returns the empty sequence if not found -->
  <!-- output can return multiple meanings, if present -->
  <xsl:function name="hra:meaning">
    <xsl:param name="code"/>
    <xsl:param name="codeSystemIn"/>
    <xsl:variable name="codeSystem" select="if (starts-with(upper-case($codeSystemIn), 'SNOMED')) then 'SNOMED_CT' else $codeSystemIn"></xsl:variable>
    <xsl:sequence select="$riskMeanings/root/row[(code eq $code) and (codeSystem eq $codeSystem)]/meaning"/>
  </xsl:function>
  
  <!-- function to cap an age at 89 when deIdentify active, to avoid PHI issue with old ages -->
  <xsl:function name="hra:capAge">
    <xsl:param name="agep"/>
    <xsl:sequence select="if ($deIdentify eq '1')
      then if (number($agep) = $agep)
            then min(($agep, 89))
            else ()
      else $agep"/>
  </xsl:function>
  
  <xsl:function name="hra:makeClinicalObservation">
    <!-- makes a Clinical Observation using the passed params, some of which can be empty -->
    <xsl:param name="code"/>
    <xsl:param name="codeSystemName"/>
    <xsl:param name="displayName"/>
    <xsl:param name="year"/>
    <xsl:param name="month"/>
    <xsl:param name="day"/>
    <xsl:param name="age"/>
    <subjectOf2 typeCode="SBJ">
      <clinicalObservation classCode="OBS" moodCode="EVN">
        <xsl:variable name="tempCode" select="translate(if ($code eq '') then 'null' else $code ,' ','_')"/>
        <xsl:variable name="tempCodeSystemName" select="($codeSystemName, 'SNOMED_CT')[1]"/>
        <xsl:variable name="treatDCISAsCancer" select="($dcisAsCancer ne '0') and (hra:meaning($tempCode, $tempCodeSystemName) = ('DCIS'))"/>
        <!-- optionally substitute special disease code for DCIS that is treated as breast cancer by risk models -->
        <xsl:choose>
          <xsl:when test="$treatDCISAsCancer">
            <code code="140" codeSystemName="UML" displayName="Breast Cancer (DCIS) that is considered Invasive Breast Cancer for BRCAPro"/>
          </xsl:when>
          <xsl:otherwise>
            <code code="{$tempCode}" codeSystemName="{$tempCodeSystemName}">
              <xsl:if test="$displayName">
                <xsl:attribute name="displayName" select="$displayName"/>
              </xsl:if>
            </code>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:if test="(number($year) gt 0) and (string-length($year) eq 4)">
          <effectiveTime value="{concat($year, if ((number($month) ge 0) and (number($month) le 12)) then format-number($month, '00') else '06', if ((number($day) gt 0) and (number($day) le 31)) then format-number($day, '00') else '15')}"/>
        </xsl:if>
        <xsl:if test="(string-length($age) gt 0) and (number($age) ge 0)">
          <subject typeCode="SUBJ">
            <dataEstimatedAge classCode="OBS" moodCode="EVN">
              <code code="397659008" displayName="Age" codeSystemName="SNOMED_CT"/>
              <value>
                <low value="{hra:capAge($age)}"/>
                <high value="{hra:capAge($age)}"/>
              </value>
            </dataEstimatedAge>
          </subject>
        </xsl:if>
      </clinicalObservation>
    </subjectOf2>
  </xsl:function>

  <!-- This is an identity copy template; it performs a deep copy, recursively  -->
  <!--    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>-->

  <!-- except throw away all non-standard HL7 FamilyHistory elements  -->
  <xsl:template match="FamilyHistory/HraObject[@type='Patient']/*[not(name() = ('relativeID', 'gender', 'dob', 'vitalStatus', 'age', 'Ethnicity', 'isAshkenazi', 'isHispanic'))]">
  </xsl:template>

  <xsl:template match="FamilyHistory">
    <FamilyHistory classCode="OBS" moodCode="EVN">
      <!-- replace apptid with 0 if de-identifying, or if exists, but is zero-length -->
      <id root="2.16.840.1.113883.6.117" extension="{(if ($deIdentify ne '0') then 0 else (), /FamilyHistory/HraObject[@type='Patient'][1]/apptid[1][string-length(.) > 0], 0)[1]}" assigningAuthorityName="HRA"/>
      <code code="10157-6" codeSystemName="LOINC" displayName="HISTORY OF FAMILY MEMBER DISEASE"/>
      <text>ClinicName; InstitutionName</text>
      <effectiveTime value="{format-dateTime(current-dateTime(),'[Y][M,2][D,2][H,2][m,2]')}"/>
      <subject typeCode="SBJ">
        <patient classCode="PAT">
          <xsl:choose>
            <!-- if the patient MRN is a zero-length string, due to de-identification, for example,
              or we're intentionally deIdentifying here, remove the extension attribute altogether to make HL7 valid -->
            <xsl:when test="($deIdentify eq '0') and exists(/FamilyHistory/HraObject[@type='Patient'][1]/unitnum[1])">
              <id root="2.16.840.1.113883.6.117" extension="{/FamilyHistory/HraObject[@type='Patient'][1]/unitnum[1]}"/>
            </xsl:when>
            <xsl:otherwise>
              <id root="2.16.840.1.113883.6.117"/>
            </xsl:otherwise>
          </xsl:choose>
          <xsl:apply-templates select="HraObject[@type='Patient']"/>
        </patient>
      </subject>
    </FamilyHistory>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']">
    <patientPerson classCode="PSN" determinerCode="INSTANCE">
      <!-- copy over the rest of patientPerson's attributes and all xml children in the appropriate order -->
      <xsl:apply-templates select="relativeID"/>
      <xsl:call-template name="processNames">
        <xsl:with-param name="first" select="firstName"/>
        <xsl:with-param name="middle" select="middleName"/>
        <xsl:with-param name="last" select="lastName"/>
        <xsl:with-param name="full" select="name"/>
      </xsl:call-template>
      <xsl:apply-templates select="gender"/>
      <xsl:apply-templates select="dob"/>
      <xsl:apply-templates select="dateOfDeath"/>
      <xsl:apply-templates select="vitalStatus"/>
      <xsl:apply-templates select="Ethnicity"/>
      <xsl:apply-templates select="isAshkenazi"/>
      <xsl:apply-templates select="isHispanic"/>
      <xsl:apply-templates select="/FamilyHistory/HraObject[@type='Person']"/>
    </patientPerson>
    <xsl:apply-templates select="age"/>
    <xsl:call-template name="obsandgts">
      <xsl:with-param name="loc" select="."/>
    </xsl:call-template>
    <xsl:if test="number(twinID[1]) gt 0">
      <xsl:variable name="ptTwinID" select="twinID[1]"/>
      <!-- verify at least one twin with this twinID is an Identical twin per the object model -->
      <xsl:if test="//FamilyHistory/HraObject[twinID[1] eq $ptTwinID]/twinType = 'Identical'">
        <subjectOf2 typeCode="SBJ">
          <clinicalObservation classCode="OBS" moodCode="EVN">
            <code code="313415001" codeSystemName="SNOMED_CT" displayName="Identical twin (person)">
              <qualifier>
                <value code="{number(twinID[1])}"/>
              </qualifier>
            </code>
          </clinicalObservation>
        </subjectOf2>
      </xsl:if>
    </xsl:if>
    <xsl:apply-templates select="ObGynHx[1]/startedMenstruating[1]"/>
    <xsl:apply-templates select="ObGynHx[1]/ageFirstChildBorn[1]"/>
    <xsl:apply-templates select="procedureHx[1]/breastBx[1]/breastBiopsies[1]"/>
    <xsl:apply-templates select="ObGynHx[1]/menopausalStatus[1]"/>
    <xsl:apply-templates select="ObGynHx[1]/stoppedMenstruating[1]"/>
    <xsl:apply-templates select="PhysicalExam[1]/heightInches[1]"/>
    <xsl:apply-templates select="PhysicalExam[1]/weightPounds[1]"/>
    <xsl:call-template name="processHRT">
      <xsl:with-param name="loc" select="ObGynHx[1]"/>
    </xsl:call-template>
    <xsl:apply-templates select="SocialHx[1]/colonoscopyLast10Years[1]"/>
    <xsl:apply-templates select="SocialHx[1]/colonPolypLast10Years[1]"/>
    <xsl:apply-templates select="SocialHx[1]/RegularAspirinUser[1]"/>
    <xsl:apply-templates select="SocialHx[1]/RegularIbuprofenUser[1]"/>
    <xsl:apply-templates select="SocialHx[1]/vigorousPhysicalActivityHoursPerWeek[1]"/>
    <xsl:apply-templates select="SocialHx[1]/numYearsSmokedCigarettes[1]"/>
    <xsl:apply-templates select="SocialHx[1]/numCigarettesPerDay[1]"/>
    <xsl:apply-templates select="SocialHx[1]/vegetableServingsPerDay[1]"/>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/relativeID">
    <id extension="{.}"/>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/gender">
    <administrativeGenderCode code="{if (starts-with(upper-case(.),'M')) then 'M' else 'F'}"/>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/dob">
    <!-- only keep if we're not de-identifying -->
    <xsl:if test="$deIdentify eq '0'">
      <!-- presumed always in mm/dd/yyyy format; need to change to yyyymmdd -->
      <birthTime value="{concat(substring(.,7,4), substring(.,1,2), substring(.,4,2))}"/>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/dateOfDeath">
    <!-- only keep if we're not de-identifying -->
    <xsl:if test="$deIdentify eq '0'">
      <!-- presumed always in mm/dd/yyyy format; need to change to yyyymmdd -->
      <deceasedTime value="{concat(substring(.,7,4), substring(.,1,2), substring(.,4,2))}"/>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/vitalStatus">
    <deceasedInd value="{if (upper-case(.) eq 'DEAD') then 'true' else 'false'}"/>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/Ethnicity[HraObject[@type='Race']/race]">
    <xsl:variable name="r" select="HraObject[@type='Race']/race"/>
    <xsl:choose>
      <xsl:when test="$r=('Black', 'African American or Black')">
        <raceCode code="2054-5" codeSystemName="HL7" displayName="African American or Black"/>
      </xsl:when>
      <xsl:when test="$r=('NativeAmerican', 'American Indian/Aleutian/Eskimo')">
        <raceCode code="1002-5" codeSystemName="HL7" displayName="American Indian/Aleutian/Eskimo"/>
      </xsl:when>
      <xsl:when test="$r=('Asian', 'Asian or Pacific Islander')">
        <raceCode code="2028-9" codeSystemName="HL7" displayName="Asian or Pacific Islander"/>
      </xsl:when>
      <xsl:when test="$r=('Caribbean/West Indian')">
        <raceCode code="2075-0" codeSystemName="HL7" displayName="Caribbean/West Indian"/>
      </xsl:when>
      <xsl:when test="$r=('Caucasian or White', 'White')">
        <raceCode code="2106-3" codeSystemName="HL7" displayName="Caucasian or White"/>
      </xsl:when>
      <xsl:when test="$r=('Ashkenazi')">
        <raceCode code="2131-1" codeSystemName="HL7" displayName="Ashkenazi"/>
      </xsl:when>
      <xsl:otherwise>
        <raceCode code="2131-1" codeSystemName="HL7" displayName="Other"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/isAshkenazi">
    <xsl:if test=".='Yes'">
      <raceCode code="2131-1" codeSystemName="HL7" displayName="Ashkenazi"/>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/isHispanic">
    <xsl:if test=".='Yes'">
      <raceCode code="2135-2" codeSystemName="HL7" displayName="Hispanic"/>
    </xsl:if>
  </xsl:template>

  <xsl:template match="/FamilyHistory/HraObject[@type='Person']">
    <relative classCode="PRS">
      <xsl:variable name="relEnglishRaw" select="./relationship"/>
      <xsl:variable name="relEnglish" select="
                if (starts-with($relEnglishRaw, 'Cousin')) then 'Cousin' else $relEnglishRaw"/>
      <xsl:variable name="bloodline" select="./bloodline"/>
      <xsl:choose>
        <xsl:when test="$bloodline=('Maternal', 'Paternal')">
          <!-- Since the bloodline is supplied in the input, we must match it exactly in the HL7Relationships file; if more than one match (shouldn't happen as HL7Relationships should contain a unique entry for each distinct relative), use the first; if no matches: NotAvailable -->
          <code code="{((($HL7Relationships/root/Relationship[(Mgh=$relEnglish) and (MghBloodline=$bloodline)]/HL7Code)[1], 'NotAvailable')[1])}"/>
        </xsl:when>
        <xsl:otherwise> <!-- Neither paternal nor maternal bloodline supplied in input -->
          <!-- Look for the first occurrence in the Hl7Relationships file for the matching relative also without bloodline - this is the correct match for relations that specifically have no bloodline, like an unlinked cousin;
                If not found, look for first occurrence in the Hl7Relationships file for the matching relative with any bloodline - this matches, for example, a mother supplied w/no bloodline;
                otherwise, NotAvailable -->
          <code code="{((($HL7Relationships/root/Relationship[(Mgh=$relEnglish) and not (MghBloodline=('Paternal', 'Maternal'))]/HL7Code)[1],
                      ($HL7Relationships/root/Relationship[(Mgh=$relEnglish)]/HL7Code)[1],
                      'NotAvailable')[1])}"/>
        </xsl:otherwise>
      </xsl:choose>
      <relationshipHolder classCode="PSN" determinerCode="INSTANCE">
        <id extension="{./relativeID}"/>
        <xsl:call-template name="processNames">
          <xsl:with-param name="first" select="firstName"/>
          <xsl:with-param name="middle" select="middleName"/>
          <xsl:with-param name="last" select="lastName"/>
          <xsl:with-param name="full" select="name"/>
        </xsl:call-template>
        <administrativeGenderCode code="{if (starts-with(upper-case(./gender),'M')) then 'M' else 'F'}"/>
        <deceasedInd value="{if (upper-case(./vitalStatus) eq 'DEAD') then 'true' else 'false'}"/>
        <xsl:if test="(string-length(./motherID) gt 0) and (./motherID ne '0')">
          <relative classCode="PRS">
            <code code="NMTH"/>
            <relationshipHolder classCode="PSN" determinerCode="INSTANCE">
              <id extension="{./motherID}"/>
            </relationshipHolder>
          </relative>
        </xsl:if>
        <xsl:if test="(string-length(./fatherID) gt 0) and (./fatherID ne '0')">
          <relative classCode="PRS">
            <code code="NFTH"/>
            <relationshipHolder classCode="PSN" determinerCode="INSTANCE">
              <id extension="{./fatherID}"/>
            </relationshipHolder>
          </relative>
        </xsl:if>
      </relationshipHolder>
      <xsl:if test="number(./age) gt 0">
        <xsl:choose>
          <xsl:when test="upper-case(./vitalStatus) eq 'DEAD'">
            <subjectOf1 typeCode="SBJ">
              <deceasedEstimatedAge classCode="OBS" moodCode="EVN">
                <code code="39016-1" displayName="AGE AT DEATH" codeSystemName="LOINC"/>
                <value value="{hra:capAge(./age)}"/>
              </deceasedEstimatedAge>
            </subjectOf1>
          </xsl:when>
          <xsl:otherwise>
            <subjectOf1 typeCode="SBJ">
              <livingEstimatedAge classCode="OBS" moodCode="EVN">
                <code code="21611-9" displayName="ESTIMATED AGE" codeSystemName="LOINC"/>
                <value value="{hra:capAge(./age)}"/>
              </livingEstimatedAge>
            </subjectOf1>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:if>
      <xsl:call-template name="obsandgts">
        <xsl:with-param name="loc" select="."/>
      </xsl:call-template>
      <xsl:if test="number(twinID[1]) gt 0">
        <xsl:variable name="thisTwinID" select="twinID[1]"/>
        <!-- verify at least one twin with this twinID is an Identical twin per the object model -->
        <xsl:if test="//FamilyHistory/HraObject[twinID[1] eq $thisTwinID]/twinType = 'Identical'">
          <subjectOf2 typeCode="SBJ">
            <clinicalObservation classCode="OBS" moodCode="EVN">
              <code code="313415001" codeSystemName="SNOMED_CT" displayName="Identical twin (person)">
                <qualifier>
                  <value code="{number(twinID[1])}"/>
                </qualifier>
              </code>
            </clinicalObservation>
          </subjectOf2>
        </xsl:if>
      </xsl:if>
    </relative>
  </xsl:template>
  
  <xsl:template name="processNames">
    <xsl:param name="first"/>
    <xsl:param name="middle"/>
    <xsl:param name="last"/>
    <xsl:param name="full"/>
    <xsl:if test="$deIdentify eq '0'">
      <xsl:variable name="parsedLast" select="if (contains($full, ','))
        then substring-before($full, ',') (: contains a comma, string presumably begins w/last name :)
        else if (matches($full, '.+ ([^ ]+)$')) then replace($full, '.+ ([^ ]+)$','$1') else ''"/>
      <xsl:variable name="parsedFirst" select="if (contains($full, ','))
        then if (matches($full, '[^,]+, *(.+)$')) then replace($full, '[^,]+, *(.+)$','$1') else '' (: contains a comma, use everything after first comma and optional spaces :)
        else replace($full, '(.+) [^ ]+$', '$1')"/>
      <xsl:variable name="given" select="
        if (string($first)) then string($first)
        else if (string($parsedFirst)) then string($parsedFirst)
        else ''"/>
      <xsl:variable name="family" select="
        if (string($last)) then string($last)
        else if (string($parsedLast)) then string($parsedLast)
        else ''"/>
      <!-- TODO this is probably wrong - given and family may or may not need to be elements, 
      usable attributes may include more than just formatted, given, and first -->
      <xsl:if test="$given or $family">
        <name>
          <xsl:attribute name ="formatted">
            <xsl:value-of select="$full"/>
          </xsl:attribute>
          <xsl:attribute name ="given">
            <xsl:value-of select="$given"/>
          </xsl:attribute>
          <xsl:attribute name ="family">
            <xsl:value-of select="$family"/>
          </xsl:attribute>
          <given><xsl:value-of select="$given"/></given>
          <family><xsl:value-of select="$family"/></family>
        </name>
      </xsl:if>
    </xsl:if>
  </xsl:template>

  <xsl:template name="obsandgts">
    <xsl:param name="loc"/>
    <xsl:if test="$loc/PMH/Observations/HraObject[@type='ClincalObservation']">
      <xsl:for-each select="$loc/PMH/Observations/HraObject[@type='ClincalObservation']">
        <xsl:variable name="this" select="."/>
        <xsl:variable name="details" select="$this/Details"/>
        <xsl:copy-of select="hra:makeClinicalObservation($this/snomed, 'SNOMED_CT', $this/disease, $this/diagnosisYear, $this/diagnosisMonth, (), $this/ageDiagnosis)"/>
        <xsl:if test="string-length($details/immunohistochemistry) gt 0">
          <xsl:copy-of select="hra:makeClinicalObservation($details/immunohistochemistry, 'immunohistochemistry', (), $details/diagnosisYear, $details/diagnosisMonth, (), $this/ageDiagnosis)"/>
        </xsl:if>
        <xsl:if test="string-length($details/location) gt 0">
          <xsl:copy-of select="hra:makeClinicalObservation($details/location, 'location', (), $details/diagnosisYear, $details/diagnosisMonth, (), $this/ageDiagnosis)"/>
        </xsl:if>
        <xsl:if test="string-length($details/msiResults) gt 0">
          <xsl:copy-of select="hra:makeClinicalObservation($details/msiResults, 'msiResults', (), $details/diagnosisYear, $details/diagnosisMonth, (), $this/ageDiagnosis)"/>
        </xsl:if>
        <xsl:if test="string-length($details/ERStatus) gt 0">
          <xsl:copy-of select="hra:makeClinicalObservation($details/ERStatus, 'ERStatus', (), $details/diagnosisYear, $details/diagnosisMonth, (), $this/ageDiagnosis)"/>
        </xsl:if>
        <xsl:if test="string-length($details/PRStatus) gt 0">
          <xsl:copy-of select="hra:makeClinicalObservation($details/PRStatus, 'PRStatus', (), $details/diagnosisYear, $details/diagnosisMonth, (), $this/ageDiagnosis)"/>
        </xsl:if>
        <xsl:if test="string-length($details/Her2NeuFISH) gt 0">
          <xsl:copy-of select="hra:makeClinicalObservation($details/Her2NeuFISH, 'Her2NeuFISH', (), $details/diagnosisYear, $details/diagnosisMonth, (), $this/ageDiagnosis)"/>
        </xsl:if>
        <xsl:if test="string-length($details/Her2NeuIHC) gt 0">
          <xsl:copy-of select="hra:makeClinicalObservation($details/Her2NeuIHC, 'Her2NeuIHC', (), $details/diagnosisYear, $details/diagnosisMonth, (), $this/ageDiagnosis)"/>
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="$loc/PMH/GeneticTests[*]">
      <!-- GeneticTests element has at least one child -->
      <xsl:for-each select="$loc/PMH/GeneticTests/HraObject[@type='GeneticTest']">
        <xsl:variable name="thisGT" select="."/>
        <xsl:if test="./status/text() eq 'Complete'">
          <xsl:for-each select="./GeneticTestResults/GeneticTestResult">
            <xsl:variable name="gtr" select="."/>
            <subjectOf2 typeCode="SBJ">
              <geneticLocus classCode="LOC" moodCode="EVN">
                <text>
                  <xsl:value-of select="$thisGT/panelName"/>
                </text>
                <xsl:if test="(number($thisGT/testYear) gt 0) and (string-length($thisGT/testYear) eq 4)">
                  <effectiveTime value="{concat($thisGT/testYear, if ((number($thisGT/testMonth) ge 0) and (number($thisGT/testMonth) le 12)) then format-number($thisGT/testMonth, '00') else '06', if ((number($thisGT/testDay) gt 0) and (number($thisGT/testDay) le 31)) then format-number($thisGT/testDay, '00') else '15')}"/>
                </xsl:if>
                <xsl:variable name="geneName" select="$gtr/geneName"/>
                <value code="{($NCBItable/Gene[displayName=$geneName]/code,'NA')[1]}" displayName="{$geneName}" codeSystemName="{($NCBItable/Gene[displayName=$geneName]/codeSystemName,'NA')[1]}">
                  <translation code="{($HGNCtable/Gene[displayName=$geneName]/code,'NA')[1]}" displayName="{$geneName}" codeSystem="HGNC"/>
                </value>
                <component3 typeCode="COMP">
                  <sequenceVariation classCode="SEQVAR" moodCode="EVN">
                    <xsl:variable name="result" select="
                      if ($thisGT/panelID ne '26')  (: regular non-familial test panel :)
                      then $gtr/resultSignificance
                      else if ($gtr/ASOResult eq 'Found')
                      then if (string-length($gtr/ASOResultSignificance) gt 0)
                      then $gtr/ASOResultSignificance
                      else if (string-length($gtr/resultSignificance) gt 0)
                      then $gtr/resultSignificance
                      else 'Deleterious'
                      else if ($gtr/ASOResult eq 'Not Found')
                      then 'Negative'
                      else $gtr/ASOResult"
                    />
                    <interpretationCode code="{if (empty($result) or (string-length($result) = 0)) then 'NotAvailable' else translate($result, ' ', '_')}"/>
                  </sequenceVariation>
                </component3>
              </geneticLocus>
            </subjectOf2>
          </xsl:for-each>
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/age">
    <xsl:if test="number(.) gt 0">
      <xsl:choose>
        <xsl:when test="upper-case(../vitalStatus) eq 'DEAD'">
          <subjectOf1 typeCode="SBJ">
            <deceasedEstimatedAge classCode="OBS" moodCode="EVN">
              <code code="39016-1" displayName="AGE AT DEATH" codeSystemName="LOINC"/>
              <value value="{hra:capAge(.)}"/>
            </deceasedEstimatedAge>
          </subjectOf1>
        </xsl:when>
        <xsl:otherwise>
          <subjectOf1 typeCode="SBJ">
            <livingEstimatedAge classCode="OBS" moodCode="EVN">
              <code code="21611-9" displayName="ESTIMATED AGE" codeSystemName="LOINC"/>
              <value value="{hra:capAge(.)}"/>
            </livingEstimatedAge>
          </subjectOf1>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/ObGynHx/startedMenstruating">
    <xsl:if test="number(.) gt 0">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="C19666" codeSystemName="NCI" displayName="Age at Menarche"/>
          <subject typeCode="SUBJ">
            <dataEstimatedAge classCode="OBS" moodCode="EVN">
              <code code="397659008" displayName="Age" codeSystemName="SNOMED CT"/>
              <value>
                <low value="{hra:capAge(.)}"/>
                <high value="{hra:capAge(.)}"/>
              </value>
            </dataEstimatedAge>
          </subject>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/ObGynHx/ageFirstChildBorn">
    <xsl:if test="number(.) gt 0">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="C19667" codeSystemName="NCI" displayName="Age at First Live Birth"/>
          <subject typeCode="SUBJ">
            <dataEstimatedAge classCode="OBS" moodCode="EVN">
              <code code="397659008" displayName="Age" codeSystemName="SNOMED CT"/>
              <value>
                <low value="{hra:capAge(.)}"/>
                <high value="{hra:capAge(.)}"/>
              </value>
            </dataEstimatedAge>
          </subject>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="HraObject[@type='Patient']/procedureHx[1]/breastBx[1]/breastBiopsies[1]">
    <xsl:if test="number(.) gt 0">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="126" codeSystemName="UML" displayName="Number of breast biopsies">
            <qualifier>
              <value code="{.}"/>
            </qualifier>
          </code>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/ObGynHx[1]/menopausalStatus[1]">
    <xsl:variable name="menopausalStatVal" select="
      if (empty(./node()))
      then if (empty(../currentlyMenstruating/node()))
              then '3'
              else if (lower-case(../currentlyMenstruating) eq 'yes')
                    then '0'
                    else if (lower-case(../currentlyMenstruating) eq 'no')
                            then '2'
                            else '3'
      else (: non-empty menopausalStatus :)
        if (starts-with(lower-case(.),'pre')) then '0'
        else if (starts-with(lower-case(.),'peri')) then '1'
        else if (starts-with(lower-case(.),'post')) then '2'
        else '3'
      "/>
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="130" codeSystemName="UML" displayName="Menopausal Status"/>
          <statusCode code="{$menopausalStatVal}"/>
        </clinicalObservation>
      </subjectOf2>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/ObGynHx[1]/stoppedMenstruating[1]">
    <xsl:if test="number(.) gt 0">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="131" codeSystemName="UML" displayName="Menopause Age"/>
          <subject typeCode="SUBJ">
            <dataEstimatedAge classCode="OBS" moodCode="EVN">
              <code code="397659008" displayName="Age" codeSystemName="SNOMED CT"/>
              <value>
                <low value="{hra:capAge(.)}"/>
                <high value="{hra:capAge(.)}"/>
              </value>
            </dataEstimatedAge>
          </subject>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/PhysicalExam[1]/heightInches[1]">
    <xsl:if test="number(.) gt 0">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="132" codeSystemName="UML" displayName="Height">
            <qualifier>
              <value code="{round-half-to-even(0.3048*number(.) div 12.0, 2)}"/>
            </qualifier>
          </code>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/PhysicalExam[1]/weightPounds[1]">
    <xsl:if test="number(.) gt 0">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="133" codeSystemName="UML" displayName="Weight">
            <qualifier>
              <value code="{round-half-to-even(0.45359237*number(.), 2)}"/>
            </qualifier>
          </code>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>
  
  <xsl:template name="processHRT">
    <xsl:param name="loc"/>
    <xsl:variable name="useRaw" select="string($loc/hormoneUse)"/>
    <xsl:variable name="ageRaw" select="string($loc/hormoneAge)"/>
    <xsl:variable name="useYearsRaw" select="string($loc/hormoneUseYears)"/>
    <xsl:variable name="yearsSinceLastUseRaw" select="string($loc/hormoneYearsSinceLastUse)"/>
    <xsl:variable name="combinedRaw" select="string($loc/hormoneCombined)"/>
    <xsl:variable name="intendedLengthRaw" select="string($loc/hormoneIntendedLength)"/>
    
    <xsl:variable name="use" select="
      if ($useRaw eq '') then '0'
      else if ($useRaw eq 'Yes, in the past')
              then if ($yearsSinceLastUseRaw ne '')
                      then if (number($yearsSinceLastUseRaw) > 5)
                            then '1'
                            else '2'
                      else if ($useYearsRaw eq '')
                              then if ($ageRaw eq '')
                                    then '2'
                                    else if ((number($ageRaw) gt 1) and (number($ageRaw) lt 100))
                                            then if ($patientAge - number($ageRaw) > 5) then '1' else '2'
                                            else '2'
                              else (: use years exist :)
                                  if ($ageRaw eq '')
                                    then '2'
                                    else if ((number($ageRaw) gt 1) and (number($ageRaw) lt 100))
                                          then if ($patientAge - (number($ageRaw) + number($useYearsRaw)) > 5) then '1' else '2'
                                          else '2'
      else if ($useRaw eq 'Yes, currently') then '3'
      else '0'"/>

    <xsl:variable name="combined" select="
      if (($use ne '0') and ($combinedRaw ne ''))
        then if ($combinedRaw eq 'No') then '1'
              else if ($combinedRaw eq 'Yes') then '2'
              else '0'
        else '0'
      "/>

    <xsl:variable name="useYears" select="
      if ((number($useYearsRaw) gt 0) and (number($useYearsRaw) lt 100))
        then string(round-half-to-even(number($useYearsRaw), 1))
        else '-99'
      "/>

    <xsl:variable name="intendedLength" select="
      if ((number($intendedLengthRaw) gt 0) and (number($intendedLengthRaw) lt 100) and ($use eq '3'))
        then string(round-half-to-even(number($intendedLengthRaw), 1))
        else '-99'
      "/>

    <xsl:variable name="yearsSinceLastUse" select="
      if ((number($yearsSinceLastUseRaw) gt 0) and (number($yearsSinceLastUseRaw) lt 100) and ($use = ('1','2')))
        then string(round-half-to-even(number($yearsSinceLastUseRaw), 1))
        else '-99'
      "/>

    <subjectOf2 typeCode="SBJ">
      <clinicalObservation classCode="OBS" moodCode="EVN">
        <code code="135" codeSystemName="UML" displayName="HRT Use"/>
        <statusCode code="{$use}"/>
      </clinicalObservation>
    </subjectOf2>
    <subjectOf2 typeCode="SBJ">
      <clinicalObservation classCode="OBS" moodCode="EVN">
        <code code="136" codeSystemName="UML" displayName="HRT Type"/>
        <statusCode code="{$combined}"/>
      </clinicalObservation>
    </subjectOf2>
    <subjectOf2 typeCode="SBJ">
      <clinicalObservation classCode="OBS" moodCode="EVN">
        <code code="137" codeSystemName="UML" displayName="HRT Length Past">
          <qualifier>
            <value code="{$useYears}"/>
          </qualifier>
        </code>
      </clinicalObservation>
    </subjectOf2>
    <subjectOf2 typeCode="SBJ">
      <clinicalObservation classCode="OBS" moodCode="EVN">
        <code code="138" codeSystemName="UML" displayName="HRT Length Intent">
          <qualifier>
            <value code="{$intendedLength}"/>
          </qualifier>
        </code>
      </clinicalObservation>
    </subjectOf2>
    <subjectOf2 typeCode="SBJ">
      <clinicalObservation classCode="OBS" moodCode="EVN">
        <code code="139" codeSystemName="UML" displayName="HRT Last Use">
          <qualifier>
            <value code="{$yearsSinceLastUse}"/>
          </qualifier>
        </code>
      </clinicalObservation>
    </subjectOf2>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/SocialHx[1]/colonoscopyLast10Years[1]">
    <xsl:if test="string(.) = ('Yes', 'No')">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="C16450" codeSystemName="NCI" displayName="Colonoscopy"/>
          <statusCode code="{if (string(.) eq 'Yes') then 'DuringPast10Years' else 'NotDuringPast10Years'}"/>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/SocialHx[1]/colonPolypLast10Years[1]">
    <xsl:if test="string(.) = ('Yes', 'No')">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="115" codeSystemName="UML" displayName="Colon Polyp-NOS"/>
          <statusCode code="{if (string(.) eq 'Yes') then 'DuringPast10Years' else 'NotDuringPast10Years'}"/>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/SocialHx[1]/RegularAspirinUser[1]">
    <xsl:if test="string(.) = ('Yes', 'No')">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="158" codeSystemName="UML" displayName="Aspirin or NSAID regular use"/>
          <statusCode code="{string(.)}"/>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/SocialHx[1]/RegularIbuprofenUser[1]">
    <xsl:if test="string(.) = ('Yes', 'No')">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="159" codeSystemName="UML" displayName="Ibuprofen regular use"/>
          <statusCode code="{string(.)}"/>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/SocialHx[1]/vigorousPhysicalActivityHoursPerWeek[1]">
    <xsl:if test="(string(.) ne '') and (string(.) ne 'Unknown')">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="141" codeSystemName="UML" displayName="Vigorous Exercise hours per week"/>
          <statusCode code="{
            if (starts-with(.,'More than 4')) then 'GT4'
            else if (starts-with(.,'Between 2-3') or starts-with(.,'Between 3-4')) then 'GT2LE4'
            else if (starts-with(.,'Up to 1') or starts-with(.,'Between 1-2')) then 'GTZeroLE2'
            else 'Zero'}"/>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/SocialHx[1]/numYearsSmokedCigarettes[1]">
    <xsl:if test="(number(.) ge 0)">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="142" codeSystemName="UML" displayName="Cigarette Smoking years">
            <qualifier>
              <value code="{number(.)}"/>
            </qualifier>
          </code>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/SocialHx[1]/numCigarettesPerDay[1]">
    <xsl:if test="(number(.) ge 0)">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="143" codeSystemName="UML" displayName="Cigarettes per day">
            <qualifier>
              <value code="{number(.)}"/>
            </qualifier>
          </code>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>

  <xsl:template match="HraObject[@type='Patient']/SocialHx[1]/vegetableServingsPerDay[1]">
    <xsl:if test="(string(.) ne '') and (string(.) ne 'Unknown')">
      <subjectOf2 typeCode="SBJ">
        <clinicalObservation classCode="OBS" moodCode="EVN">
          <code code="144" codeSystemName="UML" displayName="Vegetable servings per day"/>
            <statusCode code="{
            if (starts-with(.,'5-6') or starts-with(.,'7-10') or starts-with(.,'More than 10')) then 'GE5'
            else 'LT5'}"/>
        </clinicalObservation>
      </subjectOf2>
    </xsl:if>
  </xsl:template>
  
</xsl:stylesheet>
