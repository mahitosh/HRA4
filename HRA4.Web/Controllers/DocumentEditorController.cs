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
            //model.Content = _applicationContext.ServiceContext.TemplateService.GetTemplate(4, 20).TemplateString;
            //templates.Content = _applicationContext.ServiceContext.TemplateService.GetTemplate(4, 20).TemplateString;

            return View(templates);
        }

        public ActionResult SelectTemplate(int Id, string TemplateName)
        {
            var templates = _applicationContext.ServiceContext.TemplateService.GetTemplates();
            templates.Content = _applicationContext.ServiceContext.TemplateService.GetTemplate(4, Id).TemplateString;
            templates.TemplateName = TemplateName;
            return View("Index", templates);
        
        }




        /*
        public JsonResult SelectTemplate(string TemplateName , int templateId  )
        {

            string view = string.Empty;
            TinyMCEModelJQuery template = new TinyMCEModelJQuery();
            template.Content = _applicationContext.ServiceContext.TemplateService.GetTemplate(4, templateId).TemplateString;
            template.TemplateName = TemplateName;
            view = RenderPartialView("_TextEditor", template);
            var result = new { view = view };
            return Json(result, JsonRequestBehavior.AllowGet);


        }
        */

        public ActionResult SaveTemplate(HRA4.ViewModels.TemplateList model )
        {
            string TemplateString = string.Empty;
            TemplateString = model.Content;
            _applicationContext.ServiceContext.TemplateService.UpdateTemplate(0, 3, TemplateString);
             model = _applicationContext.ServiceContext.TemplateService.GetTemplates();
            return View("Index",model);
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