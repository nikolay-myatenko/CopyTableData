namespace CopyTableData.Models
{
    public class PersonTable
    {
        public const string TableName = "People";

        public const string TableCreateScript = @"
            CREATE TABLE [dbo].[People](
            [Id] [uniqueidentifier] NOT NULL,
            [FirstName] [varchar](50) NOT NULL,
            [LastName] [varchar](50) NOT NULL,
            [BirthDate] [datetime] NOT NULL,
            [Phone] [bigint] NULL,
            CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
            (
                [Id] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]";
    }
}
