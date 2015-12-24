using HRA4.Context;
using HRA4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRA4.Web.Controllers
{
    public class InstitutionController : BaseController
    {      

        // GET: Institution
        public ActionResult InstitutionDashboard(int? Id)
        {
            List<ViewModels.Appointment> apps = new List<ViewModels.Appointment>();
            var instList = _applicationContext.ServiceContext.AdminService.GetTenants();
            ViewBag.instListcount = instList.Count;
            if(Id!= null && Id>0)
            {
                Session.Add("InstitutionId", Id);
                int v2 = Id ?? default(int);
                //_applicationContext = new ApplicationContext();
                 apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(v2);
                return View(apps);
            }
            return View(apps);
            //return RedirectToAction("ManageInstitution","Admin");

            
        }
        [HttpPost]
        public ActionResult FilteredInstitution(string name,string dob,string appdt)
        {

            return View();

        }

        public ActionResult MarkAsComplete(int Id)
        {
            return View();
        }

        public ActionResult ManageInstitution()
        {
            return View();
        }

        

    }
}