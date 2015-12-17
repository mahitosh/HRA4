using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using RiskApps3.Model.PatientRecord;
using System.Drawing.Printing;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord.Risk
{
    public partial class CancerRiskView : HraView
    {
        BrOvCancerRiskView bocrv;
        ColoEndoCancerRiskView colorv;

        public CancerRiskView()
        {
            InitializeComponent();
        }

        private void CancerRiskView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

                string configFile = SessionManager.SelectDockConfig("CancerRiskViewDockPanel.config");
                DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

                if (File.Exists(configFile))
                {
                    theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);

                }
                else
                {
                    colorv = new ColoEndoCancerRiskView();
                    colorv.Show(theDockPanel);
                    colorv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                    
                    bocrv = new BrOvCancerRiskView();
                    bocrv.Show(theDockPanel);
                    bocrv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                }

                bocrv.SetOvarianRiskVisibility(true);
            }
        }
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(BrOvCancerRiskView).ToString())
            {
                bocrv = new BrOvCancerRiskView();
                return bocrv;
            }
            if (persistString == typeof(ColoEndoCancerRiskView).ToString())
            {
                colorv = new ColoEndoCancerRiskView();
                return colorv;
            }

            return null;
        }

        private void CancerRiskView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("CancerRiskViewDockPanel.config");

            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);


            if (bocrv != null)
                bocrv.ViewClosing = true;
            if (colorv != null)
                colorv.ViewClosing = true;

            if (bocrv != null)
                bocrv.Close();
            if (colorv != null)
                colorv.Close();

            SessionManager.Instance.RemoveHraView(this);
        }

        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{

        //}

        //private void toolStripButton2_Click(object sender, EventArgs e)
        //{

        //}

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            foreach (Person p in SessionManager.Instance.GetActivePatient().FHx.Relatives)
            {
                p.RP.HraState = RiskApps3.Model.HraObject.States.Null;
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
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (theDockPanel.ActiveContent != null)
            {


                if (theDockPanel.ActiveContent.ToString().Contains("BrOvCancerRiskView"))
                {
                    Bitmap b = new Bitmap(bocrv.Width, bocrv.Height);
                    bocrv.DrawToBitmap(b, new Rectangle(0, 0, bocrv.Width, bocrv.Height));
                    Clipboard.SetImage(b);
                }
                else if (theDockPanel.ActiveContent.ToString().Contains("ColoEndoCancerRiskView"))
                {
                    Bitmap b = new Bitmap(colorv.Width, this.Height);
                    colorv.DrawToBitmap(b, new Rectangle(0, 0, colorv.Width, colorv.Height));
                    Clipboard.SetImage(b);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Bitmap b = null;
            if (theDockPanel.ActiveContent.ToString().Contains("BrOvCancerRiskView"))
            {
                b = new Bitmap(bocrv.Width, bocrv.Height);
                bocrv.DrawToBitmap(b, new Rectangle(0, 0, bocrv.Width, bocrv.Height));
                Clipboard.SetImage(b);
            }
            else if (theDockPanel.ActiveContent.ToString().Contains("ColoEndoCancerRiskView"))
            {
                b = new Bitmap(colorv.Width, this.Height);
                colorv.DrawToBitmap(b, new Rectangle(0, 0, colorv.Width, colorv.Height));
                Clipboard.SetImage(b);
            }

            if (b != null)
            {
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
}
