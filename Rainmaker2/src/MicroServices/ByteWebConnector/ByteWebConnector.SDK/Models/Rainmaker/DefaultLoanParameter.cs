using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class DefaultLoanParameter
    {
        public int Id { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? LoanRequestId { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool? IsSystem { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public LoanRequest LoanRequest { get; set; }
    }
}