using LosIntegration.API.Models.ClientModels;

namespace LosIntegration.API.Models
{
    public class LoanInfo
    {
        public long LoanId { get; set; }

        public string LoanPurpose { get; set; }
        public string AmortizationType { get; set; }
        public string AmortizationTypeDescOv { get; set; }
        public string LoanProgramName { get; set; }
        public decimal? PurPrice { get; set; }
        public decimal? BaseLoan { get; set; }
        public double LoanWith { get; set; }
        public string MortgageType { get; set; }
        public decimal? SubFiBaseLoan { get; set; }
        public long FileDataId { get; set; }
        public double? RefinanceCashOutAmount { get; internal set; }
        public string LoanGuid { get; set; }


        public LoanInfoEntity GetRainmakerLoanInfo()
        {
            var loanInfoEntity = new LoanInfoEntity();
            loanInfoEntity.FileDataId = this.FileDataId;
            loanInfoEntity.AmortizationTypeId = GetRainMakerAmortizationId(this.AmortizationType);
            loanInfoEntity.MortgageTypeId = GetRainMakerMortgageId(this.MortgageType);
            loanInfoEntity.CashOutAmount = this.RefinanceCashOutAmount;
            loanInfoEntity.LoanAmount = this.BaseLoan;
            loanInfoEntity.PropertyValue = this.PurPrice;
            loanInfoEntity.LoanPurpose = GetRainMakerLoanPurposeId(this.LoanPurpose);
            loanInfoEntity.MortgageToBeSubordinate = this.SubFiBaseLoan;
            return loanInfoEntity;
        }


        private int? GetRainMakerLoanPurposeId(string loanPurpose)
        {
            switch (loanPurpose)
            {
                case "Purchase":
                {
                    return (int)Enums.LoanPurpose.Purchase;
                }                     
                case "Refinance":           
                {                     
                    return (int)Enums.LoanPurpose.Refinance;
                }                     
                case "Construction":  
                {                     
                    return (int)Enums.LoanPurpose.CashOut;
                }                     
                case "ConstructionPerm":         
                {                     
                    return (int)Enums.LoanPurpose.CashOut;
                }                     
                case "Second":         
                {                     
                    return (int)Enums.LoanPurpose.Refinance;
                }                     
                case "Third":   
                {                     
                    return (int)Enums.LoanPurpose.Refinance;
                }                     
                case "PurchaseMoneySecond":   
                {                     
                    return (int)Enums.LoanPurpose.Purchase;
                }
                case "Other":
                {
                    return (int)Enums.LoanPurpose.Refinance;
                }
                case "PurchaseMoneyThird":
                {
                    return (int)Enums.LoanPurpose.Purchase;
                }
                case "RefiSecond":
                {
                    return (int)Enums.LoanPurpose.Refinance;
                }
                case "RefiThird":
                {
                    return (int)Enums.LoanPurpose.Refinance;
                }
            }

            return -1;
        }


        private int? GetRainMakerMortgageId(string mortgageType)
        {
            switch (mortgageType)
            {
                case "VA":
                {
                    return (int)Enums.MortgageType.VA;
                }
                case "FHA":
                {
                    return (int)Enums.MortgageType.FHA;
                }
                case "Conventional":
                {
                    return (int)Enums.MortgageType.Conventional;
                }
                case "HELOC":
                {
                    return (int)Enums.MortgageType.Other;
                }
                case "Other":
                {
                    return (int)Enums.MortgageType.Other;
                }
                case "StateAgency":
                {
                    return (int)Enums.MortgageType.Other;
                }
                case "LocalAgency":
                {
                    return (int)Enums.MortgageType.Other;
                }
            }

            return -1;
        }


        private int? GetRainMakerAmortizationId(string amortizationType)
        {
            switch (amortizationType)
            {
                case "Fixed":
                {
                    return (int)Enums.AmortizationType.Fixed;
                }
                case "ARM":
                {
                    return (int)Enums.AmortizationType.ARM;
                }
                case "GPM":
                {
                    return (int)Enums.AmortizationType.GPM;
                }
                case "Other":
                {
                    return (int)Enums.AmortizationType.Other;
                }
            }

            return -1;
        }
    }
}
