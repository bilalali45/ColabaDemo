













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // CompanyPhoneInfo

    public partial class CompanyPhoneInfo 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Phone { get; set; } // Phone (length: 150)
        public string ServiceName { get; set; } // ServiceName (length: 200)
        public bool IsDeleted { get; set; } // IsDeleted
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public int? ServiceSettingId { get; set; } // ServiceSettingId
        public string Type { get; set; } // Type (length: 50)
        public int? DefaultStatusId { get; set; } // DefaultStatusId

        // Reverse navigation

        /// <summary>
        /// Child BranchPhones where [BranchPhone].[CompanyPhoneInfoId] point to this entity (FK_BranchPhone_CompanyPhoneInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BranchPhone> BranchPhones { get; set; } // BranchPhone.FK_BranchPhone_CompanyPhoneInfo
        /// <summary>
        /// Child BranchPhoneBinders where [BranchPhoneBinder].[CompanyPhoneInfoId] point to this entity (FK_BranchPhoneBinder_CompanyPhoneInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BranchPhoneBinder> BranchPhoneBinders { get; set; } // BranchPhoneBinder.FK_BranchPhoneBinder_CompanyPhoneInfo
        /// <summary>
        /// Child BusinessUnitPhones where [BusinessUnitPhone].[CompanyPhoneInfoId] point to this entity (FK_BusinessUnitPhone_CompanyPhoneInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BusinessUnitPhone> BusinessUnitPhones { get; set; } // BusinessUnitPhone.FK_BusinessUnitPhone_CompanyPhoneInfo
        /// <summary>
        /// Child BusinessUnitPhoneBinders where [BusinessUnitPhoneBinder].[CompanyPhoneInfoId] point to this entity (FK_BusinessUnitPhoneBinder_CompanyPhoneInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BusinessUnitPhoneBinder> BusinessUnitPhoneBinders { get; set; } // BusinessUnitPhoneBinder.FK_BusinessUnitPhoneBinder_CompanyPhoneInfo
        /// <summary>
        /// Child EmployeePhoneBinders where [EmployeePhoneBinder].[CompanyPhoneInfoId] point to this entity (FK_EmployeePhoneBinder_CompanyPhoneInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeePhoneBinder> EmployeePhoneBinders { get; set; } // EmployeePhoneBinder.FK_EmployeePhoneBinder_CompanyPhoneInfo

        // Foreign keys

        /// <summary>
        /// Parent ServicesSetting pointed by [CompanyPhoneInfo].([ServiceSettingId]) (FK_CompanyPhoneInfo_ServicesSetting)
        /// </summary>
        public virtual ServicesSetting ServicesSetting { get; set; } // FK_CompanyPhoneInfo_ServicesSetting

        public CompanyPhoneInfo()
        {
            IsDeleted = false;
            EntityTypeId = 1;
            IsDefault = false;
            DisplayOrder = 0;
            IsActive = true;
            IsSystem = false;
            BranchPhones = new System.Collections.Generic.HashSet<BranchPhone>();
            BranchPhoneBinders = new System.Collections.Generic.HashSet<BranchPhoneBinder>();
            BusinessUnitPhones = new System.Collections.Generic.HashSet<BusinessUnitPhone>();
            BusinessUnitPhoneBinders = new System.Collections.Generic.HashSet<BusinessUnitPhoneBinder>();
            EmployeePhoneBinders = new System.Collections.Generic.HashSet<EmployeePhoneBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>