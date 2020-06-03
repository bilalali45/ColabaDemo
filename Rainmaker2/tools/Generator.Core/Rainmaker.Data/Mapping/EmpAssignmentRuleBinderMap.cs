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

    // EmpAssignmentRuleBinder
    
    public partial class EmpAssignmentRuleBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<EmpAssignmentRuleBinder>
    {
        public void Configure(EntityTypeBuilder<EmpAssignmentRuleBinder> builder)
        {
            builder.ToTable("EmpAssignmentRuleBinder", "dbo");
            builder.HasKey(x => new { x.RuleId, x.EmployeeId });

            builder.Property(x => x.RuleId).HasColumnName(@"RuleId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EmployeeId).HasColumnName(@"EmployeeId").HasColumnType("int").IsRequired().ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.Employee).WithMany(b => b.EmpAssignmentRuleBinders).HasForeignKey(c => c.EmployeeId).OnDelete(DeleteBehavior.SetNull); // FK_EmpAssignmentRuleBinder_Employee
            builder.HasOne(a => a.Rule).WithMany(b => b.EmpAssignmentRuleBinders).HasForeignKey(c => c.RuleId).OnDelete(DeleteBehavior.SetNull); // FK_EmpAssignmentRuleBinder_Rule
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>