<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="2.0" extension-element-prefixes="saxon" exclude-result-prefixes="msxsl" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:saxon="http://icl.com/saxon" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
	<xsl:output method="xml" indent="yes"/>
	<xsl:variable name="percent" select="0.01"/>
	<xsl:template match="/">
		<FamilyHistory>
			<risk typeCode="RISK">
				<pedigreeAnalysisResults classCode="OBS" moodCode="RSK">
					<effectiveTime>
						<xsl:attribute name="value">
							<xsl:value-of select="format-date(current-date(), '[Y0001][M01][D01]')"/>
							<xsl:value-of select="format-time(current-time(), '[H01][m01]')"/>
						</xsl:attribute>
					</effectiveTime>
					<methodCode>
						<xsl:attribute name="code">
							<xsl:text>BRCAPRO</xsl:text>
						</xsl:attribute>
						<xsl:attribute name="codeSystemVersion">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BrcaPro_Version"/>
						</xsl:attribute>
						<familyBrcaProbs>
							<xsl:for-each select="FamilyHistory/HraObject">
								<row>
									<xsl:attribute name="id">
										<xsl:value-of select="relativeID"/>
									</xsl:attribute>
									<xsl:attribute name="probCarrier">
										<xsl:if test="RP/BrcaPro_1_2_Mut_Prob != ''">
											<xsl:value-of select="RP/BrcaPro_1_2_Mut_Prob * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probBRCA1Mutation">
										<xsl:if test="RP/BrcaPro_1_Mut_Prob != ''">
											<xsl:value-of select="RP/BrcaPro_1_Mut_Prob * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probBRCA2Mutation">
										<xsl:if test="RP/BrcaPro_2_Mut_Prob != ''">
											<xsl:value-of select="RP/BrcaPro_2_Mut_Prob * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probBothMutation">
	
										<xsl:if test="RP/foo != ''">
											<xsl:value-of select="RP/foo * $percent"/>
										</xsl:if>
									</xsl:attribute>
								</row>
							</xsl:for-each>
						</familyBrcaProbs>
						<lifetimeProbs>
							<row>
								<xsl:attribute name="age">
									<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BM_5Year_Age"/>
								</xsl:attribute>
								<xsl:attribute name="breastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/BrcaPro_5Year_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BrcaPro_5Year_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
								<xsl:attribute name="ovarianCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/BrcaPro_5Year_Ovary != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BrcaPro_5Year_Ovary * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</row>
							<row>
								<xsl:attribute name="age">
									<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BM_Lifetime_Age"/>
								</xsl:attribute>
								<xsl:attribute name="breastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/BrcaPro_Lifetime_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BrcaPro_Lifetime_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
								<xsl:attribute name="ovarianCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/BrcaPro_Lifetime_Ovary != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BrcaPro_Lifetime_Ovary * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</row>
						</lifetimeProbs>
					</methodCode>
					<methodCode>
						<xsl:attribute name="code">
							<xsl:text>MMRPRO</xsl:text>
						</xsl:attribute>
						<xsl:attribute name="codeSystemVersion">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/MmrPro_Version"/>
						</xsl:attribute>
						<familyMMRProbs>
							<xsl:for-each select="FamilyHistory/HraObject">
								<row>
									<xsl:attribute name="id">
										<xsl:value-of select="relativeID"/>
									</xsl:attribute>
									<xsl:attribute name="probCarrier">
										<xsl:if test="RP/MmrPro_1_2_6_Mut_Prob != ''">
											<xsl:value-of select="RP/MmrPro_1_2_6_Mut_Prob * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probMLH1Mutation">
										<xsl:if test="RP/MmrPro_MLH1_Mut_Prob != ''">
											<xsl:value-of select="RP/MmrPro_MLH1_Mut_Prob * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probMSH2Mutation">
										<xsl:if test="RP/MmrPro_MSH2_Mut_Prob != ''">
											<xsl:value-of select="RP/MmrPro_MSH2_Mut_Prob * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probMSH6Mutation">
										<xsl:if test="RP/MmrPro_MSH6_Mut_Prob !=''">
											<xsl:value-of select="RP/MmrPro_MSH6_Mut_Prob * $percent"/>
										</xsl:if>
									</xsl:attribute>
								</row>
							</xsl:for-each>
						</familyMMRProbs>
						<lifetimeMMRProbs>
							<row>
								<xsl:attribute name="age">
									<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BM_5Year_Age"/>
								</xsl:attribute>
								<xsl:attribute name="colorectalCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/MmrPro_5Year_Colon != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/MmrPro_5Year_Colon * $percent"/>
									</xsl:if>
								</xsl:attribute>
								<xsl:attribute name="endometrialCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/MmrPro_5Year_Endometrial != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/MmrPro_5Year_Endometrial * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</row>
							<row>
								<xsl:attribute name="age">
									<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BM_Lifetime_Age"/>
								</xsl:attribute>
								<xsl:attribute name="colorectalCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/MmrPro_Lifetime_Colon != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/MmrPro_Lifetime_Colon * $percent"/>
									</xsl:if>
								</xsl:attribute>
								<xsl:attribute name="endometrialCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/MmrPro_Lifetime_Endometrial != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/MmrPro_Lifetime_Endometrial * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</row>
						</lifetimeMMRProbs>
					</methodCode>
					<methodCode>
						<xsl:attribute name="code">
							<xsl:text>CLAUS</xsl:text>
						</xsl:attribute>
						<xsl:attribute name="codeSystemVersion">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/XXXXXXX"/>
						</xsl:attribute>
						<FiveYear>
							<value>
								<xsl:attribute name="code">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/Claus_5Year_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/Claus_5Year_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</value>
						</FiveYear>
						<Lifetime>
							<value>
								<xsl:attribute name="code">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/Claus_Lifetime_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/Claus_Lifetime_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</value>
						</Lifetime>
						<Table>
							<value>
								<xsl:attribute name="code">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/XXXXXX != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/XXXXXX"/>
									</xsl:if>
								</xsl:attribute>
							</value>
						</Table>
					</methodCode>
					<methodCode>
						<xsl:attribute name="code">
							<xsl:text>GAIL</xsl:text>
						</xsl:attribute>
						<xsl:attribute name="codeSystemVersion">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/XXXXXXX"/>
						</xsl:attribute>
						<FiveYear>
							<value>
								<xsl:attribute name="code">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/Gail_5Year_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/Gail_5Year_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</value>
						</FiveYear>
						<Lifetime>
							<value>
								<xsl:attribute name="code">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/Gail_Lifetime_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/Gail_Lifetime_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</value>
						</Lifetime>
					</methodCode>
					<methodCode>
						<xsl:attribute name="code">
							<xsl:text>MYRIAD</xsl:text>
						</xsl:attribute>
						<xsl:attribute name="codeSystemVersion">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/XXXXXXX"/>
						</xsl:attribute>
						<probGeneticMutation>
							<value>
								<xsl:attribute name="code">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/Myriad_Brca_1_2 != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/Myriad_Brca_1_2 * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</value>
						</probGeneticMutation>
					</methodCode>
					<methodCode>
						<xsl:attribute name="code">
							<xsl:text>TYRER-CUZICK</xsl:text>
						</xsl:attribute>
						<xsl:attribute name="codeSystemVersion">
							<xsl:text>6</xsl:text>
						</xsl:attribute>
						<familyTCProbs>
							<xsl:for-each select="FamilyHistory/HraObject">
								<row>
									<xsl:attribute name="id">
										<xsl:value-of select="relativeID"/>
									</xsl:attribute>
									<xsl:attribute name="probCarrier">
										<xsl:if test="RP/TyrerCuzick_Brca_1_2 != ''">
											<xsl:value-of select="RP/TyrerCuzick_Brca_1_2 * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probBRCA1Mutation">
										<xsl:if test="RP/TyrerCuzick_Brca_1 != ''">
											<xsl:value-of select="RP/TyrerCuzick_Brca_1 * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probBRCA2Mutation">
										<xsl:if test="RP/TyrerCuzick_Brca_2 != ''">
											<xsl:value-of select="RP/TyrerCuzick_Brca_2 * $percent"/>
										</xsl:if>
									</xsl:attribute>
								</row>
							</xsl:for-each>
						</familyTCProbs>
						<lifetimeTCProbs>
							<row>
								<xsl:attribute name="id">
									<xsl:text>1</xsl:text>
								</xsl:attribute>
								<xsl:attribute name="age">
									<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BM_5Year_Age"/>
								</xsl:attribute>
								<xsl:attribute name="breastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_5Year_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_5Year_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
								<xsl:attribute name="populationBreastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_5Year_populationRisk != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_5Year_populationRisk * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</row>
							<row>
								<xsl:attribute name="id">
									<xsl:text>1</xsl:text>
								</xsl:attribute>
								<xsl:attribute name="age">
									<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BM_Lifetime_Age"/>
								</xsl:attribute>
								<xsl:attribute name="breastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_Lifetime_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_Lifetime_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
								<xsl:attribute name="populationBreastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_Lifetime_populationRisk != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_Lifetime_populationRisk * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</row>
						</lifetimeTCProbs>
					</methodCode>
					<methodCode>
						<xsl:attribute name="code">
							<xsl:text>TYRER-CUZICK7</xsl:text>
						</xsl:attribute>
						<xsl:attribute name="codeSystemVersion">
							<xsl:text>7</xsl:text>
						</xsl:attribute>
						<familyTCProbs>
							<xsl:for-each select="FamilyHistory/HraObject">
								<row>
									<xsl:attribute name="id">
										<xsl:value-of select="relativeID"/>
									</xsl:attribute>
									<xsl:attribute name="probCarrier">
										<xsl:if test="RP/TyrerCuzick_v7_Brca_1_2 != ''">
											<xsl:value-of select="RP/TyrerCuzick_v7_Brca_1_2 * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probBRCA1Mutation">
										<xsl:if test="RP/TyrerCuzick_v7_Brca_1 != ''">
											<xsl:value-of select="RP/TyrerCuzick_v7_Brca_1 * $percent"/>
										</xsl:if>
									</xsl:attribute>
									<xsl:attribute name="probBRCA2Mutation">
										<xsl:if test="RP/TyrerCuzick_v7_Brca_2 != ''">
											<xsl:value-of select="RP/TyrerCuzick_v7_Brca_2 * $percent"/>
										</xsl:if>
									</xsl:attribute>
								</row>
							</xsl:for-each>
						</familyTCProbs>
						<lifetimeTCProbs>
							<row>
								<xsl:attribute name="id">
									<xsl:text>1</xsl:text>
								</xsl:attribute>
								<xsl:attribute name="age">
									<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BM_5Year_Age"/>
								</xsl:attribute>
								<xsl:attribute name="breastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_v7_5Year_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_v7_5Year_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
								<xsl:attribute name="populationBreastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_v7_5Year_populationRisk != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_v7_5Year_populationRisk * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</row>
							<row>
								<xsl:attribute name="id">
									<xsl:text>1</xsl:text>
								</xsl:attribute>
								<xsl:attribute name="age">
									<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/BM_Lifetime_Age"/>
								</xsl:attribute>
								<xsl:attribute name="breastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_v7_Lifetime_Breast != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_v7_Lifetime_Breast * $percent"/>
									</xsl:if>
								</xsl:attribute>
								<xsl:attribute name="populationBreastCancerRisk">
									<xsl:if test="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_v7_Lifetime_populationRisk != ''">
										<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/TyrerCuzick_v7_Lifetime_populationRisk * $percent"/>
									</xsl:if>
								</xsl:attribute>
							</row>
						</lifetimeTCProbs>
					</methodCode>
				</pedigreeAnalysisResults>
			</risk>
			<Penrad>
				<unitnum>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/unitnum"/>
						</xsl:attribute>
					</value>
				</unitnum>
				<apptID>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/apptid"/>
						</xsl:attribute>
					</value>
				</apptID>
				<currentlyMenstruating>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/currentlyMenstruating"/>
						</xsl:attribute>
					</value>
				</currentlyMenstruating>
				<stoppedMenstruating>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/stoppedMenstruating"/>
						</xsl:attribute>
					</value>
				</stoppedMenstruating>
				<hadHysterectomy>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/hadHysterectomy"/>
						</xsl:attribute>
					</value>
				</hadHysterectomy>
				<hysterectomyAge>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/hysterectomyAge"/>
						</xsl:attribute>
					</value>
				</hysterectomyAge>
				<bothOvariesRemoved>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/bothOvariesRemoved"/>
						</xsl:attribute>
					</value>
				</bothOvariesRemoved>
				<heightInches>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/PhysicalExam/heightInches"/>
						</xsl:attribute>
					</value>
				</heightInches>
				<weightPounds>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/PhysicalExam/weightPounds"/>
						</xsl:attribute>
					</value>
				</weightPounds>
				<hasSmoked>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/SocialHx/hasSmoked"/>
						</xsl:attribute>
					</value>
				</hasSmoked>
				<smokingPacksPerDay>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/SocialHx/smokingPacksPerDay"/>
						</xsl:attribute>
					</value>
				</smokingPacksPerDay>
				<numYearsSmokedCigarettes>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/SocialHx/numYearsSmokedCigarettes"/>
						</xsl:attribute>
					</value>
				</numYearsSmokedCigarettes>
				<smokingWhenQuit>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/SocialHx/smokingWhenQuit"/>
						</xsl:attribute>
					</value>
				</smokingWhenQuit>
				<birthControlUse>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/birthControlUse"/>
						</xsl:attribute>
					</value>
				</birthControlUse>
				<birthControlYears>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/birthControlYears"/>
						</xsl:attribute>
					</value>
				</birthControlYears>
				<birthControlContinuously>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/birthControlContinuously"/>
						</xsl:attribute>
					</value>
				</birthControlContinuously>
				<hormoneUse>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/hormoneUse"/>
						</xsl:attribute>
					</value>
				</hormoneUse>
				<hormoneUseYears>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/hormoneUseYears"/>
						</xsl:attribute>
					</value>
				</hormoneUseYears>
				<hormoneIntendedLength>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/hormoneIntendedLength"/>
						</xsl:attribute>
					</value>
				</hormoneIntendedLength>
				<hormoneYearsSinceLastUse>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/hormoneYearsSinceLastUse"/>
						</xsl:attribute>
					</value>
				</hormoneYearsSinceLastUse>
				<hormoneCombined>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/hormoneCombined"/>
						</xsl:attribute>
					</value>
				</hormoneCombined>
				<takenFertilityDrugs>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/takenFertilityDrugs"/>
						</xsl:attribute>
					</value>
				</takenFertilityDrugs>
				<currentlyPregnant>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/currentlyPregnant"/>
						</xsl:attribute>
					</value>
				</currentlyPregnant>
				<breastImplants>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/MammographyHx/breastImplants"/>
						</xsl:attribute>
					</value>
				</breastImplants>
				<takenTamoxifen>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/MedHx/chemoprevention/takenTamoxifen"/>
						</xsl:attribute>
					</value>
				</takenTamoxifen>
				<takenRaloxifene>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/MedHx/chemoprevention/takenRaloxifene"/>
						</xsl:attribute>
					</value>
				</takenRaloxifene>
				<emailAddress>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/emailAddress"/>
						</xsl:attribute>
					</value>
				</emailAddress>
				<numChildren>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/ObGynHx/numChildren"/>
						</xsl:attribute>
					</value>
				</numChildren>
				<riskCode>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/riskCode"/>
						</xsl:attribute>
					</value>
				</riskCode>
				<riskText>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/riskText"/>
						</xsl:attribute>
					</value>
				</riskText>
				<riskFormattedText>
					<value>
						<xsl:attribute name="code">
							<xsl:value-of select="FamilyHistory/HraObject[relativeID='1']/RP/riskFormattedText"/>
						</xsl:attribute>
					</value>
				</riskFormattedText>
			</Penrad>
		</FamilyHistory>
	</xsl:template>
</xsl:stylesheet>
