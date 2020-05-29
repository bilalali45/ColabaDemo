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

    // ProfitTable
    
    public partial class ProfitTableMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ProfitTable>
    {
        public void Configure(EntityTypeBuilder<ProfitTable> builder)
        {
            builder.ToTable("ProfitTable", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.RuleId).HasColumnName(@"RuleId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CalcBaseOnId).HasColumnName(@"CalcBaseOnId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.RoundedTypeId).HasColumnName(@"RoundedTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Value).HasColumnName(@"Value").HasColumnType("decimal(18,3)").IsRequired(false);
            builder.Property(x => x.FormulaId).HasColumnName(@"FormulaId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.RangeSetId).HasColumnName(@"RangeSetId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CalcTypeId).HasColumnName(@"CalcTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsSystem).HasColumnName(@"IsSystem").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Formula).WithMany(b => b.ProfitTables).HasForeignKey(c => c.FormulaId).OnDelete(DeleteBehavior.SetNull); // FK_ProfitTable_Formula
            builder.HasOne(a => a.RangeSet).WithMany(b => b.ProfitTables).HasForeignKey(c => c.RangeSetId).OnDelete(DeleteBehavior.SetNull); // FK_ProfitTable_RangeSet
            builder.HasOne(a => a.Rule).WithMany(b => b.ProfitTables).HasForeignKey(c => c.RuleId).OnDelete(DeleteBehavior.SetNull); // FK_ProfitTable_Rule
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
