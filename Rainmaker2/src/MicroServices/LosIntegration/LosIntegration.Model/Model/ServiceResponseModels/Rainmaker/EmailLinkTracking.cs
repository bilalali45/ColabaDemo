













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // EmailLinkTracking

    public partial class EmailLinkTracking 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 200)
        public string TrackingKey { get; set; } // TrackingKey (length: 50)
        public string Description { get; set; } // Description (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child EmailTrackings where [EmailTracking].[EmailLinkTrackingId] point to this entity (FK_EmailTracking_EmailLinkTracking)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmailTracking> EmailTrackings { get; set; } // EmailTracking.FK_EmailTracking_EmailLinkTracking

        public EmailLinkTracking()
        {
            EmailTrackings = new System.Collections.Generic.HashSet<EmailTracking>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
