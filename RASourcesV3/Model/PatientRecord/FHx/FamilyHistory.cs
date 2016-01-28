using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.FHx
{
    [CollectionDataContract(IsReference=true)]
    [KnownType(typeof(HRAList<Person>))]
    [KnownType(typeof(Person))]
    [KnownType(typeof(Patient))]
    public class FamilyHistory : HRAList<Person>
    {
        private object[] constructor_args;
        private ParameterCollection pc;

        /**************************************************************************************************/

        [DataMember]
        public Patient proband;

        [DataMember()]
        public List<Person> Relatives
        {
            get
            {
                return this.Select(p => (Person)p).ToList();
            }
        }

        /**************************************************************************************************/

        public FamilyHistory() { } // Default constructor for serialization

        public FamilyHistory(Patient p_proband)
        {
            proband = p_proband;
        }

        public Person this[int i]
        {
            get
            {
                object person = this.ElementAtOrDefault(i);
                if (person != null)
                {
                    return (Person)person;
                }
                else
                {
                    throw new IndexOutOfRangeException("Person not found in this FamilyHistory.");
                }
            }
            set
            {
                object person = this.ElementAtOrDefault(i);
                if (person != null)
                {
                    person = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Person not found in this FamilyHistory");
                }
            }
        }
        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            if (proband != null)
                proband.ReleaseListeners(view);
    
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (HraObject o in this)
            {
                if (!(o is Patient))
                    o.PersistFullObject(e);
            }
        }

        public override void BackgroundListLoad()
        {
            this.pc = new ParameterCollection("unitnum", this.proband.unitnum);
            pc.Add("apptid", this.proband.apptid);
            
            this.constructor_args = new object[] { this };

            List<Person> initialList = new List<Person>();
            initialList.Add(this.proband);

            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadFamilyHistory",
                this.pc,
                this.constructor_args,
                initialList);

            DoListLoad(lla);
            foreach (Person p in this)
            {
                if (String.IsNullOrEmpty(p.relationship) == false)
                {
                    if (p.relationship.Length > 1)
                        p.Person_relationship = p.relationship.Substring(0, 1).ToUpper() + p.relationship.Substring(1);
                }
                if (String.IsNullOrEmpty(p.relationshipOther) == false)
                {
                    if (p.relationshipOther.Length > 1)
                        p.Person_relationshipOther = p.relationshipOther.Substring(0, 1).ToUpper() + p.relationshipOther.Substring(1);
                }
                if (String.IsNullOrEmpty(p.bloodline) == false)
                {
                    if (p.bloodline.Length > 1)
                        p.Person_bloodline = p.bloodline.Substring(0, 1).ToUpper() + p.bloodline.Substring(1);
                }
            }
            
            AddCoreRelatives();
        }

        protected override void ListLoadingComplete()
        {
            
        }

        /**************************************************************************************************/
        public List<GeneticTestResult> GetUniqueNonNegativeGeneticTestResults(int relativeID)
        {
            List<GeneticTestResult> results = new List<GeneticTestResult>();

            var hasBeenAddedAlready = new ArrayList();
            List<GeneticTestResult> alreadyTested = new List<GeneticTestResult>();

            foreach (Person person in this)
            {
                if (person.relativeID != relativeID)
                {
                    List<GeneticTestResult> relativeResults = person.GetNonNegativeGeneticTestResults();
                    foreach (GeneticTestResult geneticTestResult in relativeResults)
                    {
                        String str = geneticTestResult.geneName + "" + geneticTestResult.mutationName +
                                     geneticTestResult.mutationAA;
                        if (hasBeenAddedAlready.Contains(str) == false)
                        {
                            results.Add(geneticTestResult);
                            hasBeenAddedAlready.Add(str);
                        }
                    }
                } 
                else 
                {
                    alreadyTested.AddRange(person.GetExistingFKMTests());
                }
            }

            results.RemoveAll(t => alreadyTested.Contains(t));
            return results;
        }

        public bool HasUniqueNonNegativeGeneticTestResults(int relativeId)
        {
            foreach (Person p in this)
            {
                if (p.HasNonNegativeGeneticTestResults(p.GetExistingFKMTests())) { return true; }
            }
            return false;
        }

        /**************************************************************************************************/
        private int GetNewRelativeID()
        {
            int retval = 8;

            foreach (Person p in this)
            {
                if (p.relativeID >= retval)
                {
                    retval = p.relativeID + 1;
                }
            }
            return retval;
        }

        /**************************************************************************************************/
        public List<Person> AddChild(Person p, bool createNewSpouse, int count, GenderEnum gender)
        {
            List<Person> retval = new List<Person>();

            Person newSpouse = null;

            for (int i = 0; i < count; i++)
            {
                //int nextID = GetNewRelativeID();
                Person newChild = new Person(this);
                
                //newChild.owningFHx = this;
                //newChild.relativeID = nextID;
                //newChild.vitalStatus = "Alive";
                //this.AddToList(newChild, new HraModelChangedEventArgs(null));
                if (gender == GenderEnum.Male)
                    newChild.gender = "Male";
                else if (gender == GenderEnum.Female)
                    newChild.gender = "Female";
                else
                    newChild.gender = "Unknown";

                if (p.gender == "Female")
                {
                    newChild.motherID = p.relativeID;
                    if (createNewSpouse == false)
                    {
                        foreach (Person sib in this)
                        {
                            if (sib.motherID == newChild.motherID)
                            {
                                newChild.fatherID = sib.fatherID;
                                break;
                            }
                        }
                    }
                    if (newChild.fatherID < 1)
                    {
                        if (newSpouse == null)
                        {
                            newSpouse = CreateSpouse(p);
                            this.AddToList(newSpouse, new HraModelChangedEventArgs(null));
                        }
                        newChild.fatherID = newSpouse.relativeID;
                    }
                }
                else if (p.gender == "Male")
                {
                    newChild.fatherID = p.relativeID;
                    if (createNewSpouse == false)
                    {
                        foreach (Person sib in this)
                        {
                            if (sib.fatherID == newChild.fatherID)
                            {
                                newChild.motherID = sib.motherID;
                                break;
                            }
                        }
                    }
                    if (newChild.motherID < 1)
                    {
                        if (newSpouse == null)
                        {
                            newSpouse = CreateSpouse(p);
                            this.AddToList(newSpouse, new HraModelChangedEventArgs(null));
                        }
                        newChild.motherID = newSpouse.relativeID;
                    }
                }
                int nextID = GetNewRelativeID();
                newChild.owningFHx = this;
                newChild.relativeID = nextID;
                newChild.vitalStatus = "Alive";
                Relationship.SetRelationshipByChildType(Gender.Parse(newChild.gender),
                                                        Relationship.Parse(p.relationship), Bloodline.Parse(p.bloodline),
                                                        out newChild.relationship, out newChild.relationshipOther,
                                                        out newChild.bloodline);

                if (createNewSpouse)
                {
                    if (newChild.relationship == Relationship.toString(RelationshipEnum.SISTER))
                    {
                        newChild.relationship = Relationship.toString(RelationshipEnum.HALF_SISTER);

                        if (p.relationship == Relationship.toString(RelationshipEnum.MOTHER))
                        {
                            newChild.bloodline = Bloodline.toString(Bloodline.BloodlineEnum.Maternal);
                        }
                        else if (p.relationship == Relationship.toString(RelationshipEnum.FATHER))
                        {
                            newChild.bloodline = Bloodline.toString(Bloodline.BloodlineEnum.Paternal);
                        }
                    }
                    else if (newChild.relationship == Relationship.toString(RelationshipEnum.BROTHER))
                    {
                        newChild.relationship = Relationship.toString(RelationshipEnum.HALF_BROTHER);
                        if (p.relationship == Relationship.toString(RelationshipEnum.MOTHER))
                        {
                            newChild.bloodline = Bloodline.toString(Bloodline.BloodlineEnum.Maternal);
                        }
                        else if (p.relationship == Relationship.toString(RelationshipEnum.FATHER))
                        {
                            newChild.bloodline = Bloodline.toString(Bloodline.BloodlineEnum.Paternal);
                        }
                    }
                }

                this.AddToList(newChild, new HraModelChangedEventArgs(null));
                newChild.HraState = HraObject.States.Ready;
                retval.Add(newChild);
            }
            if (newSpouse != null)
            {
                newSpouse.HraState = HraObject.States.Ready;
                retval.Add(newSpouse);
            }

            return retval;
        }

        /**************************************************************************************************/
        public Person CreateSpouse(Person p)
        {
            Person spouse = new Person();
            spouse.owningFHx = this;
            spouse.relativeID = GetNewRelativeID();

            if (p.gender == "Female")
                spouse.gender = "Male";
            else if (p.gender == "Male")
                spouse.gender = "Female";
            else
                spouse.gender = "Unknown";

            spouse.relationship = "Other";
            spouse.relationshipOther = "Spouse of " + p.relationship;

            return spouse;
        }

        /**************************************************************************************************/
        public List<Person> AddParents(Person p, PedigreeModel model)
        {
            List<Person> retval = new List<Person>();

            int nextID = GetNewRelativeID();
            Person newMom = new Person(this);
            newMom.HraState = HraObject.States.Ready;
            newMom.relativeID = nextID;
            newMom.motherID = 0;
            newMom.fatherID = 0;
            newMom.owningFHx = this;
            //p.Person_motherID = newMom.relativeID;
            p.motherID = newMom.relativeID;
            newMom.vitalStatus = "Alive";
            newMom.gender = "Female";
            newMom.relationship = "Other";

            Relationship.SetRelationshipByParentType(GenderEnum.Female, Relationship.Parse(p.relationship), Bloodline.Parse(p.bloodline), out newMom.relationship, out newMom.relationshipOther, out newMom.bloodline);

            if (model != null)
            {
                foreach (PedigreeIndividual pi in model.individuals)
                {
                    if (pi.HraPerson == p)
                    {
                        double x = pi.point.x;
                        double y = pi.point.y - model.parameters.verticalSpacing;
                        x += (model.parameters.horizontalSpacing / 2);
                        newMom.x_position = model.displayXMax / x;
                        newMom.y_position = model.displayYMax / y;
                        newMom.x_norm = (int)(x - (model.displayXMax / 2));
                        newMom.y_norm = (int)(y - (model.displayYMax / 2));
                        break;
                    }
                }

            }

            this.AddToList(newMom, new HraModelChangedEventArgs(null));
            retval.Add(newMom);

            nextID = GetNewRelativeID();
            Person newDad = new Person(this);
            newDad.HraState = HraObject.States.Ready;
            newDad.relativeID = nextID;
            newDad.motherID = 0;
            newDad.fatherID = 0;
            newDad.owningFHx = this;
            newDad.relationship = "Other";
            //p.Person_fatherID = newDad.relativeID;
            p.fatherID = newDad.relativeID;
            newDad.vitalStatus = "Alive";
            newDad.gender = "Male";

            Relationship.SetRelationshipByParentType(GenderEnum.Male, Relationship.Parse(p.relationship), Bloodline.Parse(p.bloodline), out newDad.relationship, out newDad.relationshipOther, out newDad.bloodline);

            if (model != null)
            {
                foreach (PedigreeIndividual pi in model.individuals)
                {
                    if (pi.HraPerson == p)
                    {
                        double x = pi.point.x;
                        double y = pi.point.y - model.parameters.verticalSpacing;
                        x -= (model.parameters.horizontalSpacing / 2);
                        newDad.x_position = model.displayXMax / x;
                        newDad.y_position = model.displayYMax / y;
                        newDad.x_norm = (int)(x - (model.displayXMax / 2));
                        newDad.y_norm = (int)(y - (model.displayYMax / 2));
                        break;
                    }
                }
            }

            this.AddToList(newDad, new HraModelChangedEventArgs(null));
            retval.Add(newDad);







            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.updatedMembers.Add(p.GetMemberByName("motherID"));
            args.updatedMembers.Add(p.GetMemberByName("fatherID"));
            p.SignalModelChanged(args);
            return retval;
        }

        /**************************************************************************************************/
        public void AddCoreRelatives()
        {
            bool createMother = true;
            bool createFather = true;
            bool createMatGranMother = true;
            bool createMatGranFather = true;
            bool createPatGranMother = true;
            bool createPatGranFather = true;


            foreach (Person p in this)
            {
                if (p.relationship != null)
                {
                    switch (p.relationship.ToLower())
                    {
                        case "self":
                            break;
                        case "mother":
                            createMother = false;
                            break;
                        case "father":
                            createFather = false;
                            break;
                        case "grandmother":
                            if (p.bloodline == "Maternal")
                                createMatGranMother = false;
                            else if (p.bloodline == "Paternal")
                                createPatGranMother = false;
                            break;
                        case "grandfather":
                            if (p.bloodline == "Maternal")
                                createMatGranFather = false;
                            else if (p.bloodline == "Paternal")
                                createPatGranFather = false;
                            break;
                    }
                }
            }



            if (createMother)
            {
                this.Add(AddStockByType("Mother", "", 2, 4, 5));
                this.proband.motherID = 2;
            }
            if (createFather)
            {
                this.Add(AddStockByType("Father", "", 3, 6, 7));
                this.proband.fatherID = 3;
            }
            if (createMatGranMother)
                this.Add(AddStockByType("Grandmother", "Maternal", 4, 0, 0));
            if (createMatGranFather)
                this.Add(AddStockByType("Grandfather", "Maternal", 5, 0, 0));
            if (createPatGranMother)
                this.Add(AddStockByType("Grandmother", "Paternal", 6, 0, 0));
            if (createPatGranFather)
                this.Add(AddStockByType("Grandfather", "Paternal", 7, 0, 0));  
            
            if (createMother ||
                createFather ||
                createMatGranMother ||
                createMatGranFather ||
                createPatGranMother ||
                createPatGranFather)
            {
                this.proband.BackgroundPersistWork(new HraModelChangedEventArgs(null));
            }         
        }

        /**************************************************************************************************/

        private Person AddStockByType(string new_rel_type, string bloodline, int relId, int motherId, int fatherId)
        {
            RelationshipEnum re = Relationship.Parse(new_rel_type);
            GenderEnum ge = Relationship.getGenderFromRelationshipType(re);

            Person rel = new Person();
            rel.owningFHx = this;
            rel.relativeID = relId;
            rel.bloodline = bloodline;
            rel.relationship = new_rel_type;
            rel.gender = Gender.ToString(ge);
            rel.motherID = motherId;
            rel.fatherID = fatherId;
            rel.HraState = HraObject.States.Ready;

            rel.BackgroundPersistWork(new HraModelChangedEventArgs(null));
            return rel;
        }

        /**************************************************************************************************/

        public List<Person> AddRelativeByType(string new_rel_type, string bloodline, int count)
        {
            List<Person> retval = new List<Person>();

            RelationshipEnum re = Relationship.Parse(new_rel_type);
            GenderEnum ge = Relationship.getGenderFromRelationshipType(re);
            if (Relationship.isOffspring(re))
            {
                Person newSpouse = null;
                foreach (Person q in this)
                {
                    if (q.relationshipOther == "Spouse of Self")
                    {
                        newSpouse = q;
                        break;
                    }
                }
                if (newSpouse == null)
                {
                    newSpouse = CreateSpouse(proband);
                    this.AddToList(newSpouse, new HraModelChangedEventArgs(null));
                }

                for (int i = 0; i < count; i++)
                {
                    int nextID = GetNewRelativeID();
                    Person newRel = new Person(this);
                    newRel.HraState = HraObject.States.Ready;
                    newRel.relativeID = nextID;
                    newRel.motherID = 0;
                    newRel.fatherID = 0;

                    newRel.owningFHx = this;
                    newRel.vitalStatus = "Alive";
                    newRel.gender = Gender.ToString(ge);
                    newRel.relationship = new_rel_type;
                    newRel.bloodline = bloodline;

                    if (proband.gender == "Female")
                    {
                        newRel.motherID = proband.relativeID;
                        newRel.fatherID = newSpouse.relativeID;
                    }
                    else
                    {
                        newRel.motherID = newSpouse.relativeID;
                        newRel.fatherID = proband.relativeID;
                    }

                    this.AddToList(newRel, new HraModelChangedEventArgs(null));
                    retval.Add(newRel);
                }
                //AddChild(proband, false, count, ge);
                //List<Person> newFolks = AddChild(proband, false, count, ge);
                //foreach (Person np in newFolks)
                //{
                //    retval.Add(np);
                //    //np.SignalModelChanged(new HraModelChangedEventArgs(null));
                //}
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    int nextID = GetNewRelativeID();
                    Person newRel = new Person(this);
                    newRel.HraState = HraObject.States.Ready;
                    newRel.relativeID = nextID;
                    newRel.motherID = 0;
                    newRel.fatherID = 0;
                    
                    newRel.owningFHx = this;
                    newRel.vitalStatus = "Alive";
                    newRel.gender = Gender.ToString(ge);
                    newRel.relationship = new_rel_type;
                    newRel.bloodline = bloodline;
                    SetIDsFromRelationship(ref newRel);
                    this.AddToList(newRel, new HraModelChangedEventArgs(null));
                    retval.Add(newRel);
                }
            }
            return retval;
        }
                /**************************************************************************************************/
        public void SetIDsFromRelationship(ref Person newRel)
        {
            if (newRel != null)
            {
                Person self = null;
                Person spouse = null;
                Person mom = null;
                Person dad = null;
                Person matGmom = null;
                Person patGmom = null;
                Person matGdad = null;
                Person patGdad = null;
                Person matGmomMom = null;
                Person patGmomDad = null;
                Person matGdadMom = null;
                Person patGdadDad = null;
                Person matGmomDad = null;
                Person patGmomMom = null;
                Person matGdadDad = null;
                Person patGdadMom = null;

                //set self mom and dad ids
                foreach (Person p in this)
                {
                    switch (Relationship.Parse(p.relationship))
                    {
                        case RelationshipEnum.SELF:
                            self = p;
                            break;
                        case RelationshipEnum.MOTHER:
                            mom = p;
                            break;
                        case RelationshipEnum.FATHER:
                            dad = p;
                            break;
                        case RelationshipEnum.GRANDMOTHER:
                            if (p.bloodline == "Maternal")
                                matGmom = p;
                            else if (p.bloodline == "Paternal")
                                patGmom = p;
                            break;
                        case RelationshipEnum.GRANDFATHER:
                            if (p.bloodline == "Maternal")
                                matGdad = p;
                            else if (p.bloodline == "Paternal")
                                patGdad = p;
                            break;
                        case RelationshipEnum.SPOUSE:
                            spouse = p;
                            break;
                        case RelationshipEnum.GRANDFATHERS_FATHER:
                            if (p.bloodline == "Maternal")
                                matGdadDad = p;
                            else if (p.bloodline == "Paternal")
                                patGdadDad = p;
                            break;
                        case RelationshipEnum.GRANDFATHERS_MOTHER:
                            if (p.bloodline == "Maternal")
                                matGdadMom = p;
                            else if (p.bloodline == "Paternal")
                                patGdadMom = p;
                            break;
                        case RelationshipEnum.GRANDMOTHERS_FATHER:
                            if (p.bloodline == "Maternal")
                                matGmomDad = p;
                            else if (p.bloodline == "Paternal")
                                patGmomDad = p;
                            break;
                        case RelationshipEnum.GRANDMOTHERS_MOTHER:
                            if (p.bloodline == "Maternal")
                                matGmomMom = p;
                            else if (p.bloodline == "Paternal")
                                patGmomMom = p;
                            break;
                    }
                }


                RelationshipEnum re = Relationship.Parse(newRel.relationship);
                if (String.IsNullOrEmpty(newRel.gender))
                {
                    newRel.gender = Relationship.getGenderFromRelationshipType(re).ToString();
                }

                    switch (re)
                    {
                        case RelationshipEnum.SELF:
                        case RelationshipEnum.BROTHER:
                        case RelationshipEnum.SISTER:
                            if (mom != null)
                                newRel.motherID = mom.relativeID;
                            if (dad != null)
                                newRel.fatherID = dad.relativeID;
                            break;
                        case RelationshipEnum.DAUGHTER:
                        case RelationshipEnum.SON:
                            if (self.gender == "Female")
                            {
                                newRel.motherID = self.relativeID;
                            }
                            else if (self.gender == "Male")
                            {
                                newRel.fatherID = self.relativeID;
                            }
                            break;

                        case RelationshipEnum.MOTHER:
                            if (matGmom != null)
                                newRel.motherID = matGmom.relativeID;
                            if (matGdad != null)
                                newRel.fatherID = matGdad.relativeID;
                            break;

                        case RelationshipEnum.FATHER:
                            if (patGmom != null)
                                newRel.motherID = patGmom.relativeID;
                            if (patGdad != null)
                                newRel.fatherID = patGdad.relativeID;
                            break;

                        case RelationshipEnum.AUNT:
                        case RelationshipEnum.UNCLE:
                            if (newRel.bloodline == "Maternal")
                            {
                                if (matGmom != null)
                                    newRel.motherID = matGmom.relativeID;
                                if (matGdad != null)
                                    newRel.fatherID = matGdad.relativeID;
                            }
                            else if (newRel.bloodline == "Paternal")
                            {
                                if (patGmom != null)
                                    newRel.motherID = patGmom.relativeID;
                                if (patGdad != null)
                                    newRel.fatherID = patGdad.relativeID;
                            }
                            break;

                        case RelationshipEnum.GRANDMOTHER:
                            if (newRel.bloodline == "Maternal")
                            {
                                if (matGmomMom != null)
                                    newRel.motherID = matGmomMom.relativeID;
                                if (matGmomDad != null)
                                    newRel.fatherID = matGmomDad.relativeID;
                            }
                            else if (newRel.bloodline == "Paternal")
                            {
                                if (patGmomMom != null)
                                    newRel.motherID = patGmomMom.relativeID;
                                if (patGmomDad != null)
                                    newRel.fatherID = patGmomDad.relativeID;
                            }
                            break;

                        case RelationshipEnum.GRANDFATHER:
                            if (newRel.bloodline == "Maternal")
                            {
                                if (matGdadMom != null)
                                    newRel.motherID = matGdadMom.relativeID;
                                if (matGdadDad != null)
                                    newRel.fatherID = matGdadDad.relativeID;
                            }
                            else if (newRel.bloodline == "Paternal")
                            {
                                if (patGdadMom != null)
                                    newRel.motherID = patGdadMom.relativeID;
                                if (patGdadDad != null)
                                    newRel.fatherID = patGdadDad.relativeID;
                            }
                            break;
                    }
                



            }
        }
        /**************************************************************************************************/
        public void SetIDsFromRelationships()
        {
            Person self = null;
            Person spouse = null;
            Person mom = null;
            Person dad = null;
            Person matGmom = null;
            Person patGmom = null;
            Person matGdad = null;
            Person patGdad = null;
            Person matGmomMom = null;
            Person patGmomDad = null;
            Person matGdadMom = null;
            Person patGdadDad = null;
            Person matGmomDad = null;
            Person patGmomMom = null;
            Person matGdadDad = null;
            Person patGdadMom = null;

            //set self mom and dad ids
            foreach (Person p in this)
            {
                switch (Relationship.Parse(p.relationship))
                {
                    case RelationshipEnum.SELF:
                        self = p;
                        break;
                    case RelationshipEnum.MOTHER:
                        mom = p;
                        break;
                    case RelationshipEnum.FATHER:
                        dad = p;
                        break;
                    case RelationshipEnum.GRANDMOTHER:
                        if (p.bloodline == "Maternal")
                            matGmom = p;
                        else if (p.bloodline == "Paternal")
                            patGmom = p;
                        break;
                    case RelationshipEnum.GRANDFATHER:
                        if (p.bloodline == "Maternal")
                            matGdad = p;
                        else if (p.bloodline == "Paternal")
                            patGdad = p;
                        break;
                    case RelationshipEnum.SPOUSE:
                        spouse = p;
                        break;
                    case RelationshipEnum.GRANDFATHERS_FATHER:
                        if (p.bloodline == "Maternal")
                            matGdadDad = p;
                        else if (p.bloodline == "Paternal")
                            patGdadDad = p;
                        break;
                    case RelationshipEnum.GRANDFATHERS_MOTHER:
                        if (p.bloodline == "Maternal")
                            matGdadMom = p;
                        else if (p.bloodline == "Paternal")
                            patGdadMom = p;
                        break;
                    case RelationshipEnum.GRANDMOTHERS_FATHER:
                        if (p.bloodline == "Maternal")
                            matGmomDad = p;
                        else if (p.bloodline == "Paternal")
                            patGmomDad = p;
                        break;
                    case RelationshipEnum.GRANDMOTHERS_MOTHER:
                        if (p.bloodline == "Maternal")
                            matGmomMom = p;
                        else if (p.bloodline == "Paternal")
                            patGmomMom = p;
                        break;
                }
            }
            lock (this)
            {
                foreach (Person p in this)
                {
                    RelationshipEnum re = Relationship.Parse(p.relationship);
                    if (String.IsNullOrEmpty(p.gender))
                    {
                        p.gender = Relationship.getGenderFromRelationshipType(re).ToString();
                    }
                    if (p.motherID <= 0 && p.fatherID <= 0)
                    {
                        switch (re)
                        {
                            case RelationshipEnum.SELF:
                            case RelationshipEnum.BROTHER:
                            case RelationshipEnum.SISTER:
                                if (mom != null)
                                    p.Person_motherID = mom.relativeID;
                                if (dad != null)
                                    p.Person_fatherID = dad.relativeID;
                                break;

                            case RelationshipEnum.MOTHER:
                                if (matGmom != null)
                                    p.Person_motherID = matGmom.relativeID;
                                if (matGdad != null)
                                    p.Person_fatherID = matGdad.relativeID;
                                break;

                            case RelationshipEnum.FATHER:
                                if (patGmom != null)
                                    p.Person_motherID = patGmom.relativeID;
                                if (patGdad != null)
                                    p.Person_fatherID = patGdad.relativeID;
                                break;

                            case RelationshipEnum.AUNT:
                            case RelationshipEnum.UNCLE:
                                if (p.bloodline == "Maternal")
                                {
                                    if (matGmom != null)
                                        p.Person_motherID = matGmom.relativeID;
                                    if (matGdad != null)
                                        p.Person_fatherID = matGdad.relativeID;
                                }
                                else if (p.bloodline == "Paternal")
                                {
                                    if (patGmom != null)
                                        p.Person_motherID = patGmom.relativeID;
                                    if (patGdad != null)
                                        p.Person_fatherID = patGdad.relativeID;
                                }
                                break;

                            case RelationshipEnum.GRANDMOTHER:
                                if (p.bloodline == "Maternal")
                                {
                                    if (matGmomMom != null)
                                        p.Person_motherID = matGmomMom.relativeID;
                                    if (matGmomDad != null)
                                        p.Person_fatherID = matGmomDad.relativeID;
                                }
                                else if (p.bloodline == "Paternal")
                                {
                                    if (patGmomMom != null)
                                        p.Person_motherID = patGmomMom.relativeID;
                                    if (patGmomDad != null)
                                        p.Person_fatherID = patGmomDad.relativeID;
                                }
                                break;

                            case RelationshipEnum.GRANDFATHER:
                                if (p.bloodline == "Maternal")
                                {
                                    if (matGdadMom != null)
                                        p.Person_motherID = matGdadMom.relativeID;
                                    if (matGdadDad != null)
                                        p.Person_fatherID = matGdadDad.relativeID;
                                }
                                else if (p.bloodline == "Paternal")
                                {
                                    if (patGdadMom != null)
                                        p.Person_motherID = patGdadMom.relativeID;
                                    if (patGdadDad != null)
                                        p.Person_fatherID = patGdadDad.relativeID;
                                }
                                break;
                        }
                    }
                }
            }
        }

        
        public void GetDescendants(int parent, ref List<int> descendants)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].motherID == parent || this[i].fatherID == parent)
                {
                    descendants.Add(this[i].relativeID);
                    GetDescendants(this[i].relativeID, ref descendants);
                }
            }
        }

        public Person getRelative(int relativeID)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].relativeID == relativeID)
                {
                    return this[i];
                }
            }
            return null;
        }

        internal void LinkRelative(Person person, PedigreeCouple newParents)
        {
            person.Person_motherID = newParents.mother.HraPerson.relativeID;
            person.Person_fatherID = newParents.father.HraPerson.relativeID;
            string newRelationsip = "";
            string newRelationsipOther = "";
            string newBloodline = "";

            //newParents.children.Add(
            Relationship.SetRelationshipByChildType(Gender.Parse(person.gender),
                                                    Relationship.Parse(newParents.mother.HraPerson.relationship), Bloodline.Parse(newParents.mother.HraPerson.bloodline),
                                                    out newRelationsip, out newRelationsipOther,
                                                    out newBloodline);

            person.Person_relationship = newRelationsip;
            person.Person_relationshipOther = newRelationsipOther;
            person.Person_bloodline = newBloodline;

        }

        internal Person LinkRelative(Person person, PedigreeIndividual single_parent)
        {
            Person newSpouse = null;
            newSpouse = CreateSpouse(single_parent.HraPerson);
            newSpouse.x_position = single_parent.HraPerson.x_position;
            newSpouse.y_position = single_parent.HraPerson.y_position;
            newSpouse.x_norm = single_parent.HraPerson.x_norm;
            newSpouse.y_norm = single_parent.HraPerson.y_norm;

            this.AddToList(newSpouse, new HraModelChangedEventArgs(null));
            newSpouse.HraState = HraObject.States.Ready;

            if (single_parent.HraPerson.gender == "Male")
            {
                person.Person_fatherID = single_parent.HraPerson.relativeID;
                person.Person_motherID = newSpouse.relativeID;
            }
            else
            {
                person.Person_motherID = single_parent.HraPerson.relativeID;
                person.Person_fatherID = newSpouse.relativeID;
            }

            string newRelationsip = "";
            string newRelationsipOther = "";
            string newBloodline = "";

            //newParents.children.Add(
            Relationship.SetRelationshipByChildType(Gender.Parse(person.gender),
                                                    Relationship.Parse(single_parent.HraPerson.relationship), Bloodline.Parse(single_parent.HraPerson.bloodline),
                                                    out newRelationsip, out newRelationsipOther,
                                                    out newBloodline);

            person.Person_relationship = newRelationsip;
            person.Person_relationshipOther = newRelationsipOther;
            person.Person_bloodline = newBloodline;


            return newSpouse;
        }

        private Dictionary<GeneticTestResult, List<Person>> familialVariants;

        /// <summary>
        /// Uses this Family history to develop a list of genetic variants 
        /// unique to this family with no duplication.
        /// </summary>
        /// <returns>
        /// A dictionary mapping unique variants to the list 
        /// of people with that variant
        /// </returns>
        /// <remarks>Caches the result in an instance member for quick access</remarks>
        internal Dictionary<GeneticTestResult, List<Person>> ReloadFamilialVariants()
        {
            Dictionary<GeneticTestResult, List<Person>> variants = new Dictionary<GeneticTestResult, List<Person>>();

            List<GeneticTest> tests = this.Relatives
                .SelectMany(relative => relative.PMH.GeneticTests)
                .ToList();

            foreach (GeneticTest test in tests)
            {
                GeneticTest geneticTest = test;
                if (geneticTest != null)
                {
                    IEnumerable<GeneticTestResult> results = geneticTest.GeneticTestResults.Where(result => !String.IsNullOrEmpty(result.mutationName));
                    UpdateVariantsWithResults(variants, results);
                }
            }
            this.familialVariants = variants;
            return variants;
        }

        /// <summary>
        /// Uses the FamilialVariants found in this family to create a textual label
        /// to display the variants in the same order that they will appear in the
        /// pedigree gpyphs
        /// </summary>
        /// <remarks>To guarantee accuracy call <code>ReloadFamilialVariants</code> prior
        /// to calling  this function.</remarks>
        internal string BuildFamilialVariantsLabel()
        {
            string label = "";
            if (this.familialVariants == null)
            {
                this.ReloadFamilialVariants();
            }
            foreach (GeneticTestResult pos in this.familialVariants.Keys)
            {
                if (String.Compare(pos.resultSignificance, "Negative", true) != 0 &&
                    String.Compare(pos.resultSignificance, "Neg", true) != 0)
                {
                    label += pos.geneName + " " + pos.mutationName + " " + pos.resultSignificance + "\n";
                }
            }
            return label;
        }

        private static void UpdateVariantsWithResults(Dictionary<GeneticTestResult, List<Person>> variants, IEnumerable<GeneticTestResult> results)
        {
            foreach (GeneticTestResult result in results.Where(r => !String.IsNullOrEmpty(r.resultSignificance) || !String.IsNullOrEmpty(r.ASOResultSignificance)))
            {
                if (variants.Keys.Contains(result))
                {
                    Person owningRelative = result.owningGeneticTest.owningPMH.RelativeOwningPMH;
                    if (!variants[result].Contains(owningRelative))
                    {
                        variants[result].Add(result.owningGeneticTest.owningPMH.RelativeOwningPMH);
                    }
                }
                else
                {
                    variants.Add(result, new List<Person>());
                    variants[result].Add(result.owningGeneticTest.owningPMH.RelativeOwningPMH);
                }
            }
        }

        public static int XmlImport(string fileName, int apptid, string existingUnitnum)
        {
            //TODO create an importer class
            DataContractSerializer ds = new DataContractSerializer(typeof(FamilyHistory));
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            FamilyHistory fhx;
            try
            {
                fhx = (FamilyHistory)ds.ReadObject(fs);
            }
            catch (Exception)  //catch exception where cdsBreastOvary data is older version
            {
                fs.Flush();
                fs.Position = 0;
                XDocument doc;
                using (XmlReader reader = XmlReader.Create(fs))
                {
                    doc = XDocument.Load(reader);
                }

                doc.XPathSelectElement("//*[local-name() = 'cdsBreastOvary']").Remove();

                var xmlDocument = new XmlDocument();
                using (var xmlReader = doc.CreateReader())
                {
                    xmlDocument.Load(xmlReader);
                }

                MemoryStream ms = new MemoryStream();
                xmlDocument.Save(ms);
                ms.Flush();
                ms.Position = 0;
                fhx = (FamilyHistory)ds.ReadObject(ms);
            }

            foreach (Patient patient in fhx.OfType<Patient>())
            {
                fhx.proband = patient;
            }
            fhx.proband.apptid = apptid;
            Appointment.DeleteApptData(apptid, true);
            if (fhx.proband.unitnum == null)  //no unitnum happens when importing from de-identified XML
            {
                fhx.proband.unitnum = existingUnitnum;  //just continue to use the existing unitnum for the appt we're overwriting
            }
            fhx.UpdateAppointmentUnitnum(fhx.proband.unitnum);
            fhx.proband.PersistFullObject(new HraModelChangedEventArgs(null));

            fs.Close();

            return fhx.proband.apptid;
        }

        /// <summary>
        /// Changes the unitnum only for the appointment associated with this family.
        /// </summary>
        private void UpdateAppointmentUnitnum(string unitnum)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_updateApptUnitnum", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (this.proband.apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = this.proband.apptid;
                        cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@unitnum"].Value = unitnum;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }
    }
}