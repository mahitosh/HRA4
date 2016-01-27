using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.RiskClinic
{
    public partial class SummaryStatusElement : UserControl
    {
        public SummaryStatusElement()
        {
            InitializeComponent();
        }
        public void SetLabels(string tag, string value)
        {
            label1.Text = tag;
            label2.Text = value;

            label2.Location = new Point(label1.Location.X + label1.Width + 3, label2.Location.Y);
            this.Width = label2.Location.X + label2.Width + 5;
        }
    }
}
