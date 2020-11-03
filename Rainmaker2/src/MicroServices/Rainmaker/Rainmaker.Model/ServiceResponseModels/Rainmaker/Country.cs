using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
        public int NumericIsoCode { get; set; }
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

        //public System.Collections.Generic.ICollection<ContactAddress> ContactAddresses { get; set; }

        //public System.Collections.Generic.ICollection<State> States { get; set; }

    }
}