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

    // AddressInfo
    
    public partial class AddressInfoMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<AddressInfo>
    {
        public void Configure(EntityTypeBuilder<AddressInfo> builder)
        {
            builder.ToTable("AddressInfo", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.CountryId).HasColumnName(@"CountryId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CountryName).HasColumnName(@"CountryName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.StateId).HasColumnName(@"StateId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.StateName).HasColumnName(@"StateName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.CountyId).HasColumnName(@"CountyId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CountyName).HasColumnName(@"CountyName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.CityName).HasColumnName(@"CityName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.CityId).HasColumnName(@"CityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.StreetAddress).HasColumnName(@"StreetAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ZipCode).HasColumnName(@"ZipCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.UnitNo).HasColumnName(@"UnitNo").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.City).WithMany(b => b.AddressInfoes).HasForeignKey(c => c.CityId).OnDelete(DeleteBehavior.SetNull); // FK_AddressInfo_City
            builder.HasOne(a => a.Country).WithMany(b => b.AddressInfoes).HasForeignKey(c => c.CountryId).OnDelete(DeleteBehavior.SetNull); // FK_AddressInfo_Country
            builder.HasOne(a => a.County).WithMany(b => b.AddressInfoes).HasForeignKey(c => c.CountyId).OnDelete(DeleteBehavior.SetNull); // FK_AddressInfo_County
            builder.HasOne(a => a.State).WithMany(b => b.AddressInfoes).HasForeignKey(c => c.StateId).OnDelete(DeleteBehavior.SetNull); // FK_AddressInfo_State
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
