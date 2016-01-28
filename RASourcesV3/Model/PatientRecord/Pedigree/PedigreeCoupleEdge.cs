using System;
using System.Collections.Generic;

using System.Text;

namespace RiskApps3.Model.PatientRecord.Pedigree
{
    public class PedigreeCoupleEdge
    {
        public readonly PedigreeCouple u;
        public readonly PedigreeCouple v;
        /// <summary>
        /// 
        /// If true, then v.generationalLevel = u.generationalLevel + 1,
        ///  in other words, u = grandparents and v = parents
        /// If false, then v.generationalLevel = u.generationalLevel.
        /// </summary>
        public readonly bool intergenerational;
        public PedigreeCoupleEdge(PedigreeCouple u, PedigreeCouple v,bool intergenerational)
        {
            this.u = u;
            this.v = v;
            this.intergenerational = intergenerational;
        }
    }
}
