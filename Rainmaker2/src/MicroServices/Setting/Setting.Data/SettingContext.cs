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


namespace Setting.Data
{
    using Microsoft.EntityFrameworkCore;
    using Setting.Data.Mapping;
    using Setting.Entity.Models;


    
    public partial class SettingContext : DbContext
    {
        //public virtual DbSet<EmailReminderLog> EmailReminderLogs { get; set; } // EmailReminderLogs
        //public virtual DbSet<JobType> JobTypes { get; set; } // JobType

		public SettingContext(DbContextOptions<SettingContext> options)
            : base(options)
        {
		            InitializePartial();
        }
		
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmailReminderLogMap());
            modelBuilder.ApplyConfiguration(new JobTypeMap());

            OnModelCreatingPartial(modelBuilder);
        }


        partial void InitializePartial();
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
// </auto-generated>
