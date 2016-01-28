using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskAppCore;
using RiskApps3.Model.MetaData;
using RiskApps3.Controllers;
using RiskApps3.View;
using System.Security.Principal;
using RiskApps3.Model;
using Configurator = RiskApps3.Utilities.Configurator;
using User = RiskApps3.Model.MetaData.User;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.DirectoryServices.AccountManagement;
using System.Collections.Specialized;

namespace RiskApps3
{
    public partial class MainFormLoading : HraView
    {
        /**************************************************************************************************/
        UserList users;
        string ntUser = "";
        
        public bool UseNtAuthentication = true;
        public int roleID = -1;
        public string roleName = "";
        private static string LDAPSecurityContext = "Off";  // jdg 10/30/15

        Stopwatch stopWatch = new Stopwatch();
        public const double requiredSplashTime = 2.0;

        /**************************************************************************************************/
        public MainFormLoading()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void MainFormLoading_Load(object sender, EventArgs e)
        {
            stopWatch.Start();

            this.Height = 370;

            SessionManager.Instance.MetaData.Diseases.LoadFullList();
            SessionManager.Instance.MetaData.GeneticTests.LoadFullList();
            SessionManager.Instance.MetaData.UserGroups.LoadFullList();
            SessionManager.Instance.MetaData.OrderTypes.LoadFullList();
            SessionManager.Instance.MetaData.Mutations.LoadFullList();
            SessionManager.Instance.MetaData.KbVariants.LoadFullObject();
            SessionManager.Instance.MetaData.BrOvCdsRecs.LoadFullObject();
            SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.LoadFullObject();
            SessionManager.Instance.MetaData.CurrentUserDefaultPedigreePrefs.LoadFullObject();
            SessionManager.Instance.MetaData.Globals.LoadFullObject();

            users = SessionManager.Instance.MetaData.Users;
            users.AddHandlersWithLoad(null, UsersListLoaded, null);
        }

