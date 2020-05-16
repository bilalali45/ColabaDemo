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

    // Message
    
    public partial class Vortex_MessageMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Vortex_Message>
    {
        public void Configure(EntityTypeBuilder<Vortex_Message> builder)
        {
            builder.ToTable("Message", "Vortex");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.AccountSid).HasColumnName(@"AccountSid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ApiVersion).HasColumnName(@"ApiVersion").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Body).HasColumnName(@"Body").HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.DateCreated).HasColumnName(@"DateCreated").HasColumnType("datetime2").IsRequired(false);
            builder.Property(x => x.DateUpdated).HasColumnName(@"DateUpdated").HasColumnType("datetime2").IsRequired(false);
            builder.Property(x => x.DateSent).HasColumnName(@"DateSent").HasColumnType("datetime2").IsRequired(false);
            builder.Property(x => x.Direction).HasColumnName(@"Direction").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ErrorCode).HasColumnName(@"ErrorCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ErrorMessage).HasColumnName(@"ErrorMessage").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.From).HasColumnName(@"FROM").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.MessagingServiceSid).HasColumnName(@"MessagingServiceSid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.NumMedia).HasColumnName(@"NumMedia").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.NumSegments).HasColumnName(@"NumSegments").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Price).HasColumnName(@"Price").HasColumnType("money(19,4)").IsRequired(false);
            builder.Property(x => x.PriceUnit).HasColumnName(@"PriceUnit").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Sid).HasColumnName(@"Sid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Status).HasColumnName(@"Status").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.SubresourceUris1).HasColumnName(@"SubresourceUris1").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(2048);
            builder.Property(x => x.SubresourceUris).HasColumnName(@"SubresourceUris").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(2048);
            builder.Property(x => x.To).HasColumnName(@"TO").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Uri).HasColumnName(@"Uri").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
