﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using webapi.Models;

namespace webapi.Controllers
{
    public class ValuesController : BaseController
    {
        public ActionResult Index()
        {
            var userCookie = Request.Cookies["user"];
            webapi.Models.User user = null;
            if (userCookie != null)
            {
                JavaScriptSerializer Json = new JavaScriptSerializer();
                user = Json.Deserialize<webapi.Models.User>(userCookie.Value);
            }
            return PartialView(user);
        }
    }
}
