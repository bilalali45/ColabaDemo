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

    // TemplateForm
    
    public partial class TemplateForm : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 250)
        public string Description { get; set; } // Description (length: 500)
        public int ViewOnId { get; set; } // ViewOnId
        public int DocumentTypeId { get; set; } // DocumentTypeId
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child TemplateFormPlots where [TemplateFormPlot].[TemplateFormId] point to this entity (FK_TemplateFormPlot_TemplateForm)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateFormPlot> TemplateFormPlots { get; set; } // TemplateFormPlot.FK_TemplateFormPlot_TemplateForm
        /// <summary>
        /// Child TemplateFormSections where [TemplateFormSection].[TemplateFormId] point to this entity (FK_TemplateFormSection_TemplateForm)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateFormSection> TemplateFormSections { get; set; } // TemplateFormSection.FK_TemplateFormSection_TemplateForm

        public TemplateForm()
        {
            IsActive = true;
            EntityTypeId = 43;
            IsDefault = false;
            IsSystem = false;
            DisplayOrder = 0;
            IsDeleted = false;
            TemplateFormPlots = new System.Collections.Generic.HashSet<TemplateFormPlot>();
            TemplateFormSections = new System.Collections.Generic.HashSet<TemplateFormSection>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
