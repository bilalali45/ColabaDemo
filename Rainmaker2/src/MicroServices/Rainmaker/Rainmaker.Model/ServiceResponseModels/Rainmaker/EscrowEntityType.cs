using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class EscrowEntityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        public int? EscrowGroupId { get; set; }

        public ICollection<OpportunityPropertyTax> OpportunityPropertyTaxes { get; set; }

        public ICollection<PropertyTax> PropertyTaxes { get; set; }

        public ICollection<PropertyTaxEscrow> PropertyTaxEscrows { get; set; }
    }
}