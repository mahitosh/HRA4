using HRA4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRA4.Web.Controllers
{
    public class DocumentEditorController : Controller
    {
        // GET: DocumentEditor
        public ActionResult Index(TinyMCEModelJQuery model )
        {
            return View();
        }
    }
}