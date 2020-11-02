













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // InvestorProduct

    public partial class InvestorProduct 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ProductId { get; set; } // ProductId
        public string ProductName { get; set; } // ProductName (length: 500)
        public int? InvestorId { get; set; } // InvestorId
        public int? DtiHousing { get; set; } // DtiHousing
        public int? DtiTotal { get; set; } // DtiTotal
        public string Code { get; set; } // Code (length: 10)
        public int EntityTypeId { get; set; } // EntityTypeId
        public string ProductDescription { get; set; } // ProductDescription (length: 500)

        // Foreign keys

        /// <summary>
        /// Parent Investor pointed by [InvestorProduct].([InvestorId]) (FK_InvestorProduct_Investor)
        /// </summary>
        public virtual Investor Investor { get; set; } // FK_InvestorProduct_Investor

        /// <summary>
        /// Parent Product pointed by [InvestorProduct].([ProductId]) (FK_InvestorProduct_Product)
        /// </summary>
        public virtual Product Product { get; set; } // FK_InvestorProduct_Product

        public InvestorProduct()
        {
            EntityTypeId = 40;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
