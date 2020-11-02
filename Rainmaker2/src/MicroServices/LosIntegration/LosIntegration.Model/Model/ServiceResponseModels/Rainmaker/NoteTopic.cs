













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // NoteTopic

    public partial class NoteTopic 
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
        /// Child Notes where [Note].[NoteTopicId] point to this entity (FK_Note_NoteTopic)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Note> Notes { get; set; } // Note.FK_Note_NoteTopic

        public NoteTopic()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 173;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Notes = new System.Collections.Generic.HashSet<Note>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
