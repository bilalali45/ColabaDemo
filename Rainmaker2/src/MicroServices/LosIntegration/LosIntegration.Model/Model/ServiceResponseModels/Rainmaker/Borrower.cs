













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Borrower

    public partial class Borrower 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LoanContactId { get; set; } // LoanContactId
        public int? LoanApplicationId { get; set; } // LoanApplicationId
        public int? RelationWithPrimaryId { get; set; } // RelationWithPrimaryId
        public int? CreditScoreNo { get; set; } // CreditScoreNo
        public int EntityTypeId { get; set; } // EntityTypeId
        public string DependentAge { get; set; } // DependentAge (length: 1000)
        public int? NoOfDependent { get; set; } // NoOfDependent
        public int? OwnTypeId { get; set; } // OwnTypeId
        public bool? IsVaEligible { get; set; } // IsVaEligible
        public string AdditionalInformation { get; set; } // AdditionalInformation (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child AssetBorrowerBinders where [AssetBorrowerBinder].[BorrowerId] point to this entity (FK_AssetBorrowerBinder_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AssetBorrowerBinder> AssetBorrowerBinders { get; set; } // AssetBorrowerBinder.FK_AssetBorrowerBinder_Borrower
        /// <summary>
        /// Child BorrowerAccountBinders where [BorrowerAccountBinder].[BorrowerId] point to this entity (FK_BorrowerAccountBinder_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerAccountBinder> BorrowerAccountBinders { get; set; } // BorrowerAccountBinder.FK_BorrowerAccountBinder_Borrower
        /// <summary>
        /// Child BorrowerBankRuptcies where [BorrowerBankRuptcy].[BorrowerId] point to this entity (FK_BorrowerBankRuptcy_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerBankRuptcy> BorrowerBankRuptcies { get; set; } // BorrowerBankRuptcy.FK_BorrowerBankRuptcy_Borrower
        /// <summary>
        /// Child BorrowerConsents where [BorrowerConsent].[BorrowerId] point to this entity (FK_BorrowerConsent_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerConsent> BorrowerConsents { get; set; } // BorrowerConsent.FK_BorrowerConsent_Borrower
        /// <summary>
        /// Child BorrowerLiabilities where [BorrowerLiability].[BorrowerId] point to this entity (FK_BorrowerLiability_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerLiability> BorrowerLiabilities { get; set; } // BorrowerLiability.FK_BorrowerLiability_Borrower
        /// <summary>
        /// Child BorrowerProperties where [BorrowerProperty].[BorrowerId] point to this entity (FK_BorrowerProperty_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerProperty> BorrowerProperties { get; set; } // BorrowerProperty.FK_BorrowerProperty_Borrower
        /// <summary>
        /// Child BorrowerQuestionResponses where [BorrowerQuestionResponse].[BorrowerId] point to this entity (FK_BorrowerQuestionResponse_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerQuestionResponse> BorrowerQuestionResponses { get; set; } // BorrowerQuestionResponse.FK_BorrowerQuestionResponse_Borrower
        /// <summary>
        /// Child BorrowerResidences where [BorrowerResidence].[BorrowerId] point to this entity (FK_BorrowerResidence_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerResidence> BorrowerResidences { get; set; } // BorrowerResidence.FK_BorrowerResidence_Borrower
        /// <summary>
        /// Child BorrowerSupportPayments where [BorrowerSupportPayment].[BorrowerId] point to this entity (FK_BorrowerSupportPayment_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerSupportPayment> BorrowerSupportPayments { get; set; } // BorrowerSupportPayment.FK_BorrowerSupportPayment_Borrower
        /// <summary>
        /// Child EmploymentInfoes where [EmploymentInfo].[BorrowerId] point to this entity (FK_EmploymentInfo_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmploymentInfo> EmploymentInfoes { get; set; } // EmploymentInfo.FK_EmploymentInfo_Borrower
        /// <summary>
        /// Child OtherIncomes where [OtherIncome].[BorrowerId] point to this entity (FK_OtherIncome_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OtherIncome> OtherIncomes { get; set; } // OtherIncome.FK_OtherIncome_Borrower
        /// <summary>
        /// Child OwnerShipInterests where [OwnerShipInterest].[BorrowerId] point to this entity (FK_OwnerShipInterest_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OwnerShipInterest> OwnerShipInterests { get; set; } // OwnerShipInterest.FK_OwnerShipInterest_Borrower
        /// <summary>
        /// Child VaDetails where [VaDetails].[BorrowerId] point to this entity (FK_VaDetails_Borrower)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<VaDetail> VaDetails { get; set; } // VaDetails.FK_VaDetails_Borrower

        // Foreign keys

        /// <summary>
        /// Parent FamilyRelationType pointed by [Borrower].([RelationWithPrimaryId]) (FK_Borrower_FamilyRelationType)
        /// </summary>
        public virtual FamilyRelationType FamilyRelationType { get; set; } // FK_Borrower_FamilyRelationType

        /// <summary>
        /// Parent LoanApplication pointed by [Borrower].([LoanApplicationId]) (FK_Borrower_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_Borrower_LoanApplication

        /// <summary>
        /// Parent LoanContact pointed by [Borrower].([LoanContactId]) (FK_Borrower_LoanContact)
        /// </summary>
        public virtual LoanContact LoanContact { get; set; } // FK_Borrower_LoanContact

        /// <summary>
        /// Parent OwnType pointed by [Borrower].([OwnTypeId]) (FK_Borrower_OwnType)
        /// </summary>
        public virtual OwnType OwnType { get; set; } // FK_Borrower_OwnType

        public Borrower()
        {
            EntityTypeId = 50;
            AssetBorrowerBinders = new System.Collections.Generic.HashSet<AssetBorrowerBinder>();
            BorrowerAccountBinders = new System.Collections.Generic.HashSet<BorrowerAccountBinder>();
            BorrowerBankRuptcies = new System.Collections.Generic.HashSet<BorrowerBankRuptcy>();
            BorrowerConsents = new System.Collections.Generic.HashSet<BorrowerConsent>();
            BorrowerLiabilities = new System.Collections.Generic.HashSet<BorrowerLiability>();
            BorrowerProperties = new System.Collections.Generic.HashSet<BorrowerProperty>();
            BorrowerQuestionResponses = new System.Collections.Generic.HashSet<BorrowerQuestionResponse>();
            BorrowerResidences = new System.Collections.Generic.HashSet<BorrowerResidence>();
            BorrowerSupportPayments = new System.Collections.Generic.HashSet<BorrowerSupportPayment>();
            EmploymentInfoes = new System.Collections.Generic.HashSet<EmploymentInfo>();
            OtherIncomes = new System.Collections.Generic.HashSet<OtherIncome>();
            OwnerShipInterests = new System.Collections.Generic.HashSet<OwnerShipInterest>();
            VaDetails = new System.Collections.Generic.HashSet<VaDetail>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
