













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanApplicationView

    public partial class LoanApplicationView 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? OpportunityId { get; set; } // OpportunityId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? StatusId { get; set; } // StatusId
        public string EncompassNumber { get; set; } // EncompassNumber (length: 50)
        public string StateName { get; set; } // StateName (length: 200)
        public string CountyName { get; set; } // CountyName (length: 200)
        public string CityName { get; set; } // CityName (length: 200)
        public string ZipCode { get; set; } // ZipCode (length: 10)
        public string LoanPurpose { get; set; } // LoanPurpose (length: 150)
        public string ApplicationStatus { get; set; } // ApplicationStatus (length: 150)
        public string BusinessUnitName { get; set; } // BusinessUnitName (length: 150)
        public string CustomerName { get; set; } // CustomerName (length: 601)
        public string AllPhone { get; set; } // AllPhone (length: 152)
        public string CellPhone { get; set; } // CellPhone (length: 50)
        public string HomePhone { get; set; } // HomePhone (length: 50)
        public string WorkPhone { get; set; } // WorkPhone (length: 50)
        public string EmailAddress { get; set; } // EmailAddress (length: 150)
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public string LoanOfficer { get; set; } // LoanOfficer (length: 1000)

        public LoanApplicationView()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
