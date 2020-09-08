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
    // LoanRequestMergeLog

    public partial class LoanRequestMergeLog : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int LoanRequestId { get; set; } // LoanRequestId
        public int OpportunityId { get; set; } // OpportunityId
        public System.DateTime MergeDateUtc { get; set; } // MergeDateUtc

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [LoanRequestMergeLog].([LoanRequestId]) (FK_LoanRequestMergeLog_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_LoanRequestMergeLog_LoanRequest

        public LoanRequestMergeLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
