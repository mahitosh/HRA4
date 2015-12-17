using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using RiskApps3.View;
using RiskApps3.Utilities;
using System.Reflection;

namespace RiskApps3.Model.PatientRecord
{
    //deprecated

    //public class Diet : HraObject
    //{
    //    /**************************************************************************************************/
    //    public Patient patientOwningSHx;

    //    [HraAttribute] public string patientUnitnum;
    //    [HraAttribute] public string vegetableServingsPerDay;

    //    /**************************************************************************************************/
    //    public Diet(Patient owner)
    //    {
    //        patientOwningSHx = owner;

    //    }

    //    /**************************************************************************************************/
    //    public void RemoveViewHandlers(HraView view)
    //    {
    //        base.ReleaseListeners(view);
    //    }

    //    /**************************************************************************************************/
    //    public override void BackgroundLoadWork()
    //    {
    //        ParameterCollection pc = new ParameterCollection("unitnum", patientOwningSHx.unitnum);
    //        pc.Add("apptid", patientOwningSHx.apptid);

    //        DoLoadWithSpAndParams("sp_3_LoadDiet", pc);
    //    }
       
    //}
}
