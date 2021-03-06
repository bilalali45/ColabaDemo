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


namespace RainMaker.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RainMaker.Entity.Models;

    // LeadGenAnswerSelection
    
    public partial class LeadGenAnswerSelectionMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LeadGenAnswerSelection>
    {
        public void Configure(EntityTypeBuilder<LeadGenAnswerSelection> builder)
        {
            builder.ToTable("LeadGenAnswerSelection", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Text).HasColumnName(@"Text").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(500);
            builder.Property(x => x.QuestionId).HasColumnName(@"QuestionId").HasColumnType("int").IsRequired();
            builder.Property(x => x.NextQuestionId).HasColumnName(@"NextQuestionId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EventName).HasColumnName(@"EventName").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.NextQuestionGroup).HasColumnName(@"NextQuestionGroup").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ImageFilePath).HasColumnName(@"ImageFilePath").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("int").IsRequired(false);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
