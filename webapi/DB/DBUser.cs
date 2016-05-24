//Class for connection the User class to the database
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace webapi.DB
{
    public class DBUser
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private DBActivity dbActivity;

        public DBUser()
        {
            dbActivity = new DBActivity();
            //con = DBConnection.GetInstance().GetConnection();
        }

        //Method that gets a User from the database based on an ID
        public webapi.Models.User GetUser(int id)
        {
            List<webapi.Models.User> listUsers = new List<webapi.Models.User>();
            using (NpgsqlConnection con = new NpgsqlConnection(DBConnection.connectionstring))
            {
                if (con.State != ConnectionState.Open)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    con.Open();
                }
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
            }
            return listUsers.FirstOrDefault();
        }

        //Method that gets a user from database based on email
        public webapi.Models.User GetUserFromEmail(string email)
        {
            List<webapi.Models.User> listUsers = new List<webapi.Models.User>();
            using (NpgsqlConnection con = new NpgsqlConnection(DBConnection.connectionstring))
            {
                if (con.State != ConnectionState.Open)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    con.Open();
                }
                string sql = "SELECT * FROM public.user WHERE public.user.email = @email";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@email", email);
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
            }
            return listUsers.FirstOrDefault();
        }

        //Method that gets all Users from database
        public List<webapi.Models.User> GetUsers()
        {
            List<webapi.Models.User> listUsers = new List<webapi.Models.User>();
            using (NpgsqlConnection con = new NpgsqlConnection(DBConnection.connectionstring))
            {
                if (con.State != ConnectionState.Open)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    con.Open();
                }
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
            }
            return listUsers;
        }

        //Method that insert a User in the database given all the necessary input - with hashed password
        public void InsertUser(string firstname, string surname, string address, string city, int zipcode, int phonenumber, string email, string passwordhash)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(DBConnection.connectionstring))
            {
                if (con.State != ConnectionState.Open)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    con.Open();
                }
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
            }
        }

        //Deletes a User and his Activities
        public void DeleteUser(int id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(DBConnection.connectionstring))
            {
                if (con.State != ConnectionState.Open)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    con.Open();
                }
                string sql = "DELETE FROM public.user WHERE id = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                int i = cmd.ExecuteNonQuery();
            }
            dbActivity.DeleteActivity(id);
        }

        //Method for updating a User - NOT FULLY IMPLEMENTET
        public void UpdateUser(string address, string city, int zipcode, int phonenumber, string email, string passwordhash)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(DBConnection.connectionstring))
            {
                if (con.State != ConnectionState.Open)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    con.Open();
                }
                string sql = "UPDATE public.user SET address = @address, city = @city, zipcode = @zipcode, phonenumber = @phonenumber, email = @email, passwordhash = @passwordhash WHERE email = @email";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@zipcode", zipcode);
                cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@passwordhash", passwordhash);
                int i = cmd.ExecuteNonQuery();
            }
        }
    }
}