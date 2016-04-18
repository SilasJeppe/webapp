using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    public class ActivitiesController : Controller
    {
        //private ActivityController aCtr = new ActivityController();
        // GET: Activities
        public ActionResult Index()
        {
            //List<Activity> list = aCtr.Get().ToList();
            List<Activity> list = GetData();
            ViewBag.List = list;
            ViewBag.Title = "Activities";
            return View();
        }

        private List<Activity> GetData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6617/");

            List<Activity> activities = new List<Activity>();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Activity").Result;

            if (response.IsSuccessStatusCode)
            {
                activities = response.Content.ReadAsAsync<IEnumerable<Activity>>().Result.ToList();
            }
            return activities;
        }

    }
}