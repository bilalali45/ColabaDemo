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

    // OtherEmploymentIncomeHistory
    
    public partial class OtherEmploymentIncomeHistoryMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<OtherEmploymentIncomeHistory>
    {
        public void Configure(EntityTypeBuilder<OtherEmploymentIncomeHistory> builder)
        {
            builder.ToTable("OtherEmploymentIncomeHistory", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.OtherEmploymentIncomeId).HasColumnName(@"OtherEmploymentIncomeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Year).HasColumnName(@"Year").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.AnnualIncome).HasColumnName(@"AnnualIncome").HasColumnType("decimal(18,0)").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.OtherEmploymentIncome).WithMany(b => b.OtherEmploymentIncomeHistories).HasForeignKey(c => c.OtherEmploymentIncomeId).OnDelete(DeleteBehavior.SetNull); // FK_OtherEmploymentIncomeHistory_OtherEmploymentIncome
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
