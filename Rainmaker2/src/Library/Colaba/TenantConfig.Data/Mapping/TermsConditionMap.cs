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


namespace TenantConfig.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TenantConfig.Entity.Models;

    // TermsCondition
    
    public partial class TermsConditionMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<TermsCondition>
    {
        public void Configure(EntityTypeBuilder<TermsCondition> builder)
        {
            builder.ToTable("TermsCondition", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.BranchId).HasColumnName(@"BranchId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TermTypeId).HasColumnName(@"TermTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TermsContent).HasColumnName(@"TermsContent").HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Branch).WithMany(b => b.TermsConditions).HasForeignKey(c => c.BranchId).OnDelete(DeleteBehavior.SetNull); // FK_TermsCondition_Branch_Id
            builder.HasOne(a => a.Tenant).WithMany(b => b.TermsConditions).HasForeignKey(c => c.TenantId).OnDelete(DeleteBehavior.SetNull); // FK_TermsCondition_Tenant_Id
            builder.HasOne(a => a.TermsConditionType).WithMany(b => b.TermsConditions).HasForeignKey(c => c.TermTypeId).OnDelete(DeleteBehavior.SetNull); // FK_TermsCondition_TermsConditionType_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
