﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;
using webapi.DB;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace webapi.Controllers
{
    public class UserController : ApiController
    {
        private DBConnection dbcon = new DBConnection();
        // GET: api/User
        public IEnumerable<webapi.Models.User> Get()
        {
            List<webapi.Models.User> userList = new List<webapi.Models.User>();
            userList = dbcon.allUsers();
            return userList;
        }

        // GET: api/User/5
        [ResponseType(typeof(webapi.Models.User))]
        public async Task<IHttpActionResult> Get(int id)
        {
            webapi.Models.User user = dbcon.GetUser(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/User
        [ResponseType(typeof(webapi.Models.User))]
        public async Task<IHttpActionResult> Post([FromBody]webapi.Models.User u)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbcon.InsertUser(u.Firstname, u.Surname, u.Address, u.City, u.ZipCode, u.PhoneNumber, u.Email, u.password);
            return CreatedAtRoute("DefaultApi", new { }, u);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
            //Skal laves senere
        }

        // DELETE: api/User/5
        [ResponseType(typeof(webapi.Models.User))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            webapi.Models.User user = dbcon.GetUser(id);
            if(user==null)
            {
                return NotFound();
            }
            dbcon.DeleteUser(id);
            return Ok(user);
        }
    }
}