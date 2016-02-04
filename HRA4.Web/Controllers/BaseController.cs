using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Context;
using log4net;
using HRA4.ViewModels;
using HRA4.Entities.UserManagement;
using HRA4.Entities;
using HRA4.Web.Filters;
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
                
            }
            else
                _applicationContext = System.Web.HttpContext.Current.Session["ApplicationContext"] as ApplicationContext;
            
            SetMenus();
        }

        private void SetMenus()
        {
            SampleData entities = new SampleData();

            if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var cuser = System.Web.HttpContext.Current.User;
                if(!string.IsNullOrWhiteSpace(cuser.Identity.Name))
                {
                    var roleId = _applicationContext.ServiceContext.UserService.GetRoleId(cuser.Identity.Name);
                    ViewBag.MenuList = _applicationContext.ServiceContext.UserService.GetMenus(roleId);
                    ViewBag.ExcludeIds = _applicationContext.ServiceContext.UserService.GetExcludeControlIds(roleId);
                }
                    
        }
     

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