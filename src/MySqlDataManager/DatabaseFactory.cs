using System;
using System.Collections.Generic;
using System.Configuration;

namespace MySqlDataManager
{
    public class DatabaseFactory
    {
        static readonly Dictionary<string, Database> _databases = new Dictionary<string, Database>();
        static readonly string _defaultKey = null;

        static DatabaseFactory()
        {
            foreach (ConnectionStringSettings connString in ConfigurationManager.ConnectionStrings)
            {
                if (connString.Name.StartsWith("mysql"))
                {
                    if (_defaultKey == null) _defaultKey = connString.Name;
                    _databases.Add(connString.Name, new Database(connString.Name));
                }
            }

        }

        DatabaseFactory() { }

        public static Database Get(string name)
        {
            return _databases[name];
        }

        public static Database Get()
        {
            return _databases[_defaultKey];
        }
    }
}
