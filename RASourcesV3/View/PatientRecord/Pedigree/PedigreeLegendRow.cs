using System;
using System.Drawing;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeLegendRow : UserControl
    {

        private int radius = 8;
        private Font legendFont = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
        private Rectangle glyphBoundingBox;
        private Brush b = Brushes.White;

        Point RightTextOrigin;
        public Pen p = Pens.Black;

        string disease;
        string diseaseDisplayName;
        string diseaseIconArea;
        string diseaseIconColor;
        string diseaseIconType;
        string diseaseShortName;

        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                if (radius != value)
                {
                    radius = value;
                    SetRadiusAndTextOrigin();
                    MeasureText();
                    Refresh();
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
                MeasureText();
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
        public PedigreeLegendRow()
        {
            InitializeComponent();

        }

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

        public void SetObservation(ClincalObservation newCo)
        {
            disease = (newCo.ClinicalObservation_diseaseShortName + " - " + newCo.ClinicalObservation_diseaseDisplayName).Trim(new char[] { ' ', '-' });
            diseaseDisplayName = newCo.ClinicalObservation_diseaseDisplayName;
            diseaseIconArea = newCo.ClinicalObservation_diseaseIconArea;
            diseaseIconColor = newCo.ClinicalObservation_diseaseIconColor;
            diseaseIconType = newCo.ClinicalObservation_diseaseIconType;
            diseaseShortName = newCo.ClinicalObservation_diseaseShortName;

            if (string.IsNullOrEmpty(diseaseIconColor) == false) //(b != null)
                b = new SolidBrush(Color.FromName(diseaseIconColor));


            SetRadiusAndTextOrigin();
            MeasureText();
        }
        private void SetRadiusAndTextOrigin()
        {
            glyphBoundingBox = new Rectangle(5, 5, 2 * radius, 2 * radius);

            Size sz = TextRenderer.MeasureText(disease, legendFont);
            RightTextOrigin = new Point(glyphBoundingBox.X + glyphBoundingBox.Width + 6, radius - (sz.Height / 3));
        }
        private void MeasureText()
        {
            Size sz = TextRenderer.MeasureText(disease, legendFont);
            this.Width = RightTextOrigin.X + sz.Width;
            this.Height = 10 + (2 * radius);
            if (RightTextOrigin.Y + sz.Height > Height)
                Height = RightTextOrigin.Y + sz.Height;
        }

        public string GetObservationisplayName()
        {
            return diseaseDisplayName;
        }
        public string GetObservationisplayShortName()
        {
            return diseaseShortName;
        }

        private void PedigreeLegendRow_Paint(object sender, PaintEventArgs e)
        {
            //the fact that all these things are drawn after the fact *i think* 
            //  prevents the built-in Scale method from working correctly
            if (diseaseIconColor == null)
            {
                b = Brushes.Gray;
                diseaseIconType = "Fill";
            }
                
            if (diseaseIconType == "Fill")
            {
                switch (diseaseIconArea)
                {
                    case "All":
                        e.Graphics.FillEllipse(b, glyphBoundingBox);                     
                        break;
                    case "UL":
                        e.Graphics.FillPie(b, glyphBoundingBox, 180, 90);
                        break;
                    case "UR":
                        e.Graphics.FillPie(b, glyphBoundingBox, 270, 90);                        
                        break;
                    case "LL":
                        e.Graphics.FillPie(b, glyphBoundingBox, 90, 90);                        
                        break;
                    case "LR":
                        e.Graphics.FillPie(b, glyphBoundingBox, 0, 90);                        
                        break;
                    default:
                        break;
                }
            }
            if (diseaseIconType == "Dot")
            {
                Rectangle fillArea = glyphBoundingBox;
                fillArea.Width = glyphBoundingBox.Width / 2;
                fillArea.Height = glyphBoundingBox.Height / 2;
                switch (diseaseIconArea)
                {
                    case "All":
                        break;
                    case "UL":
                        break;
                    case "UR":
                        fillArea.X += glyphBoundingBox.Width / 2;
                        break;
                    case "LL":
                        fillArea.Y += glyphBoundingBox.Height / 2;
                        break;
                    case "LR":
                        fillArea.X += glyphBoundingBox.Width / 2;
                        fillArea.Y += glyphBoundingBox.Height / 2;
                        break;
                    default:
                        break;
                }
                e.Graphics.FillEllipse(b, fillArea);
            }
            
            e.Graphics.DrawString(disease, legendFont, Brushes.Black, RightTextOrigin);
            e.Graphics.DrawEllipse(p, glyphBoundingBox);

        }

        internal void ScaleByFactor(float scalingFactor)
        {
            this.Size = this.Size.Scale(scalingFactor);
            this.LegendFont = this.LegendFont.Scale(scalingFactor);
            this.Radius = (int) (this.Radius * scalingFactor);
            this.MeasureText();
        }
    }
}

