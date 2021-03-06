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

    // TenantUrl
    
    public partial class TenantUrl : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Url { get; set; } // Url (length: 100)
        public int? TenantId { get; set; } // TenantId
        public int? BranchId { get; set; } // BranchId
        public int TypeId { get; set; } // TypeId

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [TenantUrl].([BranchId]) (FK_WebUrl_Branch_Id)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_WebUrl_Branch_Id

        /// <summary>
        /// Parent Tenant pointed by [TenantUrl].([TenantId]) (FK_WebUrl_Tenant_Id)
        /// </summary>
        public virtual Tenant Tenant { get; set; } // FK_WebUrl_Tenant_Id

        public TenantUrl()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
