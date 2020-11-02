













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // FileAttachment

    public partial class FileAttachment 
    {
        public int Id { get; set; } // Id (Primary key)
        public string DisplayName { get; set; } // DisplayName (length: 50)
        public string Description { get; set; } // Description (length: 500)
        public string FilePath { get; set; } // FilePath (length: 1000)
        public string FileType { get; set; } // FileType (length: 10)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDynamic { get; set; } // IsDynamic
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public int? FillCommandId { get; set; } // FillCommandId

        // Reverse navigation

        /// <summary>
        /// Child TemplateAttachments where [TemplateAttachment].[FileAttachmentId] point to this entity (FK_TemplateAttachment_FileAttachment)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateAttachment> TemplateAttachments { get; set; } // TemplateAttachment.FK_TemplateAttachment_FileAttachment

        public FileAttachment()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 83;
            IsDynamic = false;
            IsSystem = false;
            IsDeleted = false;
            TemplateAttachments = new System.Collections.Generic.HashSet<TemplateAttachment>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
