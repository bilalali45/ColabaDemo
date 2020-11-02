













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // EmployeeCsrLoBinder

    public partial class EmployeeCsrLoBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? EmployeeCsrId { get; set; } // EmployeeCsrId
        public int? StateId { get; set; } // StateId
        public int? EmployeeLoId { get; set; } // EmployeeLoId

        // Foreign keys

        /// <summary>
        /// Parent Employee pointed by [EmployeeCsrLoBinder].([EmployeeCsrId]) (FK_EmployeeCsrLoBinder_Employee)
        /// </summary>
        public virtual Employee EmployeeCsr { get; set; } // FK_EmployeeCsrLoBinder_Employee

        /// <summary>
        /// Parent Employee pointed by [EmployeeCsrLoBinder].([EmployeeLoId]) (FK_EmployeeCsrLoBinder_EmployeeLo)
        /// </summary>
        public virtual Employee EmployeeLo { get; set; } // FK_EmployeeCsrLoBinder_EmployeeLo

        /// <summary>
        /// Parent State pointed by [EmployeeCsrLoBinder].([StateId]) (FK_EmployeeCsrLoBinder_State)
        /// </summary>
        public virtual State State { get; set; } // FK_EmployeeCsrLoBinder_State

        public EmployeeCsrLoBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
