using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRA4.Web.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            var users = _applicationContext.ServiceContext.UserService.GetUsers();
            return View(users);
        }

        public ActionResult ChangePassword(string userid)
        {
            var users = _applicationContext.ServiceContext.UserService.GetUsers();
            HRA4.ViewModels.User user = users.Where(u => u.Username == userid).FirstOrDefault();
            ViewBag.AlertMessage = "Password must be at least 6 characters long, must contain at least 1 special charcter and must contain only letters and numbers and ~!@#$%^*()_+= special characters.";
            return PartialView("_ChangePassword", user);
        }

        public ActionResult SetProfile(string userid)
        {
            var userProfile = _applicationContext.ServiceContext.UserService.GetUserProfile(userid);
            return PartialView("_UserProfileList", userProfile);
        }

        [HttpPost]
        public ActionResult SaveProfile(FormCollection frm)
        {
            //Save frm 0:Clininc 1: Role 2: Module
            return RedirectToAction("Index");
        }

        public ActionResult AddNewProfile(string userid)
        {
            var profile = _applicationContext.ServiceContext.UserService.AddNewProfile(userid);
             
            return PartialView("_UserProfile",profile);
        }

        [HttpPost]
        public ActionResult SavePassword(FormCollection frm)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddNewUser(FormCollection frm)
        {
            return RedirectToAction("Index");
        }
    }
}