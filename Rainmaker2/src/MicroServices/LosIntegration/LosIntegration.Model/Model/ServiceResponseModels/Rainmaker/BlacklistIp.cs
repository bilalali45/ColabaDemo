













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BlacklistIp

    public partial class BlacklistIp 
    {
        public int Id { get; set; } // Id (Primary key)
        public long IpFrom { get; set; } // IpFrom
        public long IpTo { get; set; } // IpTo
        public int? FamilyId { get; set; } // FamilyId
        public string Description { get; set; } // Description (length: 500)
        public bool IsAllow { get; set; } // IsAllow
        public int? ApplicationId { get; set; } // ApplicationId
        public bool IsActive { get; set; } // IsActive
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted

        public BlacklistIp()
        {
            IsAllow = false;
            IsActive = true;
            EntityTypeId = 163;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
