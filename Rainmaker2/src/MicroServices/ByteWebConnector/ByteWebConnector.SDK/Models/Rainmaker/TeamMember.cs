namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class TeamMember
    {
        public int TeamId { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public Team Team { get; set; }
    }
}