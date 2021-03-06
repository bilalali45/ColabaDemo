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

    // ReviewComment
    
    public partial class ReviewCommentMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ReviewComment>
    {
        public void Configure(EntityTypeBuilder<ReviewComment> builder)
        {
            builder.ToTable("ReviewComment", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ReviewComment_).HasColumnName(@"ReviewComment").HasColumnType("nvarchar").IsRequired().HasMaxLength(1000);
            builder.Property(x => x.CustomerName).HasColumnName(@"CustomerName").HasColumnType("nvarchar").IsRequired().HasMaxLength(500);
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.ReviewSiteId).HasColumnName(@"ReviewSiteId").HasColumnType("int").IsRequired();
            builder.Property(x => x.LoanPurposeId).HasColumnName(@"LoanPurposeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.BusinessUnitId).HasColumnName(@"BusinessUnitId").HasColumnType("int").IsRequired();
            builder.Property(x => x.StateId).HasColumnName(@"StateId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CountyId).HasColumnName(@"CountyId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CityId).HasColumnName(@"CityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CityName).HasColumnName(@"CityName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ZipCode).HasColumnName(@"ZipCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.StreetAddress).HasColumnName(@"StreetAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.StarRating).HasColumnName(@"StarRating").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DisplayReviewPage).HasColumnName(@"DisplayReviewPage").HasColumnType("bit").IsRequired();
            builder.Property(x => x.DisplayCustomerPage).HasColumnName(@"DisplayCustomerPage").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.BusinessUnit).WithMany(b => b.ReviewComments).HasForeignKey(c => c.BusinessUnitId).OnDelete(DeleteBehavior.SetNull); // FK_ReviewComment_BusinessUnit
            builder.HasOne(a => a.City).WithMany(b => b.ReviewComments).HasForeignKey(c => c.CityId).OnDelete(DeleteBehavior.SetNull); // FK_ReviewComment_City
            builder.HasOne(a => a.County).WithMany(b => b.ReviewComments).HasForeignKey(c => c.CountyId).OnDelete(DeleteBehavior.SetNull); // FK_ReviewComment_County
            builder.HasOne(a => a.LoanPurpose).WithMany(b => b.ReviewComments).HasForeignKey(c => c.LoanPurposeId).OnDelete(DeleteBehavior.SetNull); // FK_ReviewComment_LoanPurpose
            builder.HasOne(a => a.ReviewSite).WithMany(b => b.ReviewComments).HasForeignKey(c => c.ReviewSiteId).OnDelete(DeleteBehavior.SetNull); // FK_ReviewComment_ReviewSite
            builder.HasOne(a => a.State).WithMany(b => b.ReviewComments).HasForeignKey(c => c.StateId).OnDelete(DeleteBehavior.SetNull); // FK_ReviewComment_State
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
