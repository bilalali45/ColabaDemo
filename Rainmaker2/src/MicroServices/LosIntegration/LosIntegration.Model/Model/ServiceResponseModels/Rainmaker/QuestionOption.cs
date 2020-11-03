













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // QuestionOption

    public partial class QuestionOption 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? QuestionId { get; set; } // QuestionId
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool? IsOther { get; set; } // IsOther
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child QuestionResponses where [QuestionResponse].[SelectedOptionId] point to this entity (FK_QuestionResponse_QuestionOption)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QuestionResponse> QuestionResponses { get; set; } // QuestionResponse.FK_QuestionResponse_QuestionOption

        // Foreign keys

        /// <summary>
        /// Parent Question pointed by [QuestionOption].([QuestionId]) (FK_QuestionOption_Question)
        /// </summary>
        public virtual Question Question { get; set; } // FK_QuestionOption_Question

        public QuestionOption()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 103;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            QuestionResponses = new System.Collections.Generic.HashSet<QuestionResponse>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
