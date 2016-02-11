using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRA4.Web.Controllers
{
    public class EditSurveyController : BaseController
    {
        // GET: EditSurvey
        public ActionResult Index(string unitnum, int apptid)
       {
           ViewBag.unitnum = unitnum;
           ViewBag.apptid = apptid;
          

           List<ViewModels.FamilyHistoryRelative> obj = new List<ViewModels.FamilyHistoryRelative>();
         //  obj = _applicationContext.ServiceContext.AppointmentService.GetFamilyHistoryRelative(unitnum,apptid); //commented by Nilesh 
            
           return View("Index", obj);
        }

        public ActionResult LoadBreastCancerFactors(string mrn, int apptId)
       {
           var breastCancer = _applicationContext.ServiceContext.SurveyRiskFactors.LoadBreastCancerRiskFactors(mrn, apptId);
           return PartialView("_CancerRiskFactorBreast", breastCancer);
       }

       public ActionResult LoadColorectalCancerRiskFactors(string mrn, int apptId)
        {
            

            var ColorectalCancer = _applicationContext.ServiceContext.SurveyRiskFactors.LoadColorectalRiskFactors(mrn, apptId);
            return PartialView("_CancerRiskFactorsColorectal", ColorectalCancer);
        }


        public ActionResult SaveBreastCancerRiskFactor(FormCollection frm)
        {
            string mrn="";
            int apptId=0;
            return LoadBreastCancerFactors(mrn, apptId);
        }
        /*
        public Action FamilyHistory()
        { 
        
        }
         */
       public JsonResult GetPatientDetails(string unitnum, int apptid)
       {
          
            var obj = _applicationContext.ServiceContext.RiskClinicServices.GetPatientDetails(unitnum, apptid);
            var obj1 = new { obj };
            return Json(obj1);

           
        }

       public ActionResult RunRiskModule(int type, string unitnum, int apptid)
       {
           if (type == 1)
           {
               _applicationContext.ServiceContext.AppointmentService.CalculateRiskAndRunAutomation(apptid, unitnum);


           }
           else
           {

               LegacyCoreAPI.stopWorkingWithAppointment(apptid);
           
           }



            return RedirectToAction("InstitutionDashboard", "Institution");
       
       }

        

        public JsonResult AddRelative(string unitnum, int apptid, string Relationship)
       {
           List<ViewModels.FamilyHistoryRelative> objList = new List<ViewModels.FamilyHistoryRelative>();

           ViewModels.FamilyHistoryRelative objModel = new ViewModels.FamilyHistoryRelative();

           objModel.Relationship = Relationship;

            _applicationContext.ServiceContext.AppointmentService.SaveSurvey(unitnum, apptid, objModel, 0);

           objList = _applicationContext.ServiceContext.AppointmentService.GetFamilyHistoryRelative(unitnum, apptid);
           var view = RenderPartialView("_FamilyHistoryRelativeRow", objList);


            var obj = new { view = view };
           return Json(obj);
       
       }

       public JsonResult DeleteRelative(string unitnum, int apptid, int RelativeId)
       {
           List<ViewModels.FamilyHistoryRelative> objList = new List<ViewModels.FamilyHistoryRelative>();
           ViewModels.FamilyHistoryRelative objModel = new ViewModels.FamilyHistoryRelative();
           objModel.RelativeId = RelativeId;
           _applicationContext.ServiceContext.AppointmentService.SaveSurvey(unitnum, apptid, objModel, 2);

           objList = _applicationContext.ServiceContext.AppointmentService.GetFamilyHistoryRelative(unitnum, apptid);
           var view = RenderPartialView("_FamilyHistoryRelativeRow", objList);


           var obj = new { view = view };
           return Json(obj);

       }

       public JsonResult SaveSurvey(FormCollection frm)
       {

           List<ViewModels.FamilyHistoryRelative> objList = new List<ViewModels.FamilyHistoryRelative>();

           ViewModels.FamilyHistoryRelative objModel = new ViewModels.FamilyHistoryRelative();

           string unitnum = frm["hdunitnum"].ToString();
           string Type = frm["hdType"].ToString();
            int apptid = Convert.ToInt32(frm["hdapptid"]);
           //objModel.Relationship = frm["ddlRelation"]; //frm["hdddlRelation"].ToString();  //
           objModel.RelativeAge = frm["txtRelativeAge"];
           objModel.VitalStatus = frm["ddlVital"]; //frm["hdddlVital"]; //
           objModel.FirstDx = frm["ddlFirstDx"]; //frm["hdddlFirstDx"]; //
           objModel.FirstAgeOnset = frm["txtFirstAgeOnset"];
           objModel.SecondDx = frm["ddlSecondDx"]; //frm["hdddlSecondDx"]; //
           objModel.SecondAgeOnset = frm["txtSecondAgeOnset"];
           objModel.ThirdDx = frm["ddlThirdDx"]; //frm["hdddlThirdDx"]; //
           objModel.ThirdAgeOnset = frm["txtThirdAgeOnset"];
           objModel.RelativeId = Convert.ToInt32(frm["hdRelativeId"]);
           //objList.Add(objModel);

           


            _applicationContext.ServiceContext.AppointmentService.SaveSurvey(unitnum, apptid, objModel, 1);

           objList = _applicationContext.ServiceContext.AppointmentService.GetFamilyHistoryRelative(unitnum, apptid);
          
           var view = RenderPartialView("_FamilyHistoryRelativeRow", objList);

           var obj = new { view = view, Type = Type };
           return Json(obj);


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


       public ActionResult SaveSurvey1()
       {
           List<ViewModels.FamilyHistoryRelative> objModel = new List<ViewModels.FamilyHistoryRelative>();
           /*
           objModel.Relationship = frm["ddlRelation"];
           objModel.RelativeAge = frm["txtRelativeAge"];
           objModel.VitalStatus = frm["ddlVital"];
           */
           
         


           /*
           var obj = PartialView("_FamilyHistoryRelativeRow",objModel );
           var obj1 = new { obj };
           return Json(obj1);
           */

           return PartialView("_FamilyHistoryRelativeRow", objModel);

       }

    }
}