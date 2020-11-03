using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class ContactEmailInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public bool? IsPrimary { get; set; }
        public int? ContactId { get; set; }
        public bool IsDeleted { get; set; }
        public int? ValidityId { get; set; }
        public int EntityTypeId { get; set; }
        public int? UseForId { get; set; }

        public ICollection<FollowUp> FollowUps { get; set; }

        public Contact Contact { get; set; }
    }
}