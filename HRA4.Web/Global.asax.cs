using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HRA4.Context;
using System.IO;
using log4net;
using System.Web.Security;
using HRA4.Entities.UserManagement;
namespace HRA4.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }

        protected void Application_Disposed(object sender, EventArgs e)
        {
            //FormsAuthentication.SignOut();
            //Session.Abandon();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Logger.Error(exception);
            FormsAuthentication.SignOut();
            Session.Abandon();
            switch(exception.GetType().ToString())
            {
                case "System.Web.HttpException":
                    var httpException = exception as HttpException;
                    int code = httpException.GetHttpCode();
                    RedirectToPage(code);                    
                    break;
                default:
                    RedirectToPage(-1);
                    break;
            }
           
            Server.ClearError();
            
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            //if (FormsAuthentication.CookiesSupported == true)
            //{
            //    if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            //    {
            //        try
            //        {
            //            //let us take out the username now                
            //            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            //            string roles = string.Empty;

            //            SampleData entities = new SampleData();

            //            SUser user = entities.SUsers.SingleOrDefault(u => u.Username == username);

            //                roles = user.Role;
                       
            //            //let us extract the roles from our own custom cookie


            //            //Let us set the Pricipal with our user specific details
            //            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
            //        }
            //        catch (Exception)
            //        {
            //            //somehting went wrong
            //        }
            //    }
            //}
        }

        private void RedirectToPage(int code)
        {
            switch(code)
            {
                case 404:
                    Response.Redirect("/Error/PageNotFound");
                    break;
                default:
                    Response.Redirect("/Error/BadRequest");
                    break;
            }
        }
    }
}
