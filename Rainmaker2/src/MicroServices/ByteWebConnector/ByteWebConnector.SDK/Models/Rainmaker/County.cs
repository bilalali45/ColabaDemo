using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class County
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StateId { get; set; }
        public int CountyTypeId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public bool IsDeleted { get; set; }
        public string ZipTasticName { get; set; }
        public string SmartName { get; set; }

        //public System.Collections.Generic.ICollection<BankRateInstance> BankRateInstances { get; set; }

        //public System.Collections.Generic.ICollection<Branch> Branches { get; set; }

        //public System.Collections.Generic.ICollection<ContactAddress> ContactAddresses { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; }

        //public System.Collections.Generic.ICollection<OfficeMetroBinder> OfficeMetroBinders { get; set; }

        //public System.Collections.Generic.ICollection<PromotionalProgram> PromotionalPrograms { get; set; }

        //public System.Collections.Generic.ICollection<ReviewComment> ReviewComments { get; set; }

        //public System.Collections.Generic.ICollection<ReviewProperty> ReviewProperties { get; set; }

        //public System.Collections.Generic.ICollection<TaxCountyBinder> TaxCountyBinders { get; set; }

        //public System.Collections.Generic.ICollection<ZipCode> ZipCodes { get; set; }

        public CountyType CountyType { get; set; }

        //public State State { get; set; }

    }
}