using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.PatientRecord
{
    [CollectionDataContract]
    [KnownType(typeof(Nation))]
public class NationalityList : HRAList
    {
        /**************************************************************************************************/
        public Person person;

        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        /**************************************************************************************************/
        public NationalityList() { } // Default constructor for serialization

        public NationalityList(Person owner)
        {
            person = owner;
            constructor_args = new object[] {this};
        }

        /**************************************************************************************************/
        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", person.owningFHx.proband.unitnum);
            pc.Add("apptid", person.owningFHx.proband.apptid);
            pc.Add("relativeID", person.relativeID);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadNationality",
                                                pc,
                                                typeof(Nation),
                                                constructor_args);
            DoListLoad(lla);
        }
        /**************************************************************************************************/
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (HraObject o in this)
            {
                ((Nation)o).owner = this;
            }

            base.PersistFullList(e);
        }

        /**************************************************************************************************/
        public string GetSummary()
        {
            string retval = "";
            foreach (Nation n in this)
            {
                retval += n.nation + ", ";
            }
            return retval.Trim(new char[] { ' ', ',' });
        }
    }

    /**************************************************************************************************/
    [DataContract]
    public class Nation : HraObject
    {
        [DataMember] [HraAttribute] public string nation;
        public NationalityList owner;

        /**************************************************************************************************/
        public Nation() { } // Default constructor for serialization

        public Nation(NationalityList nations)
        {
            owner = nations;
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", owner.person.owningFHx.proband.unitnum);
            pc.Add("relativeID", owner.person.relativeID);
            pc.Add("apptid", owner.person.owningFHx.proband.apptid);
            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_Nation",
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
            Nation p = obj as Nation;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (nation == p.nation);
        }

        public bool Equals(Nation p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (nation == p.nation);
        }

    }
}