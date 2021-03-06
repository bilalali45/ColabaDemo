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

    // TeamMember
    
    public partial class TeamMemberMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.ToTable("TeamMember", "dbo");
            builder.HasKey(x => new { x.TeamId, x.EmployeeId });

            builder.Property(x => x.TeamId).HasColumnName(@"TeamId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EmployeeId).HasColumnName(@"EmployeeId").HasColumnType("int").IsRequired().ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.Employee).WithMany(b => b.TeamMembers).HasForeignKey(c => c.EmployeeId).OnDelete(DeleteBehavior.SetNull); // FK_TeamMember_Employee
            builder.HasOne(a => a.Team).WithMany(b => b.TeamMembers).HasForeignKey(c => c.TeamId).OnDelete(DeleteBehavior.SetNull); // FK_TeamMember_Team
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
