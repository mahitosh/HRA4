using System;
using System.Collections.Generic;

using System.Text;

namespace RiskApps3.Model.PatientRecord.Pedigree
{
    /// <summary>
    /// A set of individuals on the same level which must be adjacent to one another.
    /// The mother and father for a couple is considered an IndividualSet
    /// </summary>
    public class PedigreeIndividualSet:List<PedigreeIndividual>
    {
        /// <summary>
        /// The couple whose location relative to other couples determines 
        /// where this individual set is placed relative to other individual sets.
        /// </summary>
        public readonly PedigreeCouple associatedCouple;

        public double staticLayoutOverlap = 0;

        public PedigreeIndividualSet(PedigreeCouple associatedCouple)
        {
            this.associatedCouple = associatedCouple;
        }
    }
}
