
















namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BankRatePropertyUsageBinder

    public partial class BankRatePropertyUsageBinder 
    {
        public int BankRateProductId { get; set; } // BankRateProductId (Primary key)
        public int PropertyUsageId { get; set; } // PropertyUsageId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent BankRateProduct pointed by [BankRatePropertyUsageBinder].([BankRateProductId]) (FK_BankRatePropertyUsageBinder_BankRateProduct)
        /// </summary>
        public virtual BankRateProduct BankRateProduct { get; set; } // FK_BankRatePropertyUsageBinder_BankRateProduct

        /// <summary>
        /// Parent PropertyUsage pointed by [BankRatePropertyUsageBinder].([PropertyUsageId]) (FK_BankRatePropertyUsageBinder_PropertyUsage)
        /// </summary>
        public virtual PropertyUsage PropertyUsage { get; set; } // FK_BankRatePropertyUsageBinder_PropertyUsage

        public BankRatePropertyUsageBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
