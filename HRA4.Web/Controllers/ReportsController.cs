using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Context;
using HRA4.Entities;
using VM = HRA4.ViewModels;
using HRA4.Web.Filters;
namespace HRA4.Web.Controllers
{
    [CustomAuthorize(Roles = "SuperAdmin,Administrator")] 
    public class ReportsController : BaseController
    {
        // GET: Reports
        public ActionResult AuditReports(VM.AuditReports models)
        {
             return View(models);
        }
        [HttpPost]
        public ActionResult GetReports(FormCollection frm)
        {
            var MRN = Convert.ToString(frm["txtMRNForReport"]);
            var models = _applicationContext.ServiceContext.AppointmentService.GetAuditReports(MRN, "1/19/1950 8:05:21 PM", DateTime.Now.ToString());
            ViewBag.PostedMRN=MRN;
            if(models.RSAuditMrnAccessV2Entry.Count>0)
            ViewBag.RecordStatusV2 ="";
            else
                ViewBag.RecordStatusV2 = "No Records Found";
            if (models.RSAuditMrnAccessV3Entry.Count>0)
            ViewBag.RecordStatusV3 = "";
            else
                ViewBag.RecordStatusV3 = "No Records Found";
            return View("AuditReports", models);  
                                                        
        }
    }
}