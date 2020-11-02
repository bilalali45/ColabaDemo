













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Subordinate

    public partial class Subordinate 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? EmployeeId { get; set; } // EmployeeId
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent Employee pointed by [Subordinate].([EmployeeId]) (FK_Subordinate_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_Subordinate_Employee

        public Subordinate()
        {
            EntityTypeId = 78;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
