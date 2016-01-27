using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.Common
{
    public partial class GenericProgressForm : Form
    {
        public GenericProgressForm()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }
    }
}
