using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class ReviewSiteAccount
    {
        public int Id { get; set; }
        public int ReviewSiteId { get; set; }
        public int ReviewContactId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? PostedOnUtc { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        public ReviewContact ReviewContact { get; set; }
    }
}