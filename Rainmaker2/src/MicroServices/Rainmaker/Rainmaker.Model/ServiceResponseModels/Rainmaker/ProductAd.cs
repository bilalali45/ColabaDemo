using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    // ProductAd

    public class ProductAd
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ProductId { get; set; } // ProductId
        public int? RateTypeId { get; set; } // RateTypeId
        public int? DisplayOrder { get; set; } // DisplayOrder
        public DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public string TpId { get; set; } // TpId (length: 50)

        // Foreign keys

        /// <summary>
        ///     Parent Product pointed by [ProductAd].([ProductId]) (FK_ProductAd_Product)
        /// </summary>
        public Product Product { get; set; } // FK_ProductAd_Product
    }
}