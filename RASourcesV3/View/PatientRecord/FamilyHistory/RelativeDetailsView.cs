using System;
using System.ComponentModel;
using System.Drawing;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using System.Windows.Forms;
using System.Reflection;
using RiskApps3.Controllers;
using System.Collections.Generic;
using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.Model.PatientRecord.Pedigree;
using System.Diagnostics;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    public partial class RelativeDetailsView : HraView
    {
        /**************************************************************************************************/
        private Person selectedRelative;

        /**************************************************************************************************/

        public RelativeDetailsView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/

        private void RelativeDetailsView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient +=
                new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            SessionManager.Instance.RelativeSelected +=
                new RiskApps3.Controllers.SessionManager.RelativeSelectedEventHandler(RelativeSelected);
            InitSelectedRelative();
        }

        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitSelectedRelative();
        }

        /**************************************************************************************************/

        private void RelativeSelected(RelativeSelectedEventArgs e)
        {
            InitSelectedRelative();
        }

        /**************************************************************************************************/
        private void InitSelectedRelative()
        {
            //  get selected relative object from session manager
            if (selectedRelative != null)
                selectedRelative.ReleaseListeners(this);

            selectedRelative = SessionManager.Instance.GetSelectedRelative();

            ClearUI();

            if (selectedRelative != null)
            {
                selectedRelative.AddHandlersWithLoad(selectedRelativeChanged, selectedrelativeLoaded, null);

                selectedRelative.Ethnicity.AddHandlersWithLoad(EthnicityListChanged,
                                                               EthnicityListLoaded,
                                                               null);

                selectedRelative.Nationality.AddHandlersWithLoad(NationalityChanged,
                                                                 NationalityLoaded,
                                                                 null);
            }
            else
            {
                this.Enabled = false;
            }
        }

        /**************************************************************************************************/
        private void ClearUI()
        {
            foreach (Control c in this.Controls)
            {
                if (c is ComboBox || c is TextBox)
                {
                    c.Text = "";
                }
            }

            //for (int i = 0; i < checkedListBox1.Items.Count; i++)
            //{
            //    checkedListBox1.SetItemChecked(i, false);
            //}
            //for (int i = 0; i < checkedListBox2.Items.Count; i++)
            //{
            //    checkedListBox2.SetItemChecked(i, false);
            //}
        }

        /**************************************************************************************************/
        private void EthnicityListLoaded(HraListLoadedEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            checkedListBox1.Enabled = true;

            foreach (Race r in selectedRelative.Ethnicity)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (string.Compare(checkedListBox1.Items[i].ToString(), r.race, true) == 0)
                    {
                        checkedListBox1.SetItemChecked(i, true);
                        //checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                        break;
                    }
                }
            }
            
        }
        /**************************************************************************************************/
        private void NationalityLoaded(HraListLoadedEventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
            checkedListBox2.Enabled = true;

            foreach (Nation n in selectedRelative.Nationality)
            {
                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    if (string.Compare(checkedListBox2.Items[i].ToString(), n.nation, true) == 0)
                    {
                        checkedListBox2.SetItemChecked(i, true);
                        //checkedListBox2.SetItemCheckState(i, CheckState.Checked);
                        break;
                    }
                }
            }
        }

        /**************************************************************************************************/
        private void EthnicityListChanged(HraListChangedEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }

            foreach (Race r in selectedRelative.Ethnicity)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (string.Compare(checkedListBox1.Items[i].ToString(), r.race, true) == 0)
                    {
                        checkedListBox1.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }

        /**************************************************************************************************/
        private void NationalityChanged(HraListChangedEventArgs e)
        {

        }

        /**************************************************************************************************/

        private void FillControls()
        {
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;

            if (selectedRelative != null)
            {
                this.Enabled = true;
                this.ClearUI();
                
                
                name.Text = selectedRelative.name;
                firstName.Text = selectedRelative.firstName;
                middleName.Text = selectedRelative.middleName;
                lastName.Text = selectedRelative.lastName;
                title.Text = selectedRelative.title;
                suffix.Text = selectedRelative.suffix;
                gender.Text = selectedRelative.gender;
                maidenName.Text = selectedRelative.maidenName;
                causeOfDeath.Text = selectedRelative.causeOfDeath;
                dateOfDeath.Text = selectedRelative.dateOfDeath;
                dob.Text = selectedRelative.dob;
                dobConfidence.Text = selectedRelative.dobConfidence;
                dateOfDeathConfidence.Text = selectedRelative.dateOfDeathConfidence;
                age.Text = selectedRelative.age;
                vitalStatus.Text = selectedRelative.vitalStatus;
                city.Text = selectedRelative.city;
                state.Text = selectedRelative.state;
                zipCode.Text = selectedRelative.zip;
                adopted.Text = selectedRelative.adopted;

                isAshkenaziComboBox.Text = selectedRelative.Person_isAshkenazi;
                isHispanicComboBox.Text = selectedRelative.Person_isHispanic;
                commentsTextBox.Text = selectedRelative.Person_comment;

                comboBox1.Text = selectedRelative.Person_adoptedFhxKnown;
            }
            else
            {
                this.ClearUI();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    checkedListBox2.SetItemChecked(i, false);
                }
            }
        }

        /**************************************************************************************************/
        delegate void selectedRelativeChangedCallback(object sender, HraModelChangedEventArgs e);
        private void selectedRelativeChanged(object sender, HraModelChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                selectedRelativeChangedCallback srcc = new selectedRelativeChangedCallback(selectedRelativeChanged);
                object[] args = new object[2];
                args[0] = sender;
                args[2] = e;
                this.Invoke(srcc, args);
            }
            else
            {
                // handles changes to the current relative and NOT a change from one relative to another
                FillControls();
                if (e.sendingView != this)
                {
                    foreach (MemberInfo mi in e.updatedMembers)
                    {
                        switch (mi.Name)
                        {
                            case "name":
                                name.Text = selectedRelative.name;
                                break;
                            case "firstName":
                                firstName.Text = selectedRelative.firstName;
                                break;
                            case "middleName":
                                middleName.Text = selectedRelative.middleName;
                                break;
                            case "lastName":
                                lastName.Text = selectedRelative.lastName;
                                break;
                            case "title":
                                title.Text = selectedRelative.title;
                                break;
                            case "suffix":
                                suffix.Text = selectedRelative.suffix;
                                break;
                            case "gender":
                                gender.Text = selectedRelative.gender;
                                break;
                            case "maidenName":
                                maidenName.Text = selectedRelative.maidenName;
                                break;
                            case "causeOfDeath":
                                causeOfDeath.Text = selectedRelative.causeOfDeath;
                                break;
                            case "dateOfDeath":
                                dateOfDeath.Text = selectedRelative.dateOfDeath;
                                break;
                            case "dob":
                                dob.Text = selectedRelative.dob;
                                break;
                            case "dobConfidence":
                                dobConfidence.Text = selectedRelative.dobConfidence;
                                break;
                            case "dateOfDeathConfidence":
                                dateOfDeathConfidence.Text = selectedRelative.dateOfDeathConfidence;
                                break;
                            case "age":
                                age.Text = selectedRelative.age;
                                break;
                            case "vitalStatus":
                                vitalStatus.Text = selectedRelative.vitalStatus;
                                break;
                            case "city":
                                city.Text = selectedRelative.city;
                                break;
                            case "state":
                                city.Text = selectedRelative.state;
                                break;
                            case "zip":
                                zipCode.Text = selectedRelative.zip;
                                break;
                            case "adopted":
                                adopted.Text = selectedRelative.adopted;
                                break;
                            case "isAshkenazi":
                                isAshkenaziComboBox.Text = selectedRelative.Person_isAshkenazi;
                                break;
                            case "isHispanic":
                                isHispanicComboBox.Text = selectedRelative.Person_isHispanic;
                                break;
                            case "comment":
                                commentsTextBox.Text = selectedRelative.Person_comment;
                                break;

                        }
                    }
                }
            }
        }

        /**************************************************************************************************/
        private void selectedrelativeLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void RelativeDetailsView_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }

        /**************************************************************************************************/
        private void age_Validated(object sender, EventArgs e)
        {
            //selectedRelative.Person_age = age.Text;
            selectedRelative.Set_age(age.Text,this);
        }

        /**************************************************************************************************/
        private void gender_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string current = selectedRelative.Person_gender;
            selectedRelative.Person_gender = gender.Text;

            // begin jdg 11/30/15 per brian, do not allow gender change if selected relative has children
            if(current != selectedRelative.Person_gender)
            {
                if (relativeHasChildren(selectedRelative))
                {
                    selectedRelative.Person_gender = current;   // undo change;
                    MessageBox.Show("You cannot change the gender of person that has children.", "RiskApps", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // end jdg 11/30/15

            if ((current == "Male" && gender.Text == "Female") ||
                (current == "Female" && gender.Text == "Male"))
            {
                selectedRelative.Person_relationship = Relationship.toString(Relationship.getAlternateGenderRelationship(Relationship.Parse(selectedRelative.relationship)));
            }

        }

        /// <summary>
        /// Traverse the owningFHx to see if this person is a parent.  jdg 11/30/15
        /// </summary>
        /// <param name="p">Person object</param>
        /// <returns>true if there are children, false otherwise</returns>
        private bool relativeHasChildren(Person p)
        {
            foreach (Person pp in p.owningFHx)
            {
                if((pp.fatherID == p.relativeID) || (pp.motherID == p.relativeID))
                {
                    return true;
                }
            }
            return false;
        }

        private void vitalStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //selectedRelative.Person_vitalStatus = vitalStatus.SelectedItem.ToString();
            selectedRelative.Set_vitalStatus(vitalStatus.SelectedItem.ToString(),this);
        }

        private void adopted_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //selectedRelative.Person_adopted = adopted.Text;
            selectedRelative.Set_adopted(adopted.SelectedItem.ToString(), this);
        }

        private void title_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //selectedRelative.Person_title = title.Text;
            selectedRelative.Set_title(title.SelectedItem.ToString(), this);
            SetNameFromParts();
        }

        private void firstName_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_firstName(firstName.Text,this);

            SetNameFromParts();
        }

        private void SetNameFromParts()
        {
            string pending = (title.Text + " " +
            firstName.Text + " " +
            middleName.Text + " " +
            lastName.Text + " " +
            suffix.Text).Trim();

            if (pending.Length > 0)
            {
                name.Text = pending;
                selectedRelative.Person_name = name.Text;
            }
        }
        private void middleName_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_middleName(middleName.Text,this);
            SetNameFromParts();
        }

        private void lastName_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_lastName(lastName.Text,this);
            SetNameFromParts();
        }

        private void suffix_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_suffix(suffix.Text,this);
            SetNameFromParts();
        }

        private void maidenName_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_maidenName(maidenName.Text,this);
            SetNameFromParts();
        }

        private void dob_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_dob(dob.Text,this);
            try
            {
                DateTime dt;
                if (DateTime.TryParse(dob.Text, out dt))
                {
                    if (selectedRelative.vitalStatus != "Dead")
                    {
                        selectedRelative.Person_age = (DateTime.Now.Year - dt.Year).ToString();
                        age.Text = selectedRelative.Person_age;
                    }
                    else
                    {
                        DateTime deathtime;
                        if (DateTime.TryParse(dateOfDeath.Text, out deathtime))
                        {
                            DateTime zeroTime = new DateTime(1, 1, 1);
                            TimeSpan span = deathtime - dt;
                            int years = (zeroTime + span).Year - 1;

                            selectedRelative.Person_age = years.ToString();
                            age.Text = selectedRelative.Person_age;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Logger.Instance.WriteToLog(exc.ToString());
            }
        }

        private void dobConfidence_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dobConfidence.SelectedItem != null)
                selectedRelative.Set_dobConfidence(dobConfidence.SelectedItem.ToString(), this);
        }

        private void dateOfDeath_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_dateOfDeath(dateOfDeath.Text,this);

            try
            {
                DateTime dt;
                if (DateTime.TryParse(dob.Text, out dt))
                {

                    DateTime deathtime;
                    if (DateTime.TryParse(dateOfDeath.Text, out deathtime))
                    {
                        selectedRelative.Set_vitalStatus("Dead", this);
                        vitalStatus.Text = "Dead";

                        DateTime zeroTime = new DateTime(1, 1, 1);
                        TimeSpan span = deathtime - dt;
                        int years = (zeroTime + span).Year - 1;

                        selectedRelative.Person_age = years.ToString();
                        age.Text = selectedRelative.Person_age;
                    }
                    
                }
            }
            catch (Exception exc)
            {
                Logger.Instance.WriteToLog(exc.ToString());
            }
        }

        private void dateOfDeathConfidence_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dateOfDeathConfidence.SelectedItem != null)
                selectedRelative.Set_dateOfDeathConfidence(dateOfDeathConfidence.SelectedItem.ToString(), this);
        }

        private void causeOfDeath_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (causeOfDeath.SelectedItem != null)
                selectedRelative.Set_causeOfDeath(causeOfDeath.SelectedItem.ToString(),this);
        }

        private void city_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_city(city.Text,this);
        }

        private void state_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (state.SelectedItem != null)
                selectedRelative.Set_state(state.SelectedItem.ToString(), this);
        }

        private void zipCode_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_zip(zipCode.Text,this);
        }

        private void isAshkenaziComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectedRelative.Set_isAshkenazi(isAshkenaziComboBox.Text,this);
        }

        private void isHispanicComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectedRelative.Set_isHispanic(isHispanicComboBox.Text,this);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedRelative != null)
            {
                if (checkedListBox1.CheckedItems.Contains(checkedListBox1.SelectedItem))
                {
                    List<int> descedants = new List<int>();
                    descedants.Add(selectedRelative.relativeID);
                    selectedRelative.owningFHx.GetDescendants(selectedRelative.relativeID, ref descedants);
                    foreach (int i in descedants)
                    {
                        Person p = selectedRelative.owningFHx.getRelative(i);
                        if (p != null)
                        {
                            Race r = new Race(p.Ethnicity);
                            r.race = checkedListBox1.SelectedItem.ToString();

                            p.Ethnicity.AddToList(r, new HraModelChangedEventArgs(this));
                        }
                    }
                }
                else
                {
                    Race doomed = null;
                    foreach (Race r in selectedRelative.Ethnicity)
                    {
                        if (string.Compare(checkedListBox1.SelectedItem.ToString(), r.race, true) == 0)
                        {
                            doomed = r;
                            break;
                        }

                    }
                    if (doomed != null)
                    {
                        selectedRelative.Ethnicity.RemoveFromList(doomed, SessionManager.Instance.securityContext);
                    }
                }
            }
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (selectedRelative != null)
            {
                if (checkedListBox2.CheckedItems.Contains(checkedListBox2.SelectedItem))
                {
                    List<int> descedants = new List<int>();
                    descedants.Add(selectedRelative.relativeID);
                    selectedRelative.owningFHx.GetDescendants(selectedRelative.relativeID, ref descedants);
                    foreach (int i in descedants)
                    {
                        Person p = selectedRelative.owningFHx.getRelative(i);
                        if (p != null)
                        {
                            Nation n = new Nation(p.Nationality);
                            n.nation = checkedListBox2.SelectedItem.ToString();
                            if (p.Nationality.IsLoaded)
                            {
                                p.Nationality.AddToList(n, new HraModelChangedEventArgs(this));
                            }
                            else
                            {
                                p.Nationality.BackgroundListLoad();
                                p.Nationality.AddToList(n, new HraModelChangedEventArgs(this));
                            }
                        }
                    }
                }
                else
                {
                    Nation doomed = null;
                    foreach (Nation n in selectedRelative.Nationality)
                    {
                        if (string.Compare(checkedListBox2.SelectedItem.ToString(), n.nation, true) == 0)
                        {
                            doomed = n;
                            break;
                        }

                    }
                    if (doomed != null)
                    {
                        selectedRelative.Nationality.RemoveFromList(doomed, SessionManager.Instance.securityContext);
                    }
                }
            }
        }

        private void name_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_name(name.Text,this);
        }

        private void commentsTextBox_Validated(object sender, EventArgs e)
        {
            selectedRelative.Set_comment(commentsTextBox.Text,this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (groupBox2.Size.Height < 315)
            {
                while (groupBox2.Size.Height < (315))
                {
                    Application.DoEvents();
                    groupBox2.Height += 5;
                }
                label1.Text = "Less";
            }
            else
            {
                while (groupBox2.Size.Height > (105))
                {
                    Application.DoEvents();
                    groupBox2.Height -= 5;
                    label1.Text = "More";
                }
            }
            //int x = flowLayoutPanel1.Location.Y + SumHeight(flowLayoutPanel1) + pad;
            //while (this.Height < (x))
            //{
            //    Application.DoEvents();
            //    this.Height += ScrollIntervalValue;
            //}
            //this.expanderToggle.ImageIndex = 0;
            //this.Height = x;

        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectedRelative.Person_adoptedFhxKnown = comboBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt;
                if (DateTime.TryParse(dob.Text, out dt))
                {
                    if (selectedRelative.vitalStatus != "Dead")
                    {

                        DateTime today = DateTime.Today;
                        int patientsAge = (DateTime.Now.Year - dt.Year);
                        if (dt > today.AddYears(-patientsAge)) patientsAge--;

                        selectedRelative.Person_age = patientsAge.ToString();
                        age.Text = selectedRelative.Person_age;

                    }
                    else
                    {
                        DateTime deathtime;
                        if (DateTime.TryParse(dateOfDeath.Text, out deathtime))
                        {
                            DateTime zeroTime = new DateTime(1, 1, 1);
                            TimeSpan span = deathtime - dt;
                            int years = (zeroTime + span).Year - 1;

                            selectedRelative.Person_age = years.ToString();
                            age.Text = selectedRelative.Person_age;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Logger.Instance.WriteToLog(exc.ToString());
            }
        }

    }
}