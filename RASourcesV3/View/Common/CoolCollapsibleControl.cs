using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.Common
{
    public partial class CoolCollapsibleControl : UserControl
    {
        private int ScrollIntervalValue = 5;
        private int HeaderHeightValue = 27;

        public CoolCollapsibleControl()
        {
            InitializeComponent();
        }

        public void SetScrollState(bool expanded)
        {
            if (expanded)
            {
                this.Height = dropDown.Location.Y + dropDown.Height + 3;
            }
            else
            {
                this.Height = this.header.Height;
            }
        }
        protected void DoScroll()
        {
            if (this.Height > this.header.Height)
            {
                while (this.Height > this.header.Height)
                {
                    Application.DoEvents();
                    this.Height -= ScrollIntervalValue;
                }
                //this.header.ImageIndex = 1;
                this.Height = this.header.Height;
            }
            else if (this.Height == this.header.Height)
            {
                //  int x = this.FixedHeight;
                int x = dropDown.Location.Y + dropDown.Height + 3;
                while (this.Height <= (x))
                {
                    Application.DoEvents();
                    this.Height += ScrollIntervalValue;
                }
                //this.header.ImageIndex = 0;
                this.Height = x;
            }
        }
    }
}
