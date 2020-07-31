namespace ByteWebConnector.API.Models
{
    public class ByteEmployer
    {
        public int AppNo { get; set; }
        public int EmployerId { get; set; }
        public int? BorrowerId { get; set; }
        public int DisplayOrder { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Attn { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool? SelfEmp { get; set; }
        public string Position { get; set; }
        public string TypeBus { get; set; }
        public string Phone { get; set; }
        public string DateFrom { get; set; }
        public object DateTo { get; set; }
        public int? YearsOnJob { get; set; }
        public int? YearsInProf { get; set; }
        public double? MoIncome { get; set; }
        public string Notes { get; set; }
        public string Fax { get; set; }
        public string VOEVendorID { get; set; }
        public string VOESalaryID { get; set; }
        public object TimeInLineOfWorkYears { get; set; }
        public object TimeInLineOfWorkMonths { get; set; }
        public int IsSpecialRelationship { get; set; }
        public string OwnershipInterest { get; set; }
        public bool IsForeignEmployment { get; set; }
        public bool IsSeasonalEmployment { get; set; }
        public int StreetContainsUnitNumberOv { get; set; }
        public string Country { get; set; }
        public string VoeEmployeeId { get; set; }
        public long FileDataId { get; set; }


        public object GetRainmakerBorrowerEmployer()
        {
            throw new System.NotImplementedException();
        }
    }

}
