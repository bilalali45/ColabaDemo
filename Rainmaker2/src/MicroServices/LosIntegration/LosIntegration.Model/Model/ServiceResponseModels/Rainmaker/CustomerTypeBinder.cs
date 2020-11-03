













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // CustomerTypeBinder

    public partial class CustomerTypeBinder 
    {
        public int CustomerId { get; set; } // CustomerId (Primary key)
        public int CustomerTypeId { get; set; } // CustomerTypeId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Customer pointed by [CustomerTypeBinder].([CustomerId]) (FK_CustomerTypeBinder_Customer)
        /// </summary>
        public virtual Customer Customer { get; set; } // FK_CustomerTypeBinder_Customer

        /// <summary>
        /// Parent CustomerType pointed by [CustomerTypeBinder].([CustomerTypeId]) (FK_CustomerTypeBinder_CustomerType)
        /// </summary>
        public virtual CustomerType CustomerType { get; set; } // FK_CustomerTypeBinder_CustomerType

        public CustomerTypeBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
