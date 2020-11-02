using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    // ProductType

    public class ProductType
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        ///     Child LeadSourceDefaultProducts where [LeadSourceDefaultProduct].[ProductTypeId] point to this entity
        ///     (FK_LeadSourceDefaultProduct_ProductType)
        /// </summary>
        public ICollection<LeadSourceDefaultProduct> LeadSourceDefaultProducts { get; set; } // LeadSourceDefaultProduct.FK_LeadSourceDefaultProduct_ProductType

        /// <summary>
        ///     Child LoanRequestProductTypes where [LoanRequestProductType].[ProductTypeId] point to this entity
        ///     (FK_LoanRequestProductType_ProductType)
        /// </summary>
        public ICollection<LoanRequestProductType> LoanRequestProductTypes { get; set; } // LoanRequestProductType.FK_LoanRequestProductType_ProductType

        /// <summary>
        ///     Child OpportunityDesireRates where [OpportunityDesireRate].[ProductTypeId] point to this entity
        ///     (FK_OpportunityDesireRate_ProductType)
        /// </summary>
        public ICollection<OpportunityDesireRate> OpportunityDesireRates { get; set; } // OpportunityDesireRate.FK_OpportunityDesireRate_ProductType

        /// <summary>
        ///     Child Products where [Product].[ProductTypeId] point to this entity (FK_Product_ProductType)
        /// </summary>
        public ICollection<Product> Products { get; set; } // Product.FK_Product_ProductType
    }
}