namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class EmpAssignmentRuleBinder
    {
        public int RuleId { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public Rule Rule { get; set; }
    }
}