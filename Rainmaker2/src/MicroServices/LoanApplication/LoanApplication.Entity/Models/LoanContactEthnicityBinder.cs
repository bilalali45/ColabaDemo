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

    // LoanContactEthnicityBinder
    
    public partial class LoanContactEthnicityBinder : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int LoanContactId { get; set; } // LoanContactId
        public int EthnicityId { get; set; } // EthnicityId
        public int? EthnicityDetailId { get; set; } // EthnicityDetailId
        public string OtherEthnicity { get; set; } // OtherEthnicity (length: 150)
        public int TenantId { get; set; } // TenantId

        // Foreign keys

        /// <summary>
        /// Parent Ethnicity pointed by [LoanContactEthnicityBinder].([EthnicityId]) (FK_LoanContactEthnicityBinder_Ethnicity)
        /// </summary>
        public virtual Ethnicity Ethnicity { get; set; } // FK_LoanContactEthnicityBinder_Ethnicity

        /// <summary>
        /// Parent EthnicityDetail pointed by [LoanContactEthnicityBinder].([EthnicityDetailId]) (FK_LoanContactEthnicityBinder_EthnicityDetail)
        /// </summary>
        public virtual EthnicityDetail EthnicityDetail { get; set; } // FK_LoanContactEthnicityBinder_EthnicityDetail

        /// <summary>
        /// Parent LoanContact pointed by [LoanContactEthnicityBinder].([LoanContactId]) (FK_LoanContactEthnicityBinder_LoanContact)
        /// </summary>
        public virtual LoanContact LoanContact { get; set; } // FK_LoanContactEthnicityBinder_LoanContact

        public LoanContactEthnicityBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
