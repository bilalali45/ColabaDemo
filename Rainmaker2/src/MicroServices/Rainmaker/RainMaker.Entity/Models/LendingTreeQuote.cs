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
    // LendingTreeQuote

    public partial class LendingTreeQuote : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LendingTreeLeadId { get; set; } // LendingTreeLeadId
        public string VendorId { get; set; } // VendorId (length: 50)
        public string ProductId { get; set; } // ProductId (length: 50)
        public decimal? Rate { get; set; } // Rate
        public decimal? Price { get; set; } // Price
        public decimal? Apr { get; set; } // Apr
        public string LtProductName { get; set; } // LtProductName (length: 150)
        public decimal? MonthlyPayment { get; set; } // MonthlyPayment

        // Foreign keys

        /// <summary>
        /// Parent LendingTreeLead pointed by [LendingTreeQuote].([LendingTreeLeadId]) (FK_LendingTreeQuote_LendingTreeLead)
        /// </summary>
        public virtual LendingTreeLead LendingTreeLead { get; set; } // FK_LendingTreeQuote_LendingTreeLead

        public LendingTreeQuote()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
