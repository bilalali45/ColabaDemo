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

    // CurrentRate
    
    public partial class CurrentRate : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ProductId { get; set; } // ProductId
        public int? RateTypeId { get; set; } // RateTypeId
        public int? RateArchiveId { get; set; } // RateArchiveId
        public System.DateTime? PriceDateUtc { get; set; } // PriceDateUtc
        public int? InvestorId { get; set; } // InvestorId
        public decimal? WholesalePrice { get; set; } // WholesalePrice
        public decimal? WholesaleRate { get; set; } // WholesaleRate
        public decimal? Price { get; set; } // Price
        public decimal? Rate { get; set; } // Rate
        public decimal? Apr { get; set; } // Apr
        public decimal? LenderFees { get; set; } // LenderFees
        public decimal? DiscountCharges { get; set; } // DiscountCharges
        public decimal? ClosingCost { get; set; } // ClosingCost
        public decimal? Mi { get; set; } // Mi
        public decimal? Piti { get; set; } // Piti
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? QuoteResultId { get; set; } // QuoteResultId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int RateParameterId { get; set; } // RateParameterId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? DisplayOrder { get; set; } // DisplayOrder

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [CurrentRate].([BusinessUnitId]) (FK_CurrentRate_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_CurrentRate_BusinessUnit

        /// <summary>
        /// Parent Investor pointed by [CurrentRate].([InvestorId]) (FK_CurrentRate_Investor)
        /// </summary>
        public virtual Investor Investor { get; set; } // FK_CurrentRate_Investor

        /// <summary>
        /// Parent Product pointed by [CurrentRate].([ProductId]) (FK_CurrentRate_Product)
        /// </summary>
        public virtual Product Product { get; set; } // FK_CurrentRate_Product

        /// <summary>
        /// Parent RateServiceParameter pointed by [CurrentRate].([RateParameterId]) (FK_CurrentRate_RateServiceParameter)
        /// </summary>
        public virtual RateServiceParameter RateServiceParameter { get; set; } // FK_CurrentRate_RateServiceParameter

        public CurrentRate()
        {
            EntityTypeId = 70;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
