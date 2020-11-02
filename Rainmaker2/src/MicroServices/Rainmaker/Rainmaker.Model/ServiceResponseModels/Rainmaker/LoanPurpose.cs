using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LoanPurpose
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPurchase { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public string TpId { get; set; }
        public bool IsDeleted { get; set; }

        //public System.Collections.Generic.ICollection<BankRateParameter> BankRateParameters { get; set; }

        public ICollection<LoanApplication> LoanApplications { get; set; }

        public ICollection<LoanGoal> LoanGoals { get; set; }

        public ICollection<LoanRequest> LoanRequests { get; set; }

        public ICollection<PromotionalProgram> PromotionalPrograms { get; set; }

        public ICollection<QuestionPurposeBinder> QuestionPurposeBinders { get; set; }

        public ICollection<ReviewComment> ReviewComments { get; set; }
    }
}