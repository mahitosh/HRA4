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
using HRA4.Entities.UserManagement;
using System.Web.Security;
using HRA4.Web.Filters;
namespace HRA4.Web.Controllers
{
    [CustomAuthorize(Roles = "SuperAdmin,Administrator,Clinician")] 
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
                string InstName = _applicationContext.ServiceContext.AdminService.GetInstitutionName(v2);
                Session["InstitutionName"] = InstName;
                NameValueCollection searchfilter = new NameValueCollection();
                searchfilter.Add("name", null);
                searchfilter.Add("appdt", DateTime.Now.ToString("MM/dd/yyyy"));
                searchfilter.Add("clinicId", "-1");
                apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(v2, searchfilter);
                ViewBag.AppointmentCount = apps.Count();
                ViewBag.RecordStatus = "";
                ViewBag.TodaysDate = DateTime.Now.ToString("MM/dd/yyyy");

                ViewBag.LBCCount = _applicationContext.ServiceContext.RiskClinicServices.GetPatients("LBC").Count();
                ViewBag.BRCACount = _applicationContext.ServiceContext.RiskClinicServices.GetPatients("BRCA").Count(); 

                if (apps.Count == 0)
                {
                    ViewBag.RecordStatus = "No records found.";
                }

                /*=======Start Load Clinic Dropdown======================*/
                var _ClinicList = _applicationContext.ServiceContext.AppointmentService.GetClinics((int)Session["InstitutionId"]);
                ViewBag.ClinicList = new SelectList(_ClinicList.ToList(), "clinicID", "clinicName");
                
