













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LoanApplication

    public partial class LoanApplication 
    {
        public string GetByteOrganizationCode()
        {
            var code = string.Empty;
            switch (this.BusinessUnitId)
            {
                case 1:
                    code = "2001";
                    break;
                case 2:
                    code = "2002";
                    break;
                case 3:
                    code = "2003";
                    break;
            }

            return code;
        }
    }

}
// </auto-generated>
