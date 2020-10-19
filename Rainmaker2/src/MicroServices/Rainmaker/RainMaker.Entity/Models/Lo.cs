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

    // Los
    
    public partial class Lo : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 250)

        // Reverse navigation

        /// <summary>
        /// Child LosLoanApplicationBinders where [LosLoanApplicationBinder].[LosId] point to this entity (FK_LosLoanApplicationBinder_Los)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LosLoanApplicationBinder> LosLoanApplicationBinders { get; set; } // LosLoanApplicationBinder.FK_LosLoanApplicationBinder_Los
        /// <summary>
        /// Child LosSyncLogs where [LosSyncLog].[LosId] point to this entity (FK_LosSyncLog_Los)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LosSyncLog> LosSyncLogs { get; set; } // LosSyncLog.FK_LosSyncLog_Los

        public Lo()
        {
            LosLoanApplicationBinders = new System.Collections.Generic.HashSet<LosLoanApplicationBinder>();
            LosSyncLogs = new System.Collections.Generic.HashSet<LosSyncLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>