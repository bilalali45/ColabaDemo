using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class EmployeeLicense
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? StateId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? RenewDateUtc { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }

        public Employee Employee { get; set; }

        public State State { get; set; }
    }
}