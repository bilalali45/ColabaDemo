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


namespace LoanApplicationDb.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // InActiveSetupItemSection
    
    public partial class InActiveSetupItemSection : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child InActiveSetupItemsSectionWises where [InActiveSetupItemsSectionWise].[SectionId] point to this entity (FK_InActiveSetupItemsSectionWise_InActiveSetupItemSection_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<InActiveSetupItemsSectionWise> InActiveSetupItemsSectionWises { get; set; } // InActiveSetupItemsSectionWise.FK_InActiveSetupItemsSectionWise_InActiveSetupItemSection_Id

        public InActiveSetupItemSection()
        {
            InActiveSetupItemsSectionWises = new System.Collections.Generic.HashSet<InActiveSetupItemsSectionWise>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>