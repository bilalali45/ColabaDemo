using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class RaceDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? RaceId { get; set; }
        public bool? IsOther { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

//public System.Collections.Generic.ICollection<LoanContactRaceBinder> LoanContactRaceBinders { get; set; }

//public Race Race { get; set; }

    }
}