using HRA4.Context;
using HRA4.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
                int v2 = Id ?? default(int);
                //_applicationContext = new ApplicationContext();
                 apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(v2);
                return View(apps);
            }
            // return View(apps);
            return RedirectToAction("ManageInstitution","Admin");

            
        }
        
        public JsonResult FilteredInstitution(string name,string dob,string appdt)
        {
             
            int institutionid= (int)Session["InstitutionId"];
            _applicationContext = new ApplicationContext();
            var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(institutionid).Where(a => a.PatientName.Trim().ToLower().Contains(name.Trim().ToLower()));

            if(dob.Trim().Length > 0)
            apps = apps.Where(a => a.DateOfBirth.ToString().Trim().Contains(dob.Trim()));

            if (appdt.Trim().Length > 0)
                apps = apps.Where(a => a.AppointmentDate.ToString().Trim().Contains(appdt.Trim()));

            
            var view = RenderPartialView("_InstitutionGrid", apps);

            var result = new { view = view };

            return Json(result, JsonRequestBehavior.AllowGet);
            

        }

        public ActionResult MarkAsComplete(int Id)
        {
            return View();
        }
      

        protected virtual string RenderPartialView(string partialViewName, object model)
        {
            if (ControllerContext == null)
                return string.Empty;

            if (model == null)
                throw new ArgumentNullException("model");

            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentNullException("partialViewName");

            ModelState.Clear();//Remove possible model binding error.

            ViewData.Model = model;//Set the model to the partial view

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, partialViewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }



    }
}