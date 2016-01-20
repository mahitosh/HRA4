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
namespace HRA4.Web.Controllers
{
    public class TestPatientController : BaseController
    {
        // GET: TestPatient
        public ActionResult TestPatients()
        {
            TestPatient tp = new TestPatient();
            tp = _applicationContext.ServiceContext.AppointmentService.LoadCreateTestPatients();
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

    }
}