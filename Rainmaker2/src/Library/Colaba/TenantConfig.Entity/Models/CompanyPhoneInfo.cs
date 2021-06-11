// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace TenantConfig.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // CompanyPhoneInfo
    
    public partial class CompanyPhoneInfo : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Phone { get; set; } // Phone (length: 150)
        public bool IsActive { get; set; } // IsActive
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Reverse navigation

        /// <summary>
        /// Child BranchPhoneBinders where [BranchPhoneBinder].[CompanyPhoneInfoId] point to this entity (FK_BranchPhoneBinder_CompanyPhoneInfo_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BranchPhoneBinder> BranchPhoneBinders { get; set; } // BranchPhoneBinder.FK_BranchPhoneBinder_CompanyPhoneInfo_Id
        /// <summary>
        /// Child EmployeePhoneBinders where [EmployeePhoneBinder].[CompanyPhoneInfoId] point to this entity (FK_EmployeePhoneBinder_CompanyPhoneInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeePhoneBinder> EmployeePhoneBinders { get; set; } // EmployeePhoneBinder.FK_EmployeePhoneBinder_CompanyPhoneInfo

        public CompanyPhoneInfo()
        {
            BranchPhoneBinders = new System.Collections.Generic.HashSet<BranchPhoneBinder>();
            EmployeePhoneBinders = new System.Collections.Generic.HashSet<EmployeePhoneBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>