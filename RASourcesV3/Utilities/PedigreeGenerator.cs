using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using dotnetCHARTING.WinForms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.View.PatientRecord.Pedigree;

namespace RiskApps3.Utilities
{
    public class PedigreeGenerator
    {
        PedigreeControl pedigreeControl1;
        PedigreeLegend pedigreeLegend1;
        PedigreeComment pedigreeComment1;
        PedigreeTitleBlock pedigreeTitleBlock1;
        PedigreeSettingsForm pedigreeSettingsForm1;
        PedigreeAnnotationList sysDefaultAnnotations;
        Patient proband;

        public int height = 500;
        public int width = 500;

        public bool showBrcaScores = false;
        public bool showMmrScores = false;

        public Dictionary<GeneticTestResult, List<Person>> FamilialVariants;

        GUIPreferenceList guiPreferenceList;
        GUIPreference currentPrefs;

        /// <summary>
        /// Used to statically create a pedigree for hra views.
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public PedigreeGenerator(int Width, int Height)
        {
            height = Height;
            width = Width;

            pedigreeControl1 = new PedigreeControl(false);
            pedigreeComment1 = new PedigreeComment();
            pedigreeTitleBlock1 = new PedigreeTitleBlock();
            pedigreeLegend1 = new PedigreeLegend(pedigreeTitleBlock1);
            sysDefaultAnnotations = SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.CopyAnnotations();
            pedigreeSettingsForm1 = new PedigreeSettingsForm(sysDefaultAnnotations);
        }

        /// <summary>
        /// Used to statically create a pedigree for html pages.
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="proband"></param>
        public PedigreeGenerator(int Width, int Height, Patient proband)
        {
            height = Height;
            width = Width;


            //pedigreeControl1 = new PedigreeControl();
            pedigreeControl1 = new PedigreeControl(false);
            pedigreeComment1 = new PedigreeComment();
            pedigreeTitleBlock1 = new PedigreeTitleBlock();
            pedigreeLegend1 = new PedigreeLegend(pedigreeTitleBlock1);
            sysDefaultAnnotations = new PedigreeAnnotationList("-1");
            sysDefaultAnnotations.BackgroundListLoad();
            pedigreeSettingsForm1 = new PedigreeSettingsForm(sysDefaultAnnotations);

            if (proband != null)
            {
                if (proband.guiPreferences.Count == 0)
                {
                    GUIPreference gp = new GUIPreference();
                    gp.BackgroundLoadWork();
                    proband.guiPreferences.Add(gp);
                }
            }

        }

        Bitmap image = null;

        public Bitmap GeneratePedigreeImage()
        {
            InitializePedigreeComponents();

            LoadActivePatientDataModel();

            GeneratePedigree();

            Close();

            return image;
        }
        public Bitmap GeneratePedigreeImage(Patient p)
        {
            InitializePedigreeComponents();

            LoadPatientDataModel(p);

            GeneratePedigree();

            Close();

            return image;
        }

        public void Close()
        {
            proband.ReleaseListeners(pedigreeControl1);
        }

        private void InitializePedigreeComponents()
        {
            this.pedigreeControl1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.pedigreeControl1.BackColor = Color.White;
            this.pedigreeControl1.FrameRate = 35;
            this.pedigreeControl1.Location = new Point(0, 0);
            this.pedigreeControl1.Name = "pedigreeControl1";
            this.pedigreeControl1.Size = new Size(width, height);



            this.pedigreeLegend1.BackColor = Color.White;
            this.pedigreeLegend1.Background = Color.White;
            this.pedigreeLegend1.BorderStyle = BorderStyle.FixedSingle;
            this.pedigreeLegend1.LegendFont = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.pedigreeLegend1.LegendRadius = 15;
            this.pedigreeLegend1.Location = new Point(200, 30);
            this.pedigreeLegend1.Name = "pedigreeLegend1";
            this.pedigreeLegend1.Size = new Size(450, 100);

            this.pedigreeComment1.BackColor = Color.WhiteSmoke;
            this.pedigreeComment1.Background = SystemColors.Window;
            this.pedigreeComment1.BorderStyle = BorderStyle.FixedSingle;
            this.pedigreeComment1.CommentFont = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.pedigreeComment1.Location = new Point(10, 500);
            this.pedigreeComment1.Name = "pedigreeComment1";
            this.pedigreeComment1.Size = new Size(228, 108);


            this.pedigreeTitleBlock1.BackColor = Color.White;
            this.pedigreeTitleBlock1.BorderStyle = BorderStyle.FixedSingle;
            this.pedigreeTitleBlock1.DOB = "";
            this.pedigreeTitleBlock1.DOBVis = true;
            this.pedigreeTitleBlock1.ForeColor = SystemColors.ControlText;
            this.pedigreeTitleBlock1.Location = new Point(30, 30);
            this.pedigreeTitleBlock1.Margin = new Padding(5);
            this.pedigreeTitleBlock1.MRN = "";
            this.pedigreeTitleBlock1.MRNVis = true;
            this.pedigreeTitleBlock1.Name = "pedigreeTitleBlock1";
            this.pedigreeTitleBlock1.NameText = "";
            this.pedigreeTitleBlock1.NameVis = true;
            this.pedigreeTitleBlock1.Size = new Size(150, 100);
            this.pedigreeTitleBlock1.Spacing = 3;

            pedigreeTitleBlock1.SetVariantLabel("", currentPrefs);

            FamilialVariants = new Dictionary<GeneticTestResult, List<Person>>();
            FamilialVariants.Clear();
        }

