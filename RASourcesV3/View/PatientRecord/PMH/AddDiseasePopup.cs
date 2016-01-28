using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.View.Common.AutoSearchTextBox;

namespace RiskApps3.View.PatientRecord.PMH
{
    public partial class AddDiseasePopup : Form
    {
        /******************************************************************/
        public PastMedicalHistory pmh = null;
        public HraView sendingView = null;

        public AddDiseasePopup()
        {
            InitializeComponent();

            AgeTextBox.addItem("");
            for (int i = 1; i < 120; i++)
            {
                AgeTextBox.addItem(i.ToString());
            }
        }

        private void AddDiseasePopup_Load(object sender, EventArgs e)
        {
            string [] diseaseList = SessionManager.Instance.MetaData.Diseases
                .Where(
                    d => 
                        d.diseaseGender.Equals(pmh.RelativeOwningPMH.gender) ||
                        d.diseaseGender.Equals("Both")
                    )
                .Select(d => d.diseaseName)
                .Distinct()
                .ToArray();

            Array.Sort(diseaseList);

            diseaseComboBox.Items.Clear();
            diseaseComboBox.AutoCompleteItems.Clear();

            diseaseComboBox.Items.AddRange(diseaseList);
            diseaseComboBox.AutoCompleteItems.AddRange(diseaseList.Select(d => new AutoCompleteEntry(d, d)).ToArray());

            diseaseComboBox.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pmh != null && sendingView != null)
            {
                ClincalObservation co = new ClincalObservation(pmh);
                co.disease = diseaseComboBox.Text;
                
                co.ageDiagnosis = AgeTextBox.Text;
                
                co.SetDiseaseDetails();
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                args.Persist = true;

                pmh.Observations.AddToList(co, args);

                this.Close();
            }
        }

        internal void SetDiseaseBoxFocus()
        {
            diseaseComboBox.Focus();
        }
    }
}





//co.Set_disease(diseaseComboBox.Text, this);

//SessionManager.Instance.MetaData.Diseases.SetDataFromDiseaseName(ref co);

                //int nextInstanceID = 1;
                //foreach (ClincalObservation existing in pmh.Observations)
                //{
                //    if (existing.instanceID >= nextInstanceID)
                //        nextInstanceID = existing.instanceID + 1;
                //}
                //ClincalObservation co = new ClincalObservation(pmh);
                //co.instanceID = 0; // nextInstanceID;
                //co.disease = diseaseComboBox.Text;
                //co.ageDiagnosis = AgeTextBox.Text;
                //SessionManager.Instance.MetaData.Disease.SetDataFromDiseaseName(ref co);
                //pmh.Observations.Add(co);

                //HraModelChangedEventArgs co_args = new RiskApps3.Model.HraModelChangedEventArgs(sendingView, SessionManager.Instance.securityContext);
                //co.SignalModelChanged(co_args);

                //HraModelChangedEventArgs pmh_args = new RiskApps3.Model.HraModelChangedEventArgs(sendingView, SessionManager.Instance.securityContext);
                //pmh_args.Persist = false;
                //pmh.SignalModelChanged(pmh_args);