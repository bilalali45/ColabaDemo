using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class ContactSearchView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string StateName { get; set; }
        public string Occupancy { get; set; }
        public decimal? LoanAmount { get; set; }
        public string LeadStatus { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? OpportunityId { get; set; }
        public string AssignTo { get; set; }
        public string AllInfo { get; set; }
    }
}