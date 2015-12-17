using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.View.PatientRecord;

namespace RiskApps3.View.RiskClinic.PatientSummary
{
    public partial class DiagnosticSummaryResultView : UserControl
    {
        /**************************************************************************************************/
        Diagnostic diagnostic;

        /**************************************************************************************************/
        public Diagnostic GetDiagnostic()
        {
            return diagnostic;
        }
        
        /**************************************************************************************************/
        public DiagnosticSummaryResultView(Diagnostic dx)
        {
            diagnostic = dx;
            dx.AddHandlersWithLoad(DxChanged, null, null);
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            this.ReleaseHandlers();
            base.Dispose(disposing);
        }

        /**************************************************************************************************/
        private void DiagnosticSummaryResultView_Load(object sender, EventArgs e)
        {
            label1.Text = TrimLongText(10,diagnostic.status);
            label2.Text = TrimLongText(10,diagnostic.date.ToShortDateString());
            label3.Text = TrimLongText(10,diagnostic.normal);
            label4.Text = TrimLongText(10,diagnostic.value);
            label5.Text = TrimLongText(10,diagnostic.laterality);
            
        }

        private string TrimLongText(int numChars, string text)
        {
            
            string retval = text;

            if (string.IsNullOrEmpty(retval) == false)
            {
                if (retval.Length > numChars)
                    retval = retval.Substring(0, numChars - 3) + "...";
            }
            return retval;
        }

        /**************************************************************************************************/
        public void ReleaseHandlers()
        {
            if (diagnostic != null)
                diagnostic.ReleaseListeners(this);
        }
        /**************************************************************************************************/
        private void DxChanged(object sender, HraModelChangedEventArgs e)
        {
            label1.Text = TrimLongText(10,diagnostic.status);
            label2.Text = TrimLongText(10,diagnostic.date.ToShortDateString());
            label3.Text = TrimLongText(10,diagnostic.normal);
            label4.Text = TrimLongText(10,diagnostic.value);
            label5.Text = TrimLongText(10,diagnostic.laterality);
        }

        private void DiagnosticSummaryResultView_DoubleClick(object sender, EventArgs e)
        {
            DiagnosticReportsView drv = new DiagnosticReportsView();
            drv.setInitialDx(diagnostic);
            drv.ShowDialog();
        }
    }
}




        //private void button1_Click(object sender, EventArgs e)
        //{
        //    FullReportTextPopup frtp = new FullReportTextPopup();
        //    frtp.FullReportText = diagnostic.report;
        //    frtp.ShowDialog();
        //    if (frtp.FullReportText != diagnostic.report)
        //    {
        //        diagnostic.report = frtp.FullReportText;
        //        diagnostic.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(null));
        //    }
        //}