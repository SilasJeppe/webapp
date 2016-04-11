using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace webapi.DB
{
    public class DBUser
    {
        private NpgsqlConnection con;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private DBActivity dbActivity;
        public DBUser()
        {
            dbActivity = new DBActivity();
            con = DBConnection.GetInstance().GetConnection();
        }
        public webapi.Models.User GetUser(int id)
        {
            List<webapi.Models.User> listUsers = new List<webapi.Models.User>();
            con.Open();
            string sql = "SELECT * FROM public.user WHERE public.user.id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            dt.Reset();
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
                    ActivityList = dbActivity.GetAllActivityForUser(x.Field<int>("id"))
                };

                listUsers.Add(user);
            }
            con.Close();
            return listUsers.FirstOrDefault();
        }

        public List<webapi.Models.User> GetUsers()
        {
            List<webapi.Models.User> listUsers = new List<webapi.Models.User>();
            con.Open();
            string sql = "SELECT id, firstname, surname, address, city, zipcode, phonenumber, email, passwordhash FROM public.user";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
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
                    ActivityList = dbActivity.GetAllActivityForUser(x.Field<int>("id"))
                };

                listUsers.Add(user);
            }
            con.Close();
            return listUsers;
        }

        public void InsertUser(string firstname, string surname, string address, string city, int zipcode, int phonenumber, string email, string passwordhash)
        {
            con.Open();
            string sql = "INSERT INTO public.user(firstname, surname, address, city, zipcode, phonenumber, email, passwordhash) VALUES(@firstname, @surname, @address, @city, @zipcode, @phonenumber, @email, @passwordhash)";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@zipcode", zipcode);
            cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@passwordhash", passwordhash);
            int i = cmd.ExecuteNonQuery();
            con.Close();
        }
        //Deletes a User and his Activities
        public void DeleteUser(int id)
        {
            con.Open();
            string sql = "DELETE FROM public.user WHERE id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            int i = cmd.ExecuteNonQuery();
            dbActivity.DeleteActivity(id);
            con.Close();
        }

        public void UpdateUser(string address, string city, int zipcode, int phonenumber, string email, string passwordhash)
        {
            con.Open();
            string sql = "UPDATE public.user SET address = @address, city = @city, zipcode = @zipcode, phonenumber = @phonenumber, email = @email, passwordhash = @passwordhash WHERE email = @email";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@zipcode", zipcode);
            cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@passwordhash", passwordhash);
            int i = cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}