using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.Common
{
    public partial class HtmlEditor : Form
    {
        string s = "";
        public HtmlEditor(string docName)
        {
            
            InitializeComponent();
            s = File.ReadAllText(docName);
            winFormHtmlEditor1.DocumentHtml = s;
        }

        private void HtmlEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
