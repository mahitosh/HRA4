using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Utilities;
using RiskApps3.Controllers;
using RiskApps3.Model;

namespace RiskApps3.View.PatientRecord.Risk
{
    public partial class RelativeToConsiderRow : UserControl
    {
        public int brcapro = 0;
        public int relId = -1;

        public string relativeValue;

        public HraView parentView;

        public RelativeToConsiderRow(HraView parentView)
        {
            this.parentView = parentView;
            InitializeComponent();
            UIUtils.fillComboBoxFromLookups(testingWillingnessComboBox, "tblRiskDataRelatives", "testingWillingness",
                                true);

        }

        public bool ShowDisposition
        {
            get
            {
                return testingWillingnessComboBox.Visible;
            }
            set
            {
                testingWillingnessComboBox.Visible = value;
            }
        }

        public void SetLabels(int id, string mutProb, string name, string relationship)
        {
            relId = id;

            //old v2 way:
            //testingWillingnessComboBox.Text =
            //    Patient.getRelativeValueFromDB("tblRiskDataRelatives", "testingWillingness", relId);

            RelativeValueHolder relValHolder = new RelativeValueHolder("");

            ParameterCollection pc = new ParameterCollection("unitnum", SessionManager.Instance.GetActivePatient().unitnum);
            pc.Add("relativeid", relId);
            pc.Add("table", "tblRiskDataRelatives");
            pc.Add("column", "testingWillingness");
            relValHolder.DoLoadWithSpAndParams("sp_3_LoadRelativeValue", pc);
            testingWillingnessComboBox.Text = relValHolder.relativeValue;

            if (string.IsNullOrEmpty(mutProb) == false)
            {
                try
                {
                    brcapro = (int)Math.Round(double.Parse(mutProb));
                    if (brcapro > 0)
                    {
                        BrcaLabel.Text = brcapro.ToString() + "%";
                    }
                    else
                    {
                        BrcaLabel.Text = "";
                    }
                }
                catch { }
            }
            else
            {
                BrcaLabel.Text = "";
            }
            // You might well not know the mother's name (e.g.)
            if (!String.IsNullOrEmpty(name))
            {
                if (name.Contains(" "))
                {
                    string[] tokens = name.Split(' ');
                    if (name.Contains(','))
                        name = tokens[1].Replace(",","");
                    else
                        name = tokens[0];
                }
                //NameLabel.Text = name;
                RelationshipLabel.Text = name;
            }
            else
            {
                RelationshipLabel.Text = relationship;
            }
            RelationshipLabel.Text = RelationshipLabel.Text.Replace(" ", Environment.NewLine);
            int delta = this.Height - RelationshipLabel.Height;
            RelationshipLabel.Location = new Point(RelationshipLabel.Location.X, delta / 2);
        }

        protected virtual void OnEventName(RelativeRowClickEventArgs e)
        {
            SessionManager.Instance.SetActiveRelative(parentView, SessionManager.Instance.GetActivePatient().FHx.getRelative(e.id));
        }

        private void testingWillingnessComboBox_SelectedChangeCommitted(object sender, EventArgs e)
        {
            if (relId > 0)
            {
                ParameterCollection pc = new ParameterCollection("unitnum", SessionManager.Instance.GetActivePatient().unitnum);
                pc.Add("relativeid", relId);
                pc.Add("table", "tblRiskDataRelatives");
                pc.Add("column", "testingWillingness");
                pc.Add("relativeValue", (string)testingWillingnessComboBox.SelectedItem);

                RelativeValueHolder relValHolder = new RelativeValueHolder((string)testingWillingnessComboBox.SelectedItem);
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);

                relValHolder.DoPersistWithSpAndParams(args, "sp_3_Save_RelativeValue", ref pc);
            }
        }

        private void RelativeRow_MouseClick(object sender, MouseEventArgs e)
        {
            RelativeRowClickEventArgs e2 = new RelativeRowClickEventArgs();
            e2.id = relId;
            OnEventName(e2);
        }

        private void RelationshipLabel_Click(object sender, EventArgs e)
        {
            RelativeRowClickEventArgs e2 = new RelativeRowClickEventArgs();
            e2.id = relId;
            OnEventName(e2);
        }

        private void testingWillingnessComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            RelativeRowClickEventArgs e2 = new RelativeRowClickEventArgs();
            e2.id = relId;
            OnEventName(e2);
        }

        private void NameLabel_Click(object sender, EventArgs e)
        {
            RelativeRowClickEventArgs e2 = new RelativeRowClickEventArgs();
            e2.id = relId;
            OnEventName(e2);
        }

        private void BrcaLabel_Click(object sender, EventArgs e)
        {
            RelativeRowClickEventArgs e2 = new RelativeRowClickEventArgs();
            e2.id = relId;
            OnEventName(e2);
        }

        private void RelativeToConsiderRow_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.RelativeSelected += new RiskApps3.Controllers.SessionManager.RelativeSelectedEventHandler(RelativeSelected);

            if (SessionManager.Instance.GetSelectedRelative() == SessionManager.Instance.GetActivePatient().FHx.getRelative(relId))
                SetSelection();
            else UnSetSelection();
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
            SessionManager.Instance.RemoveHraView(this);
            base.Dispose(disposing);
        }

        /**************************************************************************************************/
        private void RelativeSelected(RelativeSelectedEventArgs e)
        {
            //if (e.sendingView != this)
                if (e.selectedRelative == SessionManager.Instance.GetActivePatient().FHx.getRelative(relId))
                {
                    SetSelection();
                }
                else
                {
                    UnSetSelection();
                }
        }

        private void SetSelection()
        {
            this.BackColor = Color.SteelBlue;
        }
        private void UnSetSelection()
        {
            this.BackColor = Color.Transparent;
        }
    }

    public class RelativeRowClickEventArgs : EventArgs
    {
        public int id;
    }

    public class RelativeValueHolder : HraObject
    {
        public string relativeValue;

        public RelativeValueHolder(string s)
        {
            relativeValue = s;
        }
    }
}
