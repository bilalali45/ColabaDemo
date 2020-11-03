













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // NoteSubject

    public partial class NoteSubject 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Subject { get; set; } // Subject
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        public NoteSubject()
        {
            IsActive = true;
            EntityTypeId = 154;
            IsSystem = false;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
