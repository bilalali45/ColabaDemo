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

    // BusinessUnitPhone
    
    public partial class BusinessUnitPhone : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int BusinessUnitId { get; set; } // BusinessUnitId
        public int CompanyPhoneInfoId { get; set; } // CompanyPhoneInfoId
        public int? TypeId { get; set; } // TypeId

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [BusinessUnitPhone].([BusinessUnitId]) (FK_BusinessUnitPhone_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_BusinessUnitPhone_BusinessUnit

        /// <summary>
        /// Parent CompanyPhoneInfo pointed by [BusinessUnitPhone].([CompanyPhoneInfoId]) (FK_BusinessUnitPhone_CompanyPhoneInfo)
        /// </summary>
        public virtual CompanyPhoneInfo CompanyPhoneInfo { get; set; } // FK_BusinessUnitPhone_CompanyPhoneInfo

        public BusinessUnitPhone()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>