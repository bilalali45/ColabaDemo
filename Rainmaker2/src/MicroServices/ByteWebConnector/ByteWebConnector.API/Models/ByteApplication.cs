using System.Runtime.Serialization;
using ByteWebConnector.API.Models.ClientModels;

namespace ByteWebConnector.API.Models
{
    [DataContract]
    public class ByteApplication
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

        [DataMember]
        public int OtherExpenseType { get; set; }
        [DataMember]
        public object RetirementFunds { get; set; }
        [DataMember]
        public object LifeInsFaceValue { get; set; }
        [DataMember]
        public decimal? LifeInsCashValue { get; set; }
        [DataMember]
        public long FileDataId { get; set; }


        public ApplicationEntity GetRainmakerApplication()
        {
            var applicationEntity = new ClientModels.ApplicationEntity();
            applicationEntity.ApplicationId = this.ApplicationId;
            applicationEntity.ApplicationMethod = this.ApplicationMethod;
            applicationEntity.LifeInsuranceEstimatedMonthlyAmount = this.LifeInsCashValue;
            applicationEntity.BorrowerId = this.BorrowerId;
            applicationEntity.CoBorrowerId = this.CoBorrowerId;
            applicationEntity.FileDataId = this.FileDataId;
            return applicationEntity;
        }
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
