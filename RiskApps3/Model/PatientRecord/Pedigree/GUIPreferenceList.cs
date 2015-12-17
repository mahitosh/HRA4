using System.Runtime.Serialization;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord
{
    [CollectionDataContract]
    [KnownType(typeof(GUIPreference))]
    public class GUIPreferenceList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();

        [DataMember] public Patient OwningPatient;

        private object[] constructor_args;

        public GUIPreferenceList() { } // Default constructor for serialization

        public GUIPreferenceList(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] { OwningPatient, false };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadGUIPreferenceList",
                                                pc,
                                                typeof(GUIPreference),
                                                constructor_args);
            DoListLoad(lla);

            foreach(GUIPreference gp in this)
            {
                gp.annotations.BackgroundListLoad();
            }
        }
        /**************************************************************************************************/
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (HraObject o in this)
            {
                ((GUIPreference)o).owningPatient = OwningPatient;
            }
            base.PersistFullList(e);
        }
    }
}
