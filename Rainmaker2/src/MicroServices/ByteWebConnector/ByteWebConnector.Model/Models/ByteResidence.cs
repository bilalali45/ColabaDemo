namespace ByteWebConnector.Model.Models
{
    public class ByteResidence
    {
        public int AppNo { get; set; }
        public int ResidenceId { get; set; }
        public int? BorrowerId { get; set; }
        public int DisplayOrder { get; set; }
        public bool Current { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string LivingStatus { get; set; }
        public int? NoYears { get; set; }
        public int? NoMonths { get; set; }
        public string LLName { get; set; }
        public string LLAttn { get; set; }
        public string LLFullAddress { get; set; }
        public string LLCityStateZip { get; set; }
        public string LLStreet { get; set; }
        public string LLCity { get; set; }
        public string LLState { get; set; }
        public string LLZip { get; set; }
        public string LLPhone { get; set; }
        public string Notes { get; set; }
        public string LLFax { get; set; }
        public string Country { get; set; }
        public int? MonthlyRent { get; set; }
        public int StreetContainsUnitNumberOv { get; set; }
        public long FileDataId { get; set; }


        public ByteResidence GetBorrowerResidence() 
        { var byteResidence= new ByteResidence();
            byteResidence.AppNo = this.AppNo;
            byteResidence.ResidenceId = this.ResidenceId;
            byteResidence.BorrowerId = this.BorrowerId;
            byteResidence.DisplayOrder = this.DisplayOrder;
            byteResidence.Current = this.Current;
            byteResidence.FullAddress = this.FullAddress;
            byteResidence.CityStateZip = this.CityStateZip;
            byteResidence.Street = this.Street;
            byteResidence.City = this.City;
            byteResidence.State = this.State;
            byteResidence.Zip = this.Zip;
            byteResidence.LivingStatus = this.LivingStatus;
            byteResidence.NoYears = this.NoYears;
            byteResidence.NoMonths = this.NoMonths;
            byteResidence.LLName = this.LLName;
            byteResidence.LLAttn = this.LLAttn;
            byteResidence.LLFullAddress = this.LLFullAddress;
            byteResidence.LLCityStateZip = this.LLCityStateZip;
            byteResidence.LLStreet = this.LLStreet;
            byteResidence.LLCity = this.LLCity;
            byteResidence.LLState = this.LLState;
            byteResidence.LLZip = this.LLZip;
            byteResidence.LLPhone = this.LLPhone;
            byteResidence.Notes = this.Notes;
            byteResidence.LLFax = this.LLFax;
            byteResidence.Country = this.Country;
            byteResidence.MonthlyRent = this.MonthlyRent;
            byteResidence.StreetContainsUnitNumberOv = this.StreetContainsUnitNumberOv;
            byteResidence.FileDataId = this.FileDataId;
            return byteResidence;
        }
    }
}
