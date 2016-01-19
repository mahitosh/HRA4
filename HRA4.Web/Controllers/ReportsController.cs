using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Context;
using HRA4.Entities;

namespace HRA4.Web.Controllers
{
    public class ReportsController : BaseController
    {
        // GET: Reports
        public ActionResult AuditReports()
        {
            return View();
        }
        public ActionResult GetReports(string MRN)
        {
            var model=_applicationContext.ServiceContext.AppointmentService.GetAuditReports(MRN,DateTime.Now.ToString(),DateTime.Now.ToString());
            
            return View();
        }
    }
}