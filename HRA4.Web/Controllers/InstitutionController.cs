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
using HRA4.ViewModels;
using System.Configuration;

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
                if (Session["InstitutionId"] == null || Session["InstitutionId"].ToString() != InstitutionId.ToString())
                {
                Session.Add("InstitutionId", InstitutionId);
                    //ReInitializing Application Context with Institution Details.
                    System.Web.HttpContext.Current.Session["ApplicationContext"] = null;
                    _applicationContext = new ApplicationContext();
                    System.Web.HttpContext.Current.Session["ApplicationContext"] = _applicationContext;
                    
                }
                
                int v2 = InstitutionId ?? default(int);
                NameValueCollection searchfilter = new NameValueCollection();
                searchfilter.Add("name", null);
                searchfilter.Add("appdt", DateTime.Now.ToString("MM/dd/yyyy"));
                searchfilter.Add("clinicId", "-1");
                apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(v2, searchfilter);
                ViewBag.AppointmentCount = apps.Count();
                ViewBag.RecordStatus = "";
                ViewBag.TodaysDate = DateTime.Now.ToString("MM/dd/yyyy");
                if(apps.Count==0)
                {
                    ViewBag.RecordStatus = "No records found.";
                }
                
                /*=======Start Load Clinic Dropdown======================*/
                var _ClinicList = _applicationContext.ServiceContext.AppointmentService.GetClinics((int)Session["InstitutionId"]);
                ViewBag.ClinicList = new SelectList(_ClinicList.ToList(),"clinicID","clinicName");

                /*=======End Load Clinic Dropdown======================*/

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
                    var path = Path.Combine(Server.MapPath(Constants.RAFilePath),"Upload", fileName);
                    file.SaveAs(path);
                    VM.HraXmlFile xmlFile = new VM.HraXmlFile()
                    {
                        FileName = fileName,
                        FilePath = path,
                    };
                    _applicationContext.ServiceContext.ExportImportService.ImportXml(xmlFile, mrn, apptId);
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
                    var path = Path.Combine(Server.MapPath(Constants.RAFilePath), "Upload", fileName);
                    file.SaveAs(path);
                    VM.HraXmlFile xmlFile = new VM.HraXmlFile()
                    {
                        FileName = fileName,
                        FilePath = path,                        
                    };
                    _applicationContext.ServiceContext.ExportImportService.ImportHL7(xmlFile, mrn, apptId);
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
            VM.HraXmlFile xmlFile = _applicationContext.ServiceContext.ExportImportService.ExportAsHL7(mrn, apptId, identified);            
            byte[] fileBytes = System.IO.File.ReadAllBytes(xmlFile.FilePath);
            string fileName = string.Format("{0}{1}", xmlFile.FileName, xmlFile.Estension);
            return File(fileBytes, "text/xml, application/xml", fileName);
        }

        public FileContentResult ExportAsXml(FormCollection frm,string mrn, int apptId, bool identified)
        {
            VM.HraXmlFile xmlFile = _applicationContext.ServiceContext.ExportImportService.ExportAsXml(mrn, apptId, identified);

            byte[] fileBytes = System.IO.File.ReadAllBytes(xmlFile.FilePath);
            string fileName = string.Format("{0}{1}", xmlFile.FileName, xmlFile.Estension);

            return File(fileBytes, "text/xml, application/xml", fileName);
        }

        [HttpPost]
        public ActionResult InstitutionSave(FormCollection frm, bool MarkAsComplete, string Hfddlclinic)
        {
            HRA4.ViewModels.Appointment app = new ViewModels.Appointment();
            app.Id = Convert.ToInt32(frm["Id"]);
            app.MRN = Convert.ToString(frm["MRN"]);
            //app.DateOfBirth = Convert.ToDateTime(frm["dob-date"]);
            //app.PatientName = Convert.ToString(frm["PatientName"]);
            //app.Survey = Convert.ToString(frm["Survey"]);
            //app.appttime = Convert.ToString(frm["TimeDropdown"]);
            if (Hfddlclinic!=null && Hfddlclinic!="")
            app.clinicID = Convert.ToInt32(Hfddlclinic);
            else app.clinicID = -1;
            app.SetMarkAsComplete = MarkAsComplete;
            _applicationContext.ServiceContext.AppointmentService.SaveAppointments(app, Convert.ToInt32(Session["InstitutionId"]));
            return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });
        }


        public JsonResult ShowPedigreeImage(string unitnum, string apptid)
         {

             string PedigreeImagePath = ConfigurationManager.AppSettings["PedigreeImagePath"].ToString();
             int _institutionId = (int)Session["InstitutionId"];
             //PedigreeImagePath = Url.Content(PedigreeImagePath + "ImageName.jpg");
             string PedigreeImageSavePath = Server.MapPath(PedigreeImagePath);
             string _ImageUrl = String.Empty;
            _ImageUrl = _applicationContext.ServiceContext.AppointmentService.ShowPedigreeImage(_institutionId, unitnum, Convert.ToInt32(apptid), PedigreeImageSavePath);
             //PedigreeImagePath = Url.Content(PedigreeImagePath + _ImageUrl);
            PedigreeImagePath = @"data:image/png;base64,"+_ImageUrl+"";
            var result = new { ImageUrl = PedigreeImagePath};

            return Json(result);

        }



        public JsonResult AddRemoveTask(string name, string appdt, string isDNC, string unitnum, string apptid, string clinicId)
        {

            string view = string.Empty;
            int apps_count = 0;

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
                NameValueCollection searchfilter;
                if(Session["SearchFilter"] != null)
                {
                    searchfilter = (NameValueCollection)Session[Constants.SearchFilter];
                }
                else
                {
                    searchfilter = new NameValueCollection();
                    searchfilter.Add("name", name);
                    searchfilter.Add("appdt", appdt);
                    searchfilter.Add("clinicId", clinicId);
                }
           
                var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(instId, searchfilter).ToList();
                apps_count = apps.Count();
                view = RenderPartialView("_InstitutionGrid", apps);

            }
            var result = new { view = view, apps_count = apps_count };
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        public JsonResult FilteredInstitution(string name, string appdt, string clinicId)
        {
            
            string view = string.Empty;
            int apps_count=0;
        
            if (Session != null && Session["InstitutionId"] != null)
            {
                int instId = (int)Session["InstitutionId"];

                NameValueCollection searchfilter = new NameValueCollection();
                searchfilter.Add("name", name);
                searchfilter.Add("appdt", appdt);
                searchfilter.Add("clinicId", clinicId);
                Session[Constants.SearchFilter] = searchfilter;
                var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(instId, searchfilter).ToList();
                apps_count  = apps.Count();
                //ViewBag.AppointmentCount = apps.Count();
                view = RenderPartialView("_InstitutionGrid", apps);



            }
            var result = new { view = view, apps_count = apps_count, todaysDate = DateTime.Now.ToString("MM/dd/yyyy") };
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