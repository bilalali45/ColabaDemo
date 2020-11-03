













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // CmsPage

    public partial class CmsPage 
    {
        public int Id { get; set; } // Id (Primary key)
        public string SystemName { get; set; } // SystemName (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int IncludeInSitemap { get; set; } // IncludeInSitemap
        public bool? IsPasswordProtected { get; set; } // IsPasswordProtected
        public string Password { get; set; } // Password
        public string Title { get; set; } // Title
        public string Body { get; set; } // Body (length: 1073741823)
        public string MetaKeywords { get; set; } // MetaKeywords
        public string MetaDescription { get; set; } // MetaDescription
        public string MetaTitle { get; set; } // MetaTitle
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? TypeId { get; set; } // TypeId
        public bool IsSystem { get; set; } // IsSystem
        public bool? IsSystemPage { get; set; } // IsSystemPage
        public int? SystemPageId { get; set; } // SystemPageId
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string LinkTitle { get; set; } // LinkTitle (length: 500)
        public string MenuLocation { get; set; } // MenuLocation (length: 50)
        public int? LayoutSetting { get; set; } // LayoutSetting
        public string PageHeader { get; set; } // PageHeader (length: 200)
        public string PageHeaderCss { get; set; } // PageHeaderCss (length: 200)
        public int? ControlOnPage { get; set; } // ControlOnPage
        public int? ChangeFreqId { get; set; } // ChangeFreqId
        public decimal? Priority { get; set; } // Priority
        public int? PageForId { get; set; } // PageForId

        // Reverse navigation

        /// <summary>
        /// Child ProductCatalogs where [ProductCatalog].[CmsPageId] point to this entity (FK_ProductCatalog_CmsPage)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ProductCatalog> ProductCatalogs { get; set; } // ProductCatalog.FK_ProductCatalog_CmsPage

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [CmsPage].([BusinessUnitId]) (FK_CmsPage_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_CmsPage_BusinessUnit

        public CmsPage()
        {
            IncludeInSitemap = 0;
            DisplayOrder = 0;
            IsSystem = false;
            IsActive = true;
            EntityTypeId = 27;
            IsDeleted = false;
            ProductCatalogs = new System.Collections.Generic.HashSet<ProductCatalog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
