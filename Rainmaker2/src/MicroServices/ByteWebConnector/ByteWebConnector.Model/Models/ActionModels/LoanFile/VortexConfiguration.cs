













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // VortexConfiguration

    public partial class VortexConfiguration 
    {
        public int Id { get; set; } // id (Primary key)
        public string UserName { get; set; } // UserName (Primary key) (length: 256)
        public string Phone { get; set; } // Phone (length: 150)
        public string Type { get; set; } // Type (length: 50)
        public bool IsActive { get; set; } // IsActive (Primary key)
        public bool IsDeleted { get; set; } // IsDeleted (Primary key)
        public int? ServiceSettingId { get; set; } // ServiceSettingId
        public string WebHook { get; set; } // WebHook (Primary key) (length: 253)
        public string AppSid { get; set; } // AppSid (length: 50)
        public string CallbackDomain { get; set; } // CallbackDomain (length: 253)

        public VortexConfiguration()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
