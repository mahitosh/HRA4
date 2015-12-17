using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Model;
using RiskApps3.View.Admin;

namespace DevUtil
{
    public partial class UtilityLoginForm : Form
    {

        public string userLogin;


        public UtilityLoginForm()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void UtilityLoginForm_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.MetaData.Users.AddHandlersWithLoad(null, UsersReadyForParagraphEditor, null);
        }

        /**************************************************************************************************/
        private void UsersReadyForParagraphEditor(HraListLoadedEventArgs e)
        {
            User u = (User)SessionManager.Instance.MetaData.Users
                .FirstOrDefault(l => ((User)l).userLogin == userLogin);

            this.Hide();
                
            if (u != null)
            {
                SessionManager.Instance.ActiveUser = u;
                AdminMainForm amf = new AdminMainForm();
                amf.ShowDialog();
            }
            else
            {
                MessageBox.Show(
                    "User " + userLogin + " is not in the riskApps database.",
                    "Unable to login.");
            }
            
            this.Close();
        }
    }
}
