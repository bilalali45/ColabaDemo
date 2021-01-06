using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class ReviewContact
    {
        public int Id { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAlias { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ReviewPosted> ReviewPosteds { get; set; }

        public ICollection<ReviewSiteAccount> ReviewSiteAccounts { get; set; }
    }
}