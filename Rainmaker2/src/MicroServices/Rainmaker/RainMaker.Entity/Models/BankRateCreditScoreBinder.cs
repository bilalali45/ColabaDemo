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

    // BankRateCreditScoreBinder
    
    public partial class BankRateCreditScoreBinder : URF.Core.EF.Trackable.Entity
    {
        public int BankRateProductId { get; set; } // BankRateProductId (Primary key)
        public int CreditScoreId { get; set; } // CreditScoreId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent BankRateProduct pointed by [BankRateCreditScoreBinder].([BankRateProductId]) (FK_BankRateCreditScoreBinder_BankRateProduct)
        /// </summary>
        public virtual BankRateProduct BankRateProduct { get; set; } // FK_BankRateCreditScoreBinder_BankRateProduct

        /// <summary>
        /// Parent CreditScore pointed by [BankRateCreditScoreBinder].([CreditScoreId]) (FK_BankRateCreditScoreBinder_CreditScore)
        /// </summary>
        public virtual CreditScore CreditScore { get; set; } // FK_BankRateCreditScoreBinder_CreditScore

        public BankRateCreditScoreBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>