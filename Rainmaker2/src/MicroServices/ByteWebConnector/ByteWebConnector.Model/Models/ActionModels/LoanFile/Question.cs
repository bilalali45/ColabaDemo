













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Question

    public partial class Question 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 1000)
        public int? QuestionTypeId { get; set; } // QuestionTypeId
        public int? QuestionSectionId { get; set; } // QuestionSectionId
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsRequired { get; set; } // IsRequired
        public bool IsCustomerQuestion { get; set; } // IsCustomerQuestion
        public string DefaultValue { get; set; } // DefaultValue (length: 50)
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public int? QuestionForId { get; set; } // QuestionForId
        public bool? IsDescriptionRequired { get; set; } // IsDescriptionRequired

        // Reverse navigation

        /// <summary>
        /// Child BorrowerQuestionResponses where [BorrowerQuestionResponse].[QuestionId] point to this entity (FK_BorrowerQuestionResponse_Question)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerQuestionResponse> BorrowerQuestionResponses { get; set; } // BorrowerQuestionResponse.FK_BorrowerQuestionResponse_Question
        /// <summary>
        /// Child LoanRequestQuestionBindings where [LoanRequestQuestionBinding].[QuestionId] point to this entity (FK_LoanRequestQuestionBinding_Question)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestQuestionBinding> LoanRequestQuestionBindings { get; set; } // LoanRequestQuestionBinding.FK_LoanRequestQuestionBinding_Question
        /// <summary>
        /// Child QuestionOptions where [QuestionOption].[QuestionId] point to this entity (FK_QuestionOption_Question)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QuestionOption> QuestionOptions { get; set; } // QuestionOption.FK_QuestionOption_Question
        /// <summary>
        /// Child QuestionPurposeBinders where [QuestionPurposeBinder].[QuestionId] point to this entity (FK_QuestionPurposeBinder_Question)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QuestionPurposeBinder> QuestionPurposeBinders { get; set; } // QuestionPurposeBinder.FK_QuestionPurposeBinder_Question

        // Foreign keys

        /// <summary>
        /// Parent QuestionSection pointed by [Question].([QuestionSectionId]) (FK_Question_QuestionSection)
        /// </summary>
        public virtual QuestionSection QuestionSection { get; set; } // FK_Question_QuestionSection

        /// <summary>
        /// Parent QuestionType pointed by [Question].([QuestionTypeId]) (FK_Question_QuestionType)
        /// </summary>
        public virtual QuestionType QuestionType { get; set; } // FK_Question_QuestionType

        public Question()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 53;
            IsCustomerQuestion = false;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BorrowerQuestionResponses = new System.Collections.Generic.HashSet<BorrowerQuestionResponse>();
            LoanRequestQuestionBindings = new System.Collections.Generic.HashSet<LoanRequestQuestionBinding>();
            QuestionOptions = new System.Collections.Generic.HashSet<QuestionOption>();
            QuestionPurposeBinders = new System.Collections.Generic.HashSet<QuestionPurposeBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
