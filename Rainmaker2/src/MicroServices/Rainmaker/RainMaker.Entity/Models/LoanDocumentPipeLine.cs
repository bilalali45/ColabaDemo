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
    // LoanDocumentPipeLine

    public partial class LoanDocumentPipeLine : URF.Core.EF.Trackable.Entity
    {
        public int LoanApplicationId { get; set; } // LoanApplicationId (Primary key)
        public System.DateTime? DocumentUploadDateUtc { get; set; } // DocumentUploadDateUtc
        public System.DateTime? DocumentRequestSentDateUtc { get; set; } // DocumentRequestSentDateUtc
        public int? DocumentRemaining { get; set; } // DocumentRemaining
        public int? DocumentOutstanding { get; set; } // DocumentOutstanding
        public int? DocumentCompleted { get; set; } // DocumentCompleted
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc

        // Foreign keys

        /// <summary>
        /// Parent LoanApplication pointed by [LoanDocumentPipeLine].([LoanApplicationId]) (FK_LoanDocumentPipeLine_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_LoanDocumentPipeLine_LoanApplication

        public LoanDocumentPipeLine()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
