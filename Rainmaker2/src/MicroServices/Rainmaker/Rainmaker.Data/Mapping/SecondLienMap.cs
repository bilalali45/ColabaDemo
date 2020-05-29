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

    // SecondLien
    
    public partial class SecondLienMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<SecondLien>
    {
        public void Configure(EntityTypeBuilder<SecondLien> builder)
        {
            builder.ToTable("SecondLien", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LoanRequestId).HasColumnName(@"LoanRequestId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SecondLienTypeId).HasColumnName(@"SecondLienTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SecondLienBalance).HasColumnName(@"SecondLienBalance").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.SecondLienLimit).HasColumnName(@"SecondLienLimit").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.SecondLienPaidAtClosing).HasColumnName(@"SecondLienPaidAtClosing").HasColumnType("bit").IsRequired();
            builder.Property(x => x.WasSmTaken).HasColumnName(@"WasSmTaken").HasColumnType("bit").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.LoanRequest).WithMany(b => b.SecondLiens).HasForeignKey(c => c.LoanRequestId).OnDelete(DeleteBehavior.SetNull); // FK_SecondLien_LoanRequest
            builder.HasOne(a => a.SecondLienType).WithMany(b => b.SecondLiens).HasForeignKey(c => c.SecondLienTypeId).OnDelete(DeleteBehavior.SetNull); // FK_SecondLien_SecondLienType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
