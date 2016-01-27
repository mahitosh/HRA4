using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RiskApps3.Model.MetaData;
using RiskApps3.Model;
using BrightIdeasSoftware;
using RiskApps3.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace RiskApps3.View.Admin
{
    public partial class MasterParagraphListView : HraView
    {
        /**************************************************************************************************/
        MasterParagraphList m_MasterParagraphList = new MasterParagraphList();
        DefaultParagraphList m_DefaultParagraphList = new DefaultParagraphList();
        ProviderParagraphList m_ProviderParagraphList = new ProviderParagraphList();

        bool setup = false;

        /**************************************************************************************************/
        public MasterParagraphListView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void PatientListView_Load(object sender, EventArgs e)
        {
            if (!(SessionManager.Instance.ActiveUser.hraProviderID > 0))
            {
                MakeUserProviderForm mup = new MakeUserProviderForm();
                mup.ShowDialog();
            }

            if (SessionManager.Instance.ActiveUser.hraProviderID > 0)
            {
                m_MasterParagraphList.AddHandlersWithLoad(null, ListLoaded, null);
                m_DefaultParagraphList.AddHandlersWithLoad(null, ListLoaded, null);

                m_ProviderParagraphList.providerID = SessionManager.Instance.ActiveUser.hraProviderID;
                m_ProviderParagraphList.AddHandlersWithLoad(null, ListLoaded, null);

                loadingCircle1.Active = true;

                this.Text = "Paragraphs for " + SessionManager.Instance.ActiveUser.ToString();
            }
            else
            {
                tabControl1.Visible = false;
            }
        }

        /**************************************************************************************************/
        private void ListLoaded(HraListLoadedEventArgs e)
        {
            if (m_MasterParagraphList.IsLoaded &&
                m_DefaultParagraphList.IsLoaded &&
                m_ProviderParagraphList.IsLoaded)
            {
                FillControls();
            }
        }
        /**************************************************************************************************/
        private void FillControls()
        {
            if (setup == false)
            {
                List<string> types = new List<string>();
                foreach (MasterParagraph mp in m_MasterParagraphList)
                {
                    //Console.WriteLine(mp.impression);
                    if (!types.Contains(mp.Type))
                    {
                        if (mp.Type.Length > 0)
                            types.Add(mp.Type);
                    }
                    foreach (ProviderParagraph pp in m_DefaultParagraphList)
                    {
                        if (pp.paragraphID == mp.paragraphID)
                        {
                            mp.DefaultPatientParagraph = pp.patientParagraph;
                            mp.DefaultProviderParagraph = pp.paragraph;
                        }
                    }

                    foreach (ProviderParagraph pp in m_ProviderParagraphList)
                    {
                        if (pp.paragraphID == mp.paragraphID)
                        {
                            mp.providerSpecific = pp;
                        }
                    }
                }

                foreach (string t in types)
                {
                    TabPage tp = new TabPage(t);
                    EditParagraphControl epc = new EditParagraphControl(t);
                    if (t == "Risk")
                    {
                        epc.DefaultOnly = true;
                    }
                    foreach (MasterParagraph mp in m_MasterParagraphList)
                    {
                        if (mp.Type == t)
                        {
                            epc.AddParagraph(mp);
                        }
                    }
                    epc.Dock = DockStyle.Fill;
                    tp.Controls.Add(epc);
                    tabControl1.TabPages.Add(tp);
                }

                setup = true;
            }
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
        }

        /**************************************************************************************************/
        private void PatientListView_FormClosing(object sender, FormClosingEventArgs e)
        {
            ValidateChildren();
            m_MasterParagraphList.ReleaseListeners(this);
        }

        /**************************************************************************************************/
        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /**************************************************************************************************/
        private void objectListView1_SelectionChanged(object sender, EventArgs e)
        {

        }

    }
}
