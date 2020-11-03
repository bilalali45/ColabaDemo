













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TemplateFormSection

    public partial class TemplateFormSection 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? TemplateFormId { get; set; } // TemplateFormId
        public string Name { get; set; } // Name (length: 250)
        public string Description { get; set; } // Description (length: 500)
        public string KeyName { get; set; } // KeyName (length: 500)
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child TemplateFormPlots where [TemplateFormPlot].[TemplateSectionId] point to this entity (FK_TemplateFormPlot_TemplateFormSection)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateFormPlot> TemplateFormPlots { get; set; } // TemplateFormPlot.FK_TemplateFormPlot_TemplateFormSection

        // Foreign keys

        /// <summary>
        /// Parent TemplateForm pointed by [TemplateFormSection].([TemplateFormId]) (FK_TemplateFormSection_TemplateForm)
        /// </summary>
        public virtual TemplateForm TemplateForm { get; set; } // FK_TemplateFormSection_TemplateForm

        public TemplateFormSection()
        {
            IsActive = true;
            EntityTypeId = 117;
            IsDefault = false;
            IsSystem = false;
            DisplayOrder = 0;
            IsDeleted = false;
            TemplateFormPlots = new System.Collections.Generic.HashSet<TemplateFormPlot>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
