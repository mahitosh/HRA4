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
using HRA4.Entities.UserManagement;
using HRA4.Web.Filters;
using HRA4.Context;

namespace HRA4.Web.Controllers
{
    [CustomAuthorize(Roles="SuperAdmin")]
    public class AdminController : BaseController
    {
        //public AdminController()
        //{

        //}
        // GET: Admin
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {            
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        //Added by Aditya on 21-12-2015
        //Authenticate the Admin user.
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(User user, string action, string ReturnUrl)
        {
            if (action == "Submit")
            {
                bool result = false;
                string msg = string.Empty;
                string fullName = string.Empty;
                if (ModelState.IsValid)
                {
                    Entities.UserManagement.SampleData entities = new SampleData();
                  
                   // result = entities.SUsers.Any(u => u.Username == user.Username);
                    
                    if ( user.Username == null || user.Password == null)
                    {
                        ModelState.AddModelError("", "Please enter Username / Password !");
                        ViewBag.msg = "Error";
                        return View();
                    }
                    if (user.Username == "sadmin")
                    {
                        result = _applicationContext.ServiceContext.AdminService.Login(user.Username, user.Password);
                        fullName = _applicationContext.ServiceContext.AdminService.GetUserName();
                    }
                    else
                    {
                        if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                        && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                        {
                            string InstitutionId = ReturnUrl.Split('=')[1];
                            Session.Add("InstitutionId", InstitutionId);
                            //ReInitializing Application Context with Institution Details.
                            System.Web.HttpContext.Current.Session["ApplicationContext"] = null;
                            _applicationContext = new ApplicationContext();
                            System.Web.HttpContext.Current.Session["ApplicationContext"] = _applicationContext;

                            result = _applicationContext.ServiceContext.UserService.AuthenticateUser(user.Username, user.Password, out msg, out fullName);
                        }
                       
                       
                    }
                    if (result)
                    {
                        Session["Username"] = fullName;
                        System.Web.HttpContext.Current.Session["ApplicationContext"] = null;
                        Session["InstitutionId"] = null;
                        FormsAuthentication.SetAuthCookie(user.Username, false);

                        if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                        && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("ManageInstitution", "Admin");
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(msg))
                            msg = "Invalid username / Password !";
                        ModelState.AddModelError("", msg);
                        ViewBag.msg = "Error";
                        string tmp = System.Web.HttpContext.Current.Request.RawUrl;
                       // RedirectToAction("Index", "Admin", new { ReturnUrl = ReturnUrl });
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

       
        //End by Aditya
        public ActionResult Institution(string tenant)
        {
            return View();
        }

         
        public ActionResult ManageInstitution()
        {
            ViewBag.SearchText = "";
            ViewBag.MenuList = new List<Menu>();
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
            var instituionList = _applicationContext.ServiceContext.AdminService.GetTenants().Where(a => a.InstitutionName.ToString().ToLower().Contains(Institution.ToLower().Trim()) && a.IsActive==true ).ToList();
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
            IsActive=true
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