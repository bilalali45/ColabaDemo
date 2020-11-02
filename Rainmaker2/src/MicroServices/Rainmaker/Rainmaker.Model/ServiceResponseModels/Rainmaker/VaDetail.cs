using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class VaDetail
    {
        public int Id { get; set; }
        public int? LoanApplicationId { get; set; }
        public int? BorrowerId { get; set; }
        public int? MilitaryBranchId { get; set; }
        public int? MilitaryAffiliationId { get; set; }
        public int? MilitaryStatusId { get; set; }
        public DateTime? ExpirationDateUtc { get; set; }
        public int? VaOccupancyId { get; set; }
        public string VaFirstName { get; set; }
        public string VaLastName { get; set; }
        public int? RelationContactId { get; set; }
        public bool? IsReceivingDisabilityIncome { get; set; }

        public Borrower Borrower { get; set; }

        public LoanApplication LoanApplication { get; set; }

        public MilitaryAffiliation MilitaryAffiliation { get; set; }

        public MilitaryBranch MilitaryBranch { get; set; }

        public MilitaryStatusList MilitaryStatusList { get; set; }

        public VaOccupancy VaOccupancy { get; set; }
    }
}