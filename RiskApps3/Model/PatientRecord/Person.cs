using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using RiskApps3.Utilities;
using System.Reflection;
using System.Runtime.Serialization;

using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.Controllers;
using RiskApps3.View;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract(IsReference=true)]
    [KnownType(typeof(Patient))]
    public class Person : HraObject
    {

        /**************************************************************************************************/
        [DataMember] public FamilyHistory owningFHx;

        /**************************************************************************************************/
        /// <summary>
        /// Demographics
        /// </summary>
        [DataMember][HraAttribute] public string lastName;
        [DataMember][HraAttribute] public string firstName ;
        [DataMember][HraAttribute] public string middleName ;
        [DataMember][HraAttribute] public string maidenName ;
        [DataMember][HraAttribute] public string name ;
        [DataMember][HraAttribute] public string address1 ;
        [DataMember][HraAttribute] public string address2 ;
        [DataMember][HraAttribute] public string city ;
        [DataMember][HraAttribute] public string state ;
        [DataMember][HraAttribute] public string zip ;
        [DataMember][HraAttribute] public string country ;
        [DataMember][HraAttribute] public string homephone ;
        [DataMember][HraAttribute] public string workphone ;
        [DataMember][HraAttribute] public string cellphone ;
        [DataMember][HraAttribute] public string dob ;
        [DataMember][HraAttribute] public string maritalstatus ;
        [DataMember][HraAttribute] public string religion ;
        [DataMember][HraAttribute] public string contactname ;
        [DataMember][HraAttribute] public string contacthomephone ;
        [DataMember][HraAttribute] public string contactworkphone ;
        [DataMember][HraAttribute] public string contactcellphone ;
        [DataMember][HraAttribute] public string occupation ;
        [DataMember][HraAttribute] public string emailAddress ;
        [DataMember][HraAttribute] public string educationLevel;

        /// <summary>
        /// relative data
        /// </summary>
        [DataMember] public int relativeID = -1;
        [DataMember][HraAttribute] public int motherID = 0;
        [DataMember][HraAttribute] public int fatherID = 0;
        [DataMember][HraAttribute] public string gender;
        [DataMember][HraAttribute] public string relationship ;
        [DataMember][HraAttribute] public string relationshipOther ;
        [DataMember][HraAttribute] public string bloodline ;
        [DataMember][HraAttribute (affectsTestingDecision=true,affectsRiskProfile=true)] public string age ;
        [DataMember][HraAttribute] public string vitalStatus ;
        [DataMember][HraAttribute] public string comment ;

        [DataMember][HraAttribute] public string title ;
        [DataMember][HraAttribute] public string suffix ;
        [DataMember][HraAttribute] public int twinID = 0;
        [DataMember][HraAttribute] public string twinType ;
        [DataMember][HraAttribute] public string adopted;
        [DataMember][HraAttribute] public string adoptedFhxKnown;
        [DataMember][HraAttribute] public string dobConfidence ;
        [DataMember][HraAttribute] public string dateOfDeath ;
        [DataMember][HraAttribute] public string causeOfDeath ;
        [DataMember][HraAttribute] public string dateOfDeathConfidence ;

        [DataMember][HraAttribute] public string isAshkenazi ;
        [DataMember][HraAttribute] public string isHispanic ;

        /// <summary>
        /// Pedigree stuff
        /// </summary>
        [DataMember][HraAttribute (auditable=false)] public double x_position = 0;
        [DataMember][HraAttribute (auditable=false)] public double y_position = 0;
        [DataMember][HraAttribute (auditable=false)] public int x_norm = int.MinValue;
        [DataMember][HraAttribute (auditable=false)] public int y_norm = int.MinValue;
        [DataMember][HraAttribute (auditable = false)] public int pedigreeGroup = -1;
        [DataMember][HraAttribute(auditable = false)] public int consanguineousSpouseID = 0;  // ?? audit ??

        /// <summary>
        /// Past Medical History
        /// </summary>
        [DataMember][HraAttribute (persistable=false, affectsTestingDecision=true, auditable = false)] public PastMedicalHistory PMH;

        //Risk scores for this person
        [DataMember] [HraAttribute (persistable=false, affectsTestingDecision=true,auditable = false)] public RiskProfile RP;

        //race
        [DataMember][HraAttribute(auditable = false, persistable=false)] public EthnicBackground Ethnicity;
        [DataMember][HraAttribute(auditable = false, persistable=false)] public NationalityList Nationality;



        #region custom_get_set

        public int Person_consanguineousSpouseID
        {
            get
            {
                return consanguineousSpouseID;
            }
            set
            {
                if (value != consanguineousSpouseID)
                {
                    consanguineousSpouseID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("consanguineousSpouseID"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        public string Person_lastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("lastName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_emailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                if (value != emailAddress)
                {
                    emailAddress = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("emailAddress"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_firstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("firstName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_middleName
        {
            get
            {
                return middleName;
            }
            set
            {
                if (value != middleName)
                {
                    middleName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("middleName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_maidenName
        {
            get
            {
                return maidenName;
            }
            set
            {
                if (value != maidenName)
                {
                    maidenName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("maidenName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("name"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_address1
        {
            get
            {
                return address1;
            }
            set
            {
                if (value != address1)
                {
                    address1 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("address1"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_address2
        {
            get
            {
                return address2;
            }
            set
            {
                if (value != address2)
                {
                    address2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("address2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_city
        {
            get
            {
                return city;
            }
            set
            {
                if (value != city)
                {
                    city = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("city"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_state
        {
            get
            {
                return state;
            }
            set
            {
                if (value != state)
                {
                    state = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("state"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_zip
        {
            get
            {
                return zip;
            }
            set
            {
                if (value != zip)
                {
                    zip = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("zip"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_country
        {
            get
            {
                return country;
            }
            set
            {
                if (value != country)
                {
                    country = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("country"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_homephone
        {
            get
            {
                return homephone;
            }
            set
            {
                if (value != homephone)
                {
                    homephone = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("homephone"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_workphone
        {
            get
            {
                return workphone;
            }
            set
            {
                if (value != workphone)
                {
                    workphone = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("workphone"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_cellphone
        {
            get
            {
                return cellphone;
            }
            set
            {
                if (value != cellphone)
                {
                    cellphone = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("cellphone"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_dob
        {
            get
            {
                return dob;
            }
            set
            {
                if (value != dob)
                {
                    dob = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("dob"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_maritalstatus
        {
            get
            {
                return maritalstatus;
            }
            set
            {
                if (value != maritalstatus)
                {
                    maritalstatus = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("maritalstatus"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_religion
        {
            get
            {
                return religion;
            }
            set
            {
                if (value != religion)
                {
                    religion = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("religion"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_contactname
        {
            get
            {
                return contactname;
            }
            set
            {
                if (value != contactname)
                {
                    contactname = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("contactname"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_contacthomephone
        {
            get
            {
                return contacthomephone;
            }
            set
            {
                if (value != contacthomephone)
                {
                    contacthomephone = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("contacthomephone"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_contactworkphone
        {
            get
            {
                return contactworkphone;
            }
            set
            {
                if (value != contactworkphone)
                {
                    contactworkphone = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("contactworkphone"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_contactcellphone
        {
            get
            {
                return contactcellphone;
            }
            set
            {
                if (value != contactcellphone)
                {
                    contactcellphone = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("contactcellphone"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_occupation
        {
            get
            {
                return occupation;
            }
            set
            {
                if (value != occupation)
                {
                    occupation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("occupation"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int Person_motherID
        {
            get
            {
                return motherID;
            }
            set
            {
                if (value != motherID)
                {
                    motherID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("motherID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int Person_fatherID
        {
            get
            {
                return fatherID;
            }
            set
            {
                if (value != fatherID)
                {
                    fatherID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("fatherID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_gender
        {
            get
            {
                return gender;
            }
            set
            {
                if (value != gender)
                {
                    gender = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("gender"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_relationship
        {
            get
            {
                return relationship;
            }
            set
            {
                if (value != relationship)
                {
                    relationship = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("relationship"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_relationshipOther
        {
            get
            {
                return relationshipOther;
            }
            set
            {
                if (value != relationshipOther)
                {
                    relationshipOther = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("relationshipOther"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_bloodline
        {
            get
            {
                return bloodline;
            }
            set
            {
                if (value != bloodline)
                {
                    bloodline = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("bloodline"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_age
        {
            get
            {
                return age;
            }
            set
            {
                if (value != age)
                {
                    age = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("age"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_vitalStatus
        {
            get
            {
                return vitalStatus;
            }
            set
            {
                if (value != vitalStatus)
                {
                    vitalStatus = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("vitalStatus"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_comment
        {
            get
            {
                return comment;
            }
            set
            {
                if (value != comment)
                {
                    comment = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("comment"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("title"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_suffix
        {
            get
            {
                return suffix;
            }
            set
            {
                if (value != suffix)
                {
                    suffix = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("suffix"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int Person_twinID
        {
            get
            {
                return twinID;
            }
            set
            {
                if (value != twinID)
                {
                    twinID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("twinID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_twinType
        {
            get
            {
                return twinType;
            }
            set
            {
                if (value != twinType)
                {
                    twinType = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("twinType"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_adopted
        {
            get
            {
                return adopted;
            }
            set
            {
                if (value != adopted)
                {
                    adopted = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("adopted"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_dobConfidence
        {
            get
            {
                return dobConfidence;
            }
            set
            {
                if (value != dobConfidence)
                {
                    dobConfidence = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("dobConfidence"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_dateOfDeath
        {
            get
            {
                return dateOfDeath;
            }
            set
            {
                if (value != dateOfDeath)
                {
                    dateOfDeath = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("dateOfDeath"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_causeOfDeath
        {
            get
            {
                return causeOfDeath;
            }
            set
            {
                if (value != causeOfDeath)
                {
                    causeOfDeath = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("causeOfDeath"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_dateOfDeathConfidence
        {
            get
            {
                return dateOfDeathConfidence;
            }
            set
            {
                if (value != dateOfDeathConfidence)
                {
                    dateOfDeathConfidence = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("dateOfDeathConfidence"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double Person_x_position
        {
            get
            {
                return x_position;
            }
            set
            {
                if (value != x_position)
                {
                    x_position = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("x_position"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double Person_y_position
        {
            get
            {
                return y_position;
            }
            set
            {
                if (value != y_position)
                {
                    y_position = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("y_position"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int Person_x_norm
        {
            get
            {
                return x_norm;
            }
            set
            {
                if (value != x_norm)
                {
                    x_norm = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("x_norm"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int Person_y_norm
        {
            get
            {
                return y_norm;
            }
            set
            {
                if (value != y_norm)
                {
                    y_norm = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("y_norm"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int Person_pedigreeGroup
        {
            get
            {
                return pedigreeGroup;
            }
            set
            {
                if (value != pedigreeGroup)
                {
                    pedigreeGroup = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("pedigreeGroup"));
                    SignalModelChanged(args);
                }
            }
        }        
        /*****************************************************/
        public string Person_isAshkenazi
        {
            get
            {
                return isAshkenazi;
            }
            set
            {
                if (value != isAshkenazi)
                {
                    isAshkenazi = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("isAshkenazi"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_isHispanic
        {
            get
            {
                return isHispanic;
            }
            set
            {
                if (value != isHispanic)
                {
                    isHispanic = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("isHispanic"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Person_adoptedFhxKnown
        {
            get
            {
                return adoptedFhxKnown;
            }
            set
            {
                if (value != adoptedFhxKnown)
                {
                    adoptedFhxKnown = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("adoptedFhxKnown"));
                    SignalModelChanged(args);
                }
            }
        } 







        /*****************************************************/ 
        public void Set_lastName(string value, HraView sendingView)
        { 
           if (lastName != value) 
           { 
               lastName = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("lastName")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_firstName(string value, HraView sendingView)
        { 
           if (firstName != value) 
           { 
               firstName = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("firstName")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_middleName(string value, HraView sendingView)
        { 
           if (middleName != value) 
           { 
               middleName = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("middleName")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_maidenName(string value, HraView sendingView)
        { 
           if (maidenName != value) 
           { 
               maidenName = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("maidenName")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_name(string value, HraView sendingView)
        { 
           if (name != value) 
           { 
               name = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("name")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_address1(string value, HraView sendingView)
        { 
           if (address1 != value) 
           { 
               address1 = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("address1")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_address2(string value, HraView sendingView)
        { 
           if (address2 != value) 
           { 
               address2 = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("address2")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_city(string value, HraView sendingView)
        { 
           if (city != value) 
           { 
               city = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("city")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_state(string value, HraView sendingView)
        { 
           if (state != value) 
           { 
               state = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("state")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_zip(string value, HraView sendingView)
        { 
           if (zip != value) 
           { 
               zip = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("zip")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_country(string value, HraView sendingView)
        { 
           if (country != value) 
           { 
               country = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("country")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_homephone(string value, HraView sendingView)
        { 
           if (homephone != value) 
           { 
               homephone = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("homephone")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_workphone(string value, HraView sendingView)
        { 
           if (workphone != value) 
           { 
               workphone = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("workphone")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_cellphone(string value, HraView sendingView)
        { 
           if (cellphone != value) 
           { 
               cellphone = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("cellphone")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_dob(string value, HraView sendingView)
        { 
           if (dob != value) 
           { 
               dob = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("dob")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_maritalstatus(string value, HraView sendingView)
        { 
           if (maritalstatus != value) 
           { 
               maritalstatus = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("maritalstatus")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_religion(string value, HraView sendingView)
        { 
           if (religion != value) 
           { 
               religion = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("religion")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_contactname(string value, HraView sendingView)
        { 
           if (contactname != value) 
           { 
               contactname = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("contactname")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_contacthomephone(string value, HraView sendingView)
        { 
           if (contacthomephone != value) 
           { 
               contacthomephone = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("contacthomephone")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_contactworkphone(string value, HraView sendingView)
        { 
           if (contactworkphone != value) 
           { 
               contactworkphone = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("contactworkphone")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_contactcellphone(string value, HraView sendingView)
        { 
           if (contactcellphone != value) 
           { 
               contactcellphone = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("contactcellphone")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_occupation(string value, HraView sendingView)
        { 
           if (occupation != value) 
           { 
               occupation = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("occupation")); 
               SignalModelChanged(args); 
           } 
        } 

        /*****************************************************/ 
        public void Set_relativeID(int value, HraView sendingView)
        { 
           if (relativeID != value) 
           { 
               relativeID = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("relativeID")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_motherID(int value, HraView sendingView)
        { 
           if (motherID != value) 
           { 
               motherID = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("motherID")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_fatherID(int value, HraView sendingView)
        { 
           if (fatherID != value) 
           { 
               fatherID = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("fatherID")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_gender(string value, HraView sendingView)
        { 
           if (gender != value) 
           { 
               gender = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("gender")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_relationship(string value, HraView sendingView)
        { 
           if (relationship != value) 
           { 
               relationship = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("relationship")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_relationshipOther(string value, HraView sendingView)
        { 
           if (relationshipOther != value) 
           { 
               relationshipOther = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("relationshipOther")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_bloodline(string value, HraView sendingView)
        { 
           if (bloodline != value) 
           { 
               bloodline = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("bloodline")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_age(string value, HraView sendingView)
        { 
           if (age != value) 
           { 
               age = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("age")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_vitalStatus(string value, HraView sendingView)
        { 
           if (vitalStatus != value) 
           { 
               vitalStatus = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("vitalStatus")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_comment(string value, HraView sendingView)
        { 
           if (comment != value) 
           { 
               comment = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("comment")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_title(string value, HraView sendingView)
        { 
           if (title != value) 
           { 
               title = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("title")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_suffix(string value, HraView sendingView)
        { 
           if (suffix != value) 
           { 
               suffix = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("suffix")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_twinID(int value, HraView sendingView)
        { 
           if (twinID != value) 
           { 
               twinID = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("twinID")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_twinType(string value, HraView sendingView)
        { 
           if (twinType != value) 
           { 
               twinType = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("twinType")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_adopted(string value, HraView sendingView)
        { 
           if (adopted != value) 
           { 
               adopted = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("adopted")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_dobConfidence(string value, HraView sendingView)
        { 
           if (dobConfidence != value) 
           { 
               dobConfidence = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("dobConfidence")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_dateOfDeath(string value, HraView sendingView)
        { 
           if (dateOfDeath != value) 
           { 
               dateOfDeath = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("dateOfDeath")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_causeOfDeath(string value, HraView sendingView)
        { 
           if (causeOfDeath != value) 
           { 
               causeOfDeath = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("causeOfDeath")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_dateOfDeathConfidence(string value, HraView sendingView)
        { 
           if (dateOfDeathConfidence != value) 
           { 
               dateOfDeathConfidence = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("dateOfDeathConfidence")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_isAshkenazi(string value, HraView sendingView)
        { 
           if (isAshkenazi != value) 
           { 
               isAshkenazi = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("isAshkenazi")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_isHispanic(string value, HraView sendingView)
        { 
           if (isHispanic != value) 
           { 
               isHispanic = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("isHispanic")); 
               SignalModelChanged(args); 
           } 
        } 

        /*****************************************************/ 
        public void Set_x_position(double value, HraView sendingView)
        { 
           if (x_position != value) 
           { 
               x_position = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("x_position")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_y_position(double value, HraView sendingView)
        { 
           if (y_position != value) 
           { 
               y_position = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("y_position")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_x_norm(int value, HraView sendingView)
        { 
           if (x_norm != value) 
           { 
               x_norm = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("x_norm")); 
               SignalModelChanged(args); 
           } 
        } 
        /*****************************************************/ 
        public void Set_y_norm(int value, HraView sendingView)
        { 
           if (y_norm != value) 
           { 
               y_norm = value; 
               HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); 
               args.sendingView = sendingView; 
               args.updatedMembers.Add(GetMemberByName("y_norm")); 
               SignalModelChanged(args); 
           }
        }

        #endregion

        /**************************************************************************************************/
        public override void LoadFullObject()
        {
            base.LoadFullObject();
            PMH.LoadFullObject();
            RP.LoadFullObject();
            Ethnicity.LoadFullList();
            Nationality.LoadFullList();
        }

        /**************************************************************************************************/
        //  This way of creating the person is used to create the patient
        public Person()
        {
            Ethnicity = new EthnicBackground(this);
            Nationality = new NationalityList(this);
            PMH = new PastMedicalHistory(this);
            RP = new RiskProfile(this);
        }

        /**************************************************************************************************/
        // This way of creating a person is used to create relatives in the family history
        public Person(FamilyHistory fhx)
        {
            owningFHx = fhx;
            Nationality = new NationalityList(this);
            Ethnicity = new EthnicBackground(this);
            PMH = new PastMedicalHistory(this);
            RP = new RiskProfile(this);
        }

        /**************************************************************************************************/
        public override void PersistFullObject(HraModelChangedEventArgs e)
        {
            base.PersistFullObject(e);
            PMH.RelativeOwningPMH = this;
            PMH.PersistFullObject(e);
            RP.OwningPerson = this;
            RP.PersistFullObject(e);
            Ethnicity.person = this;
            Ethnicity.PersistFullList(e);
            if (Nationality != null)
            {
                Nationality.person = this;
                Nationality.PersistFullList(e);
            }
        }

        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            PMH.ReleaseListeners(view);
            Nationality.ReleaseListeners(view);
            RP.ReleaseListeners(view);
            Ethnicity.ReleaseListeners(view);
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        protected override void LoadCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //as soon as we know enough about the perosn, load their past medical history
            if (PMH.HraState == States.Null)
                PMH.LoadObject();

            //and load his/her Risk Profile
            if (RP.HraState == States.Null)
                RP.LoadObject();


            base.LoadCompleted(sender, e);
        }

        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            base.BackgroundLoadWork();
        }


        public List<GeneticTestResult> GetNonNegativeGeneticTestResults()
        {
            List<GeneticTestResult> results = new List<GeneticTestResult>();

            if(PMH!=null)
            {
                foreach (GeneticTest geneticTest in PMH.GeneticTests)
                {
                    results.AddRange(geneticTest.GetNonNegativeResults());
                } 
            }
          
            return results;
        }

        public List<GeneticTestResult> GetExistingFKMTests()
        {
            List<GeneticTestResult> results = new List<GeneticTestResult>();
            if (PMH != null)
            {
                foreach (GeneticTest test in PMH.GeneticTests)
                {
                    if (test.IsASOTest)
                    {
                        results.AddRange(test.GeneticTestResults);
                    }
                }
            }
            return results;
        }

        public bool HasNonNegativeGeneticTestResults(List<GeneticTestResult> toExclude)
        {
            if (PMH != null)
            {
                foreach (GeneticTest t in PMH.GeneticTests)
                {
                    if (t.HasNonNegativeResults(toExclude)) { return true; }
                }
            }
            return false;
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("relativeID", relativeID);

            if (this is Patient)
            {
                Patient patient = (Patient)this;
                pc.Add("patientUnitnum", patient.unitnum);
                pc.Add("apptid", patient.apptid);
            }
            else
            {
                pc.Add("patientUnitnum", this.owningFHx.proband.unitnum);
                pc.Add("apptid", this.owningFHx.proband.apptid);
            }

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_Person",
                                      ref pc);

        }
        public bool HasOvarianCancer()
        {
            bool retval = false;
            foreach (ClincalObservation co in PMH.Observations)
            {
                if (co.disease.ToLower().Contains("ovarian cancer"))
                    retval = true;
            }

            return retval;
        }

        public bool HasUterineCancer()
        {
            bool retval = false;
            foreach (ClincalObservation co in PMH.Observations)
            {
                if (co.disease.ToLower().Contains("uterine cancer"))
                    retval = true;
            }

            return retval;
        }

        public bool HasColonCancer()
        {
            bool retval = false;
            foreach (ClincalObservation co in PMH.Observations)
            {
                if ((co.disease.ToLower().Contains("rectal") || co.disease.ToLower().Contains("colon"))&& co.disease.ToLower().Contains("cancer"))
                    retval = true;
            }

            return retval;
        }

        public bool HasBreastCancer()
        {
            bool retval = false;
            foreach (ClincalObservation co in PMH.Observations)
            {
                if (co.disease.ToLower().Contains("breast cancer"))
                    retval = true;
            }

            return retval;
        }

        public bool HasBilateralBreastCancer()
        {
    
            int count = 0;
            foreach (ClincalObservation co in PMH.Observations)
            {
                if (co.disease.ToLower().Contains("breast cancer"))
                {
                    count++;
                }
                    
            }
            if (count >= 2)
            {
                return true;
            }
            return false;
        }

        public string GetShortRelationship()
        {
            if (relationship == null || bloodline == null)
            {
                return "";
            }
            string retval = relationship;
            if (bloodline.StartsWith("Mat"))
            {
                retval = "Mat " + relationship;
            }
            if (bloodline.StartsWith("Pat"))
            {
                retval = "Pat " + relationship;
            }
            return retval;
        }
        internal bool IsStockRelative()
        {
            bool retval = false;

            switch (Relationship.Parse(relationship))
            {
                case RelationshipEnum.SELF:
                    retval = true;
                    break;
                case RelationshipEnum.MOTHER:
                    retval = true;
                    break;
                case RelationshipEnum.FATHER:
                    retval = true;
                    break;
                case RelationshipEnum.GRANDMOTHER:
                    retval = true;
                    break;
                case RelationshipEnum.GRANDFATHER:
                    retval = true;
                    break;
            }

            return retval;
        }
    }
}
