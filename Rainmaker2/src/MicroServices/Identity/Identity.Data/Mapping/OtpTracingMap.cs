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


namespace Identity.Data.Mapping
{
    using Identity.Entity.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    // OtpTracing
    
    public partial class OtpTracingMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<OtpTracing>
    {
        public void Configure(EntityTypeBuilder<OtpTracing> builder)
        {
            builder.ToTable("OtpTracing", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Phone).HasColumnName(@"Phone").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.IpAddress).HasColumnName(@"IpAddress").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.DateUtc).HasColumnName(@"DateUtc").HasColumnType("datetime2").IsRequired(false);
            builder.Property(x => x.TracingTypeId).HasColumnName(@"TracingTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CodeEntered).HasColumnName(@"CodeEntered").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(6);
            builder.Property(x => x.ContactId).HasColumnName(@"ContactId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OtpCreatedOn).HasColumnName(@"OtpCreatedOn").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.OtpUpdatedOn).HasColumnName(@"OtpUpdatedOn").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.StatusCode).HasColumnName(@"StatusCode").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Message).HasColumnName(@"Message").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ResponseJson).HasColumnName(@"ResponseJson").HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.BranchId).HasColumnName(@"BranchId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CarrierType).HasColumnName(@"CarrierType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CarrierName).HasColumnName(@"CarrierName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OtpRequestId).HasColumnName(@"OtpRequestId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
