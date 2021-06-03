using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoanApplication.Model
{
    public class RainmakerAddressInfoBase
    {
        public int LoanApplicationId { get; set; }
        public string StreetAddress { get; set; }
        public string UnitNo { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public string CityName { get; set; }
    }
    public class RainmakerAddressInfoModel : RainmakerAddressInfoBase
    {   
        public DateTime? ExpectedClosingDate { get; set; }
        public string State { get; set; }
    }

    public class UpdateSubjectPropertyStateModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        public int? StateId { get; set; }
        public string State { get; set; }
    }

    public class GetSubjectPropertyStateModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        public int? StateId { get; set; }
    }

    public class LoanAmountDetailBase
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Purchase price is not valid")]
        public decimal? PropertyValue { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Down payment is not valid")]
        public decimal? DownPayment { get; set; }
        public bool GiftPartOfDownPayment { get; set; }
        public bool? GiftPartReceived { get; set; }
        public DateTime? DateOfTransfer { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Gift amount is not valid")]
        public decimal? GiftAmount { get; set; }
    }

    public class LoanAmountDetailModel : LoanAmountDetailBase
    {
        public string State { get; set; }
    }

    public class PropertyIdentifiedBaseModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public bool? IsIdentified { get; set; }
    }

    public class PropertyIdentifiedModel : PropertyIdentifiedBaseModel
    {
        public string State { get; set; }
    }
}
