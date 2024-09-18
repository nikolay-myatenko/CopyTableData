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

        // This method is used to bulk insert the data to the table
        public static void BulkInsert<T>(string connectionString, string destinationTableName, List<T> items)
        {
            var table = new DataTable();

            var typeProperties = typeof(T).GetProperties();

            foreach (var property in typeProperties)
            {
                table.Columns.Add(new DataColumn { ColumnName = property.Name, DataType = property.PropertyType });
            }

            foreach (var item in items)
            {
                var row = table.NewRow();

                foreach (var property in typeProperties)
                {
                    row[property.Name] = property.GetValue(item);
                }

                table.Rows.Add(row);
            }

            using var bulkCopy = new SqlBulkCopy(connectionString);
            bulkCopy.DestinationTableName = destinationTableName;
            bulkCopy.WriteToServer(table);
        }

        // This method is used to get the default type
        public static Type DefaultType => typeof(string);
    }
}
