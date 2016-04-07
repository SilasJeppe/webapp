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
        [ResponseType(typeof(webapi.Models.Point))]
        public async Task<IHttpActionResult> Post([FromBody]webapi.Models.Point p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.InsertPoint(p.ID, p.Coords, p.RouteID);
            return CreatedAtRoute("DefaultApi", new { id = p.ID }, p);
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
