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
    // CurrentRateView

    public partial class CurrentRateView : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ProductId { get; set; } // ProductId
        public int? RateTypeId { get; set; } // RateTypeId
        public System.DateTime? PriceDateUtc { get; set; } // PriceDateUtc
        public decimal? Price { get; set; } // Price
        public decimal? Rate { get; set; } // Rate
        public decimal? Apr { get; set; } // Apr
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int RateParameterId { get; set; } // RateParameterId (Primary key)
        public int? LeadSourceId { get; set; } // LeadSourceId
        public string PrdouctName { get; set; } // PrdouctName (Primary key) (length: 150)
        public string LoanTypeName { get; set; } // LoanTypeName (length: 150)
        public string ProductFamilyName { get; set; } // ProductFamilyName (length: 150)
        public int? ProductAdRateTypeId { get; set; } // ProductAdRateTypeId
        public int? ProductFamilyId { get; set; } // ProductFamilyId
        public decimal? MaxLoanAmount { get; set; } // MaxLoanAmount
        public int? DisplayOrder { get; set; } // DisplayOrder
        public int? PFamilyDisplayOrder { get; set; } // PFamilyDisplayOrder
        public string ProductAliasName { get; set; } // ProductAliasName (length: 150)
        public string ProductDescription { get; set; } // ProductDescription (length: 500)
        public string ProductDetail { get; set; } // ProductDetail (length: 1000)

        public CurrentRateView()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
