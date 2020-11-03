













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BankRuptcy

    public partial class BankRuptcy 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child BorrowerBankRuptcies where [BorrowerBankRuptcy].[BankRuptcyId] point to this entity (FK_BorrowerBankRuptcy_BankRuptcy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerBankRuptcy> BorrowerBankRuptcies { get; set; } // BorrowerBankRuptcy.FK_BorrowerBankRuptcy_BankRuptcy

        public BankRuptcy()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 214;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BorrowerBankRuptcies = new System.Collections.Generic.HashSet<BorrowerBankRuptcy>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
