













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TeamMember

    public partial class TeamMember 
    {
        public int TeamId { get; set; } // TeamId (Primary key)
        public int EmployeeId { get; set; } // EmployeeId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Employee pointed by [TeamMember].([EmployeeId]) (FK_TeamMember_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_TeamMember_Employee

        /// <summary>
        /// Parent Team pointed by [TeamMember].([TeamId]) (FK_TeamMember_Team)
        /// </summary>
        public virtual Team Team { get; set; } // FK_TeamMember_Team

        public TeamMember()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
