using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class ResidencyType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool? IsNonPermanent { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public string TpId { get; set; }
        public bool IsDeleted { get; set; }

//public System.Collections.Generic.ICollection<LoanContact> LoanContacts { get; set; }

//public System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; }

//public System.Collections.Generic.ICollection<ResidencyState> ResidencyStates { get; set; }

    }
}