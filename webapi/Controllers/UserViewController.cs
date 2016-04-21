using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        public ActionResult Create(User user)
        {
            try
            {
                PostUser(user);

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
            User user = GetUser(id);
            ViewBag.User = user;
            return View();
        }

        // POST: UserView/Delete/5
        [HttpPost]
        public ActionResult Delete(User u)
        {
            try
            {
                DeleteUser(u.ID);

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

            HttpResponseMessage response = client.GetAsync("api/User").Result;

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

            string str = "api/User/" + id;
            HttpResponseMessage response = client.GetAsync(str).Result;

            if (response.IsSuccessStatusCode)
            {
                user = response.Content.ReadAsAsync<User>().Result;
            }
            return user;
        }

        private void PostUser(User u)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6617/");
            
            var res = client.PostAsJsonAsync("api/User", u).Result;
        }

        private void DeleteUser(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6617/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string str = "api/User/" + id;
            var res = client.DeleteAsync(str).Result;
        }

    }
}
