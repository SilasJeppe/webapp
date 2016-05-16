//this class is the link between the webapi and the views for an user.
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
            List<User> list = GetUsers();
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
                // TODO: Add update logic here - not implementet yet

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

        //method for loging in the user and creating a cookie, lasting for a period of time. The userinformation is added to the cookie for easier to find the user.
        //taking an email and a password for a specific user
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            User user;
            //checking for a valid user
            if (ModelState.IsValid)
            {
                //finds user by the given email
                user = CheckUser(email);
                //if user is found, validate the password with the hashed password in the db.
                if (user != null && webapi.BLL.Hash.ValidatePassword(password, user.password))
                {
                    //creating a cookie that lasts for 30 minuts.
                    var json = JsonConvert.SerializeObject(user);
                    var userCookie = new HttpCookie("user", json);
                    userCookie.Expires.AddMinutes(30);
                    Response.SetCookie(userCookie);
                    Response.Cookies.Add(userCookie);

                    return RedirectToActionPermanent("Index", "Home");
                }
                else
                {
                    return new HttpStatusCodeResult(404, "Invalid username/email or password!");
                }
            }
            return View("Login");
        }

        //this method logs out the user by setting the cookie expiredate to -1 with a value null.
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

        //Takes an email and returns an user in the DB
        private User CheckUser(string email)
        {
            HttpClient client = new HttpClient();
            url = "http://" + Request.Url.Authority;
            client.BaseAddress = new Uri(url);

            User user = null;
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/user?email=" + email).Result;

            if (response.IsSuccessStatusCode)
            {
                //if found, convering the JSON to an user.
                user = response.Content.ReadAsAsync<User>().Result;
            }
            return user;
        }

        //gets all the users in the DB, adds them to a list and returns it.
        private List<User> GetUsers()
        {
            HttpClient client = new HttpClient();
            url = "http://" + Request.Url.Authority;
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

        //Gets a specific user by the id
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

        //Posts a user to the DB, taking a user as parameter
        private void PostUser(User u)
        {
            HttpClient client = new HttpClient();
            url = "http://" + Request.Url.Authority;
            client.BaseAddress = new Uri(url);

            var res = client.PostAsJsonAsync("api/User", u).Result;
        }

        //Deleting a user by its id
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
