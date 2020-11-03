













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // NoteDetail

    public partial class NoteDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? NoteId { get; set; } // NoteId
        public string Message { get; set; } // Message
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted

        // Foreign keys

        /// <summary>
        /// Parent Note pointed by [NoteDetail].([NoteId]) (FK_NoteDetail_Note)
        /// </summary>
        public virtual Note Note { get; set; } // FK_NoteDetail_Note

        /// <summary>
        /// Parent UserProfile pointed by [NoteDetail].([ModifiedBy]) (FK_NoteDetail_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_NoteDetail_UserProfile

        public NoteDetail()
        {
            IsActive = true;
            EntityTypeId = 69;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
