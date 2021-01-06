using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class MortgageEducation
    {
        public int Id { get; set; }
        public int LoanApplicationId { get; set; }
        public int? MortgageEducationTypeId { get; set; }
        public bool Last12Month { get; set; }
        public DateTime CompletionDateUtc { get; set; }
        public int EducationFormatId { get; set; }
        public bool? IsHudApproved { get; set; }
        public int Agency { get; set; }
        public bool AgencyId { get; set; }

        public EducationFormat EducationFormat { get; set; }

        public LoanApplication LoanApplication { get; set; }

        public MortgageEducationType MortgageEducationType { get; set; }
    }
}