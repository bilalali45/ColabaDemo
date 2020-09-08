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
    // BorrowerBankRuptcy

    public partial class BorrowerBankRuptcy : URF.Core.EF.Trackable.Entity
    {
        public int BorrowerId { get; set; } // BorrowerId (Primary key)
        public int BankRuptcyId { get; set; } // BankRuptcyId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent BankRuptcy pointed by [BorrowerBankRuptcy].([BankRuptcyId]) (FK_BorrowerBankRuptcy_BankRuptcy)
        /// </summary>
        public virtual BankRuptcy BankRuptcy { get; set; } // FK_BorrowerBankRuptcy_BankRuptcy

        /// <summary>
        /// Parent Borrower pointed by [BorrowerBankRuptcy].([BorrowerId]) (FK_BorrowerBankRuptcy_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerBankRuptcy_Borrower

        public BorrowerBankRuptcy()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
