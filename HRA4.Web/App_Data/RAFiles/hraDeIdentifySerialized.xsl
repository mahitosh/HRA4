<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:hra="http://www.hughesriskapps.com"
  xmlns:xd="http://www.oxygenxml.com/ns/doc/xsl">

  <xsl:output indent="yes"/>
  <xsl:strip-space elements="*"/>  

  <xd:doc scope="stylesheet">
    <xd:desc>
      <xd:p><xd:b>Created on:</xd:b> June 10, 2015</xd:p>
      <xd:p><xd:b>Author:</xd:b> Phil Bosinoff, Deep Code Consulting</xd:p>
      <xd:p><xd:b>Updated:</xd:b> October 2, 2015</xd:p>
      <xd:p>Ages and ageDiagnoses never exceed 89</xd:p>
    </xd:desc>
  </xd:doc>
   
  <!-- This is an identity copy template; it performs a deep copy, recursively  -->
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>

  <!-- Remove Private Health Information -->
  <!-- Removes date of birth, among many other things; ages are retained, except those > 89 changed to 89  -->
  <xsl:template match="*[local-name() = 'address1']"/>
  <xsl:template match="*[local-name() = 'address2']"/>
  <xsl:template match="*[local-name() = 'cellphone']"/>
  <xsl:template match="*[local-name() = 'city']"/>
  <xsl:template match="*[local-name() = 'comment']"/>
  <xsl:template match="*[local-name() = 'contactcellphone']"/>
  <xsl:template match="*[local-name() = 'contacthomephone']"/>
  <xsl:template match="*[local-name() = 'contactname']"/>
  <xsl:template match="*[local-name() = 'contactworkphone']"/>
  <xsl:template match="*[local-name() = 'country']"/>
  <xsl:template match="*[local-name() = 'Nationality']"/>
  
  <xsl:template match="*[local-name() = 'dob']"/>
  <xsl:template match="*[local-name() = 'dateOfDeath']"/>
  
  <xsl:template match="*[local-name() = 'emailAddress']"/>
  <xsl:template match="*[local-name() = 'firstName']"/>
  <xsl:template match="*[local-name() = 'homephone']"/>
  <xsl:template match="*[local-name() = 'lastName']"/>
  <xsl:template match="*[local-name() = 'maidenName']"/>
  <xsl:template match="*[local-name() = 'middleName']"/>
  <xsl:template match="*[local-name() = 'name']"/>
  <xsl:template match="*[local-name() = 'occupation']"/>
  <xsl:template match="*[local-name() = 'religion']"/>
  <xsl:template match="*[local-name() = 'state']"/>
  <xsl:template match="*[local-name() = 'suffix']"/>
  <xsl:template match="*[local-name() = 'title']"/>
  <xsl:template match="*[local-name() = 'workphone']"/>
  <xsl:template match="*[local-name() = 'zip']"/>
  
  <xsl:template match="*[local-name() = 'Providers']"/>

  <xsl:template match="*[local-name() = 'apptdatetime']"/>
  <xsl:template match="*[local-name() = 'apptid']"/>
  <xsl:template match="*[local-name() = 'family_comment']"/>
  <xsl:template match="*[local-name() = 'follupSatus']"/>
  <xsl:template match="*[local-name() = 'unitnum']"/>
  <xsl:template match="*[local-name() = 'ageDiagnosis'][text()]">
    <xsl:copy>
      <xsl:value-of select="if (number(.) = .) then min((., 89)) else ."/>
    </xsl:copy>
  </xsl:template>
  <xsl:template match="*[local-name() = 'age'][text()]">
    <xsl:copy>
      <xsl:value-of select="if (number(.) = .) then min((., 89)) else ."/>
    </xsl:copy>
  </xsl:template>
  
</xsl:stylesheet>
