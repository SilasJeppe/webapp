//Class to create a connection to the PostgreSQL PostGIS database
using System;
using System.Data;
using Npgsql;


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
        public static string connectionstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
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
        //Singleton constructor
        public static DBConnection GetInstance()
        {
            if (instance == null)
            {
                instance = new DBConnection();
            }
            return instance;
        }

        public NpgsqlConnection GetConnection()
        {
            return connection;
        }

        //Method that checks if connection is closed, and opens if it is
        public void Open()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            else if (connection.State == ConnectionState.Broken)
            {
                connection.Close();
                connection.Open();
            }
        }

        //Method that checks if connection is open, and closes if it is
        public void Close()
        {
            if (connection.State == ConnectionState.Open || connection.State == ConnectionState.Broken || connection.State == ConnectionState.Connecting)
            {
                connection.Close();
            }
        }
    }
}
