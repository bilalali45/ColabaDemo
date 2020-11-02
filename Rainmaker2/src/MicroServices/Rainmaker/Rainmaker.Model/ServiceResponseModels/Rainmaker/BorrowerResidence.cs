using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BorrowerResidence
    {
        public int Id { get; set; }
        public int? LoanAddressId { get; set; }
        public int? BorrowerId { get; set; }
        public int? OwnershipTypeId { get; set; }
        public int? TypeId { get; set; }
        public decimal? MonthlyRent { get; set; }
        public string AccountInNameOf { get; set; }
        public int? LandLordContactId { get; set; }
        public int? LandLordAddressId { get; set; }
        public string LandLordComments { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsSameAsPropertyAddress { get; set; }

        public AddressInfo LandLordAddress { get; set; }

        public AddressInfo LoanAddress { get; set; }

        public Borrower Borrower { get; set; }

        public LoanContact LoanContact { get; set; }

        public OwnershipType OwnershipType { get; set; }
    }
}