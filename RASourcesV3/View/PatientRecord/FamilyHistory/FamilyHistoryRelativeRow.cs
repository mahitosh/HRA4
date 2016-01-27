using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Model;
using System.Threading;

namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    public partial class FamilyHistoryRelativeRow : UserControl
    {
        /**************************************************************************************************/
        private Person relative = null;
        private PastMedicalHistory pmh;

        /**************************************************************************************************/
        public FamilyHistoryRelativeRow(Person person)
        {
            InitializeComponent();
            relative = person;
            comboBox2.Items.Add("");
            comboBox6.Items.Add("");
            comboBox8.Items.Add("");
            //foreach (DiseaseObject o in SessionManager.Instance.MetaData.Diseases)
            //{
            //    if (o.groupingName == "Breast/Ovarian")
            //    {
            //        comboBox2.Items.Add(o);
            //        comboBox6.Items.Add(o);
            //        comboBox8.Items.Add(o);
            //    }
            //}
            SetDeleteButton();
        }
        /**************************************************************************************************/
        public void SetDeleteButton()
        {
            button1.Enabled = false;
            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
        /**************************************************************************************************/
        private void FamilyHistoryRelativeRow_Load(object sender, EventArgs e)
        {
            if (relative != null)
            {
                relative.AddHandlersWithLoad(relativeChanged, relativeLoaded, null);
            }
        }

        /**************************************************************************************************/
        public Person GetRelative()
        {
            return relative;
        }

        /**************************************************************************************************/
        private void relativeChanged(object sender, HraModelChangedEventArgs e)
        {

        }


        /**************************************************************************************************/
        private void relativeLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = "";

            if (string.IsNullOrEmpty(relative.bloodline) || relative.relationship.ToLower() == "mother" || relative.relationship.ToLower() == "father")
            {
                if (relative.relationship.ToLower() != "other")
                    label1.Text = relative.relationship;
            }
            else
            {
                if (relative.bloodline.ToLower() != "unknown" && relative.bloodline.ToLower() != "both")
                    label1.Text = relative.bloodline + " " + relative.relationship;
                else
                    label1.Text = relative.relationship;
            }

            if (label1.Text.Length == 0)
            {
                if (!string.IsNullOrEmpty(relative.relationshipOther))
                {
                    label1.Text = relative.relationshipOther;
                }
            }
            
            textBox1.Text = relative.age;

            comboBox1.Text = relative.vitalStatus;

            LoadOrGetPMH();
        }

        /**************************************************************************************************/
        private void LoadOrGetPMH()
        {
            if (pmh != null)
            {
                pmh.Observations.ReleaseListeners(this);
            }

            pmh = relative.PMH;

            pmh.Observations.AddHandlersWithLoad(ClinicalObservationListChanged,
                             ClinicalObservationListLoaded,
                             ClinicalObservationChanged);
        }
        
        /**************************************************************************************************/
        private void ClinicalObservationListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null && relative != null)
            {
                ClincalObservation co = (ClincalObservation)e.hraOperand;
            }
        }

        /**************************************************************************************************/
        private void ClinicalObservationListLoaded(HraListLoadedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void ClinicalObservationChanged(object sender, HraModelChangedEventArgs e)
        {
            int senderId = ((ClincalObservation)sender).instanceID;

            if (e.Delete)
            {
               
            }
            else
            {

            }
        }

        /**************************************************************************************************/
        delegate void FillControlsCallback();
        private void FillControls()
        {
            if (Thread.CurrentThread.Name != "MainGUI")
            {
                FillControlsCallback rmc = new FillControlsCallback(FillControls);
                this.Invoke(rmc, null);
            }
            else
            {

                for (int i = 0; i < pmh.Observations.Count; i++)
                {
                    ClincalObservation co = (ClincalObservation)pmh.Observations[i];
                    switch (i)
                    {
                        case 0:
                            comboBox2.Tag = co;
                            comboBox2.Text = co.disease;
                            textBox2.Text = co.ageDiagnosis;
                            break;
                        case 1:
                            comboBox6.Tag = co;
                            comboBox6.Text = co.disease;
                            textBox3.Text = co.ageDiagnosis;
                            break;
                        case 2:
                            comboBox8.Tag = co;
                            comboBox8.Text = co.disease;
                            textBox4.Text = co.ageDiagnosis;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void comboBox2_Validated(object sender, EventArgs e)
        {
            if (comboBox2.Tag != null)
            {
                ClincalObservation co = (ClincalObservation)comboBox2.Tag;
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                if (comboBox2.Text.Length > 0)
                {
                    co.disease = comboBox2.Text;
                    co.ageDiagnosis = textBox2.Text;
                    co.SetDiseaseDetails();
                }
                else
                {
                    args.Delete = true;
                }
                
                co.SignalModelChanged(args);
            }
            else if (comboBox2.Text.Length > 0)
            {
                ClincalObservation co = new ClincalObservation(pmh);
                co.disease = comboBox2.Text;
                co.ageDiagnosis = textBox2.Text;
                co.SetDiseaseDetails();
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                pmh.Observations.AddToList(co, args);
                comboBox2.Tag = co;
            }
        }

        private void comboBox6_Validated(object sender, EventArgs e)
        {
            if (comboBox6.Tag != null)
            {
                ClincalObservation co = (ClincalObservation)comboBox6.Tag;
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                if (comboBox6.Text.Length > 0)
                {
                    co.disease = comboBox6.Text;
                    co.ageDiagnosis = textBox3.Text;
                    co.SetDiseaseDetails();
                }
                else
                {
                    args.Delete = true;
                }
                co.SignalModelChanged(args);
            }
            else if (comboBox6.Text.Length > 0)
            {
                ClincalObservation co = new ClincalObservation(pmh);
                co.disease = comboBox6.Text;
                co.ageDiagnosis = textBox3.Text;
                co.SetDiseaseDetails();
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                pmh.Observations.AddToList(co, args);
                comboBox6.Tag = co;
            }
        }

        private void comboBox8_Validated(object sender, EventArgs e)
        {
            if (comboBox8.Tag != null)
            {
                ClincalObservation co = (ClincalObservation)comboBox8.Tag;
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                if (comboBox8.Text.Length > 0)
                {
                    co.disease = comboBox8.Text;
                    co.ageDiagnosis = textBox4.Text;
                    co.SetDiseaseDetails();
                }
                else
                {
                    args.Delete = true;
                }
                co.SignalModelChanged(args);
            }
            else if (comboBox8.Text.Length > 0)
            {
                ClincalObservation co = new ClincalObservation(pmh);
                co.disease = comboBox8.Text;
                co.ageDiagnosis = textBox4.Text;
                co.SetDiseaseDetails();
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                pmh.Observations.AddToList(co, args);
                comboBox8.Tag = co;
            }
        }

        private void comboBox1_Validated(object sender, EventArgs e)
        {
            relative.Set_vitalStatus(comboBox1.Text, null);
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            relative.Set_age(textBox1.Text, null);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            relative.owningFHx.RemoveFromList(relative, null);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool retval = true;

            if (relative == null)
                return;

            if (relative.relativeID < 8)
            {
                retval = false;
            }
            else
            {
                foreach (Person p in relative.owningFHx)
                {
                    if (p.motherID == relative.relativeID || p.fatherID == relative.relativeID)
                    {
                        retval = false;
                        break;
                    }
                }
            }

            e.Result = retval;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool retval = false;
            if (e.Result != null)
            {
                retval = (bool)e.Result;

                button1.Enabled = retval;
            }

        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void OpenDiseaseDetails(ClincalObservation co)
        {

            if (co != null && co.Details != null)
            {
                if (co.Details.GetType().ToString() == "RiskApps3.Model.PatientRecord.PMH.BreastCancerDetails")
                {
                    RiskApps3.View.PatientRecord.PMH.BreastCancerDetailsView bcdv = new RiskApps3.View.PatientRecord.PMH.BreastCancerDetailsView((RiskApps3.Model.PatientRecord.PMH.BreastCancerDetails)(co.Details));
                    bcdv.ShowDialog();
                }
                if (co.Details.GetType().ToString() == "RiskApps3.Model.PatientRecord.PMH.ColonCancerDetails")
                {
                    RiskApps3.View.PatientRecord.PMH.ColonCancerDetailsView bcdv = new RiskApps3.View.PatientRecord.PMH.ColonCancerDetailsView((RiskApps3.Model.PatientRecord.PMH.ColonCancerDetails)(co.Details));
                    bcdv.ShowDialog();
                }
            }
        }
        private void diseaseDetails1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Tag != null)
            {
       
                OpenDiseaseDetails((ClincalObservation)comboBox2.Tag);
            }
            else if (comboBox2.Text.Length > 0)
            {
                OpenDiseaseDetails(new ClincalObservation(pmh));
            }
        }

        private void diseaseDetails2_Click(object sender, EventArgs e)
        {
            if (comboBox6.Tag != null)
            {

                OpenDiseaseDetails((ClincalObservation)comboBox6.Tag);
            }
            else if (comboBox6.Text.Length > 0)
            {
                OpenDiseaseDetails(new ClincalObservation(pmh));
            }
        }

        private void diseaseDetails3_Click(object sender, EventArgs e)
        {
            if (comboBox8.Tag != null)
            {

                OpenDiseaseDetails((ClincalObservation)comboBox8.Tag);
            }
            else if (comboBox8.Text.Length > 0)
            {
                OpenDiseaseDetails(new ClincalObservation(pmh));
            }
        } 

    }
}
