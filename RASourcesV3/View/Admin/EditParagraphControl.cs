using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.MetaData;
using RiskApps3.Controllers;

namespace RiskApps3.View.Admin
{
    public partial class EditParagraphControl : UserControl
    {
        string type;
        public bool DefaultOnly = false;

        public EditParagraphControl(string Type)
        {
            type = Type;
            InitializeComponent();

        }

        internal void AddParagraph(MasterParagraph mp)
        {
            objectListView1.AddObject(mp);
            objectListView1.Columns[0].Width = -2;
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectListView1.SelectedObject != null)
            {
                richTextBox1.Text = ((MasterParagraph)(objectListView1.SelectedObject)).DefaultProviderParagraph;
                richTextBox2.Text = ((MasterParagraph)(objectListView1.SelectedObject)).DefaultPatientParagraph;

                label1.Text = "Default text for " + ((MasterParagraph)(objectListView1.SelectedObject)).Description;
                label2.Text = SessionManager.Instance.ActiveUser.ToString() + "'s text for " + ((MasterParagraph)(objectListView1.SelectedObject)).Description;

                if (((MasterParagraph)(objectListView1.SelectedObject)).providerSpecific != null)
                {
                    richTextBox3.Text = ((MasterParagraph)(objectListView1.SelectedObject)).providerSpecific.paragraph;
                    richTextBox4.Text = ((MasterParagraph)(objectListView1.SelectedObject)).providerSpecific.patientParagraph;
                }
                else
                {
                    richTextBox3.Text = "";
                    richTextBox4.Text = "";
                }
            }
        }
        private void richTextBox3_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox3.Text) == false)
            {

                if (objectListView1.SelectedObject != null)
                {
                    MasterParagraph mp = ((MasterParagraph)(objectListView1.SelectedObject));

                    //if (DefaultOnly)
                    //{
                    //    mp.DefaultProviderParagraph = richTextBox3.Text;

                    //    ProviderParagraph pp = new ProviderParagraph();
                    //    pp.providerID = -1;
                    //    pp.paragraphID = mp.paragraphID;
                    //    pp.patientParagraph = mp.DefaultPatientParagraph;
                    //    pp.ProviderParagraph_paragraph = richTextBox3.Text;
                    //    mp.providerSpecific = pp;
                    //}
                    //else
                    //{
                        if (mp.providerSpecific != null)
                        {
                            mp.providerSpecific.ProviderParagraph_paragraph = richTextBox3.Text;
                        }
                        else
                        {
                            ProviderParagraph pp = new ProviderParagraph();
                            pp.providerID = SessionManager.Instance.ActiveUser.hraProviderID;
                            pp.paragraphID = mp.paragraphID;
                            pp.patientParagraph = mp.DefaultPatientParagraph;
                            pp.paragraph = mp.DefaultProviderParagraph;

                            mp.providerSpecific = pp;
                            mp.providerSpecific.ProviderParagraph_paragraph = richTextBox3.Text;
                        }
                    //}
                }

                //if (objectListView1.SelectedObject != null)
                //{
                //    if (((MasterParagraph)(objectListView1.SelectedObject)).providerSpecific != null)
                //    {
                //        ((MasterParagraph)(objectListView1.SelectedObject)).providerSpecific.ProviderParagraph_paragraph = richTextBox3.Text;
                //    }
                //    else
                //    {
                //        MasterParagraph mp = ((MasterParagraph)(objectListView1.SelectedObject));
                //        if (DefaultOnly)
                //        {
                //            mp.DefaultProviderParagraph = richTextBox3.Text;
                //        }
                //        else
                //        {
                //            ProviderParagraph pp = new ProviderParagraph();
                //            pp.providerID = SessionManager.Instance.ActiveUser.hraProviderID;
                //            pp.paragraphID = mp.paragraphID;
                //            pp.patientParagraph = mp.DefaultPatientParagraph;
                //            pp.paragraph = mp.DefaultProviderParagraph;

                //            mp.providerSpecific = pp;
                //            ((MasterParagraph)(objectListView1.SelectedObject)).providerSpecific.ProviderParagraph_paragraph = richTextBox3.Text;
                //        }
                //    }
                //}
            }
        }
        private void richTextBox4_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox4.Text) == false)
            {
                if (objectListView1.SelectedObject != null)
                {   
                    MasterParagraph mp = ((MasterParagraph)(objectListView1.SelectedObject));
                                  
                    //if (DefaultOnly)
                    //{
                    //    mp.DefaultPatientParagraph = richTextBox4.Text;

                    //    ProviderParagraph pp = new ProviderParagraph();
                    //    pp.providerID = -1;
                    //    pp.paragraphID = mp.paragraphID;
                    //    pp.paragraph = mp.DefaultProviderParagraph;
                    //    pp.ProviderParagraph_patientParagraph = richTextBox4.Text;
                    //    mp.providerSpecific = pp;
                    //}
                    //else
                    //{
                        if (mp.providerSpecific != null)
                        {
                            mp.providerSpecific.ProviderParagraph_patientParagraph = richTextBox4.Text;
                        }
                        else
                        {
                                ProviderParagraph pp = new ProviderParagraph();
                                pp.providerID = SessionManager.Instance.ActiveUser.hraProviderID;
                                pp.paragraphID = mp.paragraphID;
                                pp.patientParagraph = mp.DefaultPatientParagraph;
                                pp.paragraph = mp.DefaultProviderParagraph;

                                mp.providerSpecific = pp;
                                mp.providerSpecific.ProviderParagraph_patientParagraph = richTextBox4.Text;
                        }
                    //}
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = richTextBox1.Text;
            richTextBox4.Text = richTextBox2.Text;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
