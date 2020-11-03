













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ProductTemplatekey

    public partial class ProductTemplatekey 
    {
        public int Id { get; set; } // Id (Primary key)
        public int ProductId { get; set; } // ProductId
        public string Symbol { get; set; } // Symbol (length: 100)
        public string KeyName { get; set; } // KeyName (length: 300)
        public string Description { get; set; } // Description (length: 500)
        public int FieldTypeId { get; set; } // FieldTypeId

        // Foreign keys

        /// <summary>
        /// Parent Product pointed by [ProductTemplatekey].([ProductId]) (FK_ProductTemplatekey_Product)
        /// </summary>
        public virtual Product Product { get; set; } // FK_ProductTemplatekey_Product

        public ProductTemplatekey()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
