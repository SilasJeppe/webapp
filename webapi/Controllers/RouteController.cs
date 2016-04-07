using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.DB;
using System.Web.Http.Description;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Controllers
{
    public class RouteController : ApiController
    {
        private DBRoute db = new DBRoute();
        // GET: api/Route
        //public IEnumerable<Route> Get()
        //{
        //    List<Route> routes = new List<Route>();
        //    routes = db.GetRouteByActivityID();
        //    return routes;
        //}

        // GET: api/Route/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Route
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Route/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Route/5
        public void Delete(int id)
        {
        }
    }
}
