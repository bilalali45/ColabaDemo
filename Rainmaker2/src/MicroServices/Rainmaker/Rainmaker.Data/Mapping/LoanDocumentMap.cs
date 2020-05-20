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

    // LoanDocument
    
    public partial class LoanDocumentMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LoanDocument>
    {
        public void Configure(EntityTypeBuilder<LoanDocument> builder)
        {
            builder.ToTable("LoanDocument", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ClientFileName).HasColumnName(@"ClientFileName").HasColumnType("nvarchar").IsRequired().HasMaxLength(255);
            builder.Property(x => x.ServerFileName).HasColumnName(@"ServerFileName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LoanApplicationId).HasColumnName(@"LoanApplicationId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanDocumentTypeId).HasColumnName(@"LoanDocumentTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanDocumentSubTypeId).HasColumnName(@"LoanDocumentSubTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanDocumentStatusId).HasColumnName(@"LoanDocumentStatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.MessageForCustomer).HasColumnName(@"MessageForCustomer").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.LoanApplication).WithMany(b => b.LoanDocuments).HasForeignKey(c => c.LoanApplicationId).OnDelete(DeleteBehavior.SetNull); // FK_LoanDocument_LoanApplication
            builder.HasOne(a => a.LoanDocumentStatusList).WithMany(b => b.LoanDocuments).HasForeignKey(c => c.LoanDocumentStatusId).OnDelete(DeleteBehavior.SetNull); // FK_LoanDocument_LoanDocumentStatusList
            builder.HasOne(a => a.LoanDocumentSubType).WithMany(b => b.LoanDocuments).HasForeignKey(c => c.LoanDocumentSubTypeId).OnDelete(DeleteBehavior.SetNull); // FK_LoanDocument_LoanDocumentSubType
            builder.HasOne(a => a.LoanDocumentType).WithMany(b => b.LoanDocuments).HasForeignKey(c => c.LoanDocumentTypeId).OnDelete(DeleteBehavior.SetNull); // FK_LoanDocument_LoanDocumentType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
