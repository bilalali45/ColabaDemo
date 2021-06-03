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

    // BorrowerLiability
    
    public partial class BorrowerLiability : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BorrowerId { get; set; } // BorrowerId
        public int? AddressInfoId { get; set; } // AddressInfoId
        public string CompanyName { get; set; } // CompanyName (length: 150)
        public string AccountNumber { get; set; } // AccountNumber (length: 50)
        public int? LiabilityTypeId { get; set; } // LiabilityTypeId
        public decimal? MonthlyPayment { get; set; } // MonthlyPayment
        public decimal? Balance { get; set; } // Balance
        public bool? WillBePaidByThisLoan { get; set; } // WillBePaidByThisLoan
        public int? RemainingMonth { get; set; } // RemainingMonth
        public int TenantId { get; set; } // TenantId

        // Foreign keys

        /// <summary>
        /// Parent AddressInfo pointed by [BorrowerLiability].([AddressInfoId]) (FK_BorrowerLiability_AddressInfo)
        /// </summary>
        public virtual AddressInfo AddressInfo { get; set; } // FK_BorrowerLiability_AddressInfo

        /// <summary>
        /// Parent Borrower pointed by [BorrowerLiability].([BorrowerId]) (FK_BorrowerLiability_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerLiability_Borrower

        /// <summary>
        /// Parent LiabilityType pointed by [BorrowerLiability].([LiabilityTypeId]) (FK_BorrowerLiability_LiabilityType)
        /// </summary>
        public virtual LiabilityType LiabilityType { get; set; } // FK_BorrowerLiability_LiabilityType

        public BorrowerLiability()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
