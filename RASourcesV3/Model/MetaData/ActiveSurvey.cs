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
    public class ActiveSurvey : HraObject
    {
        [DataMember] [HraAttribute] public int surveyID;
        [DataMember] [HraAttribute] public string surveyName;
        [DataMember] [HraAttribute] public bool inactive;

        public ActiveSurvey()
        {
        }

        public ActiveSurvey(int surveyID, string surveyName, bool inactive)
        {
            this.surveyID = surveyID;
            this.surveyName = surveyName;
            this.inactive = inactive;
        }
        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();

            try
            {
                pc.Add("surveyID", surveyID);
                pc.Add("inactive", inactive);

                SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_SetActiveSurvey", pc);
            }
            catch (Exception ee)
            {
                Logger.Instance.WriteToLog("Error saving ActiveSurvey: \r\n" + ee.InnerException.ToString());
            }
        }
        /*****************************************************/

    }
}
