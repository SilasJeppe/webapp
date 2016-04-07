using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using webapi.Models;

namespace webapi.DB
{
    public class DBRoute
    {
        private NpgsqlConnection con;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private DBPoint dbPoint;
        public DBRoute()
        {
            dbPoint = new DBPoint();
            con = DBConnection.GetInstance().GetConnection();
        }

        public Route GetRouteByActivityID(int id)
        {
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
    }
}