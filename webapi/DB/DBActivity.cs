//Class for connection the Activity class to the database
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using webapi.Models;

namespace webapi.DB
{
    public class DBActivity
    {
        private NpgsqlConnection con;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private DBRoute dbRoute;

        public DBActivity()
        {
            con = DBConnection.GetInstance().GetConnection();
            dbRoute = new DBRoute();
        }

        //Gets specific Activity, used for delete
        public webapi.Models.Activity GetActivity(int id)
        {
            con.Open();
            List<webapi.Models.Activity> listActivity = new List<webapi.Models.Activity>();
            string sql = "SELECT * FROM public.activity WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Reset();
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
                    Time = x.Field<TimeSpan>("Time"),
                    StartAddress = x.Field<string>("startaddress"),
                    EndAddress = x.Field<string>("endaddress"),
                    UserID = x.Field<int>("userid"),
                    Route = dbRoute.GetRouteByActivityID(x.Field<int>("id"))
                };
                con.Close();
                listActivity.Add(activity);
            }
            return listActivity.FirstOrDefault();
        }

        //Gets all activities from database
        public List<webapi.Models.Activity> GetAllActivity()
        {
            con.Open();
            List<webapi.Models.Activity> listActivity = new List<webapi.Models.Activity>();
            string sql = "SELECT * FROM public.activity";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Reset();
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
                    Time = x.Field<TimeSpan>("Time"),
                    StartAddress = x.Field<string>("startaddress"),
                    EndAddress = x.Field<string>("endaddress"),
                    UserID = x.Field<int>("userid"),
                    Route = dbRoute.GetRouteByActivityID(x.Field<int>("id"))
                };
                listActivity.Add(activity);
            }
            con.Close();
            return listActivity;
        }

        //Method that get all activities for a User given the User ID
        public List<webapi.Models.Activity> GetAllActivityForUser(int id)
        {
            List<webapi.Models.Activity> listActivity = new List<webapi.Models.Activity>();
            string sql = "SELECT * FROM public.activity WHERE userid = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Reset();
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
                    Time = x.Field<TimeSpan>("Time"),
                    StartAddress = x.Field<string>("startaddress"),
                    EndAddress = x.Field<string>("endaddress"),
                    UserID = x.Field<int>("userid"),
                    Route = dbRoute.GetRouteByActivityID(x.Field<int>("id"))
                };
                listActivity.Add(activity);
            }
            return listActivity;
        }

        //Method that inserts an Activity given correct input
        public void InsertActivity(Activity a)
        {
            con.Open();
            string sql = "INSERT INTO public.activity(name, description, distance, date, time, startaddress, endaddress, userid) VALUES (@name, @description, @distance, @date, @time, @startaddress, @endaddress, @userid)";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", a.Name);
            cmd.Parameters.AddWithValue("@description", a.Description);
            cmd.Parameters.AddWithValue("@distance", a.Distance);
            cmd.Parameters.AddWithValue("@date", a.Date);
            cmd.Parameters.AddWithValue("@time", a.Time);
            cmd.Parameters.AddWithValue("@startaddress", a.StartAddress);
            cmd.Parameters.AddWithValue("@endaddress", a.EndAddress);
            cmd.Parameters.AddWithValue("@userid", a.UserID);
            int i = cmd.ExecuteNonQuery();
            a.Route.ActivityID = GetAllActivityForUser(a.UserID).LastOrDefault().ID;
            dbRoute.InsertRoute(a.Route);
            con.Close();
        }

        //Deletes an activity and the Route and Points 
        public void DeleteActivity(int id)
        {
            con.Open();
            string sql = "DELETE FROM public.activity WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            int i = cmd.ExecuteNonQuery();
            con.Close();
        }

        //Updates an Activity - NOT FULLY IMPLEMENTET
        public void UpdateActivity(int id, string name, string description, double distance, DateTime date, DateTime time, string startaddress, string endaddress, int userid)
        {
            con.Open();
            string sql = "UPDATE public.activity SET name = @name, description = @description, distance = @distance, date = @date, time = @time, startaddress = @startaddress, endaddress = @endaddress  WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
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
            con.Close();
        }
    }
}