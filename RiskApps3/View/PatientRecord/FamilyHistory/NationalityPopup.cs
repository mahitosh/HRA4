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

namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    public partial class NationalityPopup : Form
    {
        /******************************************************************/
        public PastMedicalHistory pmh = null;
        public HraView sendingView = null;

        public NationalityPopup()
        {
            InitializeComponent();
        }

        private void AddDiseasePopup_Load(object sender, EventArgs e)
        {
        }
        /*
            string [] diseaseList = SessionManager.Instance.MetaData.Diseases
                .Where(
                    d => 
                        ((DiseaseObject)d).diseaseGender.Equals(pmh.RelativeOwningPMH.gender) ||
                        ((DiseaseObject)d).diseaseGender.Equals("Both")
                    )
                .Select(d => ((DiseaseObject)d).diseaseName)
                .Distinct()
                .ToArray();
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
                SessionManager.Instance.MetaData.Diseases.SetDataFromDiseaseName(ref co);

                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                args.Persist = true;

                pmh.Observations.AddToList(co, args);

                this.Close();
            }
        }
         */
    }
}
