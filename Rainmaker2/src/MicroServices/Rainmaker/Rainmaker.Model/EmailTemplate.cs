using System;
using System.Collections.Generic;
using System.Text;

namespace Rainmaker.Model
{
    public static class TokenKey
    {
        public const string LoginUserEmail = "LoginUserEmail"; // system template
        public const string CustomerFirstName = "CustomerFirstName"; // tenant template
        public const string BusinessUnitName = "BusinessUnitName"; // mcu template
    }

    public class TokenModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string description { get; set; }
        public string key { get; set; }
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
        public string toAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        public List<TokenModel> lstTokens { get; set; }
    }
}
