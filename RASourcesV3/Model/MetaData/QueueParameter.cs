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
    public class QueueParameter : HraObject
    {
        [DataMember] [HraAttribute] public int ID;
        [DataMember] [HraAttribute] public string parameterName;
        [DataMember] [HraAttribute] public string rulesName;
        [DataMember] [HraAttribute] public double parameterValue;

        public QueueParameter() { }

        public QueueParameter(int ID, string parameterName, string rulesName, float parameterValue)
        {
            this.ID = ID;
            this.parameterName = parameterName;
            this.rulesName = rulesName;
            this.parameterValue = parameterValue;
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();

            try
            {
                pc.Add("ID", ID);
                pc.Add("parameterValue", parameterValue);

                SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_SetQueueParameter", pc);
            }
            catch (Exception ee)
            {
                Logger.Instance.WriteToLog("Error saving QueueParameter: \r\n" + ee.InnerException.ToString());
            }
        }
        /*****************************************************/
    }
}
