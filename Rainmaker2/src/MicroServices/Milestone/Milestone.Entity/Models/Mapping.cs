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


namespace Milestone.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // Mapping
    
    public partial class Mapping : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public string RmEntityName { get; set; } // RMEntityName (length: 50)
        public string RmEnittyId { get; set; } // RMEnittyId (length: 50)
        public string ExtOriginatorEntityName { get; set; } // ExtOriginatorEntityName (length: 50)
        public string ExtOriginatorEntityId { get; set; } // ExtOriginatorEntityId (length: 50)
        public short ExtOriginatorId { get; set; } // ExtOriginatorId

        // Foreign keys

        /// <summary>
        /// Parent ExternalOriginator pointed by [Mapping].([ExtOriginatorId]) (FK_Mapping_ExternalOriginator)
        /// </summary>
        public virtual ExternalOriginator ExternalOriginator { get; set; } // FK_Mapping_ExternalOriginator

        public Mapping()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
