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
    public class PointController : ApiController
    {
        private DBConnection dbcon = new DBConnection();
        // GET: api/Point
        public IEnumerable<webapi.Models.Point> Get()
        {
            List<webapi.Models.Point> doubleList = new List<webapi.Models.Point>();
            doubleList = dbcon.allPoints();
            return doubleList;
            //webapi.Models.Point p1 = new webapi.Models.Point(99, "Kasper Løkke", 0, 0);
            //webapi.Models.Point p2 = new webapi.Models.Point(100, "Kasper Løkke", 90, 90);
            //return new Point[] { p1, p2 };
        }

        // GET: api/Point/5
        [ResponseType(typeof(webapi.Models.Point))]
        public async Task<IHttpActionResult> Get(int id)
        {
            webapi.Models.Point point = dbcon.GetPoint(id);
            if(point == null)
            {
                return NotFound();
            }

            return Ok(point);
        }

        
        // POST: api/Point
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Point/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Point/5
        [ResponseType(typeof(webapi.Models.Point))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            webapi.Models.Point point = dbcon.GetPoint(id);
            if (point == null)
            {
                return NotFound();
            }

            dbcon.DeletePoint(id);
            return Ok(point);

        }

        
    }
}
