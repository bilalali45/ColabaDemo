













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // AuditTrail

    public partial class AuditTrail 
    {
        public int Id { get; set; } // Id (Primary key)
        public System.DateTime? DateTimeUtc { get; set; } // DateTimeUtc
        public int? EntityRefTypeId { get; set; } // EntityRefTypeId
        public int EntityTypeId { get; set; } // EntityTypeId
        public string TableName { get; set; } // TableName (length: 256)
        public int? UserId { get; set; } // UserId
        public string Actions { get; set; } // Actions (length: 1)
        public int? RecordId { get; set; } // RecordId
        public string OldData { get; set; } // OldData
        public string NewData { get; set; } // NewData
        public string ChangedColumns { get; set; } // ChangedColumns

        // Foreign keys

        /// <summary>
        /// Parent EntityType pointed by [AuditTrail].([EntityTypeId]) (FK_AuditTrail_EntityType)
        /// </summary>
        public virtual EntityType EntityType { get; set; } // FK_AuditTrail_EntityType

        /// <summary>
        /// Parent UserProfile pointed by [AuditTrail].([UserId]) (FK_AuditTrail_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_AuditTrail_UserProfile

        public AuditTrail()
        {
            EntityTypeId = 89;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
