using System;
using System.ComponentModel;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.Model.MetaData
{
    public class MetaData
    {
        /**************************************************************************************************/
        public DiseaseList Diseases;
        public GeneticTestList GeneticTests;
        public UserList Users;
        public UserGroupList UserGroups;
        public OrderTypesList OrderTypes;
        public MutationList Mutations;
        public GUIPreference SystemWideDefaultPedigreePrefs;
        public GUIPreference CurrentUserDefaultPedigreePrefs;
        public ApptProviderList ApptProviders;
        public AllProviders AllProviders;
        public VariantsFromKB KbVariants;
        public AllBrOvCdsRecs BrOvCdsRecs;
        public GlobalSettings Globals;
        public AssessmentList Assessments;

        /**************************************************************************************************/
        public MetaData()
        {
            Diseases = new DiseaseList();
            GeneticTests = new GeneticTestList();
            Users = new UserList();
            UserGroups = new UserGroupList();
            OrderTypes = new OrderTypesList();
            Mutations = new MutationList();
            SystemWideDefaultPedigreePrefs = new GUIPreference();
            CurrentUserDefaultPedigreePrefs = new GUIPreference();
            ApptProviders = new ApptProviderList();
            AllProviders = new AllProviders();
            KbVariants = new VariantsFromKB();
            BrOvCdsRecs = new AllBrOvCdsRecs();
            Globals = new GlobalSettings();
            Assessments = new AssessmentList();
        }

        public void ReleaseListeners(object o)
        {
            this.Diseases.ReleaseListeners(o);
            this.GeneticTests.ReleaseListeners(o);
            this.Users.ReleaseListeners(o);
            this.UserGroups.ReleaseListeners(o);
            this.OrderTypes.ReleaseListeners(o);
            this.Mutations.ReleaseListeners(o);
            this.SystemWideDefaultPedigreePrefs.ReleaseListeners(o);
            this.CurrentUserDefaultPedigreePrefs.ReleaseListeners(o);
            this.ApptProviders.ReleaseListeners(o);
            this.AllProviders.ReleaseListeners(o);
            this.KbVariants.ReleaseListeners(o);
            this.BrOvCdsRecs.ReleaseListeners(o);
            this.Globals.ReleaseListeners(o);
            this.ApptProviders.ReleaseListeners(o);
            this.Assessments.ReleaseListeners(o);
        }
    }
}
