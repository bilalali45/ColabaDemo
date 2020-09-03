using System.Runtime.Serialization;
using LosIntegration.API.Models.ClientModels;

namespace LosIntegration.API.Models
{
    [DataContract]
    public class Application
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
}
