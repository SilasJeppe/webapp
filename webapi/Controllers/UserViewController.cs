using Newtonsoft.Json;
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
    public class UserViewController : BaseController
    {
        private string url { get; set; }

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

                return RedirectToAction("Index", "UserView");
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            User user;
            if (ModelState.IsValid)
            {
                user = CheckUser(email);
                if (user != null && webapi.BLL.Hash.ValidatePassword(password, user.password))
                {
                    var json = JsonConvert.SerializeObject(user);
                    var userCookie = new HttpCookie("user", json);
                    userCookie.Expires.AddDays(365);
                    Response.SetCookie(userCookie);
                    Response.Cookies.Add(userCookie);

                    return RedirectToActionPermanent("Index", "Home");
                }
                else
                {
                    return new HttpStatusCodeResult(404, "User not found!");
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["user"] != null)
            {
                var user = new HttpCookie("user")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = null
                };
                Response.SetCookie(user);
                //Response.Cookies.Add(user);
            }
            return RedirectToActionPermanent("Index", "Home");
        }

        private User CheckUser(string email)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6617/");

            User user = null;
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/user?email=" + email).Result;

            if (response.IsSuccessStatusCode)
            {
                user = response.Content.ReadAsAsync<User>().Result;
            }
            return user;
        }

        private List<User> GetUsers()
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

        private List<User> GetData()
        {
            HttpClient client = new HttpClient();
            url = Request.Url.AbsoluteUri;
            client.BaseAddress = new Uri(url);

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
            url = "http://" + Request.Url.Authority;
            client.BaseAddress = new Uri(url);

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
            url = "http://" + Request.Url.Authority;
            client.BaseAddress = new Uri(url);

            var res = client.PostAsJsonAsync("api/User", u).Result;
        }

        private void DeleteUser(int id)
        {
            HttpClient client = new HttpClient();
            url = "http://" + Request.Url.Authority;
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string str = "api/User/" + id;
            var res = client.DeleteAsync(str).Result;
        }



        

    }
}
