













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // VaDetails

    public partial class VaDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LoanApplicationId { get; set; } // LoanApplicationId
        public int? BorrowerId { get; set; } // BorrowerId
        public int? MilitaryBranchId { get; set; } // MilitaryBranchId
        public int? MilitaryAffiliationId { get; set; } // MilitaryAffiliationId
        public int? MilitaryStatusId { get; set; } // MilitaryStatusId
        public System.DateTime? ExpirationDateUtc { get; set; } // ExpirationDateUtc
        public int? VaOccupancyId { get; set; } // VaOccupancyId
        public string VaFirstName { get; set; } // VaFirstName (length: 150)
        public string VaLastName { get; set; } // VaLastName (length: 150)
        public int? RelationContactId { get; set; } // RelationContactId
        public bool? IsReceivingDisabilityIncome { get; set; } // IsReceivingDisabilityIncome

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [VaDetails].([BorrowerId]) (FK_VaDetails_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_VaDetails_Borrower

        /// <summary>
        /// Parent LoanApplication pointed by [VaDetails].([LoanApplicationId]) (FK_VaDetails_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_VaDetails_LoanApplication

        /// <summary>
        /// Parent MilitaryAffiliation pointed by [VaDetails].([MilitaryAffiliationId]) (FK_VaDetails_MilitaryAffiliation)
        /// </summary>
        public virtual MilitaryAffiliation MilitaryAffiliation { get; set; } // FK_VaDetails_MilitaryAffiliation

        /// <summary>
        /// Parent MilitaryBranch pointed by [VaDetails].([MilitaryBranchId]) (FK_VaDetails_MilitaryBranch)
        /// </summary>
        public virtual MilitaryBranch MilitaryBranch { get; set; } // FK_VaDetails_MilitaryBranch

        /// <summary>
        /// Parent MilitaryStatusList pointed by [VaDetails].([MilitaryStatusId]) (FK_VaDetails_MilitaryStatusList)
        /// </summary>
        public virtual MilitaryStatusList MilitaryStatusList { get; set; } // FK_VaDetails_MilitaryStatusList

        /// <summary>
        /// Parent VaOccupancy pointed by [VaDetails].([VaOccupancyId]) (FK_VaDetails_VaOccupancy)
        /// </summary>
        public virtual VaOccupancy VaOccupancy { get; set; } // FK_VaDetails_VaOccupancy

        public VaDetail()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
