













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BranchPhoneBinder

    public partial class BranchPhoneBinder 
    {
        public int BranchId { get; set; } // BranchId (Primary key)
        public int CompanyPhoneInfoId { get; set; } // CompanyPhoneInfoId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [BranchPhoneBinder].([BranchId]) (FK_BranchPhoneBinder_Branch)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_BranchPhoneBinder_Branch

        /// <summary>
        /// Parent CompanyPhoneInfo pointed by [BranchPhoneBinder].([CompanyPhoneInfoId]) (FK_BranchPhoneBinder_CompanyPhoneInfo)
        /// </summary>
        public virtual CompanyPhoneInfo CompanyPhoneInfo { get; set; } // FK_BranchPhoneBinder_CompanyPhoneInfo

        public BranchPhoneBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
