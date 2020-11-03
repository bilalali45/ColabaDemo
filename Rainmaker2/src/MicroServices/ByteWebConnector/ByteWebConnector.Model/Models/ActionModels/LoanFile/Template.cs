













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Template

    public partial class Template 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string FromAddress { get; set; } // FromAddress (length: 500)
        public int? FromEmailAccountId { get; set; } // FromEmailAccountId
        public string ToAddresses { get; set; } // ToAddresses (length: 500)
        public string OtherToAddresses { get; set; } // OtherToAddresses (length: 500)
        public string CcAddresses { get; set; } // CcAddresses (length: 500)
        public string BccAddresses { get; set; } // BccAddresses (length: 500)
        public string OtherBccEmailAddresses { get; set; } // OtherBccEmailAddresses (length: 500)
        public string Subject { get; set; } // Subject (length: 200)
        public string Description { get; set; } // Description (length: 500)
        public int? VisibleForId { get; set; } // VisibleForId
        public int TemplateTypeId { get; set; } // TemplateTypeId
        public string TemplateBody { get; set; } // TemplateBody (length: 1073741823)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int RefEntityType { get; set; } // RefEntityType
        public bool IsDeleted { get; set; } // IsDeleted
        public string Utm { get; set; } // Utm (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child Activities where [Activity].[TemplateId] point to this entity (FK_Activity_Template)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Activity> Activities { get; set; } // Activity.FK_Activity_Template
        /// <summary>
        /// Child TemplateAttachments where [TemplateAttachment].[TemplateId] point to this entity (FK_TemplateAttachment_Template)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateAttachment> TemplateAttachments { get; set; } // TemplateAttachment.FK_TemplateAttachment_Template
        /// <summary>
        /// Child TemplateLocationBinders where [TemplateLocationBinder].[TemplateId] point to this entity (FK_TemplateLocationBinder_Template)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateLocationBinder> TemplateLocationBinders { get; set; } // TemplateLocationBinder.FK_TemplateLocationBinder_Template
        /// <summary>
        /// Child WorkQueues where [WorkQueue].[TemplateId] point to this entity (FK_WorkQueue_Template)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; } // WorkQueue.FK_WorkQueue_Template

        // Foreign keys

        /// <summary>
        /// Parent EmailAccount pointed by [Template].([FromEmailAccountId]) (FK_Template_EmailAccount)
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; } // FK_Template_EmailAccount

        /// <summary>
        /// Parent TemplateType pointed by [Template].([TemplateTypeId]) (FK_Template_TemplateType)
        /// </summary>
        public virtual TemplateType TemplateType { get; set; } // FK_Template_TemplateType

        public Template()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 139;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Activities = new System.Collections.Generic.HashSet<Activity>();
            TemplateAttachments = new System.Collections.Generic.HashSet<TemplateAttachment>();
            TemplateLocationBinders = new System.Collections.Generic.HashSet<TemplateLocationBinder>();
            WorkQueues = new System.Collections.Generic.HashSet<WorkQueue>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
