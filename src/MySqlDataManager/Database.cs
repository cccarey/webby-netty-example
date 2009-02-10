using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace MySqlDataManager
{
    public class Database
    {
        private string _name;
        private string _connString;
        private MySqlConnection _poolConn;

        public Database(string name)
        {
            this.Name = name;
            _connString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            if (_connString.Contains("pooling=true"))
            {
                _poolConn = new MySqlConnection(_connString);
                _poolConn.Open();
            }
        }

        ~Database()
        {
            if (_poolConn != null)
            {
                _poolConn.Close();
                _poolConn = null;
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(_connString);
            conn.Open();
            return conn;
        }
    }
}
