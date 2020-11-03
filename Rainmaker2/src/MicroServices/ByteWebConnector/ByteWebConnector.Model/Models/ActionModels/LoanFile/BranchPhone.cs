













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BranchPhone

    public partial class BranchPhone 
    {
        public int Id { get; set; } // Id (Primary key)
        public int BranchId { get; set; } // BranchId
        public int CompanyPhoneInfoId { get; set; } // CompanyPhoneInfoId
        public int? TypeId { get; set; } // TypeId

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [BranchPhone].([BranchId]) (FK_BranchPhone_Branch)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_BranchPhone_Branch

        /// <summary>
        /// Parent CompanyPhoneInfo pointed by [BranchPhone].([CompanyPhoneInfoId]) (FK_BranchPhone_CompanyPhoneInfo)
        /// </summary>
        public virtual CompanyPhoneInfo CompanyPhoneInfo { get; set; } // FK_BranchPhone_CompanyPhoneInfo

        public BranchPhone()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
