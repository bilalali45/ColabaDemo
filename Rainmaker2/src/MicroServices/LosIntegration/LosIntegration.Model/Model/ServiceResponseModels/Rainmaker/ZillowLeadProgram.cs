













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ZillowLeadProgram

    public partial class ZillowLeadProgram 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ThirdPartyLeadId { get; set; } // ThirdPartyLeadId
        public string ProductName { get; set; } // ProductName (length: 100)

        // Foreign keys

        /// <summary>
        /// Parent ZillowLead pointed by [ZillowLeadProgram].([ThirdPartyLeadId]) (FK_ZillowLeadProgram_ZillowLead)
        /// </summary>
        public virtual ZillowLead ZillowLead { get; set; } // FK_ZillowLeadProgram_ZillowLead

        public ZillowLeadProgram()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
