using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;
using System.Data.SqlClient;
using RiskApps3.Controllers;

namespace RiskApps3.Model.Clinic
{
    public class AppointmentList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;
        public string Date;
        public string NameOrMrn;
        public string groupName;
        public int clinicId;


        public AppointmentList()
        {
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            //if (string.IsNullOrEmpty(Date) == false)
                pc.Add("Date", Date);
            if (string.IsNullOrEmpty(NameOrMrn) == false)
                pc.Add("NameOrMrn", NameOrMrn);
            if (string.IsNullOrEmpty(groupName) == false)
                pc.Add("groupName", groupName);

            if (clinicId != null)
                pc.Add("clinicId", clinicId);

            pc.Add("userLogin", SessionManager.Instance.ActiveUser.userLogin);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadAppointmentList",
                                                pc,
                                                typeof(Appointment),
                                                constructor_args);
            DoListLoad(lla);

            //foreach (Appointment a in this)
            //{
            //    //if (a.apptID != a.GoldenAppt)
            //    //{
            //    //    if (
            //    //    a.patientname = "LEGACY APPT FOR: " + a.patientname;
            //    //}
            //}
        }
    }
}







///**************************************************************************************************/
//        //   Members
//        //
//        public List<Appointment> appointments = new List<Appointment>();
//        public string WhereClaus = "";

//        /**************************************************************************************************/
//        //    HraObject interface
//        // 
//        public override bool LoadObject()
//        {
//            appointments.Clear();
//            return(base.LoadObject());
//        }

//        /**************************************************************************************************/
//        //    BackgroundLoadWork
//        // 
//        public override void BackgroundLoadWork()
//        {
//            string sql = "select apptid, unitnum, patientname, apptPhysName, apptDate, apptTime from tblAppointments ";

//            if (WhereClaus.Length > 0)
//            {
//                sql += ("WHERE " + WhereClaus);
//            }
//            SqlDataReader reader = BCDB2.Instance.ExecuteReader(sql);

//            if (reader != null)
//            {
//                appointments.Clear();
//                while (reader.Read())
//                {
//                    Appointment apt = new Appointment();
//                    if (reader.IsDBNull(0) == false)
//                    {
//                        apt.apptID = reader.GetInt32(0);
//                    } 
//                    if (reader.IsDBNull(1) == false)
//                    {
//                        apt.unitnum = reader.GetString(1);
//                    }
//                    if (reader.IsDBNull(2) == false)
//                    {
//                        apt.patientName = reader.GetString(2);
//                    }
//                    if (reader.IsDBNull(3) == false)
//                    {
//                        apt.apptPhysName = reader.GetString(3);
//                    }
//                    string apptdate = "";
//                    string appttime = "";
//                    if (reader.IsDBNull(4) == false)
//                    {
//                        apptdate = reader.GetString(4);
//                    }
//                    if (reader.IsDBNull(5) == false)
//                    {
//                        appttime = reader.GetString(5);
//                    }
//                    try
//                    {
//                        apt.apptDateTime = DateTime.Parse(apptdate + " " + appttime);
//                    }
//                    catch (Exception ee)
//                    {
//                        Logger.Instance.WriteToLog(ee.ToString());
//                    }
//                    apt.hra_state = States.Ready;

//                    appointments.Add(apt);
//                }
//            }
//            base.BackgroundLoadWork();



//        }