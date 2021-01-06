using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class CmsPage
    {
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string Description { get; set; }
        public int IncludeInSitemap { get; set; }
        public bool? IsPasswordProtected { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int DisplayOrder { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? TypeId { get; set; }
        public bool IsSystem { get; set; }
        public bool? IsSystemPage { get; set; }
        public int? SystemPageId { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string LinkTitle { get; set; }
        public string MenuLocation { get; set; }
        public int? LayoutSetting { get; set; }
        public string PageHeader { get; set; }
        public string PageHeaderCss { get; set; }
        public int? ControlOnPage { get; set; }
        public int? ChangeFreqId { get; set; }
        public decimal? Priority { get; set; }
        public int? PageForId { get; set; }

        public ICollection<ProductCatalog> ProductCatalogs { get; set; }

        public BusinessUnit BusinessUnit { get; set; }
    }
}