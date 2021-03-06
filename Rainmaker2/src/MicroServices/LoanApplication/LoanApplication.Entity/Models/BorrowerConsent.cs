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

    // BorrowerConsent
    
    public partial class BorrowerConsent : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ConsentTypeId { get; set; } // ConsentTypeId
        public int? BorrowerId { get; set; } // BorrowerId
        public int? LoanApplicationId { get; set; } // LoanApplicationId
        public string ConsentText { get; set; } // ConsentText (length: 500)
        public string Email { get; set; } // Email (length: 150)
        public string FirstName { get; set; } // FirstName (length: 300)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 300)
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool? IsAccepted { get; set; } // IsAccepted
        public string IpAddress { get; set; } // IpAddress (length: 50)
        public int TenantId { get; set; } // TenantId

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [BorrowerConsent].([BorrowerId]) (FK_BorrowerConsent_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerConsent_Borrower

        /// <summary>
        /// Parent ConsentType pointed by [BorrowerConsent].([ConsentTypeId]) (FK_BorrowerConsent_ConsentType)
        /// </summary>
        public virtual ConsentType ConsentType { get; set; } // FK_BorrowerConsent_ConsentType

        /// <summary>
        /// Parent LoanApplication pointed by [BorrowerConsent].([LoanApplicationId]) (FK_BorrowerConsent_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_BorrowerConsent_LoanApplication

        public BorrowerConsent()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