        private void GeneratePedigree()
        {
            pedigreeTitleBlock1.NameText = proband.name;
            pedigreeTitleBlock1.MRN = proband.unitnum;
            pedigreeTitleBlock1.DOB = proband.dob;

            pedigreeComment1.proband = proband;
            pedigreeComment1.Text = proband.family_comment;

            proband.FHx.SetIDsFromRelationships();
            pedigreeControl1.ForceLoadFamily(proband.FHx);

            currentPrefs = getBestFitExistingGuiPreference(false);
            pedigreeControl1.currentPrefs = currentPrefs;
            
            ShowComments();

            if (pedigreeControl1.currentPrefs.GUIPreference_width != 0 && pedigreeControl1.currentPrefs.GUIPreference_height != 0)
            {
                pedigreeControl1.Width = width;
                pedigreeControl1.Height = height;

                float x = pedigreeControl1.model.parameters.scale = ((float)pedigreeControl1.currentPrefs.GUIPreference_zoomValue) / 100.0f;

                pedigreeControl1.model.parameters.scale = 0.65f * x;
                
                pedigreeControl1.model.parameters.hOffset = (int)(((width / 2) / pedigreeControl1.model.parameters.scale) - (float)(pedigreeControl1.model.displayXMax / 2));
                pedigreeControl1.model.parameters.vOffset = (int)(((height / 2) / pedigreeControl1.model.parameters.scale) - (float)(pedigreeControl1.model.displayYMax / 2));
            }

            AddObservations();

            pedigreeControl1.model.individuals.ForEach(SetNorms);
            var maxx = pedigreeControl1.model.individuals.Max(pi => pi.HraPerson.x_norm);
            var minx = pedigreeControl1.model.individuals.Min(pi => pi.HraPerson.x_norm);
            var maxy = pedigreeControl1.model.individuals.Max(pi => pi.HraPerson.y_norm);
            var miny = pedigreeControl1.model.individuals.Min(pi => pi.HraPerson.y_norm);

            maxx += 60;
            minx -= 60;
            maxy += 80;
            miny -= 20;

            int pedigreeHeight = maxy - miny;
            this.height = pedigreeHeight;

            //TODO not sure if this is optimal - may want to determine if scale is limited by x or y and scale accordingly
            float zoom = (float)((double)width / (double)(maxx - minx));
            if (zoom < 1.0f)
            {
                pedigreeControl1.model.parameters.scale = zoom;
                pedigreeControl1.model.parameters.hOffset = (int)(((width / 2) / pedigreeControl1.model.parameters.scale) - (float)(pedigreeControl1.model.displayXMax / 2));
                pedigreeControl1.model.parameters.vOffset = (int)(((height / 2) / pedigreeControl1.model.parameters.scale) - (float)(pedigreeControl1.model.displayYMax / 2));
            }
            else
            {
                pedigreeControl1.model.parameters.scale = 1;
                pedigreeControl1.model.parameters.hOffset = (int)(((width / 2) / pedigreeControl1.model.parameters.scale) - (float)(pedigreeControl1.model.displayXMax / 2));
                pedigreeControl1.model.parameters.vOffset = (int)(((height / 2) / pedigreeControl1.model.parameters.scale) - (float)(pedigreeControl1.model.displayYMax / 2));
            }

            if (pedigreeControl1.model.parameters.scale > 0 && pedigreeControl1.model.parameters.scale < 2.0f)
            {
                pedigreeControl1.currentPrefs.GUIPreference_zoomValue = (int)(150.0f * pedigreeControl1.model.parameters.scale);
            }

            pedigreeControl1.controller.SetMode("SELF_ORGANIZING");
            int counter = 0;
            while (pedigreeControl1.model.converged == false && counter < 200)     //for (int i = 0; i < 100; i++)
            {
                pedigreeControl1.controller.IncrementLayout();
                counter++;
            }

            pedigreeControl1.CenterIndividuals();
            
            ApplyPrefs();

            ShowBrcaScores();

            ShowMmrScores();

            pedigreeControl1.model.parameters.annotation_areas = pedigreeSettingsForm1.annotation_areas;

            pedigreeControl1.model.FamilialVariants = FamilialVariants;

            Rectangle legendLayout;
            Rectangle commentBoxLayout;
            Rectangle titleBlockLayout;
            Rectangle pedigreeRegionLayout;
            Rectangle backdropLayout;
            int totalHeight;

            ComputePedigreeLayout(width, height,
                out legendLayout, 
                out commentBoxLayout, 
                out titleBlockLayout, 
                out pedigreeRegionLayout,
                out backdropLayout,
                out totalHeight);
            
            image = new Bitmap(width, totalHeight);

            Control canvasControl = new Control
            {
                BackColor = Color.White,
                Width = backdropLayout.Width,
                Height = backdropLayout.Height
            };
            canvasControl.DrawToBitmap(image, backdropLayout);

            pedigreeControl1.DrawToBitmap(image, pedigreeRegionLayout);

            pedigreeTitleBlock1.DrawToBitmapWithBorder(image, titleBlockLayout);

            if (string.IsNullOrEmpty(proband.Patient_Comment) == false && pedigreeComment1.Visible)
            {
                pedigreeComment1.DrawToBitmapWithBorder(image, commentBoxLayout);
            }
            if (pedigreeLegend1.Visible)
            {
                pedigreeLegend1.DrawToBitmapWithBorder(image, legendLayout);
            }
        }

