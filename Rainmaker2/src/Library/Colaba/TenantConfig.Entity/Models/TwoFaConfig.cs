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


namespace TenantConfig.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // TwoFAConfig
    
    public partial class TwoFaConfig : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int TenantId { get; set; } // TenantId
        public short BorrowerTwoFaModeId { get; set; } // BorrowerTwoFAModeId
        public short McuTwoFaModeId { get; set; } // MCUTwoFAModeId
        public short? McuTwoFaMobileModeId { get; set; } // MCUTwoFAMobileModeId
        public bool IsActive { get; set; } // IsActive
        public int BranchId { get; set; } // BranchId
        public string TwilioVerifyServiceId { get; set; } // TwilioVerifyServiceId (length: 50)

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [TwoFAConfig].([BranchId]) (FK_TwoFAConfig_Branch_Id)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_TwoFAConfig_Branch_Id

        /// <summary>
        /// Parent Tenant pointed by [TwoFAConfig].([TenantId]) (FK_TwoFAConfig_Tenant)
        /// </summary>
        public virtual Tenant Tenant { get; set; } // FK_TwoFAConfig_Tenant

        /// <summary>
        /// Parent TwoFaMode pointed by [TwoFAConfig].([BorrowerTwoFaModeId]) (FK_TwoFAConfig_TwoFAModes_Id_Borrower)
        /// </summary>
        public virtual TwoFaMode BorrowerTwoFaMode { get; set; } // FK_TwoFAConfig_TwoFAModes_Id_Borrower

        /// <summary>
        /// Parent TwoFaMode pointed by [TwoFAConfig].([McuTwoFaMobileModeId]) (FK_TwoFAConfig_TwoFAModes)
        /// </summary>
        public virtual TwoFaMode McuTwoFaMobileMode { get; set; } // FK_TwoFAConfig_TwoFAModes

        /// <summary>
        /// Parent TwoFaMode pointed by [TwoFAConfig].([McuTwoFaModeId]) (FK_TwoFAConfig_TwoFAModes_Id_MCU)
        /// </summary>
        public virtual TwoFaMode McuTwoFaMode { get; set; } // FK_TwoFAConfig_TwoFAModes_Id_MCU

        public TwoFaConfig()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
