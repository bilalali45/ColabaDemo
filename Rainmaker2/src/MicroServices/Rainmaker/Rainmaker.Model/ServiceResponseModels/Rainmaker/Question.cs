using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? QuestionTypeId { get; set; }
        public int? QuestionSectionId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsRequired { get; set; }
        public bool IsCustomerQuestion { get; set; }
        public string DefaultValue { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public int? QuestionForId { get; set; }
        public bool? IsDescriptionRequired { get; set; }

        //public System.Collections.Generic.ICollection<BorrowerQuestionResponse> BorrowerQuestionResponses { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequestQuestionBinding> LoanRequestQuestionBindings { get; set; }

        public ICollection<QuestionOption> QuestionOptions { get; set; }

        public ICollection<QuestionPurposeBinder> QuestionPurposeBinders { get; set; }

        public QuestionSection QuestionSection { get; set; }

        public QuestionType QuestionType { get; set; }

    }
}