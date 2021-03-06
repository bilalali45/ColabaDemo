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

    // LoanDocumentPipeLineView
    
    public partial class LoanDocumentPipeLineViewMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LoanDocumentPipeLineView>
    {
        public void Configure(EntityTypeBuilder<LoanDocumentPipeLineView> builder)
        {
            builder.ToTable("LoanDocumentPipeLineView", "dbo");
            builder.HasKey(x => new { x.Id, x.ApplicationStatus, x.CustomerName });

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ByteLoanNumber).HasColumnName(@"ByteLoanNumber").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ApplicationStatus).HasColumnName(@"ApplicationStatus").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).ValueGeneratedNever();
            builder.Property(x => x.CustomerName).HasColumnName(@"CustomerName").HasColumnType("nvarchar").IsRequired().HasMaxLength(601).ValueGeneratedNever();
            builder.Property(x => x.BusinessUnitId).HasColumnName(@"BusinessUnitId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DocumentUploadDateUtc).HasColumnName(@"DocumentUploadDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.DocumentRequestSentDateUtc).HasColumnName(@"DocumentRequestSentDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.DocumentRemaining).HasColumnName(@"DocumentRemaining").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DocumentOutstanding).HasColumnName(@"DocumentOutstanding").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DocumentCompleted).HasColumnName(@"DocumentCompleted").HasColumnType("int").IsRequired(false);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
