using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class AdsGeoLocation
    {
        public int Id { get; set; }
        public int? AdsSourceId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
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

        public AdsSource AdsSource { get; set; }

        public City City { get; set; }

        public State State { get; set; }
    }
}