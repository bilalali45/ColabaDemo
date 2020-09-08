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


namespace RainMaker.Entity.Models
{
    // notification_object

    public partial class NotificationObject : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? NotificationTypeId { get; set; } // NotificationTypeId
        public int? EntityId { get; set; } // EntityId
        public System.DateTime? CreatedOn { get; set; } // CreatedOn
        public bool? Active { get; set; } // Active

        public NotificationObject()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
