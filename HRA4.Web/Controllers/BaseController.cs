using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Context;
namespace HRA4.Web.Controllers
{
    public class BaseController : Controller
    {
        public IApplicationContext _applicationContext;


        public BaseController()
        {           
            if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["ApplicationContext"] == null)
            {
                _applicationContext = new ApplicationContext();
                System.Web.HttpContext.Current.Session["ApplicationContext"] = _applicationContext;
            }
            else
                _applicationContext = System.Web.HttpContext.Current.Session["ApplicationContext"] as ApplicationContext;
        }
    }
}