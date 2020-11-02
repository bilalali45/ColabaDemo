using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class State
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public bool IsLicenseActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public bool IsDeleted { get; set; }
        public int? CushionMonth { get; set; }
        public int? TaxMonth { get; set; }
        public int? InsuranceMonth { get; set; }

        //public System.Collections.Generic.ICollection<AdsGeoLocation> AdsGeoLocations { get; set; }

        ////public System.Collections.Generic.ICollection<BankRateInstance> BankRateInstances { get; set; }

        //public System.Collections.Generic.ICollection<Branch> Branches { get; set; }

        //public System.Collections.Generic.ICollection<City> Cities { get; set; }

        //public System.Collections.Generic.ICollection<ContactAddress> ContactAddresses { get; set; }

        //public System.Collections.Generic.ICollection<County> Counties { get; set; }

        //public System.Collections.Generic.ICollection<EmployeeCsrLoBinder> EmployeeCsrLoBinders { get; set; }

        //public System.Collections.Generic.ICollection<EmployeeLicense> EmployeeLicenses { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; }

        //public System.Collections.Generic.ICollection<OfficeMetroBinder> OfficeMetroBinders { get; set; }

        //public System.Collections.Generic.ICollection<PromotionalProgram> PromotionalPrograms { get; set; }

        //public System.Collections.Generic.ICollection<PropertyTax> PropertyTaxes { get; set; }

        //public System.Collections.Generic.ICollection<ReviewComment> ReviewComments { get; set; }

        //public System.Collections.Generic.ICollection<ReviewProperty> ReviewProperties { get; set; }

        //public System.Collections.Generic.ICollection<ZipCode> ZipCodes { get; set; }

        public Country Country { get; set; }

    }
}