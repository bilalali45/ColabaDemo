namespace ByteWebConnector.Model.Models
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
       
        public long FileDataId { get; set; }


        public ByteParties GetParties()
        {
            var partiesEntity = new ByteParties();
            partiesEntity.PartyId = this.PartyId;
            partiesEntity.CategoryID = this.CategoryID;
            partiesEntity.FirstName = this.FirstName;
            partiesEntity.MiddleName = this.MiddleName;
            partiesEntity.LastName = this.LastName;
            partiesEntity.Title = this.Title;
            partiesEntity.Company = this.Company;
            partiesEntity.Street = this.Street;
            partiesEntity.City = this.City;
            partiesEntity.State = this.State;
            partiesEntity.Zip = this.Zip;
            partiesEntity.EMail = this.EMail;
            partiesEntity.WorkPhone = this.WorkPhone;
            partiesEntity.HomePhone = this.HomePhone;
            partiesEntity.MobilePhone = this.MobilePhone;
            partiesEntity.OtherPhone = this.OtherPhone;
            partiesEntity.Pager = this.Pager;
            partiesEntity.Fax = this.Fax;
            partiesEntity.LicenseNo = this.LicenseNo;
            partiesEntity.CHUMSNo = this.CHUMSNo;
            partiesEntity.FHAOrigOrSponsorID = this.FHAOrigOrSponsorID;
            partiesEntity.BranchID = this.BranchID;
            partiesEntity.Notes = this.Notes;
            partiesEntity.ContactNmlsid = this.ContactNmlsid;
            partiesEntity.CompanyNMLSID = this.CompanyNMLSID;
            partiesEntity.FileDataId = this.FileDataId;
          
            return partiesEntity;
            
        }
    }
}
