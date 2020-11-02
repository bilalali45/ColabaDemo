namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Subordinate
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int EntityTypeId { get; set; }

        public Employee Employee { get; set; }
    }
}