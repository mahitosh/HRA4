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

namespace RiskApps3.Utilities
{
    class DocumentGenerator3
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
