using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Utilities;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeImageView : HraView
    {
        private PedigreeGenerator pedGen;

        private Patient proband;

        public bool showBrcaScores = false;
        public bool showMmrScores = false;

        public bool delayedDraw = false;

        /**************************************************************************************************/
        public PedigreeImageView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void PedigreeImageView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += NewActivePatient;

            if (!delayedDraw)
            { 
                InitSelectedRelative();
            }
        }

        /**************************************************************************************************/
        public void DoDelayedDraw()
        {
            InitSelectedRelative();
        }
        /**************************************************************************************************/
        private void PedigreeImageView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (proband != null)
                proband.ReleaseListeners(this);

            SessionManager.Instance.RemoveHraView(this);
        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            if (proband != null)
                proband.ReleaseListeners(this);

            InitSelectedRelative();
        }

        /**************************************************************************************************/
        private void InitSelectedRelative()
        {
            proband = SessionManager.Instance.GetActivePatient();

            if (proband != null)
            {
                proband.AddHandlersWithLoad(activePatientChanged, activePatientLoaded, null);

                pedGen = new PedigreeGenerator(pictureBox1.Width, pictureBox1.Height);
                pedGen.showBrcaScores = showBrcaScores;
                pedGen.showMmrScores = showMmrScores;

                Bitmap b = pedGen.GeneratePedigreeImage();
                pictureBox1.Image = b;
            }
        }

        /**************************************************************************************************/
        public void RedrawPedigree()
        {
            if (proband != null)
            {
                pedGen = new PedigreeGenerator(pictureBox1.Width, pictureBox1.Height);
                pedGen.showBrcaScores = showBrcaScores;
                pedGen.showMmrScores = showMmrScores;

                Bitmap b = pedGen.GeneratePedigreeImage();
                pictureBox1.Image = b;
            }
        }
        /**************************************************************************************************/
        private void activePatientChanged(object sender, HraModelChangedEventArgs e)
        {

        }

        /**************************************************************************************************/

        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
