













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LeadGridView

    public partial class LeadGridView 
    {
        public int Id { get; set; } // Id (Primary key)
        public System.DateTime? AssignedOnUtc { get; set; } // AssignedOnUtc
        public bool IsPickedByOwner { get; set; } // IsPickedByOwner (Primary key)
        public System.DateTime? PickedOnUtc { get; set; } // PickedOnUtc
        public int? OwnerId { get; set; } // OwnerId
        public bool IsAutoAssigned { get; set; } // IsAutoAssigned (Primary key)
        public int? LeadSourceId { get; set; } // LeadSourceId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? StatusId { get; set; } // StatusId
        public int? LeadCreatedFromId { get; set; } // LeadCreatedFromId
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public string Purpose { get; set; } // Purpose (length: 150)
        public decimal? LoanAmount { get; set; } // LoanAmount
        public int? VisitorId { get; set; } // VisitorId
        public int? CustomerId { get; set; } // CustomerId
        public System.DateTime? SearchDateUtc { get; set; } // SearchDateUtc
        public string PropertyState { get; set; } // PropertyState (length: 150)
        public string BusinessUnit { get; set; } // BusinessUnit (length: 150)
        public string LeadSource { get; set; } // LeadSource (length: 150)
        public string OrgLeadSource { get; set; } // OrgLeadSource (length: 150)
        public string OpportunityStatus { get; set; } // OpportunityStatus (length: 150)
        public int? CategoryId { get; set; } // CategoryId
        public int? TypeId { get; set; } // TypeId
        public string EmployeeName { get; set; } // EmployeeName (length: 1000)
        public string ContactName { get; set; } // ContactName (length: 1000)
        public string Phone { get; set; } // Phone (length: 150)
        public string Email { get; set; } // Email (length: 150)
        public int? Duplicate { get; set; } // Duplicate
        public string AllEmail { get; set; } // AllEmail
        public string AllPhone { get; set; } // AllPhone
        public string Names { get; set; } // Names
        public string XmlNames { get; set; } // XmlNames
        public string TpId { get; set; } // TpId (length: 50)
        public int? LeadSourceOriginalId { get; set; } // LeadSourceOriginalId
        public System.DateTime? LeadCreatedOnUtc { get; set; } // LeadCreatedOnUtc
        public System.DateTime? EstimatedClosingDate { get; set; } // EstimatedClosingDate
        public string AdsSourceName { get; set; } // AdsSourceName (length: 150)

        public LeadGridView()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
