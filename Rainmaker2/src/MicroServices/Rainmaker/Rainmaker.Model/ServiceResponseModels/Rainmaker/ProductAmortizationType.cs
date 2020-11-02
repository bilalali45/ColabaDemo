using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    // ProductAmortizationType

    public class ProductAmortizationType
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
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
        ///     Child LoanApplications where [LoanApplication].[ProductAmortizationTypeId] point to this entity
        ///     (FK_LoanApplication_ProductAmortizationType)
        /// </summary>
        public ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_ProductAmortizationType

        /// <summary>
        ///     Child LoanRequests where [LoanRequest].[FirstMortgageAmortizationTypeId] point to this entity
        ///     (FK_LoanRequest_ProductAmortizationType)
        /// </summary>
        public ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_ProductAmortizationType

        /// <summary>
        ///     Child MortgageOnProperties where [MortgageOnProperty].[AmortizationTypeId] point to this entity
        ///     (FK_MortgageOnProperty_ProductAmortizationType)
        /// </summary>
        public ICollection<MortgageOnProperty> MortgageOnProperties { get; set; } // MortgageOnProperty.FK_MortgageOnProperty_ProductAmortizationType

        /// <summary>
        ///     Child Products where [Product].[AmortizationTypeId] point to this entity (FK_Product_ProductAmortizationType)
        /// </summary>
        public ICollection<Product> Products { get; set; } // Product.FK_Product_ProductAmortizationType
    }
}