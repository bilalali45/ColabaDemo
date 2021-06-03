using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LoanApplication.Model
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string AnswerDetail { get; set; }
        public JsonElement? Data { get; set; }
    }

    public class QuestionReviewModel
    {
        public int Id { get; set; }
        public int OwnTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string AnswerDetail { get; set; }
        [JsonIgnore]
        public int? SelectionOptionId { get; set; }
        public JsonElement? Data { get; set; }
    }


    public class QuestionRequestModel
    {
        public int LoanApplicationId { get; set; }
        public int BorrowerId { get; set; }
        public string State { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }

    public class DropDownModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class DetailDropDownModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
    public class RaceModel : DropDownModel
    {
        public List<DetailDropDownModel> RaceDetails { get; set; }
    }

    public class EthnicityModel : DropDownModel
    {
        public List<DetailDropDownModel> EthnicityDetails { get; set; }
    }



    #region Response
    public class RaceReviewDetailResponseModel
    {
        public int? DetailId { get; set; }
        public string OtherRace { get; set; }
        public bool? IsOther { get; set; }
    }
    public class RaceDetailReviewResponseModel
    {
        public int? DetailId { get; set; }
        public string RaceDetail { get; set; }
        public string OtherRace { get; set; }
        public bool? IsOther { get; set; }
    }
    public class RaceDetailResponseModel
    {
        public int? DetailId { get; set; }
        public string OtherRace { get; set; }
        public bool? IsOther { get; set; }
    }
    public class EthenticityDetailResponseModel
    {
        public int? DetailId { get; set; }
        public string OtherEthnicity { get; set; }
        public bool? IsOther { get; set; }
    }
    public class EthenticityDetailReviewResponseModel
    {
        public int? DetailId { get; set; }
        public string EthenticityDetail { get; set; }
        public string OtherEthnicity { get; set; }
        public bool? IsOther { get; set; }
    }
    public class RaceReviewResponseModel
    {
        public int RaceId { get; set; }
        public string Race { get; set; }
        public List<RaceDetailReviewResponseModel> RaceDetailIds { get; set; }
    }
    public class RaceResponseModel
    {
        public int RaceId { get; set; }
        public List<RaceDetailResponseModel> RaceDetailIds { get; set; }
    }
    public class EthenticityResponseModel
    {
        public int EthenticityId { get; set; }
        public List<EthenticityDetailResponseModel> EthnicityDetailIds { get; set; }
    }

    public class EthenticityReviewResponseModel
    {
        public int EthenticityId { get; set; }
        public string  Ethenticity { get; set; }
        public List<EthenticityDetailReviewResponseModel> EthnicityDetailIds { get; set; }
    }

    public class DemographicInfoResponseModel
    {
        public List<RaceResponseModel> RaceResponseModel { get; set; }
        public List<EthenticityResponseModel> EthenticityResponseModel { get; set; }
        public int? GenderId { get; set; }
        public int LoanApplicationId { get; set; }
        public int BorrowerId { get; set; }
        public string State { get; set; }
    }

    public class DemographicReviewResponseModel
    {
        public int OwnTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RaceReviewResponseModel> RaceResponseModel { get; set; }
        public List<EthenticityReviewResponseModel> EthenticityResponseModel { get; set; }
        public int? GenderId { get; set; }
        public string Gender { get; set; }
        public int LoanApplicationId { get; set; }
        public int BorrowerId { get; set; }
        public string State { get; set; }
    }

    #endregion Response


    public class PrimaryBorrowerSubjectPropertyModel
    {
        public bool isSubjectPropertyPrimaryResidence { get; set; }
        public bool isReoDefined { get; set; }
        public int? PropertyUsageId { get; set; }
    }

    public class SecondaryBorrowerSubjectPropertyModel
    {
        public bool willLiveInSubjectProperty { get; set; }
        public bool isReoDefined { get; set; }
        public int? PropertyUsageId { get; set; }
    }
}
