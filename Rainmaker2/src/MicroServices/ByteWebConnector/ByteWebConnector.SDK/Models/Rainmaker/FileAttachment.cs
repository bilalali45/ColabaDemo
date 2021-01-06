using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class FileAttachment
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDynamic { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public int? FillCommandId { get; set; }

        //public System.Collections.Generic.ICollection<TemplateAttachment> TemplateAttachments { get; set; }
    }
}