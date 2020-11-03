using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    // ProductFamily

    public class ProductFamily
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public decimal? MinLoanAmount { get; set; } // MinLoanAmount
        public decimal? MaxLoanAmount { get; set; } // MaxLoanAmount
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        ///     Child MortgageOnProperties where [MortgageOnProperty].[ProductFamilyId] point to this entity
        ///     (FK_MortgageOnProperty_ProductFamily)
        /// </summary>
        public ICollection<MortgageOnProperty> MortgageOnProperties { get; set; } // MortgageOnProperty.FK_MortgageOnProperty_ProductFamily

        /// <summary>
        ///     Child Products where [Product].[ProductFamilyId] point to this entity (FK_Product_ProductFamily)
        /// </summary>
        public ICollection<Product> Products { get; set; } // Product.FK_Product_ProductFamily

        /// <summary>
        ///     Child RateServiceParameters where [RateServiceParameter].[ProductFamilyId] point to this entity
        ///     (FK_RateServiceParameter_ProductFamily)
        /// </summary>
        public ICollection<RateServiceParameter> RateServiceParameters { get; set; } // RateServiceParameter.FK_RateServiceParameter_ProductFamily
    }
}