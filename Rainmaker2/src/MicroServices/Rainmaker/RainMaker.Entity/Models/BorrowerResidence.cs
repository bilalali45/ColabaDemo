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
    // BorrowerResidence

    public partial class BorrowerResidence : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LoanAddressId { get; set; } // LoanAddressId
        public int? BorrowerId { get; set; } // BorrowerId
        public int? OwnershipTypeId { get; set; } // OwnershipTypeId
        public int? TypeId { get; set; } // TypeId
        public decimal? MonthlyRent { get; set; } // MonthlyRent
        public string AccountInNameOf { get; set; } // AccountInNameOf (length: 500)
        public int? LandLordContactId { get; set; } // LandLordContactId
        public int? LandLordAddressId { get; set; } // LandLordAddressId
        public string LandLordComments { get; set; } // LandLordComments (length: 2500)
        public System.DateTime? FromDate { get; set; } // FromDate
        public System.DateTime? ToDate { get; set; } // ToDate
        public bool? IsSameAsPropertyAddress { get; set; } // IsSameAsPropertyAddress

        // Foreign keys

        /// <summary>
        /// Parent AddressInfo pointed by [BorrowerResidence].([LandLordAddressId]) (FK_BorrowerResidence_AddressInfo)
        /// </summary>
        public virtual AddressInfo LandLordAddress { get; set; } // FK_BorrowerResidence_AddressInfo

        /// <summary>
        /// Parent AddressInfo pointed by [BorrowerResidence].([LoanAddressId]) (FK_BorrowerResidence_LoanAddress)
        /// </summary>
        public virtual AddressInfo LoanAddress { get; set; } // FK_BorrowerResidence_LoanAddress

        /// <summary>
        /// Parent Borrower pointed by [BorrowerResidence].([BorrowerId]) (FK_BorrowerResidence_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerResidence_Borrower

        /// <summary>
        /// Parent LoanContact pointed by [BorrowerResidence].([LandLordContactId]) (FK_BorrowerResidence_LoanContact)
        /// </summary>
        public virtual LoanContact LoanContact { get; set; } // FK_BorrowerResidence_LoanContact

        /// <summary>
        /// Parent OwnershipType pointed by [BorrowerResidence].([OwnershipTypeId]) (FK_BorrowerResidence_OwnershipType)
        /// </summary>
        public virtual OwnershipType OwnershipType { get; set; } // FK_BorrowerResidence_OwnershipType

        public BorrowerResidence()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
