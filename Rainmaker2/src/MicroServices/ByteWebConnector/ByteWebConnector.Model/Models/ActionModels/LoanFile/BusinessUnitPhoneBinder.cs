













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BusinessUnitPhoneBinder

    public partial class BusinessUnitPhoneBinder 
    {
        public int BusinessUnitId { get; set; } // BusinessUnitId (Primary key)
        public int CompanyPhoneInfoId { get; set; } // CompanyPhoneInfoId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [BusinessUnitPhoneBinder].([BusinessUnitId]) (FK_BusinessUnitPhoneBinder_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_BusinessUnitPhoneBinder_BusinessUnit

        /// <summary>
        /// Parent CompanyPhoneInfo pointed by [BusinessUnitPhoneBinder].([CompanyPhoneInfoId]) (FK_BusinessUnitPhoneBinder_CompanyPhoneInfo)
        /// </summary>
        public virtual CompanyPhoneInfo CompanyPhoneInfo { get; set; } // FK_BusinessUnitPhoneBinder_CompanyPhoneInfo

        public BusinessUnitPhoneBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
