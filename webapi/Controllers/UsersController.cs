using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using webapi.Models;
using System.Web.Script.Serialization;

namespace webapi.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Users
        public ActionResult Index()
        {
            List<User> list = GetUsers();
            ViewBag.List = list;
            ViewBag.Title = "Brugere";
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User u)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", u);
            }

            //
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            User user;
            if (ModelState.IsValid)
            {
                user = CheckUser(email, password);

                if (user == null || user.Email != email)
                {
                    return new HttpStatusCodeResult(404, "User not found!");
                }
                else
                {
                    var json = JsonConvert.SerializeObject(user);
                    var userCookie = new HttpCookie("user", json);
                    userCookie.Expires.AddDays(365);
                    Response.SetCookie(userCookie);
                    Response.Cookies.Add(userCookie);

                    return RedirectToActionPermanent("Index", "Home");
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            if(Request.Cookies["user"] != null)
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

        private User CheckUser(string email, string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6617/");

            User user = new User();
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
    }
}