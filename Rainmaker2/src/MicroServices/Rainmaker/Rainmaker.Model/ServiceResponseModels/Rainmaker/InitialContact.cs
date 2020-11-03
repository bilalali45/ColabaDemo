using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class InitialContact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? OpportunityId { get; set; }
        public int? VisitorId { get; set; }
        public DateTime? CreatedOnUtc { get; set; }

        public Opportunity Opportunity { get; set; }

        public Visitor Visitor { get; set; }
    }
}