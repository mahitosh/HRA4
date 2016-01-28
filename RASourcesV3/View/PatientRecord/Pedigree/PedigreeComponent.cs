using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeComponent : UserControl
    {
        int gripMargin = 10;

        bool Dragging = false;
        bool Sizing = false;
        private Point ClickPosition;

        public PedigreeComponent()
        {
            InitializeComponent();
        }

        private void PedigreeComponent_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = true;
            
            ClickPosition = e.Location;

            if (this.Cursor == Cursors.Hand)
            {
                Dragging = true;
            }
            else if (this.Cursor == Cursors.SizeNWSE ||
                     this.Cursor == Cursors.SizeNS ||
                     this.Cursor == Cursors.SizeWE)
            {
                Sizing = true;
            }
        }

        private void PedigreeComponent_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                Point p = this.Parent.PointToClient(PointToScreen(e.Location));
                Location = new Point(p.X - ClickPosition.X, p.Y - ClickPosition.Y);
            }
            else if (Sizing)
            {
                if (this.Cursor == Cursors.SizeNWSE)
                {
                    if (e.X > 10 && e.Y > 10)
                        this.Size = new Size(e.X, e.Y);
                }
                else if (this.Cursor == Cursors.SizeNS)
                {
                    if (e.Y > 10)
                        this.Size = new Size(this.Width, e.Y);
                }
                else if (this.Cursor == Cursors.SizeWE)
                {
                    if (e.X > 10)
                        this.Size = new Size(e.X, this.Height);
                }
            }
            else
            {
                Cursor = Cursors.Hand;


                if (Width - e.X < gripMargin)
                {
                    Cursor = Cursors.SizeWE;
                    if (Height - e.Y < gripMargin)
                    {
                        Cursor = Cursors.SizeNWSE;
                    }
                }
                if (Height - e.Y < gripMargin)
                {
                    Cursor = Cursors.SizeNS;
                    if (Width - e.X < gripMargin)
                    {
                        Cursor = Cursors.SizeNWSE;
                    }
                }
            }
        }

        private void PedigreeComponent_MouseUp(object sender, MouseEventArgs e)
        {
            Refresh();
            Dragging = false;
            Capture = false;
            Sizing = false;
        }
    }
}
