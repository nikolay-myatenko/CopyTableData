namespace CopyTableData.Models
{
    // Person DTO
    // This class is used to store the data from the source table
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public long Phone { get; set; }
    }
}
