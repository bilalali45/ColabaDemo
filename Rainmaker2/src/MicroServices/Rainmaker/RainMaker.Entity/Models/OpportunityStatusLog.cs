// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Entity.Models
{
    // OpportunityStatusLog

    public partial class OpportunityStatusLog : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StatusId { get; set; } // StatusId
        public int? StatusCauseId { get; set; } // StatusCauseId
        public System.DateTime? DatetimeUtc { get; set; } // DatetimeUtc
        public int? OpportunityId { get; set; } // OpportunityId
        public bool IsActive { get; set; } // IsActive
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent Opportunity pointed by [OpportunityStatusLog].([OpportunityId]) (FK_OpportunityStatusLog_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_OpportunityStatusLog_Opportunity

        /// <summary>
        /// Parent StatusCause pointed by [OpportunityStatusLog].([StatusCauseId]) (FK_OpportunityStatusLog_StatusCause)
        /// </summary>
        public virtual StatusCause StatusCause { get; set; } // FK_OpportunityStatusLog_StatusCause

        /// <summary>
        /// Parent StatusList pointed by [OpportunityStatusLog].([StatusId]) (FK_OpportunityStatusLog_StatusList)
        /// </summary>
        public virtual StatusList StatusList { get; set; } // FK_OpportunityStatusLog_StatusList

        public OpportunityStatusLog()
        {
            IsActive = true;
            EntityTypeId = 129;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
