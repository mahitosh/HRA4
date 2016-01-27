using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.Security.Principal;

namespace RiskApps3.Utilities
{
    class RegistrationService
    {
        public static void logUserAccess()
        {
            NameValueCollection values = Configurator.GetConfig("globals");
            string APIServiceActive = values["APIServiceActive"] ?? "False";

            if (APIServiceActive != "True")
            {
                //don't do anything
                return;
            }

            //log the user, client, institution, machine name, ip address, etc. to the Registration Server database
            //by doing a RESTful POST with all relevant info
            string APIPostUserLogURL = values["APIPostUserLogURL"] ?? "None";
            if (APIPostUserLogURL == "None") return;

            try
            {
                using (TimedWebClient client = new TimedWebClient(5000))  //wait at most 5 seconds for a response
                {
                    System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                    RiskApps3.Model.MetaData.User u = RiskApps3.Controllers.SessionManager.Instance.ActiveUser;
                    reqparm.Add("user_login", u.User_userLogin);
                    reqparm.Add("user_fullname", u.User_userFullName);
                    reqparm.Add("ntUser", getNTUser());
                    reqparm.Add("machine_name", Environment.MachineName);
                    reqparm.Add("os_version", Environment.OSVersion.ToString());
                    //Add other desired params here
                    //  even something like this works:
                    //    reqparm.Add("param1", "<any> kinds & of = ? strings");
                    //
                    //params must be compatible with node service code (currently 'hello.js'), sp_3_Insert_UserLog, and tblUserLog

                    byte[] responsebytes = client.UploadValues(APIPostUserLogURL, "POST", reqparm);
                    string responsebody = Encoding.UTF8.GetString(responsebytes);

                    //Console.WriteLine(responsebody);
                    //TODO: Verify response is as expected
                    if (!responsebody.Contains("HRA registration server got your data!"))
                    {
                        Logger.Instance.WriteToLog("[RegistrationService] at URL " + APIPostUserLogURL + " did not send the standard expected response.");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("[RegistrationService] could not connect to URL " + APIPostUserLogURL + ":\n\t" + e.Message);
                return;
            }
        }

        static string getNTUser()
        {
            WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            string strTemp = wp.Identity.Name;
            int intPos = strTemp.IndexOf("\\", 0);
            string ntUser = strTemp.Substring(intPos + 1);
            return ntUser;
        }
    }
}