        private void ShowComments()
        {
            if (string.IsNullOrEmpty(proband.family_comment))
            {
                pedigreeComment1.Visible = false;
            }
        }

        private void AddObservations()
        {
            this.pedigreeLegend1.AddObservations(this.pedigreeControl1.model.individuals.SelectMany(pi => pi.HraPerson.PMH.Observations.Cast<ClincalObservation>()));
        }

        private void SetNorms(PedigreeIndividual pi)
        {
            if (pi.HraPerson.x_norm == int.MinValue)
                pi.HraPerson.x_norm = (int)(pi.point.x - (pedigreeControl1.model.displayXMax / 2));

            if (pi.HraPerson.y_norm == int.MinValue)
                pi.HraPerson.y_norm = (int)(pi.point.y - (pedigreeControl1.model.displayYMax / 2));
        }

        private void ShowMmrScores()
        {
            if (showMmrScores)
            {
                pedigreeSettingsForm1.showMmrScores();
            }
            else
            {
                pedigreeSettingsForm1.hideMmrScores();
            }
        }

        private void ShowBrcaScores()
        {
            if (showBrcaScores)
            {
                pedigreeSettingsForm1.showBrcaScores();
            }
            else
            {
                pedigreeSettingsForm1.hideBrcaScores();
            }
        }

        private const int EdgeMargin = 10;

        private void ComputePedigreeLayout(int width, int height, 
            out Rectangle legendLayout, 
            out Rectangle commentBoxLayout, 
            out Rectangle titleBlockLayout, 
            out Rectangle pedigreeRegionLayout,
            out Rectangle backdropLayout,
            out int totalHeight)
        {
            int usableWidth = width - EdgeMargin * 2;
            int titleBoxHeight = ComputeTitleBoxHeight(this.pedigreeTitleBlock1);
            int titleBoxWidth = ComputeTitleBoxWidth(this.pedigreeTitleBlock1);
            int legendBoxWidth = usableWidth - titleBoxWidth - EdgeMargin;
            int legendBoxHeight = ComputeLegendBoxHeight(this.pedigreeLegend1, legendBoxWidth);
            int commentBoxHeight = ComputeCommentBoxHeight(this.pedigreeComment1, usableWidth);

            titleBoxHeight = Math.Max(titleBoxHeight, legendBoxHeight);
            legendBoxHeight = Math.Max(titleBoxHeight, legendBoxHeight);

            Point titleOrigin = new Point(EdgeMargin, EdgeMargin);
            Size titleSize = new Size(titleBoxWidth, titleBoxHeight);
            titleBlockLayout = new Rectangle(titleOrigin, titleSize);

            Point legendOrigin = new Point(EdgeMargin * 2 + titleBoxWidth, EdgeMargin);
            Size legendSize = new Size(legendBoxWidth, legendBoxHeight);
            legendLayout = new Rectangle(legendOrigin, legendSize);

            Point pedigreeOrigin = new Point(0, legendLayout.Height + EdgeMargin*2);
            Size pedigreeSize = new Size(width, height);
            pedigreeRegionLayout = new Rectangle(pedigreeOrigin, pedigreeSize);

            Point commentOrigin = new Point(EdgeMargin, titleBlockLayout.Height + pedigreeRegionLayout.Height + EdgeMargin*3);
            Size commentSize = new Size(usableWidth, commentBoxHeight);
            commentBoxLayout = new Rectangle(commentOrigin, commentSize);

            totalHeight = titleBlockLayout.Height + pedigreeRegionLayout.Height + commentBoxLayout.Height + EdgeMargin*4;

            Point backdropOrigin = new Point(0,0);
            Size backdropSize = new Size(width, totalHeight);
            backdropLayout = new Rectangle(backdropOrigin, backdropSize);
        }

