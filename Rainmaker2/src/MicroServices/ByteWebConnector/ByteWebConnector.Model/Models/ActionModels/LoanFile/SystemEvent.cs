













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // SystemEvent

    public partial class SystemEvent 
    {
        public int Id { get; set; } // Id (Primary key)
        public string SystemKeyword { get; set; } // SystemKeyword (length: 100)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child SystemEventLogs where [SystemEventLog].[EventTypeId] point to this entity (FK_SystemEventLog_SystemEvent)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SystemEventLog> SystemEventLogs { get; set; } // SystemEventLog.FK_SystemEventLog_SystemEvent

        public SystemEvent()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            EntityTypeId = 98;
            IsDeleted = false;
            SystemEventLogs = new System.Collections.Generic.HashSet<SystemEventLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
