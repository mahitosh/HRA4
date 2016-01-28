using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using System.Diagnostics;
using RiskApps3.Controllers;
using System.Runtime.InteropServices;
using System.Threading;
using RiskApps3.View.PatientRecord.Pedigree;
using System.IO;
using RiskApps3.View.PatientRecord;
using System.Drawing.Printing;
using RiskApps3.Utilities;

namespace RiskApps3.View.Risk
{
    public partial class SimpleRiskModelView : HraView
    {
        public delegate void ChangeSyndromeCallbackType(string syndrome);
        public ChangeSyndromeCallbackType ChangeSyndromeDelegate;

        /**************************************************************************************************/
        PedigreeImageView pf;
        GenTestRecommendationsView gtrv;

        /**************************************************************************************************/
        public SimpleRiskModelView()
        {
            InitializeComponent();

            ChangeSyndromeDelegate = ChangeSyndrome;
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(PedigreeForm).ToString())
            {
                pf = new PedigreeImageView();
                pf.delayedDraw = true;
                //pf.SetMode("MANUAL");
                //pf.getPedigreeSettingsForm().showBrcaScores();
                pf.showBrcaScores = true;
                return pf;
            }

            if (persistString == typeof(GenTestRecommendationsView).ToString())
            {
                gtrv = new GenTestRecommendationsView();
                gtrv.Register(ChangeSyndromeDelegate);
                return gtrv;
            }
            else
                return null;
        }

        
        public void ChangeSyndrome(string syndrome)
        {
            if (syndrome == "Breast/Ovarian")
            {
                pf.showMmrScores = false;
                pf.showBrcaScores = true;
                pf.RedrawPedigree();
                //pf.getPedigreeSettingsForm().showBrcaScores();
            }
            else if (syndrome == "Colon/Endometrial")
            {
                pf.showBrcaScores = false;
                pf.showMmrScores = true;
                pf.RedrawPedigree();
                //pf.getPedigreeSettingsForm().showMmrScores();
            }
        }

        public void RefreshView()
        {
            if (pf != null)
                pf.RedrawPedigree();
        }
        /**************************************************************************************************/
        private void SimpleRiskModelView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

                string configFile = SessionManager.SelectDockConfig("SimpleRiskModelViewDockPanel.config");
                DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

                if (File.Exists(configFile))
                    theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
                else
                {
                    pf = new PedigreeImageView();
                    pf.delayedDraw = true;
                    //pf.SetMode("MANUAL");
                    //pf.getPedigreeSettingsForm().showBrcaScores();
                    pf.showBrcaScores = true; 
                    pf.Show(theDockPanel);
                    pf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    gtrv = new GenTestRecommendationsView();
                    gtrv.Register(ChangeSyndromeDelegate);
                    gtrv.Show(theDockPanel);
                    gtrv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
                }

                pf.DoDelayedDraw();
            }
        }


        /**************************************************************************************************/
        private void SimpleRiskModelView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("SimpleRiskModelViewDockPanel.config");

            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            if (pf != null)
                pf.Close();

            if (gtrv != null)
                gtrv.Close();


            SessionManager.Instance.RemoveHraView(this);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (Person p in SessionManager.Instance.GetActivePatient().FHx.Relatives)
            {
                p.RP.HraState = HraObject.States.Null;
            }

            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;
            toolStripButton1.Enabled = false;

            //SessionManager.Instance.GetActivePatient().RecalculateRisk();
            RunRiskModelsDialog rsmd = new RunRiskModelsDialog();
            rsmd.ShowDialog();

            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
            toolStripButton1.Enabled = true;

            RefreshView();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(this.Width, this.Height);
            pf.DrawToBitmap(b, new Rectangle(0, 0, pf.Width, pf.Height));
            gtrv.DrawToBitmap(b, new Rectangle(pf.Width, 0, gtrv.Width, gtrv.Height));
            Clipboard.SetImage(b);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(this.Width, this.Height);
            pf.DrawToBitmap(b, new Rectangle(0, 0, pf.Width, pf.Height));
            gtrv.DrawToBitmap(b, new Rectangle(pf.Width, 0, gtrv.Width, gtrv.Height));

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += (object printSender, PrintPageEventArgs printE) =>
            {
                float newWidth = b.Width * 100 / b.HorizontalResolution;
                float newHeight = b.Height * 100 / b.VerticalResolution;

                float widthFactor = newWidth / printE.PageBounds.Width;
                float heightFactor = newHeight / printE.PageBounds.Height;

                if (widthFactor > 1 | heightFactor > 1)
                {
                    if (widthFactor > heightFactor)
                    {
                        newWidth = newWidth / widthFactor;
                        newHeight = newHeight / widthFactor;
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }
                printE.Graphics.DrawImage(b, 0, 0, (int)newWidth, (int)newHeight);
            };


            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pd.PrinterSettings = dialog.PrinterSettings;
                pd.Print();
            }
            //PrintDocument pd = new PrintDocument();
            //pd.PrintPage += (object printSender, PrintPageEventArgs printE) =>
            //{
            //    printE.Graphics.DrawImageUnscaled(b, new Point(0, 0));
            //};

            //PrintDialog dialog = new PrintDialog();
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    pd.PrinterSettings = dialog.PrinterSettings;
            //    pd.Print();
            //}
        }




     }
}