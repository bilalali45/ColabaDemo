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


namespace LoanApplicationDb.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // QuestionBorrowerDisplayOption
    
    public partial class QuestionBorrowerDisplayOption : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child Questions where [Question].[BorrowerDisplayOptionId] point to this entity (FK_Question_QuestionBorrowerDisplayOption_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Question> Questions { get; set; } // Question.FK_Question_QuestionBorrowerDisplayOption_Id
        /// <summary>
        /// Child TenantQuestionOverrides where [TenantQuestionOverride].[BorrowerDisplayOptionId] point to this entity (FK_TenantQuestionOverride_QuestionBorrowerDisplayOption_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TenantQuestionOverride> TenantQuestionOverrides { get; set; } // TenantQuestionOverride.FK_TenantQuestionOverride_QuestionBorrowerDisplayOption_Id

        public QuestionBorrowerDisplayOption()
        {
            Questions = new System.Collections.Generic.HashSet<Question>();
            TenantQuestionOverrides = new System.Collections.Generic.HashSet<TenantQuestionOverride>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
