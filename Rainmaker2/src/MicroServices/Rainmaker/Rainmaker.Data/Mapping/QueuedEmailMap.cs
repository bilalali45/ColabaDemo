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

    // QueuedEmail
    
    public partial class QueuedEmailMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<QueuedEmail>
    {
        public void Configure(EntityTypeBuilder<QueuedEmail> builder)
        {
            builder.ToTable("QueuedEmail", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Priority).HasColumnName(@"Priority").HasColumnType("int").IsRequired();
            builder.Property(x => x.FromAddress).HasColumnName(@"FromAddress").HasColumnType("nvarchar").IsRequired().HasMaxLength(500);
            builder.Property(x => x.FromName).HasColumnName(@"FromName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ToAddress).HasColumnName(@"ToAddress").HasColumnType("nvarchar").IsRequired().HasMaxLength(500);
            builder.Property(x => x.ToName).HasColumnName(@"ToName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Cc).HasColumnName(@"Cc").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Bcc).HasColumnName(@"Bcc").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Subject).HasColumnName(@"Subject").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.Body).HasColumnName(@"Body").HasColumnType("ntext").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.SentTries).HasColumnName(@"SentTries").HasColumnType("int").IsRequired();
            builder.Property(x => x.SentOnUtc).HasColumnName(@"SentOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.EmailAccountId).HasColumnName(@"EmailAccountId").HasColumnType("int").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.EmailAccount).WithMany(b => b.QueuedEmails).HasForeignKey(c => c.EmailAccountId).OnDelete(DeleteBehavior.SetNull); // FK_QueuedEmail_EmailAccount
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
