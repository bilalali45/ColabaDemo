













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Notification

    public partial class Notification 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Subject { get; set; } // Subject (length: 350)
        public string Message { get; set; } // Message (length: 1500)
        public bool IsEmployeeNotification { get; set; } // IsEmployeeNotification
        public int? OpportunityId { get; set; } // OpportunityId
        public int? EmployeeId { get; set; } // EmployeeId
        public int? CustomerId { get; set; } // CustomerId
        public string Url { get; set; } // Url (length: 50)
        public int? NotificationTypeId { get; set; } // NotificationTypeId
        public int? CriticalId { get; set; } // CriticalId
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsPosted { get; set; } // IsPosted
        public bool IsPitched { get; set; } // IsPitched
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Reverse navigation

        /// <summary>
        /// Child NotificationToes where [NotificationTo].[NotificationId] point to this entity (FK_NotificationTo_Notification)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<NotificationTo> NotificationToes { get; set; } // NotificationTo.FK_NotificationTo_Notification

        // Foreign keys

        /// <summary>
        /// Parent Customer pointed by [Notification].([CustomerId]) (FK_Notification_Customer)
        /// </summary>
        public virtual Customer Customer { get; set; } // FK_Notification_Customer

        /// <summary>
        /// Parent Employee pointed by [Notification].([EmployeeId]) (FK_Notification_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_Notification_Employee

        /// <summary>
        /// Parent Opportunity pointed by [Notification].([OpportunityId]) (FK_Notification_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_Notification_Opportunity

        public Notification()
        {
            EntityTypeId = 192;
            IsPosted = false;
            IsPitched = false;
            NotificationToes = new System.Collections.Generic.HashSet<NotificationTo>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
