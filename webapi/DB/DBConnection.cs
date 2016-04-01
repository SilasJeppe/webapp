using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Npgsql;
using webapi.Models;


namespace webapi.DB
{
    public class DBConnection
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        // PostgeSQL-style connection string
        static string connectionstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
            "188.166.166.22",   //{0} server ip
            "5432",             //{1} server port, default: 5432
            "postgis",          //{2} username
            "postgis",          //{3} password
            "gisdb");           //{4} database name

        // Making connection with Npgsql provider
        NpgsqlConnection connection = new NpgsqlConnection(connectionstring);
        public DBConnection()
        {

        }

        #region Point

        public webapi.Models.Point GetPoint(int id)
        {
            List<webapi.Models.Point> listPoints = new List<webapi.Models.Point>();
            connection.Open();
            string sql = "SELECT gid, name, ST_AsText(geom) FROM gtest WHERE gid = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                string name = x.Field<string>("name");
                int pointID = x.Field<int>("gid");
                List<double> longlat = new List<double>();
                longlat = geomToDouble(x.Field<string>("ST_AsText"));
                double pLong = longlat.First();
                double pLat = longlat.Last();
                webapi.Models.Point p = new webapi.Models.Point(pointID, name, pLong, pLat);
                listPoints.Add(p);
            }
            connection.Close();
            return listPoints.FirstOrDefault();
        }

        public void DeletePoint(int id)
        {
            connection.Open();
            string sql = "DELETE FROM gtest WHERE gid = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void InsertPoint(int id, string name, double pLong, double pLat)
        {
            connection.Open();
            string sql = "INSERT INTO gtest(gid, name, geom) VALUES(@id, @name, ST_MakePoint(@pLong, @pLat))";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@pLong", pLong);
            cmd.Parameters.AddWithValue("@pLat", pLat);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public List<webapi.Models.Point> allPoints()
        {
            List<webapi.Models.Point> listPoints = new List<webapi.Models.Point>();
            connection.Open();
            string sql = "SELECT gid, name, ST_AsText(geom) FROM gtest";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                string name = x.Field<string>("name");
                int pointID = x.Field<int>("gid");
                List<double> longlat = new List<double>();
                longlat = geomToDouble(x.Field<string>("ST_AsText"));
                double pLong = longlat.First();
                double pLat = longlat.Last();
                webapi.Models.Point p = new webapi.Models.Point(pointID, name, pLong, pLat);
                listPoints.Add(p);
            }
            connection.Close();
            return listPoints;
        }

        public List<double> geomToDouble(string s)
        {
            List<double> dList = new List<double>();
            s = s.TrimStart('P', 'O', 'I', 'N', 'T', '(');
            s = s.TrimEnd(')');
            s = s.Replace('.', ',');
            string[] sDouble = s.Split(' ');
            foreach (string str in sDouble)
            {
                double value = double.Parse(str);
                dList.Add(value);
            }
            Console.WriteLine(dList);
            return dList;
        }

        #endregion

        #region User

        #endregion
    }


}