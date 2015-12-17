using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Gedcom;
//using GedcomParser;
using System.IO;
using System.Text.RegularExpressions;
using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using System.Diagnostics;
using System.Threading;

namespace RiskApps3.Utilities
{
    public partial class GedcomParser : Form
    {
        FamilyHistory fhx;
        //GedcomRecordReader reader;
        //GedcomIndividualRecord proband;
        Dictionary<string, int> idMapping;
        Dictionary<string, int> gedcomPeople;
        Dictionary<string, int> familyMapping;

        public GedcomParser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.ged)|*.ged|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                loadingCircle1.Enabled = true;
                loadingCircle1.Visible = true;

                backgroundWorker1.RunWorkerAsync(textBox1.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //loadingCircle1.Enabled = true;
            //loadingCircle1.Visible = true;

            //backgroundWorker1.RunWorkerAsync(textBox1.Text);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*
            try
            {
                string filename = (string)e.Argument;
                
                reader = new GedcomRecordReader();
                if (reader.ReadGedcom(filename))
                {
                    foreach (GedcomIndividualRecord individual in reader.Database.Individuals)
                    {
                        GedcomName gname = individual.GetName();
                        string id = individual.XRefID != null ? individual.XRefID : "";
                        string name = gname.Name.Replace("/", "");

                        DateTime myMinDate = Convert.ToDateTime("01/01/0100");
                        string birthdate = individual.Birth != null && individual.Birth.Date != null ? individual.Birth.Date.Date1.ToString() : "";
                        string birthday = "";
                        DateTime dob = DateTime.MinValue;
                        if (individual.Birth != null && individual.Birth.Date != null && individual.Birth.Date.DateTime1 != null && individual.Birth.Date.DateTime1 > myMinDate && individual.Birth.Date.DateTime1 <= DateTime.Now)
                        {
                            dob = (DateTime)individual.Birth.Date.DateTime1;
                            birthday = dob.ToShortDateString();
                        }
                        string deathDate = individual.Death != null && individual.Death.Date != null ? individual.Death.Date.Date1.ToString() : "";
                        DateTime dod = DateTime.MinValue;
                        string deathDay = "";
                        if (individual.Death != null && individual.Death.Date != null && individual.Death.Date.DateTime1 != null && individual.Death.Date.DateTime1 > myMinDate && individual.Death.Date.DateTime1 <= DateTime.Now)
                        {
                            dod = (DateTime)individual.Death.Date.DateTime1;
                            deathDay = dod.ToShortDateString();
                        }
                        string vitalStatus = individual.Dead == false && string.IsNullOrEmpty(deathDay) == true ? "Alive" : "Dead";
                        string age = "";
                        if (individual.Dead == false)
                        {
                            if (birthday != "")
                            {
                                age = ((DateTime.MinValue + (DateTime.Now - dob)).Year - 1).ToString();
                            }
                        }
                        else if (deathDay != "")
                        {
                            age = ((DateTime.MinValue + (dod - dob)).Year - 1).ToString();
                        }

                        string gender = GenderEnum.Unknown.ToString();
                        if (String.IsNullOrEmpty(individual.SexChar) == false)
                        {
                            if (individual.SexChar == "M")
                                gender = GenderEnum.Male.ToString();
                            else if (individual.SexChar == "F")
                                gender = GenderEnum.Female.ToString();
                        }

                        //string birthdate = individual.Birth != null && individual.Birth.Date != null ? individual.Birth.Date.Date1.ToString() : "";
                        //string age = individual.Birth != null && individual.Birth.Age != null ? individual.Birth.Age.ToString() : "";
                        //string dead = individual.Dead.ToString();
                        //string sex = individual.Sex.ToString();

                        ListViewItem lvi = new ListViewItem();
                        lvi.Tag = individual;
                        lvi.Text = name;
                        lvi.SubItems.Add(gender);
                        lvi.SubItems.Add(vitalStatus);
                        lvi.SubItems.Add(birthday);
                        lvi.SubItems.Add(age);

                        backgroundWorker1.ReportProgress(0, lvi);

                    }
                }
            }
            catch (Exception eee)
            {
                string stack = getShortStack();
                Logger.Instance.WriteToLog("RiskApps3.Utilities.GedcomParser backgroundWorker2_DoWork " + eee + " " + stack);
            }
             */
        }
    
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ListViewItem lvi = (ListViewItem)e.UserState;
            listView1.Items.Add(lvi);

