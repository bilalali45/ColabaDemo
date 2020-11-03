













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // EmpDepartmentBinder

    public partial class EmpDepartmentBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int EmployeeId { get; set; } // EmployeeId
        public int DepartmentId { get; set; } // DepartmentId
        public int PositionId { get; set; } // PositionId

        // Foreign keys

        /// <summary>
        /// Parent Department pointed by [EmpDepartmentBinder].([DepartmentId]) (FK_EmpDepartmentBinder_Department)
        /// </summary>
        public virtual Department Department { get; set; } // FK_EmpDepartmentBinder_Department

        /// <summary>
        /// Parent Employee pointed by [EmpDepartmentBinder].([EmployeeId]) (FK_EmpDepartmentBinder_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_EmpDepartmentBinder_Employee

        /// <summary>
        /// Parent Position pointed by [EmpDepartmentBinder].([PositionId]) (FK_EmpDepartmentBinder_Position)
        /// </summary>
        public virtual Position Position { get; set; } // FK_EmpDepartmentBinder_Position

        public EmpDepartmentBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
