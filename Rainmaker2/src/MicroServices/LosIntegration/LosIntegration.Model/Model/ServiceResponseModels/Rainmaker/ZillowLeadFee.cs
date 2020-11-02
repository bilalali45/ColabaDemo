













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ZillowLeadFee

    public partial class ZillowLeadFee 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ThirdPartyLeadId { get; set; } // ThirdPartyLeadId
        public int? HudLine { get; set; } // HudLine
        public string Name { get; set; } // Name (length: 100)
        public int? Amount { get; set; } // Amount
        public decimal? Percent { get; set; } // Percent

        // Foreign keys

        /// <summary>
        /// Parent ZillowLead pointed by [ZillowLeadFee].([ThirdPartyLeadId]) (FK_ZillowLeadFee_ZillowLead)
        /// </summary>
        public virtual ZillowLead ZillowLead { get; set; } // FK_ZillowLeadFee_ZillowLead

        public ZillowLeadFee()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