        /**************************************************************************************************/
        private void UsersListLoaded(HraListLoadedEventArgs e)
        {
            //don't show and ask for login + password unless not using NT Auth or NT Auth failed
            groupBox1.Visible = false;

            WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            string strTemp = wp.Identity.Name;
            int intPos = strTemp.IndexOf("\\", 0);
            ntUser = strTemp.Substring(intPos + 1);

            usernameTextBox.Text = ntUser;

            usernameTextBox.Focus();
            usernameTextBox.SelectAll();
            bool userInList = users.IsUserInList(usernameTextBox.Text);
            if (UseNtAuthentication && Configurator.useNTAuthentication() && userInList)
            {
                roleID = RiskAppCore.User.fetchUserRoleID(usernameTextBox.Text);
                roleName = RiskAppCore.User.fetchUserRoleName(usernameTextBox.Text);

                SessionManager.Instance.ActiveUser = users.GetUser(usernameTextBox.Text);
                InitUserGUIPrefs(SessionManager.Instance.ActiveUser);
                SessionManager.Instance.ActiveUser.UserClinicList.user_login = SessionManager.Instance.ActiveUser.userLogin;
                SessionManager.Instance.ActiveUser.UserClinicList.AddHandlersWithLoad(null, UserClinicListLoaded, null);
                
                HraObject.AuditUserLogin(ntUser);

                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;
                if (ts.TotalSeconds < requiredSplashTime)
                {
                    progressBar1.Style = ProgressBarStyle.Blocks;
                    progressBar1.Value = progressBar1.Maximum;
                    progressBar1.Refresh();
                    
                    for (int i = 1; i <= 50; i++)
                    {   Application.DoEvents();
                        Thread.Sleep((int)(20 * (requiredSplashTime - ts.TotalSeconds)));
                    }
                }
            }
            else
            {
                groupBox1.Visible = true;
                progressBar1.Visible = false;
                label12.Visible = false;
                foreach (User u in users)
                {
                    if (u.userLogin.Equals(ntUser))
                    {
                        usernameTextBox.Tag = u;
                        usernameTextBox.Text = u.userLogin;
                        break;
                    }
                }

                while (this.Height < 525)
                {
                    this.Height = this.Height + 5;
                    Application.DoEvents();
                }
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string lockoutMsg = "";
            User u = null;
            bool validUser = users.Any(x => ((User)x).userLogin.ToLower() == usernameTextBox.Text.ToLower());
            if (validUser) u = (User)(users.First(x => ((User)x).userLogin.ToLower() == usernameTextBox.Text.ToLower()));

            // begin jdg 10/30/15
            NameValueCollection values = Configurator.GetConfig("AppSettings");
            if (values != null)
            {
                LDAPSecurityContext = values["SecurityContext"];
            }
            if (String.IsNullOrEmpty(LDAPSecurityContext)) LDAPSecurityContext = "Off";
            bool bLDAPSuccess = false;
            bool bLDAP = (((LDAPSecurityContext.ToUpper() == "MACHINE") || (LDAPSecurityContext.ToUpper() == "DOMAIN")) ? true : false);

            try
            {
                if (bLDAP)
                {
                    switch (LDAPSecurityContext.ToUpper())
                    {
                        case "MACHINE":
                            using (var context = new PrincipalContext(ContextType.Machine))
                            {
                                if (context.ValidateCredentials(usernameTextBox.Text, passwordTextBox.Text))
                                {
                                    bLDAPSuccess = true;
                                }
                            }
                            break;
                        case "DOMAIN":
                            using (var context = new PrincipalContext(ContextType.Domain))
                            {
                                if (context.ValidateCredentials(usernameTextBox.Text, passwordTextBox.Text))
                                {
                                    bLDAPSuccess = true;
                                }
                            }
                            break;
                        default:

                            break;
                    }
                }
            }
            catch (Exception excLDAP)
            {
                RiskApps3.Utilities.Logger.Instance.WriteToLog("LDAP Authentication failed for user " + usernameTextBox.Text + " for this reason: " + excLDAP.ToString());
            }
            // end jdg 10/30/15

            String encryptedPassword = RiskAppCore.User.encryptPassword(passwordTextBox.Text);
                    
            bool authenticated = false;
            int numFailedAttempts = 0;
            int numMaxFailures = 5;
            int lockoutPeriod = 0;
            lockoutMsg = "";

            Utilities.ParameterCollection pc = new Utilities.ParameterCollection();
            pc.Add("ntLoginName", DBUtils.makeSQLSafe(ntUser));
            pc.Add("userLogin", DBUtils.makeSQLSafe((u != null) ? u.userLogin : usernameTextBox.Text));

            //if (SessionManager.Instance.MetaData.Globals.encryptPasswords)
            if ((SessionManager.Instance.MetaData.Globals.encryptPasswords) && (!bLDAP))
            {
                pc.Add("userPassword", DBUtils.makeSQLSafe(encryptedPassword));
            }
            else
            {
                pc.Add("userPassword", DBUtils.makeSQLSafe(passwordTextBox.Text));
            }
            // begin jdg 10/30/15
            pc.Add("bLDAP", (bLDAP ? 1 : 0));
            pc.Add("bLDAPSuccess", (bLDAPSuccess ? 1 : 0));
            // end jdg 10/30/15

            SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_Authenticate_User", pc);
            if (reader != null)
            {
                if (reader.Read())
                {
                    authenticated = (bool)reader.GetSqlBoolean(0);
                    numFailedAttempts = (int)reader.GetInt32(1);
                    numMaxFailures = (int)reader.GetInt32(2);
                    lockoutPeriod = (int)reader.GetInt32(3);
                }
                reader.Close();                        
            }
            if ((!validUser) || (!authenticated))  //note that if they're not a valid user they won't be authenticated, but we've updated the failed count and timeout values
            {
                if (lockoutPeriod > 0)
                    lockoutMsg = "\r\nLogin attempts will be blocked for " + lockoutPeriod.ToString() + " minute" + ((lockoutPeriod > 1) ? "s." : ".");
                else
                    lockoutMsg = "\r\nYou have made " + numFailedAttempts.ToString() + " failed Login attempt" + ((numFailedAttempts > 1) ? "s" : "") + " of a maximum " + numMaxFailures.ToString() + " allowed.";
            }
                    
            if (validUser && authenticated)
            {
                //see if user is forced to change password
                if (!bLDAP) // jdg 10/30/15
                {
                    if (ApplicationUtils.checkPasswordForceChange(u.userLogin))
                    {
                        String username = usernameTextBox.Text;
                        SessionManager.Instance.MetaData.Users.BackgroundListLoad();
                        passwordTextBox.Text = "";
                        usernameTextBox.Text = username;
                        this.DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }

                    if (ApplicationUtils.checkPasswordDateOK(u.userLogin) == false)
                    {
                        String username = usernameTextBox.Text;
                        SessionManager.Instance.MetaData.Users.BackgroundListLoad();
                        passwordTextBox.Text = "";
                        usernameTextBox.Text = username;
                        this.DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }
                }
                roleID = RiskAppCore.User.fetchUserRoleID(u.userLogin);
                roleName = RiskAppCore.User.fetchUserRoleName(u.userLogin);

                switch (roleName)
                {
                    case "Tablet":
                        RiskAppCore.ErrorMessages.Show(RiskAppCore.ErrorMessages.ROLE_ACCESS_DENIED);
                        return;

                    default:
                        break;
                }
                SessionManager.Instance.ActiveUser = u;
                InitUserGUIPrefs(u);
                u.UserClinicList.user_login = u.userLogin;
                u.UserClinicList.AddHandlersWithLoad(null, UserClinicListLoaded, null);
                //DialogResult = DialogResult.OK;

                HraObject.AuditUserLogin(u.userLogin);

                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;
                if (ts.TotalSeconds < requiredSplashTime)
                {
                    progressBar1.Style = ProgressBarStyle.Blocks;
                    progressBar1.Value = progressBar1.Maximum;
                    progressBar1.Refresh();
                    Application.DoEvents();
                    Thread.Sleep((int)(1000 * (requiredSplashTime - ts.TotalSeconds)));
                }

                return;
            }

            if (numFailedAttempts == 1)
            {
                MessageBox.Show(
                    "You have provided an incorrect username or password.\r\nPlease correct your password or try a different user." + lockoutMsg,
                    "Incorrect Username/Password",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
            else if (numFailedAttempts > 1)
            {
                MessageBox.Show(
                    lockoutMsg,
                    "Incorrect Username/Password",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
            return;
        }

        private static void InitUserGUIPrefs(User u)
        {
            SessionManager.Instance.MetaData.CurrentUserDefaultPedigreePrefs = new RiskApps3.Model.PatientRecord.GUIPreference(u);
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {
        }
        /**************************************************************************************************/
        private void UserClinicListLoaded(HraListLoadedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
