using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3;
using RiskApps3.Utilities;
using System.Data;


namespace RiskApps3.Model.MetaData
{
    public class VariantsFromKB :HraObject
    {
        public DataTable Variations = null;

        /**************************************************************************************************/

        public VariantsFromKB()
        {

        }

        public override void BackgroundLoadWork()
        {
            try
            {
                Variations = null;
                // skip for now
                //org.partners.hraweb.VariantLookup vl = new RiskApps3.org.partners.hraweb.VariantLookup();
                //Variations = vl.GetVariations();
            }
            catch(Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }
    }
}
