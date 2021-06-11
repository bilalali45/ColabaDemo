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

    // TermsCondition
    
    public partial class TermsCondition : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? TenantId { get; set; } // TenantId
        public int? BranchId { get; set; } // BranchId
        public int? TermTypeId { get; set; } // TermTypeId
        public string TermsContent { get; set; } // TermsContent
        public bool? IsActive { get; set; } // IsActive

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [TermsCondition].([BranchId]) (FK_TermsCondition_Branch_Id)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_TermsCondition_Branch_Id

        /// <summary>
        /// Parent Tenant pointed by [TermsCondition].([TenantId]) (FK_TermsCondition_Tenant_Id)
        /// </summary>
        public virtual Tenant Tenant { get; set; } // FK_TermsCondition_Tenant_Id

        /// <summary>
        /// Parent TermsConditionType pointed by [TermsCondition].([TermTypeId]) (FK_TermsCondition_TermsConditionType_Id)
        /// </summary>
        public virtual TermsConditionType TermsConditionType { get; set; } // FK_TermsCondition_TermsConditionType_Id

        public TermsCondition()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>