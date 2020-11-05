













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // WorkFlow

    public partial class WorkFlow 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StatusIdTo { get; set; } // StatusIdTo
        public int? StatusIdFrom { get; set; } // StatusIdFrom
        public bool IsSystem { get; set; } // IsSystem
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Foreign keys

        /// <summary>
        /// Parent StatusList pointed by [WorkFlow].([StatusIdFrom]) (FK_WorkFlow_StatusListFrom)
        /// </summary>
        public virtual StatusList StatusList_StatusIdFrom { get; set; } // FK_WorkFlow_StatusListFrom

        /// <summary>
        /// Parent StatusList pointed by [WorkFlow].([StatusIdTo]) (FK_WorkFlow_StatusList)
        /// </summary>
        public virtual StatusList StatusList_StatusIdTo { get; set; } // FK_WorkFlow_StatusList

        public WorkFlow()
        {
            IsSystem = false;
            IsActive = true;
            EntityTypeId = 84;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>