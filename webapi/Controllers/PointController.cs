using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;
using webapi.DB;
using System.Web.Http.Description;
using System.Threading.Tasks;
using Npgsql;

namespace webapi.Controllers
{
    public class PointController : ApiController
    {
        private DBPoint db = new DBPoint();
        //GET: api/Point
        //public IEnumerable<webapi.Models.Point> Get()
        //{
            //List<webapi.Models.Point> doubleList = new List<webapi.Models.Point>();
            //doubleList = db.GetPointsByRouteID();
            //return doubleList;
        //}

        // GET: api/Point/5
        [ResponseType(typeof(webapi.Models.Point))]
        public async Task<IHttpActionResult> Get(int id)
        {
            return NotFound();
        }


        // POST: api/Point
        [ResponseType(typeof(List<webapi.Models.Point>))]
        public async Task<IHttpActionResult> Post([FromBody]List<webapi.Models.Point> points)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (webapi.Models.Point p in points)
            {
                db.InsertPoint(p.Coords, p.RouteID);
            }
            //db.InsertPoint(p.ID, p.Coords, p.RouteID);
            return CreatedAtRoute("DefaultApi", new { }, points);
        }

        // PUT: api/Point/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]string value)
        {
            return NotFound();
        }

        // DELETE: api/Point/5
        [ResponseType(typeof(webapi.Models.Point))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            return NotFound();
        }
    }
}
