using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Utilities;
using System.Xml;
using System.Xml.Serialization;
using RiskApps3.Model.PatientRecord;
using System.Runtime.Serialization;
using System.IO;
using RiskApps3.Model.Clinic;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Threading;
using System.Data.SqlTypes;

namespace RiskApps3.View.Appointments
{
    public partial class CloudMRNReconcile : Form
    {

        HraCloudServices.RiskAppsCloudServices cloud;

        private string patientEnteredMRN;

        public string PatientEnteredMRN
        {
            get { return patientEnteredMRN; }
            set { patientEnteredMRN = value; }
        }
        private string patientName;

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        private int webApptID;

        public int WebApptID
        {
            get { return webApptID; }
            set { webApptID = value; }
        }

        public string retMRN;

        private string retName;

        /// <summary>
        /// Cloud Web Worklist To Edit Patient Entered MRN
        /// </summary>
        /// <param name="MRN"></param>
        /// <param name="Name"></param>
        /// 
        public CloudMRNReconcile(string MRN, string Name, int apptID, int ClinicID)
        {
           
            InitializeComponent();

            patientEnteredMRN = MRN;
            patientName = Name;
            webApptID = apptID;

            txtName.Text = Name;
            txtMRN.Text = MRN;
            
            retName = BCDB2.Instance.ExecuteScalarQuery("sp_getNameFromMRN @MRN=" + txtMRN.Text).ToString();

        }


        private void btnOK_Click(object sender, EventArgs e)
        {

            Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();
            pc.Add("MRN", txtMRN.Text);
         

            if ((retName != patientName) && (!String.IsNullOrEmpty(retName)))
            {
                errorProvider1.SetError(txtName, "Name and MRN discrepency found!");
                StringBuilder sb = new StringBuilder();
                sb.Append("There is an MRN associated with ");
                sb.Append("\"" + retName + "\" ");
                sb.Append("would you like to save appointment with patient identified name: ");
                sb.Append(patientName + "?");
                txtInstructions.Text = sb.ToString();
                retName = patientName;
            }
            else
            {
                //Test if mrn in text box changed from last iteration
                if (txtMRN.Text != patientEnteredMRN)
                {
                    txtInstructions.Text = "Is this correct?";
                    patientEnteredMRN = txtMRN.Text;
                }
                else
                {
                    retMRN = txtMRN.Text;
                    this.Close();
                }
            }
      
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            retMRN = "Cancel";
            this.Close();
        }
    }
}
