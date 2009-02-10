using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text.RegularExpressions;

namespace gsw.helpers.EntLibDataHelpers
{
    public class DataManager
    {
        #region ExecuteNonQuery
        public static void ExecuteNonQuery(string sql, string dbConfig, int commandTimeout)
        {
            Database db = (dbConfig == string.Empty) ?
                DatabaseFactory.CreateDatabase() :
                DatabaseFactory.CreateDatabase(dbConfig);

            DbCommand command = db.GetSqlStringCommand(ConvertNull(sql));
            if (commandTimeout > 0) command.CommandTimeout = commandTimeout;

            db.ExecuteNonQuery(command);
        }

        // Overloads follow
        public static void ExecuteNonQuery(string sql, params object[] parameters)
        {
            ExecuteNonQuery(string.Format(sql, parameters), string.Empty, -1);
        }
        public static void ExecuteNonQuery(string sql, int commandTimeout, params object[] parameters)
        {
            ExecuteNonQuery(string.Format(sql, parameters), string.Empty, commandTimeout);
        }
        public static void ExecuteNonQuery(string sql)
        {
            ExecuteNonQuery(sql, string.Empty, -1);
        }
        public static void ExecuteNonQuery(string sql, int commandTimeout)
        {
            ExecuteNonQuery(sql, string.Empty, commandTimeout);
        }
        public static void ExecuteNonQuery(string sql, string dbConfig, int commandTimeout, params object[] parameters)
        {
            ExecuteNonQuery(string.Format(sql, parameters), dbConfig, commandTimeout);
        }
        public static void ExecuteNonQuery(string sql, string dbConfig)
        {
            ExecuteNonQuery(sql, dbConfig, -1);
        }
        #endregion

        #region ExecuteScalar
        public static object ExecuteScalar(string sql, string dbConfig, int commandTimeout)
        {
            Database db = (dbConfig == string.Empty) ?
                DatabaseFactory.CreateDatabase() :
                DatabaseFactory.CreateDatabase(dbConfig);

            DbCommand command = db.GetSqlStringCommand(ConvertNull(sql));
            if (commandTimeout > 0) command.CommandTimeout = commandTimeout;

            return db.ExecuteScalar(command);
        }

        // Overloads follow
        public static object ExecuteScalar(string sql, params object[] parameters)
        {
            return ExecuteScalar(string.Format(sql, parameters), string.Empty, -1);
        }
        public static object ExecuteScalar(string sql, int commandTimeout, params object[] parameters)
        {
            return ExecuteScalar(string.Format(sql, parameters), string.Empty, commandTimeout);
        }
        public static object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, string.Empty, -1);
        }
        public static object ExecuteScalar(string sql, int commandTimeout)
        {
            return ExecuteScalar(sql, string.Empty, commandTimeout);
        }
        public static object ExecuteScalar(string sql, string dbConfig, params object[] parameters)
        {
            return ExecuteScalar(string.Format(sql, parameters), dbConfig, -1);
        }
        public static object ExecuteScalar(string sql, string dbConfig, int commandTimeout, params object[] parameters)
        {
            return ExecuteScalar(string.Format(sql, parameters), dbConfig, commandTimeout);
        }
        public static object ExecuteScalar(string sql, string dbConfig)
        {
            return ExecuteScalar(sql, dbConfig, -1);
        }
        #endregion

        #region ExecuteReader
        public static IDataReader ExecuteReader(string sql, string dbConfig, int commandTimeout)
        {
            Database db = (dbConfig == string.Empty) ?
                DatabaseFactory.CreateDatabase() :
                DatabaseFactory.CreateDatabase(dbConfig);

            DbCommand command = db.GetSqlStringCommand(ConvertNull(sql));
            if (commandTimeout > 0) command.CommandTimeout = commandTimeout;

            return db.ExecuteReader(command);
        }

        // Overloads follow
        public static IDataReader ExecuteReader(string sql, params object[] parameters)
        {
            return ExecuteReader(string.Format(sql, parameters), string.Empty, -1);
        }
        public static IDataReader ExecuteReader(string sql, int commandTimeout, params object[] parameters)
        {
            return ExecuteReader(string.Format(sql, parameters), string.Empty, commandTimeout);
        }
        public static IDataReader ExecuteReader(string sql)
        {
            return ExecuteReader(sql, string.Empty, -1);
        }
        public static IDataReader ExecuteReader(string sql, int commandTimeout)
        {
            return ExecuteReader(sql, string.Empty, commandTimeout);
        }
        public static IDataReader ExecuteReader(string sql, string dbConfig, params object[] parameters)
        {
            return ExecuteReader(string.Format(sql, parameters), dbConfig, -1);
        }
        public static IDataReader ExecuteReader(string sql, string dbConfig, int commandTimeout, params object[] parameters)
        {
            return ExecuteReader(string.Format(sql, parameters), dbConfig, commandTimeout);
        }
        public static IDataReader ExecuteReader(string sql, string dbConfig)
        {
            return ExecuteReader(sql, dbConfig, -1);
        }
        #endregion

        #region Helper Methods
        public static string SetSQL(string sql, params object[] parameters)
        {
            for (int pass = 0; pass < parameters.Length; pass++)
            {
                if (parameters[pass] != null)
                {
                    if (parameters[pass].GetType() == typeof(string)) parameters[pass] = CleanStringForSQL((string)parameters[pass]);
                    if (parameters[pass].GetType() == typeof(DateTime)) parameters[pass] = FormatDateForSQL((DateTime)parameters[pass]);
                    if (parameters[pass].GetType() == typeof(bool)) parameters[pass] = FormatBoolForSQL((bool)parameters[pass]);
                }
                else
                {
                    parameters[pass] = "null";
                }
            }
            return string.Format(sql, parameters);
        }

        public static string CleanStringForSQL(string value)
        {
            return (value == null) ? "null" : string.Format("'{0}'", value.Replace("'", "''"));
        }

        public static string FormatNullableIntForSQL(int value)
        {
            return (value < 0) ? null : value.ToString();
        }

        public static string FormatDateForSQL(DateTime date)
        {
            if (date == DateTime.MinValue) return "null";
            return "'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "'";
        }

        public static int FormatBoolForSQL(bool value)
        {
            return (value) ? 1 : 0;
        }

        public static string ConvertNull(string sql)
        {
            Regex noUpdate = new Regex("update ");
            if (noUpdate.IsMatch(sql)) return sql;
            return sql.Replace("= null", "is null");
        }

        public static string ConvertStringColumn(IDataReader reader, string columnName)
        {
            object value = reader[columnName];
            try
            {
                return (value.GetType() == typeof(System.DBNull)) ? null : (string)value;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ConvertStringColumn error encountered on column {0}", columnName), ex);
            }
        }

        public static double ConvertDoubleColumn(IDataReader reader, string columnName)
        {
            object value = reader[columnName];
            try
            {
                return (value.GetType() == typeof(System.DBNull)) ? 0 : Convert.ToDouble(value);
            }
            catch { }
            return 0;
        }

        public static int ConvertIntColumn(IDataReader reader, string columnName)
        {
            object value = reader[columnName];
            try
            {

                return (value.GetType() == typeof(System.DBNull)) ? 0 : Convert.ToInt32(value);
            }
            catch { }
            return 0;
        }
        #endregion
    }
}
