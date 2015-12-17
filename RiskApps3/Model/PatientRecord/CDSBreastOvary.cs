using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using System.Reflection;
using System.Runtime.Serialization;

using RiskApps3.View;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class CDSBreastOvary : HraObject
    {
        public CDSBreastOvary() { }  // Default constructor for serialization

        /**************************************************************************************************/
        [DataMember] public Patient patientOwning;

        [DataMember] public string patientUnitnum;

        [DataMember] [HraAttribute]  public int CBERec;
        [DataMember] [HraAttribute]  public int MammoRec;
        [DataMember] [HraAttribute]  public int MRIRec;
        [DataMember] [HraAttribute]  public int ChemoRec;
        [DataMember] [HraAttribute]  public int ProphMastRec;
        [DataMember] [HraAttribute]  public int GenTestRec;
        [DataMember] [HraAttribute]  public int GenTestResult;
        [DataMember] [HraAttribute]  public int ScreeningRec;
        [DataMember] [HraAttribute]  public int PelvicExamRec;
        [DataMember] [HraAttribute]  public int TVSRec;
        [DataMember] [HraAttribute]  public int CA125Rec;
        [DataMember] [HraAttribute]  public int ProphOophRec;
        [DataMember] [HraAttribute]  public int OCRec;


        //[DataMember] [HraAttribute]  public string BeingFollowedInRiskClinic;

        [DataMember] [HraAttribute]  public int MmrGenTestRec;
        [DataMember] [HraAttribute]  public int MmrGenTestResult;


        /**************************************************************************************************/

        public CDSBreastOvary(Patient owner)
        {
            patientOwning = owner;
        }

        /**************************************************************************************************/

        public void RemoveViewHandlers(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", patientOwning.unitnum);
            pc.Add("apptid", patientOwning.apptid);
            DoLoadWithSpAndParams("sp_3_LoadCDSRecs", pc);
            //DoLoadWithSpAndParams("sp_3_LoadFollowedInRC", pc);
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", patientOwning.unitnum);
            pc.Add("apptid", patientOwning.apptid);

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_CDSRecs",
                                      ref pc);
        }
    }
}