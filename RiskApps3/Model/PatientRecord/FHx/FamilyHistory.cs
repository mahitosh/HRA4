using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using System.Reflection;
using System.Data;
using System.Runtime.Serialization;

using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Model.PatientRecord.FHx
{
    [CollectionDataContract(IsReference=true)]
    [KnownType(typeof(HRAList))]
    [KnownType(typeof(Person))]
    [KnownType(typeof(Patient))]
    public class FamilyHistory : HRAList
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

            List<HraObject> initialList = new List<HraObject>();
            initialList.Add(this.proband);

            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadFamilyHistory",
                this.pc,
                typeof(Person),
                this.constructor_args,
                initialList);

            DoListLoad(lla);
            foreach (Person p in this)
            {
                if (string.IsNullOrEmpty(p.relationship) == false)
                {
                    if (p.relationship.Length > 1)
                        p.Person_relationship = p.relationship.Substring(0, 1).ToUpper() + p.relationship.Substring(1);
                }
                if (string.IsNullOrEmpty(p.relationshipOther) == false)
                {
                    if (p.relationshipOther.Length > 1)
                        p.Person_relationshipOther = p.relationshipOther.Substring(0, 1).ToUpper() + p.relationshipOther.Substring(1);
                }
                if (string.IsNullOrEmpty(p.bloodline) == false)
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
        public int GetNewRelativeID() // Silicus change on 03-02-16
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
                            newChild.bloodline = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Maternal);
                        }
                        else if (p.relationship == Relationship.toString(RelationshipEnum.FATHER))
                        {
                            newChild.bloodline = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Paternal);
                        }
                    }
                    else if (newChild.relationship == Relationship.toString(RelationshipEnum.BROTHER))
                    {
                        newChild.relationship = Relationship.toString(RelationshipEnum.HALF_BROTHER);
                        if (p.relationship == Relationship.toString(RelationshipEnum.MOTHER))
                        {
                            newChild.bloodline = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Maternal);
                        }
                        else if (p.relationship == Relationship.toString(RelationshipEnum.FATHER))
                        {
                            newChild.bloodline = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Paternal);
                        }
                    }
                }

                this.AddToList(newChild, new HraModelChangedEventArgs(null));
                newChild.HraState = RiskApps3.Model.HraObject.States.Ready;
                retval.Add(newChild);
            }
            if (newSpouse != null)
            {
                newSpouse.HraState = RiskApps3.Model.HraObject.States.Ready;
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
            newMom.HraState = RiskApps3.Model.HraObject.States.Ready;
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
            newDad.HraState = RiskApps3.Model.HraObject.States.Ready;
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
            rel.gender = Gender.toString(ge);
            rel.motherID = motherId;
            rel.fatherID = fatherId;
            rel.HraState = RiskApps3.Model.HraObject.States.Ready;

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
                    newRel.HraState = RiskApps3.Model.HraObject.States.Ready;
                    newRel.relativeID = nextID;
                    newRel.motherID = 0;
                    newRel.fatherID = 0;

                    newRel.owningFHx = this;
                    newRel.vitalStatus = "Alive";
                    newRel.gender = Gender.toString(ge);
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
                    newRel.HraState = RiskApps3.Model.HraObject.States.Ready;
                    newRel.relativeID = nextID;
                    newRel.motherID = 0;
                    newRel.fatherID = 0;
                    
                    newRel.owningFHx = this;
                    newRel.vitalStatus = "Alive";
                    newRel.gender = Gender.toString(ge);
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
                if (string.IsNullOrEmpty(newRel.gender))
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
                    if (string.IsNullOrEmpty(p.gender))
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
            newSpouse.HraState = RiskApps3.Model.HraObject.States.Ready;

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

            List<HraObject> tests = this.Relatives
                .SelectMany(relative => relative.PMH.GeneticTests)
                .ToList();

            foreach (HraObject test in tests)
            {
                GeneticTest geneticTest = test as GeneticTest;
                if (geneticTest != null)
                {
                    IEnumerable<GeneticTestResult> results = geneticTest.GeneticTestResults.Where(result => !string.IsNullOrEmpty(result.mutationName));
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
                if (string.Compare(pos.resultSignificance, "Negative", true) != 0 &&
                    string.Compare(pos.resultSignificance, "Neg", true) != 0)
                {
                    label += pos.geneName + " " + pos.mutationName + " " + pos.resultSignificance + "\n";
                }
            }
            return label;
        }

        private static void UpdateVariantsWithResults(Dictionary<GeneticTestResult, List<Person>> variants, IEnumerable<GeneticTestResult> results)
        {
            foreach (GeneticTestResult result in results.Where(r => !string.IsNullOrEmpty(r.resultSignificance) || !string.IsNullOrEmpty(r.ASOResultSignificance)))
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
    }
}



