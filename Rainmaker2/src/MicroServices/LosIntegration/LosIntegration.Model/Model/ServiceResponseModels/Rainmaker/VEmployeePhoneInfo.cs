













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // vEmployeePhoneInfo

    public partial class VEmployeePhoneInfo 
    {
        public string UserName { get; set; } // UserName (Primary key) (length: 256)
        public int EmployeeId { get; set; } // EmployeeId (Primary key)
        public string Phone { get; set; } // Phone (length: 150)
        public int TypeId { get; set; } // TypeId (Primary key)

        public VEmployeePhoneInfo()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
