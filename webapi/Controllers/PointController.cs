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

namespace webapi.Controllers
{
    public class PointController : ApiController
    {
        private DBConnection dbcon = new DBConnection();
        // GET: api/Point
        //public IEnumerable<webapi.Models.Point> Get()
        //{
        //    List<webapi.Models.Point> doubleList = new List<webapi.Models.Point>();
        //    doubleList = dbcon.allPoints();
        //    return doubleList;
        //}

        // GET: api/Point/5
        //[ResponseType(typeof(webapi.Models.Point))]
        //public async Task<IHttpActionResult> Get(int id)
        //{
        //    webapi.Models.Point point = dbcon.GetPoint(id);
        //    if(point == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(point);
        //}


        // POST: api/Point
        [ResponseType(typeof(webapi.Models.Point))]
        public async Task<IHttpActionResult> Post([FromBody]webapi.Models.Point p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbcon.InsertPoint(p.pointID, p.name, p.Coords.pLong, p.Coords.pLat);
            return CreatedAtRoute("DefaultApi", new { id = p.pointID }, p);
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
