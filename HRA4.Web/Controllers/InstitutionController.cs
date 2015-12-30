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
        public ActionResult InstitutionDashboard(int? InstitutionId)
        {
            List<ViewModels.Appointment> apps = new List<ViewModels.Appointment>();
            var instList = _applicationContext.ServiceContext.AdminService.GetTenants();
            ViewBag.instListcount = instList.Count;
            if (instList.Count == 0)
            {
                return View(apps);
            }
            if (InstitutionId != null && InstitutionId > 0)
            {
                Session.Add("InstitutionId", InstitutionId);
                int v2 = InstitutionId ?? default(int);
                //_applicationContext = new ApplicationContext();
                 apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(v2);
                ViewBag.AppointmentCount = apps.Count();
                return View(apps);
            }
            // return View(apps);
            return RedirectToAction("ManageInstitution", "Admin");

            
        }
        [HttpPost]
        public ActionResult InstitutionDashboard(FormCollection frm, bool MarkAsComplete)
        {
            HRA4.ViewModels.Appointment app = new ViewModels.Appointment();
            app.Id = Convert.ToInt32(frm["Id"]);
            app.MRN = Convert.ToString(frm["MRN"]);
            app.SetMarkAsComplete = MarkAsComplete;
            
            _applicationContext.ServiceContext.AppointmentService.SaveAppointments(app, Convert.ToInt32(Session["InstitutionId"]));

           // return View("InstitutionDashboard/" + Session["InstitutionId"]);
            return RedirectToAction("InstitutionDashboard",new {InstitutionId= Session["InstitutionId"]});
        }
        
        public JsonResult FilteredInstitution(string name, string dob, string appdt)
        {
             //Session.Add("InstitutionId", 1);
            string view = string.Empty;

            if (Session != null && Session["InstitutionId"] != null)
            {

            int instId = (int)Session["InstitutionId"];

                var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(instId).ToList();


              /*
              for (int i = 0; i < apps.Count; i++)
              {

                  DateTime bdt = apps[i].DateOfBirth;
                  DateTime apt = apps[i].AppointmentDate;
                  bdt = bdt.Date;
                  apt = apt.Date;
                 string bdts = bdt.Date.ToString("MM/dd/yyyy");
                 string apts = apt.Date.ToString("MM/dd/yyyy"); ;



              }
                */
            
   
                if (name.Length > 0)
                {
                 apps = apps.Where(a => a.PatientName.Trim().ToLower().Contains(name.Trim().ToLower())).ToList();                
                }
                
                if (dob.Trim().Length > 0)
                {
                    apps = apps.Where(a => a.DateOfBirth.Date.ToString("MM/dd/yyyy").Trim().Contains(dob.Trim())).ToList();
                }

                if (appdt.ToString().Trim().Length > 0)
                {
                    apps = apps.Where(a => a.AppointmentDate.Date.ToString("MM/dd/yyyy").Trim().Contains(appdt.Trim())).ToList();

                }

                view = RenderPartialView("_InstitutionGrid", apps);



            }
        
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