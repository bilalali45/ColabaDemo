using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosIntegration.API.Models.ClientModels.Document
{
    public class DeleteFileRequest
    {
        public string  FileId { get; set; }
        public int LoanApplicationId { get; set; }
    }
}
