namespace ByteWebConnector.API.Models
{
    public class ByteEmployer
    {
        public int AppNo { get; set; }
        public int EmployerID { get; set; }
        public int? BorrowerID { get; set; }
        public int DisplayOrder { get; set; }
        public int Status { get; set; }
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
        public object DateFrom { get; set; }
        public object DateTo { get; set; }
        public string YearsOnJob { get; set; }
        public object YearsInProf { get; set; }
        public string MoIncome { get; set; }
        public string Notes { get; set; }
        public string Fax { get; set; }
        public string VOEVendorID { get; set; }
        public string VOESalaryID { get; set; }
        public object TimeInLineOfWorkYears { get; set; }
        public object TimeInLineOfWorkMonths { get; set; }
        public int IsSpecialRelationship { get; set; }
        public int OwnershipInterest { get; set; }
        public bool IsForeignEmployment { get; set; }
        public bool IsSeasonalEmployment { get; set; }
        public int StreetContainsUnitNumberOV { get; set; }
        public string Country { get; set; }
        public string VOEEmployeeID { get; set; }
        public long FileDataID { get; set; }


        public object GetRainmakerBorrowerEmployer()
        {
            throw new System.NotImplementedException();
        }
    }

}
