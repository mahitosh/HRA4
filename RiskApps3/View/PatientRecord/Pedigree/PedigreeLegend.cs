using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeLegend : PedigreeComponent
    {
        private int radius = 8;
        private int legendPadding = 12;

        Font legendFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        public PedigreeLegend()
        {
            InitializeComponent();
        }
        public int LegendRadius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                foreach (Control c in flowLayoutPanel.Controls)
                {
                    PedigreeLegendRow plr = (PedigreeLegendRow)c;
                    plr.Radius = value;
                }
            }
        }
        public Font LegendFont
        {
            get
            {
                return legendFont;
            }
            set
            {
                legendFont = value;
                foreach (Control c in flowLayoutPanel.Controls)
                {
                    PedigreeLegendRow plr = (PedigreeLegendRow)c;
                    plr.LegendFont = value;
                }
            }
        }
        public Color Background
        {
            get
            {
                return BackColor;
            }
            set
            {
                flowLayoutPanel.BackColor = value;
                RefreshMe();
            }
        }
        /**************************************************************************************************/
        delegate void RefreshMeCallback();
        private void RefreshMe()
        {
            if (this.InvokeRequired)
            {
                RefreshMeCallback rmc = new RefreshMeCallback(RefreshMe);
                this.Invoke(rmc, null);
            }
            else
            {
                Refresh();
            }
        }
        /**************************************************************************************************/
        delegate void AddObservationsCallback(ClinicalObservationList dxList);
        public void AddObservations(ClinicalObservationList dxList)
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                AddObservationsCallback aoc = new AddObservationsCallback(AddObservations);
                object[] args = new object[1];
                args[0] = dxList;
                this.Invoke(aoc, args);
            }
            else
            {
                lock (dxList)
                {
                    foreach (ClincalObservation co in dxList)
                    {
                        bool add = true;
                        foreach (Control c in flowLayoutPanel.Controls)
                        {
                            PedigreeLegendRow plr = (PedigreeLegendRow)c;
                            string plrDisplayName = plr.GetObservationisplayName();
                            string plrShortName = plr.GetObservationisplayShortName();
                            if (string.Compare(co.ClinicalObservation_diseaseDisplayName, plrDisplayName, true) == 0 &&
                                string.Compare(co.ClinicalObservation_diseaseShortName, plrShortName) == 0)
                            {
                                add = false;
                                break;
                            }
                        }
                        if (add)
                        {
                            if (string.IsNullOrEmpty(co.disease) == false)
                            {
                                PedigreeLegendRow plr = new PedigreeLegendRow();
                                plr.SetObservation(co);
                                flowLayoutPanel.Controls.Add(plr);

                                this.Height = flowLayoutPanel.Location.Y + plr.Location.Y + plr.Height + legendPadding;
                                this.Refresh();
                            }
                        }
                    }
                }
            }
        }

        /**************************************************************************************************/
        delegate void CheckForEmptyCallback();
        public void CheckForEmpty()
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                CheckForEmptyCallback aoc = new CheckForEmptyCallback(CheckForEmpty);
                this.Invoke(aoc);
            }
            else
            {
                if (flowLayoutPanel.Controls.Count == 0)
                    this.Visible = false;
                else
                    this.Visible = true;
            }
        }
        /**************************************************************************************************/
        delegate void AddSingleObservationCallback(ClincalObservation dx, bool isNew);
        public void AddSingleObservation(ClincalObservation dx, bool isNew)
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                AddSingleObservationCallback aoc = new AddSingleObservationCallback(AddSingleObservation);
                object[] args = new object[1];
                args[0] = dx;
                args[1] = isNew;
                this.Invoke(aoc, args);
            }
            else
            {
                bool add = true;
                foreach (Control c in flowLayoutPanel.Controls)
                {
                    PedigreeLegendRow plr = (PedigreeLegendRow)c;
                    string plrDisplayName = plr.GetObservationisplayName();
                    string plrShortName = plr.GetObservationisplayShortName();
                    if (string.Compare(dx.ClinicalObservation_diseaseDisplayName, plrDisplayName, true) == 0 &&
                        string.Compare(dx.ClinicalObservation_diseaseShortName, plrShortName) == 0)
                    {
                        add = false;
                    }
                }
                if (add)
                {
                    if (string.IsNullOrEmpty(dx.disease) == false)
                    {
                        PedigreeLegendRow plr = new PedigreeLegendRow();
                        plr.SetObservation(dx);

                        flowLayoutPanel.Controls.Add(plr);

                        this.Height = flowLayoutPanel.Location.Y + plr.Location.Y + plr.Height + legendPadding;
                        if (isNew)
                        {
                            if (flowLayoutPanel.Controls.Count == 1)
                            {
                                this.Location = new Point(SessionManager.Instance.MetaData.CurrentUserDefaultPedigreePrefs.GUIPreference_LegendX, 
                                                          SessionManager.Instance.MetaData.CurrentUserDefaultPedigreePrefs.GUIPreference_LegendY);
                            }
                            int maxWidth = 0;
                            foreach (Control c in flowLayoutPanel.Controls)
                            {
                                PedigreeLegendRow row = (PedigreeLegendRow)c;
                                if (row.Width > maxWidth)
                                    maxWidth = row.Width;
                            }
                            if (maxWidth + legendPadding > this.Width)
                                this.Width = maxWidth + legendPadding;
                        }
                        this.Refresh();
                    }
                }
                CheckForEmpty();
            }
        }

        public void ClearObservations()
        {
            flowLayoutPanel.Controls.Clear();
            this.Visible = false;
        }
        internal delegate void RemoveObservationCallback(ClincalObservation co);
        internal void RemoveObservation(ClincalObservation co)
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                RemoveObservationCallback aoc = new RemoveObservationCallback(RemoveObservation);
                object[] args = new object[1];
                args[0] = co;
                this.Invoke(aoc, args);
            }
            else
            {
                Control toBeRemoved = null;
                bool noDups = true;
                int maxWidth = 0;
                int totalHeight = 0;
                foreach (Control c in flowLayoutPanel.Controls)
                {
                    PedigreeLegendRow plr = (PedigreeLegendRow)c;
                    string plrDisplayName = plr.GetObservationisplayName();
                    string plrShortName = plr.GetObservationisplayShortName();
                    if (string.Compare(co.ClinicalObservation_diseaseDisplayName, plrDisplayName, true) == 0 &&
                        string.Compare(co.ClinicalObservation_diseaseShortName, plrShortName) == 0)
                    {
                        if (noDups)
                        {
                            toBeRemoved = c;
                        }
                        else
                        {
                            noDups = false;
                        }
                    }
                    else
                    {
                        if (plr.Width > maxWidth)
                            maxWidth = plr.Width;
                        totalHeight += plr.Height + legendPadding;
                    }
                }
                if (toBeRemoved != null && noDups)
                {
                    flowLayoutPanel.Controls.Remove(toBeRemoved);
                    this.Height -= toBeRemoved.Height + legendPadding;
                    if (maxWidth > 0 && this.Width > maxWidth + legendPadding)
                    {
                        this.Width = maxWidth + legendPadding;
                        this.Height = totalHeight + legendPadding;
                    }
                }
                this.Refresh();
                CheckForEmpty();
            }
        }

        internal void DrawToBitmapWithBorder(Bitmap image, Rectangle rectangle, float scalingFactor = 1)
        {
            if (rectangle.X < 0)
                rectangle.X = 0;
            if (rectangle.Y < 0)
                rectangle.Y = 0;

            //rectangle.Size = new Size(image.Width - (EdgeMargin * 2), rectangle.Size.Height / 2);
            this.Size = rectangle.Size;
            this.flowLayoutPanel.Size = rectangle.Size;
            this.legendTitleLabel.Size = this.legendTitleLabel.Size.Scale(scalingFactor);
            this.legendTitleLabel.Font = this.legendTitleLabel.Font.Scale(scalingFactor);
            this.flowLayoutPanel.Controls.OfType<PedigreeLegendRow>().ToList().ForEach(row =>row.ScaleByFactor(scalingFactor));
            this.Refresh();

            Graphics g = Graphics.FromImage(image);
            DrawToBitmap(image, rectangle);
            g.DrawRectangle(Pens.Black, rectangle);
        }
    }
    public class PedigreeFlowLayoutPanel : FlowLayoutPanel
    {
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);
            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        } 
    }
}

