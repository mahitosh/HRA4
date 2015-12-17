using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.Utilities;
using RiskApps3.Model.MetaData;

namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class PtFollowupRow : UserControl
    {

        private PtFollowup ptFollowup;
        private bool triggerEvents = false;

        private int ScrollIntervalValue = 5;
        private int HeaderHeightValue = 30;

        public PtFollowupRow(PtFollowup ptFollowup)
        {
            this.ptFollowup = ptFollowup;
            InitializeComponent();

            foreach (User u in SessionManager.Instance.MetaData.Users)
            {
                this.who.Items.Add(u);
            }
   
            UIUtils.fillComboBoxFromLookups(cboFollowupType, "tblFollowup", "FollowupType", true);
            UIUtils.fillComboBoxFromLookups(cboFollowupDisposition, "tblFollowup", "FollowupDisposition", true);
            UIUtils.fillComboBoxFromLookups(cboFollowupNoReason, "tblFollowup", "noApptReason", true);

            FillControls();
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

            //there may be a bug in the framework that causes us to have to do this
            //search for UserPreferenceChangedEventHandler memory leak
            this.txtFollowupDate.Dispose();
            base.Dispose(disposing);
        }

        public PtFollowup GetFollowup()
        {
            return ptFollowup;
        }
        private void FillControls()
        {
            triggerEvents = false;
            txtComment.Text = ptFollowup.Comment;
            cboFollowupType.Text = ptFollowup.TypeOfFollowup;
            cboFollowupDisposition.Text = ptFollowup.Disposition;
            cboFollowupNoReason.Text = ptFollowup.Reason;
            who.Text = ptFollowup.Who;

            if (ptFollowup.Date != null)
            {
                txtFollowupDate.Text = "" + ptFollowup.Date;
            }
            updateControls();
            triggerEvents = true;
        }

        private void updateControls()
        {
            if (cboFollowupDisposition.Text.ToUpper() == "NO APPOINTMENT MADE" ||
                cboFollowupDisposition.Text.ToUpper() == "OMIT FROM LIST")
            {
                //lblReason.Visible = true;
                cboFollowupNoReason.Visible = true;
            }
            else
            {
                //lblReason.Visible = false;
                cboFollowupNoReason.Visible = false;
                cboFollowupNoReason.Text = "";
            }
        }
        private void cboFollowupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (triggerEvents == false)
            {
                return;
            }
            ptFollowup.TypeOfFollowup = cboFollowupType.Text;
        }

        private void cboFollowupDisposition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (triggerEvents == false)
            {
                return;
            }

            ptFollowup.Disposition = cboFollowupDisposition.Text;

            updateControls();
        }

        private void cboFollowupNoReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (triggerEvents == false)
            {
                return;
            }
            ptFollowup.Reason = cboFollowupNoReason.Text;

        }

        private void txtComment_Validated(object sender, EventArgs e)
        {
            if (triggerEvents == false)
            {
                return;
            }
            ptFollowup.Comment = txtComment.Text;

        }

        private void txtFollowupDate_Validated(object sender, EventArgs e)
        {
            if (triggerEvents == false)
            {
                return;
            }
            ptFollowup.Date =DateTime.Parse(txtFollowupDate.Text);
        }

        public void SetScrollState(bool expanded)
        {
            if (expanded)
            {
                this.Height = txtComment.Location.Y + txtComment.Height + 3;
                label1.Text = "- Note";
            }
            else
            {
                this.Height = HeaderHeightValue;
                label1.Text = "+ Note";
            }
        }

        protected void DoScroll()
        {
            if (this.Height > HeaderHeightValue)
            {
                while (this.Height > HeaderHeightValue)
                {
                    Application.DoEvents();
                    this.Height -= ScrollIntervalValue;
                }
                //this.header.ImageIndex = 1;
                this.Height = HeaderHeightValue;
                label1.Text = "+ Note";
            }
            else if (this.Height == HeaderHeightValue)
            {
                //  int x = this.FixedHeight;
                int x = txtComment.Location.Y + txtComment.Height + 3;
                while (this.Height <= (x))
                {
                    Application.DoEvents();
                    this.Height += ScrollIntervalValue;
                }
                //this.header.ImageIndex = 0;
                this.Height = x;
                label1.Text = "- Note";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                DoScroll();
        }

        private void who_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.ptFollowup.Who = this.who.SelectedItem.ToString();
        }

    }
}
