













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // VendorCustomerBinder

    public partial class VendorCustomerBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int VendorId { get; set; } // VendorId
        public int CustomerId { get; set; } // CustomerId
        public int? OwnTypeId { get; set; } // OwnTypeId

        // Foreign keys

        /// <summary>
        /// Parent Customer pointed by [VendorCustomerBinder].([CustomerId]) (FK_VendorCustomerBinder_Customer)
        /// </summary>
        public virtual Customer Customer { get; set; } // FK_VendorCustomerBinder_Customer

        /// <summary>
        /// Parent OwnType pointed by [VendorCustomerBinder].([OwnTypeId]) (FK_VendorCustomerBinder_OwnType)
        /// </summary>
        public virtual OwnType OwnType { get; set; } // FK_VendorCustomerBinder_OwnType

        /// <summary>
        /// Parent Vendor pointed by [VendorCustomerBinder].([VendorId]) (FK_VendorCustomerBinder_Vendor)
        /// </summary>
        public virtual Vendor Vendor { get; set; } // FK_VendorCustomerBinder_Vendor

        public VendorCustomerBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
