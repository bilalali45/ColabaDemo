using System;
using System.Collections.Generic;

namespace Rainmaker.Model
{
    public class LoanSummary
    {
        public string LoanPurpose { get; set; }
        public string PropertyType { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public decimal? LoanAmount { get; set; }
        public string CountryName { get; set; }
        public string UnitNumber { get; set; }
    }

    public class AdminLoanSummary
    {
        public string LoanPurpose { get; set; }
        public string PropertyType { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public decimal? LoanAmount { get; set; }
        public string CountryName { get; set; }
        public string UnitNumber { get; set; }
        public string Status { get; set; }
        public List<string> Borrowers { get; set; }
        public string LoanNumber { get; set; }
        public DateTime? ExpectedClosingDate { get; set; }
        public decimal? PopertyValue { get; set; }
        public decimal? Rate { get; set; }
        public string LoanProgram { get; set; }
        public string LockStatus { get; set; }
        public DateTime? LockDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
    public class LoanOfficer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WebUrl { get; set; }
        public string NMLS { get; set; }
        public string Photo { get; set; }
    }
}
