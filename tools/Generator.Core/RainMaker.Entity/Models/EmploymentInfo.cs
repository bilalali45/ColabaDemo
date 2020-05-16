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
    using System;
    using System.Collections.Generic;

    // EmploymentInfo
    
    public partial class EmploymentInfo : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LoanId { get; set; } // LoanId
        public int? EmployerAddressId { get; set; } // EmployerAddressId
        public string Name { get; set; } // Name (length: 150)
        public string JobTitle { get; set; } // JobTitle (length: 500)
        public int? BorrowerId { get; set; } // BorrowerId
        public bool? IsSelfEmployed { get; set; } // IsSelfEmployed
        public string Position { get; set; } // Position (length: 50)
        public int? JobTypeId { get; set; } // JobTypeId
        public string Phone { get; set; } // Phone (length: 15)
        public string Email { get; set; } // Email (length: 100)
        public decimal? MonthlyBaseIncome { get; set; } // MonthlyBaseIncome
        public int EntityTypeId { get; set; } // EntityTypeId
        public string EmployeeNumber { get; set; } // EmployeeNumber (length: 50)
        public string EmployerComment { get; set; } // EmployerComment (length: 2500)
        public int? YearsInProfession { get; set; } // YearsInProfession
        public int? ContactPersonId { get; set; } // ContactPersonId
        public bool? IsCurrentJob { get; set; } // IsCurrentJob
        public System.DateTime? StartDate { get; set; } // StartDate
        public System.DateTime? EndDate { get; set; } // EndDate
        public bool? IsHourlyPayment { get; set; } // IsHourlyPayment
        public bool? IsPlanToChangePriorToClosing { get; set; } // IsPlanToChangePriorToClosing
        public decimal? OwnershipPercentage { get; set; } // OwnershipPercentage
        public bool? IsEmployedByPartyInTransaction { get; set; } // IsEmployedByPartyInTransaction

        // Reverse navigation

        /// <summary>
        /// Child OtherEmploymentIncomes where [OtherEmploymentIncome].[EmploymentInfoId] point to this entity (FK_OtherEmploymentIncome_EmploymentInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OtherEmploymentIncome> OtherEmploymentIncomes { get; set; } // OtherEmploymentIncome.FK_OtherEmploymentIncome_EmploymentInfo

        // Foreign keys

        /// <summary>
        /// Parent AddressInfo pointed by [EmploymentInfo].([EmployerAddressId]) (FK_EmploymentInfo_AddressInfo)
        /// </summary>
        public virtual AddressInfo AddressInfo { get; set; } // FK_EmploymentInfo_AddressInfo

        /// <summary>
        /// Parent Borrower pointed by [EmploymentInfo].([BorrowerId]) (FK_EmploymentInfo_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_EmploymentInfo_Borrower

        /// <summary>
        /// Parent JobType pointed by [EmploymentInfo].([JobTypeId]) (FK_EmploymentInfo_JobType)
        /// </summary>
        public virtual JobType JobType { get; set; } // FK_EmploymentInfo_JobType

        /// <summary>
        /// Parent LoanContact pointed by [EmploymentInfo].([ContactPersonId]) (FK_EmploymentInfo_LoanContact)
        /// </summary>
        public virtual LoanContact LoanContact { get; set; } // FK_EmploymentInfo_LoanContact

        public EmploymentInfo()
        {
            EntityTypeId = 79;
            OtherEmploymentIncomes = new System.Collections.Generic.HashSet<OtherEmploymentIncome>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
