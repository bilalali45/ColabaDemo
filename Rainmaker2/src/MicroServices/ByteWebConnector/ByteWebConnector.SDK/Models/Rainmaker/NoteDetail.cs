using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class NoteDetail
    {
        public int Id { get; set; }
        public int? NoteId { get; set; }
        public string Message { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }

        public Note Note { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}