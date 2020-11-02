using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class ReviewProperty
    {
        public int Id { get; set; }
        public int? StateId { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string ZipCode { get; set; }
        public string StreetAddress { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ReviewPosted> ReviewPosteds { get; set; }

        public City City { get; set; }

        public County County { get; set; }

        public State State { get; set; }
    }
}