













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanRequestDetail

    public partial class LoanRequestDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public string RequestTpXml { get; set; } // RequestTpXml (length: 255)
        public string ResponseTpXml { get; set; } // ResponseTpXml (length: 255)
        public bool IsDeleted { get; set; } // IsDeleted
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool? HasResult { get; set; } // HasResult
        public bool? HasError { get; set; } // HasError
        public string ErrorMessage { get; set; } // ErrorMessage (length: 1000)
        public System.DateTime? RequestStartTimeUtc { get; set; } // RequestStartTimeUtc
        public System.DateTime? RequestEndTimeUtc { get; set; } // RequestEndTimeUtc
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? NoOfProduct { get; set; } // NoOfProduct
        public string Guid { get; set; } // Guid (length: 50)

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [LoanRequestDetail].([LoanRequestId]) (FK_LoanRequestDetail_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_LoanRequestDetail_LoanRequest

        public LoanRequestDetail()
        {
            IsDeleted = false;
            EntityTypeId = 131;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
