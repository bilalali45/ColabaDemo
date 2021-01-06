using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? RegionTypeId { get; set; }
        public string TypeList { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public string States { get; set; }
        public string Counties { get; set; }
        public string ZipCodes { get; set; }
        public string Cities { get; set; }
        public bool IsDeleted { get; set; }
        public string Xml { get; set; }
    }
}