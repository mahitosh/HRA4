using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HRA4.Context;
using System.IO;
using log4net;
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

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Logger.Error(exception);
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
