using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Context;
using log4net;
using HRA4.ViewModels;
using HRA4.Entities.UserManagement;
namespace HRA4.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public IApplicationContext _applicationContext;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaseController));

        public BaseController()
        {           
            if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["ApplicationContext"] == null)
            {
                _applicationContext = new ApplicationContext();
                System.Web.HttpContext.Current.Session["ApplicationContext"] = _applicationContext;
               // ViewBag.MenuList = CreateMenu();
            }
            else
                _applicationContext = System.Web.HttpContext.Current.Session["ApplicationContext"] as ApplicationContext;
            
        }
     

        

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            Logger.ErrorFormat("Controller: {0}", filterContext.RouteData.Values["controller"].ToString());
            Logger.ErrorFormat("Action: {0}", filterContext.RouteData.Values["action"].ToString());
            Logger.Error(filterContext.Exception);
            var model = new HandleErrorInfo(filterContext.Exception,
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString());
             
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Error/CustomError.cshtml",
                ViewData = new ViewDataDictionary(model)
            };
             
        }
    }
}