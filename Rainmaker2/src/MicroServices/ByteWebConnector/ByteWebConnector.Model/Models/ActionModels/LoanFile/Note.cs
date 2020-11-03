













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Note

    public partial class Note 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? NoteTopicId { get; set; } // NoteTopicId
        public string Subject { get; set; } // Subject
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? EntityRefTypeId { get; set; } // EntityRefTypeId
        public int? EntityRefId { get; set; } // EntityRefId
        public bool IsActive { get; set; } // IsActive
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child NoteDepartmentBinders where [NoteDepartmentBinder].[NoteId] point to this entity (FK_NoteDepartmentBinder_Note)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<NoteDepartmentBinder> NoteDepartmentBinders { get; set; } // NoteDepartmentBinder.FK_NoteDepartmentBinder_Note
        /// <summary>
        /// Child NoteDetails where [NoteDetail].[NoteId] point to this entity (FK_NoteDetail_Note)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<NoteDetail> NoteDetails { get; set; } // NoteDetail.FK_NoteDetail_Note
        

        // Foreign keys

        /// <summary>
        /// Parent EntityType pointed by [Note].([EntityRefTypeId]) (FK_Note_EntityRefType)
        /// </summary>
        public virtual EntityType EntityRefType { get; set; } // FK_Note_EntityRefType

        /// <summary>
        /// Parent EntityType pointed by [Note].([EntityTypeId]) (FK_Note_EntityType)
        /// </summary>
        public virtual EntityType EntityType_EntityTypeId { get; set; } // FK_Note_EntityType

        /// <summary>
        /// Parent NoteTopic pointed by [Note].([NoteTopicId]) (FK_Note_NoteTopic)
        /// </summary>
        public virtual NoteTopic NoteTopic { get; set; } // FK_Note_NoteTopic

        /// <summary>
        /// Parent UserProfile pointed by [Note].([ModifiedBy]) (FK_Note_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_Note_UserProfile

        public Note()
        {
            EntityTypeId = 48;
            IsActive = true;
            IsDeleted = false;
            
            NoteDepartmentBinders = new System.Collections.Generic.HashSet<NoteDepartmentBinder>();
            NoteDetails = new System.Collections.Generic.HashSet<NoteDetail>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
