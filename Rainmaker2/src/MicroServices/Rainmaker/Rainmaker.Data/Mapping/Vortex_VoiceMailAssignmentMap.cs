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

    // VoiceMailAssignment
    
    public partial class Vortex_VoiceMailAssignmentMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Vortex_VoiceMailAssignment>
    {
        public void Configure(EntityTypeBuilder<Vortex_VoiceMailAssignment> builder)
        {
            builder.ToTable("VoiceMailAssignment", "Vortex");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.RecordingSid).HasColumnName(@"RecordingSid").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.EmployeeId).HasColumnName(@"EmployeeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsRead).HasColumnName(@"IsRead").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsRecordingFileDeleted).HasColumnName(@"IsRecordingFileDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.RecordingFileToBeDeletedOn).HasColumnName(@"RecordingFileToBeDeletedOn").HasColumnType("datetime2").IsRequired(false);
            builder.Property(x => x.IsArchived).HasColumnName(@"IsArchived").HasColumnType("bit").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Employee).WithMany(b => b.Vortex_VoiceMailAssignments).HasForeignKey(c => c.EmployeeId).OnDelete(DeleteBehavior.SetNull); // FK_VoiceMailAssignment_Employee
            builder.HasOne(a => a.Vortex_Recording).WithMany(b => b.Vortex_VoiceMailAssignments).HasForeignKey(c => c.RecordingSid).OnDelete(DeleteBehavior.SetNull); // FK_VoiceMailAssignment_Recording
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
