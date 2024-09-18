using System.Data;
using System.Data.SqlClient;

namespace CopyTableData.Services
{
    public static class DatabaseService
    {
        // This method is used to check if the table exists
        public static bool IsTableExist(string connectionString, string tableName)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT TOP 1 * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = @tname", conn);
            cmd.Parameters.AddWithValue("@tname", tableName);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                return true;
            else
                return false;
        }

        // This method is used to run the SQL query
        public static void RunSql(string connectionString, string sqlQuery)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();
            var cmd = new SqlCommand(sqlQuery, conn);
            cmd.ExecuteNonQuery();
        }

        // This method is used to read the data from the table
        public static List<S> ReadTable<S>(string connectionString, string query, Func<IDataRecord, S> selector)
        {
            using SqlConnection conn = new(connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Connection.Open();
            using var r = cmd.ExecuteReader();
            var items = new List<S>();
            while (r.Read())
                items.Add(selector(r));
            return items;
        }
    }
}
