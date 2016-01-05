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
//using HRA4.Repositories.Interfaces;
using HRA4.Utilities;

using VM = HRA4.ViewModels;
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
        public ActionResult ImportAsXml(HttpPostedFileBase file, string mrn, int apptId, bool deIdentified)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/RAFiles/Upload"), fileName);
                    file.SaveAs(path);
                    VM.HraXmlFile xmlFile = new VM.HraXmlFile()
                    {
                        FileName = fileName,
                        FilePath = path,
                    };
                    _applicationContext.ServiceContext.AppointmentService.ImportXml(xmlFile, mrn, apptId);
                }
                ViewBag.Message = "Upload successful";


                return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("InstitutionDashboard");
        }
        }

        [HttpPost]
        public ActionResult ImportAsHL7(HttpPostedFileBase file, string mrn, int apptId, bool deIdentified)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/RAFiles/Upload"), fileName);
                    file.SaveAs(path);
                    VM.HraXmlFile xmlFile = new VM.HraXmlFile()
                    {
                        FileName = fileName,
                        FilePath = path,                        
                    };
                    _applicationContext.ServiceContext.AppointmentService.ImportHL7(xmlFile, mrn, apptId);
                }
                ViewBag.Message = "Upload successful";
               

                return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("InstitutionDashboard");
            }
        }

        public FileContentResult ExportAsHL7(FormCollection frm,string mrn, int apptId, bool identified)
        {           
            VM.HraXmlFile xmlFile = _applicationContext.ServiceContext.AppointmentService.ExportAsHL7(mrn, apptId, identified);            
            byte[] fileBytes = System.IO.File.ReadAllBytes(xmlFile.FilePath);
            string fileName = string.Format("{0}{1}", xmlFile.FileName, xmlFile.Estension);
            return File(fileBytes, "text/xml, application/xml", fileName);
        }

        public FileContentResult ExportAsXml(FormCollection frm,string mrn, int apptId, bool identified)
        {
            VM.HraXmlFile xmlFile = _applicationContext.ServiceContext.AppointmentService.ExportAsXml(mrn, apptId, identified);

            byte[] fileBytes = System.IO.File.ReadAllBytes(xmlFile.FilePath);
            string fileName = string.Format("{0}{1}", xmlFile.FileName, xmlFile.Estension);

            return File(fileBytes, "text/xml, application/xml", fileName);
        }

        [HttpPost]
        public ActionResult InstitutionDashboard(FormCollection frm, bool MarkAsComplete)
        {
            HRA4.ViewModels.Appointment app = new ViewModels.Appointment();
            app.Id = Convert.ToInt32(frm["Id"]);
            app.MRN = Convert.ToString(frm["MRN"]);
            app.SetMarkAsComplete = MarkAsComplete;
            _applicationContext.ServiceContext.AppointmentService.SaveAppointments(app, Convert.ToInt32(Session["InstitutionId"]));
            return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });
        }




        public JsonResult AddRemoveTask(string name, string appdt, string isDNC, string unitnum, string apptid)
        {



            string view = string.Empty;


            if (Session != null && Session["InstitutionId"] != null)
            {
                int instId = (int)Session["InstitutionId"];


                if (isDNC.Trim().Length > 0)
                {

                    if (isDNC.Trim().ToLower() == "True".ToLower())
                    {

                        _applicationContext.ServiceContext.AppointmentService.DeleteTasks(instId, unitnum, Convert.ToInt32(apptid));
                    }
                    else
                    {

                        _applicationContext.ServiceContext.AppointmentService.AddTasks(instId, unitnum, Convert.ToInt32(apptid));


                    }


                }


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



    }
}