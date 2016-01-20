using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Context;
using HRA4.Entities;
using VM = HRA4.ViewModels;
namespace HRA4.Web.Controllers
{
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
            return View("AuditReports", models);  
                                                        
        }
    }
}