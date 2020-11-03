













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // NotificationChange

    public partial class NotificationChange 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? NotificationObjectId { get; set; } // NotificationObjectId
        public int? ActorId { get; set; } // ActorId
        public string Active { get; set; } // Active (length: 50)
        public byte? Status { get; set; } // Status

        // Foreign keys

        /// <summary>
        /// Parent NotificationObject pointed by [NotificationChange].([NotificationObjectId]) (FK_NotificationChange_NotificationObject_Id)
        /// </summary>
        public virtual NotificationObject NotificationObject { get; set; } // FK_NotificationChange_NotificationObject_Id

        public NotificationChange()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
