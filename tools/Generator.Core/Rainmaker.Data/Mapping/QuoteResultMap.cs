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

    // QuoteResult
    
    public partial class QuoteResultMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<QuoteResult>
    {
        public void Configure(EntityTypeBuilder<QuoteResult> builder)
        {
            builder.ToTable("QuoteResult", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.VisitorId).HasColumnName(@"VisitorId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanRequestId).HasColumnName(@"LoanRequestId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CustomerId).HasColumnName(@"CustomerId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EmployeId).HasColumnName(@"EmployeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedFromId).HasColumnName(@"CreatedFromId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.RequestXml).HasColumnName(@"RequestXml").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.FinalXml).HasColumnName(@"FinalXml").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.HasResult).HasColumnName(@"HasResult").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.HasError).HasColumnName(@"HasError").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.ErrorMessage).HasColumnName(@"ErrorMessage").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.BenchMarkRateId).HasColumnName(@"BenchMarkRateId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OriginalQuoteId).HasColumnName(@"OriginalQuoteId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OriginalQuoteOnUtc).HasColumnName(@"OriginalQuoteOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.Guid).HasColumnName(@"Guid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);

            // Foreign keys
            builder.HasOne(a => a.BenchMarkRate).WithMany(b => b.QuoteResults).HasForeignKey(c => c.BenchMarkRateId).OnDelete(DeleteBehavior.SetNull); // FK_QuoteResult_BenchMarkRate
            builder.HasOne(a => a.Customer).WithMany(b => b.QuoteResults).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.SetNull); // FK_QuoteResult_Customer
            builder.HasOne(a => a.Employee).WithMany(b => b.QuoteResults).HasForeignKey(c => c.EmployeId).OnDelete(DeleteBehavior.SetNull); // FK_QuoteResult_Employee
            builder.HasOne(a => a.LoanRequest).WithMany(b => b.QuoteResults).HasForeignKey(c => c.LoanRequestId).OnDelete(DeleteBehavior.SetNull); // FK_QuoteResult_LoanRequest
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
