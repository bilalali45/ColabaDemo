













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // PropertyTax

    public partial class PropertyTax 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 200)
        public string Description { get; set; } // Description (length: 500)
        public string FeeNumber { get; set; } // FeeNumber (length: 50)
        public int? FeeTypeId { get; set; } // FeeTypeId
        public int? PaidById { get; set; } // PaidById
        public int? FeeBlockId { get; set; } // FeeBlockId
        public int? StateId { get; set; } // StateId
        public int EscrowEntityTypeId { get; set; } // EscrowEntityTypeId
        public int RoundTypeId { get; set; } // RoundTypeId
        public int CalcTypeId { get; set; } // CalcTypeId
        public int? FormulaId { get; set; } // FormulaId
        public decimal Value { get; set; } // Value
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
        public int? CalcBaseOnId { get; set; } // CalcBaseOnId
        public int? RangSetId { get; set; } // RangSetId

        // Reverse navigation

        /// <summary>
        /// Child PaymentOns where [PaymentOn].[PropertyTaxId] point to this entity (FK_PaymentOn_PropertyTax)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PaymentOn> PaymentOns { get; set; } // PaymentOn.FK_PaymentOn_PropertyTax
        /// <summary>
        /// Child TaxCityBinders where [TaxCityBinder].[PropertyTaxId] point to this entity (FK_TaxCityBinder_PropertyTax)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TaxCityBinder> TaxCityBinders { get; set; } // TaxCityBinder.FK_TaxCityBinder_PropertyTax
        /// <summary>
        /// Child TaxCountyBinders where [TaxCountyBinder].[PropertyTaxId] point to this entity (FK_TaxCountyBinder_PropertyTax)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TaxCountyBinder> TaxCountyBinders { get; set; } // TaxCountyBinder.FK_TaxCountyBinder_PropertyTax

        // Foreign keys

        /// <summary>
        /// Parent EscrowEntityType pointed by [PropertyTax].([EscrowEntityTypeId]) (FK_PropertyTax_EscrowEntityType)
        /// </summary>
        public virtual EscrowEntityType EscrowEntityType { get; set; } // FK_PropertyTax_EscrowEntityType

        /// <summary>
        /// Parent FeeBlock pointed by [PropertyTax].([FeeBlockId]) (FK_PropertyTax_FeeBlock)
        /// </summary>
        public virtual FeeBlock FeeBlock { get; set; } // FK_PropertyTax_FeeBlock

        /// <summary>
        /// Parent FeeType pointed by [PropertyTax].([FeeTypeId]) (FK_PropertyTax_FeeType)
        /// </summary>
        public virtual FeeType FeeType { get; set; } // FK_PropertyTax_FeeType

        /// <summary>
        /// Parent Formula pointed by [PropertyTax].([FormulaId]) (FK_PropertyTax_Formula)
        /// </summary>
        public virtual Formula Formula { get; set; } // FK_PropertyTax_Formula

        /// <summary>
        /// Parent PaidBy pointed by [PropertyTax].([PaidById]) (FK_PropertyTax_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_PropertyTax_PaidBy

        /// <summary>
        /// Parent RangeSet pointed by [PropertyTax].([RangSetId]) (FK_PropertyTax_RangeSet)
        /// </summary>
        public virtual RangeSet RangeSet { get; set; } // FK_PropertyTax_RangeSet

        /// <summary>
        /// Parent State pointed by [PropertyTax].([StateId]) (FK_PropertyTax_State)
        /// </summary>
        public virtual State State { get; set; } // FK_PropertyTax_State

        public PropertyTax()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 104;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            PaymentOns = new System.Collections.Generic.HashSet<PaymentOn>();
            TaxCityBinders = new System.Collections.Generic.HashSet<TaxCityBinder>();
            TaxCountyBinders = new System.Collections.Generic.HashSet<TaxCountyBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
