using HRA4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using HRA4.ViewModels;
using HRA4.Services;
using System.Web.Security;

namespace HRA4.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        //Added by Aditya on 21-12-2015
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(User user,string action)
        {
            if (action == "Submit")
            {
                bool result = false;
                result= _applicationContext.ServiceContext.AdminService.Login(user.Username, user.Password);
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login Attempt !");
                    ViewBag.msg = "Error";
                    return View();
                }
            }
            else
            {
                ModelState.Clear();
                ViewBag.msg = null;
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Admin");
        }
        //End by Aditya
        public ActionResult Institution(string tenant)
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            
            var instituionList = _applicationContext.ServiceContext.AdminService.GetTenants();
           
            return View(instituionList);
        }

        public ActionResult AddInstitution()
        {
            return View();
        }

         [HttpPost]         
        public ActionResult Create(Institution instituion)
        {
            var institution = _applicationContext.ServiceContext.AdminService.AddUpdateTenant(instituion);

            Task taskA = Task.Run(() => _applicationContext.ServiceContext.AdminService.CreateTenantDb(institution));
            return RedirectToAction("Dashboard");
        }
    }
}