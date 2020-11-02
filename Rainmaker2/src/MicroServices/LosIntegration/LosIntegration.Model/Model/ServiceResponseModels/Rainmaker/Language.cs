













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Language

    public partial class Language 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string LanguageCulture { get; set; } // LanguageCulture (length: 50)
        public string UniqueSeoCode { get; set; } // UniqueSeoCode (length: 2)
        public string FlagImageFileName { get; set; } // FlagImageFileName (length: 50)
        public bool Rtl { get; set; } // Rtl
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child LocaleStringResources where [LocaleStringResource].[LanguageId] point to this entity (FK_LocaleStringResource_Language)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LocaleStringResource> LocaleStringResources { get; set; } // LocaleStringResource.FK_LocaleStringResource_Language

        public Language()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 5;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LocaleStringResources = new System.Collections.Generic.HashSet<LocaleStringResource>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
