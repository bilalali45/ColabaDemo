using LosIntegration.API.Models.ClientModels;

namespace LosIntegration.API.Models
{
    public class ByteParties
    {
        public long PartyId { get; set; }
        public long CategoryID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string EMail { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string OtherPhone { get; set; }
        public string Pager { get; set; }
        public string Fax { get; set; }
        public string LicenseNo { get; set; }
        public string CHUMSNo { get; set; }
        public string FHAOrigOrSponsorID { get; set; }
        public string BranchID { get; set; }
        public string Notes { get; set; }
        public string ContactNmlsid { get; set; }
        public string CompanyNMLSID { get; set; }
        //public string LockToUser { get; set; }
        //public string CompanyEIN { get; set; }
        //public string MobilePhoneSMSGateway { get; set; }
        //public string CompanyLicenseNo { get; set; }
        //public string SyncData { get; set; }
        //public DateTime EAndOPolicyExpirationDate { get; set; }
        //public string LicensingAgencyCode { get; set; }
        //public string EMail2 { get; set; }
        //public string EMail3 { get; set; }
        public long FileDataId { get; set; }


        public PartiesEntity GetRainmakerParties()
        {
            var partiesEntity = new PartiesEntity
                                {
                                    InterviewerName = this.FirstName,
                                    NmlsLoanOriginatorId = this.ContactNmlsid,
                                    InterviewerPhoneNumber = this.WorkPhone,
                                    InterviewerEmail = this.EMail,
                                    FileDataId = this.FileDataId
                                };
            return partiesEntity;
            
        }
    }
}
