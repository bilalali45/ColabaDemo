













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // TemplateType

    public partial class TemplateType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Templates where [Template].[TemplateTypeId] point to this entity (FK_Template_TemplateType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Template> Templates { get; set; } // Template.FK_Template_TemplateType

        public TemplateType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 106;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Templates = new System.Collections.Generic.HashSet<Template>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
