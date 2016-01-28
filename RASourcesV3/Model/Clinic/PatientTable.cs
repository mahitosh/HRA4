using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic
{
    public class PatientTable : HraObject
    {
        /**************************************************************************************************/
        //   Members
        //
        public DataTable PatientsTable = new DataTable();


        /**************************************************************************************************/
        //    HraObject interface
        // 
        public override bool LoadObject()
        {
            PatientsTable.Clear();

            return (base.LoadObject());
        }

        /**************************************************************************************************/
        //    BackgroundLoadWork
        // 
        public override void BackgroundLoadWork()
        {
            PatientsTable = BCDB2.Instance.getDataTable(@"SELECT * from dbo.v_3_MaxCompletedAppt");
        }
    }
}
