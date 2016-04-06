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

        public List<webapi.Models.Point> GetPointsByRouteID(int id)
        {
            List<webapi.Models.Point> listPoints = new List<webapi.Models.Point>();
            connection.Open();
            string sql = "SELECT id, ST_AsText(geom), routeid FROM public.point WHERE routeid = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                int ID = x.Field<int>("id");
                List<double> longlat = new List<double>();
                longlat = geomToDouble(x.Field<string>("ST_AsText"));
                double pLong = longlat.First();
                double pLat = longlat.Last();
                int RouteID = x.Field<int>("routeid");
                webapi.Models.Point p = new webapi.Models.Point(ID, pLong, pLat, RouteID);
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
            string sql = "SELECT * FROM public.user WHERE public.user.id = @id";
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
                    password = x.Field<string>("passwordhash"),
                    ActivityList = GetAllActivityForUser(x.Field<int>("id"))
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
            string sql = "DELETE FROM public.user WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateUser(string address, string city, int zipcode, int phonenumber, string email, string passwordhash)
        {
            connection.Open();
            string sql = "UPDATE public.user SET address = @address, city = @city, zipcode = @zipcode, phonenumber = @phonenumber, email = @email, passwordhash = @passwordhash WHERE email = @email";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
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
        public webapi.Models.Activity GetActivity(int id)
        {
            List<webapi.Models.Activity> listActivity = new List<webapi.Models.Activity>();
            connection.Open();
            string sql = "SELECT * FROM public.activity WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                webapi.Models.Activity activity = new webapi.Models.Activity()
                {
                    ID = x.Field<int>("id"),
                    Name = x.Field<string>("name"),
                    Description = x.Field<string>("description"),
                    Distance = x.Field<double>("distance"),
                    Date = x.Field<DateTime>("Date"),
                    Time = x.Field<DateTime>("Time"),
                    StartAddress = x.Field<string>("startaddress"),
                    EndAddress = x.Field<string>("endaddress"),
                    UserID = x.Field<int>("userid"),
                    Route = GetRouteByActivityID(id)
                };

                listActivity.Add(activity);
            }
            connection.Close();
            return listActivity.FirstOrDefault();
        }

        public List<webapi.Models.Activity> GetAllActivityForUser(int id)
        {
            List<webapi.Models.Activity> listActivity = new List<webapi.Models.Activity>();
            connection.Open();
            string sql = "SELECT * FROM public.activity WHERE userid = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                webapi.Models.Activity activity = new webapi.Models.Activity()
                {
                    ID = x.Field<int>("id"),
                    Name = x.Field<string>("name"),
                    Description = x.Field<string>("description"),
                    Distance = x.Field<double>("distance"),
                    Date = x.Field<DateTime>("Date"),
                    Time = x.Field<DateTime>("Time"),
                    StartAddress = x.Field<string>("startaddress"),
                    EndAddress = x.Field<string>("endaddress"),
                    UserID = x.Field<int>("userid"),
                    Route = GetRouteByActivityID(x.Field<int>("id"))
                };

                listActivity.Add(activity);
            }
            connection.Close();
            return listActivity;
        }

        public void InsertActivity(string name, string description, double distance, DateTime date, DateTime time, string startaddress, string endaddress, int userid)
        {
            connection.Open();
            string sql = "INSERT INTO public.activity(name, description, distance, date, time, startaddress, endaddress, userid) VALUES (@name, @description, @distance, @date, @time, @startaddress, @endaddress, @userid)";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@distance", distance);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@startaddress", startaddress);
            cmd.Parameters.AddWithValue("@endaddress", endaddress);
            cmd.Parameters.AddWithValue("@userid", userid);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteActivity(int id)
        {
            connection.Open();
            string sql = "DELETE FROM public.activity WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateActivity(int id, string name, string description, double distance, DateTime date, DateTime time, string startaddress, string endaddress, int userid)
        {
            connection.Open();
            string sql = "UPDATE public.activity SET name = @name, description = @description, distance = @distance, date = @date, time = @time, startaddress = @startaddress, endaddress = @endaddress  WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@distance", distance);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@startaddress", startaddress);
            cmd.Parameters.AddWithValue("@endaddress", endaddress);
            cmd.Parameters.AddWithValue("@userid", userid);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }
        #endregion

        #region Route

        public webapi.Models.Route GetRouteByActivityID(int id)
        {
            List<webapi.Models.Route> listRoute = new List<webapi.Models.Route>();
            connection.Open();
            string sql = "SELECT * FROM public.route WHERE activityid = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow x in list)
            {
                webapi.Models.Route route = new webapi.Models.Route()
                {
                    ID = x.Field<int>("id"),
                    ActivityID = x.Field<int>("activityid"),
                    PointList = GetPointsByRouteID(x.Field<int>("id"))
                };

                listRoute.Add(route);
            }
            connection.Close();
            return listRoute.FirstOrDefault();
        }


        #endregion

        #region Point
        #endregion


    }

}
