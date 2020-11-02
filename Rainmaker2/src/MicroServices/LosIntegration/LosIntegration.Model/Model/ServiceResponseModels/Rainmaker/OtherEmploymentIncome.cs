













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // OtherEmploymentIncome

    public partial class OtherEmploymentIncome 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? EmploymentInfoId { get; set; } // EmploymentInfoId
        public int? OtherIncomeTypeId { get; set; } // OtherIncomeTypeId
        public decimal? MonthlyIncome { get; set; } // MonthlyIncome
        public string Description { get; set; } // Description (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child OtherEmploymentIncomeHistories where [OtherEmploymentIncomeHistory].[OtherEmploymentIncomeId] point to this entity (FK_OtherEmploymentIncomeHistory_OtherEmploymentIncome)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OtherEmploymentIncomeHistory> OtherEmploymentIncomeHistories { get; set; } // OtherEmploymentIncomeHistory.FK_OtherEmploymentIncomeHistory_OtherEmploymentIncome

        // Foreign keys

        /// <summary>
        /// Parent EmploymentInfo pointed by [OtherEmploymentIncome].([EmploymentInfoId]) (FK_OtherEmploymentIncome_EmploymentInfo)
        /// </summary>
        public virtual EmploymentInfo EmploymentInfo { get; set; } // FK_OtherEmploymentIncome_EmploymentInfo

        /// <summary>
        /// Parent OtherEmploymentIncomeType pointed by [OtherEmploymentIncome].([OtherIncomeTypeId]) (FK_OtherEmploymentIncome_OtherEmploymentIncomeType)
        /// </summary>
        public virtual OtherEmploymentIncomeType OtherEmploymentIncomeType { get; set; } // FK_OtherEmploymentIncome_OtherEmploymentIncomeType

        public OtherEmploymentIncome()
        {
            OtherEmploymentIncomeHistories = new System.Collections.Generic.HashSet<OtherEmploymentIncomeHistory>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
