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
    using System;
    using System.Collections.Generic;

    // BankRateRequest
    
    public partial class BankRateRequest : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public System.DateTime? RequestStartDateUtc { get; set; } // RequestStartDateUtc
        public System.DateTime? RequestEndDateUtc { get; set; } // RequestEndDateUtc
        public bool? IsCompleted { get; set; } // IsCompleted
        public int? LastStatusId { get; set; } // LastStatusId
        public int? StatusId { get; set; } // StatusId
        public string Remarks { get; set; } // Remarks
        public int EntityTypeId { get; set; } // EntityTypeId

        // Reverse navigation

        /// <summary>
        /// Child BankRateArchives where [BankRateArchive].[BankRequestId] point to this entity (FK_BankRateArchive_BankRateRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateArchive> BankRateArchives { get; set; } // BankRateArchive.FK_BankRateArchive_BankRateRequest

        // Foreign keys

        /// <summary>
        /// Parent BankRateStage pointed by [BankRateRequest].([LastStatusId]) (FK_BankRateRequest_BankRateLastStage)
        /// </summary>
        public virtual BankRateStage LastStatus { get; set; } // FK_BankRateRequest_BankRateLastStage

        /// <summary>
        /// Parent BankRateStage pointed by [BankRateRequest].([StatusId]) (FK_BankRateRequest_BankRateStage)
        /// </summary>
        public virtual BankRateStage Status { get; set; } // FK_BankRateRequest_BankRateStage

        public BankRateRequest()
        {
            EntityTypeId = 109;
            BankRateArchives = new System.Collections.Generic.HashSet<BankRateArchive>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>