//Class that access the Views for Activity
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    public class ActivitiesController : BaseController
    {
        private string url { get; set; }

        // GET: Activities
        //Returns view Index with a list of Activities
        public ActionResult Index()
        {
            ViewBag.List = GetData();
            ViewBag.Title = "Activities";
            return View();
        }

        //Method that gets the activities using JSon and the API
        private IEnumerable<webapi.Models.Activity> GetData()
        {
            HttpClient client = new HttpClient();
            url = Request.Url.AbsoluteUri;
            client.BaseAddress = new Uri(url);

            IEnumerable<webapi.Models.Activity> activities = null;
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Activity").Result; 

            if (response.IsSuccessStatusCode)
            {
                activities = response.Content.ReadAsAsync<IEnumerable<Activity>>().Result;
            }
            return activities;
        }
    }
}