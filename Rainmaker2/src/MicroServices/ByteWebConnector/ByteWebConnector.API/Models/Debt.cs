namespace ByteWebConnector.API.Models
{
    public class Debt
    {
        public int AppNo { get; set; }
        public int DebtID { get; set; }
        public int? BorrowerID { get; set; }
        public int DisplayOrder { get; set; }
        public object REOID { get; set; }
        public int DebtType { get; set; }
        public string Name { get; set; }
        public string Attn { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string AccountNo { get; set; }
        public string MoPayment { get; set; }
        public string PaymentsLeft { get; set; }
        public string PaymentsLeftTextOV { get; set; }
        public string UnpaidBal { get; set; }
        public bool NotCounted { get; set; }
        public bool? ToBePaidOff { get; set; }
        public object LienPosition { get; set; }
        public bool Resubordinated { get; set; }
        public bool Omitted { get; set; }
        public string Notes { get; set; }
        public bool IsLienOnSubProp { get; set; }
        public object TotalPaymentsOV { get; set; }
        public string Fax { get; set; }
        public int SSOrDILAccepted { get; set; }
        public bool ListedOnCreditReport { get; set; }
        public string QMATRNotes { get; set; }
        public string OtherDesc { get; set; }
        public int MortgageType { get; set; }
        public object HELOCCreditLimit { get; set; }
        public int AccountHeldByType { get; set; }
        public long FileDataID { get; set; }

    }

}
