using HRA4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace HRA4.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

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