using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRA4.Context;
namespace HRA4.Web.Controllers
{
    public class BaseController : Controller
    {
        public IApplicationContext _applicationContext;


        public BaseController()
        {
            _applicationContext = new ApplicationContext();
            //var temp =_applicationContext.Services.AppointmentService.GetAppointments();
            // var temp = ApplicationContext.AppointmentService.GetAppointments();
        }
    }
}