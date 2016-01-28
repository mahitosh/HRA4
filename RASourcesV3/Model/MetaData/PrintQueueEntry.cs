using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;

using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.MetaData
{
    [DataContract]
    public class PrintQueueEntry : HraObject
    {
        [DataMember] [HraAttribute] public int ID;
        [DataMember] [HraAttribute] public int apptID;
        [DataMember] [HraAttribute] public int documentTemplateID;
        [DataMember] [HraAttribute] public string unitnum;
        [DataMember] [HraAttribute] public string patientName;
        [DataMember] [HraAttribute] public string dob;
        [DataMember] [HraAttribute] public string apptDate;
        [DataMember] [HraAttribute] public string documentName;
        [DataMember] [HraAttribute] public string lastPrinted;
        [DataMember] [HraAttribute] public string docType;
        
        public PrintQueueEntry() {}

        public PrintQueueEntry(int ID, int apptID, int documentTemplateID, string unitnum, string patientName,
                               string dob, string apptDate, string documentName, string lastPrinted, string docType)
        {
            this.ID = ID;
            this.apptID = apptID;
            this.documentTemplateID = documentTemplateID;
            this.unitnum = unitnum;
            this.patientName = patientName;
            this.dob = dob;
            this.apptDate = apptDate;
            this.documentName = documentName;
            this.lastPrinted = lastPrinted;
            this.docType = docType;
        }
        ///**************************************************************************************************/
        /// Probably won't need to persist this.
        ///**************************************************************************************************/
        //public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        //{
        //    //ParameterCollection pc = new ParameterCollection();

        //    try
        //    {
        //        //pc.Add("...", ...); 
        //        ...
        //
        //        //SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_SetPrintQueueEntry", pc);
        //    }
        //    catch (Exception ee)
        //    {
        //        Logger.Instance.WriteToLog("Error setting PrintQueueEntry: \r\n" + ee.InnerException.ToString());
        //    }
        //}
        /*****************************************************/
    }
}
