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
    // TemplateFormPlotDetail

    public partial class TemplateFormPlotDetail : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? TemplateFormPlotId { get; set; } // TemplateFormPlotId
        public int? DisplayOrder { get; set; } // DisplayOrder
        public int? FeeId { get; set; } // FeeId
        public string Description { get; set; } // Description (length: 500)
        public bool IsDeleted { get; set; } // IsDeleted

        // Foreign keys

        /// <summary>
        /// Parent Fee pointed by [TemplateFormPlotDetail].([FeeId]) (FK_TemplateFormPlotDetail_Fee)
        /// </summary>
        public virtual Fee Fee { get; set; } // FK_TemplateFormPlotDetail_Fee

        /// <summary>
        /// Parent TemplateFormPlot pointed by [TemplateFormPlotDetail].([TemplateFormPlotId]) (FK_TemplateFormPlotDetail_TemplateFormPlot)
        /// </summary>
        public virtual TemplateFormPlot TemplateFormPlot { get; set; } // FK_TemplateFormPlotDetail_TemplateFormPlot

        public TemplateFormPlotDetail()
        {
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
