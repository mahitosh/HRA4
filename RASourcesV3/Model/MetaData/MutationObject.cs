using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.MetaData
{
    public class MutationObject : HraObject
    {
        [HraAttribute] public string mutationAA;
        [HraAttribute] public string significance;
        [HraAttribute] public string externalID;
        [HraAttribute] public string geneName;
        [HraAttribute] public string mutationDNA;

        public override string ToString()
        {
            StringBuilder strB = new StringBuilder();
            strB.Append("Mutation: " + mutationAA ?? "NULL; ");
            strB.Append("Significance: " + significance ?? "NULL; ");
            strB.Append("External ID: " + externalID ?? "NULL; ");
            strB.Append("Gene Name: " + geneName ?? "NULL; ");
            strB.Append("Mutation DNA: " + mutationDNA ?? "NULL; ");

            return strB.ToString();
        }
    }
}
