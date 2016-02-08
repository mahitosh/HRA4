using HRA4.Context;
using HRA4.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HRA4.Web.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string View { get; set; }
        public CustomAuthorizeAttribute()
        {
            View = "~/Views/Admin/Index.cshtml";
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        private void IsUserAuthorized(AuthorizationContext filterContext)
        {
            // If the Result returns null then the user is Authorized 
            if (filterContext.Result == null)
                return;

            //If the user is Un-Authorized then Navigate to Auth Failed View 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                // var result = new ViewResult { ViewName = View };
               
                

                var vr = new ViewResult();
                vr.ViewName = View;

                ViewDataDictionary dict = new ViewDataDictionary();
                dict.Add("Message", "Sorry you are not Authorized to Perform this Action");

                vr.ViewData = dict;

                var result = vr;

                filterContext.Result = result;
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (ticket != null)
                    {
                        string username = FormsAuthentication.Decrypt(httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        //SampleData entities = new SampleData();

                        //SUser user = entities.SUsers.SingleOrDefault(u => u.Username == username);

                        //roles = user.Role;

                        IApplicationContext _applicationContext;
                        if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["ApplicationContext"] != null)
                        {
                            _applicationContext = System.Web.HttpContext.Current.Session["ApplicationContext"] as ApplicationContext;

                            roles = _applicationContext.ServiceContext.UserService.GetRoles(username);
                        }
                         
                        var identity = new GenericIdentity(ticket.Name);

                        //Pass array of roles associated to the logged in user to Principle class.
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, roles.Split('|'));
                       
                    }
                }
           
            }

            return base.AuthorizeCore(httpContext);
        }

      
       
    }
}