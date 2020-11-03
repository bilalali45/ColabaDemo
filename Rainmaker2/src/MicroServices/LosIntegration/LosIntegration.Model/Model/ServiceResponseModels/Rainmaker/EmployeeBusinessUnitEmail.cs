













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // EmployeeBusinessUnitEmail

    public partial class EmployeeBusinessUnitEmail 
    {
        public int Id { get; set; } // Id (Primary key)
        public int EmployeeId { get; set; } // EmployeeId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int EmailAccountId { get; set; } // EmailAccountId
        public int TypeId { get; set; } // TypeId

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [EmployeeBusinessUnitEmail].([BusinessUnitId]) (FK_EmployeeBusinessUnitEmail_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_EmployeeBusinessUnitEmail_BusinessUnit

        /// <summary>
        /// Parent EmailAccount pointed by [EmployeeBusinessUnitEmail].([EmailAccountId]) (FK_EmployeeBusinessUnitEmail_EmailAccount)
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; } // FK_EmployeeBusinessUnitEmail_EmailAccount

        /// <summary>
        /// Parent Employee pointed by [EmployeeBusinessUnitEmail].([EmployeeId]) (FK_EmployeeBusinessUnitEmail_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_EmployeeBusinessUnitEmail_Employee

        public EmployeeBusinessUnitEmail()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
