<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <!--xmlns="urn:hl7-org:v3" xmlns:voc="urn:hl7-org:v3/voc" 
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="urn:hl7-org:v3 CDA.xsd">-->

  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes" omit-xml-declaration="no"/>
  <xsl:strip-space elements="*"/>

  <!--Following can be overidden by transformer invocation with parameter set-->
  <xsl:param name="localBaseUri" select="'file:/./App_Data/RAFiles/'"/>

  <!-- This is an identity copy template; it performs a deep copy, recursively  -->
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="HraObject">
    <xsl:element name="{@type}">
      <xsl:choose>
        <xsl:when test="count(@*) > 0">
          <xsl:for-each select="./@*">
            <xsl:if test="name() != 'type'">
              <xsl:attribute name="{local-name()}">
                <xsl:value-of select="."/>
              </xsl:attribute>
            </xsl:if>
          </xsl:for-each>
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="count(*) = 0">
          <xsl:value-of select="."/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:apply-templates select="./*"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:element>
  </xsl:template>

  <xsl:template match="HraObject[@type='Race']">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="HraObject[@type='Nation']">
    <xsl:apply-templates />
  </xsl:template>

  <!--<xsl:include href="remove_namespaces.xsl"/>-->

</xsl:stylesheet>
