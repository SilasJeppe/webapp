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

        public List<webapi.Models.Point> allPoints()
        {
            List<webapi.Models.Point> listPoints = new List<webapi.Models.Point>();
            connection.Open();
            string sql = "SELECT gid, name, ST_AsText(geom) FROM gtest;";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, connection);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
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
            foreach(string str in sDouble)
            {
                double value = double.Parse(str);
                dList.Add(value);
            }
            Console.WriteLine(dList);
            return dList;
        }


    }


}