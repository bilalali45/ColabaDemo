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

    // OwnerShipInterest
    
    public partial class OwnerShipInterest : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BorrowerId { get; set; } // BorrowerId
        public int? PropertyTypeId { get; set; } // PropertyTypeId
        public int? TitleHeldWithId { get; set; } // TitleHeldWithId
        public int TenantId { get; set; } // TenantId

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [OwnerShipInterest].([BorrowerId]) (FK_OwnerShipInterest_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_OwnerShipInterest_Borrower

        /// <summary>
        /// Parent PropertyType pointed by [OwnerShipInterest].([PropertyTypeId]) (FK_OwnerShipInterest_PropertyType)
        /// </summary>
        public virtual PropertyType PropertyType { get; set; } // FK_OwnerShipInterest_PropertyType

        /// <summary>
        /// Parent TitleHeldWith pointed by [OwnerShipInterest].([TitleHeldWithId]) (FK_OwnerShipInterest_TitleHeldWith)
        /// </summary>
        public virtual TitleHeldWith TitleHeldWith { get; set; } // FK_OwnerShipInterest_TitleHeldWith

        public OwnerShipInterest()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