                /*=======End Load Clinic Dropdown======================*/
                return View(apps);
               

            }
            else if (Session["InstitutionId"] != null)
            {
                   InstitutionId = Convert.ToInt32(Session["InstitutionId"]);
                   return RedirectToAction("InstitutionDashboard", new { InstitutionId = InstitutionId });
            }

            return RedirectToAction("ManageInstitution", "Admin");    
            


        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public JsonResult ImportAsXml(HttpPostedFileBase file, string mrn, int apptId)
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
                    _applicationContext.ServiceContext.ExportImportService.ImportXml(xmlFile, mrn, apptId);
                }
                ViewBag.Message = "Upload successful";

                var result = new { view = "doc.." };
                return Json(result, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Upload failed";
                var result = new { view = "doc.." };
                return Json(result, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });
            }
        }

        [HttpPost]
        public JsonResult ImportAsHL7(HttpPostedFileBase file, string mrn, int apptId)
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

                var result = new { view = "doc.." };
                return Json(result, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Upload failed";
                // return RedirectToAction("InstitutionDashboard");
                var result = new { view = "doc.." };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public FileContentResult ExportAsHL7(FormCollection frm, string mrn, int apptId, bool identified)
        {
            VM.HraXmlFile xmlFile = _applicationContext.ServiceContext.ExportImportService.ExportAsHL7(mrn, apptId, identified);
            byte[] fileBytes = System.IO.File.ReadAllBytes(xmlFile.FilePath);
            string fileName = string.Format("{0}{1}", xmlFile.FileName, xmlFile.Estension);
            return File(fileBytes, "text/xml, application/xml", fileName);
        }

        public FileContentResult ExportAsXml(FormCollection frm, string mrn, int apptId, bool identified)
        {
            VM.HraXmlFile xmlFile = _applicationContext.ServiceContext.ExportImportService.ExportAsXml(mrn, apptId, identified);

            byte[] fileBytes = System.IO.File.ReadAllBytes(xmlFile.FilePath);
            string fileName = string.Format("{0}{1}", xmlFile.FileName, xmlFile.Estension);

            return File(fileBytes, "text/xml, application/xml", fileName);
        }

        [HttpPost]
        public JsonResult InstitutionSave(FormCollection frm, bool MarkAsComplete, string Hfddlclinic)
        {
            HRA4.ViewModels.Appointment app = new ViewModels.Appointment();
            app.Id = Convert.ToInt32(frm["Id"]);
            app.MRN = Convert.ToString(frm["MRN"]);
            app.DateOfBirth = Convert.ToDateTime(frm["dob-date"]);
            app.PatientName = Convert.ToString(frm["PatientName"]);
            app.Survey = Convert.ToString(frm["Survey"]);
            app.appttime = Convert.ToString(frm["ddlappttimes"]);
            app.Address1 = Convert.ToString(frm["Address1"]);
            app.Address2 = Convert.ToString(frm["Address2"]);
            app.AppointmentDate = Convert.ToDateTime(frm["edit-app-date"]);
            app.AppointmentPhysician = Convert.ToString(frm["ddlAppointmentPhysicians"]);
            app.AssessmentName = Convert.ToString(frm["Assessment"]);
            app.Cellphone = Convert.ToString(frm["Cellphone"]);
            app.City = Convert.ToString(frm["City"]);
            app.Country = Convert.ToString(frm["ddlCountries"]);
            //app.DateCompleted = Convert.ToDateTime(frm["dob-date"]);
            app.DiseaseHx = Convert.ToString(frm["DiseaseHx"]);
            app.Education = Convert.ToString(frm["Education"]);
            app.EmailAddress = Convert.ToString(frm["EmailAddress"]);
            app.Gender = Convert.ToString(frm["ddlGenders"]);
            app.Homephone = Convert.ToString(frm["Homephone"]);
            app.Language = Convert.ToString(frm["ddlLanguages"]);
            app.Maritalstatus = Convert.ToString(frm["Maritalstatus"]);
            app.Nationality = Convert.ToString(frm["ddlNationalities"]);
            app.Occupation = Convert.ToString(frm["Occupation"]);
            app.PCP = Convert.ToInt32(frm["ddlPCP"]);
            app.RefPhysician = Convert.ToInt32(frm["ddlRefPhysician"]);
            app.clinicID = Convert.ToInt32(frm["ddlclinics"]);
            app.Race = Convert.ToString(frm["ddlRaces"]);
            app.State = Convert.ToString(frm["ddlStates"]);
            app.Workphone = Convert.ToString(frm["Workphone"]);
            app.Zip = Convert.ToString(frm["Zip"]);

            //if (Hfddlclinic != null && Hfddlclinic != "")
            //    app.clinicID = Convert.ToInt32(Hfddlclinic);// verfiy 
            //else app.clinicID = -1;
            app.SetMarkAsComplete = MarkAsComplete;
            _applicationContext.ServiceContext.AppointmentService.SaveAppointments(app, Convert.ToInt32(Session["InstitutionId"]));
            // return RedirectToAction("InstitutionDashboard", new { InstitutionId = Session["InstitutionId"] });

            string view = string.Empty;
            NameValueCollection searchfilter = new NameValueCollection();
            searchfilter = GetSearchFilter(null, null, app.clinicID.ToString());
            int instId = 0;
            if (Session != null && Session["InstitutionId"] != null)
            {
                instId = (int)Session["InstitutionId"];
            }
            var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(instId, searchfilter).ToList();
            view = RenderPartialView("_InstitutionGrid", apps);
            var result = new { view = view };
            return Json(result, JsonRequestBehavior.AllowGet);
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
            PedigreeImagePath = @"data:image/png;base64," + _ImageUrl + "";
            var result = new { ImageUrl = PedigreeImagePath };

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
                NameValueCollection searchfilter = new NameValueCollection();
                searchfilter = GetSearchFilter(name, appdt, clinicId);

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
            int apps_count = 0;

            if (Session != null && Session["InstitutionId"] != null)
            {
                int instId = (int)Session["InstitutionId"];
                Session[Constants.SearchFilter] = null;
                NameValueCollection searchfilter = new NameValueCollection();
                searchfilter = GetSearchFilter(name, appdt, clinicId);
                var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(instId, searchfilter).ToList();
                apps_count = apps.Count();
                //ViewBag.AppointmentCount = apps.Count();
                view = RenderPartialView("_InstitutionGrid", apps);



            }
            var result = new { view = view, apps_count = apps_count, todaysDate = DateTime.Now.ToString("MM/dd/yyyy") };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ShowHtml(int templateId, string mrn, int apptId)
        {
            string view = string.Empty;
            var template = _applicationContext.ServiceContext.TemplateService.GenerateHtmlFromTemplate(templateId, mrn, apptId);
            view = RenderPartialView("_ShowHtmlDocument", template);
            var result = new { view = view };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NewDocument(string mrn, int apptid)
        {
            string view = string.Empty;
            var templates = _applicationContext.ServiceContext.TemplateService.GetTemplates();
            view = RenderPartialView("_NewDocument", templates);
            var result = new { view = view };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public FileStreamResult DownloadDocument(string filePath, string templateName, string id_mrn, int templateId)
        {
            string mrn = id_mrn.Split('-')[1];
            int apptId = Convert.ToInt32(id_mrn.Split('-')[0]);
            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                var fileStream = new MemoryStream(fileBytes);
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (Exception ex)
                {

                }
                return File(fileStream, "application/pdf", string.Format("{0}_{1}.pdf", mrn, templateName));
            }
            else
            {
                var template = _applicationContext.ServiceContext.TemplateService.GenerateHtmlFromTemplate(templateId, mrn, apptId);
                byte[] fileBytes = System.IO.File.ReadAllBytes(template.PdfFilePath);
                var fileStream = new MemoryStream(fileBytes);
                try
                {
                    System.IO.File.Delete(template.PdfFilePath);
                }
                catch (Exception ex)
                {

                }
                return File(fileStream, "application/pdf", string.Format("{0}_{1}.pdf", mrn, templateName));
            }
           
        }

        public JsonResult RiskCalculation(string MRN, int apptid, string status)
        {
            string view = string.Empty;
            var apps = (RiskScore)null;
            if (status == "Show")
                apps = _applicationContext.ServiceContext.AppointmentService.RiskScore(apptid, MRN);
            else
                apps = _applicationContext.ServiceContext.AppointmentService.RiskCalculateAndRunAutomation(apptid, MRN);
            view = RenderPartialView("_RiskScore", apps);
            var result = new { view = view };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAppoitmentForEdit(string apptid, string name, string appdt, string clinicId)
        {
            string view = string.Empty;
            if (Session != null && Session["InstitutionId"] != null)
            {
                int instId = (int)Session["InstitutionId"];
                NameValueCollection searchfilter = new NameValueCollection();
                searchfilter = GetSearchFilter(name, appdt, clinicId);
                var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointment(instId, searchfilter, apptid);
                apps.DisplayHeaderMenus = "Yes";
                view = RenderPartialView("_InstitutionRow", apps);

            }
           
            var result = new { view = view };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAppoitmentForAdd(string MRN, string clinicId)
        {
            string view = string.Empty;
            var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointmentForAdd(MRN,Convert.ToInt32(clinicId));
            apps.DisplayHeaderMenus = "No";
            view = RenderPartialView("_InstitutionRow", apps);
            var result = new { view = view};
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

        public JsonResult DeleteAppointment(int apptid,bool flag)
        {
            _applicationContext.ServiceContext.AppointmentService.DeleteAppointment(Convert.ToInt32(Session["InstitutionId"]), apptid,Convert.ToBoolean(flag));
            string view = string.Empty;
            int apps_count = 0;

            if (Session != null && Session["InstitutionId"] != null)
            {
                int instId = (int)Session["InstitutionId"];

                NameValueCollection searchfilter = new NameValueCollection();
                searchfilter = GetSearchFilter("", "", "");
                var apps = _applicationContext.ServiceContext.AppointmentService.GetAppointments(instId, searchfilter).ToList();
                apps_count = apps.Count();
                //ViewBag.AppointmentCount = apps.Count();
                view = RenderPartialView("_InstitutionGrid", apps);



            }
            var result = new { view = view, apps_count = apps_count, todaysDate = DateTime.Now.ToString("MM/dd/yyyy") };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private NameValueCollection GetSearchFilter(string name, string appdt, string clinicId)
        {
            NameValueCollection searchfilter;
            if (Session[Constants.SearchFilter] != null)
            {
                searchfilter = (NameValueCollection)Session[Constants.SearchFilter];
            }
            else
            {
                searchfilter = new NameValueCollection();
                searchfilter.Add("name", name);
                searchfilter.Add("appdt", appdt);
                if (!string.IsNullOrWhiteSpace(clinicId))
                    searchfilter.Add("clinicId", clinicId);
                else
                    searchfilter.Add("clinicId", "-1");
                Session[Constants.SearchFilter] = searchfilter;
            }
            return searchfilter;
        }

       
        public JsonResult RunAutomationDocuments(string apptid, string MRN)
        {

            FileInfo fileinfo = _applicationContext.ServiceContext.AppointmentService.RunAutomationDocuments(Convert.ToInt32(Session["InstitutionId"]), Convert.ToInt32(apptid), MRN);
            //var result = new { view = fileinfo };
            Session["FileInfo"] = fileinfo;

            var result = new { view = "doc.." };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public FileContentResult DownloadFile()
        {

            byte[] fileBytes = null;
            string fileName = string.Empty;
            if (Session["FileInfo"] != null)
            {
                FileInfo fileinfo = (FileInfo)Session["FileInfo"];
                fileBytes = System.IO.File.ReadAllBytes(fileinfo.FullName);
                fileName = string.Format("{0}{1}", fileinfo.Name, fileinfo.Extension);

                return File(fileBytes, "Application/pdf", fileName);
            }
            return File(new byte[0], "Application/pdf", "ErrorOccured"); 
        }

    }



}