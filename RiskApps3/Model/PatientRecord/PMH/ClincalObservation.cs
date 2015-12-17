using System.Runtime.Serialization;

using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Utilities;
using RiskApps3.View;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class ClincalObservation : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public DiseaseDetails Details;
        [DataMember] [Hra]  private string Problem;
        [DataMember] [Hra(affectsRiskProfile = true)] public string ageDiagnosis;
        [DataMember] [Hra]  public string comments;
        [DataMember] [Hra(affectsRiskProfile = true)] public string disease = "";
        // These items now have setters and getters which will signal the model changed event
        // the sp_3_Save_ClinicalObservations doesn't do anything to persist them at this time, fyi
        [DataMember] [HraAttribute(auditable = false)] private string diseaseDisplayName;
        [DataMember] [HraAttribute(auditable = false)]  private string diseaseGender;
        [DataMember] [HraAttribute(auditable = false)]  private string diseaseIconArea;
        [DataMember] [HraAttribute(auditable = false)]  private string diseaseIconColor;
        [DataMember] [HraAttribute(auditable = false)]  private string diseaseIconType;
        [DataMember] [HraAttribute(auditable = false)]  private string diseaseOrder;
        [DataMember] [HraAttribute(auditable = false)]  private string diseaseShortName;
        [DataMember] [HraAttribute(auditable = false)]  private string diseaseSyndrome;
        [DataMember] public int instanceID = 0;
        [DataMember] public PastMedicalHistory owningPMH;

        [DataMember] public string riskMeaning = "";
        [DataMember] [Hra]  private string snomed;

        #region custom setters

        /*****************************************************/

        public void Set_disease(string value, HraView sendingView)
        {
            if (disease != value)
            {
                disease = value;
                SetDiseaseDetails();
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("disease"));
                SignalModelChanged(args);
            }
        }

        public void SetDiseaseDetails()
        {
            if (string.IsNullOrEmpty(disease))
                return;

            foreach (DiseaseObject dx in SessionManager.Instance.MetaData.Diseases)
            {
                if (dx.diseaseName.Equals(disease))
                {
                    ClinicalObservation_diseaseDisplayName = dx.diseaseDisplayName;
                    ClinicalObservation_diseaseGender = dx.diseaseGender;
                    ClinicalObservation_diseaseIconArea = dx.diseaseIconArea;
                    ClinicalObservation_diseaseIconColor = dx.diseaseIconColor;
                    ClinicalObservation_diseaseIconType = dx.diseaseIconType;
                    ClinicalObservation_diseaseOrder = dx.diseaseOrder;
                    ClinicalObservation_diseaseShortName = dx.diseaseShortName;
                    ClinicalObservation_diseaseSyndrome = dx.diseaseSyndrome;
                    ClinicalObservation_snomed = dx.SNOMED;
                    riskMeaning = dx.riskMeaning;
                    break;
                }
            }
            if (disease.ToLower().Contains("breast") && disease.ToLower().Contains("cancer"))
            {
                Details = new BreastCancerDetails();
                Details.owningClincalObservation = this;
                Details.BackgroundLoadWork();
            }
            else if ((disease.ToLower().Contains("colon") || 
                      disease.ToLower().Contains("uterine") || 
                      disease.ToLower().Contains("rectal")) && disease.ToLower().Contains("cancer"))
            {
                Details = new ColonCancerDetails();
                Details.owningClincalObservation = this;
                Details.BackgroundLoadWork();
            }
            else
            {
                Details = null;
            }

            if (string.IsNullOrEmpty(snomed))
            {
                snomed = "missing";
            }
            if (string.IsNullOrEmpty(diseaseIconArea))
            {
                diseaseIconArea = "All";
            }
            if (string.IsNullOrEmpty(diseaseIconColor))
            {
                diseaseIconColor = "Gray";
            }
            if (string.IsNullOrEmpty(diseaseIconType))
            {
                diseaseIconType = "Fill";
            }

            if (string.IsNullOrEmpty(diseaseDisplayName))
            {
                diseaseDisplayName = disease;
            }
        }

        /*****************************************************/

        public void Set_ageDiagnosis(string value, HraView sendingView)
        {
            if (ageDiagnosis != value)
            {
                ageDiagnosis = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("ageDiagnosis"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_comments(string value, HraView sendingView)
        {
            if (comments != value)
            {
                comments = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("comments"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_diseaseShortName(string value, HraView sendingView)
        {
            if (diseaseShortName != value)
            {
                diseaseShortName = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("diseaseShortName"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_diseaseSyndrome(string value, HraView sendingView)
        {
            if (diseaseSyndrome != value)
            {
                diseaseSyndrome = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("diseaseSyndrome"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_diseaseIconType(string value, HraView sendingView)
        {
            if (diseaseIconType != value)
            {
                diseaseIconType = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("diseaseIconType"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_diseaseIconArea(string value, HraView sendingView)
        {
            if (diseaseIconArea != value)
            {
                diseaseIconArea = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("diseaseIconArea"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_diseaseIconColor(string value, HraView sendingView)
        {
            if (diseaseIconColor != value)
            {
                diseaseIconColor = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("diseaseIconColor"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_diseaseDisplayName(string value, HraView sendingView)
        {
            if (diseaseDisplayName != value)
            {
                diseaseDisplayName = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("diseaseDisplayName"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_diseaseGender(string value, HraView sendingView)
        {
            if (diseaseGender != value)
            {
                diseaseGender = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("diseaseGender"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_diseaseOrder(string value, HraView sendingView)
        {
            if (diseaseOrder != value)
            {
                diseaseOrder = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("diseaseOrder"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_Problem(string value, HraView sendingView)
        {
            if (Problem != value)
            {
                Problem = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("Problem"));
                SignalModelChanged(args);
            }
        }

        /*****************************************************/

        public void Set_snomed(string value, HraView sendingView)
        {
            if (snomed != value)
            {
                snomed = value;
                var args = new HraModelChangedEventArgs(null);
                args.sendingView = sendingView;
                args.updatedMembers.Add(GetMemberByName("snomed"));
                SignalModelChanged(args);
            }
        }

        #endregion

        #region accessors

        /*****************************************************/

        public string ClinicalObservation_disease
        {
            get { return disease; }
            set
            {
                if (value != disease)
                {
                    disease = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("disease"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_ageDiagnosis
        {
            get { return ageDiagnosis; }
            set
            {
                if (value != ageDiagnosis)
                {
                    ageDiagnosis = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ageDiagnosis"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_diseaseShortName
        {
            get { return diseaseShortName; }
            set
            {
                if (value != diseaseShortName)
                {
                    diseaseShortName = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseShortName"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_diseaseSyndrome
        {
            get { return diseaseSyndrome; }
            set
            {
                if (value != diseaseSyndrome)
                {
                    diseaseSyndrome = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseSyndrome"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_diseaseIconType
        {
            get { return diseaseIconType; }
            set
            {
                if (value != diseaseIconType)
                {
                    diseaseIconType = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseIconType"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_diseaseIconArea
        {
            get { return diseaseIconArea; }
            set
            {
                if (value != diseaseIconArea)
                {
                    diseaseIconArea = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseIconArea"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_diseaseIconColor
        {
            get { return diseaseIconColor; }
            set
            {
                if (value != diseaseIconColor)
                {
                    diseaseIconColor = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseIconColor"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_diseaseDisplayName
        {
            get { return diseaseDisplayName; }
            set
            {
                if (value != diseaseDisplayName)
                {
                    diseaseDisplayName = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseDisplayName"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_diseaseGender
        {
            get { return diseaseGender; }
            set
            {
                if (value != diseaseGender)
                {
                    diseaseGender = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseGender"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_diseaseOrder
        {
            get { return diseaseOrder; }
            set
            {
                if (value != diseaseOrder)
                {
                    diseaseOrder = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseOrder"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_Problem
        {
            get { return Problem; }
            set
            {
                if (value != Problem)
                {
                    Problem = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Problem"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/

        public string ClinicalObservation_snomed
        {
            get { return snomed; }
            set
            {
                if (value != snomed)
                {
                    snomed = value;
                    var args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("snomed"));
                    SignalModelChanged(args);
                }
            }
        }

        /**************************************************************************************************/
        //public string AgeOfDiagnosis
        //{
        //    get
        //    {
        //        return ageDiagnosis;
        //    }
        //    set
        //    {
        //        if (value != ageDiagnosis)
        //        {
        //            ageDiagnosis = value;
        //            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
        //            args.updatedMembers.Add(GetMemberByName("ageDiagnosis"));
        //            SignalModelChanged(args);
        //        }
        //    }
        //}

        #endregion

        /**************************************************************************************************/
        
        public ClincalObservation() {} // Default constructor for serialization

        public ClincalObservation(PastMedicalHistory pmh)
        {
            owningPMH = pmh;
        }
        public override void LoadFullObject()
        {
            base.LoadFullObject();
        }

        public override void PersistFullObject(HraModelChangedEventArgs e)
        {
            if (Details != null)
            { 
                Details.owningClincalObservation = this;
                Details.BackgroundPersistWork(e);
            }

            base.PersistFullObject(e);
        }
        /**************************************************************************************************/

        public override void ReleaseListeners(object view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        public string GetSummaryText()
        {
            string retval = "";

            retval = disease + " @ " + ageDiagnosis;
            retval += Problem;
            string trimchars = " @";
            return retval.Trim(trimchars.ToCharArray());
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            var pc = new ParameterCollection();
            pc.Add("patientUnitnum", owningPMH.RelativeOwningPMH.owningFHx.proband.unitnum);
            pc.Add("relativeID", owningPMH.RelativeOwningPMH.relativeID);
            pc.Add("instanceID", instanceID, true);
            pc.Add("apptid", owningPMH.RelativeOwningPMH.owningFHx.proband.apptid);

            DoPersistWithSpAndParams(e,
                                     "sp_3_Save_ClinicalObservation",
                                     ref pc);

            instanceID = (int) pc["instanceID"].obj;
        }
    }
}

//this.instanceID = DoPersistWithRelativeAndInstance(e,
//                                "sp_3_Save_ClinicalObservation",
//                                this.owningPMH.RelativeOwningPMH.ownningFHx.proband,
//                                this.owningPMH.RelativeOwningPMH,
//                                this.instanceID);


//public override void BackgroundPersistWork(HraModelChangedEventArgs e)
//{
//    int relativeId = owningPMH.RelativeOwningPMH.relativeID;
//    //Console.WriteLine("relativeId=" + relativeId);

//    String unitnum = owningPMH.RelativeOwningPMH.ownningFHx.proband.unitnum;
//    //Console.WriteLine("unitnum=" + unitnum);


//    try
//    {
//        //////////////////////
//        using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
//        {
//            connection.Open();

//            SqlCommand cmdProcedure = new SqlCommand("sp_3_Save_ClinicalObservation", connection);
//            cmdProcedure.CommandType = CommandType.StoredProcedure;


//            BCDB2.AddUnitnumToCommand(owningPMH.RelativeOwningPMH.ownningFHx.proband, cmdProcedure);

//            cmdProcedure.Parameters.Add("@createdBy", SqlDbType.NVarChar);
//            cmdProcedure.Parameters["@createdBy"].Value = e.securityContext.user;


//            cmdProcedure.Parameters.Add("@relativeID", SqlDbType.Int);
//            cmdProcedure.Parameters["@relativeID"].Value = owningPMH.RelativeOwningPMH.relativeID;


//            cmdProcedure.Parameters.Add("@instanceID", SqlDbType.Int);
//            cmdProcedure.Parameters["@instanceID"].Value = instanceID;
//            cmdProcedure.Parameters["@instanceID"].Direction = ParameterDirection.InputOutput;

//            cmdProcedure.Parameters.Add("@disease", SqlDbType.NVarChar);
//            cmdProcedure.Parameters["@disease"].Value = disease;

//            cmdProcedure.Parameters.Add("@delete", SqlDbType.Bit);
//            cmdProcedure.Parameters["@delete"].Value = e.Delete;

//            if (String.IsNullOrEmpty(ageDiagnosis) == false)
//            {
//                cmdProcedure.Parameters.Add("@ageDiagnosis", SqlDbType.NVarChar);
//                cmdProcedure.Parameters["@ageDiagnosis"].Value = ageDiagnosis;
//            }

//            if (String.IsNullOrEmpty(comments) == false)
//            {
//                cmdProcedure.Parameters.Add("@comments", SqlDbType.NVarChar);
//                cmdProcedure.Parameters["@comments"].Value = comments;
//            }


//            //Console.WriteLine("********");
//            //Console.WriteLine(cmdProcedure.CommandText);
//            foreach (IDataParameter i in cmdProcedure.Parameters)
//            {
//                //Console.WriteLine(i.ParameterName + ": " + i.Value);
//            }

//            //Console.WriteLine("********");
//            cmdProcedure.ExecuteNonQuery();


//            instanceID = Int32.Parse(cmdProcedure.Parameters["@instanceID"].Value.ToString());

//            connection.Close();
//        } //end of using connection
//    }
//    catch (Exception exc)
//    {
//        Logger.Instance.WriteToLog("Persisting object - " + exc.ToString());
//        //Console.WriteLine("Persisting object - " + exc.ToString() + exc.StackTrace);
//    }
//    //BackgroundPersistWork(e);
//        //}
//    }
//}