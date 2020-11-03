using System;
using System.Collections.Generic;
using System.Text;

namespace ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi
{
    public class ByteNewFileResponse
    {
        public string OrganizationCode { get; set; }
        public int TemplateId { get; set; }
        public string SubPropState { get; set; }
        public int DesiredOriginationChannel { get; set; }
        public string LoanOfficerUserName { get; set; }
        public string LoanProcessorUserName { get; set; }
        public int FileDataId { get; set; }
        public string FileName { get; set; }
        public string BorrowerFirstName { get; set; }
        public string BorrowerLastName { get; set; }
        public string BorrowerMiddleName { get; set; }
        public string BorrowerSuffix { get; set; }
        public string CoBorrowerFirstName { get; set; }
        public string CoBorrowerLastName { get; set; }
        public string CoBorrowerMiddleName { get; set; }
        public string CoBorrowerSuffix { get; set; }
        public double LoanAmount { get; set; }
        public int ApplicationId { get; set; }
    }
}
