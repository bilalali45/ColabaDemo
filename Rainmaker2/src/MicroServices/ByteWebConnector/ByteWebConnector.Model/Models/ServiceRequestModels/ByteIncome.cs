namespace ByteWebConnector.Model.Models.ServiceRequestModels
{
    public class ByteIncome
    {
        public int AppNo { get; set; }
        public int IncomeId { get; set; }
        public int? BorrowerId { get; set; }
        public int DisplayOrder { get; set; }
        public string IncomeType { get; set; }
        public double? Amount { get; set; }
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
        public int EmployerId { get; set; }
        public long FileDataId { get; set; }


        public ByteIncome GetBorrowerIncome()
        {
            var byteIncome = new ByteIncome();
            byteIncome.AppNo = this.AppNo;
            byteIncome.IncomeId = this.IncomeId;
            byteIncome.BorrowerId = this.BorrowerId;
            byteIncome.DisplayOrder = this.DisplayOrder;
            byteIncome.IncomeType = this.IncomeType;
            byteIncome.Amount = this.Amount;
            byteIncome.DescriptionOV = this.DescriptionOV;
            byteIncome.IncomeFrequencyType = this.IncomeFrequencyType;
            byteIncome.IncomeRate = this.IncomeRate;
            byteIncome.HoursPerWeek = this.HoursPerWeek;
            byteIncome.Notes = this.Notes;
            byteIncome.QMATRNotes = this.QMATRNotes;
            byteIncome.VariablePeriod1Desc = this.VariablePeriod1Desc;
            byteIncome.VariablePeriod1Income = this.VariablePeriod1Income;
            byteIncome.VariablePeriod1Months = this.VariablePeriod1Months;
            byteIncome.VariablePeriod2Desc = this.VariablePeriod2Desc;
            byteIncome.VariablePeriod2Income = this.VariablePeriod2Income;
            byteIncome.VariablePeriod2Months = this.VariablePeriod2Months;
            byteIncome.VariablePeriod3Desc = this.VariablePeriod3Desc;
            byteIncome.VariablePeriod3Income = this.VariablePeriod3Income;
            byteIncome.VariablePeriod3Months = this.VariablePeriod3Months;
            byteIncome._CalcDescription = this._CalcDescription;
            byteIncome._RateDescription = this._RateDescription;
            byteIncome.SelfEmploymentIncome = this.SelfEmploymentIncome;
            byteIncome.EmployerId = this.EmployerId;
            byteIncome.FileDataId = this.FileDataId;
            return byteIncome;
        }
    }

}
