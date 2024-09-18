using CopyTableData.Services;
using CopyTableData.Settings;

Console.WriteLine("CopyTableData Started!");

try
{
    // Initialize items where we will store the data
    var items = CopySettings.InitItems();

    // Check if the source table exists
    if (!DatabaseService.IsTableExist(CopySettings.SourceConnectionString, CopySettings.SourceTableName))
        // Read the data from the source table
        items = DatabaseService.ReadTable(CopySettings.SourceConnectionString, CopySettings.SourceTableSelectScript, CopySettings.ItemSelector);
    else Console.WriteLine($"Table {CopySettings.SourceTableName} does not exist!");

    // Check if the copy table exists
    if (!DatabaseService.IsTableExist(CopySettings.CopyConnectionString, CopySettings.CopyTableName))
        // Create the copy table if it does not exist
        DatabaseService.RunSql(CopySettings.CopyConnectionString, CopySettings.CopyTableCreateScript);

    // Check if there is data to copy
    if (items.Count > 0)
        // Copy the data to the copy table
        DatabaseService.BulkInsert(CopySettings.CopyConnectionString, CopySettings.CopyTableName, items);
    else Console.WriteLine("No data to copy!");
}
catch (Exception ex)
{
    Console.WriteLine($"Application Error! Error Message: {ex.Message}");
}

Console.WriteLine("CopyTableData Completed!");
Console.ReadLine();
