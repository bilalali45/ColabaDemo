// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Entity.Models
{
    // TemplateAttachment

    public partial class TemplateAttachment : URF.Core.EF.Trackable.Entity
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
