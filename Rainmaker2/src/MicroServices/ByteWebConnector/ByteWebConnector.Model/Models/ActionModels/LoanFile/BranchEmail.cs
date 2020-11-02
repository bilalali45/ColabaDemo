













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BranchEmail

    public partial class BranchEmail 
    {
        public int Id { get; set; } // Id (Primary key)
        public int BranchId { get; set; } // BranchId
        public int EmailAccountId { get; set; } // EmailAccountId
        public int TypeId { get; set; } // TypeId

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [BranchEmail].([BranchId]) (FK_BranchEmail_Branch)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_BranchEmail_Branch

        /// <summary>
        /// Parent EmailAccount pointed by [BranchEmail].([EmailAccountId]) (FK_BranchEmail_EmailAccount)
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; } // FK_BranchEmail_EmailAccount

        public BranchEmail()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
