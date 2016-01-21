using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Web.Mvc;

namespace HRA4.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
		 
		[AllowAnonymous]
        public ActionResult BadRequest()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
			return View();
        }

        
		[AllowAnonymous]
        public ActionResult Unauthorized()
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
			return View();
        }
		
		 
		[AllowAnonymous]
        public ActionResult Forbidden()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;

		    var model = (ViewData.Model as HandleErrorInfo);
		    string returnUrl = string.Empty;

		    if (model != null)
		    {
		        returnUrl = Url.Action(model.ActionName, model.ControllerName);
		    }

		    return RedirectToAction("Login", "Account", new { returnUrl = returnUrl, name = "" });
        }
		
		 
		[AllowAnonymous]
        public ActionResult PageNotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
			return View();
        }
		
		 
		[AllowAnonymous]
        public ActionResult ServerError()
        {
			Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			return View("CustomError");
        }
		
		 
		[AllowAnonymous]
        public ActionResult CustomError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			return View();
        }
    }
}
