using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class Diagnostic : HraObject
    {

        [DataMember] public string reportId = "";
        [DataMember] public string reportIdType = "";
        [DataMember] public string unitnum;

        [DataMember] [HraAttribute] public DateTime date;
        [DataMember] [HraAttribute] public string normal = null;
        [DataMember] [HraAttribute] public string status = null;
        [DataMember] [HraAttribute] public string report = null;

        public Diagnostic() { }  // Default constructor for serialization

        /*****************************************************/
        public DateTime Dx_date
        {
            get
            {
                return date;
            }
            set
            {
                if (value != date)
                {
                    date = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("date"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Dx_normal
        {
            get
            {
                return normal;
            }
            set
            {
                if (value != normal)
                {
                    normal = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("normal"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Dx_status
        {
            get
            {
                return status;
            }
            set
            {
                if (value != status)
                {
                    status = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("status"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Dx_report
        {
            get
            {
                return report;
            }
            set
            {
                if (value != report)
                {
                    report = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("report"));
                    SignalModelChanged(args);
                }
            }
        } 

        public string type
        {
            get
            {
                return GetDiagnosticType();
            }
            set
            {
                SetDiagnosticType(value);
            }
        }
        public virtual string GetDiagnosticType()
        {
            return "";
        }
        public virtual void SetDiagnosticType(string text)
        {

        }
        
        public string value
        {
            get
            {
                return GetValue();
            }
            set
            {
                SetValue(value);
            }
        }
        public virtual string GetValue()
        {
            return "";
        }
        public virtual void SetValue(string text)
        {

        }

        public string laterality
        {
            get
            {
                return GetSide();
            }
            set
            {
                SetSide(value);
            }
        }
        public virtual string GetSide()
        {
            return "";
        }
        public virtual void SetSide(string text)
        {

        }

        public static int CompareDiagnosticByDate(HraObject x, HraObject y)
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
                    int retval = (((Diagnostic)y).date - (((Diagnostic)x).date)).Days;

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {

                        return (((Diagnostic)y).date- (((Diagnostic)x).date)).Minutes;
                    }
                }
            }
        }
    }
}
