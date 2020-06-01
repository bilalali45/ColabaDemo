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


namespace RainMaker.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // EmployeePhoneBinder
    
    public partial class EmployeePhoneBinder : URF.Core.EF.Trackable.Entity
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
