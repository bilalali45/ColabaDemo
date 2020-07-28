using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using ByteWebConnector.API.Enums;
using ByteWebConnector.API.Models.ClientModels;

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
        //public int _USCitizen { get; set; }
        //public int _ResidentAlien { get; set; }
        [DataMember]
        public string EthnicityOtherHispanicOrLatinoDesc { get; set; }
        [DataMember]
        public string RaceAmericanIndianTribe { get; set; }
        [DataMember]
        public string RaceOtherAsianDesc { get; set; }
        [DataMember]
        public string RaceOtherPacificIslanderDesc { get; set; }
        //public int DemographicInfoProvidedMethod { get; set; }
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

        public BorrowerEntity GetRainmakerBorrower()
        {
            var borrowerEntity = new ClientModels.BorrowerEntity();
            borrowerEntity.BorrowerId = this.BorrowerId;
            borrowerEntity.FileDataId = this.FileDataId;
            borrowerEntity.CellPhone = this.MobilePhone;
            borrowerEntity.FirstName = this.FirstName;
            borrowerEntity.LastName = this.LastName;
            borrowerEntity.MiddleName = this.MiddleName;
            borrowerEntity.Suffix = this.Generation;
            borrowerEntity.HomePhone = this.HomePhone;
            borrowerEntity.EmailAddress = this.Email;
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
            borrowerEntity.DeclarationsJIndicator = GetResidencyStateId(this.CitizenResidencyType) == 1 ? 1 : 0;
            borrowerEntity.DeclarationsKIndicator = GetResidencyStateId(this.CitizenResidencyType) == 2 ? 1 : 0;

            borrowerEntity.PriorPropertyUsageType = GetPropertyTypeId(this.PropertyType);
            borrowerEntity.PriorPropertyTitleType = GetTitleHeldId(this.TitleHeld);

            borrowerEntity.GenderIds = GetGenderId(this.Gender2);

            Dictionary<int, List<int>> ethnicityDictionary;
            borrowerEntity.EthnicityId = GetEthnicityId(this.Ethnicity2,out ethnicityDictionary);
            borrowerEntity.EthnicityDetailId = GetEthnicityDetailId(this.Ethnicity2);
            borrowerEntity.EthnicityDictionary = ethnicityDictionary;


            Dictionary<int, List<int>> raceDictionary;
            borrowerEntity.RaceInfo = GetRaceInfo(Race2).Select(raceMap => new RaceInfoItem(raceMap.RmRaceId,
                                                                                       raceMap.RmRaceDetailId))
                                                   .ToList();



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

               #region OldCode

               //switch (raceString.Trim())
               //{
               //    case "AmericanIndian":
               //    {
               //        const int americanIndianOrAlaskaNative = (int)RaceEnum.AmericanIndianOrAlaskaNative;
               //        ids.Add(americanIndianOrAlaskaNative);
               //        raceDictionary.Add(americanIndianOrAlaskaNative, new List<int>());
               //        break;
               //    }
               //    case "Asian":
               //    {
               //        const int asian = (int) RaceEnum.Asian;
               //        ids.Add(asian);
               //        List<int> childList = new List<int>();
               //        foreach (string s in raceStrings)
               //        {
               //                switch (s.Trim())
               //                {
               //                    case "AsianIndian":
               //                        {
               //                            const int asianIndian = (int)RaceDetailEnum.AsianIndian;
               //                            childList.Add(asianIndian);
               //                            break;
               //                        }
               //                    case "Chinese":
               //                        {
               //                            const int chinese = (int)RaceDetailEnum.Chinese;
               //                            childList.Add(chinese);
               //                            break;
               //                        }
               //                    case "Filipino":
               //                        {
               //                            const int filipino = (int)RaceDetailEnum.Filipino;
               //                            childList.Add(filipino);
               //                            break;
               //                        }
               //                    case "Japanese":
               //                        {
               //                            const int japanese = (int)RaceDetailEnum.Japanese;
               //                            childList.Add(japanese);
               //                            break;
               //                        }
               //                    case "Korean":
               //                        {
               //                            const int korean = (int)RaceDetailEnum.Korean;
               //                            childList.Add(korean);
               //                            break;
               //                        }
               //                    case "Vietnamese":
               //                        {
               //                            const int vietnamese = (int)RaceDetailEnum.Vietnamese;
               //                            childList.Add(vietnamese);
               //                            break;
               //                        }
               //                    case "OtherAsian":
               //                        {
               //                            const int otherAsian = (int)RaceDetailEnum.OtherAsian;
               //                            childList.Add(otherAsian);
               //                            break;
               //                        }
               //                }
               //        }
               //        raceDictionary.Add(asian, childList);
               //        break;
               //    }
               //    case "Black":
               //    {
               //        const int black = (int)RaceEnum.BlackOrAfricanAmerican;
               //        ids.Add(black);
               //        raceDictionary.Add(black, new List<int>());
               //        break;
               //    }
               //    case "PacificIslander":
               //    {
               //        const int pacificIslander = (int)RaceEnum.NativeHawaiianOrOtherPacificIslander;
               //        ids.Add(pacificIslander);
               //        List<int> childList = new List<int>();
               //        foreach (string s in raceStrings)
               //        {
               //            switch (s.Trim())
               //            {
               //                case "NativeHawaiian":
               //                {
               //                    const int nativeHawaiian = (int)RaceDetailEnum.NativeHawaiian;
               //                    childList.Add(nativeHawaiian);
               //                    break;
               //                }
               //                case "GuamanianOrChamorro":
               //                {
               //                    const int guamanianOrChamorro = (int)RaceDetailEnum.GuamanianOrChamorro;
               //                    childList.Add(guamanianOrChamorro);
               //                    break;
               //                }
               //                case "Samoan":
               //                {
               //                    const int samoan = (int)RaceDetailEnum.Samoan;
               //                    childList.Add(samoan);
               //                    break;
               //                }
               //                case "OtherPacificIslander":
               //                {
               //                    const int otherPacificIslander = (int)RaceDetailEnum.OtherPacificIslander;
               //                    childList.Add(otherPacificIslander);
               //                    break;
               //                }
               //            }
               //        }
               //        raceDictionary.Add(pacificIslander, childList);
               //        break;
               //    }
               //    case "White":
               //    {
               //        const int white = (int)RaceEnum.White;
               //        ids.Add(white);
               //        raceDictionary.Add(white, new List<int>());
               //        break;
               //    }
               //    case "IDoNotWishToFurnish":
               //    {
               //        const int doNotWishToProvideThisInformation = (int)RaceEnum.DoNotWishToProvideThisInformation;
               //        ids.Add(doNotWishToProvideThisInformation);
               //        raceDictionary.Add(doNotWishToProvideThisInformation, new List<int>());
               //        break;
               //    }

               #endregion

            }
            

            return raceMaps;
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
                    const int primary = (int)Enums.PropertyType.Primary;
                    property = primary.ToString();
                    break;
                }
                case "SHSecondHome":
                {
                    const int second = (int)Enums.PropertyType.Second;
                    property = second.ToString();
                    break;
                }
                case "IPInvestmentProperty":
                {
                    const int rental = (int)Enums.PropertyType.Rental;
                    property = rental.ToString();
                    break;
                    }
            }

            return property;
        }


        private int GetDeclarationId(string declaration)
        {
            int id = 0;
            switch (declaration)
            {
                case "Yes":
                {
                    const int yes = (int)Enums.Declaration.Yes;
                    id = yes;
                    break;
                }
                case "No":
                {
                    const int male = (int)Enums.Declaration.No;
                    id = male;
                    break;
                }
            }

            return id;
        }


        private string GetEthnicityValue(string[] ethnicityValues)
        {
            foreach (string ethnicityValue in ethnicityValues)
            {
                if (ethnicityValue == EthnicityEnum.HispanicOrLatino.ToString())
                {

                    return ethnicityValue;
                }
                else if (ethnicityValue == EthnicityEnum.NotHispanicOrLatino.ToString())
                {
                    return ethnicityValue;
                }
                else if (ethnicityValue == EthnicityEnum.DoNotWishToProvideThisInformation.ToString())
                {
                    return ethnicityValue;
                }
            }

            return null;
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


        private List<int> GetRaceDetailId(string race2,
                                          Dictionary<int, List<int>> raceDictionary)
        {
            List<int> ids= new List<int>();
            string[] raceValues = race2.Split(",");
            foreach (string raceValue in raceValues)
            {
                switch (raceValue.Trim())
                {
                    case "AsianIndian":
                        {
                            const int asianIndian = (int)RaceDetailEnum.AsianIndian;
                            ids.Add(asianIndian);
                            break;
                        }
                    case "Chinese":
                        {
                            const int chinese = (int)RaceDetailEnum.Chinese;
                            ids.Add(chinese);
                            break;
                        }
                    case "Filipino":
                        {
                            const int filipino = (int)RaceDetailEnum.Filipino;
                            ids.Add(filipino);
                            break;
                        }
                    case "Japanese":
                        {
                            const int japanese = (int)RaceDetailEnum.Japanese;
                            ids.Add(japanese);
                            break;
                        }
                    case "Korean":
                        {
                            const int korean = (int)RaceDetailEnum.Korean;
                            ids.Add(korean);
                            break;
                        }
                    case "Vietnamese":
                        {
                            const int vietnamese = (int)RaceDetailEnum.Vietnamese;
                            ids.Add(vietnamese);
                            break;
                        }
                    case "OtherAsian":
                        {
                            const int otherAsian = (int)RaceDetailEnum.OtherAsian;
                            ids.Add(otherAsian);
                            break;
                        }
                    case "NativeHawaiian":
                        {
                            const int nativeHawaiian = (int)RaceDetailEnum.NativeHawaiian;
                            ids.Add(nativeHawaiian);
                            break;
                        }
                    case "GuamanianOrChamorro":
                        {
                            const int guamanianOrChamorro = (int)RaceDetailEnum.GuamanianOrChamorro;
                            ids.Add(guamanianOrChamorro);
                            break;
                        }
                    case "Samoan":
                        {
                            const int samoan = (int)RaceDetailEnum.Samoan;
                            ids.Add(samoan);
                            break;
                        }
                    case "OtherPacificIslander":
                        {
                            const int otherPacificIslander = (int)RaceDetailEnum.OtherPacificIslander;
                            ids.Add(otherPacificIslander);
                            break;
                        }
                }
            }
         

            return ids;
        }


        private List<int> GetEthnicityId(string ethnicity2,
                                         out Dictionary<int, List<int>> ethnicityDictionary)
        {
             ethnicityDictionary = new Dictionary<int, List<int>>();
            List<int> ids = new List<int>();
            string[] ethnicityValues = ethnicity2.Split(",");
            foreach (var ethnicityValue in ethnicityValues)
            {
                switch (ethnicityValue.Trim())
                {
                    case "HispanicOrLatino":
                    {
                        const int hispanicOrLatino = (int)EthnicityEnum.HispanicOrLatino;
                        ids.Add(hispanicOrLatino);
                        List<int> ethnicityDetailIds = GetEthnicityDetailId(ethnicity2);
                        ethnicityDictionary.Add(hispanicOrLatino, ethnicityDetailIds);
                        break;
                    }
                    case "NotHispanicOrLatino":
                    {
                        const int notHispanicOrLatino = (int)EthnicityEnum.NotHispanicOrLatino;
                        ids.Add(notHispanicOrLatino);
                        ethnicityDictionary.Add(notHispanicOrLatino, new List<int>());
                        break;
                        }
                    case "IDoNotWishToFurnish":
                    {
                        const int doNotWishToProvideThisInformation = (int)EthnicityEnum.DoNotWishToProvideThisInformation;
                        ids.Add(doNotWishToProvideThisInformation);
                        ethnicityDictionary.Add(doNotWishToProvideThisInformation, new List<int>());
                        break;
                    }
                }
            }
            return ids;
        }


        private List<int> GetEthnicityDetailId(string ethnicity2)
        {
            List<int> ids = new List<int>();
            string[] ethnicityValues = ethnicity2.Split(",");
            foreach (var ethnicityValue in ethnicityValues)
            {
                switch (ethnicityValue.Trim())
                {

                    case "Mexican":
                    {
                        const int mexican = (int)EthnicityDetailEnum.Mexican;
                        ids.Add(mexican);
                        break;
                    }
                    case "PuertoRican":
                    {
                        const int puertoRican = (int)EthnicityDetailEnum.PuertoRican;
                        ids.Add(puertoRican);
                        break;
                    }
                    case "Cuban":
                    {
                        const int cuban = (int)EthnicityDetailEnum.Cuban;
                        ids.Add(cuban);
                        break;
                    }
                    case "OtherHispanicOrLatino":
                    {
                        const int otherHispanicOrLatino = (int)EthnicityDetailEnum.OtherHispanicOrLatino;
                        ids.Add(otherHispanicOrLatino);
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
                residencyStateId = (int)ResidencyStateEnum.PermanentResident;
            }
            else if (residencyType == "USCitizen")
            {
                residencyStateId = (int)ResidencyStateEnum.UsCitizen;
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
    public class RaceInfoItem
    {
        public int? RaceId { get; }
        public int? RaceDetailId { get; }


        public RaceInfoItem(int? raceId,
                            int? raceDetailId)
        {
            RaceId = raceId;
            RaceDetailId = raceDetailId;
        }
    }

}