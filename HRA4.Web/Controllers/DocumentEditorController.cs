using HRA4.Entities;
using HRA4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Services;
using System.IO;

namespace HRA4.Web.Controllers
{
    public class DocumentEditorController : BaseController
    {
        // GET: DocumentEditor
        public ActionResult Index()
        {
            var templates = _applicationContext.ServiceContext.TemplateService.GetTemplates();
            ViewBag.Message = "";
            return View(templates);
        }

        public ActionResult SelectTemplate(int Id, string TemplateName)
        {
            var templates = _applicationContext.ServiceContext.TemplateService.GetTemplates();
            templates.Content = _applicationContext.ServiceContext.TemplateService.GetTemplate(Id).TemplateString;
            templates.TemplateName = TemplateName;
            templates.Id = Id;
            ViewBag.Message = "";
            return View("Index", templates);
        
        }
        public ActionResult SaveTemplate(HRA4.ViewModels.TemplateList model )
        {
            string TemplateString = string.Empty;
            TemplateString = model.Content;
            var new_model = _applicationContext.ServiceContext.TemplateService.GetTemplates();
            if (!ModelState.IsValid)
            {
               // ModelState.AddModelError("keyName", "Form is not valid");
                return View("Index", new_model);
            }

            _applicationContext.ServiceContext.TemplateService.UpdateTemplate(model.Id, TemplateString);
            
            new_model.TemplateName = model.TemplateName;
            new_model.Id = model.Id;
            ViewBag.Message = "Template edited sucessfully.";
            return View("Index", new_model);
        }
        public ActionResult Cancel()
        {
            ViewBag.Message = "";
            ViewModels.TemplateList template = new ViewModels.TemplateList();
            template = _applicationContext.ServiceContext.TemplateService.GetTemplates();
            return View("Index", template);
        }

    
    }
    



}