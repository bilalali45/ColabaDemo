using System.Runtime.Serialization;
using ByteWebConnector.API.Models.ClientModels;

namespace ByteWebConnector.API.Models
{
    [DataContract]
    public class ByteApplication
    {
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


        public Application GetApplication()
        {
            var applicationEntity = new ClientModels.Application();
            applicationEntity.ApplicationId = this.ApplicationId;
            applicationEntity.ApplicationMethod = this.ApplicationMethod;
            applicationEntity.LifeInsuranceEstimatedMonthlyAmount = this.LifeInsCashValue;
            applicationEntity.BorrowerId = this.BorrowerId;
            applicationEntity.CoBorrowerId = this.CoBorrowerId;
            applicationEntity.FileDataId = this.FileDataId;
            return applicationEntity;
        }
    }
}
