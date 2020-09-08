// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Entity.Models
{
    // QuestionResponse

    public partial class QuestionResponse : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? SelectedOptionId { get; set; } // SelectedOptionId
        public string AnswerText { get; set; } // AnswerText (length: 50)
        public string Description { get; set; } // Description (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child BorrowerQuestionResponses where [BorrowerQuestionResponse].[ResponseId] point to this entity (FK_BorrowerQuestionResponse_QuestionResponse)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerQuestionResponse> BorrowerQuestionResponses { get; set; } // BorrowerQuestionResponse.FK_BorrowerQuestionResponse_QuestionResponse
        /// <summary>
        /// Child LoanRequestQuestionBindings where [LoanRequestQuestionBinding].[ResponseId] point to this entity (FK_LoanRequestQuestionBinding_QuestionResponse)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestQuestionBinding> LoanRequestQuestionBindings { get; set; } // LoanRequestQuestionBinding.FK_LoanRequestQuestionBinding_QuestionResponse

        // Foreign keys

        /// <summary>
        /// Parent QuestionOption pointed by [QuestionResponse].([SelectedOptionId]) (FK_QuestionResponse_QuestionOption)
        /// </summary>
        public virtual QuestionOption QuestionOption { get; set; } // FK_QuestionResponse_QuestionOption

        public QuestionResponse()
        {
            BorrowerQuestionResponses = new System.Collections.Generic.HashSet<BorrowerQuestionResponse>();
            LoanRequestQuestionBindings = new System.Collections.Generic.HashSet<LoanRequestQuestionBinding>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
