














namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Acl
    
    public partial class Acl 
    {
        public int UserId { get; set; } // UserId (Primary key)
        public int EntityRefTypeId { get; set; } // EntityRefTypeId (Primary key)
        public int EntityRefId { get; set; } // EntityRefId (Primary key)
        public bool EditPermit { get; set; } // EditPermit
        public string EditLogic { get; set; } // EditLogic (length: 500)
        public bool DeletePermit { get; set; } // DeletePermit
        public string DeleteLogic { get; set; } // DeleteLogic (length: 500)
        public bool ViewPermit { get; set; } // ViewPermit
        public string ViewLogic { get; set; } // ViewLogic (length: 500)
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Foreign keys

        /// <summary>
        /// Parent EntityType pointed by [Acl].([EntityRefTypeId]) (FK_Acl_EntityType)
        /// </summary>
        public virtual EntityType EntityType { get; set; } // FK_Acl_EntityType

        /// <summary>
        /// Parent UserProfile pointed by [Acl].([UserId]) (FK_Acl_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_Acl_UserProfile

        public Acl()
        {
            EditPermit = true;
            DeletePermit = true;
            ViewPermit = true;
            EntityTypeId = 11;
            IsSystem = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
