













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // NotificationObject

    public partial class NotificationObject1 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? NotificationTypeId { get; set; } // NotificationTypeId
        public int? EntityId { get; set; } // EntityId
        public System.DateTime? CreatedOn { get; set; } // CreatedOn
        public bool? Active { get; set; } // Active
        public byte? Status { get; set; } // Status

        // Reverse navigation

        /// <summary>
        /// Child NotificationChanges where [NotificationChange].[NotificationObjectId] point to this entity (FK_NotificationChange_NotificationObject_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<NotificationChange> NotificationChanges { get; set; } // NotificationChange.FK_NotificationChange_NotificationObject_Id

        public NotificationObject1()
        {
            NotificationChanges = new System.Collections.Generic.HashSet<NotificationChange>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
