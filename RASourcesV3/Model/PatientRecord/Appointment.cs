using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using JetBrains.Annotations;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord
{
    public class Appointment : HraObject
    {
        // ReSharper disable InconsistentNaming - refelction is used to map these members to sp param names and to HraView controls so names must be preserved
        [Hra] public int apptID;
        public string unitnum;
        [Hra] public string patientname;
        public string diseases;   //TODO should be Diseases? or is used at all???
        [Hra] public string dob;
        [Hra] public string gender;
        [Hra] public string apptdate;
        [Hra] public string appttime;
        [Hra] public string apptphysname;
        [Hra] public int familyNumber = 0; //should be initialized when object loads from db
        [Hra] public string clinic;
        [Hra] public int clinicID;     //this is the actual clinic
        [Hra] public string clinicName; //this is the ra clinic
        public DateTime GoldenApptTime;
        [Hra] public DateTime apptdatetime;   //what is this for???
        public int GoldenAppt;
        [Hra] public string language;
        [Hra] public string race;
        [Hra] public string nationality;
        [Hra] public string surveyType = "";
        public DateTime riskdatacompleted;
        public string accession;    //TODO needed for fastdatalistview but where is it ever used elsewhere???...
        // ReSharper restore InconsistentNaming

        public string CloudWebQueueState = "";
        public int CloudWebQueueSynchId = -1;
        private int? _clinicId;

        /// <summary>
        /// Default constructor used in serialization and loading from DB.
        /// </summary>
        public Appointment() { }

        /// <summary>
        /// Use when creating a new appointment (not in serialization or loading from DB).
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="mrn"></param>
        public Appointment(int? clinicId, string mrn)
        {
            this._clinicId = clinicId;
            this.unitnum = mrn;

            this.apptID = CreateAppointment();
        }

        /// <summary>
        /// Used to copy an appointment from another appointment
        /// </summary>
        /// <param name="appointment"></param>
        public Appointment(Appointment appointment, int clinicId)
        {
            this._clinicId = clinicId;

            this.apptID = CreateAppointment();

            this.unitnum = appointment.unitnum;
            this.patientname = appointment.patientname;
            this.diseases = appointment.diseases;
            this.dob = appointment.dob;
            this.gender = appointment.gender;
            this.apptdate = appointment.apptdate;
            this.appttime = appointment.appttime;
            this.apptphysname = appointment.apptphysname;
            this.familyNumber = appointment.familyNumber;
            this.clinic = appointment.clinic;
            this.GoldenApptTime = appointment.GoldenApptTime;
            this.GoldenAppt = appointment.GoldenAppt;
            this.language = appointment.language;
            this.race = appointment.race;
            this.nationality = appointment.nationality;
            this.surveyType = appointment.surveyType;
            this.riskdatacompleted = appointment.riskdatacompleted;
            this.accession = appointment.accession;

            UpdateMrn(this.unitnum);
            this.PersistFullObject(new HraModelChangedEventArgs(null));
        }

        public ClinicList ClinicList { set; private get; }

        [PublicAPI]
        public int? FormattedFamilyNumber
        {
            get
            {
                return familyNumber > 0 ? familyNumber : (int?) null;
            }
            private set
            {
                if (value.HasValue) familyNumber = value.Value;
            }
        }

        public string PatientName
        {
            get { return patientname; }
            set
            {
                if (patientname != value)
                {
                    patientname = value;
                    this.SignalMemberChanged(GetMemberByName("patientname"));
                }
            }
        }

        public string Unitnum
        {
            get { return unitnum; }
            set
            {
                if (unitnum!=value)
                {
                    //TODO consider rolling UpdateAppointmentUnitnum into sp_3_Save_Appointment
                    unitnum = value;
                    UpdateMrn(unitnum);
                }
            }
        }

        public string Dob
        {
            get { return dob; }
            set
            {
                if (dob!=value)
                {
                    dob = value;
                    this.SignalMemberChanged(GetMemberByName("dob")); 
                }
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                if (gender!=value)
                {
                    gender = value;
                    this.SignalMemberChanged(GetMemberByName("gender")); 
                }
            }
        }

        public string ApptDate
        {
            get { return apptdate; }
            set
            {
                if (ApptDate!=value)
                {
                    apptdate = value;
                    this.SignalMemberChanged(GetMemberByName("apptdate")); 
                }
            }
        }

        public string ApptTime
        {
            get { return appttime; }
            set
            {
                if (appttime!=value)
                {
                    appttime = value;
                    this.SignalMemberChanged(GetMemberByName("appttime")); 
                }
            }
        }

        public string SurveyType
        {
            get { return surveyType; }
            set
            {
                if (surveyType!=value)
                {
                    surveyType = value;
                    this.SignalMemberChanged(GetMemberByName("surveyType")); 
                }
            }
        }

        [PublicAPI]
        public MetaData.Clinic Clinic
        {
            set
            {
                if (clinicID!=value.clinicID)
                {
                    this.clinicID = value.clinicID;
                    this.SignalMemberChanged(GetMemberByName("clinicID")); 
                }
            }
            get { return this.ClinicList.FirstOrDefault(c => c.clinicID == this.clinicID); }
        }

        public int ClinicId { get { return this.clinicID; } }

        public string AssessmentName
        {
            get
            {
                return this.clinic;
            }
            set
            {
                if (clinic!=value)
                {
                    this.clinic = value;
                    this.SignalMemberChanged(GetMemberByName("clinic")); 
                }
            }
        }

        public string Language
        {
            get
            {
                return this.language;
            }
            set
            {
                if (language!=value)
                {
                    this.language = value;
                    this.SignalMemberChanged(GetMemberByName("language")); 
                }
            }
        }

        public string Nationality
        {
            get
            {
                return this.nationality;
            }
            set
            {
                if (nationality!=value)
                {
                    this.nationality = value;
                    this.SignalMemberChanged(GetMemberByName("nationality")); 
                }
            }
        }

        public string Race
        {
            get
            {
                return this.race;
            }
            set
            {
                if (race!=value)
                {
                    this.race = value;
                    this.SignalMemberChanged(GetMemberByName("race")); 
                }
            }
        }

        public string Apptphysname
        {
            get { return apptphysname; }
            set
            {
                if (apptphysname!=value)
                {
                    apptphysname = value;
                    this.SignalMemberChanged(GetMemberByName("apptphysname")); 
                }
            }
        }

        /// <summary>
        /// Changes the unitnum for all appointments (and associated records) 
        /// having the present unitnum to the specified unitnum.
        /// </summary>
        public void UpdateMrn(string newUnitnum)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_updateMRN", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.Add("oldUnitnum", SqlDbType.NVarChar).Value = this.unitnum;
                    command.Parameters.Add("newUnitnum", SqlDbType.NVarChar).Value = newUnitnum;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        /// <summary>
        /// Used to save a new Appointment to the DB.
        /// </summary>
        /// <returns>new appt ID</returns>
        private int CreateAppointment()
        {
            int newApptId = -1; 
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_createMasteryAppointment", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmdProcedure.Parameters.Add("@Return_Value", SqlDbType.Int);
                    cmdProcedure.Parameters["@Return_Value"].Direction = ParameterDirection.ReturnValue;

                    if (_clinicId.HasValue)
                    {
                        cmdProcedure.Parameters.Add("@clinicId", SqlDbType.Int).Value = _clinicId.Value;
                    }

                    if (!string.IsNullOrEmpty(unitnum))
                    {
                        cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar).Value = unitnum;
                    }

                    cmdProcedure.ExecuteNonQuery();
                    newApptId = (int)cmdProcedure.Parameters["@Return_Value"].Value;
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            } 
            return newApptId;
        }

        public void CreateAppointmentRecordsIfNeeded()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_3_CreateAppointmentRecordsIfNeeded", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.Add("@apptId", SqlDbType.Int).Value = this.apptID;
                    command.Parameters.Add("@riskId", SqlDbType.NVarChar).Value = BuildRiskId();
                    command.Parameters.Add("@patientUnitnum", SqlDbType.NVarChar).Value = this.unitnum;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }

        }

        private string BuildRiskId()
        {
            DateTime now = DateTime.Now;
            return string.Format("{0}{1}{2}{3}{4}{5}{6}", 
                now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, this.apptID);
        }

        public static void DeleteApptData(int apptid, bool keepEmptyAppt)
        {
            //TODO shouldnt be static
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_deleteWebAppointment", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = apptid;
                        cmdProcedure.Parameters.Add("@keepApptRecord", SqlDbType.Bit);
                        cmdProcedure.Parameters["@keepApptRecord"].Value = keepEmptyAppt;
                        cmdProcedure.Parameters.Add("@purgeAuditData", SqlDbType.Bit);
                        cmdProcedure.Parameters["@purgeAuditData"].Value = 0;
                        cmdProcedure.Parameters.Add("@userLoginID", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@userLoginID"].Value = SessionManager.Instance.ActiveUser.userLogin;
                        cmdProcedure.Parameters.Add("@machineNameID", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@machineNameID"].Value = Environment.MachineName;
                        cmdProcedure.Parameters.Add("@applicationID", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@applicationID"].Value = "RiskApps3 - Appointment.cs";
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }
        public void MarkIncomplete()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_3_MarkApptIncomplete", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (this.apptID > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = this.apptID;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        public void MarkComplete()
        {
            //TODO shouldnt be static
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_3_MarkApptComplete", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (this.apptID > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = this.apptID;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }
        public static void MarkPrinted(int apptid)
        {
            //TODO shouldnt be static
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_3_MarkApptPrinted", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = apptid;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        public static void MarkForAutomation(int apptid)
        {
            //TODO shouldnt be static  / get rid of ???
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_3_MarkApptForAutomation", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = apptid;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("apptID", this.apptID);

            DoPersistWithSpAndParams(e, "sp_3_Save_Appointment", ref pc);
        }
    }
}
