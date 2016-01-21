using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using HRA4.Services;
using HRA4.ViewModels;
namespace HRA4.Web.Controllers
{
    public class HighRiskFollowupController : BaseController
    {
        // GET: HighRiskFollowup
        public ActionResult Index()
        {
            

            List<HighRiskLifetimeBreast> lst = new List<HighRiskLifetimeBreast>();
            lst = _applicationContext.ServiceContext.RiskClinicServices.GetPatients();
            return View(lst);
        }

        public ActionResult Search()
        {
            
            List<HighRiskLifetimeBreast> lst = new List<HighRiskLifetimeBreast>();
            lst = _applicationContext.ServiceContext.RiskClinicServices.GetPatients();
            return PartialView("_HighRiskLifetimeBreastGrid", lst);


        }

        public JsonResult GetPatientDetails()
        {
        
            var obj = _applicationContext.ServiceContext.RiskClinicServices.GetPatientDetails("", -1);
            string PedigreeImagePath = @"data:image/png;base64," + obj.PedigreeImage + "";
            var obj1 = new { obj, ImageUrl = PedigreeImagePath };
            return Json(obj1);

        }




        

    }
}