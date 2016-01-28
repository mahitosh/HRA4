using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using System.Data;

namespace RiskApps3.Model.MetaData
{
    public class AllBrOvCdsRecs :HraObject
    {
        public DataTable Recs = null;

        /**************************************************************************************************/

        public AllBrOvCdsRecs()
        {

        }

        public override void BackgroundLoadWork()
        {
            try
            {
                Recs = null;
                Recs = BCDB2.Instance.getDataTable(@"EXEC sp_3_GetAllPossibleCDSRecs");
            }
            catch(Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }
        public string GetRecTextFromID(int p)
        {
            string retval = "";
            foreach (object o in Recs.AsEnumerable())
            {
                DataRow dr = (DataRow)o;
                if (dr["recID"] != null)
                {
                    int id = (int)(dr["recID"]);
                    if (p==id)
                    {
                        if (dr["recValue"] != null)
                            retval = dr["recValue"].ToString();
                    }
                }
            }
            return retval;
        }
    }
}
