using System.Data;
using System.Data.SqlClient;

namespace CopyTableData.Services
{
    public class BulkInsertService
    {
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

            using (var bulkCopy = new SqlBulkCopy(connectionString))
            {
                bulkCopy.DestinationTableName = destinationTableName;
                bulkCopy.WriteToServer(table);
            }
        }

        public static Type DefaultType => typeof(string);
    }
}
