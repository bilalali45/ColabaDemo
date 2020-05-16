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

    // LendingTreeOption
    
    public partial class LendingTreeOptionMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LendingTreeOption>
    {
        public void Configure(EntityTypeBuilder<LendingTreeOption> builder)
        {
            builder.ToTable("LendingTreeOption", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LendingTreeLeadId).HasColumnName(@"LendingTreeLeadId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OptionId).HasColumnName(@"OptionId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Option).HasColumnName(@"Option").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);

            // Foreign keys
            builder.HasOne(a => a.LendingTreeLead).WithMany(b => b.LendingTreeOptions).HasForeignKey(c => c.LendingTreeLeadId).OnDelete(DeleteBehavior.SetNull); // FK_LendingTreeOption_LendingTreeLead
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
