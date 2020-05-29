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

    // LoanContactEthnicityBinder
    
    public partial class LoanContactEthnicityBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LoanContactEthnicityBinder>
    {
        public void Configure(EntityTypeBuilder<LoanContactEthnicityBinder> builder)
        {
            builder.ToTable("LoanContactEthnicityBinder", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LoanContactId).HasColumnName(@"LoanContactId").HasColumnType("int").IsRequired();
            builder.Property(x => x.EthnicityId).HasColumnName(@"EthnicityId").HasColumnType("int").IsRequired();
            builder.Property(x => x.EthnicityDetailId).HasColumnName(@"EthnicityDetailId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OtherEthnicity).HasColumnName(@"OtherEthnicity").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);

            // Foreign keys
            builder.HasOne(a => a.Ethnicity).WithMany(b => b.LoanContactEthnicityBinders).HasForeignKey(c => c.EthnicityId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContactEthnicityBinder_Ethnicity
            builder.HasOne(a => a.EthnicityDetail).WithMany(b => b.LoanContactEthnicityBinders).HasForeignKey(c => c.EthnicityDetailId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContactEthnicityBinder_EthnicityDetail
            builder.HasOne(a => a.LoanContact).WithMany(b => b.LoanContactEthnicityBinders).HasForeignKey(c => c.LoanContactId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContactEthnicityBinder_LoanContact
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
