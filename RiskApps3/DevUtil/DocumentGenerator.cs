using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Controllers;
using RiskApps3.Model.Clinic;
using RiskApps3.Utilities;
using RiskApps3.Model;
using System.Threading;
using RiskApps3.Model.PatientRecord;
using System.ComponentModel;
using RiskApps3.Model.PatientRecord.FHx;

namespace DevUtil
{
    public class DocumentGenerator
    {

        public string GenerateHtmlDocument(int documentID, string unitnum, int apptid)
        {
            SessionManager.Instance.SetActivePatientNoCallback(unitnum, apptid);

            DocumentTemplate dt = new DocumentTemplate();
            dt.documentTemplateID = documentID;
            dt.BackgroundLoadWork();
            dt.OpenHTML();
            dt.ProcessDocument();

            SessionManager.Instance.Shutdown();
            return dt.htmlText;
        }
        public string GenerateHtmlDocument(int documentID, string unitnum, int apptid, string configFile)
        {
            RiskApps3.Utilities.Configurator.configFilePath = configFile;
            SessionManager.Instance.SetCoreConfigPath(configFile);

            SessionManager.Instance.SetActivePatientNoCallback(unitnum, apptid);

            DocumentTemplate dt = new DocumentTemplate();
            dt.documentTemplateID = documentID;
            dt.BackgroundLoadWork();
            dt.OpenHTML();
            dt.ProcessDocument();

            SessionManager.Instance.Shutdown();
            return dt.htmlText;
        }

        //public string RunRiskCalcsAndGenerateHtmlDocument(int documentID, string unitnum, int apptid, string configFile)
        //{
        //    RiskApps3.Utilities.Configurator.configFilePath = configFile;
        //    SessionManager.Instance.SetCoreConfigPath(configFile);
        //    SessionManager.Instance.SetActivePatientNoCallback(unitnum, apptid);
        //    SessionManager.Instance.GetActivePatient().RunModels();
        //    DocumentTemplate dt = new DocumentTemplate();
        //    dt.documentTemplateID = documentID;
        //    dt.BackgroundLoadWork();
        //    dt.OpenHTML();
        //    dt.ProcessDocument();
        //    SessionManager.Instance.Shutdown();

        //    return dt.htmlText;
        //}

    }
}



//            SessionManager.Instance.SetActivePatient(p_unitnum, p_apptid);

//            proband = SessionManager.Instance.GetActivePatient();
//            if (proband != null)
//            {
//                proband.AddHandlersWithLoad(null, activePatientLoaded, null);
//            }

//            return "";
//        }

//        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
//        {
//            //  get active patinet object from session manager
//            fhx = SessionManager.Instance.GetActivePatient().FHx;

//            if (fhx != null)
//            {
//                fhx.AddHandlersWithLoad(null, fhxLoaded, null);
//            }

//        }
//        /**************************************************************************************************/
//        private void fhxLoaded(HraListLoadedEventArgs e)
//        {
//            proband.guiPreferences.AddHandlersWithLoad(null,
//                                      null,
//                                      null);
//            foreach (Person p in SessionManager.Instance.GetActivePatient().FHx)
//            {
//                p.PMH.Observations.AddHandlersWithLoad(null,
//                             PedigreeElementLoaded,
//                             null);

//                p.PMH.GeneticTests.AddHandlersWithLoad(null,
//                                       PedigreeElementLoaded,
//                                       null);

//                p.Ethnicity.AddHandlersWithLoad(null, PedigreeElementLoaded, null);
//                p.Nationality.AddHandlersWithLoad(null, PedigreeElementLoaded, null);
//            }
//        }
//        /**************************************************************************************************/
//        private void PedigreeElementLoaded(HraListLoadedEventArgs e)
//        {
//            foreach (Person p in SessionManager.Instance.GetActivePatient().FHx)
//            {
//                if (p.PMH.Observations.IsLoaded == false)
//                    return;

//                if (p.PMH.GeneticTests.IsLoaded == false)
//                    return;

//                if (p.Ethnicity.IsLoaded == false)
//                    return;

//                if (p.Nationality.IsLoaded == false)
//                    return;
//            }

//            ProcessDocument();
//        }

//        private void ProcessDocument()
//        {
//            DocumentTemplate dt = new DocumentTemplate();
//            dt.documentTemplateID = p_documentID;
//            dt.BackgroundLoadWork();
//            dt.OpenHTML();
//            dt.ProcessDocument();

//            string x = dt.htmlText;
//            ////99907181301
//            //DocumentReadyEvent.Set();
//        }  

//        //    //try
//        //    //{
//        //    //    SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);

//        //    //    SessionManager.Instance.SetActivePatient(p_unitnum, p_apptid);

//        //    //    //DocumentReadyEvent.WaitOne();
//        //    //}
//        //    //catch (Exception exc)
//        //    //{
//        //    //    Logger.Instance.WriteToLog(exc.ToString());
//        //    //}

//        //    return htmlRetval;
//        //}
//        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
//        //{
//        //    //DocumentTemplate dt = new DocumentTemplate();
//        //    //dt.documentTemplateID = p_documentID;
//        //    //dt.BackgroundLoadWork();
//        //    //dt.OpenHTML();
//        //    //dt.ProcessDocument();

//        //    //htmlRetval = dt.htmlText;
//        //    ////99907181301
//        //    //DocumentReadyEvent.Set();
//        //}

//        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
//        //{

//        //}

//        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        //{

//        //}
//        //private void NewActivePatient(object sender, NewActivePatientEventArgs e)
//        //{
//        //    Patient proband = SessionManager.Instance.GetActivePatient();
//        //    if (proband != null)
//        //    {
//        //        proband.AddHandlersWithLoad(null, activePatientLoaded, null);
//        //    }
//        //}

//        //private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
//        //{
//        //    //bw.RunWorkerAsync();
//        //    DocumentTemplate dt = new DocumentTemplate();
//        //    dt.documentTemplateID = p_documentID;
//        //    dt.BackgroundLoadWork();
//        //    dt.OpenHTML();
//        //    dt.ProcessDocument();

//        //    //htmlRetval = dt.htmlText;
//        //    ////99907181301
//        //    //DocumentReadyEvent.Set();
//        //}

//        //bw.RunWorkerAsync();
//        //DocumentTemplate dt = new DocumentTemplate();
//        //dt.documentTemplateID = p_documentID;
//        //dt.BackgroundLoadWork();
//        //dt.OpenHTML();
//        //dt.ProcessDocument();

//        //htmlRetval = dt.htmlText;
//        ////99907181301
//        //DocumentReadyEvent.Set();
//        //string htmlRetval;
//        //BackgroundWorker bw;
//        //ManualResetEvent DocumentReadyEvent;

//        //public DocumentGenerator()
//        //{
//        //    htmlRetval = "";
//        //    bw = new BackgroundWorker();
//        //    bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
//        //    bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
//        //    bw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);

//        //    DocumentReadyEvent = new ManualResetEvent(false);
//        //}
//    }
//}
