













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // QuestionGroup

    public partial class QuestionGroup 
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
        /// Child QuestionSections where [QuestionSection].[QuestionGroupId] point to this entity (FK_QuestionSection_QuestionGroup)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QuestionSection> QuestionSections { get; set; } // QuestionSection.FK_QuestionSection_QuestionGroup

        public QuestionGroup()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 64;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            QuestionSections = new System.Collections.Generic.HashSet<QuestionSection>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
