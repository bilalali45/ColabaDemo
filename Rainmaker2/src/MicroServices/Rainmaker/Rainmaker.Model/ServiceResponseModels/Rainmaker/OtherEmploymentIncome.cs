using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OtherEmploymentIncome
    {
        public int Id { get; set; }
        public int? EmploymentInfoId { get; set; }
        public int? OtherIncomeTypeId { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public string Description { get; set; }

        public ICollection<OtherEmploymentIncomeHistory> OtherEmploymentIncomeHistories { get; set; }

        //public EmploymentInfo EmploymentInfo { get; set; }

        public OtherEmploymentIncomeType OtherEmploymentIncomeType { get; set; }

    }
}