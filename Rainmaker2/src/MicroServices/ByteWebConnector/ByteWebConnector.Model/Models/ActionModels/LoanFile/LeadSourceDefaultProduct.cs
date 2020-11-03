













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LeadSourceDefaultProduct

    public partial class LeadSourceDefaultProduct 
    {
        public int LeadSourceId { get; set; } // LeadSourceId (Primary key)
        public int ProductTypeId { get; set; } // ProductTypeId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LeadSource pointed by [LeadSourceDefaultProduct].([LeadSourceId]) (FK_LeadSourceDefaultProduct_LeadSource)
        /// </summary>
        public virtual LeadSource LeadSource { get; set; } // FK_LeadSourceDefaultProduct_LeadSource

        /// <summary>
        /// Parent ProductType pointed by [LeadSourceDefaultProduct].([ProductTypeId]) (FK_LeadSourceDefaultProduct_ProductType)
        /// </summary>
        public virtual ProductType ProductType { get; set; } // FK_LeadSourceDefaultProduct_ProductType

        public LeadSourceDefaultProduct()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
