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
            List<ViewModels.Appointment> apps=null;
            if(Id!= null && Id>0)
            {
                Session.Add("InstitutionId", Id);                 
                 apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments();
                return View(apps);
            }

            return View(apps);

            
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