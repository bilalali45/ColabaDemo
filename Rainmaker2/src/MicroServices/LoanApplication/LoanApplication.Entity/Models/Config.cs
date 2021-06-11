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

    // Config
    
    public partial class Config : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public int DisplayOrder { get; set; } // DisplayOrder

        // Reverse navigation

        /// <summary>
        /// Child ConfigSelections where [ConfigSelection].[ConfigId] point to this entity (FK_LoanApplicationConfigSelection_LoanApplicationConfig_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ConfigSelection> ConfigSelections { get; set; } // ConfigSelection.FK_LoanApplicationConfigSelection_LoanApplicationConfig_Id

        public Config()
        {
            DisplayOrder = 0;
            ConfigSelections = new System.Collections.Generic.HashSet<ConfigSelection>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>