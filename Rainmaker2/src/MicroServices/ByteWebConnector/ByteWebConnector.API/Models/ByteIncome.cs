namespace ByteWebConnector.API.Models
{
    public class ByteIncome
    {
        public int AppNo { get; set; }
        public int IncomeID { get; set; }
        public int? BorrowerID { get; set; }
        public int DisplayOrder { get; set; }
        public int IncomeType { get; set; }
        public string Amount { get; set; }
        public string DescriptionOV { get; set; }
        public int IncomeFrequencyType { get; set; }
        public object IncomeRate { get; set; }
        public object HoursPerWeek { get; set; }
        public string Notes { get; set; }
        public string QMATRNotes { get; set; }
        public string VariablePeriod1Desc { get; set; }
        public object VariablePeriod1Income { get; set; }
        public object VariablePeriod1Months { get; set; }
        public string VariablePeriod2Desc { get; set; }
        public object VariablePeriod2Income { get; set; }
        public object VariablePeriod2Months { get; set; }
        public string VariablePeriod3Desc { get; set; }
        public object VariablePeriod3Income { get; set; }
        public object VariablePeriod3Months { get; set; }
        public object _CalcDescription { get; set; }
        public object _RateDescription { get; set; }
        public bool SelfEmploymentIncome { get; set; }
        public int EmployerID { get; set; }
        public long FileDataID { get; set; }


        public object GetRainmakerBorrowerIncome()
        {
            throw new System.NotImplementedException();
        }
    }

}
