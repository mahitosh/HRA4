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
using RiskApps3.View.PatientRecord.Risk;

namespace RiskApps3.Utilities
{
    public class RiskChartGenerator
    {
        public int height = 500;
        public int width = 500;

        Patient proband;
        BrOvCancerRiskView bocrv;

        Bitmap image = null;

        public RiskChartGenerator(int Width, int Height)
        {
            height = Height;
            width = Width;
        }

        public RiskChartGenerator(int Width, int Height, Patient patient)
        {
            height = Height;
            width = Width;
            proband = patient;
        }

        public Bitmap GenerateRiskChartImage()
        {
            int MaxWidth = width;
            int MaxHeight = height;

            LoadPatientDataModel();

            GenerateRiskChart();

            Close();

            float MaxRatio = MaxWidth / (float)MaxHeight;
            float ImgRatio = image.Width / (float)image.Height;

            if (image.Width > MaxWidth)
                image = new Bitmap(image, new Size(MaxWidth, (int)Math.Round(MaxWidth /
                ImgRatio, 0)));

            if (image.Height > MaxHeight)
                image = new Bitmap(image, new Size((int)Math.Round(MaxWidth * ImgRatio,
                0), MaxHeight));


            return image;
        }
        
        public void Close()
        {
        }

        private void GenerateRiskChart()
        {
            bocrv = new BrOvCancerRiskView(proband);
            image = bocrv.getRiskChartToDisplay();
        }

        /**************************************************************************************************/
        private void LoadPatientDataModel()
        {
            if (proband == null)
            {
                proband = SessionManager.Instance.GetActivePatient();
            }

            proband.RP.LoadFullObject();
        }

    }
}
