using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class ReviewPosted
    {
        public int Id { get; set; }
        public int ReviewContactId { get; set; }
        public int ReviewCommentId { get; set; }
        public int ReviewPropertyId { get; set; }
        public string ReviewUrl { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        public ReviewContact ReviewContact { get; set; }

        public ReviewProperty ReviewProperty { get; set; }
    }
}