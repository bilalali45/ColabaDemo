namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class EmpDepartmentBinder
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }

        public Department Department { get; set; }

        public Employee Employee { get; set; }

        public Position Position { get; set; }
    }
}