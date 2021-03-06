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

    // Acl
    
    public partial class AclMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Acl>
    {
        public void Configure(EntityTypeBuilder<Acl> builder)
        {
            builder.ToTable("Acl", "dbo");
            builder.HasKey(x => new { x.UserId, x.EntityRefTypeId, x.EntityRefId });

            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EntityRefTypeId).HasColumnName(@"EntityRefTypeId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EntityRefId).HasColumnName(@"EntityRefId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EditPermit).HasColumnName(@"EditPermit").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EditLogic).HasColumnName(@"EditLogic").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.DeletePermit).HasColumnName(@"DeletePermit").HasColumnType("bit").IsRequired();
            builder.Property(x => x.DeleteLogic).HasColumnName(@"DeleteLogic").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ViewPermit).HasColumnName(@"ViewPermit").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ViewLogic).HasColumnName(@"ViewLogic").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsSystem).HasColumnName(@"IsSystem").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.EntityType).WithMany(b => b.Acls).HasForeignKey(c => c.EntityRefTypeId).OnDelete(DeleteBehavior.SetNull); // FK_Acl_EntityType
            builder.HasOne(a => a.UserProfile).WithMany(b => b.Acls).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.SetNull); // FK_Acl_UserProfile
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
