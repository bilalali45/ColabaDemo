using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class PropertyTax
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FeeNumber { get; set; }
        public int? FeeTypeId { get; set; }
        public int? PaidById { get; set; }
        public int? FeeBlockId { get; set; }
        public int? StateId { get; set; }
        public int EscrowEntityTypeId { get; set; }
        public int RoundTypeId { get; set; }
        public int CalcTypeId { get; set; }
        public int? FormulaId { get; set; }
        public decimal Value { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public int? CalcBaseOnId { get; set; }
        public int? RangSetId { get; set; }

        public ICollection<PaymentOn> PaymentOns { get; set; }

        public ICollection<TaxCityBinder> TaxCityBinders { get; set; }

        public ICollection<TaxCountyBinder> TaxCountyBinders { get; set; }

        public EscrowEntityType EscrowEntityType { get; set; }

        public FeeBlock FeeBlock { get; set; }

        public FeeType FeeType { get; set; }

        public Formula Formula { get; set; }

        public PaidBy PaidBy { get; set; }

        public RangeSet RangeSet { get; set; }

        public State State { get; set; }
    }
}