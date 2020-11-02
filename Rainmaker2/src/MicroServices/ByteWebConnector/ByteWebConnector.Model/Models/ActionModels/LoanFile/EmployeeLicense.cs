













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // EmployeeLicense

    public partial class EmployeeLicense 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? EmployeeId { get; set; } // EmployeeId
        public int? StateId { get; set; } // StateId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public string LicenseNo { get; set; } // LicenseNo (length: 50)
        public System.DateTime? RenewDateUtc { get; set; } // RenewDateUtc
        public System.DateTime? ExpiryDateUtc { get; set; } // ExpiryDateUtc

        // Foreign keys

        /// <summary>
        /// Parent Employee pointed by [EmployeeLicense].([EmployeeId]) (FK_EmployeeLicense_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_EmployeeLicense_Employee

        /// <summary>
        /// Parent State pointed by [EmployeeLicense].([StateId]) (FK_EmployeeLicense_State)
        /// </summary>
        public virtual State State { get; set; } // FK_EmployeeLicense_State

        public EmployeeLicense()
        {
            EntityTypeId = 52;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
