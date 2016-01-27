using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Controllers;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using RiskApps3.Model.MetaData;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeSettingsForm : Form
    {
        public delegate void ChangedFrameRateHandler(int newRate);
        public event ChangedFrameRateHandler FrameRateChanged;

        public GUIPreference preferences;

        Pen pedigreeSymbolPen = new Pen(Color.Black, 3);
     
        bool dragging = false;
        Point initialControl;
        Point initialDrag;
        Point PedigreeOrigin = new Point(150, 140);

        public int FrameRate
        {
            get
            {
                return FrameRateTrackBar.Value;
            }
            set
            {
                FrameRateTrackBar.Value = value;
            }
        }


        public List<AnnotationContainer> annotation_areas = new List<AnnotationContainer>();

        //Annotation brcaScoreAnnotation = new Annotation("Brca Score");
        //Annotation mmrScoreAnnotation = new Annotation("MMR Score");

        AnnotationContainer theHoldingPen;
        AnnotationContainer lowerList;
        AnnotationContainer middleLeftUpperList;
        AnnotationContainer middleLeftLowerList;
        AnnotationContainer middleRightUpperList;
        AnnotationContainer middleRightLowerList;
        AnnotationContainer upperLeftList;
        AnnotationContainer upperRightList;
        AnnotationContainer upperCenterList;

        public PedigreeSettingsForm(PedigreeAnnotationList annotations)
        {
            InitializeComponent();

            /////////////
            theHoldingPen = new AnnotationContainer(new Point(5, 20), new Size(110, 340), "theHoldingPen");
            theHoldingPen.active = false;
            theHoldingPen.justification = (int)AnnotationContainer.Alignements.LEFT_JUSTIFIED;
            annotation_areas.Add(theHoldingPen);

            /////////////
           // lowerList = new AnnotationContainer(new Point(172, 128), new Size(175, 123), "lowerList");
            lowerList = new AnnotationContainer(new Point(172, 154), new Size(175, 123), "lowerList");
            lowerList.justification = (int)AnnotationContainer.Alignements.CENTER_JUSTIFIED;
            lowerList.Position = (int)AnnotationContainer.IconRelation.LOWER_CENTER;
            annotation_areas.Add(lowerList);

            /////////////
           // middleLeftUpperList = new AnnotationContainer(new Point(126, 67), new Size(100, 24), "middleLeftUpperList");
            middleLeftUpperList = new AnnotationContainer(new Point(126, 93), new Size(100, 24), "middleLeftUpperList");
            middleLeftUpperList.Position = (int)AnnotationContainer.IconRelation.MID_LEFT_UPPER;
            middleLeftUpperList.justification = (int)AnnotationContainer.Alignements.RIGHT_JUSTIFIED;
            annotation_areas.Add(middleLeftUpperList);

            /////////////
            //middleLeftLowerList = new AnnotationContainer(new Point(126, 99), new Size(100, 24), "middleLeftLowerList");
            middleLeftLowerList = new AnnotationContainer(new Point(126, 125), new Size(100, 24), "middleLeftLowerList");
            middleLeftLowerList.Position = (int)AnnotationContainer.IconRelation.MID_LEFT_LOWER;
            middleLeftLowerList.justification = (int)AnnotationContainer.Alignements.RIGHT_JUSTIFIED;
            annotation_areas.Add(middleLeftLowerList);

            /////////////
           // middleRightUpperList = new AnnotationContainer(new Point(294, 67), new Size(100, 24), "middleRightUpperList");
            middleRightUpperList = new AnnotationContainer(new Point(294, 93), new Size(100, 24), "middleRightUpperList");
            middleRightUpperList.Position = (int)AnnotationContainer.IconRelation.MID_RIGHT_UPPER;
            middleRightUpperList.justification = (int)AnnotationContainer.Alignements.LEFT_JUSTIFIED;
            annotation_areas.Add(middleRightUpperList);

            /////////////            
            //middleRightLowerList = new AnnotationContainer(new Point(294, 99), new Size(100, 24), "middleRightLowerList");
            middleRightLowerList = new AnnotationContainer(new Point(294, 125), new Size(100, 24), "middleRightLowerList");
            middleRightLowerList.Position = (int)AnnotationContainer.IconRelation.MID_RIGHT_LOWER;
            middleRightLowerList.justification = (int)AnnotationContainer.Alignements.LEFT_JUSTIFIED;
            annotation_areas.Add(middleRightLowerList);

            /////////////
            //upperLeftList = new AnnotationContainer(new Point(152, 39), new Size(100, 24), "upperLeftList");
            upperLeftList = new AnnotationContainer(new Point(152, 65), new Size(100, 24), "upperLeftList");
            upperLeftList.Position = (int)AnnotationContainer.IconRelation.UPPER_LEFT;
            upperLeftList.justification = (int)AnnotationContainer.Alignements.RIGHT_JUSTIFIED;
            annotation_areas.Add(upperLeftList);

            /////////////
            //upperRightList = new AnnotationContainer(new Point(270, 39), new Size(100, 24), "upperRightList");
            upperRightList = new AnnotationContainer(new Point(270, 65), new Size(100, 24), "upperRightList");
            upperRightList.Position = (int)AnnotationContainer.IconRelation.UPPER_RIGHT;
            upperRightList.justification = (int)AnnotationContainer.Alignements.LEFT_JUSTIFIED;
            annotation_areas.Add(upperRightList);

            /////////////////////////////////////////////////////
           // upperCenterList = new AnnotationContainer(new Point(215, 5), new Size(100, 24), "upperCenterList");
            upperCenterList = new AnnotationContainer(new Point(215, 5), new Size(100, 50), "upperCenterList");
            upperCenterList.Position = (int)AnnotationContainer.IconRelation.CENTER_TOP;
            upperCenterList.justification = (int)AnnotationContainer.Alignements.CENTER_JUSTIFIED;
            annotation_areas.Add(upperCenterList);


            if (annotations.IsLoaded == false)
                annotations.BackgroundListLoad();
            

            foreach (PedigreeAnnotation pa in annotations)
            {
                Annotation a = new Annotation(pa);

                a.MouseMove += new System.Windows.Forms.MouseEventHandler(this.annotation_MouseMove);
                a.MouseDown += new System.Windows.Forms.MouseEventHandler(this.annotation_MouseDown);
                a.MouseUp += new System.Windows.Forms.MouseEventHandler(this.annotation_MouseUp);
                a.MouseEnter += new System.EventHandler(this.annotation_MouseEnter);
                a.MouseLeave += new System.EventHandler(this.annotation_MouseLeave);
                tabPage1.Controls.Add(a);
                a.BringToFront();

                if (a.hraAnnotation != null)
                {
                    foreach (AnnotationContainer ac in annotation_areas)
                    {
                        if (ac.area == a.hraAnnotation.area)
                        {
                            ac.AddAnnotation(a);
                            break;
                        }
                    }
                }
            }

            //foreach (DiseaseObject o in SessionManager.Instance.MetaData.Diseases)
            //{
            //    PedigreeSymbolRow psr = new PedigreeSymbolRow(o);
            //    symbolsPanel.Controls.Add(psr);
            //}
        }

        private string ParseBorderStyle(BorderStyle style)
        {
            switch (style)
            {
                case BorderStyle.Fixed3D:
                    return "3D";
                case BorderStyle.FixedSingle:
                    return "Single";
                case BorderStyle.None:
                    return "None";
                default:
                    return "None";
            }
        }

        public void FillControls()
        {
            if (this.preferences != null)
            {
                int index = 0;

                this.nameWidthSlider.Value = preferences.GUIPreference_nameWidth;
                this.nationalityCheck.Checked = preferences.GUIPreference_limitedNationality;
                this.ethnicityCheck.Checked = preferences.GUIPreference_limitedEthnicity;
                this.numRelativesCheck.Checked = preferences.GUIPreference_ShowRelIds;
                
                this.Title.Checked = preferences.GUIPreference_ShowTitle;
                this.nameCheck.Checked = preferences.GUIPreference_ShowName;
                this.unitnumCheck.Checked = preferences.GUIPreference_ShowUnitnum;
                this.dobCheck.Checked = preferences.GUIPreference_ShowDob;
                this.spacingSlider.Value = preferences.GUIPreference_TitleSpacing;
                index = this.titleBorderDrop.FindStringExact(ParseBorderStyle(preferences.GUIPreference_TitleBorder));
                this.titleBorderDrop.SelectedIndex = index;

                this.Legend.Checked = preferences.GUIPreference_ShowLegend;
                this.legendSlider.Value = preferences.GUIPreference_LegendRadius;
                index = this.legendBorderDrop.FindStringExact(ParseBorderStyle(preferences.GUIPreference_LegendBorder));
                this.legendBorderDrop.SelectedIndex = index;

                this.CommentCheck.Checked = preferences.GUIPreference_ShowComment;
                index = this.commentsBorderDrop.FindStringExact(ParseBorderStyle(preferences.GUIPreference_CommentBorder));
                this.commentsBorderDrop.SelectedIndex = index;

                this.FoundBox.Text = preferences.GUIPreference_VariantFoundText;
                this.FoundVusBox.Text = preferences.GUIPreference_VariantFoundVusText;
                this.NotFoundBox.Text = preferences.GUIPreference_VariantNotFoundText;
                this.NotTestedBox.Text = preferences.GUIPreference_VariantNotTestedText;
                this.UnknownBox.Text = preferences.GUIPreference_VariantUnknownText;
                this.HeteroBox.Text = preferences.GUIPreference_VariantHeteroText;
            }
        }

        public void showBrcaScores()
        {
            foreach (AnnotationContainerSlot acs in upperRightList.slots)
            {
                if (acs.resident != null)
                {
                    if (acs.resident.label != "Brca Score")
                    {
                        theHoldingPen.AddAnnotation(acs.resident);
                        upperRightList.RemoveAnnotation(acs.resident);
                    }
                }
            }
            Annotation a = null;
            foreach (AnnotationContainer ac in annotation_areas)
            {
                bool found = false;
                foreach (AnnotationContainerSlot acs in ac.slots)
                {
                    if (acs.resident != null)
                        if (acs.resident.label == "Brca Score")
                        {
                            a = acs.resident;
                            found = true;
                            break;
                        }
                }
                if (found)
                    break;
            }
            if (a != null)
            {
                foreach (AnnotationContainer ac in annotation_areas)
                {
                    if (ac.area == "upperRightList")
                    {
                        if (a.StartingContainer != ac)
                        {
                            ac.AddAnnotation(a);
                            a.hraAnnotation.area = ac.area;
                        }
                    }
                    else
                    {
                        ac.RemoveAnnotation(a);
                    }
                }
            }

        }

        public void hideBrcaScores()
        {

            Annotation a = null;
            foreach (AnnotationContainer ac in annotation_areas)
            {
                bool found = false;
                foreach (AnnotationContainerSlot acs in ac.slots)
                {
                    if (acs.resident != null)
                        if (acs.resident.label == "Brca Score")
                        {
                            a = acs.resident;
                            found = true;
                            break;
                        }
                }
                if (found)
                    break;
            }
            if (a != null)
            {
                foreach (AnnotationContainer ac in annotation_areas)
                {
                        ac.RemoveAnnotation(a);
                }
            }

        }

        public void showMmrScores()
        {
            foreach (AnnotationContainerSlot acs in upperRightList.slots)
            {
                if (acs.resident != null)
                {
                    if (acs.resident.label != "MMR Score")
                    {
                        theHoldingPen.AddAnnotation(acs.resident);
                        upperRightList.RemoveAnnotation(acs.resident);
                    }
                }
            }
            Annotation a = null;
            foreach (AnnotationContainer ac in annotation_areas)
            {
                bool found = false;
                foreach (AnnotationContainerSlot acs in ac.slots)
                {
                    if (acs.resident != null)
                        if (acs.resident.label == "MMR Score")
                        {
                            a = acs.resident;
                            found = true;
                            break;
                        }
                }
                if (found)
                    break;
            }
            if (a != null)
            {
                foreach (AnnotationContainer ac in annotation_areas)
                {
                    if (ac.area == "upperRightList")
                    {
                        if (a.StartingContainer != ac)
                        {
                            ac.AddAnnotation(a);
                            a.hraAnnotation.area = ac.area;
                        }
                    }
                    else
                    {
                        ac.RemoveAnnotation(a);
                    }
                }
            }

        }
        public void hideMmrScores()
        {

            Annotation a = null;
            foreach (AnnotationContainer ac in annotation_areas)
            {
                bool found = false;
                foreach (AnnotationContainerSlot acs in ac.slots)
                {
                    if (acs.resident != null)
                        if (acs.resident.label == "MMR Score")
                        {
                            a = acs.resident;
                            found = true;
                            break;
                        }
                }
                if (found)
                    break;
            }
            if (a != null)
            {
                foreach (AnnotationContainer ac in annotation_areas)
                {
                    ac.RemoveAnnotation(a);
                }
            }

        }

        private void AddAnnotation(Annotation a, AnnotationContainer ac)
        {
            a.MouseMove += new System.Windows.Forms.MouseEventHandler(this.annotation_MouseMove);
            a.MouseDown += new System.Windows.Forms.MouseEventHandler(this.annotation_MouseDown);
            a.MouseUp += new System.Windows.Forms.MouseEventHandler(this.annotation_MouseUp);
            a.MouseEnter += new System.EventHandler(this.annotation_MouseEnter);
            a.MouseLeave += new System.EventHandler(this.annotation_MouseLeave);
            tabPage1.Controls.Add(a);
              a.BringToFront();

              ac.AddAnnotation(a);
        }

        private void PedigreeBuilderTemplateForm_Move(object sender, EventArgs e)
        {

        }

        private void annotation_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            initialControl = ((Annotation)sender).Location;
            initialDrag = ((Annotation)sender).PointToScreen(new Point(e.X, e.Y));
            

        }

        private void annotation_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Annotation a = ((Annotation)sender);
                Point current = ((Annotation)sender).PointToScreen(new Point(e.X, e.Y));
                int deltax = initialDrag.X - current.X;
                int deltay = initialDrag.Y - current.Y;
                ((Annotation)sender).Location = new Point(initialControl.X - deltax, initialControl.Y - deltay);
            }
        }
        public void SetPedigreeAnnotation(PedigreeAnnotation pa)
        {
            Annotation a = null;
            foreach (AnnotationContainer ac in annotation_areas)
            {
                bool found = false;
                foreach (AnnotationContainerSlot acs in ac.slots)
                {
                    if (acs.resident != null)
                    if (acs.resident.label == pa.annotation)
                    {
                        if (acs.resident.hraAnnotation != pa)
                        {
                            acs.resident.hraAnnotation = pa;
                            
                        }
                        
                        a = acs.resident;
                        break;
                    }
                }
                if (found)
                    break;
            }
            if (a != null)
            {
                foreach (AnnotationContainer ac in annotation_areas)
                {
                    if (ac.area == pa.area)
                    {
                        if (a.StartingContainer != ac)
                        {
                            ac.AddAnnotation(a);
                            a.hraAnnotation.area = ac.area;
                        }
                    }
                    else
                    {
                        ac.RemoveAnnotation(a);
                    }
                }
            }

        }
        private void annotation_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                dragging = false;
                
                bool foundNewHome = false;

                Annotation a = ((Annotation)sender);
                foreach (AnnotationContainer ac in annotation_areas)
                {
                    if (new Rectangle(ac.Location, ac.Size).Contains(new Point(a.Location.X + e.Location.X, a.Location.Y + e.Location.Y)))
                    {
                        if (a.StartingContainer != ac)
                        {
                            ac.AddAnnotation(a);
                            foundNewHome = true;
                            a.hraAnnotation.area = ac.area;
                            if (a.hraAnnotation.unitnum == "-1")
                            {
                                a.hraAnnotation.unitnum = SessionManager.Instance.GetActivePatient().unitnum;
                            }
                            a.hraAnnotation.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(null));
                        }
                        else
                        {
                            ac.ResetAnnotationPosition(a);
                        }
                    }
                    else
                    {
                        ac.RemoveAnnotation(a);
                    }
                }
                if (foundNewHome == false)
                {
                    a.Location = initialControl;
                }
                else
                {
                    //if (UpdateAnnotationAreasCallback != null)
                    //{
                    //    UpdateAnnotationAreasCallback.Invoke();
                    //}
                }
            }
        }
        

        private void annotation_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void annotation_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            int yoffset = 26;
            //pedigree symbol
            e.Graphics.DrawRectangle(pedigreeSymbolPen, new Rectangle(235, 70 + yoffset, 50, 50));
            e.Graphics.DrawLine(pedigreeSymbolPen, 260, 33 + yoffset, 260, 70 + yoffset);
            e.Graphics.DrawLine(pedigreeSymbolPen, 260, 33 + yoffset, 300, 33 + yoffset);

            e.Graphics.DrawLine(pedigreeSymbolPen, 215, 95 + yoffset, 235, 95 + yoffset);
            e.Graphics.DrawLine(pedigreeSymbolPen, 285, 95 + yoffset, 305, 95 + yoffset);

            // holding pen seperator
            e.Graphics.DrawLine(Pens.Black, 115, 20 + yoffset, 115, 235 + yoffset);

            foreach (AnnotationContainer ac in annotation_areas)
            {
                if (ac.active)
                    ac.Draw(e.Graphics);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_ShowRelIds = numRelativesCheck.Checked;
            }
        }


        private void FrameRateTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (FrameRateChanged != null)
                FrameRateChanged(FrameRateTrackBar.Value);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_ShowName = nameCheck.Checked;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_ShowUnitnum = unitnumCheck.Checked;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_ShowDob = dobCheck.Checked;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_ShowLegend = Legend.Checked;
            }
        }

        private void Title_CheckedChanged(object sender, EventArgs e)
        {
            if (Title.Checked)
            {
                nameCheck.Enabled = true;
                unitnumCheck.Enabled = true;
                dobCheck.Enabled = true;
            }
            else
            {
                nameCheck.Enabled = false;
                unitnumCheck.Enabled = false;
                dobCheck.Enabled = false;
            }
            if (preferences != null)
            {
                preferences.GUIPreference_ShowTitle = Title.Checked;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                fontDialog1.Font = preferences.GUIPreference_NameFont;
                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    preferences.GUIPreference_NameFont = fontDialog1.Font;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                fontDialog1.Font = preferences.GUIPreference_UnitnumFont;
                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    preferences.GUIPreference_UnitnumFont = fontDialog1.Font;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                fontDialog1.Font = preferences.GUIPreference_DobFont;
                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    preferences.GUIPreference_DobFont = fontDialog1.Font;
                }
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_ShowComment = CommentCheck.Checked;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
            if (preferences != null)
            {
                preferences.GUIPreference_TitleSpacing = spacingSlider.Value;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                colorDialog1.Color = preferences.GUIPreference_TitleBackground;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    preferences.GUIPreference_TitleBackground = colorDialog1.Color;
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                if (titleBorderDrop.Text == "None")
                    preferences.GUIPreference_TitleBorder = BorderStyle.None;
                if (titleBorderDrop.Text == "Single")
                    preferences.GUIPreference_TitleBorder = BorderStyle.FixedSingle;
                if (titleBorderDrop.Text == "3D")
                    preferences.GUIPreference_TitleBorder = BorderStyle.Fixed3D;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                colorDialog1.Color = preferences.GUIPreference_LegendBackground;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    preferences.GUIPreference_LegendBackground = colorDialog1.Color;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                colorDialog1.Color = preferences.GUIPreference_CommentBackground;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    preferences.GUIPreference_CommentBackground = colorDialog1.Color;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                if (legendBorderDrop.Text == "None")
                    preferences.GUIPreference_LegendBorder = BorderStyle.None;
                if (legendBorderDrop.Text == "Single")
                    preferences.GUIPreference_LegendBorder = BorderStyle.FixedSingle;
                if (legendBorderDrop.Text == "3D")
                    preferences.GUIPreference_LegendBorder = BorderStyle.Fixed3D;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                if (commentsBorderDrop.Text == "None")
                    preferences.GUIPreference_CommentBorder = BorderStyle.None;
                if (commentsBorderDrop.Text == "Single")
                    preferences.GUIPreference_CommentBorder = BorderStyle.FixedSingle;
                if (commentsBorderDrop.Text == "3D")
                    preferences.GUIPreference_CommentBorder = BorderStyle.Fixed3D;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_LegendRadius = legendSlider.Value;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                fontDialog1.Font = preferences.GUIPreference_LegendFont;
                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    preferences.GUIPreference_LegendFont = fontDialog1.Font;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                fontDialog1.Font = preferences.GUIPreference_CommentFont;
                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    preferences.GUIPreference_CommentFont = fontDialog1.Font;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                colorDialog1.Color = preferences.GUIPreference_PedigreeBackground;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    preferences.GUIPreference_PedigreeBackground = colorDialog1.Color;
                }
            }
        }


        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_nameWidth = nameWidthSlider.Value;
            }
        }

        private void FrameRateTrackBar_Scroll(object sender, EventArgs e)
        {
            //if (preferences != null)
            //{
            //    preferences.GUIPreference_nameWidth = trackBar3.Value;
            //}
        }

        private void trackBar1_Validated(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_limitedEthnicity = ethnicityCheck.Checked;
            }
        }

        private void checkBox6_CheckedChanged_1(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_limitedNationality = nationalityCheck.Checked;
            }
        }

        #region save / load buttons

        private void SaveAsSystem_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.ConsumeSettings(preferences);
            DialogResult result = MessageBox
                .Show("System Default Set","",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);
        }

        private void SaveAsUser_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.MetaData.CurrentUserDefaultPedigreePrefs.ConsumeSettings(preferences);
        }

        private void ApplySystemDefaultToAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox
                .Show(
                    "This action will overwrite all Pedigree GUI Preferences in the riskApps database.  Are you sure this is what you want to do?", 
                    "Irreversible Action", 
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));

                SqlCommand cmdProcedure = null;
                try
                {

                    using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                    {
                        connection.Open();
                        cmdProcedure = new SqlCommand("sp_3_UpdateLkpGuiPrefsEnMasse", connection);
                        cmdProcedure.CommandType = CommandType.StoredProcedure;
                        cmdProcedure.CommandTimeout = 300; //change command timeout from default to 5 minutes

                        //SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.ConsumeSettings(this.preferences);

                        //GUIPreference p = SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs;

                        cmdProcedure.Parameters.Add("@modifiedDate", SqlDbType.DateTime);
                        cmdProcedure.Parameters["@modifiedDate"].Value = DateTime.Now; 
                        cmdProcedure.Parameters.Add("@pedigreeZoomValue", SqlDbType.Int);
                        cmdProcedure.Parameters["@pedigreeZoomValue"].Value = preferences.GUIPreference_zoomValue; 
                        cmdProcedure.Parameters.Add("@pedigreeVerticalSpacing", SqlDbType.Int);
                        cmdProcedure.Parameters["@pedigreeVerticalSpacing"].Value = preferences.GUIPreference_verticalSpacing; 
                        cmdProcedure.Parameters.Add("@modifiedBy", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@modifiedBy"].Value = SessionManager.Instance.ActiveUser.userLogin; 
                        cmdProcedure.Parameters.Add("@ShowRelIds", SqlDbType.Bit);
                        cmdProcedure.Parameters["@ShowRelIds"].Value = preferences.GUIPreference_ShowRelIds; 
                        cmdProcedure.Parameters.Add("@PedigreeBackground", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@PedigreeBackground"].Value = System.Drawing.ColorTranslator.ToHtml(preferences.GUIPreference_PedigreeBackground); 
                        cmdProcedure.Parameters.Add("@nameWidth", SqlDbType.Int);
                        cmdProcedure.Parameters["@nameWidth"].Value = preferences.GUIPreference_nameWidth; 
                        cmdProcedure.Parameters.Add("@limitedEthnicity", SqlDbType.Bit);
                        cmdProcedure.Parameters["@limitedEthnicity"].Value = preferences.GUIPreference_limitedEthnicity; 
                        cmdProcedure.Parameters.Add("@limitedNationality", SqlDbType.Bit);
                        cmdProcedure.Parameters["@limitedNationality"].Value = preferences.GUIPreference_limitedNationality; 
                        cmdProcedure.Parameters.Add("@ShowTitle", SqlDbType.Bit);
                        cmdProcedure.Parameters["@ShowTitle"].Value = preferences.GUIPreference_ShowTitle; 
                        cmdProcedure.Parameters.Add("@ShowName", SqlDbType.Bit);
                        cmdProcedure.Parameters["@ShowName"].Value = preferences.GUIPreference_ShowName; 
                        cmdProcedure.Parameters.Add("@NameFont", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@NameFont"].Value = converter.ConvertToString(preferences.GUIPreference_NameFont); 
                        cmdProcedure.Parameters.Add("@ShowUnitnum", SqlDbType.Bit);
                        cmdProcedure.Parameters["@ShowUnitnum"].Value = preferences.GUIPreference_ShowUnitnum; 
                        cmdProcedure.Parameters.Add("@UnitnumFont", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@UnitnumFont"].Value = converter.ConvertToString(preferences.GUIPreference_UnitnumFont); 
                        cmdProcedure.Parameters.Add("@ShowDob", SqlDbType.Bit);
                        cmdProcedure.Parameters["@ShowDob"].Value = preferences.GUIPreference_ShowDob; 
                        cmdProcedure.Parameters.Add("@DobFont", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@DobFont"].Value = converter.ConvertToString(preferences.GUIPreference_DobFont); 
                        cmdProcedure.Parameters.Add("@TitleSpacing", SqlDbType.Int);
                        cmdProcedure.Parameters["@TitleSpacing"].Value = preferences.GUIPreference_TitleSpacing; 
                        cmdProcedure.Parameters.Add("@TitleBackground", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@TitleBackground"].Value = System.Drawing.ColorTranslator.ToHtml(preferences.GUIPreference_TitleBackground); 
                        cmdProcedure.Parameters.Add("@TitleBorder", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@TitleBorder"].Value = this.ParseBorderStyle(preferences.GUIPreference_TitleBorder); 
                        cmdProcedure.Parameters.Add("@ShowLegend", SqlDbType.Bit);
                        cmdProcedure.Parameters["@ShowLegend"].Value = preferences.GUIPreference_ShowLegend; 
                        cmdProcedure.Parameters.Add("@LegendBackground", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@LegendBackground"].Value = System.Drawing.ColorTranslator.ToHtml(preferences.GUIPreference_LegendBackground); 
                        cmdProcedure.Parameters.Add("@LegendBorder", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@LegendBorder"].Value = this.ParseBorderStyle(preferences.GUIPreference_LegendBorder); 
                        cmdProcedure.Parameters.Add("@LegendRadius", SqlDbType.Int);
                        cmdProcedure.Parameters["@LegendRadius"].Value = preferences.GUIPreference_LegendRadius; 
                        cmdProcedure.Parameters.Add("@LegendFont", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@LegendFont"].Value = converter.ConvertToString(preferences.GUIPreference_LegendFont); 
                        cmdProcedure.Parameters.Add("@ShowComment", SqlDbType.Bit);
                        cmdProcedure.Parameters["@ShowComment"].Value = preferences.GUIPreference_ShowComment; 
                        cmdProcedure.Parameters.Add("@CommentBackground", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@CommentBackground"].Value = System.Drawing.ColorTranslator.ToHtml(preferences.GUIPreference_CommentBackground); 
                        cmdProcedure.Parameters.Add("@CommentBorder", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@CommentBorder"].Value = this.ParseBorderStyle(preferences.GUIPreference_CommentBorder); 
                        cmdProcedure.Parameters.Add("@CommentFont", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@CommentFont"].Value = converter.ConvertToString(preferences.GUIPreference_CommentFont); 
                        cmdProcedure.Parameters.Add("@LegendX", SqlDbType.Int);
                        cmdProcedure.Parameters["@LegendX"].Value = preferences.GUIPreference_LegendX; 
                        cmdProcedure.Parameters.Add("@LegendY", SqlDbType.Int);
                        cmdProcedure.Parameters["@LegendY"].Value = preferences.GUIPreference_LegendY; 
                        cmdProcedure.Parameters.Add("@LegendHeight", SqlDbType.Int);
                        cmdProcedure.Parameters["@LegendHeight"].Value = preferences.GUIPreference_LegendHeight; 
                        cmdProcedure.Parameters.Add("@LegendWidth", SqlDbType.Int);
                        cmdProcedure.Parameters["@LegendWidth"].Value = preferences.GUIPreference_LegendWidth; 
                        cmdProcedure.Parameters.Add("@TitleX", SqlDbType.Int);
                        cmdProcedure.Parameters["@TitleX"].Value = preferences.GUIPreference_TitleX; 
                        cmdProcedure.Parameters.Add("@TitleY", SqlDbType.Int);
                        cmdProcedure.Parameters["@TitleY"].Value = preferences.GUIPreference_TitleY; 
                        cmdProcedure.Parameters.Add("@TitleHeight", SqlDbType.Int);
                        cmdProcedure.Parameters["@TitleHeight"].Value = preferences.GUIPreference_TitleHeight; 
                        cmdProcedure.Parameters.Add("@TitleWidth", SqlDbType.Int);
                        cmdProcedure.Parameters["@TitleWidth"].Value = preferences.GUIPreference_TitleWidth; 
                        cmdProcedure.Parameters.Add("@CommentX", SqlDbType.Int);
                        cmdProcedure.Parameters["@CommentX"].Value = preferences.GUIPreference_CommentX; 
                        cmdProcedure.Parameters.Add("@CommentY", SqlDbType.Int);
                        cmdProcedure.Parameters["@CommentY"].Value = preferences.GUIPreference_CommentY; 
                        cmdProcedure.Parameters.Add("@CommentHeight", SqlDbType.Int);
                        cmdProcedure.Parameters["@CommentHeight"].Value = preferences.GUIPreference_CommentHeight; 
                        cmdProcedure.Parameters.Add("@CommentWidth", SqlDbType.Int);
                        cmdProcedure.Parameters["@CommentWidth"].Value = preferences.GUIPreference_CommentWidth; 
                        cmdProcedure.Parameters.Add("@VariantFoundText", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@VariantFoundText"].Value = preferences.GUIPreference_VariantFoundText; 
                        cmdProcedure.Parameters.Add("@VariantFoundVusText", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@VariantFoundVusText"].Value = preferences.GUIPreference_VariantFoundVusText; 
                        cmdProcedure.Parameters.Add("@VariantNotFoundText", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@VariantNotFoundText"].Value = preferences.GUIPreference_VariantNotFoundText; 
                        cmdProcedure.Parameters.Add("@VariantUnknownText", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@VariantUnknownText"].Value = preferences.GUIPreference_VariantUnknownText; 
                        cmdProcedure.Parameters.Add("@VariantNotTestedText", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@VariantNotTestedText"].Value = preferences.GUIPreference_VariantNotTestedText; 
                        cmdProcedure.Parameters.Add("@VariantHeteroText", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@VariantHeteroText"].Value = preferences.GUIPreference_VariantHeteroText; 
                        cmdProcedure.Parameters.Add("@hideNonBloodRelatives", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@hideNonBloodRelatives"].Value = preferences.GUIPreference_hideNonBloodRelatives;



                        cmdProcedure.ExecuteNonQuery();
                    }


                    //bw 6/2/2014 - added annotation placements
                    foreach (PedigreeAnnotation pa in preferences.annotations)
                    {
                        ParameterCollection pc = new ParameterCollection();
                        pc.Add("unitnum", "-2");
                        pc.Add("area", pa.area);
                        pc.Add("slot", pa.slot);
                        pc.Add("annotation", pa.annotation); 
                        pc.Add("user", SessionManager.Instance.ActiveUser.userLogin);
                        BCDB2.Instance.RunSPWithParams("sp_3_Save_PedigreeAnnotations", pc);
                    }

                    MessageBox.Show(
                        "Current GUI Preferences applied to all patients in database.",
                        "Complete", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteToLog("[sp_3_UpdateLkpGuiPrefsEnMasse] Executing Stored Procedure - "
                            + cmdProcedure.ToString() + "; " + ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        private void LoadSystemDefault_Click(object sender, EventArgs e)
        {
            preferences.ConsumeSettings(SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs);
        }

        private void LoadUserDefault_Click(object sender, EventArgs e)
        {
            preferences.ConsumeSettings(SessionManager.Instance.MetaData.CurrentUserDefaultPedigreePrefs);
        }

        #endregion


        private void FoundBox_Validated(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_VariantFoundText = FoundBox.Text;
            }
        }

        private void NotFoundBox_Validated(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_VariantNotFoundText = NotFoundBox.Text;
            }
        }

        private void NotTestedBox_Validated(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_VariantNotTestedText = NotTestedBox.Text;
            }
        }

        private void UnknownBox_Validated(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_VariantUnknownText = UnknownBox.Text;
            }
        }

        private void HeteroBox_Validated(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_VariantHeteroText = HeteroBox.Text;
            }
        }

        private void FoundVusBox_Validated(object sender, EventArgs e)
        {
            if (preferences != null)
            {
                preferences.GUIPreference_VariantFoundVusText = FoundVusBox.Text;
            }
        }

        private void PedigreeSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ValidateChildren();
        }


    }

    public class AnnotationContainer
    {
        public enum Alignements { LEFT_JUSTIFIED = 0, RIGHT_JUSTIFIED, CENTER_JUSTIFIED};
        public enum IconRelation { UNDEFINED = 0, LOWER_CENTER, MID_LEFT_UPPER, MID_LEFT_LOWER, UPPER_LEFT, UPPER_RIGHT, MID_RIGHT_UPPER, MID_RIGHT_LOWER, CENTER_TOP };

        public int justification = (int)Alignements.CENTER_JUSTIFIED;
        public int Position = (int)IconRelation.UNDEFINED;

        public string area;
        public Point Location;
        public Size Size;

        Pen annotationContainerPen = new Pen(Color.Blue, 2);

        public List<AnnotationContainerSlot> slots = new List<AnnotationContainerSlot>();

        public bool active = true;

        public AnnotationContainer(Point loc, Size siz, string name)
        {
            annotationContainerPen.DashPattern = new float[] { 3, 2 };
            Location = loc;
            Size = siz;
            for (int i = 5; i < Size.Height; i = i + 24)
            {
                AnnotationContainerSlot h = new AnnotationContainerSlot();
                h.Location = new Point(5, i);
                h.resident = null;
                slots.Add(h);
            }
            area = name;
        }
        public void Draw(Graphics g)
        {
            g.DrawRectangle(annotationContainerPen, new Rectangle(Location, Size)); 
        }
        public void AddAnnotation(Annotation a)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].resident == null)
                {
                    slots[i].resident = a;
                    int deltaX = 0;
                    switch (justification)
                    {
                        case (int)Alignements.CENTER_JUSTIFIED:
                            deltaX = ((Size.Width - a.Width) / 2);
                            break;
                        case (int)Alignements.LEFT_JUSTIFIED:
                            deltaX = 0;
                            break;
                        case (int)Alignements.RIGHT_JUSTIFIED:
                            deltaX = (Size.Width - a.Width) - 5;
                            break;
                        default:
                            break;
                    }
                    a.Location = new Point(Location.X + slots[i].Location.X + deltaX, Location.Y + slots[i].Location.Y);
                    a.hraAnnotation.slot = i + 1;
                    a.StartingContainer = this;
                    break;
                }
            }
        }
        public void ResetAnnotationPosition(Annotation a)
        {
            foreach (AnnotationContainerSlot hs in slots)
            {
                if (hs.resident != null)
                {
                    if (hs.resident  == a)
                    {
                        a.Location = new Point(Location.X + hs.Location.X, Location.Y + hs.Location.Y);
                    }
                }
                //if (hs.occupied)
                //{
                //    if (hs.occupant == a.label)
                //    {
                //        a.Location = new Point(Location.X + hs.Location.X, Location.Y + hs.Location.Y);
                //    }
                //}
            }
        }

        internal void RemoveAnnotation(Annotation a)
        {
            foreach (AnnotationContainerSlot hs in slots)
            {
                if (hs.resident == a)
                {
                    a.StartingContainer = null;
                    hs.resident = null;
                }
                //if (hs.occupant == a.label)
                //{
                //    a.StartingContainer = null;
                //    hs.occupant = null;
                //    hs.occupied = false;
                //}
            }
        }
    }
    public class AnnotationContainerSlot
    {
        public Point Location;
        //public bool occupied;
        //public string occupant;
        public Annotation resident;
    }
}