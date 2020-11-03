













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BusinessUnitPhone

    public partial class BusinessUnitPhone 
    {
        public int Id { get; set; } // Id (Primary key)
        public int BusinessUnitId { get; set; } // BusinessUnitId
        public int CompanyPhoneInfoId { get; set; } // CompanyPhoneInfoId
        public int? TypeId { get; set; } // TypeId

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [BusinessUnitPhone].([BusinessUnitId]) (FK_BusinessUnitPhone_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_BusinessUnitPhone_BusinessUnit

        /// <summary>
        /// Parent CompanyPhoneInfo pointed by [BusinessUnitPhone].([CompanyPhoneInfoId]) (FK_BusinessUnitPhone_CompanyPhoneInfo)
        /// </summary>
        public virtual CompanyPhoneInfo CompanyPhoneInfo { get; set; } // FK_BusinessUnitPhone_CompanyPhoneInfo

        public BusinessUnitPhone()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
