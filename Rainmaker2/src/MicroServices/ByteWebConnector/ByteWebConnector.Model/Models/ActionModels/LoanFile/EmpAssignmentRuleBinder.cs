













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // EmpAssignmentRuleBinder

    public partial class EmpAssignmentRuleBinder 
    {
        public int RuleId { get; set; } // RuleId (Primary key)
        public int EmployeeId { get; set; } // EmployeeId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Employee pointed by [EmpAssignmentRuleBinder].([EmployeeId]) (FK_EmpAssignmentRuleBinder_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_EmpAssignmentRuleBinder_Employee

        /// <summary>
        /// Parent Rule pointed by [EmpAssignmentRuleBinder].([RuleId]) (FK_EmpAssignmentRuleBinder_Rule)
        /// </summary>
        public virtual Rule Rule { get; set; } // FK_EmpAssignmentRuleBinder_Rule

        public EmpAssignmentRuleBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
