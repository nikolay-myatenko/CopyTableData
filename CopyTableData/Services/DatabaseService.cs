using System.Data.SqlClient;

namespace CopyTableData.Services
{
    public class DatabaseService
    {
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

        public static void RunSql(string connectionString, string sqlQuery)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();
            var cmd = new SqlCommand(sqlQuery, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
