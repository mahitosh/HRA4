using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.View.RiskClinic.PatientSummary;

namespace RiskApps3.View.RiskClinic
{
    public partial class LastKnownValueRow : UserControl
    {

        private string type;
        private bool selected;
        List<Diagnostic> Items = new List<Diagnostic>();
        Pen SelectedPen = new Pen(Color.Red, 3);
        ToolTip ttRec = new ToolTip();

        public event EventHandler Clicked;

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                groupBox1.Text = type;
            }
        }
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }
        
       

        public LastKnownValueRow()
        {
            InitializeComponent();
        }
        public void Clear()
        {
            Items.Clear();
            flowLayoutPanel1.Controls.Clear();
            Selected = false;
        }

        /***************************************************************/
        public void SetRec(String s)
        {
            if (String.IsNullOrEmpty(s))
            {
                groupBox1.Text = type;
            }
            else
            {
                groupBox1.Text = type + ": " + s;
            }
        }
        /***************************************************************/
        public void AddDiagnostic(Diagnostic d)
        {
            Items.Add(d);
            DiagnosticSummaryResultView dsrv = new DiagnosticSummaryResultView(d);
            flowLayoutPanel1.Controls.Add(dsrv);
        }

        /***************************************************************/
        public void RemoveDiagnostic(Diagnostic theStudy)
        {
            Control doomed = null;
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is DiagnosticSummaryResultView)
                {
                    DiagnosticSummaryResultView dsrv = (DiagnosticSummaryResultView)c;
                    if (dsrv.GetDiagnostic() == theStudy)
                    {
                        doomed = dsrv;
                    }
                }
            }
            if (doomed != null)
                flowLayoutPanel1.Controls.Remove(doomed);
        }

        /***************************************************************/
        private void Control_Click(object sender, EventArgs e)
        {
            if (Clicked != null)
            {
                Clicked(this, e);
            }
        }


        /***************************************************************/
        private void DiagnosticSummaryResultView_DateChaged(object sender, EventArgs e)
        {
            Items.Sort(Diagnostic.CompareDiagnosticByDate);
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                DiagnosticSummaryResultView dsrv = (DiagnosticSummaryResultView)c;
                flowLayoutPanel1.Controls.SetChildIndex(c, Items.IndexOf(dsrv.GetDiagnostic()));
            }
        }

        /***************************************************************/
        private void setToolTip30SecDelay(ToolTip tt)
        {
            //This works despite official MS documentation that says TT can only be visible for max of 5 seconds
            tt.AutomaticDelay = 30;
            tt.AutoPopDelay = 30000; //keep tool tip visible as long as 30 seconds
            tt.InitialDelay = 30;
            tt.ReshowDelay = 100;
        }


    }
}

