using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Utilities;
using HRA4.ViewModels;
using System.Configuration;
using HRA4.Context;
using HRA4.Entities;
using HRA4.Web.Filters;
namespace HRA4.Web.Controllers
{
    [CustomAuthorize(Roles = "SuperAdmin,Administrator,Clinician")] 
    public class TestPatientController : BaseController
    {
        // GET: TestPatient
        public ActionResult TestPatients()
        {
            TestPatient tp = new TestPatient();
            tp = _applicationContext.ServiceContext.AppointmentService.LoadCreateTestPatients();
            tp.InitateTestPatients = new List<TestPatientAppointment>();
            ViewBag.TodaysDate = DateTime.Now.ToString("MM/dd/yyyy");
            return View(tp);
        }
        [HttpPost]
        public ActionResult TestPatients(FormCollection frm)
        {
            int NoOfAppointments =Convert.ToInt32(frm["AppCount"]);
            string date = frm["dob-date"];
            int surveyid =Convert.ToInt32(frm["ddlsurveys"]);
            string surveynm = frm["ddlsurveystext"];
            int clinicid = Convert.ToInt32(frm["ddlclinics"]);
            _applicationContext.ServiceContext.AppointmentService.CreateTestPatients(NoOfAppointments,date,surveyid,surveynm,clinicid);

            return RedirectToAction("TestPatients");
        }
        public ActionResult DeleteTestPatientsByapptids(string ids)
        {
            string[] stringOfapptids = ids.Split(',');
            int[] apptids = Array.ConvertAll(stringOfapptids,int.Parse);
            _applicationContext.ServiceContext.AppointmentService.DeleteTestPatientsByapptids(apptids);
            TestPatient tp = new TestPatient();
            tp = _applicationContext.ServiceContext.AppointmentService.LoadCreateTestPatients();
            return PartialView("_DeleteTestPatientGrid",tp);


        }
        public ActionResult ExcludeTestPatientsByapptids(string ids)
        {
            string[] stringOfapptids = ids.Split(',');
            int[] apptids = Array.ConvertAll(stringOfapptids, int.Parse);
            _applicationContext.ServiceContext.AppointmentService.ExcludeTestPatientsByapptids(apptids);
            TestPatient tp = new TestPatient();
            tp = _applicationContext.ServiceContext.AppointmentService.LoadCreateTestPatients();
            return PartialView("_DeleteTestPatientGrid", tp);
            
        }
        public ActionResult RefreshTestPatients()
        {
            TestPatient tp = new TestPatient();
            tp = _applicationContext.ServiceContext.AppointmentService.LoadCreateTestPatients();
            return PartialView("_DeleteTestPatientGrid", tp);

        }

    }
}