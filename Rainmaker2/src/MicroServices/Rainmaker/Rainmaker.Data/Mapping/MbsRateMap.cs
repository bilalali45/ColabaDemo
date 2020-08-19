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

    // MbsRate
    
    public partial class MbsRateMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<MbsRate>
    {
        public void Configure(EntityTypeBuilder<MbsRate> builder)
        {
            builder.ToTable("MbsRate", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UpdateOnUtc).HasColumnName(@"UpdateOnUtc").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.MbsSecurityId).HasColumnName(@"MbsSecurityId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CouponRate).HasColumnName(@"CouponRate").HasColumnType("decimal(18,3)").IsRequired(false);
            builder.Property(x => x.SettlementMonth).HasColumnName(@"SettlementMonth").HasColumnType("date").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.BidBasis).HasColumnName(@"BidBasis").HasColumnType("decimal(18,8)").IsRequired(false);
            builder.Property(x => x.Bid32Nds).HasColumnName(@"Bid32nds").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(20);
            builder.Property(x => x.TMaturity).HasColumnName(@"TMaturity").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(20);
            builder.Property(x => x.TPrice).HasColumnName(@"TPrice").HasColumnType("decimal(18,8)").IsRequired(false);
            builder.Property(x => x.TPriceChange).HasColumnName(@"TPriceChange").HasColumnType("decimal(18,8)").IsRequired(false);
            builder.Property(x => x.TYield).HasColumnName(@"TYield").HasColumnType("decimal(18,8)").IsRequired(false);
            builder.Property(x => x.TYieldChange).HasColumnName(@"TYieldChange").HasColumnType("decimal(18,8)").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.MbsSecurity).WithMany(b => b.MbsRates).HasForeignKey(c => c.MbsSecurityId).OnDelete(DeleteBehavior.SetNull); // FK_MbsRate_MbsSecurity
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
