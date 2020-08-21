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


namespace Notification.Data
{
    using Microsoft.EntityFrameworkCore;
    using Notification.Data.Mapping;
    using Notification.Entity.Models;


    
    public partial class NotificationContext : DbContext
    {
        //public virtual DbSet<DeliveryModeEnum> DeliveryModeEnums { get; set; } // DeliveryModeEnum
        //public virtual DbSet<NotificationActor> NotificationActors { get; set; } // NotificationActor
        //public virtual DbSet<NotificationMedium> NotificationMediums { get; set; } // NotificationMedium
        //public virtual DbSet<NotificationObject> NotificationObjects { get; set; } // NotificationObject
        //public virtual DbSet<NotificationObjectStatusLog> NotificationObjectStatusLogs { get; set; } // NotificationObjectStatusLog
        //public virtual DbSet<NotificationRecepient> NotificationRecepients { get; set; } // NotificationRecepient
        //public virtual DbSet<NotificationRecepientMedium> NotificationRecepientMediums { get; set; } // NotificationRecepientMedium
        //public virtual DbSet<NotificationRecepientStatusLog> NotificationRecepientStatusLogs { get; set; } // NotificationRecepientStatusLog
        //public virtual DbSet<NotificationRecipientMediumStatusList> NotificationRecipientMediumStatusLists { get; set; } // NotificationRecipientMediumStatusList
        //public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; } // NotificationTemplate
        //public virtual DbSet<NotificationType> NotificationTypes { get; set; } // NotificationType
        //public virtual DbSet<Setting> Settings { get; set; } // Settings
        //public virtual DbSet<StatusListEnum> StatusListEnums { get; set; } // StatusListEnum
        //public virtual DbSet<TenantSetting> TenantSettings { get; set; } // TenantSettings
        //public virtual DbSet<UserNotificationMedium> UserNotificationMediums { get; set; } // UserNotificationMedium

		public NotificationContext(DbContextOptions<NotificationContext> options)
            : base(options)
        {
		            InitializePartial();
        }
		
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DeliveryModeEnumMap());
            modelBuilder.ApplyConfiguration(new NotificationActorMap());
            modelBuilder.ApplyConfiguration(new NotificationMediumMap());
            modelBuilder.ApplyConfiguration(new NotificationObjectMap());
            modelBuilder.ApplyConfiguration(new NotificationObjectStatusLogMap());
            modelBuilder.ApplyConfiguration(new NotificationRecepientMap());
            modelBuilder.ApplyConfiguration(new NotificationRecepientMediumMap());
            modelBuilder.ApplyConfiguration(new NotificationRecepientStatusLogMap());
            modelBuilder.ApplyConfiguration(new NotificationRecipientMediumStatusListMap());
            modelBuilder.ApplyConfiguration(new NotificationTemplateMap());
            modelBuilder.ApplyConfiguration(new NotificationTypeMap());
            modelBuilder.ApplyConfiguration(new SettingMap());
            modelBuilder.ApplyConfiguration(new StatusListEnumMap());
            modelBuilder.ApplyConfiguration(new TenantSettingMap());
            modelBuilder.ApplyConfiguration(new UserNotificationMediumMap());

            OnModelCreatingPartial(modelBuilder);
        }


        partial void InitializePartial();
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
// </auto-generated>
