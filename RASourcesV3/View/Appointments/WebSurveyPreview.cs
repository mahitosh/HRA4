using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.Appointments
{
    public partial class WebSurveyPreview : Form
    {
        string p_html;

        public WebSurveyPreview(string html)
        {
            InitializeComponent();
            p_html = html;
        }

        private void WebSurveyPreview_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("about:blank");
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.Write(string.Empty);
            }
            webBrowser1.DocumentText = p_html;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
