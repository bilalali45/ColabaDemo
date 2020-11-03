













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TemplateForm

    public partial class TemplateForm 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 250)
        public string Description { get; set; } // Description (length: 500)
        public int ViewOnId { get; set; } // ViewOnId
        public int DocumentTypeId { get; set; } // DocumentTypeId
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
        /// Child TemplateFormPlots where [TemplateFormPlot].[TemplateFormId] point to this entity (FK_TemplateFormPlot_TemplateForm)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateFormPlot> TemplateFormPlots { get; set; } // TemplateFormPlot.FK_TemplateFormPlot_TemplateForm
        /// <summary>
        /// Child TemplateFormSections where [TemplateFormSection].[TemplateFormId] point to this entity (FK_TemplateFormSection_TemplateForm)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateFormSection> TemplateFormSections { get; set; } // TemplateFormSection.FK_TemplateFormSection_TemplateForm

        public TemplateForm()
        {
            IsActive = true;
            EntityTypeId = 43;
            IsDefault = false;
            IsSystem = false;
            DisplayOrder = 0;
            IsDeleted = false;
            TemplateFormPlots = new System.Collections.Generic.HashSet<TemplateFormPlot>();
            TemplateFormSections = new System.Collections.Generic.HashSet<TemplateFormSection>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
