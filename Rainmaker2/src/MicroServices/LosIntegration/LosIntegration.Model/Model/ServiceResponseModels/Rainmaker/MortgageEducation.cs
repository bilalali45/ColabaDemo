













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // MortgageEducation

    public partial class MortgageEducation 
    {
        public int Id { get; set; } // Id (Primary key)
        public int LoanApplicationId { get; set; } // LoanApplicationId
        public int? MortgageEducationTypeId { get; set; } // MortgageEducationTypeId
        public bool Last12Month { get; set; } // Last12Month
        public System.DateTime CompletionDateUtc { get; set; } // CompletionDateUtc
        public int EducationFormatId { get; set; } // EducationFormatId
        public bool? IsHudApproved { get; set; } // IsHudApproved
        public int Agency { get; set; } // Agency
        public bool AgencyId { get; set; } // AgencyId

        // Foreign keys

        /// <summary>
        /// Parent EducationFormat pointed by [MortgageEducation].([EducationFormatId]) (FK_MortgageEducation_EducationFormat)
        /// </summary>
        public virtual EducationFormat EducationFormat { get; set; } // FK_MortgageEducation_EducationFormat

        /// <summary>
        /// Parent LoanApplication pointed by [MortgageEducation].([MortgageEducationTypeId]) (FK_MortgageEducation_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_MortgageEducation_LoanApplication

        /// <summary>
        /// Parent MortgageEducationType pointed by [MortgageEducation].([MortgageEducationTypeId]) (FK_MortgageEducation_MortgageEducationType)
        /// </summary>
        public virtual MortgageEducationType MortgageEducationType { get; set; } // FK_MortgageEducation_MortgageEducationType

        public MortgageEducation()
        {
            EducationFormatId = 1;
            AgencyId = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
