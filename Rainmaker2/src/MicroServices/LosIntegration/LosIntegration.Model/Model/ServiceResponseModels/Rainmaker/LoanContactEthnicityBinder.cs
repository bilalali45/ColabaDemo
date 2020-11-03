













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanContactEthnicityBinder

    public partial class LoanContactEthnicityBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int LoanContactId { get; set; } // LoanContactId
        public int EthnicityId { get; set; } // EthnicityId
        public int? EthnicityDetailId { get; set; } // EthnicityDetailId
        public string OtherEthnicity { get; set; } // OtherEthnicity (length: 150)

        // Foreign keys

        /// <summary>
        /// Parent Ethnicity pointed by [LoanContactEthnicityBinder].([EthnicityId]) (FK_LoanContactEthnicityBinder_Ethnicity)
        /// </summary>
        public virtual Ethnicity Ethnicity { get; set; } // FK_LoanContactEthnicityBinder_Ethnicity

        /// <summary>
        /// Parent EthnicityDetail pointed by [LoanContactEthnicityBinder].([EthnicityDetailId]) (FK_LoanContactEthnicityBinder_EthnicityDetail)
        /// </summary>
        public virtual EthnicityDetail EthnicityDetail { get; set; } // FK_LoanContactEthnicityBinder_EthnicityDetail

        /// <summary>
        /// Parent LoanContact pointed by [LoanContactEthnicityBinder].([LoanContactId]) (FK_LoanContactEthnicityBinder_LoanContact)
        /// </summary>
        public virtual LoanContact LoanContact { get; set; } // FK_LoanContactEthnicityBinder_LoanContact

        public LoanContactEthnicityBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
