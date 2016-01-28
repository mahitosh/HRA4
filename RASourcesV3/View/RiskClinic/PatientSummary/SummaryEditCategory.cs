using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.RiskClinic.PatientSummary
{
    public partial class SummaryEditCategory : UserControl
    {
        public SummaryEditCategory(Type t)
        {
            InitializeComponent();
            object o = t.GetConstructor(new Type[] { }).Invoke(new object[] { });


        }
    }
}
