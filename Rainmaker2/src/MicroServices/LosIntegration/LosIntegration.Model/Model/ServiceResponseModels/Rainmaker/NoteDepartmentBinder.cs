













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // NoteDepartmentBinder

    public partial class NoteDepartmentBinder 
    {
        public int DepartmentId { get; set; } // DepartmentId (Primary key)
        public int NoteId { get; set; } // NoteId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Department pointed by [NoteDepartmentBinder].([DepartmentId]) (FK_NoteDepartmentBinder_Department)
        /// </summary>
        public virtual Department Department { get; set; } // FK_NoteDepartmentBinder_Department

        /// <summary>
        /// Parent Note pointed by [NoteDepartmentBinder].([NoteId]) (FK_NoteDepartmentBinder_Note)
        /// </summary>
        public virtual Note Note { get; set; } // FK_NoteDepartmentBinder_Note

        public NoteDepartmentBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