        private int ComputeTitleBoxHeight(PedigreeTitleBlock pedigreeTitleBlock)
        {
            return pedigreeTitleBlock.ComputeOptimalHeight();
        }

        private int ComputeTitleBoxWidth(PedigreeTitleBlock pedigreeTitleBlock)
        {
            return pedigreeTitleBlock.ComputeOptimalWidth();
        }

        private int ComputeLegendBoxHeight(PedigreeLegend pedigreeLegend, int usableWidth)
        {
            return pedigreeLegend.CalculateOptimalDimensions(usableWidth, true);
        }

        private int ComputeCommentBoxHeight(PedigreeComment pedigreeComment, int usableWidth)
        {
            return pedigreeComment1.ComputeOptimalHeight(usableWidth);
        }

        private Rectangle ComputeRectangleForTitleBlock()
        {
            return new Rectangle(pedigreeTitleBlock1.Location, pedigreeTitleBlock1.Size);
        }

        /**************************************************************************************************/
        private void LoadActivePatientDataModel()
        {
            //  get active patinet object from session manager
            proband = SessionManager.Instance.GetActivePatient();

            LoadDataModel();
        }

        /**************************************************************************************************/
        private void LoadPatientDataModel(Patient p)
        {
            proband = p;

            LoadDataModel();
        }


        /**************************************************************************************************/
        private void LoadDataModel()
        {
            if (proband.guiPreferences.IsLoaded == false)
                proband.guiPreferences.BackgroundListLoad();

            if (proband.HraState != HraObject.States.Ready)
                proband.BackgroundLoadWork();

            if (proband.FHx.IsLoaded == false)
                proband.FHx.BackgroundListLoad();

            foreach (Person p in proband.FHx)
            {
                if (p.PMH.Observations.IsLoaded == false)
                    p.PMH.Observations.BackgroundListLoad();

                if (p.PMH.GeneticTests.IsLoaded == false)
                    p.PMH.GeneticTests.BackgroundListLoad();

                if (p.RP.HraState != HraObject.States.Ready)
                    p.RP.BackgroundLoadWork();

                foreach (GeneticTest gt in p.PMH.GeneticTests)
                {
                    ProcessGeneticTest(gt);
                }

                if (p.Ethnicity.IsLoaded == false)
                    p.Ethnicity.BackgroundListLoad();

                if (p.Nationality.IsLoaded == false)
                    p.Nationality.BackgroundListLoad();
            }
        }

        /**************************************************************************************************/
        private void ProcessGeneticTest(GeneticTest gt)
        {
            string label = "";

            lock (FamilialVariants)
            {
                FamilialVariants = this.proband.FHx.ReloadFamilialVariants();

                label = this.proband.FHx.BuildFamilialVariantsLabel();
            }
            pedigreeTitleBlock1.SetVariantLabel(label, currentPrefs);
        }

