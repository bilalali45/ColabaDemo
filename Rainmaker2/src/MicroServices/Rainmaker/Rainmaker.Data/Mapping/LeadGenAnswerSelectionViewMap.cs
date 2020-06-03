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

    // LeadGenAnswerSelectionView
    
    public partial class LeadGenAnswerSelectionViewMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LeadGenAnswerSelectionView>
    {
        public void Configure(EntityTypeBuilder<LeadGenAnswerSelectionView> builder)
        {
            builder.ToTable("LeadGenAnswerSelectionView", "dbo");
            builder.HasKey(x => new { x.QuestionId, x.QuestionText, x.AnswerId, x.TableName });

            builder.Property(x => x.QuestionGroupId).HasColumnName(@"QuestionGroupId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.QuestionId).HasColumnName(@"QuestionId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.QuestionText).HasColumnName(@"QuestionText").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(1000).ValueGeneratedNever();
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.AnswerId).HasColumnName(@"AnswerID").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.TableName).HasColumnName(@"TableName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(23).ValueGeneratedNever();
            builder.Property(x => x.AnswerText).HasColumnName(@"AnswerText").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.NexQuestionId).HasColumnName(@"NexQuestionId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EventName).HasColumnName(@"EventName").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.NextQuestionGroup).HasColumnName(@"NextQuestionGroup").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.AnswerDisplayOrder).HasColumnName(@"AnswerDisplayOrder").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ImagePath).HasColumnName(@"ImagePath").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(1028);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>