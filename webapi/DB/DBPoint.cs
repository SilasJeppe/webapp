using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;

namespace webapi.DB
{
    public class DBPoint
    {
        private NpgsqlConnection con;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        public DBPoint()
        {
            con = DBConnection.GetInstance().GetConnection();
        }
        public void InsertPoint(NpgsqlPoint p, int routeID)
        {
            con.Open();
            string sql = "INSERT INTO public.point(geom, routeid) VALUES(ST_MakePoint(@Long, @Lat), @routeID)";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Long", p.X);
            cmd.Parameters.AddWithValue("@Lat", p.Y);
            cmd.Parameters.AddWithValue("@routeID", routeID);
            int i = cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<webapi.Models.Point> GetPointsByRouteID(int id)
        {
            List<webapi.Models.Point> listPoints = new List<webapi.Models.Point>();
            string sql = "SELECT id, st_x(geom), st_y(geom), routeid FROM public.point WHERE routeid = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Reset();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                int ID = x.Field<int>("id");
                int RouteID = x.Field<int>("routeid");
                NpgsqlPoint npgP = new NpgsqlPoint(x.Field<double>("st_x"), x.Field<double>("st_y"));
                webapi.Models.Point p = new webapi.Models.Point(ID, npgP, RouteID);
                listPoints.Add(p);
            }
            return listPoints;
        }
    }
}