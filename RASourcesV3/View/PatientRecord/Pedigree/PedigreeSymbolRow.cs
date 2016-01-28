using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.MetaData;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeSymbolRow : UserControl
    {
        DiseaseObject dx;

        public PedigreeSymbolRow()
        {
            InitializeComponent();
        }
                    
        public PedigreeSymbolRow(DiseaseObject disease)
        {
            InitializeComponent();
            dx = disease;
            shortNameTextBox.Text = disease.diseaseShortName;
            label1.Text = disease.diseaseName;
            comboBox2.Text = disease.diseaseIconType;
            comboBox1.Text = disease.diseaseIconArea;

            try
            {
                
                button1.BackColor = Color.FromName(disease.diseaseIconColor);
            }
            catch (Exception e)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NamedColorChooser ncc = new NamedColorChooser();
            ncc.choosen = dx.DiseaseObject_diseaseIconColor;
            if (ncc.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(ncc.choosen) == false)
                {
                    button1.BackColor = Color.FromName(ncc.choosen);
                    dx.DiseaseObject_diseaseIconColor = ncc.choosen;
                }
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dx.DiseaseObject_diseaseIconType = comboBox2.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dx.DiseaseObject_diseaseIconArea = comboBox1.Text;
        }

        private void shortNameTextBox_TextChanged(object sender, EventArgs e)
        {
            dx.DiseaseObject_diseaseShortName = shortNameTextBox.Text;
        }
    }
}
