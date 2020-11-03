













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TemplateFormPlot

    public partial class TemplateFormPlot 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public string Description { get; set; } // Description (length: 50)
        public int? TemplateFormId { get; set; } // TemplateFormId
        public int? TemplateSectionId { get; set; } // TemplateSectionId
        public bool? IsItemized { get; set; } // IsItemized
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
        /// Child TemplateFormPlotDetails where [TemplateFormPlotDetail].[TemplateFormPlotId] point to this entity (FK_TemplateFormPlotDetail_TemplateFormPlot)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateFormPlotDetail> TemplateFormPlotDetails { get; set; } // TemplateFormPlotDetail.FK_TemplateFormPlotDetail_TemplateFormPlot

        // Foreign keys

        /// <summary>
        /// Parent TemplateForm pointed by [TemplateFormPlot].([TemplateFormId]) (FK_TemplateFormPlot_TemplateForm)
        /// </summary>
        public virtual TemplateForm TemplateForm { get; set; } // FK_TemplateFormPlot_TemplateForm

        /// <summary>
        /// Parent TemplateFormSection pointed by [TemplateFormPlot].([TemplateSectionId]) (FK_TemplateFormPlot_TemplateFormSection)
        /// </summary>
        public virtual TemplateFormSection TemplateFormSection { get; set; } // FK_TemplateFormPlot_TemplateFormSection

        public TemplateFormPlot()
        {
            IsActive = true;
            EntityTypeId = 75;
            IsDefault = false;
            IsSystem = false;
            DisplayOrder = 0;
            IsDeleted = false;
            TemplateFormPlotDetails = new System.Collections.Generic.HashSet<TemplateFormPlotDetail>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
