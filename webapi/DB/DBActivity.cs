//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace webapi.DB
//{
//    public class DBActivity
//    {
//        public List<webapi.Models.Activity> GetAllActivity()
//        {
//            List<webapi.Models.Activity> listActivity = new List<webapi.Models.Activity>();
//            connection.Open();
//            string sql = "SELECT * FROM public.activity";
//            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
//            NpgsqlDataReader dr = cmd.ExecuteReader();
//            dt.Load(dr);
//            List<DataRow> list = dt.AsEnumerable().ToList();
//            foreach (DataRow x in list)
//            {
//                webapi.Models.Activity activity = new webapi.Models.Activity()
//                {
//                    ID = x.Field<int>("id"),
//                    Name = x.Field<string>("name"),
//                    Description = x.Field<string>("description"),
//                    Distance = x.Field<double>("distance"),
//                    Date = x.Field<DateTime>("Date"),
//                    Time = x.Field<TimeSpan>("Time"),
//                    StartAddress = x.Field<string>("startaddress"),
//                    EndAddress = x.Field<string>("endaddress"),
//                    UserID = x.Field<int>("userid"),
//                    //Route = GetRouteByActivityID(x.Field<int>("id"))
//                };

//                listActivity.Add(activity);
//            }
//            connection.Close();
//            return listActivity;
//        }

//        public webapi.Models.Activity GetActivity(int id)
//        {
//            List<webapi.Models.Activity> listActivity = new List<webapi.Models.Activity>();
//            connection.Open();
//            string sql = "SELECT * FROM public.activity WHERE id = @id";
//            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
//            cmd.Parameters.AddWithValue("@id", id);
//            NpgsqlDataReader dr = cmd.ExecuteReader();
//            dt.Load(dr);
//            List<DataRow> list = dt.AsEnumerable().ToList();
//            foreach (DataRow x in list)
//            {
//                webapi.Models.Activity activity = new webapi.Models.Activity()
//                {
//                    ID = x.Field<int>("id"),
//                    Name = x.Field<string>("name"),
//                    Description = x.Field<string>("description"),
//                    Distance = x.Field<double>("distance"),
//                    Date = x.Field<DateTime>("Date"),
//                    Time = x.Field<TimeSpan>("Time"),
//                    StartAddress = x.Field<string>("startaddress"),
//                    EndAddress = x.Field<string>("endaddress"),
//                    UserID = x.Field<int>("userid"),
//                    //Route = GetRouteByActivityID(x.Field<int>("id"))
//                };

//                listActivity.Add(activity);
//            }
//            connection.Close();
//            return listActivity.FirstOrDefault();
//        }

//        public List<webapi.Models.Activity> GetAllActivityForUser(int id)
//        {
//            List<webapi.Models.Activity> listActivity = new List<webapi.Models.Activity>();
//            connection.Open();
//            string sql = "SELECT * FROM public.activity WHERE userid = @id";
//            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
//            cmd.Parameters.AddWithValue("@id", id);
//            NpgsqlDataReader dr = cmd.ExecuteReader();
//            dt.Load(dr);
//            List<DataRow> list = dt.AsEnumerable().ToList();
//            foreach (DataRow x in list)
//            {
//                webapi.Models.Activity activity = new webapi.Models.Activity()
//                {
//                    ID = x.Field<int>("id"),
//                    Name = x.Field<string>("name"),
//                    Description = x.Field<string>("description"),
//                    Distance = x.Field<double>("distance"),
//                    Date = x.Field<DateTime>("Date"),
//                    Time = x.Field<TimeSpan>("Time"),
//                    StartAddress = x.Field<string>("startaddress"),
//                    EndAddress = x.Field<string>("endaddress"),
//                    UserID = x.Field<int>("userid"),
//                    // Route = GetRouteByActivityID(x.Field<int>("id"))
//                };

//                listActivity.Add(activity);
//            }
//            connection.Close();
//            return listActivity;
//        }

//        public void InsertActivity(string name, string description, double distance, DateTime date, DateTime time, string startaddress, string endaddress, int userid)
//        {
//            connection.Open();
//            string sql = "INSERT INTO public.activity(name, description, distance, date, time, startaddress, endaddress, userid) VALUES (@name, @description, @distance, @date, @time, @startaddress, @endaddress, @userid)";
//            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
//            cmd.Parameters.AddWithValue("@name", name);
//            cmd.Parameters.AddWithValue("@description", description);
//            cmd.Parameters.AddWithValue("@distance", distance);
//            cmd.Parameters.AddWithValue("@date", date);
//            cmd.Parameters.AddWithValue("@time", time);
//            cmd.Parameters.AddWithValue("@startaddress", startaddress);
//            cmd.Parameters.AddWithValue("@endaddress", endaddress);
//            cmd.Parameters.AddWithValue("@userid", userid);
//            int i = cmd.ExecuteNonQuery();
//            connection.Close();
//        }

//        public void DeleteActivity(int id)
//        {
//            connection.Open();
//            string sql = "DELETE FROM public.activity WHERE id = @id";
//            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
//            cmd.Parameters.AddWithValue("@id", id);
//            int i = cmd.ExecuteNonQuery();
//            connection.Close();
//        }

//        public void UpdateActivity(int id, string name, string description, double distance, DateTime date, DateTime time, string startaddress, string endaddress, int userid)
//        {
//            connection.Open();
//            string sql = "UPDATE public.activity SET name = @name, description = @description, distance = @distance, date = @date, time = @time, startaddress = @startaddress, endaddress = @endaddress  WHERE id = @id";
//            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
//            cmd.Parameters.AddWithValue("@id", id);
//            cmd.Parameters.AddWithValue("@name", name);
//            cmd.Parameters.AddWithValue("@description", description);
//            cmd.Parameters.AddWithValue("@distance", distance);
//            cmd.Parameters.AddWithValue("@date", date);
//            cmd.Parameters.AddWithValue("@time", time);
//            cmd.Parameters.AddWithValue("@startaddress", startaddress);
//            cmd.Parameters.AddWithValue("@endaddress", endaddress);
//            cmd.Parameters.AddWithValue("@userid", userid);
//            int i = cmd.ExecuteNonQuery();
//            connection.Close();
//        }
//    }
//}