using HRA4.Context;
using HRA4.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HRA4.ViewModels;

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
                NameValueCollection searchfilter = new NameValueCollection();
                searchfilter.Add("name", null);
                searchfilter.Add("appdt", DateTime.Now.ToString("MM/dd/yyyy"));
                apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(v2, searchfilter);
                ViewBag.AppointmentCount = apps.Count();
                return View(apps);
            }
            return RedirectToAction("ManageInstitution", "Admin");


        }
        [HttpPost]
        public ActionResult InstitutionDashboard(FormCollection frm, bool MarkAsComplete)
        {
            HRA4.ViewModels.Appointment app = new ViewModels.Appointment();
            app.Id = Convert.ToInt32(frm["Id"]);
            app.MRN = Convert.ToString(frm["MRN"]);
            //app.DateOfBirth = Convert.ToDateTime(frm["dob-date"]);
            //app.PatientName = Convert.ToString(frm["PatientName"]);
            //app.Survey = Convert.ToString(frm["Survey"]);
            //app.appttime = Convert.ToString(frm["TimeDropdown"]);
            //app.clinicID = Convert.ToInt32(frm["ClinicDropdown"]);
            app.SetMarkAsComplete = MarkAsComplete;
            _applicationContext.ServiceContext.AppointmentService.SaveAppointments(app, Convert.ToInt32(Session["InstitutionId"]));
            return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });
        }

        public JsonResult FilteredInstitution(string name, string appdt)
        {
            
            string view = string.Empty;
            if (Session != null && Session["InstitutionId"] != null)
            {
                int instId = (int)Session["InstitutionId"];
                NameValueCollection searchfilter = new NameValueCollection();
                searchfilter.Add("name", name);
                searchfilter.Add("appdt", appdt);
                var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(instId, searchfilter).ToList();
                ViewBag.AppointmentCount = apps.Count();
                view = RenderPartialView("_InstitutionGrid", apps);

            }
            var result = new { view = view };
            return Json(result, JsonRequestBehavior.AllowGet);

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

        public ActionResult DeleteAppointment(int apptid)
        {
            _applicationContext.ServiceContext.AppointmentService.DeleteAppointment(Convert.ToInt32(Session["InstitutionId"]),apptid);
            return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });
        }
      
        public JsonResult RunAutomationDocuments(string  apptid,string MRN)
        {
            FileInfo fileinfo=_applicationContext.ServiceContext.AppointmentService.RunAutomationDocuments(Convert.ToInt32(Session["InstitutionId"]),Convert.ToInt32(apptid),MRN);
            //var result = new { view = fileinfo };
            Session["FileInfo"] = fileinfo;
            var result = new { view = "doc.." };
            return Json(result, JsonRequestBehavior.AllowGet);
                      
        }
     
        public FileContentResult DownloadFile()
        {
            FileInfo fileinfo = (FileInfo)Session["FileInfo"];
            byte[] fileBytes = System.IO.File.ReadAllBytes(fileinfo.FullName);
            string fileName = string.Format("{0}{1}", fileinfo.Name, fileinfo.Extension);
            return File(fileBytes, "Application/pdf", fileName);
        }

    }



}