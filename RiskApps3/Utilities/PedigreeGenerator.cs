using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RiskApps3.View.PatientRecord.Pedigree;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Controllers;
using RiskApps3.Model;
using System.ComponentModel;
using System.Threading;
using RiskApps3.Model.PatientRecord.PMH;

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

        public PedigreeGenerator(int Width, int Height)
        {
            height = Height;
            width = Width;


            //pedigreeControl1 = new PedigreeControl();
            pedigreeControl1 = new PedigreeControl(false);
            pedigreeLegend1 = new PedigreeLegend();
            pedigreeComment1 = new PedigreeComment();
            pedigreeTitleBlock1 = new PedigreeTitleBlock();
            sysDefaultAnnotations = SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.CopyAnnotations();
            pedigreeSettingsForm1 = new PedigreeSettingsForm(sysDefaultAnnotations);
        }
        public PedigreeGenerator(int Width, int Height, Patient proband)
        {
            height = Height;
            width = Width;


            //pedigreeControl1 = new PedigreeControl();
            pedigreeControl1 = new PedigreeControl(false);
            pedigreeLegend1 = new PedigreeLegend();
            pedigreeComment1 = new PedigreeComment();
            pedigreeTitleBlock1 = new PedigreeTitleBlock();
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
            this.pedigreeControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pedigreeControl1.BackColor = System.Drawing.Color.White;
            this.pedigreeControl1.FrameRate = 35;
            this.pedigreeControl1.Location = new System.Drawing.Point(0, 0);
            this.pedigreeControl1.Name = "pedigreeControl1";
            this.pedigreeControl1.Size = new System.Drawing.Size(width, height);



            this.pedigreeLegend1.BackColor = System.Drawing.Color.White;
            this.pedigreeLegend1.Background = System.Drawing.Color.White;
            this.pedigreeLegend1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pedigreeLegend1.LegendFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pedigreeLegend1.LegendRadius = 15;
            this.pedigreeLegend1.Location = new System.Drawing.Point(220, 50);
            this.pedigreeLegend1.Name = "pedigreeLegend1";
            this.pedigreeLegend1.Size = new System.Drawing.Size(350, 100);

            this.pedigreeComment1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pedigreeComment1.Background = System.Drawing.SystemColors.Window;
            this.pedigreeComment1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pedigreeComment1.CommentFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pedigreeComment1.Location = new System.Drawing.Point(10, 500);
            this.pedigreeComment1.Name = "pedigreeComment1";
            this.pedigreeComment1.Size = new System.Drawing.Size(228, 108);


            this.pedigreeTitleBlock1.BackColor = System.Drawing.Color.White;
            this.pedigreeTitleBlock1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pedigreeTitleBlock1.DOB = "";
            this.pedigreeTitleBlock1.DOBVis = true;
            this.pedigreeTitleBlock1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pedigreeTitleBlock1.Location = new System.Drawing.Point(10, 10);
            this.pedigreeTitleBlock1.Margin = new System.Windows.Forms.Padding(5);
            this.pedigreeTitleBlock1.MRN = "";
            this.pedigreeTitleBlock1.MRNVis = true;
            this.pedigreeTitleBlock1.Name = "pedigreeTitleBlock1";
            this.pedigreeTitleBlock1.NameText = "";
            this.pedigreeTitleBlock1.NameVis = true;
            this.pedigreeTitleBlock1.Size = new System.Drawing.Size(200, 50);
            this.pedigreeTitleBlock1.Spacing = 3;

            pedigreeTitleBlock1.SetVariantLabel("");

            FamilialVariants = new Dictionary<GeneticTestResult, List<Person>>();
            FamilialVariants.Clear();
        }

        private void GeneratePedigree()
        {
            int MaxWidth = width;
            int MaxHeight = height;

            bool new_gui_pref = true;
            if (proband.guiPreferences.Count == 0)
                new_gui_pref = true;

            pedigreeTitleBlock1.NameText = proband.name;
            pedigreeTitleBlock1.MRN = proband.unitnum;
            pedigreeTitleBlock1.DOB = proband.dob;

            pedigreeComment1.proband = proband;
            pedigreeComment1.Text = proband.family_comment;

            proband.FHx.SetIDsFromRelationships();
            pedigreeControl1.ForceLoadFamily(proband.FHx);

            currentPrefs = getBestFitExistingGuiPreference(false);
            pedigreeControl1.currentPrefs = currentPrefs;



            if (string.IsNullOrEmpty(proband.family_comment))
                pedigreeComment1.Visible = false;

            if (pedigreeControl1.currentPrefs.GUIPreference_width != 0 && pedigreeControl1.currentPrefs.GUIPreference_height != 0)
            {
                //pedigreeControl1.Width = pedigreeControl1.currentPrefs.GUIPreference_width;
                //pedigreeControl1.Height = pedigreeControl1.currentPrefs.GUIPreference_height;

                //width = pedigreeControl1.currentPrefs.GUIPreference_width;
                //height = pedigreeControl1.currentPrefs.GUIPreference_height;

                if (!new_gui_pref)
                {
                    pedigreeControl1.Width = pedigreeControl1.currentPrefs.GUIPreference_width;
                    pedigreeControl1.Height = pedigreeControl1.currentPrefs.GUIPreference_height;

                    width = pedigreeControl1.currentPrefs.GUIPreference_width;
                    height = pedigreeControl1.currentPrefs.GUIPreference_height;
                }
                else
                {
                    pedigreeControl1.Width = width;
                    pedigreeControl1.Height = height;

                }
                float x = pedigreeControl1.model.parameters.scale = ((float)pedigreeControl1.currentPrefs.GUIPreference_zoomValue) / 100.0f;

                pedigreeControl1.model.parameters.scale = 0.65f * x;
                //pedigreeControl1.model.parameters.scale = (float)height / (float)pedigreeControl1.currentPrefs.GUIPreference_height * x;
                //pedigreeControl1.model.parameters.scale = (float)width / (float)pedigreeControl1.currentPrefs.GUIPreference_width * x;

                pedigreeControl1.model.parameters.hOffset = (int)(((width / 2) / pedigreeControl1.model.parameters.scale) - (float)(pedigreeControl1.model.displayXMax / 2));
                pedigreeControl1.model.parameters.vOffset = (int)(((height / 2) / pedigreeControl1.model.parameters.scale) - (float)(pedigreeControl1.model.displayYMax / 2));

                foreach (PedigreeIndividual pi in pedigreeControl1.model.individuals)
                {
                    foreach (ClincalObservation co in pi.HraPerson.PMH.Observations)
                    {
                        pedigreeLegend1.AddSingleObservation(co, false);
                        pedigreeLegend1.Visible = true;
                    }

                }
            }
            if (new_gui_pref)
            {
                int minx = int.MaxValue;
                int maxx = int.MinValue;

                int miny = int.MaxValue;
                int maxy = int.MinValue;

                foreach (PedigreeIndividual pi in pedigreeControl1.model.individuals)
                {
                    foreach (ClincalObservation co in pi.HraPerson.PMH.Observations)
                    {
                        pedigreeLegend1.AddSingleObservation(co, false);
                        pedigreeLegend1.Visible = true;
                    }
                    if (pi.HraPerson.x_norm == int.MinValue)
                        pi.HraPerson.x_norm = (int)(pi.point.x - (pedigreeControl1.model.displayXMax / 2));

                    if (pi.HraPerson.y_norm == int.MinValue)
                        pi.HraPerson.y_norm = (int)(pi.point.y - (pedigreeControl1.model.displayYMax / 2));


                    if (pi.HraPerson.x_norm < minx)
                        minx = pi.HraPerson.x_norm;

                    if (pi.HraPerson.x_norm > maxx)
                        maxx = pi.HraPerson.x_norm;

                    if (pi.HraPerson.y_norm < miny)
                        miny = pi.HraPerson.y_norm;

                    if (pi.HraPerson.y_norm > maxx)
                        maxy = pi.HraPerson.y_norm;
                }

                maxx += 60;
                minx -= 60;

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
            }

            ApplyPrefs();


            if (new_gui_pref)
            {
                //pedigreeLegend1.Location = new Point(pedigreeTitleBlock1.Location.X + pedigreeTitleBlock1.Width + 10, pedigreeTitleBlock1.Location.Y);
            }

            if (showBrcaScores)
                pedigreeSettingsForm1.showBrcaScores();
            else
                pedigreeSettingsForm1.hideBrcaScores();

            if (showMmrScores)
                pedigreeSettingsForm1.showMmrScores();
            else
                pedigreeSettingsForm1.hideMmrScores();

            pedigreeControl1.model.parameters.annotation_areas = pedigreeSettingsForm1.annotation_areas;

            pedigreeControl1.model.FamilialVariants = FamilialVariants;

            image = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(image);

            pedigreeControl1.DrawFromView(g, width, height);

            pedigreeTitleBlock1.DrawToBitmapWithBorder(image, new Rectangle(pedigreeTitleBlock1.Location, pedigreeTitleBlock1.Size));

            if (string.IsNullOrEmpty(proband.Patient_Comment) == false && pedigreeComment1.Visible)
            {
                pedigreeComment1.DrawToBitmapWithBorder(image, new Rectangle(pedigreeComment1.Location, pedigreeComment1.Size));
            }
            if (pedigreeLegend1.Visible)
                pedigreeLegend1.DrawToBitmapWithBorder(image, new Rectangle(pedigreeLegend1.Location, pedigreeLegend1.Size));



            float MaxRatio = MaxWidth / (float)MaxHeight;
            float ImgRatio = image.Width / (float)image.Height;

            if (image.Width > MaxWidth)
                image = new Bitmap(image, new Size(MaxWidth, (int)Math.Round(MaxWidth /
                ImgRatio, 0)));

            if (image.Height > MaxHeight)
                image = new Bitmap(image, new Size((int)Math.Round(MaxWidth * ImgRatio,
                0), MaxHeight));
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
            pedigreeTitleBlock1.SetVariantLabel(label);
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