













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Team

    public partial class Team 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public string ScheduleUrl { get; set; } // ScheduleUrl (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child Opportunities where [Opportunity].[TeamId] point to this entity (FK_Opportunity_Team)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_Team
        /// <summary>
        /// Child TeamMembers where [TeamMember].[TeamId] point to this entity (FK_TeamMember_Team)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TeamMember> TeamMembers { get; set; } // TeamMember.FK_TeamMember_Team

        public Team()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 46;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            TeamMembers = new System.Collections.Generic.HashSet<TeamMember>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
