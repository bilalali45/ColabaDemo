using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace LoanApplication.Model
{

    public class BorrowerConsentModel
    { 
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
       public int LoanApplicationId { get; set; }
       [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
       public bool IsAccepted { get; set; }
       public string Description { get; set; }
       [Range(1, int.MaxValue, ErrorMessage = "Invalid consent type id")]
        public int ConsentTypeId { get; set; }
        public string State { get; set; }
        public string ConsentHash  { get; set; }
    }

    public class BorrowerConsentPostModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid consent type id")]
        public int ConsentTypeId { get; set; }
        public string Description { get; set; }
    }

    public class BorrowerMultipleConsentsBase
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }

        public bool IsAccepted { get; set; }

        public string State { get; set; }

        public string ConsentHash { get; set; }
    }


    public class BorrowerMultipleConsentsModel : BorrowerMultipleConsentsBase
    {
        public List<BorrowerConsentPostModel> BorrowerConsents { get; set; }
    }

    public class BorrowerAcceptedConsentsGetModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
    }
}
