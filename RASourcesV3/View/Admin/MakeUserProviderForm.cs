using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;

namespace RiskApps3.View.Admin
{
    public partial class MakeUserProviderForm : Form
    {
        public MakeUserProviderForm()
        {
            InitializeComponent();
        }

        private void MakeUserProviderForm_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.MetaData.ApptProviders.AddHandlersWithLoad(null, ListLoaded, null);
            if (SessionManager.Instance.ActiveUser != null)
            {
                label3.Text = SessionManager.Instance.ActiveUser.userFullName;
            }
        }



        /**************************************************************************************************/
        private void ListLoaded(HraListLoadedEventArgs e)
        {
            foreach (Provider u in SessionManager.Instance.MetaData.ApptProviders)
            {
                comboBox1.Items.Add(u);
            }
        }

        private void MakeUserProviderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.MetaData.ApptProviders.ReleaseListeners(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Provider p = (Provider)comboBox1.SelectedItem;
            if (p != null)
            {
                SessionManager.Instance.ActiveUser.User_hraProviderID = p.providerID;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a provider from the list.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Provider p = new Provider();
            p.firstName = SessionManager.Instance.ActiveUser.userFirstName;
            p.lastName = SessionManager.Instance.ActiveUser.userLastName;
            p.fullName = SessionManager.Instance.ActiveUser.userFullName;
            p.displayName = SessionManager.Instance.ActiveUser.userFullName;
            p.isApptProvider = "Yes";

            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            p.BackgroundPersistWork(args);

            SessionManager.Instance.ActiveUser.User_hraProviderID = p.providerID;

            args.Persist = false;

            SessionManager.Instance.MetaData.ApptProviders.AddToList(p, args);

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
