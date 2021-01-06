using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Borrower
    {
        public int Id { get; set; }
        public int? LoanContactId { get; set; }
        public int? LoanApplicationId { get; set; }
        public int? RelationWithPrimaryId { get; set; }
        public int? CreditScoreNo { get; set; }
        public int EntityTypeId { get; set; }
        public string DependentAge { get; set; }
        public int? NoOfDependent { get; set; }
        public int? OwnTypeId { get; set; }
        public bool? IsVaEligible { get; set; }
        public string AdditionalInformation { get; set; }

        public ICollection<AssetBorrowerBinder> AssetBorrowerBinders { get; set; }

        public ICollection<BorrowerAccountBinder> BorrowerAccountBinders { get; set; }

        public ICollection<BorrowerBankRuptcy> BorrowerBankRuptcies { get; set; }

        public ICollection<BorrowerConsent> BorrowerConsents { get; set; }

        public ICollection<BorrowerLiability> BorrowerLiabilities { get; set; }

        public ICollection<BorrowerProperty> BorrowerProperties { get; set; }

        public ICollection<BorrowerQuestionResponse> BorrowerQuestionResponses { get; set; }

        public ICollection<BorrowerResidence> BorrowerResidences { get; set; }

        public ICollection<BorrowerSupportPayment> BorrowerSupportPayments { get; set; }

        public ICollection<EmploymentInfo> EmploymentInfoes { get; set; }

        public ICollection<OtherIncome> OtherIncomes { get; set; }

        public ICollection<OwnerShipInterest> OwnerShipInterests { get; set; }

        public FamilyRelationType FamilyRelationType { get; set; }

        public LoanApplication LoanApplication { get; set; }

        public LoanContact LoanContact { get; set; }

        public OwnType OwnType { get; set; }

    }
}