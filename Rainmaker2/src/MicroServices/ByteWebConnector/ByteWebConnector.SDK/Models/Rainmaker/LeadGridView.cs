using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LeadGridView
    {
        public int Id { get; set; }
        public DateTime? AssignedOnUtc { get; set; }
        public bool IsPickedByOwner { get; set; }
        public DateTime? PickedOnUtc { get; set; }
        public int? OwnerId { get; set; }
        public bool IsAutoAssigned { get; set; }
        public int? LeadSourceId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? StatusId { get; set; }
        public int? LeadCreatedFromId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public string Purpose { get; set; }
        public decimal? LoanAmount { get; set; }
        public int? VisitorId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? SearchDateUtc { get; set; }
        public string PropertyState { get; set; }
        public string BusinessUnit { get; set; }
        public string LeadSource { get; set; }
        public string OrgLeadSource { get; set; }
        public string OpportunityStatus { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }
        public string EmployeeName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? Duplicate { get; set; }
        public string AllEmail { get; set; }
        public string AllPhone { get; set; }
        public string Names { get; set; }
        public string XmlNames { get; set; }
        public string TpId { get; set; }
        public int? LeadSourceOriginalId { get; set; }
        public DateTime? LeadCreatedOnUtc { get; set; }
        public DateTime? EstimatedClosingDate { get; set; }
        public string AdsSourceName { get; set; }
    }
}