            groupBox2.Text = "Select Proband from " + listView1.Items.Count.ToString() + " people.";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingCircle1.Enabled = false;
            loadingCircle1.Visible = false;
        }

        private void GedcomParser_Load(object sender, EventArgs e)
        {
            loadingCircle1.Enabled = false;
            loadingCircle1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                loadingCircle1.Enabled = true;
                loadingCircle1.Visible = true;

                backgroundWorker2.RunWorkerAsync(listView1.SelectedItems[0].Tag);
            }
            

        }


        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            /*

            proband = (GedcomIndividualRecord)e.Argument;
            idMapping = new Dictionary<string, int>();
            Dictionary<int, string> xrefMappring = new Dictionary<int, string>();

            foreach (GedcomIndividualRecord individual in reader.Database.Individuals)
            {
                idMapping.Add(individual.XRefID, -1);
            }

            fhx = SessionManager.Instance.GetActivePatient().FHx;
            lock (fhx.Relatives)
            {
                DeleteNonNuclearFamily();
                MapNuclearIDs();
                foreach (Person p in fhx)
                {
                    if (p.relativeID < 8)
                    {
                        foreach (string xref in idMapping.Keys)
                        {
                            if (idMapping[xref] == p.relativeID)
                            {
                                GedcomIndividualRecord indi = GetGedcomIndividualRecord(xref);
                                SetPersonFromIndividual(indi, p);
                                break;
                            }
                        }
                    }
                }

                foreach (GedcomIndividualRecord individual in reader.Database.Individuals)
                {
                    if (idMapping[individual.XRefID] == -1)
                    {
                        List<Person> newbie = fhx.AddRelativeByType("Other", "", 1);
                        if (newbie.Count > 0)
                        {
                            idMapping[individual.XRefID] = newbie[0].relativeID;
                            SetPersonFromIndividual(individual,newbie[0]);
                        }
                    }
                }
                foreach (string k in idMapping.Keys)
                {
                    xrefMappring.Add(idMapping[k], k);
                }
                bool placed = true;
                while (placed)
                {
                    placed = false;
                    foreach (Person p in fhx)
                    {
                        if (p.motherID == 0 && p.fatherID == 0)
                        {
                            string momXref = "";
                            string dadXref = "";
                            string pXref = "";
                            if (xrefMappring.Keys.Contains(p.relativeID))
                            {
                                pXref = xrefMappring[p.relativeID];
                                GedcomFamilyRecord family = GetParentsFromChild(pXref, out momXref, out dadXref);
                                if (family != null)
                                {
                                    int mId = 0;
                                    if (string.IsNullOrEmpty(family.Wife) == false && idMapping.Keys.Contains(family.Wife))
                                    {
                                        mId = idMapping[family.Wife];
                                    }
                                    int dId = 0;
                                    if (string.IsNullOrEmpty(family.Husband) == false && idMapping.Keys.Contains(family.Husband))
                                    {
                                        dId = idMapping[family.Husband];
                                    }
                                    if (mId > 0 && dId > 0)
                                    {
                                        p.Person_motherID = mId;
                                        p.Person_fatherID = dId;
                                        placed = true;
                                    }
                                }
                            }
                        }
                    }
                }
                bool related = true;
                while (related)
                {
                    related = false;

                    //look for relatives needing to have there relationship set
                    foreach (Person p in fhx)
                    {
                        if (p.relationship=="Other")
                        {
                            string newRelationship = "";
                            string relationshipOther = "";
                            string bloodline = "";
                            
                            //look for someone in their family
                            foreach (Person rel in fhx)
                            {
                                //if we have found a daughter
                                if (rel.motherID == p.relativeID)
                                {
                                    if (rel.relationship != "Other")
                                    {
                                        Relationship.SetRelationshipByParentType(Gender.Parse(p.gender),
                                                                                Relationship.Parse(rel.relationship), Bloodline.Parse(rel.bloodline),
                                                                                out newRelationship, out relationshipOther,
                                                                                out bloodline);
                                        if (newRelationship != "Other")
                                        {
                                            p.relationship = newRelationship;
                                            p.relationshipOther = relationshipOther;
                                            p.bloodline = bloodline;
                                            related = true;
                                        }
                                        
                                    }
                                    break;
                                }
                                else if (rel.fatherID == p.relativeID)
                                {
                                    if (rel.relationship != "Other")
                                    {
                                        Relationship.SetRelationshipByParentType(Gender.Parse(p.gender),
                                                                                Relationship.Parse(rel.relationship), Bloodline.Parse(rel.bloodline),
                                                                                out newRelationship, out relationshipOther,
                                                                                out bloodline);
                                        if (newRelationship != "Other")
                                        {
                                            p.relationship = newRelationship;
                                            p.relationshipOther = relationshipOther;
                                            p.bloodline = bloodline;
                                            related = true;
                                        }
                                    }
                                    break;
                                }
                                else if (rel.relativeID == p.motherID)
                                {
                                    if (rel.relationship != "Other")
                                    {
                                        Relationship.SetRelationshipByChildType(Gender.Parse(p.gender),
                                                                                Relationship.Parse(rel.relationship), Bloodline.Parse(rel.bloodline),
                                                                                out newRelationship, out relationshipOther,
                                                                                out bloodline);
                                        if (newRelationship != "Other")
                                        {
                                            p.relationship = newRelationship;
                                            p.relationshipOther = relationshipOther;
                                            p.bloodline = bloodline;
                                            related = true;
                                        }
                                    }
                                    break;
                                }
                                else if (rel.relativeID == p.fatherID)
                                {
                                    if (rel.relationship != "Other")
                                    {
                                        Relationship.SetRelationshipByChildType(Gender.Parse(p.gender),
                                                                                Relationship.Parse(rel.relationship), Bloodline.Parse(rel.bloodline),
                                                                                out newRelationship, out relationshipOther,
                                                                                out bloodline);
                                        if (newRelationship != "Other")
                                        {
                                            p.relationship = newRelationship;
                                            p.relationshipOther = relationshipOther;
                                            p.bloodline = bloodline;
                                            related = true;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            */


            //try
            //{
            //    proband = (GedcomIndividualRecord)e.Argument;
            //    idMapping = new Dictionary<string, int>();
            //    familyMapping = new Dictionary<string, int>();
            //    gedcomPeople = new Dictionary<string, int>();
            //    fhx = SessionManager.Instance.GetActivePatient().FHx;

            //    ////////////////////////////////////////////////
            //    // Clear the pedigree

            //    //lock (fhx)
            //    {
            //        List<Person> doomed = new List<Person>();
            //        foreach (Person p in fhx)
            //        {
            //            if (p.relativeID > 7)
            //                doomed.Add(p);
            //        }
            //        foreach (Person p in doomed)
            //        {
            //            fhx.RemoveFromList(p, SessionManager.Instance.securityContext);
            //        }
            //    }

            //    ////////////////////////////////////////////////////////////////////////////////
            //    // Map realtive ID's, family IDs and gedcom-xref-to-pedigree-relativeIDs

            //    foreach (GedcomIndividualRecord individual in reader.Database.Individuals)
            //    {
            //        idMapping.Add(individual.XRefID, -1);
            //    }
            //    foreach (GedcomFamilyRecord gfamily in reader.Database.Families)
            //    {
            //        familyMapping.Add(gfamily.XRefID, -1);
            //    }
            //    foreach (GedcomIndividualRecord individual in reader.Database.Individuals)
            //    {
            //        gedcomPeople.Add(individual.XRefID, -1);
            //    }
            //    try
            //    {
            //        //////////////////////////////////////////////////////////
            //        // Map and set proband and proband's nuclear family

            //        GedcomFamilyRecord probandFamilyC = setProbandFamily();
                    
            //        familyMapping[probandFamilyC.XRefID] = 1;


            //        ///////////////////////////////////////////////////////////
            //        // Finish first pass: iterate through the rest of the gedcom families .
            //        foreach (GedcomFamilyRecord gedcomfamily in reader.Database.Families)
            //        {
            //            if (gedcomfamily != probandFamilyC)
            //            {
            //                {
            //                    AddParentsToPedigree(gedcomfamily, fhx);
            //                    setChildrenFromGedcomFamily(gedcomfamily); // gedcomfamily.Wife, gedcomfamily.Husband);
            //                    familyMapping[gedcomfamily.XRefID] = 1;
            //                }
            //            }
            //        }
            //        ///////////////////////
            //        // Second pass - Process the ancestor families of proband and children (recursively upward).
            //        processFamilyParents(probandFamilyC);

            //        ///////////////////////
            //        // Third pass - Process Spouse-in families of proband and children (recursively downward).
            //        processFamilyChildren(probandFamilyC);

            //        //////////////////////
            //        // Done??
            //    }
            //    catch (Exception ee)
            //    {
            //        string stack = getShortStack();
            //        Logger.Instance.WriteToLog("RiskApps3.Utilities.GedcomParser backgroundWorker2_DoWork " + ee + " " + stack);
            //    }
            //    backgroundWorker2.ReportProgress(100);

            //}
            //catch (Exception eee)
            //{
            //    Logger.Instance.WriteToLog("RiskApps3.Utilities.GedcomParser backgroundWorker2_DoWork " + eee + " " + getShortStack());
            //}
        }

        private void DeleteNonNuclearFamily()
        {
            lock (fhx)
            {
                List<Person> doomed = new List<Person>();
                foreach (Person p in fhx)
                {
                    if (p.relativeID > 7)
                        doomed.Add(p);
                }
                foreach (Person p in doomed)
                {
                    fhx.RemoveFromList(p, SessionManager.Instance.securityContext);
                }
            }
        }

        private void MapNuclearIDs()
        {
            ////now assign the proband
            //idMapping[proband.XRefID] = 1;

            ////now find the mother and father;
            //string motherXref = "";
            //string fatherXref = "";

            //GedcomFamilyRecord family = GetParentsFromChild(proband.XRefID, out motherXref, out fatherXref);

            //if (string.IsNullOrEmpty(motherXref) == false)
            //{
            //    idMapping[motherXref] = 2;

            //    //now find the maternal Grandmother and Grandfather
            //    string matGrandmotherXref = "";
            //    string matGrandfatherXref = "";

            //    GetParentsFromChild(motherXref, out matGrandmotherXref, out matGrandfatherXref);

            //    if (string.IsNullOrEmpty(matGrandmotherXref) == false)
            //        idMapping[matGrandmotherXref] = 4;

            //    if (string.IsNullOrEmpty(matGrandfatherXref) == false)
            //        idMapping[matGrandfatherXref] = 5;

            //}
            //if (string.IsNullOrEmpty(fatherXref) == false)
            //{
            //    idMapping[fatherXref] = 3;
            //    //now find the maternal Grandmother and Grandfather
            //    string patGrandmotherXref = "";
            //    string patGrandfatherXref = "";

            //    GetParentsFromChild(fatherXref, out patGrandmotherXref, out patGrandfatherXref);

            //    if (string.IsNullOrEmpty(patGrandmotherXref) == false)
            //        idMapping[patGrandmotherXref] = 6;

            //    if (string.IsNullOrEmpty(patGrandfatherXref) == false)
            //        idMapping[patGrandfatherXref] = 7;
            //}
        }



        private bool allPeopleProcessed()
        {
            foreach (string xref in idMapping.Keys)
            {
                if (idMapping[xref] < 0)
                    return false;

                if (gedcomPeople.Keys.Contains(xref) == false || gedcomPeople[xref] < 0)
                    return false;
            }
            return true;
        }
        
        //private void processFamilyParents(GedcomFamilyRecord family)
        //{
        //    GedcomIndividualRecord father = null;
        //    GedcomFamilyRecord fatherFamily = null;
        //    if (string.IsNullOrEmpty(family.Husband) == false)
        //    {
        //        father = GetGedcomIndividualRecord(family.Husband);

        //        foreach (GedcomFamilyLink flink in father.ChildIn)
        //        {
        //            if (string.IsNullOrEmpty(flink.Family) == false)
        //            {
        //                if (father.ChildInFamily(flink.Family) == true)
        //                {
        //                    foreach (GedcomFamilyRecord f in reader.Database.Families)
        //                    {
        //                        if (f.XRefID == flink.Family)
        //                        {
        //                            fatherFamily = f;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    } 
        //    GedcomIndividualRecord mother = null;
        //    GedcomFamilyRecord motherFamily = null;
        //    if (string.IsNullOrEmpty(family.Wife) == false)
        //    {
        //        mother = GetGedcomIndividualRecord(family.Wife);

        //        foreach (GedcomFamilyLink flink in mother.ChildIn)
        //        {
        //            if (string.IsNullOrEmpty(flink.Family) == false)
        //            {
        //                if (mother.ChildInFamily(flink.Family) == true)
        //                {
        //                    foreach (GedcomFamilyRecord f in reader.Database.Families)
        //                    {
        //                        if (f.XRefID == flink.Family)
        //                        {
        //                            motherFamily = f;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (motherFamily != null)
        //    {
        //        AddParentsToPedigree(motherFamily, fhx);
        //        processFamilyParents(motherFamily);
        //        processFamilyChildren(motherFamily);
        //    }
        //    if (fatherFamily != null)
        //    {
        //        AddParentsToPedigree(fatherFamily, fhx);
        //        processFamilyParents(fatherFamily);
        //        processFamilyChildren(fatherFamily);
        //    }
        //}
        //private void processFamilyChildren(GedcomFamilyRecord family)
        //{
        //    if (family == null || family.Children == null)
        //        return;

        //    foreach (string childXref in family.Children)
        //    {
        //        GedcomIndividualRecord child = GetGedcomIndividualRecord(childXref);
        //        if (child == null)
        //            continue;

        //        foreach (GedcomFamilyLink familyLink in child.SpouseIn)
        //        {
        //            if (string.IsNullOrEmpty(familyLink.Family) == false) // && child.SpouseInFamily(familyLink.Family) == true)
        //            {
        //                GedcomFamilyRecord childFamily = null;
        //                foreach (GedcomFamilyRecord f in reader.Database.Families)
        //                {
        //                    if (f.XRefID == familyLink.Family)
        //                    {
        //                        childFamily = f;
        //                        break;
        //                    }
        //                }
        //                if (family != null && child.SpouseInFamily(childFamily.XRefID) == true)
        //                {
        //                    setChildrenFromGedcomFamily(childFamily); // , childFamily.Wife, childFamily.Husband);
        //                    processFamilyChildren(childFamily);
        //                }
        //            }
        //        }
        //    }
        //}
        //private GedcomFamilyRecord setProbandFamily()
        //{
        //    //now assign the proband
        //    idMapping[proband.XRefID] = 1;
          
        //    //now find the mother and father;
        //    string motherXref = "";
        //    string fatherXref = "";

        //    GedcomFamilyRecord family = GetParentsFromChild(proband.XRefID, out motherXref, out fatherXref);

        //    if (string.IsNullOrEmpty(motherXref) == false)
        //    {
        //        idMapping[motherXref] = 2;

        //        //now find the maternal Grandmother and Grandfather
        //        string matGrandmotherXref = "";
        //        string matGrandfatherXref = "";

        //        GetParentsFromChild(motherXref, out matGrandmotherXref, out matGrandfatherXref);

        //        if (string.IsNullOrEmpty(matGrandmotherXref) == false)
        //            idMapping[matGrandmotherXref] = 4;

        //        if (string.IsNullOrEmpty(matGrandfatherXref) == false)
        //            idMapping[matGrandfatherXref] = 5;

        //    }
        //    if (string.IsNullOrEmpty(fatherXref) == false)
        //    {
        //        idMapping[fatherXref] = 3;
        //        //now find the maternal Grandmother and Grandfather
        //        string patGrandmotherXref = "";
        //        string patGrandfatherXref = "";

        //        GetParentsFromChild(fatherXref, out patGrandmotherXref, out patGrandfatherXref);

        //        if (string.IsNullOrEmpty(patGrandmotherXref) == false)
        //            idMapping[patGrandmotherXref] = 6;

        //        if (string.IsNullOrEmpty(patGrandfatherXref) == false)
        //            idMapping[patGrandfatherXref] = 7;
        //    }


        //    //now read in the data
        //    foreach (string xref in idMapping.Keys)
        //    {
        //        int riskAppsId = idMapping[xref];
        //        if (riskAppsId > 0)
        //        {
        //            if (riskAppsId == 1)
        //            {
        //                bool noop = true;
        //            }
        //            GedcomIndividualRecord gedcomIndi = GetGedcomIndividualRecord(xref);
        //            SetPersonFromIndividual(riskAppsId, gedcomIndi, fhx);
        //        }
        //    }
        //    setChildrenFromGedcomFamily(family); // , motherXref, fatherXref);

        //    return family;
        //}

        //void setChildrenFromGedcomFamily(GedcomFamilyRecord family)
        //{
        //    if (family == null)
        //        return;

        //    bool addSpouse = false;

        //    Person motherPerson = getPerson(family.Wife);
        //    Person fatherPerson = getPerson(family.Husband);

        //    Person parentPerson = null;
            
        //    if (motherPerson == null && fatherPerson == null)
        //    {
        //        if (AddParentsToPedigree(family, fhx) == false)
        //        {
        //            Logger.Instance.WriteToLog("GedcomParser: could not find a parent for family=(xref=" + family.XRefID + ").");
        //            return;
        //        }
        //        motherPerson = getPerson(family.Wife);
        //        fatherPerson = getPerson(family.Husband);
        //    }
        //    if (motherPerson == null && fatherPerson == null)
        //    {
        //        Logger.Instance.WriteToLog("GedcomParser: could not find a parent for family=(xref=" + family.XRefID + ").");
        //        return;
        //    }
        //    parentPerson = motherPerson;
        //    if (motherPerson == null || fatherPerson == null)
        //    {
        //         if (fatherPerson == null)
        //        {
        //            parentPerson = motherPerson;
        //            addSpouse = true;
        //        }
        //        else if (motherPerson == null)
        //        {
        //            parentPerson = fatherPerson;
        //            addSpouse = true;
        //        }
        //    }


        //    try
        //    {
               
        //        if (family.Children != null && family.Children.Count > 0)
        //        {
        //            foreach (string childXref in family.Children)
        //            {
        //                Person childPerson = null;
        //                if (idMapping[childXref] < 0)
        //                {
        //                    GedcomIndividualRecord gedcomInd = GetGedcomIndividualRecord(childXref);
        //                    GedcomFamilyRecord f = gedcomInd.GetFamily();

        //                    GenderEnum gender = GenderEnum.Unknown;
        //                    if (gedcomInd.SexChar == "F")
        //                        gender = GenderEnum.Female;
        //                    if (gedcomInd.SexChar == "M")
        //                        gender = GenderEnum.Male;

        //                    childPerson = fhx.AddChild(parentPerson, false, 1, gender)[0];
        //                    Application.DoEvents();
        //                    //System.Threading.Thread.Sleep(5000);
        //                    idMapping[childXref] = childPerson.relativeID;

        //                    SetPersonFromIndividual(childPerson.relativeID, gedcomInd, fhx);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string stack = getShortStack();
        //        Logger.Instance.WriteToLog("RiskApps3.Utilities.GedcomParser setChildrenFromGedcomFamily " + e + " " + stack);
        //    }
        //}

        //private void SetPersonFromIndividual(int relativeID, GedcomIndividualRecord g, FamilyHistory fhx)
        //{
        //    if (g == null || gedcomPeople.ContainsKey(g.XRefID) == false) // ???????????
        //        return;

        //    try
        //    {
        //        GedcomName gname = g.GetName();
        //        string name = gname.Name.Replace("/", "");
        //        string surname = string.IsNullOrEmpty(gname.Surname) ? "" : gname.Surname;
        //        string firstName = string.IsNullOrEmpty(gname.Given) ? "" : gname.Given;
        //        string middleName = "";
        //        string[] given = gname.Given.Split(' ');
        //        if (given.Length > 1)
        //        {
        //            firstName = given[0];
        //            middleName = given[1];
        //        }
        //        string prefix = string.IsNullOrEmpty(gname.Prefix) ? "" : gname.Prefix;
        //        string suffix = string.IsNullOrEmpty(gname.Suffix) ? "" : gname.Suffix;

        //        DateTime myMinDate = Convert.ToDateTime("01/01/0100");
        //        string birthdate = g.Birth != null && g.Birth.Date != null ? g.Birth.Date.Date1.ToString() : "";
        //        string birthday = "";
        //        DateTime dob = DateTime.MinValue;
        //        if (g.Birth != null && g.Birth.Date != null && g.Birth.Date.DateTime1 != null && g.Birth.Date.DateTime1 > myMinDate && g.Birth.Date.DateTime1 <= DateTime.Now)
        //        {
        //            dob = (DateTime)g.Birth.Date.DateTime1;
        //            birthday = dob.ToShortDateString();
        //        }
        //        string deathDate = g.Death != null && g.Death.Date != null ? g.Death.Date.Date1.ToString() : "";
        //        DateTime dod = DateTime.MinValue;
        //        string deathDay = "";
        //        if (g.Death != null && g.Death.Date != null && g.Death.Date.DateTime1 != null && g.Death.Date.DateTime1 > myMinDate && g.Death.Date.DateTime1 <= DateTime.Now)
        //        {
        //            dod = (DateTime)g.Death.Date.DateTime1;
        //            deathDay = dod.ToShortDateString();
        //        }
        //        string vitalStatus = g.Dead == false && string.IsNullOrEmpty(deathDay) == true ? "Alive" : "Dead";
        //        string age = "";
        //        if (g.Dead == false)
        //        {
        //            if (birthday != "")
        //            {
        //                age = ((DateTime.MinValue + (DateTime.Now - dob)).Year - 1).ToString();
        //            }
        //        }
        //        else if (deathDay != "")
        //        {
        //            age = ((DateTime.MinValue + (dod - dob)).Year - 1).ToString();
        //        }

        //        string gender = GenderEnum.Unknown.ToString();
        //        if (String.IsNullOrEmpty(g.SexChar) == false)
        //        {
        //            if (g.SexChar == "M")
        //                gender = GenderEnum.Male.ToString();
        //            else if (g.SexChar == "F")
        //                gender = GenderEnum.Female.ToString();
        //        }
        //        foreach (Person p in fhx)
        //        {
        //            if (p.relativeID == relativeID)
        //            {
        //                bool alreadyProcessed = gedcomPeople.ContainsKey(g.XRefID) && gedcomPeople[g.XRefID] > 0 ? true : false;
        //                if (alreadyProcessed)
        //                {
        //                    Logger.Instance.WriteToLog("GedcomParser.SetPersonIndividualRecord already processed: name=" + name +
        //                        " born=" + birthday + "died=" + deathDay + " age=" + age + "vitalStatus=" + vitalStatus + "gender=" + gender);
        //                }
        //                else if (gedcomPeople.ContainsKey(g.XRefID) == false)
        //                {
        //                }
        //                else
        //                {
        //                    p.Person_name = name;
        //                    p.Person_firstName = firstName;
        //                    p.Person_middleName = middleName;
        //                    p.Person_lastName = surname;
        //                    p.Person_title = prefix;
        //                    p.Person_suffix = suffix;

        //                    p.Person_dob = birthday;
        //                    p.Person_dateOfDeath = deathDate;
        //                    p.Person_vitalStatus = vitalStatus;
        //                    p.Person_age = age;
        //                    p.Person_gender = gender;

        //                    gedcomPeople[g.XRefID] = p.relativeID;
        //                }
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string stack = getShortStack();
        //        Logger.Instance.WriteToLog("RiskApps3.Utilities.GedcomParser SetPersonFromIndividual " + e + " " + stack);
        //    }
        //}
        //private void SetPersonFromIndividual( GedcomIndividualRecord g, Person p)
        //{
 
        //    try
        //    {
        //        GedcomName gname = g.GetName();
        //        string name = gname.Name.Replace("/", "");
        //        string surname = string.IsNullOrEmpty(gname.Surname) ? "" : gname.Surname;
        //        string firstName = string.IsNullOrEmpty(gname.Given) ? "" : gname.Given;
        //        string middleName = "";
        //        string[] given = gname.Given.Split(' ');
        //        if (given.Length > 1)
        //        {
        //            firstName = given[0];
        //            middleName = given[1];
        //        }
        //        string prefix = string.IsNullOrEmpty(gname.Prefix) ? "" : gname.Prefix;
        //        string suffix = string.IsNullOrEmpty(gname.Suffix) ? "" : gname.Suffix;

        //        DateTime myMinDate = Convert.ToDateTime("01/01/0100");
        //        string birthdate = g.Birth != null && g.Birth.Date != null ? g.Birth.Date.Date1.ToString() : "";
        //        string birthday = "";
        //        DateTime dob = DateTime.MinValue;
        //        if (g.Birth != null && g.Birth.Date != null && g.Birth.Date.DateTime1 != null && g.Birth.Date.DateTime1 > myMinDate && g.Birth.Date.DateTime1 <= DateTime.Now)
        //        {
        //            dob = (DateTime)g.Birth.Date.DateTime1;
        //            birthday = dob.ToShortDateString();
        //        }
        //        string deathDate = g.Death != null && g.Death.Date != null ? g.Death.Date.Date1.ToString() : "";
        //        DateTime dod = DateTime.MinValue;
        //        string deathDay = "";
        //        if (g.Death != null && g.Death.Date != null && g.Death.Date.DateTime1 != null && g.Death.Date.DateTime1 > myMinDate && g.Death.Date.DateTime1 <= DateTime.Now)
        //        {
        //            dod = (DateTime)g.Death.Date.DateTime1;
        //            deathDay = dod.ToShortDateString();
        //        }
        //        string vitalStatus = g.Dead == false && string.IsNullOrEmpty(deathDay) == true ? "Alive" : "Dead";
        //        string age = "";
        //        if (g.Dead == false)
        //        {
        //            if (birthday != "")
        //            {
        //                age = ((DateTime.MinValue + (DateTime.Now - dob)).Year - 1).ToString();
        //            }
        //        }
        //        else if (deathDay != "")
        //        {
        //            age = ((DateTime.MinValue + (dod - dob)).Year - 1).ToString();
        //        }

        //        string gender = GenderEnum.Unknown.ToString();
        //        if (String.IsNullOrEmpty(g.SexChar) == false)
        //        {
        //            if (g.SexChar == "M")
        //                gender = GenderEnum.Male.ToString();
        //            else if (g.SexChar == "F")
        //                gender = GenderEnum.Female.ToString();
        //        }

        //        p.Person_name = name;
        //        p.Person_firstName = firstName;
        //        p.Person_middleName = middleName;
        //        p.Person_lastName = surname;
        //        p.Person_title = prefix;
        //        p.Person_suffix = suffix;

        //        p.Person_dob = birthday;
        //        p.Person_dateOfDeath = deathDate;
        //        p.Person_vitalStatus = vitalStatus;
        //        p.Person_age = age;
        //        p.Person_gender = gender;
                
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Instance.WriteToLog("RiskApps3.Utilities.GedcomParser SetPersonFromIndividual " + e.ToString());
        //    }
        //}
        //private Person getPerson(string xref)
        //{
        //    Person retval = null;
        //    if (string.IsNullOrEmpty(xref) == false && gedcomPeople.ContainsKey(xref) && gedcomPeople[xref] > 0)
        //    {
        //        retval = fhx.getRelative(gedcomPeople[xref]);
        //    }
        //    return retval;
        //}

        //private GedcomIndividualRecord GetGedcomIndividualRecord(int relativeID)
        //{
        //    GedcomIndividualRecord retval = null;
        //    if (relativeID > 0 && gedcomPeople.ContainsValue(relativeID))
        //    {
        //        foreach (string xref in gedcomPeople.Keys)
        //        {
        //            if (string.IsNullOrEmpty(xref) == false && gedcomPeople[xref] == relativeID)
        //                retval = GetGedcomIndividualRecord(xref);
        //            break;
        //        }
        //    }
        //    return retval;
        //}
        //private GedcomIndividualRecord GetGedcomIndividualRecord(string XRef)
        //{
        //    GedcomIndividualRecord retval = null;

        //    if (string.IsNullOrEmpty(XRef) == false)
        //    {
        //        foreach (GedcomIndividualRecord individual in reader.Database.Individuals)
        //        {
        //            if (XRef == individual.XRefID)
        //            {
        //                retval = individual;
        //                break;
        //            }
        //        }
        //    }
        //    return retval;
        //}
        //private GedcomFamilyRecord GetParentsFromChild(string queryXRef, out string motherXref, out string fatherXref)
        //{
        //    GedcomFamilyRecord ret = null;
        //    motherXref = "";
        //    fatherXref = "";

        //    foreach (GedcomFamilyRecord family in reader.Database.Families)
        //    {
        //        bool isFamily = false;
        //        if (family.Children != null)
        //        {
        //            foreach (string child in family.Children)
        //            {
        //                if (child == queryXRef)
        //                {
        //                    isFamily = true;
        //                    break;
        //                }
        //            }
        //        }
        //        if (isFamily)
        //        {
        //            ret = family;

        //            if (string.IsNullOrEmpty(family.Wife) == false)
        //            {
        //                motherXref = family.Wife;
        //            }
        //            if (string.IsNullOrEmpty(family.Husband) == false)
        //            {
        //                fatherXref = family.Husband;
        //            }
        //            break;
        //        }
        //    }
        //    return ret;
        //}

        //private List<string> GetChildrenFromParents(string queryXRef, string motherXRef, string fatherXref)
        //{
        //    motherXref = "";
        //    fatherXref = "";

        //    foreach (GedcomFamilyRecord family in reader.Database.Families)
        //    {
        //        bool isFamily = false;
        //        if (family.Children != null)
        //        {
        //            foreach (string child in family.Children)
        //            {
        //                if (child == queryXRef)
        //                {
        //                    isFamily = true;
        //                    break;
        //                }
        //            }
        //        }
        //        if (isFamily)
        //        {
        //            if (string.IsNullOrEmpty(family.Wife) == false)
        //            {
        //                motherXref = family.Wife;
        //            }
        //            if (string.IsNullOrEmpty(family.Husband) == false)
        //            {
        //                fatherXref = family.Husband;
        //            }
        //            break;
        //        }
        //    }
        //}
        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //private bool AddParentsToPedigree(GedcomFamilyRecord family, FamilyHistory fhx)
        //{
        //    if (family == null)
        //        return false;

        //    Person motherPerson = null;
        //    GedcomIndividualRecord gedcomMom = null;
            
        //    Person fatherPerson = null;
        //    GedcomIndividualRecord gedcomDad = null;

        //    try
        //    {
        //        if (string.IsNullOrEmpty(family.Wife) == false)
        //        {
        //            gedcomMom = GetGedcomIndividualRecord(family.Wife);
        //            if (gedcomPeople.Keys.Contains(family.Wife) && gedcomPeople[family.Wife] > 0)
        //                motherPerson = fhx.getRelative(gedcomPeople[family.Wife]);
        //        }
        //        if (motherPerson == null)
        //        {
        //            foreach (string childXref in family.Children)
        //            {
        //                if (gedcomPeople.ContainsKey(childXref) == true && gedcomPeople[childXref] > 0)
        //                {
        //                    Person child = fhx.getRelative(gedcomPeople[childXref]);
        //                    if (child != null && child.motherID > 0)
        //                        motherPerson = fhx.getRelative(child.motherID);
        //                    break;
        //                }
        //            }
        //        }                    

        //        if (string.IsNullOrEmpty(family.Husband) == false)
        //        {
        //            gedcomDad = GetGedcomIndividualRecord(family.Husband);
        //            if (gedcomPeople.Keys.Contains(family.Husband) && gedcomPeople[family.Husband] > 0)
        //                fatherPerson = fhx.getRelative(gedcomPeople[family.Husband]);
        //        }
        //        if (fatherPerson == null)
        //        {
        //            foreach (string childXref in family.Children)
        //            {
        //                if (gedcomPeople.ContainsKey(childXref) == true && gedcomPeople[childXref] > 0)
        //                {
        //                    Person child = fhx.getRelative(gedcomPeople[childXref]);
        //                    if (child != null && child.fatherID > 0)
        //                        fatherPerson = fhx.getRelative(child.fatherID);
        //                    break;
        //                }
        //            }
        //        }            

        //        if (motherPerson == null && fatherPerson == null)
        //        {
        //            List<Person> newParents = null;
        //            foreach (string childXref in family.Children)
        //            {
        //                Person childPerson = null;
        //                GedcomIndividualRecord gedchild = null;

        //                gedchild = GetGedcomIndividualRecord(childXref);
        //                if (gedcomPeople.Keys.Contains(childXref))
        //                    childPerson = getPerson(childXref); 

        //                if (childPerson != null && gedchild != null)
        //                {
        //                    newParents = fhx.AddParents(childPerson,null);
        //                    Application.DoEvents();
        //                    //Thread.Sleep(5000);
        //                    foreach (Person newbie in newParents)
        //                    {
        //                        if (newbie.gender == "Female")
        //                            motherPerson = newbie;
        //                        else if (newbie.gender == "Male")
        //                            fatherPerson = newbie;

        //                        SetPersonFromIndividual(idMapping[childXref], gedchild, fhx);
        //                    }
        //                    //todo
        //                    // SetPersonFromIndividual(idMapping[childXref], gedchild, fhx);
        //                    break;
        //                }
        //            }
        //            if (newParents != null)
        //            {
        //                foreach (string childXref in family.Children)
        //                {
        //                    //add children to the newParents that where found above...
        //                    //other than the one that's already there....

        //                    GedcomIndividualRecord gedchild = null;
        //                    gedchild = GetGedcomIndividualRecord(childXref);
        //                    Person childPerson;

        //                    if (idMapping[childXref] <= 0)
        //                    {
        //                        GenderEnum gender = GenderEnum.Unknown;
        //                        if (gedchild.SexChar == "F")
        //                            gender = GenderEnum.Female;
        //                        if (gedchild.SexChar == "M")
        //                            gender = GenderEnum.Male;

        //                        childPerson = fhx.AddChild(motherPerson, false, 1, gender)[0];
        //                        Application.DoEvents();
        //                        //System.Threading.Thread.Sleep(5000);

        //                        idMapping[childXref] = childPerson.relativeID;
        //                        SetPersonFromIndividual(idMapping[childXref], gedchild, fhx);
        //                    }
        //                    else
        //                    {
        //                        childPerson = getPerson(gedchild.XRefID);  // gedcomPeople[gedchild];
        //                        childPerson.Person_motherID = motherPerson.relativeID;
        //                        childPerson.Person_fatherID = fatherPerson.relativeID;
        //                    }
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        string stack = getShortStack();
        //        Logger.Instance.WriteToLog("RiskApps3.Utilities.GedcomParser  AddParentsToPedigree " +  e + " " + stack);
        //        return false;
        //    }
        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //private string getShortStack()
        //{
        //    return getShortStack(2, 1);
        //}
        //private string getShortStack(int topFrame, int bottomFrame)
        //{
        //    StackTrace stack = new StackTrace();
        //    String shortStack = "";

        //    for (int i = topFrame;  i  < stack.FrameCount && i >= bottomFrame && i >= 0; i--)
        //    {
        //        shortStack += " at " + stack.GetFrame(i).GetMethod().Name + "() ";
        //    }
        //    return shortStack;
        //}
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private class hraGedcomIndividual
        //{
        //    public GedcomIndividualRecord gedcomIndi;
        //    Person person;

        //    GedcomName gname;
        //    string name;
        //    string birthdate;
        //    DateTime dob;

        //    int relativeID;

        //    public hraGedcomIndividual(GedcomIndividualRecord g)
        //    {
        //        gedcomIndi = g;
        //        gname = gedcomIndi.GetName();
        //        name = gname.Name.Replace("/", "");
        //        birthdate = gedcomIndi.Birth != null && gedcomIndi.Birth.Date != null ? gedcomIndi.Birth.Date.Date1.ToString() : "";
        //        DateTime.TryParse(birthdate, out dob);

        //        person = new Person();
        //        person.name = gname.Name;
        //        person.dob = dob;
        //    }
        //}
         
    }    
         
}
