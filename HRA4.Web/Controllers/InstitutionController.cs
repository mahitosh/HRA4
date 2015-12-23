using HRA4.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRA4.Web.Controllers
{
    public class InstitutionController : BaseController
    {
        // GET: Institution
        public ActionResult InstitutionDashboard(int Id)
        {
            Session.Add("InstitutionId", Id);
            _applicationContext = new ApplicationContext();
            var apps =_applicationContext.ServiceContext.AppointmentService.GetAppointments();

            return View(apps);
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
    }
}