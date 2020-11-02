using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BorrowerAccount
    {
        public int Id { get; set; }
        public int? AccountTypeId { get; set; }
        public string Name { get; set; }
        public string AccountTitle { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public decimal? UseForDownpayment { get; set; }
        public DateTime? BalanceDate { get; set; }
        public bool? IsJoinType { get; set; }

        public AccountType AccountType { get; set; }

    }
}