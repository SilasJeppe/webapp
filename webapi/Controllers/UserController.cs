﻿//Class for the User API
using System.Collections.Generic;
using System.Web.Http;
using webapi.DB;
using webapi.Models;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace webapi.Controllers
{
    public class UserController : ApiController
    {
        private DBUser db = new DBUser();

        // GET: api/User
        public IEnumerable<webapi.Models.User> Get()
        {
            List<webapi.Models.User> userList = new List<webapi.Models.User>();
            userList = db.GetUsers();
            return userList;
        }

        // GET: api/User/5
        [ResponseType(typeof(webapi.Models.User))]
        public async Task<IHttpActionResult> Get(int id)
        {
            webapi.Models.User user = db.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET from email, used at website
        [ResponseType(typeof(webapi.Models.User))]
        public async Task<IHttpActionResult> GetEmail(string email)
        {
            webapi.Models.User user = db.GetUserFromEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/User
        [ResponseType(typeof(webapi.Models.User))]
        public async Task<IHttpActionResult> Post([FromBody]webapi.Models.User u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string hash = webapi.BLL.Hash.CreateHash(u.password);
            db.InsertUser(u.Firstname, u.Surname, u.Address, u.City, u.ZipCode, u.PhoneNumber, u.Email, hash);
            return CreatedAtRoute("DefaultApi", new { }, u);
        }

        // PUT: api/User/5 - NOT IMPLEMENTET
        public void Put(int id, [FromBody]string value)
        {
            //Skal laves senere
        }

        // DELETE: api/User/5
        [ResponseType(typeof(webapi.Models.User))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            webapi.Models.User user = db.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            db.DeleteUser(id);
            return Ok(user);
        }
    }
}
