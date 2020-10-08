namespace ByteWebConnector.API.Models
{
    public class ByteLiability
    {
        public int AppNo { get; set; }
        public int DebtId { get; set; }
        public int? BorrowerId { get; set; }
        public int DisplayOrder { get; set; }
        public object REOID { get; set; }
        public string DebtType { get; set; }
        public string Name { get; set; }
        public string Attn { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string AccountNo { get; set; }
        public double? MoPayment { get; set; }
        public double? PaymentsLeft { get; set; }
        public string PaymentsLeftTextOV { get; set; }
        public double? UnpaidBal { get; set; }
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
        public long FileDataId { get; set; }


        public ByteLiability GetBorrowerLiability()
        {
            var byteLiability = new ByteLiability();
            byteLiability.AppNo = this.AppNo;
            byteLiability.DebtId = this.DebtId;
            byteLiability.BorrowerId = this.BorrowerId;
            byteLiability.DisplayOrder = this.DisplayOrder;
            byteLiability.REOID = this.REOID;
            byteLiability.DebtType = this.DebtType;
            byteLiability.Name = this.Name;
            byteLiability.Attn = this.Attn;
            byteLiability.FullAddress = this.FullAddress;
            byteLiability.CityStateZip = this.CityStateZip;
            byteLiability.Street = this.Street;
            byteLiability.City = this.City;
            byteLiability.State = this.State;
            byteLiability.Zip = this.Zip;
            byteLiability.AccountNo = this.AccountNo;
            byteLiability.MoPayment = this.MoPayment;
            byteLiability.PaymentsLeft = this.PaymentsLeft;
            byteLiability.PaymentsLeftTextOV = this.PaymentsLeftTextOV;
            byteLiability.UnpaidBal = this.UnpaidBal;
            byteLiability.NotCounted = this.NotCounted;
            byteLiability.ToBePaidOff = this.ToBePaidOff;
            byteLiability.LienPosition = this.LienPosition;
            byteLiability.Resubordinated = this.Resubordinated;
            byteLiability.Omitted = this.Omitted;
            byteLiability.Notes = this.Notes;
            byteLiability.IsLienOnSubProp = this.IsLienOnSubProp;
            byteLiability.TotalPaymentsOV = this.TotalPaymentsOV;
            byteLiability.Fax = this.Fax;
            byteLiability.SSOrDILAccepted = this.SSOrDILAccepted;
            byteLiability.ListedOnCreditReport = this.ListedOnCreditReport;
            byteLiability.QMATRNotes = this.QMATRNotes;
            byteLiability.OtherDesc = this.OtherDesc;
            byteLiability.MortgageType = this.MortgageType;
            byteLiability.HELOCCreditLimit = this.HELOCCreditLimit;
            byteLiability.AccountHeldByType = this.AccountHeldByType;
            byteLiability.FileDataId = this.FileDataId;
            return byteLiability;
        }
    }

}
