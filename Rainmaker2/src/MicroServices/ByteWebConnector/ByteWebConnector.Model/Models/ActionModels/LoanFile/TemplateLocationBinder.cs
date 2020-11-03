













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TemplateLocationBinder

    public partial class TemplateLocationBinder 
    {
        public int TemplateId { get; set; } // TemplateId (Primary key)
        public int TemplateLocationId { get; set; } // TemplateLocationId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Template pointed by [TemplateLocationBinder].([TemplateId]) (FK_TemplateLocationBinder_Template)
        /// </summary>
        public virtual Template Template { get; set; } // FK_TemplateLocationBinder_Template

        public TemplateLocationBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
