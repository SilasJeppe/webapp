//This controller is used to load the frontpage
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    //this method loads the frontpage with the given title and returns view.
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "RunnerApp";

            return View();
        }
    }
}
