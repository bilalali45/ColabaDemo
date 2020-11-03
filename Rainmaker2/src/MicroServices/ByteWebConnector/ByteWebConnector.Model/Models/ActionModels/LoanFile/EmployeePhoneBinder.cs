













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // EmployeePhoneBinder

    public partial class EmployeePhoneBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int EmployeeId { get; set; } // EmployeeId
        public int CompanyPhoneInfoId { get; set; } // CompanyPhoneInfoId
        public int TypeId { get; set; } // TypeId

        // Foreign keys

        /// <summary>
        /// Parent CompanyPhoneInfo pointed by [EmployeePhoneBinder].([CompanyPhoneInfoId]) (FK_EmployeePhoneBinder_CompanyPhoneInfo)
        /// </summary>
        public virtual CompanyPhoneInfo CompanyPhoneInfo { get; set; } // FK_EmployeePhoneBinder_CompanyPhoneInfo

        /// <summary>
        /// Parent Employee pointed by [EmployeePhoneBinder].([EmployeeId]) (FK_EmployeePhoneBinder_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_EmployeePhoneBinder_Employee

        public EmployeePhoneBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
