using ByteWebConnector.API.Enums;
using ByteWebConnector.API.Models.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ByteWebConnector.API.Models
{
    [DataContract(Name = "Borrower")]
    public class ByteBorrower
    {
        [DataMember]
        public int? BorrowerId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string OldFirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Generation { get; set; }
        [DataMember]
        public string NickName { get; set; }
        [DataMember]
        public string Ssn { get; set; }
        [DataMember]
        public string HomePhone { get; set; }
        [DataMember]
        public string MobilePhone { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public object Age { get; set; }
        [DataMember]
        public DateTime? Dob { get; set; }
        [DataMember]
        public string Ethnicity { get; set; }
        [DataMember]
        public bool GovDoNotWishToFurnish { get; set; }
        [DataMember]
        public bool RaceNotApplicable { get; set; }
        [DataMember]
        public bool RaceNotProvided { get; set; }
        [DataMember]
        public bool RaceAmericanIndian { get; set; }
        [DataMember]
        public bool RaceAsian { get; set; }
        [DataMember]
        public bool RaceBlack { get; set; }
        [DataMember]
        public bool RacePacificIslander { get; set; }
        [DataMember]
        public bool RaceWhite { get; set; }
        [DataMember]
        public int Gender { get; set; }
        [DataMember]
        public object YearsSchool { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public int? NoDeps { get; set; }
        [DataMember]
        public string DepsAges { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string OldEmail { get; set; }
        [DataMember]
        public string OutstandingJudgements { get; set; }
        [DataMember]
        public string Bankruptcy { get; set; }
        [DataMember]
        public string PropertyForeclosed { get; set; }
        [DataMember]
        public string PartyToLawsuit { get; set; }
        [DataMember]
        public string LoanForeclosed { get; set; }
        [DataMember]
        public string DelinquentFederalDebt { get; set; }
        [DataMember]
        public string AlimonyObligation { get; set; }
        [DataMember]
        public string DownPaymentBorrowed { get; set; }
        [DataMember]
        public string EndorserOnNote { get; set; }
        [DataMember]
        public string OccupyAsPrimaryRes { get; set; }
        [DataMember]
        public string OwnershipInterest { get; set; }
        [DataMember]
        public string PropertyType { get; set; }
        [DataMember]
        public string TitleHeld { get; set; }
        [DataMember]
        public string CitizenResidencyType { get; set; }
     
        [DataMember]
        public string EthnicityOtherHispanicOrLatinoDesc { get; set; }
        [DataMember]
        public string RaceAmericanIndianTribe { get; set; }
        [DataMember]
        public string RaceOtherAsianDesc { get; set; }
        [DataMember]
        public string RaceOtherPacificIslanderDesc { get; set; }
     
        [DataMember]
        public string Race2 { get; set; }
        [DataMember]
        public string Ethnicity2 { get; set; }
        [DataMember]
        public string Gender2 { get; set; }
        [DataMember]
        public int Race2CompletionMethod { get; set; }
        [DataMember]
        public int Ethnicity2CompletionMethod { get; set; }
        [DataMember]
        public long FileDataId { get; set; }
        public BorrowerEntity GetBorrower()
        {
            var borrowerEntity = new ClientModels.BorrowerEntity();
            borrowerEntity.BorrowerId = this.BorrowerId;
            borrowerEntity.FileDataId = this.FileDataId;
            borrowerEntity.CellPhone = this.MobilePhone;
            borrowerEntity.FirstName = this.FirstName;
            borrowerEntity.OldFirstName = this.OldFirstName;
            borrowerEntity.LastName = this.LastName;
            borrowerEntity.MiddleName = this.MiddleName;
            borrowerEntity.Suffix = this.Generation;
            borrowerEntity.HomePhone = this.HomePhone;
            borrowerEntity.EmailAddress = this.Email;
            borrowerEntity.OldEmailAddress = this.OldEmail;
            borrowerEntity.MaritalStatusId = GetMaritalStatusId(this.MaritalStatus);
            borrowerEntity.ResidencyStateId = GetResidencyStateId(this.CitizenResidencyType);
            borrowerEntity.NoOfDependent = this.NoDeps;
            borrowerEntity.DependentAge = this.DepsAges;
            borrowerEntity.OutstandingJudgementsIndicator = GetDeclarationId(this.OutstandingJudgements);
            borrowerEntity.BankruptcyIndicator = GetDeclarationId(this.Bankruptcy);
            borrowerEntity.PartyToLawsuitIndicator = GetDeclarationId(this.PartyToLawsuit);
            borrowerEntity.LoanForeclosureOrJudgementIndicator = GetDeclarationId(this.LoanForeclosed);
            borrowerEntity.AlimonyChildSupportObligationIndicator = GetDeclarationId(this.AlimonyObligation);
            borrowerEntity.BorrowedDownPaymentIndicator = GetDeclarationId(this.DownPaymentBorrowed);
            borrowerEntity.CoMakerEndorserOfNoteIndicator = GetDeclarationId(this.EndorserOnNote);
            borrowerEntity.HomeownerPastThreeYearsIndicator = GetDeclarationId(this.OwnershipInterest);
            borrowerEntity.PresentlyDelinquentIndicator = GetDeclarationId(this.DelinquentFederalDebt);
            borrowerEntity.IntentToOccupyIndicator = GetDeclarationId(this.OccupyAsPrimaryRes);
            borrowerEntity.DeclarationsJIndicator = GetResidencyStateId(this.CitizenResidencyType) == 1 ? 1 : 0;//USCitizen
            borrowerEntity.DeclarationsKIndicator = GetResidencyStateId(this.CitizenResidencyType) == 2 ? 1 : 0;// PermanentResidentAlien

            borrowerEntity.PriorPropertyUsageType = GetPropertyTypeId(this.PropertyType);
            borrowerEntity.PriorPropertyTitleType = GetTitleHeldId(this.TitleHeld);

            borrowerEntity.GenderIds = GetGenderId(this.Gender2);

            borrowerEntity.EthnicityInfo = GetEthnicityInfo(Ethnicity2).Select(ethnicMap => new EthnicInfoItem(ethnicMap.RmEthId,
                                                                                                             ethnicMap.RmEthDetailId))
                                                        .ToList();
            borrowerEntity.RaceInfo = GetRaceInfo(Race2).Select(raceMap => new RaceInfoItem(raceMap.RmRaceId,
                                                                                       raceMap.RmRaceDetailId))
                                                   .ToList();
            borrowerEntity.IsAddOrUpdate = true;


            return borrowerEntity;
        }
        private  List<RaceMap> GetRaceMapping()
        {
            var raceMaps  = new List<RaceMap>();
            raceMaps.Add(new RaceMap {RmRaceId=1,	RmRaceName="American Indian or Alaska Native",	                        RmRaceDetailId=null,	            RmRaceDetailName=null,	                        ByteRaceId=null,	ByteRaceName= "AmericanIndian",	ByteRaceDetailId=null,	ByteRaceDetailName=null});
            raceMaps.Add(new RaceMap {RmRaceId=2,	RmRaceName="Asian",	                                                    RmRaceDetailId=1,                 	RmRaceDetailName="Asian Indian",	            ByteRaceId=null,	ByteRaceName= "Asian",	        ByteRaceDetailId=null,	ByteRaceDetailName= "AsianIndian" });
            raceMaps.Add(new RaceMap {RmRaceId=2,	RmRaceName="Asian",	                                                    RmRaceDetailId=2,                 	RmRaceDetailName="Chinese",	                    ByteRaceId=null,	ByteRaceName= "Asian",	        ByteRaceDetailId=null,	ByteRaceDetailName= "Chinese" });
            raceMaps.Add(new RaceMap {RmRaceId=2,	RmRaceName="Asian",	                                                    RmRaceDetailId=3,                 	RmRaceDetailName="Filipino",	                ByteRaceId=null,	ByteRaceName= "Asian",	        ByteRaceDetailId=null,	ByteRaceDetailName= "Filipino" });
            raceMaps.Add(new RaceMap {RmRaceId=2,	RmRaceName="Asian",	                                                    RmRaceDetailId=4,                 	RmRaceDetailName="Japanese",	                ByteRaceId=null,	ByteRaceName= "Asian",	        ByteRaceDetailId=null,	ByteRaceDetailName= "Japanese" });
            raceMaps.Add(new RaceMap {RmRaceId=2,	RmRaceName="Asian",	                                                    RmRaceDetailId=5,                 	RmRaceDetailName="Korean",	                    ByteRaceId=null,	ByteRaceName= "Asian",	        ByteRaceDetailId=null,	ByteRaceDetailName= "Korean" });
            raceMaps.Add(new RaceMap {RmRaceId=2,	RmRaceName="Asian",	                                                    RmRaceDetailId=6,                 	RmRaceDetailName="Vietnamese",	                ByteRaceId=null,	ByteRaceName= "Asian",	        ByteRaceDetailId=null,	ByteRaceDetailName= "Vietnamese" });
            raceMaps.Add(new RaceMap {RmRaceId=2,	RmRaceName="Asian",	                                                    RmRaceDetailId=7,                 	RmRaceDetailName="Other Asian",	                ByteRaceId=null,	ByteRaceName= "Asian",	        ByteRaceDetailId=null,	ByteRaceDetailName= "OtherAsian" });
            raceMaps.Add(new RaceMap {RmRaceId=3,	RmRaceName="Black or African American",	                                RmRaceDetailId=null,                RmRaceDetailName=null,	                        ByteRaceId=null,	ByteRaceName= "Black",	        ByteRaceDetailId=null,	ByteRaceDetailName=null});
            raceMaps.Add(new RaceMap {RmRaceId=4,	RmRaceName="Native Hawaiian or Other Pacific Islander",	                RmRaceDetailId=8,                 	RmRaceDetailName="Native Hawaiian",	            ByteRaceId=null,	ByteRaceName= "PacificIslander",	ByteRaceDetailId=null,	ByteRaceDetailName= "NativeHawaiian" });
            raceMaps.Add(new RaceMap {RmRaceId=4,	RmRaceName="Native Hawaiian or Other Pacific Islander",	                RmRaceDetailId=9,                 	RmRaceDetailName="Guamanian or Chamorro",	    ByteRaceId=null,	ByteRaceName= "PacificIslander",	ByteRaceDetailId=null,	ByteRaceDetailName= "GuamanianOrChamorro" });
            raceMaps.Add(new RaceMap {RmRaceId=4,	RmRaceName="Native Hawaiian or Other Pacific Islander",	                RmRaceDetailId=10,                  RmRaceDetailName="Samoan",	                    ByteRaceId=null,	ByteRaceName= "PacificIslander",	ByteRaceDetailId=null,	ByteRaceDetailName= "Samoan" });
            raceMaps.Add(new RaceMap {RmRaceId=4,	RmRaceName="Native Hawaiian or Other Pacific Islander",	                RmRaceDetailId=11,                  RmRaceDetailName="Other Pacific Islander",	    ByteRaceId=null,	ByteRaceName= "PacificIslander",	ByteRaceDetailId=null,	ByteRaceDetailName= "OtherPacificIslander" });
            raceMaps.Add(new RaceMap {RmRaceId=5,	RmRaceName="White",	                                                    RmRaceDetailId=null,                RmRaceDetailName=null,	                        ByteRaceId=null,	ByteRaceName= "White",	ByteRaceDetailId=null,	ByteRaceDetailName=null});
            raceMaps.Add(new RaceMap {RmRaceId=6,	RmRaceName="I do not wish to provide this information",                 RmRaceDetailId=null,                RmRaceDetailName=null,	                        ByteRaceId=null,	ByteRaceName= "IDoNotWishToFurnish",	ByteRaceDetailId=null,	ByteRaceDetailName=null	});

            return raceMaps;

        }
        private class RaceMap
        {
            public int? RmRaceId { get; set; }
            public string RmRaceName { get; set; }
            public int? RmRaceDetailId { get; set; }
            public string RmRaceDetailName { get; set; }
            public int? ByteRaceId { get; set; }
            public string ByteRaceName { get; set; }
            public int? ByteRaceDetailId { get; set; }
            public string ByteRaceDetailName { get; set; } 

        }
        private List<RaceMap> GetRaceInfo(string race2)
        {
            var raceMapping = GetRaceMapping();
            var raceMaps = new List<RaceMap>();
            
            string[] raceStrings = race2.Split(",");
            foreach (string raceString in raceStrings)
            {
               var raceMap =  raceMapping.SingleOrDefault(map => map.ByteRaceDetailName == raceString.Trim());
               if (raceMap == null)
               {
                   raceMap = raceMapping.FirstOrDefault(map => map.ByteRaceName == raceString.Trim());
               }
               if (raceMap != null)
                    raceMaps.Add(raceMap); 

            }
            

            return raceMaps;
        }
        private class EthnicMap
        {
            public int? RmEthId { get; set; }
            public string RmEthName { get; set; }
            public int? RmEthDetailId { get; set; }
            public string RmEthDetailName { get; set; }
            public int? ByteEthId { get; set; }
            public string ByteEthName { get; set; }
            public int? ByteEthDetailId { get; set; }
            public string ByteEthDetailName { get; set; }

        }
        private List<EthnicMap> GetEthnicMapping()
        {
            var ethnicMaps = new List<EthnicMap>();
            ethnicMaps.Add(new EthnicMap { RmEthId = 1, RmEthName = "Hispanic or Latino",                         RmEthDetailId = 1,    RmEthDetailName = "Mexican",                  ByteEthId = null, ByteEthName = "HispanicOrLatino",    ByteEthDetailId = null, ByteEthDetailName = "Mexican" });
            ethnicMaps.Add(new EthnicMap { RmEthId = 1, RmEthName = "Hispanic or Latino",                         RmEthDetailId = 2,    RmEthDetailName = "Puerto Rican",             ByteEthId = null, ByteEthName = "HispanicOrLatino",    ByteEthDetailId = null, ByteEthDetailName = "PuertoRican" });
            ethnicMaps.Add(new EthnicMap { RmEthId = 1, RmEthName = "Hispanic or Latino",                         RmEthDetailId = 3,    RmEthDetailName = "Cuban",                    ByteEthId = null, ByteEthName = "HispanicOrLatino",    ByteEthDetailId = null, ByteEthDetailName = "Cuban" });
            ethnicMaps.Add(new EthnicMap { RmEthId = 1, RmEthName = "Hispanic or Latino",                         RmEthDetailId = 4,    RmEthDetailName = "Other Hispanic or Latino", ByteEthId = null, ByteEthName = "HispanicOrLatino",    ByteEthDetailId = null, ByteEthDetailName = "OtherHispanicOrLatino" });
            ethnicMaps.Add(new EthnicMap { RmEthId = 2, RmEthName = "Not Hispanic or Latino",                     RmEthDetailId = null, RmEthDetailName = null,                       ByteEthId = null, ByteEthName = "NotHispanicOrLatino", ByteEthDetailId = null, ByteEthDetailName = null });
            ethnicMaps.Add(new EthnicMap { RmEthId = 3, RmEthName = "I do not wish to provide this information",  RmEthDetailId = null, RmEthDetailName = null,                       ByteEthId = null, ByteEthName = "IDoNotWishToFurnish", ByteEthDetailId = null, ByteEthDetailName = null });
            return ethnicMaps;

        }
        private List<EthnicMap> GetEthnicityInfo(string ethnicity2)
        {
            var ethnicMapping = GetEthnicMapping();
            var ethnicMaps = new List<EthnicMap>();

            string[] ethnicityStrings = ethnicity2.Split(",");
            foreach (string ethnicString in ethnicityStrings)
            {
                var ethnicMap = ethnicMapping.SingleOrDefault(map => map.ByteEthDetailName == ethnicString.Trim());
                if (ethnicMap == null)
                {
                    ethnicMap = ethnicMapping.FirstOrDefault(map => map.ByteEthName == ethnicString.Trim());
                }
                if (ethnicMap != null)
                    ethnicMaps.Add(ethnicMap);


            }
            return ethnicMaps;
        }
        private string GetTitleHeldId(string titleHeld)
        {
            string title = "";
            switch (titleHeld)
            {
                case "SSolely":
                {
                    const int primary = (int)Enums.TitleHeld.Yourself;
                    title = primary.ToString();
                    break;
                }
                case "SPJointlyWithSpouse":
                {
                    const int second = (int)Enums.TitleHeld.JointlyWithYourSpouse;
                    title = second.ToString();
                    break;
                }
                case "OJointlyWithAnotherPerson":
                {
                    const int rental = (int)Enums.TitleHeld.JointlyWithAnotherPerson;
                    title = rental.ToString();
                    break;
                }
            }

            return title;
        }
        private string GetPropertyTypeId(string propertyType)
        {
            string property = "";
            switch (propertyType)
            {
                case "PRPrincipalResidence":
                {
                    const int primary = (int)Enums.PropertyUsageType.Primary;
                    property = primary.ToString();
                    break;
                }
                case "SHSecondHome":
                {
                    const int second = (int)Enums.PropertyUsageType.Second;
                    property = second.ToString();
                    break;
                }
                case "IPInvestmentProperty":
                {
                    const int rental = (int)Enums.PropertyUsageType.Rental;
                    property = rental.ToString();
                    break;
                    }
            }

            return property;
        }
        private int GetDeclarationId(string declaration)
        {
            switch (declaration)
            {
                case "Yes":
                {
                    return (int)Enums.Declaration.Yes;
                    
                }
                case "No":
                {
                    return  (int)Enums.Declaration.No;
                }
            }

            return -1;
        }
        private List<int> GetGenderId(string gender2)
        {
            List<int> ids = new List<int>();
            string[] genderVal = gender2.Split(",");
            foreach (string gender in genderVal)
            {
                switch (gender.Trim())
                {
                    case "Female":
                    {
                        const int female = (int)Enums.Gender.Female;
                        ids.Add(female);
                        break;
                    }
                    case "Male":
                    {
                        const int male = (int)Enums.Gender.Male;
                        ids.Add(male);
                        break;
                    }
                    case "IDoNotWishToFurnish":
                    {
                        const int doNotWish = (int)Enums.Gender.Do_Not_Wish;
                        ids.Add(doNotWish);
                        break;
                    }
                }
            }

            return ids;
        }
        public static int? GetResidencyStateId(string residencyType)
        {
            int? residencyStateId;
            // Residency State
            if (residencyType == "PermanentResidentAlien")
            {
                residencyStateId = (int)ResidencyState.PermanentResident;
            }
            else if (residencyType == "USCitizen")
            {
                residencyStateId = (int)ResidencyState.UsCitizen;
            }
            else
            {
                residencyStateId = null;
            }

            return residencyStateId;
        }
        public static int? GetMaritalStatusId(string maritalType)
        {
            int? maritalStatusId;
            // Residency State
            if (maritalType == "Married")
            {
                maritalStatusId = (int)Enums.MaritalStatus.Married;
            }
            else if (maritalType == "Separated")
            {
                maritalStatusId = (int)Enums.MaritalStatus.Separated;
            }
            else if (maritalType == "Unmarried")
            {
                maritalStatusId = (int)Enums.MaritalStatus.Unmarried;
            }
            else
            {
                maritalStatusId = null;
            }

            return maritalStatusId;
        }
    }
}