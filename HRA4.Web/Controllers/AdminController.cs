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
        //Authenticate the Admin user.
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(User user, string action, string returnUrl)
        {
            if (action == "Submit")
            {
                bool result = false;
                if (ModelState.IsValid)
                {
                    result = _applicationContext.ServiceContext.AdminService.Login(user.Username, user.Password);
                    Session["Username"] = _applicationContext.ServiceContext.AdminService.GetUserName();
                    if ( user.Username == null || user.Password == null)
                    {
                        ModelState.AddModelError("", "Please enter Username / Password !");
                        ViewBag.msg = "Error";
                        return View();
                    }
                    if (result)
                    {
                        System.Web.HttpContext.Current.Session["ApplicationContext"] = null;
                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                            return RedirectToAction("ManageInstitution", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username / Password !");
                        ViewBag.msg = "Error";
                        return View();
                    }
                }
            }
            else
            {
                ModelState.Clear();
                ViewBag.msg = null;
                return View();
            }
            return View();

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Admin");
        }
        //End by Aditya
        public ActionResult Institution(string tenant)
        {
            return View();
        }


        public ActionResult ManageInstitution()
        {
            ViewBag.SearchText = "";
            var instituionList = _applicationContext.ServiceContext.AdminService.GetTenants().Where(a => a.IsActive == true).ToList();
            AssignRecordStatus(instituionList);
            return View(instituionList);
        }

        public void AssignRecordStatus(List<Institution> instituionList)
        {
            ViewBag.RecordStatus = "";
            if (instituionList.Count == 0)
            {
                ViewBag.RecordStatus = "No records found.";
            }

        }

        public ActionResult SearchInstitution(string Institution)
        {
           
            ViewBag.SearchText = Institution;
            var instituionList = _applicationContext.ServiceContext.AdminService.GetTenants().Where(a => a.InstitutionName.ToString().Contains(Institution.Trim()) && a.IsActive==true ).ToList();
            AssignRecordStatus(instituionList);
            return View("ManageInstitution", instituionList);

        }
        public ActionResult DeleteInstitution(int Id)
        {
            //Institution institution = new Entities.Institution();
            //List<Institution> institution = new List<Entities.Institution>();
            _applicationContext.ServiceContext.AdminService.UpdateTenantById(Id);
            return RedirectToAction("ManageInstitution");
        }



        public ActionResult AddInstitution()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string instName)
        {
            //Save institution details in RiskAppDb and set the id. This is not possible now as there is dependency to old dlls.
            // string instName = "test";
            Institution institution = new Entities.Institution()
        {
            InstitutionName = instName,
            DateCreated = DateTime.Now,
        };


            string scriptPath = HttpContext.Server.MapPath(@"~/App_Data/HRATenantDBCreation.sql");

            institution = _applicationContext.ServiceContext.AdminService.AddUpdateTenant(institution);
            _applicationContext.ServiceContext.AdminService.CreateTenantDb(institution, scriptPath);
          //  Task taskA = Task.Run(() => _applicationContext.ServiceContext.AdminService.CreateTenantDb(institution,scriptPath));
            return RedirectToAction("ManageInstitution");
        }

        public ActionResult ShowError()
        {
            return View();
        }
    }
}