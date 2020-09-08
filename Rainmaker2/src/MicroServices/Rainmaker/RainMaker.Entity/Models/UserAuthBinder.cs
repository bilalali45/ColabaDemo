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
    // UserAuthBinder

    public partial class UserAuthBinder : URF.Core.EF.Trackable.Entity
    {
        public int UserProfileId { get; set; } // UserProfileId (Primary key)
        public int AuthProviderId { get; set; } // AuthProviderId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent AuthProvider pointed by [UserAuthBinder].([AuthProviderId]) (FK_UserAuthBinder_AuthProvider)
        /// </summary>
        public virtual AuthProvider AuthProvider { get; set; } // FK_UserAuthBinder_AuthProvider

        /// <summary>
        /// Parent UserProfile pointed by [UserAuthBinder].([UserProfileId]) (FK_UserAuthBinder_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_UserAuthBinder_UserProfile

        public UserAuthBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
