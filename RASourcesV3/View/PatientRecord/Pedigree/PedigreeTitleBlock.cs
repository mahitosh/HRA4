﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeTitleBlock : PedigreeComponent
    {
        

        public PedigreeTitleBlock()
        {
            InitializeComponent();
        }
        public string NameText
        {
            get
            {
                return NameLabel.Text;
            }
            set
            {
                NameLabel.Text = value;
            }
        }
        public string MRN
        {
            get
            {
                return mrnLabel.Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value)==false)
                    mrnLabel.Text = "MRN: " + value;
                else
                    mrnLabel.Text = value;
            }
        }
        public string DOB
        {
            get
            {
                return dobLabel.Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    dobLabel.Text = "DOB: " + value;
                else
                    dobLabel.Text = value;
            }
        }
        public bool NameVis
        {
            get
            {
                return NameLabel.Visible;
            }
            set
            {
                NameLabel.Visible = value;
            }
        }
        public bool MRNVis
        {
            get
            {
                return mrnLabel.Visible;
            }
            set
            {
                mrnLabel.Visible = value;
            }
        }
        public bool DOBVis
        {
            get
            {
                return dobLabel.Visible;
            }
            set
            {
                dobLabel.Visible = value;
            }
        }
        public int Spacing
        {
            get
            {
                return NameLabel.Margin.Left;
            }
            set
            {
                NameLabel.Margin = new Padding(3,3,value,3);
                dobLabel.Margin = new Padding(value, 3, value, 3);
                mrnLabel.Margin = new Padding(value, 3, 3, 3);
            }
        }

        internal void SetFonts(Font name, Font mrn, Font dob)
        {
            NameLabel.Font = name;
            mrnLabel.Font = mrn;
            dobLabel.Font = dob;
        }

        internal void AdjustFontSize(float scalingFactor = 1)
        {
            this.NameLabel.Font = this.NameLabel.Font.Scale(scalingFactor);
            this.mrnLabel.Font = this.mrnLabel.Font.Scale(scalingFactor);
            this.dobLabel.Font = this.dobLabel.Font.Scale(scalingFactor);
        }

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void flowLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        delegate void SetVariantLabelCallback(string label, GUIPreference currentGuiPrefs);
        internal void SetVariantLabel(string label, GUIPreference currentGuiPrefs)
        {
            if (variantLabel.InvokeRequired)
            {
                SetVariantLabelCallback aoc = SetVariantLabel;
                object[] args = new object[1];
                args[0] = label;
                args[1] = currentGuiPrefs;
                this.Invoke(aoc, args);
            }
            else
            {
                variantLabel.Text = label;
                int width = this.ComputeOptimalWidth();
                int height = this.ComputeOptimalHeight();
                if (currentGuiPrefs != null)
                {
                    currentGuiPrefs.GUIPreference_TitleWidth = width;
                    currentGuiPrefs.GUIPreference_TitleHeight = height;
                }
            }
        }

        internal void DrawToBitmapWithBorder(Bitmap image, Rectangle rectangle, float scalingFactor = 1)
        {
            if (rectangle.X < 0)
                rectangle.X = 50;
            if (rectangle.Y < 0)
                rectangle.Y = 50;

            this.mrnLabel.Size = this.mrnLabel.Size.Scale(scalingFactor);
            //rectangle.Size = new Size(image.Width - (EdgeMargin * 2), this.mrnLabel.Size.Height + 4);
            this.Size = rectangle.Size;

            Graphics g = Graphics.FromImage(image);
            DrawToBitmap(image, rectangle);
            g.DrawRectangle(Pens.Black, rectangle);
        }

        internal int ComputeOptimalWidth()
        {
            var labels = new[] { this.NameLabel, this.mrnLabel, this.dobLabel, this.variantLabel };
            
            int width = (int)labels
                .Select(label => label.CreateGraphics().MeasureString(ExtractLongestString(label), label.Font))
                .Max(size => size.Width);

            int maxMargins = labels.Max(label => 
                label.Margin.Left + 
                label.Margin.Right);
            
            int maxPadding = labels.Max(label => 
                label.Padding.Left + 
                label.Padding.Right);

            int trueWidth = maxMargins + maxPadding + width;
            
            this.Width = trueWidth;
            this.flowLayoutPanel1.Width = trueWidth;

            return trueWidth;
        }

        private static string ExtractLongestString(Label label)
        {
            float longestSize = 0;
            string longestLabel = string.Empty;
            foreach (string test in label.Text.Split('\n'))
            {
                float testWidth = label.CreateGraphics().MeasureString(test, label.Font).Width;
                if (testWidth >= longestSize)
                {
                    longestSize = testWidth;
                    longestLabel = test;
                }
            }

            return longestLabel;
        }

        internal int ComputeOptimalHeight()
        {
            int height = this.variantLabel.Location.Y +
                         (int)this.variantLabel.CreateGraphics()
                            .MeasureString(this.variantLabel.Text, this.variantLabel.Font)
                            .Height + 
                        this.variantLabel.Margin.Bottom + 
                        this.variantLabel.Padding.Bottom;
            
            this.Height = height;
            this.flowLayoutPanel1.Height = height;

            return height;
        }
    }
}