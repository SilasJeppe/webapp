//Class with methods that communicates with the DB, and can be accessed via API
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
    public class ActivityController : ApiController
    {
        private DBActivity db = new DBActivity();
        // GET: api/Activity
        public IEnumerable<webapi.Models.Activity> Get()
        {
            List<webapi.Models.Activity> activityList = new List<Activity>();
            activityList = db.GetAllActivity();
            return activityList;
        }

        // GET: api/Activity/5
        [ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> Get(int id)
        {
            webapi.Models.Activity activity = db.GetActivity(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        // POST: api/Activity
        [ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> Post([FromBody]Activity a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.InsertActivity(a);
            return CreatedAtRoute("DefaultApi", new { }, a);
        }

        //// PUT: api/Activity/5
        //public void Put(int id, [FromBody]string value)
        //{
        //    //Laves på et senere tidspunkt
        //}

        // DELETE: api/Activity/5
        [ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Activity activity = db.GetActivity(id);
            if (activity == null)
            {
                return NotFound();
            }
            db.DeleteActivity(id);
            return Ok(activity);
        }
    }
}
