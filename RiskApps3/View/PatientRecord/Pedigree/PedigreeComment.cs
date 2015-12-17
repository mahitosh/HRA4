using System;
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
    public partial class PedigreeComment : PedigreeComponent
    {
        public Patient proband;

        public Font CommentFont
        {
            get
            {
                return commentsTextBox.Font;
            }
            set
            {
                commentsTextBox.Font = value;
            }
        }

        public string Text
        {
            get
            {
                return commentsTextBox.Text;
            }
            set
            {
                commentsTextBox.Text = value;

            }
        }

        public Color Background
        {
            get
            {
                return commentsTextBox.BackColor;
            }
            set
            {
                commentsTextBox.BackColor = value;
            }
        }
        public PedigreeComment()
        {
            InitializeComponent();
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (proband != null)
            {
                proband.Patient_Comment = commentsTextBox.Text;
            }
        }

        internal void AdjustFontSize(float scalingFactor = 1)
        {
            this.CommentFont = this.CommentFont.Scale(scalingFactor);
        }

        internal void DrawToBitmapWithBorder(Bitmap image, Rectangle rectangle, float scalingFactor = 1)
        {
            if (rectangle.X < 0)
                rectangle.X = 0;
            if (rectangle.Y < 0)
                rectangle.Y = 0;

            //rectangle.Size = new Size(image.Width - (EdgeMargin * 2), (int)Math.Round(rectangle.Size.Height * scalingFactor));
            this.Size = rectangle.Size;
            this.commentsTextBox.Size = this.commentsTextBox.GetPreferredSize(this.Size);
            
            Graphics g = Graphics.FromImage(image); 
            DrawToBitmap(image, rectangle);
            g.DrawRectangle(Pens.Black, rectangle);
        }
    }
}
