













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // QuestionType

    public partial class QuestionType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Questions where [Question].[QuestionTypeId] point to this entity (FK_Question_QuestionType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Question> Questions { get; set; } // Question.FK_Question_QuestionType

        public QuestionType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 42;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Questions = new System.Collections.Generic.HashSet<Question>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
