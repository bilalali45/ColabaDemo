namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class EmployeeCsrLoBinder
    {
        public int Id { get; set; }
        public int? EmployeeCsrId { get; set; }
        public int? StateId { get; set; }
        public int? EmployeeLoId { get; set; }

        public Employee EmployeeCsr { get; set; }

        public Employee EmployeeLo { get; set; }

        public State State { get; set; }
    }
}