













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BusinessUnitEmail

    public partial class BusinessUnitEmail 
    {
        public int Id { get; set; } // Id (Primary key)
        public int BusinessUnitId { get; set; } // BusinessUnitId
        public int EmailAccountId { get; set; } // EmailAccountId
        public int TypeId { get; set; } // TypeId

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [BusinessUnitEmail].([BusinessUnitId]) (FK_BusinessUnitEmail_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_BusinessUnitEmail_BusinessUnit

        /// <summary>
        /// Parent EmailAccount pointed by [BusinessUnitEmail].([EmailAccountId]) (FK_BusinessUnitEmail_EmailAccount)
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; } // FK_BusinessUnitEmail_EmailAccount

        public BusinessUnitEmail()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
