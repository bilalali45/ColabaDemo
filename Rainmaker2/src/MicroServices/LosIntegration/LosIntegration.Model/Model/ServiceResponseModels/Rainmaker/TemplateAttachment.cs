













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // TemplateAttachment

    public partial class TemplateAttachment 
    {
        public int TemplateId { get; set; } // TemplateId (Primary key)
        public int FileAttachmentId { get; set; } // FileAttachmentId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent FileAttachment pointed by [TemplateAttachment].([FileAttachmentId]) (FK_TemplateAttachment_FileAttachment)
        /// </summary>
        public virtual FileAttachment FileAttachment { get; set; } // FK_TemplateAttachment_FileAttachment

        /// <summary>
        /// Parent Template pointed by [TemplateAttachment].([TemplateId]) (FK_TemplateAttachment_Template)
        /// </summary>
        public virtual Template Template { get; set; } // FK_TemplateAttachment_Template

        public TemplateAttachment()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
