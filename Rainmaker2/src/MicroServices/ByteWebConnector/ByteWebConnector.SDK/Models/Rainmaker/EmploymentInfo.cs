using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class EmploymentInfo
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public int? EmployerAddressId { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public int? BorrowerId { get; set; }
        public bool? IsSelfEmployed { get; set; }
        public string Position { get; set; }
        public int? JobTypeId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal? MonthlyBaseIncome { get; set; }
        public int EntityTypeId { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployerComment { get; set; }
        public int? YearsInProfession { get; set; }
        public int? ContactPersonId { get; set; }
        public bool? IsCurrentJob { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsHourlyPayment { get; set; }
        public bool? IsPlanToChangePriorToClosing { get; set; }
        public decimal? OwnershipPercentage { get; set; }
        public bool? IsEmployedByPartyInTransaction { get; set; }

        public ICollection<OtherEmploymentIncome> OtherEmploymentIncomes { get; set; }

        public AddressInfo AddressInfo { get; set; }

        //public Borrower Borrower { get; set; }

        public JobType JobType { get; set; }

        public LoanContact LoanContact { get; set; }
    }
}