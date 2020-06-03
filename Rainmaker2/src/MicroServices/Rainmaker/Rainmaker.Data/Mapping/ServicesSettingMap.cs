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

    // ServicesSetting
    
    public partial class ServicesSettingMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ServicesSetting>
    {
        public void Configure(EntityTypeBuilder<ServicesSetting> builder)
        {
            builder.ToTable("ServicesSetting", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("nvarchar").IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).HasColumnName(@"Password").HasColumnType("nvarchar").IsRequired().HasMaxLength(250);
            builder.Property(x => x.WebHook).HasColumnName(@"WebHook").HasColumnType("nvarchar").IsRequired().HasMaxLength(253);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.AppSid).HasColumnName(@"AppSid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.CallbackDomain).HasColumnName(@"CallbackDomain").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(253);
            builder.Property(x => x.MessageAppSid).HasColumnName(@"MessageAppSid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.VoicePushCredentialSid).HasColumnName(@"VoicePushCredentialSid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ApiServiceKeySid).HasColumnName(@"APIServiceKeySid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ApiServiceKeySecret).HasColumnName(@"APIServiceKeySecret").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ChatPushCredentialSid).HasColumnName(@"ChatPushCredentialSid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>