













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ProductCatalog

    public partial class ProductCatalog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ProductId { get; set; } // ProductId
        public string Code { get; set; } // Code (length: 100)
        public string Title { get; set; } // Title (length: 500)
        public string ShortTitle { get; set; } // ShortTitle (length: 500)
        public string Description { get; set; } // Description (length: 4000)
        public string LongDescription { get; set; } // LongDescription
        public string ImagePath { get; set; } // ImagePath (length: 200)
        public int? CmsPageId { get; set; } // CmsPageId

        // Foreign keys

        /// <summary>
        /// Parent CmsPage pointed by [ProductCatalog].([CmsPageId]) (FK_ProductCatalog_CmsPage)
        /// </summary>
        public virtual CmsPage CmsPage { get; set; } // FK_ProductCatalog_CmsPage

        /// <summary>
        /// Parent Product pointed by [ProductCatalog].([ProductId]) (FK_ProductCatalog_Product)
        /// </summary>
        public virtual Product Product { get; set; } // FK_ProductCatalog_Product

        public ProductCatalog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
