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

    // LoanContactRaceBinder
    
    public partial class LoanContactRaceBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LoanContactRaceBinder>
    {
        public void Configure(EntityTypeBuilder<LoanContactRaceBinder> builder)
        {
            builder.ToTable("LoanContactRaceBinder", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LoanContactId).HasColumnName(@"LoanContactId").HasColumnType("int").IsRequired();
            builder.Property(x => x.RaceId).HasColumnName(@"RaceId").HasColumnType("int").IsRequired();
            builder.Property(x => x.RaceDetailId).HasColumnName(@"RaceDetailId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OtherRace).HasColumnName(@"OtherRace").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);

            // Foreign keys
            builder.HasOne(a => a.LoanContact).WithMany(b => b.LoanContactRaceBinders).HasForeignKey(c => c.LoanContactId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContactRaceBinder_LoanContact
            builder.HasOne(a => a.Race).WithMany(b => b.LoanContactRaceBinders).HasForeignKey(c => c.RaceId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContactRaceBinder_Race
            builder.HasOne(a => a.RaceDetail).WithMany(b => b.LoanContactRaceBinders).HasForeignKey(c => c.RaceDetailId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContactRaceBinder_RaceDetail
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
