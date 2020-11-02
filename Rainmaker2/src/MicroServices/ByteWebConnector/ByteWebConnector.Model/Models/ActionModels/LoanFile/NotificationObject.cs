













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // notification_object

    public partial class NotificationObject 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? NotificationTypeId { get; set; } // NotificationTypeId
        public int? EntityId { get; set; } // EntityId
        public System.DateTime? CreatedOn { get; set; } // CreatedOn
        public bool? Active { get; set; } // Active

        public NotificationObject()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
