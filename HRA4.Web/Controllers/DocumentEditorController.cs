using HRA4.Entities;
using HRA4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Services;

namespace HRA4.Web.Controllers
{
    public class DocumentEditorController : BaseController
    {
        // GET: DocumentEditor
        public ActionResult Index(TinyMCEModelJQuery model )
        {

            model.Content = _applicationContext.ServiceContext.TemplateService.GetTemplate(0, 3).TemplateString;

            return View(model);
        }

        public ActionResult SaveTemplate(TinyMCEModelJQuery model)
        {
            string TemplateString = string.Empty;
            TemplateString = model.Content;
            _applicationContext.ServiceContext.TemplateService.UpdateTemplate(0, 3, TemplateString);
            return View("Index",model);
        }

    }
    



}