using CopyTableData.Models;
using System.Data;

namespace CopyTableData.Settings
{
    public static class CopySettings
    {
        // Source and Copy connection strings
        public const string SourceConnectionString = "Data Source=(local);Initial Catalog=Test;Integrated Security=SSPI;Persist Security Info=true;MultipleActiveResultSets=True";
        public const string CopyConnectionString = "Data Source=(local);Initial Catalog=Test;Integrated Security=SSPI;Persist Security Info=true;MultipleActiveResultSets=True";

        // Source and Copy table names
        public const string SourceTableName = "dbo.PeopleSource";
        public const string CopyTableName = "dbo.PeopleCopy";

        // Copy table create script
        public const string CopyTableCreateScript = @"
            CREATE TABLE [dbo].[PeopleCopy](
            [Id] [uniqueidentifier] NOT NULL,
            [FirstName] [varchar](50) NOT NULL,
            [LastName] [varchar](50) NOT NULL,
            [BirthDate] [datetime] NOT NULL,
            [Phone] [bigint] NULL,
            CONSTRAINT [PK_PeopleCopy] PRIMARY KEY CLUSTERED 
            (
                [Id] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]";

        // Source table select script
        public const string SourceTableSelectScript = $"SELECT * FROM {SourceTableName};";

        // Item selector
        // This method is used to select the data from the IDataRecord and map it to the Person object
        public static Person ItemSelector(IDataRecord r) => new()
        {
            Id = r.GetGuid(0),
            FirstName = r.GetString(1),
            LastName = r.GetString(2),
            BirthDate = r.GetDateTime(3),
            Phone = r.GetInt64(4)
        };

        // Initialize items
        // This method is used to initialize the items list
        public static List<Person> InitItems() => new();
    }
}
