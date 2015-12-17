using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.PatientRecord;
using RiskApps3.View;
using System.Reflection;
using RiskApps3.Model.Security;
using System.ComponentModel;


namespace RiskApps3.Model
{
    public class HraModelChangedEventArgs : EventArgs
    {
        public HraView sendingView;
        public List<MemberInfo> updatedMembers;
        public bool Persist = true;
        public bool Delete = false;

        public HraModelChangedEventArgs(HraView p_sendingView)
        {
            sendingView = p_sendingView;
            updatedMembers = new List<MemberInfo>();
        }

    }

   
    public class HraListChangedEventArgs : EventArgs
    {

        public enum HraListChangeType
        {
            ADD,
            DELETE
        } ;

        public HraListChangeType hraListChangeType;
        public HraObject hraOperand;

        public HraListChangedEventArgs(HraListChangeType hraListChangeType, HraObject hraOperand)
        {
            this.hraListChangeType = hraListChangeType;
            this.hraOperand = hraOperand;
        }

    }

    public class HraListLoadedEventArgs : EventArgs
    {

        public object sender;
        public RunWorkerCompletedEventArgs workerArgs;


    }

    public class NewActivePatientEventArgs : HraModelChangedEventArgs
    {
        public Patient newActivePatient;

        public NewActivePatientEventArgs(Patient p_newActivePatient, SecurityContext p_securityContext)
            : base(null)
        {
            newActivePatient = p_newActivePatient;
        }
    }

    public class RelativeSelectedEventArgs : HraModelChangedEventArgs
    {
        public Person selectedRelative;

        public RelativeSelectedEventArgs(Person p_selectedRelative, SecurityContext p_securityContext)
            : base(null)
        {
            selectedRelative = p_selectedRelative;
        }
    }

    public class AppointmentSelectedEventArgs : HraModelChangedEventArgs
    {
        public int apptID;

        public AppointmentSelectedEventArgs(int p_apptID, SecurityContext p_securityContext)
            : base(null)
        {
            apptID = p_apptID;
        }
    }
}