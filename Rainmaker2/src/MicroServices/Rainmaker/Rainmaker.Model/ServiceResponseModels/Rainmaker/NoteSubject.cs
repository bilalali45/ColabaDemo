using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class NoteSubject
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
    }
}