        /**************************************************************************************************/
        private GUIPreference getBestFitExistingGuiPreference(bool exactMatchOnly)
        {
            List<GUIPreference> localList = proband.guiPreferences.ConvertAll(x => (GUIPreference)x);
            String parentFormText = "";

            if (localList.Count == 0)
            {
                GUIPreference guiPreference;
                //String parentFormText = (this.ParentForm != null) ? this.ParentForm.Text : "";

                guiPreference = new GUIPreference(proband, DateTime.Now, "", parentFormText, width, height, true);
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                args.Persist = true;
                proband.guiPreferences.AddToList(guiPreference, args);
                return guiPreference;
            }
            else
            {
                GUIPreference guiPreference = null;
                foreach (GUIPreference gp in localList)
                {
                    //if (guiPreference == null)
                    guiPreference = gp;
                }
                return guiPreference;
            }
        }
        /**************************************************************************************************/
        private void ApplyPrefs()
        {
            if (currentPrefs != null)
            {
                pedigreeControl1.model.parameters.hideNonBloodRelatives = currentPrefs.GUIPreference_hideNonBloodRelatives;

                if (currentPrefs.GUIPreference_ShowLegend)
                    pedigreeLegend1.CheckForEmpty();
                else
                    pedigreeLegend1.Visible = false;

                pedigreeLegend1.Background = currentPrefs.GUIPreference_LegendBackground;
                pedigreeLegend1.BorderStyle = currentPrefs.GUIPreference_LegendBorder;
                pedigreeLegend1.LegendFont = currentPrefs.GUIPreference_LegendFont;
                pedigreeLegend1.LegendRadius = currentPrefs.GUIPreference_LegendRadius;
                pedigreeComment1.Visible = currentPrefs.GUIPreference_ShowComment;
                pedigreeComment1.Background = currentPrefs.GUIPreference_CommentBackground;
                pedigreeComment1.BorderStyle = currentPrefs.GUIPreference_CommentBorder;
                pedigreeComment1.CommentFont = currentPrefs.GUIPreference_CommentFont;
                pedigreeTitleBlock1.Visible = currentPrefs.GUIPreference_ShowTitle;
                pedigreeTitleBlock1.SetFonts(currentPrefs.GUIPreference_NameFont, currentPrefs.GUIPreference_UnitnumFont, currentPrefs.GUIPreference_DobFont);
                pedigreeTitleBlock1.NameVis = currentPrefs.GUIPreference_ShowName;
                pedigreeTitleBlock1.MRNVis = currentPrefs.GUIPreference_ShowUnitnum;
                pedigreeTitleBlock1.DOBVis = currentPrefs.GUIPreference_ShowDob;
                pedigreeTitleBlock1.Spacing = currentPrefs.GUIPreference_TitleSpacing;
                pedigreeTitleBlock1.BackColor = currentPrefs.GUIPreference_TitleBackground;
                pedigreeTitleBlock1.BorderStyle = currentPrefs.GUIPreference_TitleBorder;
                pedigreeControl1.model.parameters.BackgroundBrush = new SolidBrush(currentPrefs.GUIPreference_PedigreeBackground);
                pedigreeControl1.model.parameters.nameWidth = currentPrefs.GUIPreference_nameWidth;
                pedigreeControl1.model.parameters.limitedEthnicity = currentPrefs.GUIPreference_limitedEthnicity;
                pedigreeControl1.model.parameters.limitedNationality = currentPrefs.GUIPreference_limitedNationality;

                pedigreeLegend1.Location = new Point(currentPrefs.GUIPreference_LegendX, currentPrefs.GUIPreference_LegendY);

                if (!(currentPrefs.GUIPreference_LegendHeight == SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.GUIPreference_LegendHeight))
                {
                    pedigreeLegend1.Height = currentPrefs.GUIPreference_LegendHeight;
                }

                if (!(currentPrefs.GUIPreference_LegendWidth == SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.GUIPreference_LegendWidth))
                {
                    pedigreeLegend1.Width = currentPrefs.GUIPreference_LegendWidth;
                }
                pedigreeTitleBlock1.Location = new Point(currentPrefs.GUIPreference_TitleX, currentPrefs.GUIPreference_TitleY);
                pedigreeTitleBlock1.Height = currentPrefs.GUIPreference_TitleHeight;
                pedigreeTitleBlock1.Width = currentPrefs.GUIPreference_TitleWidth;

                pedigreeComment1.Location = new Point(currentPrefs.GUIPreference_CommentX, currentPrefs.GUIPreference_CommentY);
                pedigreeComment1.Height = currentPrefs.GUIPreference_CommentHeight;
                pedigreeComment1.Width = currentPrefs.GUIPreference_CommentWidth;

                foreach (PedigreeAnnotation pa in sysDefaultAnnotations)
                {
                    pedigreeSettingsForm1.SetPedigreeAnnotation(pa);
                }
                foreach (PedigreeAnnotation pa in currentPrefs.annotations)
                {
                    pedigreeSettingsForm1.SetPedigreeAnnotation(pa);
                }

                if (pedigreeControl1.model != null)
                {
                    pedigreeControl1.model.parameters.verticalSpacing = currentPrefs.GUIPreference_verticalSpacing;
                }
            }
            if (string.IsNullOrEmpty(proband.family_comment))
                pedigreeComment1.Visible = false;

        }
    }
}