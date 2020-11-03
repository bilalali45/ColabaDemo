













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Department

    public partial class Department 
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
        /// Child EmpDepartmentBinders where [EmpDepartmentBinder].[DepartmentId] point to this entity (FK_EmpDepartmentBinder_Department)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmpDepartmentBinder> EmpDepartmentBinders { get; set; } // EmpDepartmentBinder.FK_EmpDepartmentBinder_Department
        /// <summary>
        /// Child NoteDepartmentBinders where [NoteDepartmentBinder].[DepartmentId] point to this entity (FK_NoteDepartmentBinder_Department)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<NoteDepartmentBinder> NoteDepartmentBinders { get; set; } // NoteDepartmentBinder.FK_NoteDepartmentBinder_Department

        public Department()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 2;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            EmpDepartmentBinders = new System.Collections.Generic.HashSet<EmpDepartmentBinder>();
            NoteDepartmentBinders = new System.Collections.Generic.HashSet<NoteDepartmentBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
