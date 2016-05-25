//This controller is used to fill the the user, that is logged in, into the viewbag. This method is called "OnActionExecuting" and is able on every page.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace webapi.Controllers
{
    public class BaseController : Controller
    {
        //on action execute, checks for a user and puts it into the viewbag, if any
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userCookie = Request.Cookies["user"];
            Models.User user = null;
            if (userCookie != null)
            {
                JavaScriptSerializer Json = new JavaScriptSerializer();
                user = Json.Deserialize<Models.User>(userCookie.Value);
            }
            ViewBag.User = user;
            base.OnActionExecuting(filterContext);
        }
    }
}
