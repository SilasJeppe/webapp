//Class for connection the Point class to the database
using NpgsqlTypes;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System.Data;

namespace webapi.DB
{
    public class DBPoint
    {
        private NpgsqlConnection con = new NpgsqlConnection(DBConnection.connectionstring);
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        public DBPoint()
        {
        }

        //method that inserts a list of Points in the database
        public void InsertPoints(List<webapi.Models.Point> points)
        {
            foreach (webapi.Models.Point p in points)
            {
                string sql = "INSERT INTO public.point(geom, routeid) VALUES(ST_MakePoint(@Long, @Lat), @routeID)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Long", p.Coords.X);
                cmd.Parameters.AddWithValue("@Lat", p.Coords.Y);
                cmd.Parameters.AddWithValue("@routeID", p.RouteID);
                int i = cmd.ExecuteNonQuery();
            }
        }

        //Method that gets a list of Points based on a Route ID
        public List<webapi.Models.Point> GetPointsByRouteID(int id)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

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
                long ID = x.Field<long>("id");
                int RouteID = x.Field<int>("routeid");
                NpgsqlPoint npgP = new NpgsqlPoint(x.Field<double>("st_x"), x.Field<double>("st_y"));
                webapi.Models.Point p = new webapi.Models.Point(ID, npgP, RouteID);
                listPoints.Add(p);
            }
            con.Close();
            return listPoints;
        }
    }
}