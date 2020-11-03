using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LoanApplicationView
    {
        public int Id { get; set; }
        public int? OpportunityId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? StatusId { get; set; }
        public string EncompassNumber { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string ZipCode { get; set; }
        public string LoanPurpose { get; set; }
        public string ApplicationStatus { get; set; }
        public string BusinessUnitName { get; set; }
        public string CustomerName { get; set; }
        public string AllPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public string LoanOfficer { get; set; }
    }
}