//adapted from V2 version
/*
public List<int> getSortedOrder()
{
    List<int> sortedRelativeIDs = new List<int>();

    //------------------------------------------
    //get list of all relative IDs
    //------------------------------------------
    List<int> relativeIDs = new List<int>();
    foreach (Person p in this)
    {
        relativeIDs.Add(p.relativeID);
    }

    //------------------------------------------
    //add stock relatives, aunts/uncles, grand aunts/uncles, siblings and children
    //------------------------------------------
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.FATHER));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDFATHERS_FATHER, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDFATHERS_MOTHER, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDFATHER, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.BROTHER_OF_PATERNAL_GF, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.SISTER_OF_PATERNAL_GF, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDMOTHERS_FATHER, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDMOTHERS_MOTHER, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDMOTHER, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.BROTHER_OF_PATERNAL_GM, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.SISTER_OF_PATERNAL_GM, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.UNCLE, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.AUNT, Bloodline.BloodlineEnum.Paternal));
    sortedRelativeIDs.Add(1); //SELF
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.BROTHER));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.SISTER));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.SON));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.DAUGHTER));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.MOTHER));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDFATHERS_FATHER, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDFATHERS_MOTHER, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDFATHER, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.BROTHER_OF_MATERNAL_GF, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.SISTER_OF_MATERNAL_GF, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDMOTHERS_FATHER, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDMOTHERS_MOTHER, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.Add(getRelativeIDOfStockRelative(RelationshipEnum.GRANDMOTHER, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.BROTHER_OF_MATERNAL_GM, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.SISTER_OF_MATERNAL_GM, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.UNCLE, Bloodline.BloodlineEnum.Maternal));
    sortedRelativeIDs.AddRange(getRelativeIDsOfRelatives(RelationshipEnum.AUNT, Bloodline.BloodlineEnum.Maternal));

    //remove relatives that were not found
    int j = 0;
    while (j < sortedRelativeIDs.Count)
    {
        if ((int)sortedRelativeIDs[j] == -1)
        {
            sortedRelativeIDs.RemoveAt(j);
        }
        else
        {
            j++;
        }
    }

    //place spouses
    for (int i = 0; i < sortedRelativeIDs.Count; i++)
    {
        int relativeID = (int)sortedRelativeIDs[i];
        List<int> spouses = getSpouses(relativeID);
        if (spouses.Count > 0)
        {
            for (j = 0; j < spouses.Count; j++)
            {
                if (sortedRelativeIDs.Contains((int)spouses[j]))
                {
                    continue;
                }
                //if children have been placed, we what this above the top-most child
                List<int> children = getChildren((int)spouses[j]);
                int indexOfTopmostChild = Int32.MaxValue;
                for (int k = 0; k < children.Count; k++)
                {
                    int index = sortedRelativeIDs.IndexOf(children[k]);
                    if (index >= 0 && index < indexOfTopmostChild)
                    {
                        indexOfTopmostChild = index;
                    }
                }
                if (indexOfTopmostChild >= 0 && indexOfTopmostChild < sortedRelativeIDs.Count)
                {
                    sortedRelativeIDs.Insert(indexOfTopmostChild, spouses[j]);
                }
                else
                {
                    int indexOfExistingRelative = sortedRelativeIDs.IndexOf(relativeID);

                    sortedRelativeIDs.Insert(indexOfExistingRelative + 1, spouses[j]);
                }
            }
        }
    }

    //place relatives based on mother/father IDs
    for (int i = 0; i < relativeIDs.Count; i++)
    {
        int relativeID = (int)relativeIDs[i];

        if (sortedRelativeIDs.Contains(relativeID) == false)
        {
            Person relative = getRelative(relativeID);
            int motherID = relative.motherID;
            int fatherID = relative.fatherID;
            int indexOfParent = 0;
            if (motherID > 0)
            {
                indexOfParent = sortedRelativeIDs.IndexOf(motherID);
            }
            if (fatherID > 0)
            {
                if (sortedRelativeIDs.IndexOf(fatherID) > indexOfParent)
                {
                    indexOfParent = sortedRelativeIDs.IndexOf(fatherID);
                }
            }
            if (indexOfParent > 0)
            {
                sortedRelativeIDs.Insert(indexOfParent + 1, relativeID);
            }
        }
    }

    for (int i = 0; i < relativeIDs.Count; i++)
    {
        if (sortedRelativeIDs.Contains(relativeIDs[i]) == false)
        {
            sortedRelativeIDs.Add(relativeIDs[i]);
        }
    }

    return sortedRelativeIDs;
}
        
public int getRelativeIDOfStockRelative(RelationshipEnum relationship, Bloodline.BloodlineEnum bloodline)
{
    for (int i = 0; i < this.Count; i++)
    {
        if ((Relationship.Parse(this[i].relationship) == relationship) && (Bloodline.Parse(this[i].bloodline) == bloodline))
        {
            return this[i].relativeID;
        }
    }
    return -1;
}

public int getRelativeIDOfStockRelative(RelationshipEnum relationship)
{
    for (int i = 0; i < this.Count; i++)
    {
        if (Relationship.Parse(this[i].relationship) == relationship)
        {
            return this[i].relativeID;
        }
    }
    return -1;
}

public List<int> getRelativeIDsOfRelatives(RelationshipEnum relationship)
{
    var relativeIDs = new List<int>();
    for (int i = 0; i < this.Count; i++)
    {
        if (Relationship.Parse(this[i].relationship) == relationship)
        {
            relativeIDs.Add(this[i].relativeID);
        }
    }
    return relativeIDs;
}

public List<int> getRelativeIDsOfRelatives(RelationshipEnum relationship, Bloodline.BloodlineEnum bloodline)
{
    if (bloodline == Bloodline.BloodlineEnum.Unknown || bloodline == Bloodline.BloodlineEnum.Both)
    {
        return getRelativeIDsOfRelatives(relationship);
    }
    var relativeIDs = new List<int>();
    for (int i = 0; i < this.Count; i++)
    {
        if ((Relationship.Parse(this[i].relationship) == relationship) && (Bloodline.Parse(this[i].bloodline) == bloodline))
        {
            relativeIDs.Add(this[i].relativeID);
        }
    }
    return relativeIDs;
}

public List<int> getSpouses(int relativeID)
{
    var spouses = new List<int>();

    for (int i = 0; i < this.Count; i++)
    {
        if (this[i].motherID == relativeID && this[i].fatherID > 0 &&
            spouses.Contains(this[i].fatherID) == false)
        {
            spouses.Add(this[i].fatherID);
        }
        if (this[i].fatherID == relativeID && this[i].motherID > 0 &&
            spouses.Contains(this[i].motherID) == false)
        {
            spouses.Add(this[i].motherID);
        }
    }
    return spouses;
}

public List<int> getChildren(int motherID, int fatherID)
{
    var children = new List<int>();

    for (int i = 0; i < this.Count; i++)
    {
        if (motherID > 0 && fatherID > 0)
        {
            if (this[i].motherID == motherID && this[i].fatherID == fatherID)
            {
                children.Add(this[i].relativeID);
            }
        }
        else
        {
            if (motherID > 0)
            {
                if (this[i].motherID == motherID)
                {
                    children.Add(this[i].relativeID);
                }
            }
            if (fatherID > 0)
            {
                if (this[i].fatherID == fatherID)
                {
                    children.Add(this[i].relativeID);
                }
            }
        }
    }
    return children;
}

public List<int> getChildren(int parent)
{
    var children = new List<int>();

    for (int i = 0; i < this.Count; i++)
    {
        if (this[i].motherID == parent || this[i].fatherID == parent)
        {
            children.Add(this[i].relativeID);
        }
    }
    return children;
}
*/