using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Serialization;

using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.Utilities;
using RiskApps3.View;
using RiskApps3.Model.PatientRecord.FHx;
using System.Data;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Model.PatientRecord.Labs;
using RiskApps3.Model.Clinic;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using RiskApps3.Model.PatientRecord.Risk;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Collections.Specialized;

using EvoPdf;       // jdg 8/31/15 ported from web tablet...
using Foxit.PDF.Printing;
using System.Web;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract(IsReference=true)]
    public class Patient : Person
    {
        /**************************************************************************************************/
        [DataMember] public string unitnum;
        [DataMember] public int apptid;
        [DataMember] public DateTime apptdatetime;
        [DataMember] [HraAttribute] public string family_comment;
        [DataMember] public FamilyHistory FHx;
        [DataMember] public MammographyHx MammographyHx;
        [DataMember] public MedicationHx MedHx;
        [DataMember] public SocialHistory SocialHx;
        [DataMember] public PhysicalExamination PhysicalExam;
        [DataMember] public ObGynHistory ObGynHx;
        // bd - deprecated public Diet diet;
        [DataMember] public ProcedureHx procedureHx;
        [DataMember] public BreastImagingHx breastImagingHx;
        [DataMember] public LabsHx labsHx;
        [DataMember] public FollowupStatus follupSatus;
        [DataMember] public TransvaginalImagingHx transvaginalImagingHx;
        [DataMember] public TaskList Tasks;
        [DataMember] public ProviderList Providers;
        [DataMember] public CDSBreastOvary cdsBreastOvary;
        [DataMember] public GUIPreferenceList guiPreferences;

        [DataMember] public PediatricConsiderations PediatricCDS;

        [DataMember] private string summary;
        [DataMember] public string geneticTesting = "";
        [DataMember] public string geneticTestingResult = "";
        [DataMember] public int riskFactorsConfirmed = -1;

        [DataMember] public SurveyResponseList SurveyReponses;
        private bool bSilent = false;
        private string toolsPath = "";
        private string strServiceBinding = "";

        public FileInfo file { get;set;}
        /**************************************************************************************************/
        public void summarize()
        {
            DocumentGenerator3 docGen = new DocumentGenerator3();
            summary = docGen.GenerateHtmlDocument(1, unitnum, apptid);
        }
        /**************************************************************************************************/
        public string Patient_unitnum
        {
            get
            {
                return unitnum;
            }
            set
            {
                if (unitnum != value)
                {
                    unitnum = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("unitnum"));
                    SignalModelChanged(args);
                }
            }
        }

        /**************************************************************************************************/
        public string Patient_Comment
        {
            get
            {
                return family_comment;
            }
            set
            {
                if (family_comment != value)
                {
                    family_comment = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("family_comment"));
                    SignalModelChanged(args);
                }
            }
        }

        /**************************************************************************************************/
        public string Patient_geneticTesting
        {
            get
            {
                return geneticTesting;
            }
            set
            {
                if (geneticTesting != value)
                {
                    geneticTesting = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("geneticTesting"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string Patient_geneticTestingResult
        {
            get
            {
                return geneticTestingResult;
            }
            set
            {
                if (geneticTestingResult != value)
                {
                    geneticTestingResult = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("geneticTestingResult"));
                    SignalModelChanged(args);
                }
            }
        }

        /**************************************************************************************************/
        public int Patient_riskFactorsConfirmed
        {
            get
            {
                return riskFactorsConfirmed;
            }
            set
            {
                if (riskFactorsConfirmed != value)
                {
                    riskFactorsConfirmed = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("riskFactorsConfirmed"));
                    SignalModelChanged(args);
                }
            }
        }

        /**************************************************************************************************/

        public Patient() { } // Default constructor for serialization

        public Patient(string p_unitnum)
        {
            unitnum = p_unitnum;

            relativeID = 1;
            relationship = "Self";

            FHx = new FamilyHistory(this);
            owningFHx = FHx;

            MedHx = new MedicationHx(this);
            SocialHx = new SocialHistory(this);
            PhysicalExam = new PhysicalExamination(this);
            ObGynHx = new ObGynHistory(this);
            // deprecated diet = new Diet(this);
            procedureHx = new ProcedureHx(this);
            breastImagingHx = new BreastImagingHx(this);
            transvaginalImagingHx = new TransvaginalImagingHx(this);
            labsHx = new LabsHx(this);
            follupSatus = new FollowupStatus(unitnum, apptid);
            Tasks = new TaskList(this);
            Providers = new ProviderList(this);
            cdsBreastOvary = new CDSBreastOvary(this);
            guiPreferences = new GUIPreferenceList(this);
            PediatricCDS = new PediatricConsiderations(this);
            SurveyReponses = new SurveyResponseList(this);
            MammographyHx = new MammographyHx(this);
        }
        public void ReleaseProband(object view)
        {
            FHx.ReleaseListeners(view);
            MedHx.ReleaseListeners(view);
            SocialHx.ReleaseListeners(view);
            PhysicalExam.ReleaseListeners(view);
            ObGynHx.ReleaseListeners(view);
            //diet.ReleaseListeners(view);
            procedureHx.ReleaseListeners(view);
            breastImagingHx.ReleaseListeners(view);
            labsHx.ReleaseListeners(view);
            follupSatus.ReleaseListeners(view);
            transvaginalImagingHx.ReleaseListeners(view);
            Tasks.ReleaseListeners(view);
            cdsBreastOvary.ReleaseListeners(view);
            Providers.ReleaseListeners(view);
            guiPreferences.ReleaseListeners(view);
            PediatricCDS.ReleaseListeners(view);
            SurveyReponses.ReleaseListeners(view);
            MammographyHx.ReleaseListeners(view);
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/
        public override void  LoadFullObject()
        {
 	        base.LoadFullObject();

            ParameterCollection pc = new ParameterCollection("unitnum", unitnum);
            pc.Add("apptid", apptid);
            DoLoadWithSpAndParams("sp_3_LoadPatient", pc);

            FHx.LoadFullList();
            MedHx.LoadFullObject();
            SocialHx.LoadFullObject();
            PhysicalExam.LoadFullObject();
            ObGynHx.LoadFullObject();
            procedureHx.LoadFullObject();
            breastImagingHx.LoadFullList();
            labsHx.LoadFullList();
            follupSatus.LoadFullObject();
            transvaginalImagingHx.LoadFullList();
            Tasks.LoadFullList();
            cdsBreastOvary.LoadFullObject();
            Providers.LoadFullList();
            guiPreferences.LoadFullList();
            PediatricCDS.LoadFullList();
            SurveyReponses.LoadFullList();
        }
        /**************************************************************************************************/
        public override void PersistFullObject(HraModelChangedEventArgs e)
        {
            base.PersistFullObject(e);
            FHx.proband = this;
            FHx.PersistFullList(e);
            MedHx.theProband = this;
            MedHx.PersistFullObject(e);
            SocialHx.patientOwning = this;
            SocialHx.PersistFullObject(e);
            PhysicalExam.patientOwning = this;
            PhysicalExam.PersistFullObject(e);
            ObGynHx.patientOwning = this;
            ObGynHx.PersistFullObject(e);
            procedureHx.theProband = this;
            procedureHx.PersistFullObject(e);
            breastImagingHx.OwningPatient = this;
            breastImagingHx.PersistFullList(e);
            labsHx.OwningPatient = this;
            labsHx.PersistFullList(e);
            if (follupSatus != null)
            {
                follupSatus.unitnum = this.unitnum;
                follupSatus.apptid = this.apptid;
                follupSatus.PersistFullObject(e);
            }
            transvaginalImagingHx.OwningPatient = this;
            transvaginalImagingHx.PersistFullList(e);
            Tasks.OwningPatient = this;
            Tasks.PersistFullList(e);
            if (cdsBreastOvary != null)
            {
                cdsBreastOvary.patientOwning = this;
                cdsBreastOvary.PersistFullObject(e);
            }
            if (Providers != null)
            {
                Providers.proband = this;
                Providers.PersistFullList(e);
            } 
            guiPreferences.OwningPatient = this;
            guiPreferences.PersistFullList(e);
            PediatricCDS.OwningPatient = this;
            PediatricCDS.PersistFullList(e);
            SurveyReponses.OwningPatient = this;
            foreach (SurveyResponse sr in SurveyReponses)
            {
                sr.owningPatient = this;
            }
            SurveyReponses.PersistFullList(e);
        }
        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", unitnum);
            pc.Add("apptid", apptid);
            DoLoadWithSpAndParams("sp_3_LoadPatient", pc);
        }

        public void RunAutomation()     // jdg 8/31/15 ported from SimpleInfoScreen per Brian
        {


            SqlDataReader reader = null;
            try 
            {
            RiskApps3.Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection("unitnum", unitnum);
            RiskApps3.Utilities.BCDB2.Instance.ExecuteNonQueryWithParams("DELETE FROM tblPrintQueue WHERE unitnum=@unitnum AND printed IS NULL", pc);

            pc = new RiskApps3.Utilities.ParameterCollection("apptid", apptid);
            reader = RiskApps3.Utilities.BCDB2.Instance.ExecuteReaderSPWithParams("sp_AutomationHtmlDocsToPrint", pc);

            while (reader.Read())
            {
                int templateId = 0;
                string printer = "";
                string documentType = "";
                string saveLocation = "";

                if (reader.IsDBNull(0) == false)
                {
                    templateId = reader.GetInt32(0);
                }
                if (reader.IsDBNull(1) == false)
                {
                    printer = reader.GetString(1);
                }
                if (reader.IsDBNull(2) == false)
                {
                    documentType = reader.GetString(2);
                }
                if (reader.IsDBNull(3) == false)
                {
                    if(HttpContext.Current!=null)
                        saveLocation = HttpContext.Current.Server.MapPath(reader.GetString(3));
                    else
                    saveLocation = reader.GetString(3);
                }

                if (string.IsNullOrEmpty(printer)==false)
                {
                    if (printer.ToUpper() == "BATCH")
                    {
                        RiskApps3.Utilities.ParameterCollection pc2 = new RiskApps3.Utilities.ParameterCollection();
                        pc2.Add("apptid", apptid);
                        pc2.Add("dateTime", DateTime.Now);
                        pc2.Add("templateID", templateId);
                        pc2.Add("unitnum", unitnum);
                        pc2.Add("documentType", documentType);

                        string sqlStr =
                            "INSERT INTO tblPrintQueue (apptID, created, documentTemplateID, unitnum, automationPrint, automationSave, documentType) VALUES (@apptid, @dateTime, @templateID, @unitnum, 0, 0, @documentType)";
                        RiskApps3.Utilities.BCDB2.Instance.ExecuteNonQueryWithParams(sqlStr, pc2);
                    }
                    else
                    {
                        DocumentTemplate dt = MakeDocument(this, templateId, String.Empty);
                        FileInfo fInfo;
                        if (printer.ToUpper().StartsWith("SAVE AS"))
                        {
                            if (printer.ToUpper() == "SAVE AS PDF")
                            {
                                fInfo = dt.CalculateFileName(name,
                                                    apptdatetime.ToShortDateString().Replace("/", "-"),
                                                    apptid,
                                                    unitnum,
                                                    "pdf", saveLocation);

                                try
                                {
                                    DocumentTemplate.ConvertToPdf(dt.htmlText, fInfo.FullName);  // brian's new static class 8/31/15
                                    if (Configurator.getNodeValue("globals", "SurveyDemo").ToUpper() == "TRUE")
                                    {
                                        if (HttpContext.Current == null) // Silicus: Added check so that the below line is executed only in windows mode.
                                        Process.Start(fInfo.FullName);
                                        file = fInfo;
                                    }
                               
                                    // Do we need to transmit this file somewhere?  This call checks the interface table (lkpInterfaceDefinitions),
                                    // and ships out the file, if needed, no-op otherwise.  
                                    // This is starting out as a PACS project, but could morph into other interfaces as well.  jdg 7/27/15
                                    RiskApps3.Utilities.InterfaceUtils.send2PACS(dt, this, fInfo.FullName, apptid);
                                }
                                catch (Exception e)
                                {
                                    Logger.Instance.WriteToLog("MakeDocumentWorker: Error when converting to PDF or bubbled up from send2PACS from appt# " + apptid.ToString() + ", filename: " + fInfo.FullName + "; underlying error = " + e.Message);
                                }
                            }
                            else
                            {
                                try
                                {
                                    fInfo = dt.CalculateFileName(name,
                                                    apptdatetime.ToShortDateString().Replace("/", "-"),
                                                    apptid,
                                                    unitnum,
                                                    "html", saveLocation);

                                    File.WriteAllText(fInfo.FullName, dt.htmlText);
                                    if (Configurator.getNodeValue("globals", "SurveyDemo").ToUpper() == "TRUE")
                                    {
                                        Process.Start(fInfo.FullName);
                                       
                                    }
                                }
                                catch (Exception e)
                                {
                                    Logger.Instance.WriteToLog(e.ToString());
                                }
                            }
                        }
                        else
                        {
                            if (printer.ToUpper() == "POWERSCRIBE")
                            {
                                // jdg 7/7/2015
                                int interfaceId = -1;
                                string interfaceSql = "select InterfaceId from lkp_AutomationDocuments where documentTemplateID=" + dt.documentTemplateID;
                                reader = RiskApps3.Utilities.BCDB2.Instance.ExecuteReader(interfaceSql);

                                while (reader.Read())
                                {
                                    if (reader.IsDBNull(0) == false)
                                    {
                                        interfaceId = reader.GetInt32(0);
                                    }
                                }
                                if (interfaceId > 0)
                                {
                                    // note:  this method returns a bool success flag... use it to win cool prizes
                                    RiskApps3.Utilities.InterfaceUtils.sendPowerscribe(dt);
                                }
                            }
                            else
                            {
                                if (printer == "*")
                                {
                                    PrinterSettings settings = new PrinterSettings();
                                    printer = settings.PrinterName;
                                }
                                DocumentTemplate.Print(dt.htmlText, printer);
                            }
                        }
                    }
                }
            }
            reader.Close();

            ///***********************  Mark as printed to complete automation   ********************/
            try
            {
                RiskApps3.Utilities.BCDB2.Instance.ExecuteNonQuery("UPDATE tblRiskData SET printed = 1 WHERE apptid = " + apptid.ToString());
            }
            catch (Exception e)
            {
                // If this error occurs, you would never have got here in the first place, seems to me.
                Logger.Instance.WriteToLog("MakeDocumentWorker:  Could not mark appointment #" + apptid.ToString() + " as printed.  Underlying error = " + e.ToString());
            }

        }
        catch (Exception e)
        {
            if (reader != null)
                reader.Close();
        }
    }

        public DocumentTemplate MakeDocument(Patient proband, int templateID, string templatesRoot)     // ported from SimpleInfoScreen jdg 8/31/15 per Brian
        {
            DocumentTemplate dt = new DocumentTemplate();
            dt.documentTemplateID = templateID;
            dt.SetPatient(proband);
            dt.BackgroundLoadWork();
            if (!String.IsNullOrEmpty(templatesRoot))
            {
                dt.htmlPath = Path.Combine(templatesRoot, Path.GetFileName(dt.htmlPath));
            }
            dt.OpenHTML();
            dt.ProcessDocument();

            return dt;
        }

        public void RecalculateRisk(bool bSilentRiskCalcs, string strToolsPath, string binding)      // overloaded jdg 9/1/15 web tablet calls this and the context is different from the thick client
        {
            bSilent = bSilentRiskCalcs;
            if (!String.IsNullOrEmpty(strToolsPath)) toolsPath = strToolsPath;
            if (!String.IsNullOrEmpty(binding)) strServiceBinding = binding;
            RecalculateRisk();
        }

        public void RecalculateRisk()
        {
            //Assembly.LoadFrom("C:\\HRA\\Dev\\RiskAppCoreProjects\\RiskClinicApp\\bin\\Debug\\RiskAppCore.dll");
            

            if (Configurator.useAggregatorService())
            {

                //new way
                //Serialize the FH
                this.LoadFullObject(); //needed to ensure patient object is complete, including ObGynHx, etc.
                RiskApps3.Model.PatientRecord.FHx.FamilyHistory theFH = this.owningFHx;
                
                
                bool dcis_found = false;
                bool dcisAsCancer = true;
                foreach (Person p in this.FHx)
                {
                    foreach (ClincalObservation co in p.PMH.Observations)
                    {
                        if (string.Compare(co.riskMeaning, "DCIS", true) == 0)
                        {
                            dcis_found = true;
                        }
                    }
                }

                if (dcis_found  && !bSilent)    // added silent mode jdg 9/1/15
                {
                    DialogResult dr = MessageBox.Show("Would you like to consider DCIS a cancer diagnosis for BRCAPRO and Tyrer-Cuzick model calculations?  DCIS WILL be considered as cancer for use with the Myriad, Claus and Gail models.", "BRCAPRO Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                    {
                        dcisAsCancer = false;
                    }
                }
                else
                {
                    if (dcis_found && bSilent)
                    {
                        // to use or not to use?  that is the question.
                        // And the answer is... YES.
                        dcisAsCancer = true;    // though it defaulted to this anyway, as it turns out
                    }
                }

                string fhAsString = TransformUtils.DataContractSerializeObject<RiskApps3.Model.PatientRecord.FHx.FamilyHistory>(theFH);

                //transform it
                XmlDocument inDOM = new XmlDocument();
                inDOM.LoadXml(fhAsString);
                if (String.IsNullOrEmpty(toolsPath))
                {
                    toolsPath = RiskApps3.Utilities.Configurator.getNodeValue("Globals", "ToolsPath"); // @"C:\Program Files\riskappsv2\tools\";
                }
                if (String.IsNullOrEmpty(strServiceBinding))
                {
                    strServiceBinding = "WSHttpBinding_IService1";
                }
                XmlDocument resultXmlDoc = TransformUtils.performTransform(inDOM, toolsPath, @"hra_to_ccd_remove_namespaces.xsl");
                XmlDocument hl7FHData;
                if (dcis_found && dcisAsCancer)
                {
                    //only do this when calling the RiskAggravator Service; we don't want to change the actual diseases in the object model
                    hl7FHData = TransformUtils.performTransformWith2Params(resultXmlDoc, toolsPath, @"hra_serialized_to_hl7.xsl", "dcisAsCancer", "1", "deIdentify", "1");

                }
                else
                {
                    hl7FHData = TransformUtils.performTransformWithParam(resultXmlDoc, toolsPath, @"hra_serialized_to_hl7.xsl", "deIdentify", "1");
                }

                //Insert Clinic Name and Institution Name into HL7
                //Get the first (and only) text node
                XmlNode theFirstTextNode = hl7FHData.SelectSingleNode("(//FamilyHistory/text)[1]");
                //and replace its contents with the clinic name and institution names
                theFirstTextNode.InnerText = " Current User:  ; " + BCDB2.Instance.GetClinicAndInstitutionNames(apptid);
                
                bool dump = Configurator.getConfigBool("WriteAggregatorFiles");
                
                //convert to string
                string hl7Request = convertXMLDocToString(hl7FHData);
                if (dump)
                {
                    File.WriteAllText("AggregatorRequest.xml", hl7Request);
                }
                
                //call the aggregator servicethis is a blocking call, up to 60 seconds
                //AggregatorServiceReference2.Service1Client client = new AggregatorServiceReference2.Service1Client();
                //next line is for testing locally only
                //AggregatorServiceReferenceDev3.Service1Client client = new AggregatorServiceReferenceDev3.Service1Client();
                HraRiskAggregator.Service1Client client = new HraRiskAggregator.Service1Client(strServiceBinding);
                client.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);  //set the timeout to 1 minute

                string hl7Reply = "";
                try
                {
                    // old way:
                    // hl7Reply = client.GetRiskData(hl7Request);
                    //

                    //call the aggregator using the licenseID in the config file
                    string licID = Configurator.getAggregatorLicenseID();
                    hl7Reply = client.GetRiskDataWithDetails(hl7Request, licID);

                    //if we make it here, the synchronous web service call completed successfully
                    if (dump)
                    {
                        File.WriteAllText("AggregatorReply.xml", hl7Reply);
                    }

                    RP.ClearRP();

                    //Use an XPath navigator to retrieve the XML results into the object model
                    StringReader sr = new StringReader(hl7Reply);
                    XmlTextReader tx = new XmlTextReader(sr);
                    XPathDocument docNav = new XPathDocument(tx);
                    XPathNavigator nav = docNav.CreateNavigator();

                    //get BMRS requestId, effectiveTime, and top level messages from HL7 reply
                    XPathNavigator bmrsInfo = nav.SelectSingleNode("//pedigreeAnalysisResults[id/@extension][1]");
                    if (bmrsInfo != null)
                    {
                        RP.BMRS_RequestId = Convert.ToInt64(bmrsInfo.Evaluate("string(id/@extension)").ToString());
                        string hl7effTime = bmrsInfo.Evaluate("string(effectiveTime/@value)").ToString();
                        string[] formats = {"yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyyMMddHH", "yyyyMMdd"};
                        DateTime dateValue;
                        if (DateTime.TryParseExact(hl7effTime, formats, null, System.Globalization.DateTimeStyles.None, out dateValue))
                            RP.BMRS_EffectiveTime = dateValue;
                        else
                            RP.BMRS_EffectiveTime = null;
                        RP.BMRS_Messages = bmrsInfo.Evaluate("string(text)").ToString().Trim();
                    }

                    processBrcaProHL7Scores(nav);
                    processMmrProHL7Scores(nav);
                    processTyrerCuzickHL7Scores(nav);
                    processTyrerCuzickv7HL7Scores(nav);
                    processMyriadHL7Score(nav);
                    processClausHL7Scores(nav);
                    processGailHL7Scores(nav);
                    processCCRATHL7Scores(nav);
                    processPREMMScores(nav);
                    //invoke sp_RunNCCN if appropriate
                    BCDB2.Instance.ExecuteNonQuery("EXEC sp_RunNCCN @apptid='" + apptid + "'");
                    RP.NCCNGuideline.BackgroundListLoad();

                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);

                    //since we know we have new risk scores, persist 'em
                    foreach (Person p in FHx.Relatives)
                    {
                        p.RP.PersistFullObject(args);
                    }

                }
                catch (Exception e)  //exceptioned out on web service call; likely due to timeout
                {
                    if (dump)
                    {
                        File.WriteAllText("AggregatorReply.xml", e.Message);
                    }
                    //Configurator.aggregatorActive = false;
                    //updateServiceStatusOnMainForm(false);
                    //doThisWhenNotUsingAggregator();
                }
            
            } //end of if block for using Aggregator Service
            else
            {
                doThisWhenNotUsingAggregator();
            }

            BCDB2.Instance.ExecuteNonQuery("EXEC sp_3_populateBigQueue @unitnum='"+unitnum+"'");


            //RunCdsForm cds = new RunCdsForm();
            //cds.apptid = apptid;
            //cds.unitnum = unitnum;

            //cds.ShowDialog();

            //cdsBreastOvary.LoadObject();

        }

        private void doThisWhenNotUsingAggregator()
        {
            RiskAppCore.Globals.setApptID(apptid);
            RiskAppCore.Globals.setUnitNum(unitnum);
            //old way, uses core
            string clinic = "";
            string institution = "";
            BCDB2.Instance.GetClinicAndInstitutionNames(apptid, out clinic, out institution);
            RiskAppCore.User.setClinicName(clinic);
            RiskAppCore.User.setInstitutionName(institution);

            RiskModels.RiskEngine.recalculateRisk(false);

            RiskAppCore.Globals.setApptID(-1);
            RiskAppCore.Globals.setUnitNum("");

            foreach (Person fm in FHx.Relatives)
            {
                fm.RP.LoadObject();
            }
            RP.BracproCancerRisk.LoadList();
            RP.MmrproCancerRiskList.LoadList();
            RP.GailModel.LoadList();
            RP.ClausModel.LoadList();
            RP.TyrerCuzickModel.LoadList();
            RP.TyrerCuzickModel_v7.LoadList();

            RP.CCRATModel.LoadList();
            RP.NCCNGuideline.LoadList();
        }

        public void ProcessAggregatorCalculations(XPathNavigator nav)
        {
            processBrcaProHL7Scores(nav);
            processMmrProHL7Scores(nav);
            processTyrerCuzickHL7Scores(nav);
            processTyrerCuzickv7HL7Scores(nav);
            processMyriadHL7Score(nav);
            processClausHL7Scores(nav);
            processGailHL7Scores(nav);
            processCCRATHL7Scores(nav);
            processPREMMScores(nav);
        }
        //public void RunModels()
        //{
        //    RiskAppCore.Globals.setApptID(apptid);
        //    RiskModels.RiskEngine.RunModelsNoGui();
        //    BCDB2.Instance.ExecuteNonQuery("EXEC sp_3_populateBigQueue @unitnum='" + unitnum + "'");
        //}

        public string convertXMLDocToString(XmlDocument doc)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "   ";
            settings.Encoding = Encoding.UTF8;

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter, settings))
            {
                doc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        private XmlNode createElement(XmlDocument docToAddThisTo, string xml)
        {
            XmlDocument docTemp = new XmlDocument();
            docTemp.LoadXml(xml);
            XmlNode oNode = docTemp.DocumentElement;
            XmlNode importNode = docToAddThisTo.ImportNode(oNode, true);
            return importNode;
        }

        private void processBrcaProHL7Scores(XPathNavigator nav)
        {
            //get BrcaPro scores
            XPathNavigator bpNode = nav.SelectSingleNode("//pedigreeAnalysisResults[methodCode/@code='BRCAPRO'][1]");
            if (bpNode != null)
            {
                RP.BrcaPro_Version = bpNode.Evaluate("string(methodCode[@code='BRCAPRO']/@codeSystemVersion)").ToString();
                XPathNavigator textNode = bpNode.SelectSingleNode("text");
                //create XML fragment with messages and hazard rates
                XmlDocument myTempDoc = new XmlDocument();
                myTempDoc.LoadXml("<text>" + System.Web.HttpUtility.HtmlDecode(textNode.InnerXml) + "</text>");
                XPathNavigator n1 = myTempDoc.CreateNavigator();
                RP.BrcaPro_Messages = n1.Evaluate("string(//messages[1])").ToString().Trim();
                XPathNodeIterator xnit = n1.Select("//hazardRates[1]/row");  //gets all the hazard rate rows (one per age)
                int ptAge = -1;  //handy to have age as an integer for hazard rate zero interest rows
                Int32.TryParse(this.age, out ptAge);

                foreach (XPathNavigator row in xnit)
                {
                    int id = Convert.ToInt32(row.Evaluate("string(@id)").ToString());
                    if (id <= ptAge) continue;  //no need to save all 0 hazard rate rows in object model or DB

                    //find an existing BrcaProCancerRiskByAge for this age, if any
                    //  else make a new one and add it to the BracproCancerRiskList
                    BrcaProCancerRiskByAge bpcrba = (BrcaProCancerRiskByAge)RP.BracproCancerRisk.FirstOrDefault(item => ((BrcaProCancerRiskByAge)item).age == id);
                    if (bpcrba == null)
                    {
                        bpcrba = new BrcaProCancerRiskByAge();
                        bpcrba.age = id;
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        RP.BracproCancerRisk.AddToList(bpcrba, args);
                    }
                    bpcrba.hFX0 = Convert.ToDouble(row.Evaluate("concat(string(@hFX0), string(@hMX0))").ToString());
                    bpcrba.hFY0 = Convert.ToDouble(row.Evaluate("concat(string(@hFY0), string(@hMY0))").ToString());
                    bpcrba.hFX1 = Convert.ToDouble(row.Evaluate("concat(string(@hFX1), string(@hMX1))").ToString());
                    bpcrba.hFY1 = Convert.ToDouble(row.Evaluate("concat(string(@hFY1), string(@hMY1))").ToString());
                    bpcrba.hFX2 = Convert.ToDouble(row.Evaluate("concat(string(@hFX2), string(@hMX2))").ToString());
                    bpcrba.hFY2 = Convert.ToDouble(row.Evaluate("concat(string(@hFY2), string(@hMY2))").ToString());
                    bpcrba.hFX12 = Convert.ToDouble(row.Evaluate("concat(string(@hFX12), string(@hMX12))").ToString());
                    bpcrba.hFY12 = Convert.ToDouble(row.Evaluate("concat(string(@hFY12), string(@hMY12))").ToString());
                }

                XPathNodeIterator ptCPRIter = bpNode.Select("component/percentageRisk[typeId/@extension = '" + this.relativeID + "']");
                foreach (XPathNavigator cpr in ptCPRIter)
                {
                    switch (cpr.Evaluate("string(code/@displayName)").ToString())
                    {
                        case "probCarrier":
                            RP.BrcaPro_1_2_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probBRCA1Mutation":
                            RP.BrcaPro_1_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probBRCA2Mutation":
                            RP.BrcaPro_2_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }

                XPathNodeIterator ptAgeChunkIter = bpNode.Select("component/age[value/low/@value]");
                int five_year_age = 105;
                int lifetime_age = 0;
                foreach (XPathNavigator ac in ptAgeChunkIter)
                {
                    int age = Convert.ToInt32(ac.Evaluate("string(value/low/@value)").ToString());
                    if (age < five_year_age) five_year_age = age;
                    if (age > lifetime_age) lifetime_age = age;
                    //find an existing BrcaProCancerRiskByAge for this age, if any
                    //  else make a new one and add it to the BracproCancerRiskList
                    BrcaProCancerRiskByAge bpcrba = (BrcaProCancerRiskByAge)RP.BracproCancerRisk.FirstOrDefault(item => ((BrcaProCancerRiskByAge)item).age == age);
                    if (bpcrba == null)  //since we already processed hazard rates, list element for every age should already exist, and this code block should never run
                    {
                        bpcrba = new BrcaProCancerRiskByAge();
                        bpcrba.age = age;
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        RP.BracproCancerRisk.AddToList(bpcrba, args);
                    }

                    double brca0 = 100 - (double)RP.BrcaPro_1_2_Mut_Prob;
                    double brca11_and_2 = (double)(RP.BrcaPro_1_2_Mut_Prob - (RP.BrcaPro_1_Mut_Prob + RP.BrcaPro_2_Mut_Prob));

                    bpcrba.BreastCaRisk = (bpcrba.hFX0 * brca0) +
                                          (bpcrba.hFX1 * (double)RP.BrcaPro_1_Mut_Prob) +
                                          (bpcrba.hFX2 * (double)RP.BrcaPro_2_Mut_Prob) +
                                          (bpcrba.hFX12 * brca11_and_2);
                    bpcrba.BreastCaRiskWithNoMutation = (bpcrba.hFX0 * 100) +
                                                          (bpcrba.hFX1 * 0) +
                                                          (bpcrba.hFX2 * 0) +
                                                          (bpcrba.hFX12 * 0);
                    bpcrba.BreastCaRiskWithBrca1Mutation = (bpcrba.hFX0 * 0) +
                                                          (bpcrba.hFX1 * 100) +
                                                          (bpcrba.hFX2 * 0) +
                                                          (bpcrba.hFX12 * 0);
                    bpcrba.BreastCaRiskWithBrca2Mutation = (bpcrba.hFX0 * 0) +
                                                          (bpcrba.hFX1 * 0) +
                                                          (bpcrba.hFX2 * 100) +
                                                          (bpcrba.hFX12 * 0);
                    bpcrba.BreastCaRiskWithBrca1And2Mutation = (bpcrba.hFX0 * 0) +
                                                                (bpcrba.hFX1 * 0) +
                                                                (bpcrba.hFX2 * 0) +
                                                                (bpcrba.hFX12 * 100);
                    bpcrba.OvarianCaRisk = (bpcrba.hFY0 * brca0) +
                                          (bpcrba.hFY1 * (double)RP.BrcaPro_1_Mut_Prob) +
                                          (bpcrba.hFY2 * (double)RP.BrcaPro_2_Mut_Prob) +
                                          (bpcrba.hFY12 * brca11_and_2);
                    bpcrba.OvarianCaRiskWithNoMutation = (bpcrba.hFY0 * 100) +
                                                          (bpcrba.hFY1 * 0) +
                                                          (bpcrba.hFY2 * 0) +
                                                          (bpcrba.hFY12 * 0);
                    bpcrba.OvarianCaRiskWithBrca1Mutation = (bpcrba.hFY0 * 0) +
                                                          (bpcrba.hFY1 * 100) +
                                                          (bpcrba.hFY2 * 0) +
                                                          (bpcrba.hFY12 * 0);
                    bpcrba.OvarianCaRiskWithBrca2Mutation = (bpcrba.hFY0 * 0) +
                                                          (bpcrba.hFY1 * 0) +
                                                          (bpcrba.hFY2 * 100) +
                                                          (bpcrba.hFY12 * 0);
                    bpcrba.OvarianCaRiskWithBrca1And2Mutation = (bpcrba.hFY0 * 0) +
                                                                (bpcrba.hFY1 * 0) +
                                                                (bpcrba.hFY2 * 0) +
                                                                (bpcrba.hFY12 * 100);


                    switch (ac.Evaluate("string(pertinentInformation/realmCode/@code)").ToString())
                    {
                        case "breastCancerRisk":
                            bpcrba.BreastCaRisk = Math.Round(100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString()), 6);
                            if (age == five_year_age) RP.BrcaPro_5Year_Breast = bpcrba.BreastCaRisk;
                            if (age == lifetime_age) RP.BrcaPro_Lifetime_Breast = bpcrba.BreastCaRisk;
                            break;
                        case "ovarianCancerRisk":
                            bpcrba.OvarianCaRisk = Math.Round(100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString()), 6);
                            if (age == five_year_age) RP.BrcaPro_5Year_Ovary = bpcrba.OvarianCaRisk;
                            if (age == lifetime_age) RP.BrcaPro_Lifetime_Ovary = bpcrba.OvarianCaRisk;
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }
                RP.BM_5Year_Age = five_year_age;
                RP.BM_Lifetime_Age = lifetime_age;

                //update risk scores for all non-proband family members
                //these are mutation probs only
                XPathNodeIterator relCPRIter = bpNode.Select("component/percentageRisk[typeId/@extension != '" + this.relativeID + "']");
                foreach (XPathNavigator cpr in relCPRIter)
                {
                    int relId = Convert.ToInt32(cpr.Evaluate("string(typeId/@extension)").ToString());
                    Person rel = FHx.Relatives.FirstOrDefault(item => ((Person)item).relativeID == relId);
                    if (rel.RP.BMRS_EffectiveTime == null) rel.RP.BMRS_EffectiveTime = RP.BMRS_EffectiveTime;
                    if (rel.RP.BrcaPro_Version == null) rel.RP.BrcaPro_Version = RP.BrcaPro_Version;
                    switch (cpr.Evaluate("string(code/@displayName)").ToString())
                    {
                        case "probCarrier":
                            rel.RP.BrcaPro_1_2_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probBRCA1Mutation":
                            rel.RP.BrcaPro_1_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probBRCA2Mutation":
                            rel.RP.BrcaPro_2_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }
            }
        }

        private void processMmrProHL7Scores(XPathNavigator nav)
        {
            //get MmrPro scores
            XPathNavigator mpNode = nav.SelectSingleNode("//pedigreeAnalysisResults[methodCode/@code='MMRPRO'][1]");
            if (mpNode != null)
            {
                RP.MmrPro_Version = mpNode.Evaluate("string(methodCode[@code='MMRPRO']/@codeSystemVersion)").ToString();
                XPathNavigator textNode = mpNode.SelectSingleNode("text");
                //create XML fragment with messages and hazard rates
                XmlDocument myTempDoc = new XmlDocument();
                myTempDoc.LoadXml("<text>" + System.Web.HttpUtility.HtmlDecode(textNode.InnerXml) + "</text>");
                XPathNavigator n1 = myTempDoc.CreateNavigator();
                RP.MmrPro_Messages = n1.Evaluate("string(//messages[1])").ToString().Trim();
                XPathNodeIterator xnit = n1.Select("//hazardMMRRates[1]/row");  //gets all the hazard rate rows (one per age)
                int ptAge = -1;
                Int32.TryParse(this.age, out ptAge);

                XPathNodeIterator ptCPRIter = mpNode.Select("component/percentageRisk[typeId/@extension = '" + this.relativeID + "']");
                foreach (XPathNavigator cpr in ptCPRIter)
                {
                    switch (cpr.Evaluate("string(code/@displayName)").ToString())
                    {
                        case "probCarrier":
                            RP.MmrPro_1_2_6_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probMLH1Mutation":
                            RP.MmrPro_MLH1_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probMSH2Mutation":
                            RP.MmrPro_MSH2_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probMSH6Mutation":
                            RP.MmrPro_MSH6_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }

                foreach (XPathNavigator row in xnit)
                {
                    int id = Convert.ToInt32(row.Evaluate("string(@id)").ToString());
                    if (id <= ptAge) continue;  //no need to save all 0 hazard rate rows in object model or DB

                    //find an existing MMRproCancerRiskByAge for this age, if any
                    //  else make a new one and add it to the MmrproCancerRiskList
                    MMRproCancerRiskByAge mpcrba = (MMRproCancerRiskByAge)RP.MmrproCancerRiskList.FirstOrDefault(item => ((MMRproCancerRiskByAge)item).age == id);
                    if (mpcrba == null)
                    {
                        mpcrba = new MMRproCancerRiskByAge();
                        mpcrba.age = id;
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        RP.MmrproCancerRiskList.AddToList(mpcrba, args);
                    }
                    mpcrba.hFX0 = Convert.ToDouble(row.Evaluate("concat(string(@hFX0), string(@hMX0))").ToString());
                    mpcrba.hFX1 = Convert.ToDouble(row.Evaluate("concat(string(@hFX1), string(@hMX1))").ToString());
                    mpcrba.hFX2 = Convert.ToDouble(row.Evaluate("concat(string(@hFX2), string(@hMX2))").ToString());
                    mpcrba.hFX6 = Convert.ToDouble(row.Evaluate("concat(string(@hFX6), string(@hMX6))").ToString());
                    mpcrba.hFY0 = Convert.ToDouble(row.Evaluate("concat(string(@hFY0), string(@hMY0))").ToString());
                    mpcrba.hFY1 = Convert.ToDouble(row.Evaluate("concat(string(@hFY1), string(@hMY1))").ToString());
                    mpcrba.hFY2 = Convert.ToDouble(row.Evaluate("concat(string(@hFY2), string(@hMY2))").ToString());
                    mpcrba.hFY6 = Convert.ToDouble(row.Evaluate("concat(string(@hFY6), string(@hMY6))").ToString());

                    mpcrba.ColonCaRisk = ((double)RP.MmrPro_MLH1_Mut_Prob / 100.0 * mpcrba.hFX1) +
                     ((double)RP.MmrPro_MSH2_Mut_Prob / 100.0 * mpcrba.hFX2) +
                     ((double)RP.MmrPro_MSH6_Mut_Prob / 100.0 * mpcrba.hFX6) +
                     ((1 - (double)(RP.MmrPro_MLH1_Mut_Prob / 100.0 + RP.MmrPro_MSH2_Mut_Prob / 100.0 + RP.MmrPro_MSH6_Mut_Prob / 100.0)) * mpcrba.hFX0);


                    mpcrba.EndometrialCaRisk = ((double)RP.MmrPro_MLH1_Mut_Prob / 100.0 * mpcrba.hFY1) +
                                                ((double)RP.MmrPro_MSH2_Mut_Prob / 100.0 * mpcrba.hFY2) +
                                                ((double)RP.MmrPro_MSH6_Mut_Prob / 100.0 * mpcrba.hFY6) +
                     ((1 - (double)(RP.MmrPro_MLH1_Mut_Prob / 100.0 + RP.MmrPro_MSH2_Mut_Prob / 100.0 + RP.MmrPro_MSH6_Mut_Prob / 100.0)) * mpcrba.hFY0);

                    mpcrba.ColonCaRisk = mpcrba.ColonCaRisk * 100;
                    mpcrba.EndometrialCaRisk = mpcrba.EndometrialCaRisk * 100;

                    mpcrba.ColonCaRiskWithMLH1 = 100 * mpcrba.hFX1;
                    mpcrba.EndometrialCaRiskWithMLH1 = 100 * mpcrba.hFY1;

                    mpcrba.ColonCaRiskWithMSH2 = 100 * mpcrba.hFX2;
                    mpcrba.EndometrialCaRiskWithMSH2 = 100 * mpcrba.hFY2;

                    mpcrba.ColonCaRiskWithMSH6 = 100 * mpcrba.hFX6;
                    mpcrba.EndometrialCaRiskWithMSH6 = 100 * mpcrba.hFY6;

                    mpcrba.ColonCaRiskNoMut = 100 * mpcrba.hFX0;
                    mpcrba.EndometrialCaRiskNoMut = 100 * mpcrba.hFY0;
                }


                XPathNodeIterator ptAgeChunkIter = mpNode.Select("component/age[value/low/@value]");
                int five_year_age = 105;
                int lifetime_age = 0;
                foreach (XPathNavigator ac in ptAgeChunkIter)
                {
                    int age = Convert.ToInt32(ac.Evaluate("string(value/low/@value)").ToString());
                    if (age < five_year_age) five_year_age = age;
                    if (age > lifetime_age) lifetime_age = age;
                    //find an existing MmrProCancerRiskByAge for this age, if any
                    //  else make a new one and add it to the MmrproCancerRiskList
                    MMRproCancerRiskByAge mpcrba = (MMRproCancerRiskByAge)RP.MmrproCancerRiskList.FirstOrDefault(item => ((MMRproCancerRiskByAge)item).age == age);
                    if (mpcrba == null)
                    {
                        mpcrba = new MMRproCancerRiskByAge();
                        mpcrba.age = age;
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        RP.MmrproCancerRiskList.AddToList(mpcrba, args);
                    }




                    switch (ac.Evaluate("string(pertinentInformation/realmCode/@code)").ToString())
                    {
                        case "colorectalCancerRisk":
                            mpcrba.ColonCaRisk = Math.Round(100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString()), 6);
                            if (age == five_year_age) RP.MmrPro_5Year_Colon = mpcrba.ColonCaRisk;
                            if (age == lifetime_age) RP.MmrPro_Lifetime_Colon = mpcrba.ColonCaRisk;
                            break;
                        case "endometrialCancerRisk":
                            mpcrba.EndometrialCaRisk = Math.Round(100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString()), 6);
                            if (age == five_year_age) RP.MmrPro_5Year_Endometrial = mpcrba.EndometrialCaRisk;
                            if (age == lifetime_age) RP.MmrPro_Lifetime_Endometrial = mpcrba.EndometrialCaRisk;
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }

                //update risk scores for all non-proband family members
                //these are mutation probs only
                XPathNodeIterator relCPRIter = mpNode.Select("component/percentageRisk[typeId/@extension != '" + this.relativeID + "']");
                foreach (XPathNavigator cpr in relCPRIter)
                {
                    int relId = Convert.ToInt32(cpr.Evaluate("string(typeId/@extension)").ToString());
                    Person rel = FHx.Relatives.FirstOrDefault(item => ((Person)item).relativeID == relId);
                    if (rel.RP.BMRS_EffectiveTime == null) rel.RP.BMRS_EffectiveTime = RP.BMRS_EffectiveTime;
                    if (rel.RP.MmrPro_Version == null) rel.RP.MmrPro_Version = RP.MmrPro_Version;
                    switch (cpr.Evaluate("string(code/@displayName)").ToString())
                    {
                        case "probCarrier":
                            rel.RP.MmrPro_1_2_6_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probMLH1Mutation":
                            rel.RP.MmrPro_MLH1_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probMSH2Mutation":
                            rel.RP.MmrPro_MSH2_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        case "probMSH6Mutation":
                            rel.RP.MmrPro_MSH6_Mut_Prob = Math.Round(100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString()), 6);
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }
            }
        }

        private void processTyrerCuzickHL7Scores(XPathNavigator nav)
        {
            //get TC scores
            XPathNavigator tcNode = nav.SelectSingleNode("//pedigreeAnalysisResults[methodCode/@code='TYRER-CUZICK'][1]");
            if (tcNode != null)
            {
                RP.TyrerCuzickModel.RiskFactors.TYRER_CUZICK_VERSION = tcNode.Evaluate("string(methodCode[@code='TYRER-CUZICK']/@codeSystemVersion)").ToString();
                XPathNavigator textNode = tcNode.SelectSingleNode("text");
                //create XML fragment with messages and hazard rates
                XmlDocument myTempDoc = new XmlDocument();
                myTempDoc.LoadXml("<text>" + (textNode != null ? System.Web.HttpUtility.HtmlDecode(textNode.InnerXml) : "<messages>No TC Messages</messages>") + "</text>");
                XPathNavigator n1 = myTempDoc.CreateNavigator();
                RP.TyrerCuzickModel.RiskFactors.TYRER_CUZICK_MESSAGES = n1.Evaluate("string(//messages[1])").ToString().Trim();

                XPathNodeIterator ptCPRIter = tcNode.Select("component/percentageRisk[typeId/@extension = '" + this.relativeID + "']");
                foreach (XPathNavigator cpr in ptCPRIter)
                {
                    switch (cpr.Evaluate("string(code/@displayName)").ToString())
                    {
                        case "probCarrier":
                            RP.TyrerCuzickModel.RiskFactors.TYRER_CUZICK_1_2 = (100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString())).ToString();
                            RP.TyrerCuzick_Brca_1_2 = Convert.ToDouble(RP.TyrerCuzickModel.RiskFactors.TYRER_CUZICK_1_2);
                            break;
                        case "probBRCA1Mutation":
                            RP.TyrerCuzickModel.RiskFactors.TYRER_CUZICK_1 = (100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString())).ToString();
                            break;
                        case "probBRCA2Mutation":
                            RP.TyrerCuzickModel.RiskFactors.TYRER_CUZICK_2 = (100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString())).ToString();
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }

                XPathNodeIterator ptAgeChunkIter = tcNode.Select("component/age[value/low/@value]");
                int five_year_age = 105;
                int lifetime_age = 0;
                int ptAge = -1;
                Int32.TryParse(this.age, out ptAge);
                foreach (XPathNavigator ac in ptAgeChunkIter)
                {
                    int age = Convert.ToInt32(ac.Evaluate("string(value/low/@value)").ToString());
                    if (age < five_year_age) five_year_age = age;
                    if (age > lifetime_age) lifetime_age = age;
                    //find an existing TcRiskByAge for this age, if any
                    //  else make a new one and add it to the TyrerCuzick list
                    TcRiskByAge tcrba = (TcRiskByAge)RP.TyrerCuzickModel.FirstOrDefault(item => ((TcRiskByAge)item).age == age);
                    if (tcrba == null)
                    {
                        tcrba = new TcRiskByAge();
                        tcrba.age = age;
                        if (ptAge >= 0)
                        {
                            int deltaAge = age - ptAge;
                            tcrba.description = (deltaAge % 5 == 0) ? deltaAge.ToString() + " Year" : "";
                        }
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        RP.TyrerCuzickModel.AddToList(tcrba, args);
                    }

                    switch (ac.Evaluate("string(pertinentInformation/realmCode/@code)").ToString())
                    {
                        case "breastCancerRisk":
                            tcrba.BreastCaRisk = Math.Round(100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString()), 6);
                            if (age == five_year_age) RP.TyrerCuzick_5Year_Breast = tcrba.BreastCaRisk;
                            if (age == lifetime_age) RP.TyrerCuzick_Lifetime_Breast = tcrba.BreastCaRisk;
                            break;
                        case "populationBreastCancerRisk":
                            tcrba.PopulationCaRisk = 100.0 * Math.Round(Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString()), 6);
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }

                TcRiskByAge tcrbaoldest = (TcRiskByAge)RP.TyrerCuzickModel.FirstOrDefault(item => ((TcRiskByAge)item).age == lifetime_age);
                if (tcrbaoldest != null)
                {
                    TcRiskByAge tcrbaLifetime = new TcRiskByAge();
                    tcrbaLifetime.age = tcrbaoldest.age;
                    tcrbaLifetime.BreastCaRisk = tcrbaoldest.BreastCaRisk;
                    tcrbaLifetime.PopulationCaRisk = tcrbaoldest.PopulationCaRisk;
                    tcrbaLifetime.description = "Lifetime";
                    RP.TyrerCuzickModel.AddToList(tcrbaLifetime, new HraModelChangedEventArgs(null));
                }
            }
        }

        private void processTyrerCuzickv7HL7Scores(XPathNavigator nav)
        {
            //get TC scores
            XPathNavigator tcNode = nav.SelectSingleNode("//pedigreeAnalysisResults[methodCode/@code='TYRER-CUZICK7'][1]");
            if (tcNode != null)
            {
                RP.TyrerCuzickModel_v7.RiskFactors.TYRER_CUZICK7_VERSION = tcNode.Evaluate("string(methodCode[@code='TYRER-CUZICK7']/@codeSystemVersion)").ToString();
                XPathNavigator textNode = tcNode.SelectSingleNode("text");
                //create XML fragment with messages and hazard rates
                XmlDocument myTempDoc = new XmlDocument();
                myTempDoc.LoadXml("<text>" + (textNode != null ? System.Web.HttpUtility.HtmlDecode(textNode.InnerXml) : "<messages>No TC Messages</messages>") + "</text>");
                XPathNavigator n1 = myTempDoc.CreateNavigator();
                RP.TyrerCuzickModel_v7.RiskFactors.TYRER_CUZICK7_MESSAGES = n1.Evaluate("string(//messages[1])").ToString().Trim();

                XPathNodeIterator ptCPRIter = tcNode.Select("component/percentageRisk[typeId/@extension = '" + this.relativeID + "']");
                foreach (XPathNavigator cpr in ptCPRIter)
                {
                    switch (cpr.Evaluate("string(code/@displayName)").ToString())
                    {
                        case "probCarrier":
                            RP.TyrerCuzickModel_v7.RiskFactors.TYRER_CUZICK7_1_2 = (100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString())).ToString();
                            RP.TyrerCuzick_v7_Brca_1_2 = Convert.ToDouble(RP.TyrerCuzickModel_v7.RiskFactors.TYRER_CUZICK7_1_2);
                            break;
                        case "probBRCA1Mutation":
                            RP.TyrerCuzickModel_v7.RiskFactors.TYRER_CUZICK7_1 = (100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString())).ToString();
                            break;
                        case "probBRCA2Mutation":
                            RP.TyrerCuzickModel_v7.RiskFactors.TYRER_CUZICK7_2 = (100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString())).ToString();
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }

                XPathNodeIterator ptAgeChunkIter = tcNode.Select("component/age[value/low/@value]");
                int five_year_age = 105;
                int lifetime_age = 0;
                int ptAge = -1;
                Int32.TryParse(this.age, out ptAge);
                foreach (XPathNavigator ac in ptAgeChunkIter)
                {
                    int age = Convert.ToInt32(ac.Evaluate("string(value/low/@value)").ToString());
                    if (age < five_year_age) five_year_age = age;
                    if (age > lifetime_age) lifetime_age = age;
                    //find an existing TcRiskByAge for this age, if any
                    //  else make a new one and add it to the TyrerCuzick_v7 list
                    TcRiskByAge tcrba = (TcRiskByAge)RP.TyrerCuzickModel_v7.FirstOrDefault(item => ((TcRiskByAge)item).age == age);
                    if (tcrba == null)
                    {
                        tcrba = new TcRiskByAge();
                        tcrba.age = age;
                        if (ptAge >= 0)
                        {
                            int deltaAge = age - ptAge;
                            tcrba.description = (deltaAge % 5 == 0) ? deltaAge.ToString() + " Year" : "";
                        }
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        RP.TyrerCuzickModel_v7.AddToList(tcrba, args);
                    }

                    switch (ac.Evaluate("string(pertinentInformation/realmCode/@code)").ToString())
                    {
                        case "breastCancerRisk":
                            tcrba.BreastCaRisk = Math.Round(100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString()), 6);
                            if (age == five_year_age) RP.TyrerCuzick_v7_5Year_Breast = tcrba.BreastCaRisk;
                            if (age == lifetime_age) RP.TyrerCuzick_v7_Lifetime_Breast = tcrba.BreastCaRisk;
                            break;
                        case "populationBreastCancerRisk":
                            tcrba.PopulationCaRisk = Math.Round(100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString()), 6);
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }

                TcRiskByAge tcrbaoldest = (TcRiskByAge)RP.TyrerCuzickModel_v7.FirstOrDefault(item => ((TcRiskByAge)item).age == lifetime_age);
                if (tcrbaoldest != null)
                {
                    TcRiskByAge tcrbaLifetime = new TcRiskByAge();
                    tcrbaLifetime.age = tcrbaoldest.age;
                    tcrbaLifetime.BreastCaRisk = tcrbaoldest.BreastCaRisk;
                    tcrbaLifetime.PopulationCaRisk = tcrbaoldest.PopulationCaRisk;
                    tcrbaLifetime.description = "Lifetime";
                    RP.TyrerCuzickModel_v7.AddToList(tcrbaLifetime, new HraModelChangedEventArgs(null));
                }

            }
        }

        private void processMyriadHL7Score(XPathNavigator nav)
        {
            //get Myriad score
            XPathNavigator mNode = nav.SelectSingleNode("//pedigreeAnalysisResults[methodCode/@code='MYRIAD'][1]");
            if (mNode != null)
            {
                XPathNodeIterator ptCPRIter = mNode.Select("component/percentageRisk[typeId/@extension = '" + this.relativeID + "']");
                foreach (XPathNavigator cpr in ptCPRIter)
                {
                    switch (cpr.Evaluate("string(code/@displayName)").ToString())
                    {
                        case "probCarrier":
                            //RP.Myriad_Brca_1_2 = 100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString());
                            RP.Myriad_Brca_1_2 = 100.0 * Convert.ToDouble(cpr.Evaluate("string(value/@value)").ToString());
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }
            }
        }

        private void processClausHL7Scores(XPathNavigator nav)
        {
            //get Claus scores
            XPathNavigator cNode = nav.SelectSingleNode("//pedigreeAnalysisResults[methodCode/@code='CLAUS'][1]");
            if (cNode != null)
            {
                RP.ClausModel.Clear();
                RP.ClausModel.RiskFactors.Claus_Table = "";
                RP.ClausModel.RiskFactors.RelOne = "";
                RP.ClausModel.RiskFactors.RelTwo = "";


                XPathNavigator textNode = cNode.SelectSingleNode("text");
                string clausMessages = textNode.Evaluate("string(.)").ToString().Trim().Replace("\r", "");
                //extract the three Claus messages that are always present
                string pattern = @"^\s*Claus_Table: (?<clausTable>.*)$\s*^\s*Claus_RelOne: (?<clausRel1>.*)$\s*^\s*Claus_RelTwo: (?<clausRel2>.*)$";
                Match m = Regex.Match(clausMessages, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (m.Success) {
                    RP.ClausModel.RiskFactors.Claus_Table = m.Result("${clausTable}");
                    RP.ClausModel.RiskFactors.ClausRiskFactors_RelOne = m.Result("${clausRel1}");
                    RP.ClausModel.RiskFactors.ClausRiskFactors_RelTwo = m.Result("${clausRel2}");
                }

                RP.ClausModel.RiskFactors.effectiveTime = cNode.Evaluate("string(effectiveTime/@value)").ToString();
                XPathNodeIterator ptAgeChunkIter = cNode.Select("component/age[value/low/@value]");
                foreach (XPathNavigator ac in ptAgeChunkIter)
                {
                    int age = Convert.ToInt32(ac.Evaluate("string(value/low/@value)").ToString());
                    //find an existing ClausRiskByAge for this age, if any
                    //  else make a new one and add it to the Claus list
                    ClausRiskByAge crba = (ClausRiskByAge)RP.ClausModel.FirstOrDefault(item => ((ClausRiskByAge)item).age == age);
                    if (crba == null)
                    {
                        crba = new ClausRiskByAge();
                        crba.age = age;
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        RP.ClausModel.AddToList(crba, args);
                    }

                    crba.BreastCaRisk = 100 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString());

                    switch (ac.Evaluate("string(pertinentInformation/realmCode/@code)").ToString())
                    {
                        //case "breastCancerRisk":
                        //    crba.BreastCaRisk = Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString());
                        //    break;
                        case "Claus_FiveYear":
                            RP.Claus_5Year_Breast = 100 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString());
                            break;
                        case "Claus_Lifetime":
                            RP.Claus_Lifetime_Breast = 100 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString());
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }
            }
        }

        private void processGailHL7Scores(XPathNavigator nav)
        {
            //get Gail scores
            XPathNavigator gNode = nav.SelectSingleNode("//pedigreeAnalysisResults[methodCode/@code='GAIL'][1]");
            if (gNode != null)
            {
                XPathNavigator textNode = gNode.SelectSingleNode("text");
                string gailInputs = textNode.Evaluate("string(.)").ToString().Trim().Replace("\r", "");

                //recover Gail Input values
                Dictionary<string, string>  ptGailInfo = new Dictionary<string, string>();
                List<string> names = null;

                //Now iterate through the three lines and create the Gail values
                //routine works even if headers produced by hraGetGail.xsl change in the future
                foreach (var myRow in gailInputs.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (myRow.Trim().StartsWith("Gail inputs:"))
                    {
                        //do nothing
                    }
                    else if (myRow.Trim().StartsWith("currentAge menarcheAge"))
                    {
                        //this is the first row which contains the headers
                        string[] headers = myRow.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                        names = headers.ToList<string>();
                    }
                    else  //this is the Gail info row
                    {
                        string[] gail_inputs = myRow.Trim().Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < gail_inputs.Length; i++) ptGailInfo.Add(names[i], gail_inputs[i]);
                    }
                }

                RP.GailModel.RiskFactors.currentAge = ptGailInfo.ContainsKey("currentAge") ? ptGailInfo["currentAge"] : "UNKNOWN";
                RP.GailModel.RiskFactors.menarcheAge = ptGailInfo.ContainsKey("menarcheAge") ? ptGailInfo["menarcheAge"] : "UNKNOWN";
                RP.GailModel.RiskFactors.firstLiveBirthAge = ptGailInfo.ContainsKey("firstLiveBirthAge") ? ptGailInfo["firstLiveBirthAge"] : "UNKNOWN";
                RP.GailModel.RiskFactors.hadBiopsy = ptGailInfo.ContainsKey("hadBiopsy") ? ptGailInfo["hadBiopsy"] : "UNKNOWN";
                RP.GailModel.RiskFactors.numBiopsy = ptGailInfo.ContainsKey("numBiopsy") ? ptGailInfo["numBiopsy"] : "UNKNOWN";
                RP.GailModel.RiskFactors.hyperPlasia = ptGailInfo.ContainsKey("hyperPlasia") ? ptGailInfo["hyperPlasia"] : "UNKNOWN";
                RP.GailModel.RiskFactors.firstDegreeRel = ptGailInfo.ContainsKey("firstDegreeRel") ? ptGailInfo["firstDegreeRel"] : "UNKNOWN";
                RP.GailModel.RiskFactors.race = ptGailInfo.ContainsKey("race") ? ptGailInfo["race"] : "UNKNOWN";

                if (string.Compare("white", RP.GailModel.RiskFactors.race, true) == 0)
                    RP.GailModel.RiskFactors.race = "1";
                else if (string.Compare("black", RP.GailModel.RiskFactors.race, true) == 0)
                    RP.GailModel.RiskFactors.race = "2";
                else if (string.Compare("hispanic", RP.GailModel.RiskFactors.race, true) == 0)
                    RP.GailModel.RiskFactors.race = "3";
                else
                    RP.GailModel.RiskFactors.race = "";

                if (string.Compare("UNKNOWN", RP.GailModel.RiskFactors.menarcheAge, true) == 0)
                    RP.GailModel.RiskFactors.menarcheAge = "99";
                if (string.Compare("UNKNOWN", RP.GailModel.RiskFactors.firstLiveBirthAge, true) == 0)
                    RP.GailModel.RiskFactors.firstLiveBirthAge = "99";
                if (string.Compare("UNKNOWN", RP.GailModel.RiskFactors.hadBiopsy, true) == 0)
                    RP.GailModel.RiskFactors.hadBiopsy = "99";
                if (string.Compare("UNKNOWN", RP.GailModel.RiskFactors.numBiopsy, true) == 0)
                    RP.GailModel.RiskFactors.numBiopsy = "99";
                if (string.Compare("UNKNOWN", RP.GailModel.RiskFactors.hyperPlasia, true) == 0)
                    RP.GailModel.RiskFactors.hyperPlasia = "99";
                if (string.Compare("UNKNOWN", RP.GailModel.RiskFactors.firstDegreeRel, true) == 0)
                    RP.GailModel.RiskFactors.firstDegreeRel = "99";
                if (string.Compare("UNKNOWN", RP.GailModel.RiskFactors.race, true) == 0)
                    RP.GailModel.RiskFactors.race = "99";

                RP.GailModel.RiskFactors.effectiveTime = gNode.Evaluate("string(effectiveTime/@value)").ToString();

                XPathNodeIterator ptAgeChunkIter = gNode.Select("component/age[value/low/@value]");
                foreach (XPathNavigator ac in ptAgeChunkIter)
                {
                    int age = Convert.ToInt32(ac.Evaluate("string(value/low/@value)").ToString());
                    //find an existing GailRiskByAge for this age, if any
                    //  else make a new one and add it to the Gail list
                    GailRiskByAge grba = (GailRiskByAge)RP.GailModel.FirstOrDefault(item => ((GailRiskByAge)item).age == age);
                    if (grba == null)
                    {
                        grba = new GailRiskByAge();
                        grba.age = age;
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        RP.GailModel.AddToList(grba, args);
                    }

                    switch (ac.Evaluate("string(pertinentInformation/realmCode/@code)").ToString())
                    {
                        case "Gail_Proband":
                            grba.BreastCaRisk = 100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString());
                            break;
                        case "Gail_Population":
                            grba.PopulationCaRisk = 100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString());
                            break;
                        case "Gail_Proband_FiveYear":
                            RP.Gail_5Year_Breast = 100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString());
                            break;
                        case "Gail_Proband_Lifetime":
                            RP.Gail_Lifetime_Breast = 100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString());
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }
            }
        }

        private void processCCRATHL7Scores(XPathNavigator nav)
        {
            //get ccrat scores
            XPathNavigator ccNode = nav.SelectSingleNode("//pedigreeAnalysisResults[methodCode/@code='CCRAT'][1]");
            if (ccNode != null)
            {
                RP.CCRATModel.Details.CCRAT_VERSION = ccNode.Evaluate("string(methodCode[@code='CCRAT']/@codeSystemVersion)").ToString();
                XPathNavigator textNode = ccNode.SelectSingleNode("text");
                if (textNode != null)
                {
                    //create XML fragment with messages
                    XmlDocument myTempDoc = new XmlDocument();
                    myTempDoc.LoadXml("<text>" + System.Web.HttpUtility.HtmlDecode(textNode.InnerXml) + "</text>");
                    XPathNavigator n1 = myTempDoc.CreateNavigator();
                    RP.CCRATModel.Details.CCRAT_MESSAGES = n1.Evaluate("string(//messages[1])").ToString().Trim();
                    RP.CCRATModel.Details.CCRAT_NAERRORS = n1.Evaluate("string(//NAError[1])").ToString().Trim();
                    RP.CCRATModel.Details.CCRAT_ERRORS = n1.Evaluate("string(//Error[1])").ToString().Trim();
                }
                else
                {
                    RP.CCRATModel.Details.CCRAT_MESSAGES = "";
                    RP.CCRATModel.Details.CCRAT_NAERRORS = "";
                    RP.CCRATModel.Details.CCRAT_ERRORS = "";
                }

                XPathNodeIterator ptAgeChunkIter = ccNode.Select("component/age[value/low/@value]");
                int five_year_age = 105;
                int lifetime_age = 0;
                foreach (XPathNavigator ac in ptAgeChunkIter)
                {
                    int age = Convert.ToInt32(ac.Evaluate("string(value/low/@value)").ToString());
                    if (age < five_year_age) five_year_age = age;
                    if (age > lifetime_age) lifetime_age = age;
                    //find an existing CCRATRiskByAge for this age, if any
                    //  else make a new one and add it to the CCRAT list
                    CCRATRiskByAge ccrba = (CCRATRiskByAge)RP.CCRATModel.FirstOrDefault(item => ((CCRATRiskByAge)item).age == age);
                    if (ccrba == null)
                    {
                        ccrba = new CCRATRiskByAge();
                        ccrba.age = age;
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        RP.CCRATModel.AddToList(ccrba, args);
                    }

                    switch (ac.Evaluate("string(pertinentInformation/realmCode/@code)").ToString())
                    {
                        case "colorectalCancerRisk":
                            ccrba.ColonCaRisk = 100.0 * Convert.ToDouble(ac.Evaluate("string(pertinentInformation/probability/value/@value)").ToString());
                            if (age == five_year_age) RP.CCRATModel.Details.CCRAT_FiveYear_CRC = ccrba.ColonCaRisk;
                            if (age == lifetime_age) RP.CCRATModel.Details.CCRAT_Lifetime_CRC = ccrba.ColonCaRisk;
                            break;
                        default:
                            //do nothing
                            break;
                    }
                }
            }
        }

        public static Patient processHL7Import(int apptid, string hl7, string riskMeaningsXml, string HL7Relationships)
        {
            Patient targetPatient = null;

            StringReader sr = new StringReader(hl7);
            XmlTextReader tx = new XmlTextReader(sr);
            XPathDocument docNav = new XPathDocument(tx);
            XPathNavigator nav = docNav.CreateNavigator();
            XPathNavigator patientNode = nav.SelectSingleNode("//patient");

            StringReader riskMean_sr = new StringReader(riskMeaningsXml);
            XmlTextReader riskMean_tx = new XmlTextReader(riskMean_sr);
            XPathDocument riskMean_docNav = new XPathDocument(riskMean_tx);
            XPathNavigator riskMean_nav = riskMean_docNav.CreateNavigator();
            XPathNavigator riskMean_rootNode = riskMean_nav.SelectSingleNode("//root");

            StringReader relations_sr = new StringReader(HL7Relationships);
            XmlTextReader relations_tx = new XmlTextReader(relations_sr);
            XPathDocument relations_docNav = new XPathDocument(relations_tx);
            XPathNavigator relations_nav = relations_docNav.CreateNavigator();
            XPathNavigator relations_rootNode = relations_nav.SelectSingleNode("//root");

            ////////////////////////////////////////////////
            //  Patient Node
            ////////////////////////////////////////////////
            if (patientNode != null)
            {
                //grab the unitnum and create new patient object
                //next line edited by PRB 2015.04.14; must include patientPerson at start to be looking at correct id
                string newMRN = patientNode.Evaluate("string(patientPerson/id/@extension)").ToString();
                if (string.IsNullOrEmpty(newMRN) == false)
                {
                    targetPatient = new Patient(newMRN);
                    targetPatient.apptid = apptid;
                   
                }
                else
                    return targetPatient;

                XPathNavigator patientPerson = patientNode.SelectSingleNode("patientPerson");
                if (patientPerson != null)
                {
                    int nextRelIdIfNeeded = 1;
                    Person p = targetPatient;
                    ProcessPersonNode(patientNode, riskMean_rootNode, ref p, true, ref nextRelIdIfNeeded);
                    targetPatient.FHx.Add(p);

                    XPathNodeIterator relativesIter = patientPerson.Select("relative");
                    foreach (XPathNavigator relativeNode in relativesIter)
                    {
                        p = new Person(targetPatient.FHx);
                        string hl7relCode = relativeNode.Evaluate("string(code/@code)").ToString();
                        string hraRelCode = relations_rootNode.Evaluate("string(Relationship[HL7Code/.='" + hl7relCode + "']/Mgh/.)").ToString();
                        string hraBloodlineCode = relations_rootNode.Evaluate("string(Relationship[HL7Code/.='" + hl7relCode + "']/MghBloodline/.)").ToString();

                        p.relationship = hraRelCode;
                        p.bloodline = hraBloodlineCode;

                        ProcessPersonNode(relativeNode, riskMean_rootNode, ref p, false, ref nextRelIdIfNeeded);

                        targetPatient.FHx.Add(p);
                    }
                    Dictionary<int, int> idMap = new Dictionary<int, int>();
                    int new_rel_id = 8;
                    foreach(Person rel in targetPatient.FHx)
                    {
                        switch(rel.relationship)
                        {
                            case "Self":
                                idMap.Add(rel.relativeID, 1);
                                break;
                            case "Mother":
                                idMap.Add(rel.relativeID, 2);
                                break;
                            case "Father":
                                idMap.Add(rel.relativeID, 3);
                                break;
                            case "Grandmother":
                                if (string.Compare(rel.bloodline, "maternal", true) == 0)
                                {
                                    idMap.Add(rel.relativeID, 4);
                                }
                                else if (string.Compare(rel.bloodline, "paternal", true) == 0)
                                {
                                    idMap.Add(rel.relativeID, 6);
                                }
                                break;
                            case "Grandfather":
                                if (string.Compare(rel.bloodline, "maternal", true) == 0)
                                {
                                    idMap.Add(rel.relativeID, 5);
                                }
                                else if (string.Compare(rel.bloodline, "paternal", true) == 0)
                                {
                                    idMap.Add(rel.relativeID, 7);
                                }
                                break; 
                            default:
                                idMap.Add(rel.relativeID, new_rel_id);
                                new_rel_id++;
                                break;
                        }
                    }
                    foreach (Person rel in targetPatient.FHx)
                    {
                        rel.relativeID = idMap[rel.relativeID];
                        if (rel.motherID > 0)
                            rel.motherID = idMap[rel.motherID];
                        if (rel.fatherID > 0)
                            rel.fatherID = idMap[rel.fatherID];
                    }

                    targetPatient.FHx.AddCoreRelatives();
                }
            }

            return targetPatient;
        }

        private static void ProcessPersonNode(XPathNavigator personNode, XPathNavigator riskMean_rootNode, ref Person targetPerson, bool isProband, ref int nextRelIdIfNeeded)
        { 
            string dataroot; 
            if (isProband)
                dataroot = "patientPerson";
            else
                dataroot = "relationshipHolder";

            XPathNavigator dataRootNode = personNode.SelectSingleNode(dataroot);

            /////////////////////////
            //Age
            /////////////////////////
            string age_val = personNode.Evaluate("string(subjectOf1/livingEstimatedAge/value/@value)").ToString();
            if (string.IsNullOrEmpty(age_val) == false)
            {
                targetPerson.age = age_val;
            }
            else
            {
                age_val = personNode.Evaluate("string(subjectOf1/deceasedEstimatedAge/value/@value)").ToString();
                if (string.IsNullOrEmpty(age_val) == false)
                {
                    targetPerson.age = age_val;
                }
            }

            if (String.IsNullOrEmpty(age_val))
            {
                //try to get age from the birthdate
                string birthDateRaw = personNode.Evaluate("string(patientPerson/birthTime/@value)").ToString();
                if (!string.IsNullOrEmpty(birthDateRaw))
                {
                    //convert the raw birthdate in YYYYMMDD format to the current age
                    string[] format = { "yyyyMMdd" };  //official HL7 date format
                    DateTime birthDate;
                    if (DateTime.TryParseExact(birthDateRaw,
                                               format,
                                               System.Globalization.CultureInfo.InvariantCulture,
                                               System.Globalization.DateTimeStyles.None,
                                               out birthDate))
                    {
                        DateTime today = DateTime.Today;
                        int age = today.Year - birthDate.Year;
                        if (birthDate > today.AddYears(-age)) age--;
                        targetPerson.age = age.ToString();
                    }
                }
            }


            //////////////////////////
            // other basics
            //////////////////////////

            //old way fails when relative Id isn't integer in source xml/hl7
            //targetPerson.relativeID = Convert.ToInt32(dataRootNode.Evaluate("string(id/@extension)").ToString());

            int number;
            bool resultRelId = Int32.TryParse(dataRootNode.Evaluate("string(id/@extension)").ToString(), out number);
            if (resultRelId)
            {
                targetPerson.relativeID = number;
            }
            else
            {
                targetPerson.relativeID = nextRelIdIfNeeded++;
            }

            targetPerson.name = dataRootNode.Evaluate("string(name/@formatted)").ToString();
            targetPerson.firstName = dataRootNode.Evaluate("string(name/@first)").ToString();
            targetPerson.lastName = dataRootNode.Evaluate("string(name/@last)").ToString();

            targetPerson.homephone = dataRootNode.Evaluate("string(telecom[@use='H']/@value)").ToString();
            targetPerson.workphone = dataRootNode.Evaluate("string(telecom[@use='WP']/@value)").ToString();

            targetPerson.gender = HL7FormatTranslator.GenderFromHL7(dataRootNode.Evaluate("string(administrativeGenderCode/@code)").ToString());
            targetPerson.dob = HL7FormatTranslator.DateFromHL7(dataRootNode.Evaluate("string(birthTime/@value)").ToString()); ;
            targetPerson.vitalStatus = HL7FormatTranslator.VitalStatusFromHL7(dataRootNode.Evaluate("string(deceasedInd/@value)").ToString());

            int temp_mom_id;
            int temp_dad_id;

            if (int.TryParse(dataRootNode.Evaluate("string(relative[code/@code='NMTH']/relationshipHolder/id/@extension)").ToString(), out temp_mom_id))
            {
                targetPerson.motherID = temp_mom_id;
            }
            if (int.TryParse(dataRootNode.Evaluate("string(relative[code/@code='NFTH']/relationshipHolder/id/@extension)").ToString(), out temp_dad_id))
            {
                targetPerson.fatherID = temp_dad_id;
            } 
            

            //////////////////////////
            // ethnicity
            //////////////////////////

            XPathNodeIterator raceIter = dataRootNode.Select("raceCode");
            foreach (XPathNavigator raceNode in raceIter)
            {
                string code = raceNode.Evaluate("string(@code)").ToString();
                string displayName = raceNode.Evaluate("string(@displayName)").ToString();
                string hraRaceText = HL7FormatTranslator.RaceFromHL7(code, displayName);
                if (hraRaceText == "Ashkenazi")
                {
                    targetPerson.isAshkenazi = "Yes";
                }
                else if (hraRaceText == "Hispanic")
                {
                    targetPerson.isHispanic = "Yes";
                }
                else
                {
                    Race r = new Race();
                    r.race = hraRaceText;
                    targetPerson.Ethnicity.Add(r);
                }
            }

            //SG encodes above in ethnicGroupCode rather than raceCode, so we check these too
            XPathNodeIterator ethnicIter = dataRootNode.Select("ethnicGroupCode");
            foreach (XPathNavigator ethnicNode in ethnicIter)
            {
                string code = ethnicNode.Evaluate("string(@code)").ToString();
                string displayName = ethnicNode.Evaluate("string(@displayName)").ToString();
                string hraEthnicityText = HL7FormatTranslator.EthnicityFromHL7(code, displayName);
                if (hraEthnicityText == "Ashkenazi")
                {
                    targetPerson.isAshkenazi = "Yes";
                }
                else if (hraEthnicityText == "Hispanic or Latino")
                {
                    targetPerson.isHispanic = "Yes";
                }
                else if (hraEthnicityText == "not Hispanic or Latino")
                {
                    targetPerson.isHispanic = "No";
                }
                else
                {
                    Race r = new Race();
                    r.race = hraEthnicityText;
                    targetPerson.Ethnicity.Add(r);
                }
            }

            //////////////////////////
            // Clinical Observations
            //////////////////////////
            //PRB modified XPath in next to *not* include COs that are for cause of death, which SG duplicates from another CO with that same disease
            XPathNodeIterator coIter = personNode.Select(@"subjectOf2/clinicalObservation[not(sourceOf/@typeCode='CAUS')]");
            foreach (XPathNavigator coNode in coIter)
            { 
                string coText = coNode.Evaluate("string(code/@displayName)").ToString();
                string coCode = coNode.Evaluate("string(code/@code)").ToString();
                string coCodeSystem = coNode.Evaluate("string(code/@codeSystemName)").ToString();
                string ageLowText = coNode.Evaluate("string(subject/dataEstimatedAge/value/low/@value)").ToString();
                string ageHighText = coNode.Evaluate("string(subject/dataEstimatedAge/value/high/@value)").ToString();
                string statusCode = coNode.Evaluate("string(statusCode/@code)").ToString();
                string coValue = coNode.Evaluate("string(code/qualifier/value/@code)").ToString();
                string coAgeDx = HL7FormatTranslator.GetIntFromHL7HighLow(ageLowText, ageHighText);

                string hra_tag = riskMean_rootNode.Evaluate("string(row[codeSystem/.='" + coCodeSystem + "'][code/.='" + coCode + "']/Mgh/.)").ToString();
                if (string.IsNullOrEmpty(hra_tag) == false)
                {
                    if (string.Compare(hra_tag, "Identical twin", true) == 0)
                    {
                        int temp;
                        if (int.TryParse(coValue, out temp))
                        {
                            targetPerson.twinID = temp;
                        }

                    }
                    else
                    {
                        if (targetPerson is Patient)
                        {
                            Patient p = (Patient)targetPerson;
                            bool result = processAsRiskFactorClinicalObservation(hra_tag, ref p, coAgeDx, statusCode, coValue);
                            if (!result)
                            {
                                ClincalObservation co = new ClincalObservation(targetPerson.PMH);
                                co.ClinicalObservation_disease = hra_tag;
                                co.ClinicalObservation_ageDiagnosis = coAgeDx;
                                targetPerson.PMH.Observations.Add(co);
                            }
                        }
                        else
                        {
                            ClincalObservation co = new ClincalObservation(targetPerson.PMH);
                            co.ClinicalObservation_disease = hra_tag;
                            co.ClinicalObservation_ageDiagnosis = coAgeDx;
                            targetPerson.PMH.Observations.Add(co);
                        }
                    }
                }
                else
                {
                    ClincalObservation co = new ClincalObservation(targetPerson.PMH);
                    co.ClinicalObservation_disease = coText;
                    co.ClinicalObservation_ageDiagnosis = coAgeDx;
                    targetPerson.PMH.Observations.Add(co);
                }
            }


            //////////////////////////
            // Cause of Death
            //////////////////////////
            XPathNavigator cdNav = personNode.SelectSingleNode(@"subjectOf2/clinicalObservation[sourceOf[@typeCode='CAUS']/clinicalObservation/code[(@displayName='death') or (@code='419620001')]]/code[1]");
            if (cdNav != null) {
                string cdText = cdNav.Evaluate("string(@displayName)").ToString();
                string cdCode = cdNav.Evaluate("string(@code)").ToString();
                string cdCodeSystem = cdNav.Evaluate("string(@codeSystemName)").ToString();
                string cd_hra_tag = riskMean_rootNode.Evaluate("string(row[codeSystem/.='" + cdCodeSystem + "'][code/.='" + cdCode + "']/Mgh/.)").ToString();
                if (string.IsNullOrEmpty(cd_hra_tag) == false)
                {
                    targetPerson.causeOfDeath = cd_hra_tag;
                }
                else
                {
                    targetPerson.causeOfDeath = cdText;
                }
            }
            
            //////////////////////////
            // Genetic Testing
            //////////////////////////

            //Panels first
            XPathNodeIterator gtIter = personNode.Select("subjectOf2/geneticLocus");
            List<GeneticTest> panels = new List<GeneticTest>();
            List<string> panelNames = new List<string>();

            foreach (XPathNavigator gtNode in gtIter)
            {
                string locusText = gtNode.Evaluate("string(text/.)").ToString();
                if (string.IsNullOrEmpty(locusText) == false)
                {
                    if (panelNames.Contains(locusText) == false)
                    {
                        GeneticTest new_panel = new GeneticTest(targetPerson.PMH);
                        new_panel.comments = locusText;
                        new_panel.GeneticTest_panelID = 34;
                        panels.Add(new_panel);
                        panelNames.Add(locusText);
                    }
                }
            }
            //then results
            foreach(GeneticTest panel in panels)
            {
                XPathNodeIterator resultIter = personNode.Select("subjectOf2/geneticLocus[text/.='" + panel.comments + "']");
                foreach (XPathNavigator resultNode in resultIter)
                {
                    string geneName = resultNode.Evaluate("string(value/@displayName)").ToString();
                    if (string.IsNullOrEmpty(geneName) == false)
                    {
                        GeneticTestResult result = new GeneticTestResult(panel);
                        result.geneName = geneName;

                        string significance = resultNode.Evaluate("string(component3/sequenceVariation/interpretationCode/@code)").ToString();
                        result.resultSignificance = significance;

                        panel.GeneticTestResults.Add(result);

                    }
                }
                targetPerson.PMH.GeneticTests.Add(panel);
            }
                
        }
        private static bool processAsRiskFactorClinicalObservation(string hra_tag, ref Patient targetPatient, string coAgeDx, string statusCode, string coValue)
        {
            bool retval = true;

            switch (hra_tag)
            {
                case "startedMenstruating":
                    targetPatient.ObGynHx.ObGynHistory_startedMenstruating = coAgeDx;
                    break;
                case "ageFirstChildBorn":
                    targetPatient.ObGynHx.ObGynHistory_ageFirstChildBorn = coAgeDx;
                    break;
                case "Colonoscopy":
                    targetPatient.SocialHx.colonoscopyLast10Years = HL7FormatTranslator.GetColonoscopyType(statusCode);
                    break;
                case "Colon Polyp-NOS":
                    targetPatient.SocialHx.colonPolypLast10Years = HL7FormatTranslator.GetColonPolypType(statusCode); ;
                    break;
                case "Number of breast biopsies":
                    targetPatient.procedureHx.breastBx.BreastBx_breastBiopsies = coValue;
                    break;
                case "Menopausal Status":
                    HL7FormatTranslator.DecodeMenopausalStatus(statusCode, ref targetPatient.ObGynHx);
                    break;
                case "Menopause Age":
                    targetPatient.ObGynHx.ObGynHistory_stoppedMenstruating = coAgeDx;
                    break;
                case "Height":
                    targetPatient.PhysicalExam.PhysicalExamination_heightInches = HL7FormatTranslator.GetInchesFromMeters(coValue);
                    targetPatient.PhysicalExam.PhysicalExamination_heightFeetInches = HL7FormatTranslator.GetFeetInchesFromMeters(coValue);
                    break;
                case "Weight":
                    targetPatient.PhysicalExam.PhysicalExamination_weightPounds = HL7FormatTranslator.GetPoundsFromKg(coValue);
                    break;
                case "HRT Use":
                    targetPatient.ObGynHx.ObGynHistory_hormoneUse = HL7FormatTranslator.GetHRTUse(statusCode);
                    break;
                case "HRT Type":
                    targetPatient.ObGynHx.ObGynHistory_hormoneCombined = HL7FormatTranslator.GetHRTType(statusCode);
                    break;
                case "HRT Length Past":
                    if (coValue != "-99")
                    {
                        targetPatient.ObGynHx.ObGynHistory_hormoneUseYears = coValue;
                    }
                    break;
                case "HRT Length Intent":
                    if (coValue != "-99")
                    {
                        targetPatient.ObGynHx.ObGynHistory_hormoneIntendedLength = coValue;
                    }
                    break;
                case "HRT Last Use":
                    if (coValue != "-99")
                    {
                        targetPatient.ObGynHx.ObGynHistory_hormoneYearsSinceLastUse = coValue;
                    }
                    break;
                case "Vigorous Exercise hours per week":
                    targetPatient.SocialHx.vigorousPhysicalActivityHoursPerWeek = HL7FormatTranslator.GetExcersizeType(statusCode);
                    break;
                case "Cigarette Smoking years":
                    targetPatient.SocialHx.SocialHistory_numYearsSmokedCigarettes = coValue;
                    break;
                case "Cigarettes per day":
                    targetPatient.SocialHx.SocialHistory_numCigarettesPerDay = coValue;
                    break;
                case "Vegetable servings per day":
                    targetPatient.SocialHx.vegetableServingsPerDay = HL7FormatTranslator.GetVegetablesType(statusCode);
                    break;
                case "Aspirin or NSAID regular use":
                    targetPatient.SocialHx.RegularAspirinUser = statusCode;
                    break;
                case "Ibuprofen regular use":
                    targetPatient.SocialHx.RegularIbuprofenUser = statusCode;
                    break;
                default:
                    retval = false;
                    break;
            }
            return retval;
        }
        private void processPREMMScores(XPathNavigator nav)
        {
            //get PREMM score
            //  only proband prob carrier, for now
            XPathNavigator premmNode = nav.SelectSingleNode("//pedigreeAnalysisResults[methodCode/@code='PREMM'][1]");
            if (premmNode != null)
            {
                RP.PREMM_Version = premmNode.Evaluate("string(methodCode[@code='PREMM']/@codeSystemVersion)").ToString();
                XPathNavigator textNode = premmNode.SelectSingleNode("text");
                //create XML fragment with messages
                XmlDocument myTempDoc = new XmlDocument();
                myTempDoc.LoadXml("<text>" + (textNode != null ? System.Web.HttpUtility.HtmlDecode(textNode.InnerXml) : "<messages>No PREMM Messages</messages>") + "</text>");
                XPathNavigator n1 = myTempDoc.CreateNavigator();
                RP.PREMM_Messages = n1.Evaluate("string(//messages[1])").ToString().Trim();
                RP.PREMM_Errors = n1.Evaluate("string(//Error[1])").ToString().Trim();

                //Risk Service only delivers PREMM2 results
                RP.PREMM2 = 100 * Convert.ToDouble(premmNode.Evaluate("string(component/percentageRisk[typeId/@extension = '1'][code/@displayName = 'probCarrier']/value/@value)").ToString());
            }
        }

        private void updateServiceStatusOnMainForm(bool status)
        {
            Control statusControl = null;
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "MainForm")  //we'll assume there's only one of these open, and in any case, just take the first one we find
                {
                    statusControl = form.Controls.Find("toolStripStatusLabel1", false).FirstOrDefault() as Control;
                    break;
                }
                if (statusControl != null) break;
            }

            if (statusControl != null)
            {
                statusControl.Invoke((MethodInvoker)delegate
                {
                    ((Label)statusControl).Text = (status) ? "Connected" : "Not Connected"; // runs on UI thread
                });
            }
        }
    }
}
        