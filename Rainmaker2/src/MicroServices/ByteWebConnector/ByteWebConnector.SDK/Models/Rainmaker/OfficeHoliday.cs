using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class OfficeHoliday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HolidayTypeId { get; set; }
        public DateTime? HolidayOn { get; set; }
        public bool WillOfficeClosed { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }

        public HolidayType HolidayType { get; set; }
    }
}