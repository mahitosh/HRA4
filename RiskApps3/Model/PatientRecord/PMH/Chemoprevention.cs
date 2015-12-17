using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using System.Reflection;
using RiskApps3.View;

namespace RiskApps3.Model.PatientRecord.PMH
{
    [DataContract]
    public class Chemoprevention : HraObject
    {
        /**************************************************************************************************/
        public Patient patientOwning;

        public string patientUnitnum;
        [DataMember] [HraAttribute]  private string takenTamoxifen;
        [DataMember] [HraAttribute]  private string tamoxifenYears;
        [DataMember] [HraAttribute]  private string tamoxifenAge;
        [DataMember] [HraAttribute]  private string takenRaloxifene;
        [DataMember] [HraAttribute]  private string raloxifeneYears;
        [DataMember] [HraAttribute]  private string raloxifeneAge;


        /**************************************************************************************************/
        public Chemoprevention() { } // Default constructor for serialization

        public Chemoprevention(Patient owner)
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
            DoLoadWithSpAndParams("sp_3_LoadChemoprevention", pc);
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("patientUnitnum", patientOwning.unitnum);
            pc.Add("apptid", patientOwning.apptid);
            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_Chemoprevention",
                                      ref pc);

        }

        #region gets/sets
        /*****************************************************/
        public string Chemoprevention_takenTamoxifen
        {
            get
            {
                return takenTamoxifen;
            }
            set
            {
                if (value != takenTamoxifen)
                {
                    takenTamoxifen = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("takenTamoxifen"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Chemoprevention_tamoxifenYears
        {
            get
            {
                return tamoxifenYears;
            }
            set
            {
                if (value != tamoxifenYears)
                {
                    tamoxifenYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("tamoxifenYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Chemoprevention_tamoxifenAge
        {
            get
            {
                return tamoxifenAge;
            }
            set
            {
                if (value != tamoxifenAge)
                {
                    tamoxifenAge = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("tamoxifenAge"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Chemoprevention_takenRaloxifene
        {
            get
            {
                return takenRaloxifene;
            }
            set
            {
                if (value != takenRaloxifene)
                {
                    takenRaloxifene = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("takenRaloxifene"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Chemoprevention_raloxifeneYears
        {
            get
            {
                return raloxifeneYears;
            }
            set
            {
                if (value != raloxifeneYears)
                {
                    raloxifeneYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("raloxifeneYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Chemoprevention_raloxifeneAge
        {
            get
            {
                return raloxifeneAge;
            }
            set
            {
                if (value != raloxifeneAge)
                {
                    raloxifeneAge = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("raloxifeneAge"));
                    SignalModelChanged(args);
                }
            }
        }
        #endregion

    }
}