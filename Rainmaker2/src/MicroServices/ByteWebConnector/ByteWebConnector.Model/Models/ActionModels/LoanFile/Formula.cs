













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Formula

    public partial class Formula 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string Expression { get; set; } // Expression (length: 500)
        public string DependOnIds { get; set; } // DependOnIds
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Adjustments where [Adjustment].[FormulaId] point to this entity (FK_Adjustment_Formula)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Adjustment> Adjustments { get; set; } // Adjustment.FK_Adjustment_Formula
        /// <summary>
        /// Child FeeDetails where [FeeDetail].[FormulaId] point to this entity (FK_FeeDetail_Formula)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FeeDetail> FeeDetails { get; set; } // FeeDetail.FK_FeeDetail_Formula
        /// <summary>
        /// Child ProfitTables where [ProfitTable].[FormulaId] point to this entity (FK_ProfitTable_Formula)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ProfitTable> ProfitTables { get; set; } // ProfitTable.FK_ProfitTable_Formula
        /// <summary>
        /// Child PropertyTaxes where [PropertyTax].[FormulaId] point to this entity (FK_PropertyTax_Formula)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyTax> PropertyTaxes { get; set; } // PropertyTax.FK_PropertyTax_Formula
        /// <summary>
        /// Child RangeSetDetails where [RangeSetDetail].[FormulaId] point to this entity (FK_RangeSetDetail_Formula)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RangeSetDetail> RangeSetDetails { get; set; } // RangeSetDetail.FK_RangeSetDetail_Formula

        public Formula()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 33;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Adjustments = new System.Collections.Generic.HashSet<Adjustment>();
            FeeDetails = new System.Collections.Generic.HashSet<FeeDetail>();
            ProfitTables = new System.Collections.Generic.HashSet<ProfitTable>();
            PropertyTaxes = new System.Collections.Generic.HashSet<PropertyTax>();
            RangeSetDetails = new System.Collections.Generic.HashSet<RangeSetDetail>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
