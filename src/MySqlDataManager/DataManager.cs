using System;
using System.Data;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace MySqlDataManager
{
    public class DataManager
    {
        public static void ExecuteNonQuery(string sql)
        {
            using (MySqlConnection conn = DatabaseFactory.Get().GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql)
        {
            object retVal;
            using (MySqlConnection conn = DatabaseFactory.Get().GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                retVal = cmd.ExecuteScalar();
            }
            return retVal;
        }

        public static IDataReader ExecuteReader(string sql)
        {
            MySqlConnection conn = DatabaseFactory.Get().GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
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
