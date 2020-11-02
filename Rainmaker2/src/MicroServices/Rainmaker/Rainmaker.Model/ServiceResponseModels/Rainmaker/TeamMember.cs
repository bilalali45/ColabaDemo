namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class TeamMember
    {
        public int TeamId { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public Team Team { get; set; }
    }
}