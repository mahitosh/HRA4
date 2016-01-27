using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.Reporting
{
    public partial class ReportElement : UserControl
    {
        public DateTime start;
        public DateTime end;
        public int clinicID;

        public ReportElement()
        {
            InitializeComponent();
        }
    }
}
