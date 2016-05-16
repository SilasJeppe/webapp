using System.Web.Http;
using webapi.DB;

namespace webapi.Controllers
{
    public class RouteController : ApiController
    {
        private DBRoute db = new DBRoute();

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
