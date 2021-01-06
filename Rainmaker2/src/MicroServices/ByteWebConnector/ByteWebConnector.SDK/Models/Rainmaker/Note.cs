using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Note
    {
        public int Id { get; set; }
        public int? NoteTopicId { get; set; }
        public string Subject { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int EntityTypeId { get; set; }
        public int? EntityRefTypeId { get; set; }
        public int? EntityRefId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<NoteDepartmentBinder> NoteDepartmentBinders { get; set; }

        public ICollection<NoteDetail> NoteDetails { get; set; }

        //public System.Collections.Generic.ICollection<Vortex_ActivityLog> Vortex_ActivityLogs { get; set; }

        public EntityType EntityRefType { get; set; }

        public EntityType EntityType_EntityTypeId { get; set; }

        public NoteTopic NoteTopic { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}