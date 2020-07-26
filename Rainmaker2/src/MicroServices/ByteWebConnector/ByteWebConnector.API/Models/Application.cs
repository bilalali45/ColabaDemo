using System.Runtime.Serialization;

namespace ByteWebConnector.API.Models
{
    [DataContract]
    public class Application
    {
        //[DataMember(Name = "Borrower")]
        //public ByteBorrower Borrower { get; set; }
        //[DataMember(Name = "CoBorrower")]
        //public ByteCoBorrower CoBorrower { get; set; }
        [DataMember]
        public long ApplicationId { get; set; }
        [DataMember]

        public int DisplayOrder { get; set; }
        [DataMember]
        public int? BorrowerId { get; set; }
        [DataMember]
        public int? CoBorrowerId { get; set; }
        [DataMember]
        public int ApplicationMethod { get; set; }
        //public bool OtherIncome { get; set; }
        //public bool IncomeSpouse { get; set; }
        //public string CreditRefNo { get; set; }
        //public string AutoDesc1 { get; set; }
        //public object AutoValue1 { get; set; }
        //public string AutoDesc2 { get; set; }
        //public object AutoValue2 { get; set; }
        //public string AutoDesc3 { get; set; }
        //public object AutoValue3 { get; set; }
        [DataMember]
        public int OtherExpenseType { get; set; }
        //public string OtherExpenseOwedTo { get; set; }
        //public object OtherExpenseAmount { get; set; }
        //public string JobExpenseDesc1 { get; set; }
        //public object JobExpenseAmount1 { get; set; }
        //public string JobExpenseDesc2 { get; set; }
        //public object JobExpenseAmount2 { get; set; }
        //public string OtherAssetDesc1 { get; set; }
        //public object OtherAssetValue1 { get; set; }
        //public string OtherAssetDesc2 { get; set; }
        //public object OtherAssetValue2 { get; set; }
        //public string OtherAssetDesc3 { get; set; }
        //public object OtherAssetValue3 { get; set; }
        //public string OtherAssetDesc4 { get; set; }
        //public object OtherAssetValue4 { get; set; }
        //public object NetWorthOfBusiness { get; set; }
        [DataMember]
        public object RetirementFunds { get; set; }
        [DataMember]
        public object LifeInsFaceValue { get; set; }
        [DataMember]
        public object LifeInsCashValue { get; set; }
        [DataMember]
        public long FileDataId { get; set; }

    }
    //[DataContract(Name = "Borrower")]
    //public class ByteBorrower
    //{
    //    [DataMember]
    //    public long FileDataID { get; set; }
    //    [DataMember]
    //    public int? BorrowerID { get; set; }
    //    //[DataMember(Name = "Borrower")]
    //    //public Bor Borrower { get; set; }
    //    //[DataMember]
    //    //public List<Residence> Residences { get; set; }
    //    //[DataMember]
    //    //public List<Employer> Employers { get; set; }
    //    //[DataMember]
    //    //public List<Income> Incomes { get; set; }
    //    //[DataMember]
    //    //public List<Asset> Assets { get; set; }
    //    //[DataMember]
    //    //public List<REO> REOs { get; set; }
    //    //[DataMember]
    //    //public List<Debt> Debts { get; set; }
    //    //[DataMember]
    //    //public List<object> CreditAliases { get; set; }
    //    //[DataMember]
    //    //public List<object> Expenses { get; set; }
    //    //[DataMember]
    //    //public List<object> Gifts { get; set; }

    //}
    //[DataContract(Name = "CoBorrower")]
    //public class ByteCoBorrower
    //{
    //    [DataMember]
    //    public long FileDataID { get; set; }
    //    [DataMember]
    //    public int? BorrowerID { get; set; }

    //    //[DataMember(Name = "Borrower")]
    //    //public Bor Borrower { get; set; }
    //    //[DataMember]
    //    //public List<Residence> Residences { get; set; }
    //    //[DataMember]
    //    //public List<Employer> Employers { get; set; }
    //    //[DataMember]
    //    //public List<Income> Incomes { get; set; }
    //    //[DataMember]
    //    //public List<Asset> Assets { get; set; }
    //    //[DataMember]
    //    //public List<REO> REOs { get; set; }
    //    //[DataMember]
    //    //public List<Debt> Debts { get; set; }
    //    //[DataMember]
    //    //public List<object> CreditAliases { get; set; }
    //    //[DataMember]
    //    //public List<object> Expenses { get; set; }
    //    //[DataMember]
    //    //public List<object> Gifts { get; set; }

    //}
}
