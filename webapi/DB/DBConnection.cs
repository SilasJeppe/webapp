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

            //Uncomment this on danish systems
            //s = s.Replace('.', ',');

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
        public webapi.Models.User GetUser(int id)
        {
            List<webapi.Models.User> listUsers = new List<webapi.Models.User>();
            connection.Open();
            string sql = "SELECT id, firstname, surname, address, city, zipcode, phonenumber, email, passwordhash FROM user WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                webapi.Models.User user = new webapi.Models.User()
                {
                    ID = x.Field<int>("id"),
                    Firstname = x.Field<string>("firstname"),
                    Surname = x.Field<string>("surname"),
                    Address = x.Field<string>("address"),
                    City = x.Field<string>("city"),
                    ZipCode = x.Field<int>("zipcode"),
                    PhoneNumber = x.Field<int>("phonenumber"),
                    Email = x.Field<string>("email"),
                    password = x.Field<string>("passwordhash")
                };
                
                listUsers.Add(user);
            }
            connection.Close();
            return listUsers.FirstOrDefault();
        }

        public List<webapi.Models.User> allUsers()
        {
            List<webapi.Models.User> listUsers = new List<webapi.Models.User>();
            connection.Open();
            string sql = "SELECT id, firstname, surname, address, city, zipcode, phonenumber, email, passwordhash FROM public.user";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                webapi.Models.User user = new webapi.Models.User()
                {
                    ID = x.Field<int>("id"),
                    Firstname = x.Field<string>("firstname"),
                    Surname = x.Field<string>("surname"),
                    Address = x.Field<string>("address"),
                    City = x.Field<string>("city"),
                    ZipCode = x.Field<int>("zipcode"),
                    PhoneNumber = x.Field<int>("phonenumber"),
                    Email = x.Field<string>("email"),
                    password = x.Field<string>("passwordhash")
                };

                listUsers.Add(user);
            }
            connection.Close();
            return listUsers;
        }

        public void InsertUser(string firstname, string surname, string address, string city, int zipcode, int phonenumber, string email, string passwordhash)
        {
            connection.Open();
            string sql = "INSERT INTO public.user(firstname, surname, address, city, zipcode, phonenumber, email, passwordhash) VALUES(@firstname, @surname, @address, @city, @zipcode, @phonenumber, @email, @passwordhash)";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@zipcode", zipcode);
            cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@passwordhash", passwordhash);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteUser(int id)
        {
            connection.Open();
            string sql = "DELETE FROM user WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateUser(int id, string address, string city, int zipcode, int phonenumber, string email, string passwordhash)
        {
            connection.Open();
            string sql = "UPDATE user SET address = @address, city = @city, zipcode = @zipcode, phonenumber = @phonenumber, email = @email, passwordhash = @passwordhash WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@zipcode", zipcode);
            cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@passwordhash", passwordhash);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }

        #endregion

        #region Activity
        #endregion

        #region Route
        #endregion

    }


}