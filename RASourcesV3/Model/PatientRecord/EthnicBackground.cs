using System.Runtime.Serialization;

using RiskApps3.View;
using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.PatientRecord
{
    [CollectionDataContract]
    [KnownType(typeof(Race))]
    public class EthnicBackground : HRAList<Race>
    {
        /**************************************************************************************************/
        public Person person;

        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        /**************************************************************************************************/
        
        public EthnicBackground() { } // Default constructor for serialization

        public EthnicBackground(Person owner)
        {
            person = owner;
            constructor_args = new object[] {this};
        }
        
        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", person.owningFHx.proband.unitnum);
            pc.Add("relativeID", person.relativeID);
            pc.Add("apptid", person.owningFHx.proband.apptid);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadEthnicity",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);
        }

        /**************************************************************************************************/
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (HraObject o in this)
            {
                ((Race)o).owner = this;
            }

            base.PersistFullList(e);
        }

        /**************************************************************************************************/
        public string GetSummary()
        {
            string retval = "";
            foreach (Race r in this)
            {
                retval += r.race + ", ";
            }
            return retval.Trim(new char[] { ' ', ',' });
        }
    }

    /**************************************************************************************************/
    [DataContract]
    public class Race : HraObject
    {
        [DataMember] [HraAttribute] public string race;
        public EthnicBackground owner;

        /**************************************************************************************************/
                
        public Race() { } // Default constructor for serialization

        public Race(EthnicBackground ethnicity)
        {
            owner = ethnicity;
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", owner.person.owningFHx.proband.unitnum);
            pc.Add("relativeID", owner.person.relativeID);
            pc.Add("apptid", owner.person.owningFHx.proband.apptid);

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_Race",
                                      ref pc);
        }
        /**************************************************************************************************/
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Race p = obj as Race;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (race == p.race);
        }

        public bool Equals(Race p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (race == p.race);
        }


    }
}