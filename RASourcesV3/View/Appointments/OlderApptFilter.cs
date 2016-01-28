using BrightIdeasSoftware;
using RiskApps3.Model.PatientRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.View.Appointments
{
    public class OlderApptFilter : IModelFilter
    {
        public bool Filter(object x)
        {
            Appointment appt = (Appointment)x;

            if (appt.GoldenAppt != appt.apptID)
            {
                //if (appt.apptdatetime < appt.GoldenApptTime)
                //{
                return false;
                //}
            }
            return true;
        }
    }
}
