using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic
{
    public class PatientList : HraObject
    {
        /**************************************************************************************************/
        //   Members
        //
        public List<PatientListEntry> patients = new List<PatientListEntry>();
        public string WhereClaus = "";
        public bool LookByUser = false;
        public string user = "";

        /**************************************************************************************************/
        //    HraObject interface
        // 
        public override bool LoadObject()
        {
            patients.Clear();

            return (base.LoadObject());
        }

        /**************************************************************************************************/
        //    BackgroundLoadWork
        // 
        public override void BackgroundLoadWork()
        {
            string sql;

            if (LookByUser)
            {
                sql = "SELECT unitnum FROM v_3_PatientListByUser GROUP BY unitnum ";
                if (WhereClaus.Length > 0)
                {
                    sql += (" AND " + WhereClaus);
                }

            }
            else
            {
                //old way: sql = "SELECT unitnum, CONVERT(datetime,MAX(dob)) AS dob, MAX(patientname) AS patientname FROM tblAppointments ";
                sql = @"SELECT  unitnum,
	                    MAX(CASE
		                    WHEN ISDATE(dob) = 1 THEN CONVERT(datetime,dob)
		                    ELSE NULL
		                    END) as dob,
	                    MAX(patientname) AS patientname
                    FROM tblAppointments ";
                if (WhereClaus.Length > 0)
                {
                    sql += ("WHERE " + WhereClaus);
                } 

                 sql += "GROUP BY unitnum";
            }

            using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(sql))
            {
                if (reader != null)
                {
                    patients.Clear();
                    while (reader.Read())
                    {

                        PatientListEntry ple = new PatientListEntry();
                        if (reader.IsDBNull(0) == false)
                        {
                            ple.unitnum = reader.GetString(0);
                        }
                        if (reader.IsDBNull(1) == false)
                        {
                            ple.dob = reader.GetDateTime(1);
                        }
                        if (reader.IsDBNull(2) == false)
                        {
                            ple.name = reader.GetString(2);
                        }

                        patients.Add(ple);
                    }
                }
            }
            base.BackgroundLoadWork();



 
        }
    }
    public class PatientListEntry
    {
        public string name;
        public DateTime dob;
        public string unitnum;
    }
}

