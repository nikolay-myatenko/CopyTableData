using CopyTableData.Models;
using CopyTableData.Services;

Console.WriteLine("CopyTableData Started!");

var databaseName = "Test";

var connectionStringFrom = "Data Source=(local);Initial Catalog=Test;Integrated Security=SSPI;Persist Security Info=true;MultipleActiveResultSets=True";
var sourceTableName = "People";

var connectionStringTo = "Data Source=(local);Initial Catalog=Test;Integrated Security=SSPI;Persist Security Info=true;MultipleActiveResultSets=True";
var destinationTableName = "People";

var items = new List<Person>
{
    new()
    {
        Id = Guid.NewGuid(),
        FirstName = "Mykola",
        LastName = "Mykola",
        BirthDate = DateTime.Parse("1985-09-25"),
        Phone = 380965510277
    }
};

if (!DatabaseService.IsTableExist(connectionStringTo, PersonTable.TableName))
    DatabaseService.RunSql(connectionStringTo, PersonTable.TableCreateScript);

BulkInsertService.BulkInsert(connectionStringTo, destinationTableName, items);

Console.WriteLine("CopyTableData Completed!");
Console.ReadLine();
