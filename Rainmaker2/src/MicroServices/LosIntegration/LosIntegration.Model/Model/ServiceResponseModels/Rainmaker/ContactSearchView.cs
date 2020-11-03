













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ContactSearchView

    public partial class ContactSearchView 
    {
        public int Id { get; set; } // Id (Primary key)
        public string FirstName { get; set; } // FirstName (Primary key) (length: 300)
        public string MiddleName { get; set; } // MiddleName (Primary key) (length: 50)
        public string LastName { get; set; } // LastName (Primary key) (length: 300)
        public string NickName { get; set; } // NickName (Primary key) (length: 50)
        public string Email { get; set; } // Email (Primary key) (length: 150)
        public string Phone { get; set; } // Phone (Primary key) (length: 150)
        public string StateName { get; set; } // StateName (length: 150)
        public string Occupancy { get; set; } // Occupancy (length: 150)
        public decimal? LoanAmount { get; set; } // LoanAmount
        public string LeadStatus { get; set; } // LeadStatus (length: 150)
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? OpportunityId { get; set; } // OpportunityId
        public string AssignTo { get; set; } // AssignTo (length: 1000)
        public string AllInfo { get; set; } // AllInfo

        public ContactSearchView()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
