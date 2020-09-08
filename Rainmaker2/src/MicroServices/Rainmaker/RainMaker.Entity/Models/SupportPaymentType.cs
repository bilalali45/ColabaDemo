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
    // SupportPaymentType

    public partial class SupportPaymentType : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child BorrowerSupportPayments where [BorrowerSupportPayment].[SupportPaymentTypeId] point to this entity (FK_BorrowerSupportPayment_SupportPaymentType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerSupportPayment> BorrowerSupportPayments { get; set; } // BorrowerSupportPayment.FK_BorrowerSupportPayment_SupportPaymentType

        public SupportPaymentType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 182;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BorrowerSupportPayments = new System.Collections.Generic.HashSet<BorrowerSupportPayment>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
