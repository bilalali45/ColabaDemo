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


namespace LoanApplicationDb.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // InActiveSetupItemsSectionWise
    
    public partial class InActiveSetupItemsSectionWise : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string EntityName { get; set; } // EntityName (length: 50)
        public int EntityRefId { get; set; } // EntityRefId
        public int SectionId { get; set; } // SectionId
        public int? TenantId { get; set; } // TenantId

        // Foreign keys

        /// <summary>
        /// Parent InActiveSetupItemSection pointed by [InActiveSetupItemsSectionWise].([SectionId]) (FK_InActiveSetupItemsSectionWise_InActiveSetupItemSection_Id)
        /// </summary>
        public virtual InActiveSetupItemSection InActiveSetupItemSection { get; set; } // FK_InActiveSetupItemsSectionWise_InActiveSetupItemSection_Id

        public InActiveSetupItemsSectionWise()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
