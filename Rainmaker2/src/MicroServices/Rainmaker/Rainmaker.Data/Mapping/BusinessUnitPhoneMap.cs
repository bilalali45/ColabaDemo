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

    // BusinessUnitPhone
    
    public partial class BusinessUnitPhoneMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<BusinessUnitPhone>
    {
        public void Configure(EntityTypeBuilder<BusinessUnitPhone> builder)
        {
            builder.ToTable("BusinessUnitPhone", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.BusinessUnitId).HasColumnName(@"BusinessUnitId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CompanyPhoneInfoId).HasColumnName(@"CompanyPhoneInfoId").HasColumnType("int").IsRequired();
            builder.Property(x => x.TypeId).HasColumnName(@"TypeId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.BusinessUnit).WithMany(b => b.BusinessUnitPhones).HasForeignKey(c => c.BusinessUnitId).OnDelete(DeleteBehavior.SetNull); // FK_BusinessUnitPhone_BusinessUnit
            builder.HasOne(a => a.CompanyPhoneInfo).WithMany(b => b.BusinessUnitPhones).HasForeignKey(c => c.CompanyPhoneInfoId).OnDelete(DeleteBehavior.SetNull); // FK_BusinessUnitPhone_CompanyPhoneInfo
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
