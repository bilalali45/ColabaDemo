using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BorrowerConsent
    {
        public int Id { get; set; }
        public int? ConsentTypeId { get; set; }
        public int? BorrowerId { get; set; }
        public int? LoanApplicationId { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool? IsAccepted { get; set; }
        public string IpAddress { get; set; }

        public ICollection<BorrowerConsentLog> BorrowerConsentLogs { get; set; }

        public Borrower Borrower { get; set; }

        public ConsentType ConsentType { get; set; }

        public LoanApplication LoanApplication { get; set; }
    }
}