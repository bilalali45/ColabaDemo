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


namespace Milestone.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Milestone.Entity.Models;

    // MilestoneLog
    
    public partial class MilestoneLogMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<MilestoneLog>
    {
        public void Configure(EntityTypeBuilder<MilestoneLog> builder)
        {
            builder.ToTable("MilestoneLog", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LoanApplicationId).HasColumnName(@"LoanApplicationId").HasColumnType("int").IsRequired();
            builder.Property(x => x.MilestoneId).HasColumnName(@"MilestoneId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CreatedDateUtc).HasColumnName(@"CreatedDateUtc").HasColumnType("datetime2").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Milestone).WithMany(b => b.MilestoneLogs).HasForeignKey(c => c.MilestoneId).OnDelete(DeleteBehavior.SetNull); // FK_MilestoneLog_Milestone_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
