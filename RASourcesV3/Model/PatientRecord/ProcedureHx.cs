using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization;

using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Utilities;
using System.Reflection;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class ProcedureHx : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public Patient theProband;

        [DataMember] [HraAttribute] public string chestRadiation;
        [DataMember] public BreastBx breastBx;

        /**************************************************************************************************/

        /**************************************************************************************************/
        public override void LoadFullObject()
        {
            base.LoadFullObject();
            breastBx.LoadFullObject();

        }
        /**************************************************************************************************/
        public override void PersistFullObject(HraModelChangedEventArgs e)
        {
            base.PersistFullObject(e);
            breastBx.PersistFullObject(e);
        }
        /*****************************************************/
        public string ProcedureHx_chestRadiation
        {
            get
            {
                return chestRadiation;
            }
            set
            {
                if (value != chestRadiation)
                {
                    chestRadiation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("chestRadiation"));
                    SignalModelChanged(args);
                }
            }
        } 

        /**************************************************************************************************/
        public ProcedureHx() { } // Default constructor for serialization

        public ProcedureHx(Patient owner)
        {
            theProband = owner;

            breastBx = new BreastBx(owner);
        }

        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            breastBx.ReleaseListeners(view);
            base.ReleaseListeners(view);
        }
        
      

        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", theProband.unitnum);
            pc.Add("apptid", theProband.apptid);
            DoLoadWithSpAndParams("sp_3_LoadProcHx", pc);


            breastBx.BackgroundLoadWork();
            base.BackgroundLoadWork();
        }
        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", theProband.unitnum);
            pc.Add("apptid", theProband.apptid);
            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_ProcHx",
                                      ref pc);

        }
    }
}
