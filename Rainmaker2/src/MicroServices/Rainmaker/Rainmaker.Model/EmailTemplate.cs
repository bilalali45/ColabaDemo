using System;
using System.Collections.Generic;
using System.Text;

namespace Rainmaker.Model
{
    public static class TokenKey
    {
        public const string Date = "Date";
        public const string PrimaryBorrowerEmailAddress = "PrimaryBorrowerEmailAddress";
        public const string PrimaryBorrowerFirstName = "PrimaryBorrowerFirstName";
        public const string PrimaryBorrowerLastName = "PrimaryBorrowerLastName";
        public const string CoBorrowerFirstName = "Co-BorrowerFirstName";
        public const string CoBorrowerLastName = "Co-BorrowerLastName";
        public const string CoBorrowerEmailAddress = "Co-BorrowerEmailAddress";
        public const string EmailTag = "EmailTag";
        public const string LoanPortalUrl = "LoanPortalUrl";
        public const string LoanStatus = "LoanStatus";
        public const string SubjectPropertyAddress = "SubjectPropertyAddress";
        public const string SubjectPropertyState = "SubjectPropertyState";
        public const string SubjectPropertyStateAbbreviation = "SubjectPropertyStateAbbreviation";
        public const string SubjectPropertyCounty = "SubjectPropertyCounty";
        public const string SubjectPropertyCity = "SubjectPropertyCity";
        public const string SubjectPropertyZipCode = "SubjectPropertyZipCode";
        public const string LoanPurpose = "LoanPurpose";
        public const string LoanAmount = "LoanAmount";
        public const string PropertyValue = "PropertyValue";
        public const string PropertyType = "PropertyType";
        public const string PropertyUsage = "PropertyUsage";
        public const string ResidencyType = "ResidencyType";
        public const string BranchNmlsNo = "BranchNmlsNo";
        public const string BusinessUnitName = "BusinessUnitName";
        public const string BusinessUnitPhoneNumber = "BusinessUnitPhoneNumber";
        public const string BusinessUnitWebSiteUrl = "BusinessUnitWebSiteUrl";
        public const string LoanApplicationLoginLink = "LoanApplicationLoginLink";
        public const string LoanOfficerPageUrl = "LoanOfficerPageUrl";
        public const string LoanOfficerFirstName = "LoanOfficerFirstName";
        public const string LoanOfficerLastName = "LoanOfficerLastName";
        public const string RequestDocumentList = "RequestDocumentList";
        public const string RequestorUserEmail = "RequestorUserEmail";

    }

    public class TokenModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string description { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }

    public class EmailTemplate
    {
        public int id { get; set; }
        public int templateTypeId { get; set; }
        public int tenantId { get; set; }
        public string templateName { get; set; }
        public string templateDescription { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string CCAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        public int sortOrder { get; set; }
    }
    public class EmailTemplateModel
    {
        public int id { get; set; }
        public int loanApplicationId { get; set; }
        public int tenantId { get; set; }
        public string templateName { get; set; }
        public string templateDescription { get; set; }
        public string fromAddress { get; set; }
        public string ccAddress { get; set; }
        public string toAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        public List<TokenModel> lstTokens { get; set; }
    }
}
