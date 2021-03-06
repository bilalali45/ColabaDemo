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

    // TransactionInfoBinder
    
    public partial class TransactionInfoBinder : URF.Core.EF.Trackable.Entity
    {
        public int LoanApplicationId { get; set; } // LoanApplicationId (Primary key)
        public int TransactionInfoId { get; set; } // TransactionInfoId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LoanApplication pointed by [TransactionInfoBinder].([LoanApplicationId]) (FK_TransactionInfoBinder_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_TransactionInfoBinder_LoanApplication

        /// <summary>
        /// Parent TransactionInfo pointed by [TransactionInfoBinder].([TransactionInfoId]) (FK_TransactionInfoBinder_TransactionInfo)
        /// </summary>
        public virtual TransactionInfo TransactionInfo { get; set; } // FK_TransactionInfoBinder_TransactionInfo

        public TransactionInfoBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
