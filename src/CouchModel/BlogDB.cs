using System;
using System.Configuration;

namespace Model
{
    public class BlogDB
    {
        private static readonly SharpCouch.DB _db;
        private static readonly string _server;
        private static readonly string _database;

        static BlogDB()
        {
            _server = "http://" + 
                ConfigurationManager.AppSettings["couchdb_server"] + ":" + 
                ConfigurationManager.AppSettings["couchdb_port"];
            _database = ConfigurationManager.AppSettings["post_db_name"];
            _db = new SharpCouch.DB();
            CheckDatabase();
        }

        public static void CheckDatabase()
        {
            string[] databases = _db.GetDatabases(_server);
            foreach (string db in databases)
            {
                if (db == _database) return;
            }
            _db.CreateDatabase(_server, _database);
        }

        public static string[] GetDatabases()
        {
            return _db.GetDatabases(_server);
        }

        public static string GetDocument(string docid)
        {
            return _db.GetDocument(_server, _database, docid);
        }

        public static string GetDocument(string docid, string startkey, string endkey)
        {
            return _db.GetDocument(_server, _database, docid, startkey, endkey);
        }

        public static void CreateDocument(string content)
        {
            _db.CreateDocument(_server, _database, content);
        }

        public static void CreateDocument(string id, string content)
        {
            _db.CreateDocument(_server, _database, id, content);
        }

        public static void DeleteDatabase()
        {
            _db.DeleteDatabase(_server, _database);
        }

        public static string ExecTempView(string map, string reduce, string startKey, string endKey)
        {
            return _db.ExecTempView(_server, _database, map, reduce, startKey, endKey);
        }
    }
}
