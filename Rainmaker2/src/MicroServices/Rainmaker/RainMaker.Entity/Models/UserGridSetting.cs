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
    // UserGridSetting

    public partial class UserGridSetting : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? UserId { get; set; } // UserId
        public string GridName { get; set; } // GridName (length: 300)
        public string Setting { get; set; } // Setting (length: 2000)

        // Foreign keys

        /// <summary>
        /// Parent UserProfile pointed by [UserGridSetting].([UserId]) (FK_UserGridSetting_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_UserGridSetting_UserProfile

        public UserGridSetting()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
