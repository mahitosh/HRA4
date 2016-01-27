using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class Annotation : UserControl
    {
        public string label = "";
        public bool active = false;
        public AnnotationContainer StartingContainer;
        public PedigreeAnnotation hraAnnotation;
        public Font textFont = new System.Drawing.Font("Tahoma", 10);

        public Annotation()
        {
            InitializeComponent();
        }
        public Annotation(string Name)
        {
            InitializeComponent();
            label = Name;
            Size labelSize = TextRenderer.MeasureText(label, textFont);
            this.Size = new Size(labelSize.Width, labelSize.Height);
            this.Name = Name;
        }
        public Annotation(PedigreeAnnotation annotation)
        {
            InitializeComponent();
            label = annotation.annotation;
            Size labelSize = TextRenderer.MeasureText(label, textFont);
            this.Size = new Size(labelSize.Width, labelSize.Height);
            this.Name = label;

            hraAnnotation = annotation;
        }

        private void Annotation_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(label, textFont, Brushes.Black, new PointF(0, 0));

        }
    }
}
