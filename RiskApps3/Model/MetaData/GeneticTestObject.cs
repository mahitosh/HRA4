using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.MetaData
{
    public class GeneticTestObject : HraObject
    {
        [HraAttribute] public int panelID;
        [HraAttribute] public string panelName;
        [HraAttribute] public string TEST_CODE;
        [HraAttribute] public string panelShortName;
        [HraAttribute] public float sensitivity;
        [HraAttribute] public float specificity;
        [HraAttribute] public int geneID;
        [HraAttribute] public string geneName;
        [HraAttribute] public string geneDescription;
        [HraAttribute] public string groupingName;
        [HraAttribute] public int groupingID;
        [HraAttribute] public int displayOrder;


        public override string ToString()
        {
            if (
                this.panelShortName != null &&
                !this.panelShortName.Equals(string.Empty))
            {
                return this.panelShortName;
            }
            else if (
                this.geneName != null &&
                !this.geneName.Equals(string.Empty))
            {
                return this.geneName;
            }
            else
            {
                return "Unknown genetic test";
            }
        }
    }
}
