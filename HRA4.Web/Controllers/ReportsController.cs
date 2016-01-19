using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRA4.Web.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult AuditReports()
        {
            return View();
        }
    }
}