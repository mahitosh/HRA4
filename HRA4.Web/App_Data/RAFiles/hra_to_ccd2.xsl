<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes"
                exclude-result-prefixes="msxsl dt" extension-element-prefixes="user" xmlns:user="urn:user">
  <!--xmlns:date="http://exslt.org/dates-and-times"  extension-element-prefixes="date">-->

  <!--xmlns="urn:hl7-org:v3" xmlns:voc="urn:hl7-org:v3/voc" 
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="urn:hl7-org:v3 CDA.xsd">-->

  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/> <!--omit-xml-declaration="no"/>-->
  <xsl:strip-space elements="*"/>
  
  <msxsl:script language="C#" implements-prefix="user">
    <![CDATA[
    public string dateNow()
    {
        return(System.DateTime.Now.ToString("yyyyMMdd"));
    }
    public string timeNow()
    {
        return(System.DateTime.Now.ToString("hhmm"));
    }
    public string dateTimeNow()
    {
        return(System.DateTime.Now.ToString("yyyyMMdd+hhmm"));
    }
    public string xsdDateTime(string str)
    {
        System.DateTime dt;
        if (System.DateTime.TryParse(str, out dt) == true)     
          return(dt.ToString("yyyyMMdd+hhmm"));
        else
          return(String.Empty);
    }
]]>
  </msxsl:script>
  
  <!--Following can be overidden by transformer invocation with parameter set-->
  <xsl:param name="localBaseUri" select="'file:/./App_Data/RAFiles/'"/>
  
  <xsl:template match="Patient" mode="patientRole">
    <patientRole>
      <id extension="996-756-495" root="2.16.840.1.113883.19.5"/>
      <patient>
        <name>
          <given>
            <xsl:value-of select="firstName"/>
          </given>
          <family>
            <xsl:value-of select="lastName"/>
          </family>
          <suffix></suffix>
        </name>
        <xsl:if test="gender='Male'">
          <administrativeGenderCode code="M" codeSystem="2.16.840.1.113883.5.1"/>
        </xsl:if>
        <xsl:if test="gender='Female'">
          <administrativeGenderCode code="F" codeSystem="2.16.840.1.113883.5.1"/>
        </xsl:if>
        <xsl:element name="birthTime">
          <xsl:attribute name="value">
            <xsl:value-of select="user:xsdDateTime(dob)"/>
          </xsl:attribute>
        </xsl:element>
      </patient>
      <providerOrganization>
        <id root="2.16.840.1.113883.19.5"/>
        <name>Good Health Clinic</name>
      </providerOrganization>
    </patientRole>
  </xsl:template>
  <xsl:template match="*" mode="AUTHOR">
    <author>
      <time>
        <xsl:attribute name="value">
          <xsl:value-of select="user:dateTimeNow()"/>
        </xsl:attribute>
      </time>
      <assignedAuthor>
        <id/>
        <addr/>
        <telecom/>
        <assignedPerson>
          <name>Staff</name>
        </assignedPerson>
        <representedOrganization>
          <name>Hughes Risk Apps</name>
          <telecom/>
          <addr/>
        </representedOrganization>
      </assignedAuthor>
    </author>
  </xsl:template>
  
  <xsl:template match="*" mode="DOC">
    <documentationOf typeCode="DOC">
      <serviceEvent classCode="PCPR">
        <!--<effectiveTime>
          <low value="20020601"/>
          <high value="20101026"/>
        </effectiveTime>-->
        <xsl:for-each select="Provider">
          <performer typeCode="PRF">
            <templateId root="2.16.840.1.113883.3.88.11.83.4" assigningAuthorityName="HITSP C83"/>
            <templateId root="1.3.6.1.4.1.19376.1.5.3.1.2.3" assigningAuthorityName="IHE PCC"/>
            <functionCode code="PP" codeSystem="2.16.840.1.113883.12.443" codeSystemName="HL7 Provider Role">
              <xsl:attribute name="displayName">
                <xsl:value-of select="defaultRole"/>
              </xsl:attribute>
              <originalText>
                <xsl:value-of select="defaultRole"/>
              </originalText>
            </functionCode>
            <!--<time>
              <low value="20020716"/>
              <high value="20070915"/>
            </time>-->
            <assignedEntity>
              <!--<id extension="Unknown" root="2.16.840.1.113883.4.6" assigningAuthorityName="NPI"/>
              <id extension="PseudoMD-1" root="2.16.840.1.113883.3.72.5.2" assigningAuthorityName="NIST Healthcare Testing Laboratory"/>
              <code code="200000000X" displayName="Allopathic and Osteopathic Physicians" codeSystemName="Provider Codes" codeSystem="2.16.840.1.113883.6.101"/>-->
              <addr>
                <xsl:value-of select="concat(address1, ' ', address2, ', ', city, ', ', state, ', ', country, ' ', zipcode)"/>
              </addr>
              <telecom>
                <xsl:value-of select="phone"/>
              </telecom>
              <assignedPerson>
                <name>
                  <prefix>
                    <xsl:value-of select="title"/>
                  </prefix>
                  <given>
                    <xsl:value-of select="firstName"/>
                  </given>
                  <family>
                    <xsl:value-of select="lastName"/>
                  </family>
                </name>
              </assignedPerson>
              <representedOrganization>
                <id root="2.16.840.1.113883.3.72.5"/>
                <name>
                  <xsl:value-of select="Institution"/>
                </name>
                <telecom/>
                <addr/>
              </representedOrganization>
            </assignedEntity>
          </performer>
        </xsl:for-each>
      </serviceEvent>
    </documentationOf>
  </xsl:template>

  <xsl:template match="/" >
    <ClinicalDocument xmlns="urn:hl7-org:v3" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                      xsi:schemaLocation="urn:hl7-org:v3 http://xreg2.nist.gov:8080/hitspValidation/schema/cdar2c32/infrastructure/cda/C32_CDA.xsd">
      <!--xmlns:sdtc="urn:hl7-org:sdtc" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
                      xsi:schemaLocation="urn:hl7-org:v3 http://xreg2.nist.gov:8080/hitspValidation/schema/cdar2c32/infrastructure/cda/C32_CDA.xsd">-->
      <![CDATA[<!-- ******************************************************** CDA Header ******************************************************** -->]]>
      <typeId root="2.16.840.1.113883.1.3" extension="POCD_HD000040"/>
      <templateId root="2.16.840.1.113883.10.20.1"/>
      <![CDATA[<!--CCD v1.0 Templates Root-->]]>
      <id root="db734647-fc99-424c-a864-7e3cda82e703"/>
      <code code="34133-9" codeSystem="2.16.840.1.113883.6.1" displayName="Summarization of episode note"/>
      <title>Good Health Clinic Continuity of Care Document</title>
      <effectiveTime>
        <xsl:attribute name="value">
          <xsl:value-of select="user:dateTimeNow()"/>
        </xsl:attribute>
      </effectiveTime>
      <confidentialityCode code="N" codeSystem="2.16.840.1.113883.5.25"/>
      <languageCode code="en-US"/>
      <recordTarget>
        <xsl:apply-templates select="//Patient" mode="patientRole"/>
      </recordTarget>
      <xsl:apply-templates select="//Patient/Providers" mode="DOC"/>
    </ClinicalDocument>
  </xsl:template>

</xsl:stylesheet>
