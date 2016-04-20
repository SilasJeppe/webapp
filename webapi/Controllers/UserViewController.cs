using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    public class UserViewController : Controller
    {
        // GET: UserView
        public ActionResult Index()
        {
            List<User> list = GetData();
            ViewBag.List = list;
            ViewBag.Title = "Brugere";
            return View();
        }

        // GET: UserView/Details/5
        public ActionResult Details(int id)
        {
            User user = GetUser(id);
            ViewBag.User = user;
            return View();
        }

        // GET: UserView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserView/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserView/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserView/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserView/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserView/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private List<User> GetData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6617/");

            List<User> users = new List<User>();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/User?action=").Result;

            if (response.IsSuccessStatusCode)
            {
                users = response.Content.ReadAsAsync<IEnumerable<User>>().Result.ToList();
            }
            return users;
        }

        private User GetUser(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6617/");

            User user = new User();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/User/?=id").Result;

            if (response.IsSuccessStatusCode)
            {
                user = response.Content.ReadAsAsync<IEnumerable<User>>().Result.Where(x => x.ID == id).SingleOrDefault();
            }
            return user;
        }
    }
}
