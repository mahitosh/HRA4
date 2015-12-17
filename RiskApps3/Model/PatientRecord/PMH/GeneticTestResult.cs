using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using System.Windows.Forms;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class GeneticTestResult : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public GeneticTest owningGeneticTest;

        /**************************************************************************************************/
        [DataMember] public int instanceID = 0;
        [DataMember] public int testInstanceID;
        
        [DataMember] [HraAttribute] public string geneName;
        [DataMember] [HraAttribute] public string resultSignificance;
        [DataMember] [HraAttribute] public string mutationName;
        [DataMember] [HraAttribute] public string comments;
        [DataMember] [HraAttribute] public string mutationAA;
        [DataMember] [HraAttribute] public string geneRegion;
        [DataMember] [HraAttribute] public string allelicState;
        [DataMember] [HraAttribute] public string ASOMutationName;
        [DataMember] [HraAttribute] public string ASOMutationAA;
        [DataMember] [HraAttribute] public string ASOResultSignificance;
        [DataMember] [HraAttribute] public string ASOResult;
        [DataMember] [HraAttribute] public int relativeIDofRelative;
        [DataMember] [HraAttribute] public int instanceIDofRelative;
        [DataMember] [HraAttribute] public string externalMutationID;

        #region getters_setters
        /*****************************************************/
        public string GeneticTestResult_geneName
        {
            get
            {
                return geneName;
            }
            set
            {
                if (value != geneName)
                {
                    geneName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("geneName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GeneticTestResult_resultSignificance
        {
            get
            {
                return resultSignificance;
            }
            set
            {
                if (value != resultSignificance)
                {
                    resultSignificance = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("resultSignificance"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GeneticTestResult_mutationName
        {
            get
            {
                return mutationName;
            }
            set
            {
                if (value != mutationName)
                {
                    mutationName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("mutationName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GeneticTestResult_comments
        {
            get
            {
                return comments;
            }
            set
            {
                if (value != comments)
                {
                    comments = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("comments"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string GeneticTestResult_mutationAA
        {
            get
            {
                return mutationAA;
            }
            set
            {
                if (value != mutationAA)
                {
                    mutationAA = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("mutationAA"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GeneticTestResult_geneRegion
        {
            get
            {
                return geneRegion;
            }
            set
            {
                if (value != geneRegion)
                {
                    geneRegion = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("geneRegion"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GeneticTestResult_allelicState
        {
            get
            {
                return allelicState;
            }
            set
            {
                if (value != allelicState)
                {
                    allelicState = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("allelicState"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GeneticTestResult_ASOMutationName
        {
            get
            {
                return ASOMutationName;
            }
            set
            {
                if (value != ASOMutationName)
                {
                    ASOMutationName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ASOMutationName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GeneticTestResult_ASOMutationAA
        {
            get
            {
                return ASOMutationAA;
            }
            set
            {
                if (value != ASOMutationAA)
                {
                    ASOMutationAA = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ASOMutationAA"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GeneticTestResult_ASOResultSignificance
        {
            get
            {
                return ASOResultSignificance;
            }
            set
            {
                if (value != ASOResultSignificance)
                {
                    ASOResultSignificance = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ASOResultSignificance"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GeneticTestResult_ASOResult
        {
            get
            {
                return ASOResult;
            }
            set
            {
                if (value != ASOResult)
                {
                    ASOResult = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ASOResult"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GeneticTestResult_relativeIDofRelative
        {
            get
            {
                return relativeIDofRelative;
            }
            set
            {
                if (value != relativeIDofRelative)
                {
                    relativeIDofRelative = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("relativeIDofRelative"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GeneticTestResult_instanceIDofRelative
        {
            get
            {
                return instanceIDofRelative;
            }
            set
            {
                if (value != instanceIDofRelative)
                {
                    instanceIDofRelative = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("instanceIDofRelative"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string GeneticTestResult_externalMutationID
        {
            get
            {
                return externalMutationID;
            }
            set
            {
                if (value != externalMutationID)
                {
                    externalMutationID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("externalMutationID"));
                    SignalModelChanged(args);
                }
            }
        }
        #endregion



        /**************************************************************************************************/



        public GeneticTestResult() { } // Default constructor for serialization

        public GeneticTestResult(GeneticTest geneticTest)
        {
            owningGeneticTest = geneticTest;
        }

        public override int GetHashCode()
        {
            return
                (this.geneName!=null ? this.geneName.GetHashCode() : string.Empty.GetHashCode()) * 13 ^
                (this.mutationName!=null ? this.mutationName.GetHashCode() : string.Empty.GetHashCode()) * 7 ^
                (this.mutationAA!=null ? this.mutationAA.GetHashCode() : string.Empty.GetHashCode()) * 3;
        }

        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/
        public string GetASOSummary()
        {
            String retVal = "";

            if (retVal.Length > 0)
            {
                retVal = retVal + " ";
            }
            retVal = retVal + geneName;
            retVal = retVal.Trim();

            if (retVal.Length > 0)
            {
                retVal = retVal + " ";
            }
            retVal = retVal + ASOMutationName;
            retVal = retVal.Trim();

            if (retVal.Length > 0)
            {
                retVal = retVal + " ";
            }
            retVal = retVal + ASOMutationAA;
            retVal = retVal.Trim();

            if (retVal.Length > 0)
            {
                retVal = retVal + " ";
            }
            retVal = retVal + ASOResultSignificance;
            retVal = retVal.Trim();

            return retVal;
        }

        /**************************************************************************************************/

        public string getNonNegativeResult()
        {
            if (this.owningGeneticTest.IsASOTest)
            {
                if (this.ASOResult == "Found")
                {
                    return this.GetASOSummary();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                String meaning = getMeaning();
                if (isNonNegativeResult())
                {
                    return geneName + " " + meaning;
                }

                return "";
            }
        }

        /**************************************************************************************************/

        public bool isNonNegativeResult()
        {
            String meaning = getMeaning();
            if (meaning.Length > 0 && meaning.Equals("Negative", StringComparison.OrdinalIgnoreCase) == false)
            {
               return true;
            }

            return false;
        }

        /**************************************************************************************************/

        public string getMeaning()
        {
            if (resultSignificance == null)
            {
                return "";
            }
            if (resultSignificance.Equals("Benign", StringComparison.OrdinalIgnoreCase))
            {
                return "Favor Polymorphism";
            }
            if (resultSignificance.Equals("Deleterious", StringComparison.OrdinalIgnoreCase))
            {
                return "Deleterious";
            }
            if (resultSignificance.Equals("Favor Polymorphism", StringComparison.OrdinalIgnoreCase))
            {
                return "Favor Polymorphism";
            }
            if (resultSignificance.Equals("NEG", StringComparison.OrdinalIgnoreCase))
            {
                return "Negative";
            }
            if (resultSignificance.Equals("Negative", StringComparison.OrdinalIgnoreCase))
            {
                return "Negative";
            }
            if (resultSignificance.Equals("Pathogenic", StringComparison.OrdinalIgnoreCase))
            {
                return "Deleterious";
            }
            if (resultSignificance.Equals("POS", StringComparison.OrdinalIgnoreCase))
            {
                return "Deleterious";
            }
            if (resultSignificance.Equals("Presumed Benign", StringComparison.OrdinalIgnoreCase))
            {
                return "Favor Polymorphism";
            }
            if (resultSignificance.Equals("Presumed Pathogenic", StringComparison.OrdinalIgnoreCase))
            {
                return "Suspected Deleterious";
            }
            if (resultSignificance.Equals("Probably Deleterious", StringComparison.OrdinalIgnoreCase))
            {
                return "Suspected Deleterious";
            }
            if (resultSignificance.Equals("Suspected Deleterious", StringComparison.OrdinalIgnoreCase))
            {
                return "Suspected Deleterious";
            }
            if (resultSignificance.Equals("Unknown", StringComparison.OrdinalIgnoreCase))
            {
                return "Unknown";
            }
            if (resultSignificance.Equals("Unknown Significance", StringComparison.OrdinalIgnoreCase))
            {
                return "VUS";
            }
            if (resultSignificance.Equals("VUS", StringComparison.OrdinalIgnoreCase))
            {
                return "VUS";
            }
            return "";
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            lock (this.owningGeneticTest)
            {
                if (!string.IsNullOrEmpty(geneName))
                {
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("patientUnitnum", owningGeneticTest.owningPMH.RelativeOwningPMH.owningFHx.proband.unitnum);
                    pc.Add("apptid", owningGeneticTest.owningPMH.RelativeOwningPMH.owningFHx.proband.apptid);
                    pc.Add("relativeID", owningGeneticTest.owningPMH.RelativeOwningPMH.relativeID);
                    pc.Add("instanceID", instanceID, true);
                    if (testInstanceID == 0)
                    {
                        testInstanceID = owningGeneticTest.instanceID;
                    }
                    pc.Add("testInstanceID", testInstanceID);

                    if (e.updatedMembers.Count > 0)
                    {
                        bool found = false;
                        foreach (System.Reflection.MemberInfo mi in e.updatedMembers)
                        {
                            if (mi.Name == "geneName")
                            {
                                found = true;
                                break;
                            }

                        }
                        if (found == false)
                            pc.Add("geneName", geneName);
                    }

                    DoPersistWithSpAndParams(e,
                                              "sp_3_Save_GeneticTestResult",
                                              ref pc);

                    this.instanceID = (int)pc["instanceID"].obj;
                }
            }
        }
        public override bool Equals(object obj)
        {
            GeneticTestResult result = obj as GeneticTestResult;
            if (result != null)
            {
                GeneticTestResult test = result;
                return
                    test.geneName == this.geneName &&
                    (test.mutationName == this.mutationName ||
                    test.mutationAA == this.mutationAA);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Genetic Test Result: Gene: {0}, Mutation: {1}, Significance: {2}", 
                this.geneName, this.mutationName, this.resultSignificance);
        }
    }

    public class GeneticTestResultComparer : IComparer<GeneticTestResult>
    {
        public int Compare(GeneticTestResult x, GeneticTestResult y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    if (string.IsNullOrEmpty(x.geneName) && !string.IsNullOrEmpty(y.geneName))
                        return -1;
                    else if (!string.IsNullOrEmpty(x.geneName) && string.IsNullOrEmpty(y.geneName))
                        return 1;
                    else if (string.IsNullOrEmpty(x.geneName) && string.IsNullOrEmpty(y.geneName))
                        return 0;
                    else
                        return x.geneName.CompareTo(y.geneName);
          
                }
            }
        }
    }

}