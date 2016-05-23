//Class for connection the Route class to the database
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using webapi.Models;

namespace webapi.DB
{
    public class DBRoute
    {
        private NpgsqlConnection con = new NpgsqlConnection(DBConnection.connectionstring);
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private DBPoint dbPoint;

        public DBRoute()
        {
            dbPoint = new DBPoint();
        }
        
        //Method that gets a Route by Activity ID
        public Route GetRouteByActivityID(int id)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            List<webapi.Models.Route> listRoute = new List<webapi.Models.Route>();
            string sql = "SELECT * FROM public.route WHERE activityid = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Reset();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                webapi.Models.Route route = new webapi.Models.Route()
                {
                    ID = x.Field<int>("id"),
                    ActivityID = x.Field<int>("activityid"),
                    PointList = dbPoint.GetPointsByRouteID(x.Field<int>("id"))
                };
                listRoute.Add(route);
            }
            return listRoute.FirstOrDefault();
        }

        //Inserts a route in DB
        public void InsertRoute(Route r)
        {
            string sql = "INSERT INTO public.route(activityid) VALUES (@activityid)";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@activityid", r.ActivityID);
            int i = cmd.ExecuteNonQuery();
            int routeid = GetRouteByActivityID(r.ActivityID).ID;
            foreach (webapi.Models.Point p in r.PointList)
            {
                p.RouteID = routeid;
            }
            dbPoint.InsertPoints(r.PointList);
        }
    }
}