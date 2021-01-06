namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Subordinate
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int EntityTypeId { get; set; }

        public Employee Employee { get; set; }
    }
}