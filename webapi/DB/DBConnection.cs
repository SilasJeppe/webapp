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
using NpgsqlTypes;


namespace webapi.DB
{
    public class DBConnection
    {

        #region Connection
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private static NpgsqlConnection connection;
        private static DBConnection instance = null;
        // PostgeSQL-style connection string
        static string connectionstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
            "188.166.166.22",   //{0} server ip
            "5432",             //{1} server port, default: 5432
            "postgis",          //{2} username
            "postgis",          //{3} password
            "gisdb");           //{4} database name


        #endregion
        // Making connection with Npgsql provider
        
        private DBConnection()
        {
            connection = new NpgsqlConnection(connectionstring);
        }

        public static DBConnection GetInstance()
        {
            if(instance == null)
            {
                instance = new DBConnection();
            }
            return instance;
        }

        public NpgsqlConnection GetConnection()
        {
            return connection;
        }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
