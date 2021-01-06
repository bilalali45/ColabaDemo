using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LockDaysOnRule
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public int ActionTypeId { get; set; }
        public int Value { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        public Rule Rule { get; set; }
    }
}