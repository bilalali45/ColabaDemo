using ByteWebConnector.SDK.Models.Rainmaker;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using ByteWebConnector.SDK.Mismo;
using ByteWebConnector.SDK.Models;
using Castle.Components.DictionaryAdapter;
using Newtonsoft.Json;
using Xunit;

namespace ByteWebConnector.SDK.Tests
{
    public class TestMismoConversion
    {
        [Fact]
        public void TestAssetsWhenNetEquityExists()
        {
            //Arrange            
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 1,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 1,
                BusinessUnitId = 1,
                CustomerId = 1,
                LoanOriginatorId = 1,
                LoanGoalId = 1,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                    {
                        Id=1,
                        BorrowerAccountBinders= new List<BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=1,
                            BorrowerId=1,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=33//NetEquity
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=1,
                               PropertyInfoId=1,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=1,
                                        CityId=1,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=1,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 1,
                        CityId = 1,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            //Assert
            Assert.NotNull(result);
            var expected = new
            ASSETS
            {
                ASSET = new List<ASSET>
            {
               new  ASSET{
                ASSET_DETAIL = new ASSET_DETAIL
                {
                    AssetAccountIdentifier = null,
                    AssetCashOrMarketValueAmount = 1000,
                    AssetType = "Stock",
                    AssetTypeOtherDescription = null,
                    EXTENSION = null
                },
                ASSET_HOLDER = new ASSET_HOLDER
                {
                    NAME = new NAME
                    {
                        FullName = null,
                        FirstName = null,
                        LastName = null,
                        MiddleName = null,
                        SuffixName = null
                    }
                },
                SequenceNumber = 1,
                Label = "ASSET_1",
                OWNED_PROPERTY = null
            }
               ,new ASSET
                           {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY=  new OWNED_PROPERTY{
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "Retain",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= true,
                                OwnedPropertyRentalIncomeGrossAmount= 0,
                                OwnedPropertyRentalIncomeNetAmount= 0
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS= new List<ADDRESS> {
                                     new ADDRESS
                                     {

                                        AddressLineText=string.Empty ,
                                        CountryCode= "US",
                                        CountryName= "United States",
                                        CityName= null,
                                        PostalCode= null,
                                        StateCode= "Pak",
                                        CountyName= null


                                    }},
                                PROPERTY_DETAIL= new PROPERTY_DETAIL
                                {
                                    PropertyCurrentUsageType= null,
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "PrimaryResidence",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                          }
               , new  ASSET
                           {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 3,
                        Label= "ASSET_3",
                        OWNED_PROPERTY=new OWNED_PROPERTY {
                            OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "Retain",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= false,
                                OwnedPropertyRentalIncomeGrossAmount= 0,
                                OwnedPropertyRentalIncomeNetAmount= 0
                            },
                            PROPERTY= new PROPERTY  {
                                ADDRESS= new List<ADDRESS>
                                { new ADDRESS

                                    {
                                        AddressLineText= string.Empty,
                                        CountryCode= "US",
                                        CountryName= "United States",
                                        CityName= null,
                                        PostalCode= "123",
                                        StateCode= null,
                                        CountyName= null
                                    }
                                }
                                ,
                                PROPERTY_DETAIL= new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= "PrimaryResidence",
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "PrimaryResidence",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }
                }
            };


            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetType);
            Assert.Equal("Stock", result.ASSET[0].ASSET_DETAIL.AssetType);

            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);

        }

        [Fact]
        public void TestAssetsWhenOtherNonLiquidAssetsExists()
        {
            //Arrange  
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 555,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 555,
                BusinessUnitId = 555,
                CustomerId = 555,
                LoanOriginatorId = 555,
                LoanGoalId = 555,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=555,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=555,
                            BorrowerId=555,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=32//OtherNonLiquidAssets
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=555,
                               PropertyInfoId=555,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=555,
                                        CityId=555,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=555,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 555,
                        CityId = 555,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);


            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                    {
                    new  ASSET
                    {
                        ASSET_DETAIL= new ASSET_DETAIL{
                            AssetAccountIdentifier=null,
                            AssetCashOrMarketValueAmount=1000,
                            AssetType=null,
                            AssetTypeOtherDescription="Other Non Liquid Asset",
                            EXTENSION=null
                        },
                        ASSET_HOLDER= new ASSET_HOLDER{
                            NAME= new NAME{
                                FullName=null,
                                FirstName=null,
                                LastName=null,
                                MiddleName=null,
                                SuffixName=null
                            }
                        }
                        ,
                        SequenceNumber=1,
                        Label="ASSET_1",
                        OWNED_PROPERTY=null
                    }

                    , new ASSET

                        {
                        ASSET_DETAIL=null,
                        ASSET_HOLDER=null,
                        SequenceNumber=2,
                        Label="ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY{
                            OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL
                            {
                                OwnedPropertyLienUPBAmount=null,
                                OwnedPropertyDispositionStatusType="Retain",
                                OwnedPropertyMaintenanceExpenseAmount=null,
                                OwnedPropertySubjectIndicator=true,
                                OwnedPropertyRentalIncomeGrossAmount=0,
                                OwnedPropertyRentalIncomeNetAmount=0
                            },
                            PROPERTY= new PROPERTY{
                                ADDRESS=new List<ADDRESS>
                                {
                                     new ADDRESS
                                    {
                                        AddressLineText=string.Empty,
                                        CountryCode="US",
                                        CountryName="United States",
                                        CityName=null,
                                        PostalCode=null,
                                        StateCode="Pak",
                                        CountyName=null
                                    }
                                },
                                PROPERTY_DETAIL= new PROPERTY_DETAIL{
                                    PropertyCurrentUsageType=null,
                                    PropertyEstimatedValueAmount=null,
                                    FinancedUnitCount=null,
                                    PropertyInProjectIndicator=null,
                                    PropertyMixedUsageIndicator=null,
                                    PropertyUsageType="PrimaryResidence",
                                    AttachmentType=null,
                                    ConstructionMethodType=null,
                                    PUDIndicator=null,
                                    PropertyAcquiredDate=null,
                                    PropertyEstateType=null
                                }
                            }
                        }
                         }
                    , new ASSET
                   {
            ASSET_DETAIL=null,
            ASSET_HOLDER=null,
            SequenceNumber=3,
            Label="ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY{
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL



                {
                    OwnedPropertyLienUPBAmount=null,
                    OwnedPropertyDispositionStatusType="Retain",
                    OwnedPropertyMaintenanceExpenseAmount=null,
                    OwnedPropertySubjectIndicator=false,
                    OwnedPropertyRentalIncomeGrossAmount=0,
                    OwnedPropertyRentalIncomeNetAmount=0
                },
                PROPERTY=new PROPERTY{
                    ADDRESS= new List<ADDRESS>
                    {
                        new ADDRESS
                        {
                            AddressLineText=string.Empty,
                            CountryCode="US",
                            CountryName="United States",
                            CityName=null,
                            PostalCode="123",
                            StateCode=null,
                            CountyName=null
                        }
                    },
                    PROPERTY_DETAIL= new PROPERTY_DETAIL{
                        PropertyCurrentUsageType="PrimaryResidence",
                        PropertyEstimatedValueAmount=null,
                        FinancedUnitCount=null,
                        PropertyInProjectIndicator=null,
                        PropertyMixedUsageIndicator=null,
                        PropertyUsageType="PrimaryResidence",
                        AttachmentType=null,
                        ConstructionMethodType=null,
                        PUDIndicator=null,
                        PropertyAcquiredDate=null,
                        PropertyEstateType=null
                    }
                }
            }
        }
                       }
            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            Assert.Equal("Other Non Liquid Asset", result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

        }

        [Fact]
        public void TestAssetsWhenOtherLiquidAssetsExists()
        {
            //Arrange
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 556,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 556,
                BusinessUnitId = 556,
                CustomerId = 556,
                LoanOriginatorId = 556,
                LoanGoalId = 556,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=556,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=556,
                            BorrowerId=556,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=18 // OtherLiquidAssets
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=556,
                               PropertyInfoId=556,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=556,
                                        CityId=556,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=556,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 556,
                        CityId = 556,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);


            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                    {
                    new  ASSET
                    {
                        ASSET_DETAIL= new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= null,
                            AssetTypeOtherDescription= "OtherLiquidAsset",
                            EXTENSION= null
                        },
                        ASSET_HOLDER= new ASSET_HOLDER {
                            NAME= new NAME {
                                FullName= null,
                                FirstName= null,
                                LastName= null,
                                MiddleName= null,
                                SuffixName= null
                            }
                        },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                     , new ASSET
                    {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 2,
            Label= "ASSET_2",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= true,
                    OwnedPropertyRentalIncomeGrossAmount= 0,
                    OwnedPropertyRentalIncomeNetAmount= 0
                },
                PROPERTY=new PROPERTY {
                    ADDRESS=  new List<ADDRESS>
                    {
                        new  ADDRESS
                        {
                            AddressLineText=string.Empty ,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= null,
                            StateCode= "Pak",
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL= new PROPERTY_DETAIL {
                        PropertyCurrentUsageType= null,
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
               , new ASSET
               {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 3,
            Label= "ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= false,
                    OwnedPropertyRentalIncomeGrossAmount= 0,
                    OwnedPropertyRentalIncomeNetAmount= 0
                },
                PROPERTY= new PROPERTY {
                    ADDRESS=  new List<ADDRESS>
                    {
                         new  ADDRESS
                        {
                            AddressLineText= string.Empty,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= "123",
                            StateCode= null,
                            CountyName= null
                        }

                    },
                    PROPERTY_DETAIL= new PROPERTY_DETAIL {
                        PropertyCurrentUsageType= "PrimaryResidence",
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
               }
            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            Assert.Equal("OtherLiquidAsset", result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);

            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

        }

        [Fact]
        public void TestAssetsWhenGiftNotDepositedeExists()
        {
            //Arrange
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 557,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 557,
                BusinessUnitId = 557,
                CustomerId = 557,
                LoanOriginatorId = 557,
                LoanGoalId = 557,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=557,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=557,
                            BorrowerId=557,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=25 //GiftNotDepositede
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=557,
                               PropertyInfoId=557,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=557,
                                        CityId=557,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=557,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 557,
                        CityId = 557,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);

            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                    {
                     new  ASSET
                           {
                        ASSET_DETAIL= new ASSET_DETAIL{
                            AssetAccountIdentifier=null,
                            AssetCashOrMarketValueAmount=1000,
                            AssetType="GiftOfCash",
                            AssetTypeOtherDescription="Gift Not Deposited",
                            EXTENSION= new EXTENSION{
                                OTHER=new OTHER{
                                    GOVERNMENT_MONITORING_DETAIL_EXTENSION=null,
                                    HMDA_RACE_DESIGNATION_EXTENSION=null,
                                    GOVERNMENT_MONITORING_EXTENSION=null,
                                    EMPLOYMENT_EXTENSION=null,
                                    ASSET_DETAIL_EXTENSION= new ASSET_DETAIL_EXTENSION{
                                        IncludedInAssetAccountIndicator=false
                                    }
                                }
                            }
                        },
                        ASSET_HOLDER= new ASSET_HOLDER{
                            NAME= new NAME{
                                FullName=null,
                                FirstName=null,
                                LastName=null,
                                MiddleName=null,
                                SuffixName=null
                            }
                        },
                        SequenceNumber=1,
                        Label="ASSET_1",
                        OWNED_PROPERTY=null

                            }
                    , new ASSET
                    {
            ASSET_DETAIL=null,
            ASSET_HOLDER=null,
            SequenceNumber=2,
            Label="ASSET_2",
            OWNED_PROPERTY= new OWNED_PROPERTY{
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL{
                    OwnedPropertyLienUPBAmount=null,
                    OwnedPropertyDispositionStatusType="Retain",
                    OwnedPropertyMaintenanceExpenseAmount=null,
                    OwnedPropertySubjectIndicator=true,
                    OwnedPropertyRentalIncomeGrossAmount=0,
                    OwnedPropertyRentalIncomeNetAmount=0
                },
                PROPERTY= new PROPERTY{
                    ADDRESS= new List<ADDRESS>{
                         new ADDRESS
                        {
                            AddressLineText=string.Empty,
                            CountryCode="US",
                            CountryName="United States",
                            CityName=null,
                            PostalCode=null,
                            StateCode="Pak",
                            CountyName=null
                        }
                    },
                    PROPERTY_DETAIL= new PROPERTY_DETAIL{
                        PropertyCurrentUsageType=null,
                        PropertyEstimatedValueAmount=null,
                        FinancedUnitCount=null,
                        PropertyInProjectIndicator=null,
                        PropertyMixedUsageIndicator=null,
                        PropertyUsageType="PrimaryResidence",
                        AttachmentType=null,
                        ConstructionMethodType=null,
                        PUDIndicator=null,
                        PropertyAcquiredDate=null,
                        PropertyEstateType=null
                    }
                }
            }
        }
                    , new ASSET
                        {
            ASSET_DETAIL=null,
            ASSET_HOLDER=null,
            SequenceNumber=3,
            Label="ASSET_3",
            OWNED_PROPERTY=new OWNED_PROPERTY{
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL{
                    OwnedPropertyLienUPBAmount=null,
                    OwnedPropertyDispositionStatusType="Retain",
                    OwnedPropertyMaintenanceExpenseAmount=null,
                    OwnedPropertySubjectIndicator=false,
                    OwnedPropertyRentalIncomeGrossAmount=0,
                    OwnedPropertyRentalIncomeNetAmount=0
                },
                PROPERTY=new PROPERTY{
                    ADDRESS= new List<ADDRESS>
                    {
                         new ADDRESS
                        {
                            AddressLineText=string.Empty,
                            CountryCode="US",
                            CountryName="United States",
                            CityName=null,
                            PostalCode="123",
                            StateCode=null,
                            CountyName=null
                        }
                    },
                    PROPERTY_DETAIL= new PROPERTY_DETAIL{
                        PropertyCurrentUsageType="PrimaryResidence",
                        PropertyEstimatedValueAmount=null,
                        FinancedUnitCount=null,
                        PropertyInProjectIndicator=null,
                        PropertyMixedUsageIndicator=null,
                        PropertyUsageType="PrimaryResidence",
                        AttachmentType=null,
                        ConstructionMethodType=null,
                        PUDIndicator=null,
                        PropertyAcquiredDate=null,
                        PropertyEstateType=null
                    }
                }
            }
        }
                    }
            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            Assert.Equal("Gift Not Deposited", result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }


        [Fact]
        public void TestWhenAssetsGiftOfEquityExists()
        {
            //Arrange
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 558,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 558,
                BusinessUnitId = 558,
                CustomerId = 558,
                LoanOriginatorId = 558,
                LoanGoalId = 558,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=558,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=558,
                            BorrowerId=558,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=26 //GiftOfEquity
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=558,
                               PropertyInfoId=558,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=558,
                                        CityId=558,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=558,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 558,
                        CityId = 558,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);


            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                   {
            ASSET_DETAIL=new ASSET_DETAIL {
                AssetAccountIdentifier= null,
                AssetCashOrMarketValueAmount= 1000,
                AssetType= "GiftOfCash",
                AssetTypeOtherDescription= "Gift Of Equity",
                EXTENSION= new EXTENSION {
                    OTHER= new OTHER{
                        GOVERNMENT_MONITORING_DETAIL_EXTENSION= null,
                        HMDA_RACE_DESIGNATION_EXTENSION= null,
                        GOVERNMENT_MONITORING_EXTENSION= null,
                        EMPLOYMENT_EXTENSION= null,
                        ASSET_DETAIL_EXTENSION= new ASSET_DETAIL_EXTENSION{
                            IncludedInAssetAccountIndicator= true
                        }
                    }
                }
            },
            ASSET_HOLDER= new ASSET_HOLDER {
                NAME= new NAME {
                    FullName= null,
                    FirstName= null,
                    LastName= null,
                    MiddleName= null,
                    SuffixName= null
                }
            },
            SequenceNumber= 1,
            Label= "ASSET_1",
            OWNED_PROPERTY= null
        }
                    , new ASSET
                        {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 2,
            Label= "ASSET_2",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL{
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= true,
                    OwnedPropertyRentalIncomeGrossAmount= 0,
                    OwnedPropertyRentalIncomeNetAmount= 0
                },
                PROPERTY= new PROPERTY{
                    ADDRESS= new List<ADDRESS>{
                        new ADDRESS

                        {
                            AddressLineText= string.Empty,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= null,
                            StateCode= "Pak",
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL= new PROPERTY_DETAIL{
                        PropertyCurrentUsageType= null,
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                    , new ASSET
                    {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 3,
            Label= "ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY{
                OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= false,
                    OwnedPropertyRentalIncomeGrossAmount= 0,
                    OwnedPropertyRentalIncomeNetAmount= 0
                },
                PROPERTY= new PROPERTY{
                    ADDRESS= new List<ADDRESS>{
                         new  ADDRESS
                        {
                            AddressLineText= string.Empty,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= "123",
                            StateCode= null,
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL= new PROPERTY_DETAIL{
                        PropertyCurrentUsageType= "PrimaryResidence",
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                }
            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            Assert.Equal("Gift Of Equity", result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);

            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }


        [Fact]
        public void TestAssetsWhenSecuredBorrowedFundsNotDepositedExists()
        {
            //Arrange
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 559,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 559,
                BusinessUnitId = 559,
                CustomerId = 559,
                LoanOriginatorId = 559,
                LoanGoalId = 559,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=559,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=559,
                            BorrowerId=559,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=28//SecuredBorrowedFundsNotDeposited
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=559,
                               PropertyInfoId=559,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=559,
                                        CityId=559,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=559,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 559,
                        CityId = 559,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                  {
            ASSET_DETAIL= new ASSET_DETAIL {
                AssetAccountIdentifier= null,
                AssetCashOrMarketValueAmount= 1000,
                AssetType= null,
                AssetTypeOtherDescription= "Secured Borrowed Funds Not Deposited",
                EXTENSION= null
            },
            ASSET_HOLDER=new ASSET_HOLDER {
                NAME= new NAME{
                    FullName= null,
                    FirstName= null,
                    LastName= null,
                    MiddleName= null,
                    SuffixName= null
                }
            },
            SequenceNumber= 1,
            Label= "ASSET_1",
            OWNED_PROPERTY= null
        }
                    , new ASSET
                        {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 2,
            Label= "ASSET_2",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL{
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= true,
                    OwnedPropertyRentalIncomeGrossAmount= 0,
                    OwnedPropertyRentalIncomeNetAmount= 0
                },
                PROPERTY= new PROPERTY{
                    ADDRESS= new List<ADDRESS>{
                        new ADDRESS

                        {
                            AddressLineText= string.Empty,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= null,
                            StateCode= "Pak",
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL= new PROPERTY_DETAIL{
                        PropertyCurrentUsageType= null,
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                    , new ASSET
                    {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 3,
            Label= "ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY{
                OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= false,
                    OwnedPropertyRentalIncomeGrossAmount= 0,
                    OwnedPropertyRentalIncomeNetAmount= 0
                },
                PROPERTY= new PROPERTY{
                    ADDRESS= new List<ADDRESS>{
                         new  ADDRESS
                        {
                            AddressLineText= string.Empty,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= "123",
                            StateCode= null,
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL= new PROPERTY_DETAIL{
                        PropertyCurrentUsageType= "PrimaryResidence",
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                }
            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            Assert.Equal("Secured Borrowed Funds Not Deposited", result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
            
        }

        [Fact]
        public void TestAssetsWhenCashDepositOnSalesContractExists()
        {
            //Arrange
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 560,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 560,
                BusinessUnitId = 560,
                CustomerId = 560,
                LoanOriginatorId = 560,
                LoanGoalId = 560,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=560,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=560,
                            BorrowerId=560,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=29//Cash Deposit On Sales Contract
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=560,
                               PropertyInfoId=560,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=560,
                                        CityId=560,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=560,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 560,
                        CityId = 560,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                    {
                    ASSET_DETAIL= new ASSET_DETAIL{
                    AssetAccountIdentifier=null,
                    AssetCashOrMarketValueAmount=1000,
                    AssetType=null,
                    AssetTypeOtherDescription="Cash Deposit On Sales Contract",
                    EXTENSION=null
                    },
                    ASSET_HOLDER= new ASSET_HOLDER{
                        NAME=new NAME{
                            FullName=null,
                            FirstName=null,
                            LastName=null,
                            MiddleName=null,
                            SuffixName=null
                        }
                    },
                    SequenceNumber=1,
                    Label="ASSET_1",
                    OWNED_PROPERTY=null
                 }
                   ,new ASSET
                                        {
                            ASSET_DETAIL=null,
                            ASSET_HOLDER=null,
                            SequenceNumber=2,
                            Label="ASSET_2",
                            OWNED_PROPERTY=new OWNED_PROPERTY{
                                OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                    OwnedPropertyLienUPBAmount=null,
                                    OwnedPropertyDispositionStatusType="Retain",
                                    OwnedPropertyMaintenanceExpenseAmount=null,
                                    OwnedPropertySubjectIndicator=true,
                                    OwnedPropertyRentalIncomeGrossAmount=0,
                                    OwnedPropertyRentalIncomeNetAmount=0
                                },
                                PROPERTY= new PROPERTY{
                                    ADDRESS= new List<ADDRESS>

                                        {
                                        new ADDRESS{
                                            AddressLineText=string.Empty,
                                            CountryCode="US",
                                            CountryName="United States",
                                            CityName=null,
                                            PostalCode=null,
                                            StateCode="Pak",
                                            CountyName=null
                                        }
                                    }
                                    ,
                                    PROPERTY_DETAIL= new PROPERTY_DETAIL{
                                        PropertyCurrentUsageType=null,
                                        PropertyEstimatedValueAmount=null,
                                        FinancedUnitCount=null,
                                        PropertyInProjectIndicator=null,
                                        PropertyMixedUsageIndicator=null,
                                        PropertyUsageType="PrimaryResidence",
                                        AttachmentType=null,
                                        ConstructionMethodType=null,
                                        PUDIndicator=null,
                                        PropertyAcquiredDate=null,
                                        PropertyEstateType=null
                                    }
                                }
                            }
                        }
                   , new ASSET
                   {
            ASSET_DETAIL=null,
            ASSET_HOLDER=null,
            SequenceNumber=3,
            Label="ASSET_3",
            OWNED_PROPERTY=new OWNED_PROPERTY{
                OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL{
                    OwnedPropertyLienUPBAmount=null,
                    OwnedPropertyDispositionStatusType="Retain",
                    OwnedPropertyMaintenanceExpenseAmount=null,
                    OwnedPropertySubjectIndicator=false,
                    OwnedPropertyRentalIncomeGrossAmount=0,
                    OwnedPropertyRentalIncomeNetAmount=0
                },
                PROPERTY= new PROPERTY{
                    ADDRESS=new List<ADDRESS>

                        {
                         new ADDRESS{
                            AddressLineText=string.Empty,
                            CountryCode="US",
                            CountryName="United States",
                            CityName=null,
                            PostalCode="123",
                            StateCode=null,
                            CountyName=null
                        }

                    },
                    PROPERTY_DETAIL=new PROPERTY_DETAIL{
                        PropertyCurrentUsageType="PrimaryResidence",
                        PropertyEstimatedValueAmount=null,
                        FinancedUnitCount=null,
                        PropertyInProjectIndicator=null,
                        PropertyMixedUsageIndicator=null,
                        PropertyUsageType="PrimaryResidence",
                        AttachmentType=null,
                        ConstructionMethodType=null,
                        PUDIndicator=null,
                        PropertyAcquiredDate=null,
                        PropertyEstateType=null
                    }
                }
            }
        }
            }
            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            Assert.Equal("Cash Deposit On Sales Contract", result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);


        }

        [Fact]
        public void TestAssetsWhenGiftsTotalExists()
        {
            //Arrange
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 561,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 561,
                BusinessUnitId = 561,
                CustomerId = 561,
                LoanOriginatorId = 561,
                LoanGoalId = 561,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=561,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=561,
                            BorrowerId=561,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=24 //Gifts Total
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=561,
                               PropertyInfoId=561,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=561,
                                        CityId=561,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=561,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 561,
                        CityId = 561,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                   {
            ASSET_DETAIL=new ASSET_DETAIL{
                AssetAccountIdentifier=null,
                AssetCashOrMarketValueAmount=1000,
                AssetType=null,
                AssetTypeOtherDescription="Gifts Total",
                EXTENSION=null
            },
                    ASSET_HOLDER=new ASSET_HOLDER{
                        NAME= new NAME{
                            FullName=null,
                            FirstName=null,
                            LastName=null,
                            MiddleName=null,
                            SuffixName=null
                        }
                    },
                    SequenceNumber=1,
                    Label="ASSET_1",
                    OWNED_PROPERTY=null
                }
                    , new ASSET
                        {
            ASSET_DETAIL=null,
            ASSET_HOLDER=null,
            SequenceNumber=2,
            Label="ASSET_2",
            OWNED_PROPERTY=new OWNED_PROPERTY{
                OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL{
                    OwnedPropertyLienUPBAmount=null,
                    OwnedPropertyDispositionStatusType="Retain",
                    OwnedPropertyMaintenanceExpenseAmount=null,
                    OwnedPropertySubjectIndicator=true,
                    OwnedPropertyRentalIncomeGrossAmount=0,
                    OwnedPropertyRentalIncomeNetAmount=0
                },
                PROPERTY=new PROPERTY{
                    ADDRESS= new List<ADDRESS>

                        {
                        new  ADDRESS{
                            AddressLineText=string.Empty,
                            CountryCode="US",
                            CountryName="United States",
                            CityName=null,
                            PostalCode=null,
                            StateCode="Pak",
                            CountyName=null
                        }
                    },
                    PROPERTY_DETAIL=new PROPERTY_DETAIL{
                        PropertyCurrentUsageType=null,
                        PropertyEstimatedValueAmount=null,
                        FinancedUnitCount=null,
                        PropertyInProjectIndicator=null,
                        PropertyMixedUsageIndicator=null,
                        PropertyUsageType="PrimaryResidence",
                        AttachmentType=null,
                        ConstructionMethodType=null,
                        PUDIndicator=null,
                        PropertyAcquiredDate=null,
                        PropertyEstateType=null
                    }
                }
            }
        }

                    ,new ASSET
                    {
            ASSET_DETAIL=null,
            ASSET_HOLDER=null,
            SequenceNumber=3,
            Label="ASSET_3",
            OWNED_PROPERTY=new OWNED_PROPERTY{
                OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL{
                    OwnedPropertyLienUPBAmount=null,
                    OwnedPropertyDispositionStatusType="Retain",
                    OwnedPropertyMaintenanceExpenseAmount=null,
                    OwnedPropertySubjectIndicator=false,
                    OwnedPropertyRentalIncomeGrossAmount=0,
                    OwnedPropertyRentalIncomeNetAmount=0
                },
                PROPERTY= new PROPERTY{
                    ADDRESS= new List<ADDRESS>

                        {
                         new ADDRESS{
                            AddressLineText=string.Empty,
                            CountryCode="US",
                            CountryName="United States",
                            CityName=null,
                            PostalCode= "123",
                            StateCode=null,
                            CountyName=null
                        }
                    }
                    ,
                    PROPERTY_DETAIL=new PROPERTY_DETAIL{
                        PropertyCurrentUsageType="PrimaryResidence",
                        PropertyEstimatedValueAmount=null,
                        FinancedUnitCount=null,
                        PropertyInProjectIndicator=null,
                        PropertyMixedUsageIndicator=null,
                        PropertyUsageType="PrimaryResidence",
                        AttachmentType=null,
                        ConstructionMethodType=null,
                        PUDIndicator=null,
                        PropertyAcquiredDate=null,
                        PropertyEstateType=null
                    }
                }
            }
        }
            }
            };
            //Assert
            Assert.NotNull(result);

            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);
            Assert.Equal("Gifts Total", result.ASSET[0].ASSET_DETAIL.AssetTypeOtherDescription);

            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestAssetsDefault()
        {
            //Arrange
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 562,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 562,
                BusinessUnitId = 562,
                CustomerId = 562,
                LoanOriginatorId = 562,
                LoanGoalId = 562,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=562,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=562,
                            BorrowerId=562,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=0
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=562,
                               PropertyInfoId=562,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=562,
                                        CityId=562,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=562,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 562,
                        CityId = 562,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                               {
                        ASSET_DETAIL=new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= "Other",
                            AssetTypeOtherDescription= null,
                            EXTENSION= null
                        },
                    ASSET_HOLDER=new ASSET_HOLDER {
                        NAME=new NAME {
                            FullName= null,
                            FirstName= null,
                            LastName= null,
                            MiddleName= null,
                            SuffixName= null
                        }
                    },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                    , new ASSET
                    {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY
                        {
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "Retain",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= true,
                                OwnedPropertyRentalIncomeGrossAmount= 0,
                                OwnedPropertyRentalIncomeNetAmount= 0
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS= new List<ADDRESS>
                                {
                                     new ADDRESS
                                    {

                                        AddressLineText= string.Empty,
                                        CountryCode="US",
                                        CountryName= "United States",
                                        CityName= null,
                                        PostalCode= null,
                                        StateCode= "Pak",
                                        CountyName= null
                                    }
                                },
                                PROPERTY_DETAIL=new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= null,
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "PrimaryResidence",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }
                   ,new ASSET
                    {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 3,
            Label= "ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= false,
                    OwnedPropertyRentalIncomeGrossAmount= 0,
                    OwnedPropertyRentalIncomeNetAmount= 0
                },
                PROPERTY= new PROPERTY {
                    ADDRESS=  new List<ADDRESS>
                    {
                         new ADDRESS
                        {
                            AddressLineText=string.Empty ,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= "123",
                            StateCode= null,
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL=new PROPERTY_DETAIL {
                        PropertyCurrentUsageType= "PrimaryResidence",
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                }


            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetType);
            Assert.Equal("Other", result.ASSET[0].ASSET_DETAIL.AssetType);
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestAssetsWhenDefaultAccountTypeIdIsNotZero()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 563,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 563,
                BusinessUnitId = 563,
                CustomerId = 563,
                LoanOriginatorId = 563,
                LoanGoalId = 563,
                LoanPurposeId = 2,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=563,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=563,
                            BorrowerId=563,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=563,
                               PropertyInfoId=563,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=563,
                                        CityId=563,
                                        ZipCode = "123"
                                    }
                                    ,PropertyUsageId=1
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=563,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 563,
                        CityId = 563,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                               {
                        ASSET_DETAIL=new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= "EarnestMoneyCashDepositTowardPurchase",
                            AssetTypeOtherDescription= null,
                            EXTENSION= null
                        },
                    ASSET_HOLDER=new ASSET_HOLDER {
                        NAME=new NAME {
                            FullName= null,
                            FirstName= null,
                            LastName= null,
                            MiddleName= null,
                            SuffixName= null
                        }
                    },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                    , new ASSET
                    {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY
                        {
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "Retain",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= true,
                                OwnedPropertyRentalIncomeGrossAmount= 0,
                                OwnedPropertyRentalIncomeNetAmount= 0
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS= new List<ADDRESS>
                                {
                                     new ADDRESS
                                    {

                                        AddressLineText= string.Empty,
                                        CountryCode="US",
                                        CountryName= "United States",
                                        CityName= null,
                                        PostalCode= null,
                                        StateCode= "Pak",
                                        CountyName= null
                                    }
                                },
                                PROPERTY_DETAIL=new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= null,
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "PrimaryResidence",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }
                   ,new ASSET
                    {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 3,
            Label= "ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= false,
                    OwnedPropertyRentalIncomeGrossAmount= 0,
                    OwnedPropertyRentalIncomeNetAmount= 0
                },
                PROPERTY= new PROPERTY {
                    ADDRESS=  new List<ADDRESS>
                    {
                         new ADDRESS
                        {
                            AddressLineText=string.Empty ,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= "123",
                            StateCode= null,
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL=new PROPERTY_DETAIL {
                        PropertyCurrentUsageType= "PrimaryResidence",
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                }


            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL);
            Assert.NotNull(result.ASSET[0].ASSET_DETAIL.AssetType);
            Assert.Equal("EarnestMoneyCashDepositTowardPurchase", result.ASSET[0].ASSET_DETAIL.AssetType);
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestAssetsWhenLoanPurposeIdThreeExists()//discuss
        {
            var service = new MismoConverter34();

            LoanApplication loanApplication = new LoanApplication
            {
                Id = 564,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 564,
                BusinessUnitId = 564,
                CustomerId = 564,
                LoanOriginatorId = 564,
                LoanGoalId = 564,
                LoanPurposeId = 3,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=564,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=564,
                            BorrowerId=564,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                         BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=564,
                               PropertyInfoId=564,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                   {
                               Id=564,
                              CityId=564,
                              ZipCode="12"
                             , CityName="asdad"
                              ,CountryName="sadasd"
                             ,State= new State
                             {
                                 Id=564,
                                 Name="a",
                                 Abbreviation="asdads"
                             }
                               ,Country= new Country
                             {
                                 Id=564,
                                 Name="b",
                                 TwoLetterIsoCode="334"
                             },
                              StreetAddress= "sadadsad"
                              ,UnitNo="12"


                           }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=564,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 564,
                        CityId = 564,
                        ZipCode = "123",
                        CityName = "asdad",
                        CountryName = "sadasd",

                        State = new State
                        {
                            Id = 564,
                            Name = "a"
                            ,
                            Abbreviation = "Pak"
                        },
                        Country = new Country
                        {
                            Id = 564,
                            Name = "b",
                            TwoLetterIsoCode = "334"
                        },
                        StreetAddress = "sadadsad",
                        UnitNo = "12"


                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                               {
                        ASSET_DETAIL=new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= "EarnestMoneyCashDepositTowardPurchase",
                            AssetTypeOtherDescription= null,
                            EXTENSION= null
                        },
                    ASSET_HOLDER=new ASSET_HOLDER {
                        NAME=new NAME {
                            FullName= null,
                            FirstName= null,
                            LastName= null,
                            MiddleName= null,
                            SuffixName= null
                        }
                    },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                    , new ASSET
                    {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY
                        {
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "Retain",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= true,
                                OwnedPropertyRentalIncomeGrossAmount= 0,
                                OwnedPropertyRentalIncomeNetAmount= 0
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS= new List<ADDRESS>
                                {
                                     new ADDRESS
                                    {

                                        AddressLineText= "sadadsad #12",
                                        CountryCode="334",
                                        CountryName= "b",
                                        CityName= "asdad",
                                        PostalCode= "123",
                                        StateCode= "Pak",
                                        CountyName= null
                                    }
                                },
                                PROPERTY_DETAIL=new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= null,
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "PrimaryResidence",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }
                   ,new ASSET
                    {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 3,
            Label= "ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= false,
                    OwnedPropertyRentalIncomeGrossAmount= null,
                    OwnedPropertyRentalIncomeNetAmount= null
                },
                PROPERTY= new PROPERTY {
                    ADDRESS=  new List<ADDRESS>
                    {
                         new ADDRESS
                        {
                             AddressLineText= "sadadsad #12" ,
                            CountryCode= "334",
                            CountryName= "b",
                            CityName= "asdad",
                            PostalCode= "12",
                            StateCode= "asdads",
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL=new PROPERTY_DETAIL {
                        PropertyCurrentUsageType= "PrimaryResidence",
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                }


            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyUsageType);
            Assert.Equal("PrimaryResidence", result.ASSET[1].OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyUsageType);
            var actualJson = JsonConvert.SerializeObject(result.ASSET[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.ASSET[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.ASSET[1]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[1]);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.ASSET[2]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[2]);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestAssetsAddressIsNull()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 565,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 565,
                BusinessUnitId = 565,
                CustomerId = 565,
                LoanOriginatorId = 565,
                LoanGoalId = 565,
                LoanPurposeId = 3,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=565,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=565,
                            BorrowerId=565,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                         BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=565,
                               PropertyInfoId=565,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                   {
                               Id=565,
                              CityId=565,
                              ZipCode="12"
                             , CityName="asdad"
                             ,State= new State
                             {
                                 Id=565,
                                 Name="a"
                             }
                               ,Country= new Country
                             {
                                 Id=565,
                                 Name="b",
                                 TwoLetterIsoCode="334"
                             },
                              StreetAddress= "sadadsad"
                              ,UnitNo="12"


                           }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=565,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = null,
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                               {
                        ASSET_DETAIL=new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= "EarnestMoneyCashDepositTowardPurchase",
                            AssetTypeOtherDescription= null,
                            EXTENSION= null
                        },
                    ASSET_HOLDER=new ASSET_HOLDER {
                        NAME=new NAME {
                            FullName= null,
                            FirstName= null,
                            LastName= null,
                            MiddleName= null,
                            SuffixName= null
                        }
                    },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                    , new ASSET
                    {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY
                        {
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "Retain",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= true,
                                OwnedPropertyRentalIncomeGrossAmount= 0,
                                OwnedPropertyRentalIncomeNetAmount= 0
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS=   new List<ADDRESS>
                                {
                                   null
                                 },
                                PROPERTY_DETAIL=new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= null,
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "PrimaryResidence",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }
                   ,new ASSET
                    {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 3,
            Label= "ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= false,
                    OwnedPropertyRentalIncomeGrossAmount= null,
                    OwnedPropertyRentalIncomeNetAmount= null
                },
                PROPERTY= new PROPERTY {
                    ADDRESS=  new List<ADDRESS>
                    {
                         new ADDRESS
                        {
                             AddressLineText= "sadadsad #12" ,
                            CountryCode= "334",
                            CountryName= "b",
                            CityName= "asdad",
                            PostalCode= "12",
                            StateCode=null,
                            CountyName= null,
                        }
                    },
                    PROPERTY_DETAIL=new PROPERTY_DETAIL {
                        PropertyCurrentUsageType= "PrimaryResidence",
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                }


            };
            //Assert
            Assert.NotNull(result);


            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY);
            Assert.Null(result.ASSET[1].OWNED_PROPERTY.PROPERTY.ADDRESS[0]);

            var actualJson = JsonConvert.SerializeObject(result.ASSET[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.ASSET[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.ASSET[1]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[1]);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.ASSET[2]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[2]);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestAssetsWhenPropertyUsageIdIsNotExist()// mostly place use is not exist means not match 
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 566,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 566,
                BusinessUnitId = 566,
                CustomerId = 566,
                LoanOriginatorId = 566,
                LoanGoalId = 566,
                LoanPurposeId = 3,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=566,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=566,
                            BorrowerId=566,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                         BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=566,
                               PropertyInfoId=566,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                   {
                               Id=566,
                              CityId=566,
                              ZipCode="123"
                             , CityName="asdad"
                             ,State= new State
                             {
                                 Id=566,
                                 Name="a"
                             }
                               ,Country= new Country
                             {
                                 Id=566,
                                 Name="b",
                                 TwoLetterIsoCode="334"
                             },
                              StreetAddress= "sadadsad"
                              ,UnitNo="12"


                           }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=566,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 566,
                        CityId = 566,
                        ZipCode = "1234"
                             ,
                        CityName = "asdad"
                             ,
                        State = new State
                        {
                            Id = 566,
                            Name = "a"
                        }
                               ,
                        Country = new Country
                        {
                            Id = 566,
                            Name = "b",
                            TwoLetterIsoCode = "334"
                        },
                        StreetAddress = "sadadsad"
                              ,
                        UnitNo = "12"


                    },
                    PropertyUsageId = 102
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                               {
                        ASSET_DETAIL=new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= "EarnestMoneyCashDepositTowardPurchase",
                            AssetTypeOtherDescription= null,
                            EXTENSION= null
                        },
                    ASSET_HOLDER=new ASSET_HOLDER {
                        NAME=new NAME {
                            FullName= null,
                            FirstName= null,
                            LastName= null,
                            MiddleName= null,
                            SuffixName= null
                        }
                    },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                    , new ASSET
                    {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY
                        {
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "Retain",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= true,
                                OwnedPropertyRentalIncomeGrossAmount= 0,
                                OwnedPropertyRentalIncomeNetAmount= 0
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS=   new List<ADDRESS>
                                {
                                   new ADDRESS
                                   {
                                        AddressLineText= "sadadsad #12",
                                        CountryCode="334",
                                        CountryName= "b",
                                        CityName= "asdad",
                                        PostalCode= "1234",
                                        StateCode= null,
                                        CountyName= null

                                  }
                                 },
                                PROPERTY_DETAIL=new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= null,
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "Other",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }
                   ,new ASSET
                    {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 3,
            Label= "ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= false,
                    OwnedPropertyRentalIncomeGrossAmount= null,
                    OwnedPropertyRentalIncomeNetAmount= null
                },
                PROPERTY= new PROPERTY {
                    ADDRESS=  new List<ADDRESS>
                    {
                         new ADDRESS
                        {
                             AddressLineText= "sadadsad #12" ,
                            CountryCode= "334",
                            CountryName= "b",
                            CityName= "asdad",
                            PostalCode= "123",
                            StateCode=null,
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL=new PROPERTY_DETAIL {
                        PropertyCurrentUsageType= "PrimaryResidence",
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                }


            };
            //Assert
            Assert.NotNull(result);

            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.PROPERTY);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyUsageType);

            Assert.Equal("Other", result.ASSET[1].OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyUsageType);
            var actualJson = JsonConvert.SerializeObject(result.ASSET[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.ASSET[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.ASSET[1]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[1]);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.ASSET[2]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[2]);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestAssetsWhenPropertyStatusIsNotRental()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 567,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 567,
                BusinessUnitId = 567,
                CustomerId = 567,
                LoanOriginatorId = 567,
                LoanGoalId = 567,
                LoanPurposeId = 3,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=567,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=567,
                            BorrowerId=567,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                         BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=567,
                               PropertyInfoId=567,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                   {
                               Id=5671,
                              CityId=567,
                              ZipCode="12"
                             , CityName="asdad"
                             ,State= new State
                             {
                                 Id=567,
                                 Name="a"
                             }
                               ,Country= new Country
                             {
                                 Id=567,
                                 Name="b",
                                 TwoLetterIsoCode="334"
                             },
                              StreetAddress= "sadadsad"
                              ,UnitNo="12"


                           }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="Rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=567,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 567,
                        CityId = 567,
                        ZipCode = "1234"
                             ,
                        CityName = "asdad"
                             ,
                        State = new State
                        {
                            Id = 567,
                            Name = "a"
                        }
                               ,
                        Country = new Country
                        {
                            Id = 567,
                            Name = "b",
                            TwoLetterIsoCode = "334"
                        },
                        StreetAddress = "sadadsad"
                              ,
                        UnitNo = "12"


                    },
                    PropertyUsageId = 102
                },



            };

            //Act
            var result = service.GetAssets(loanApplication);
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                               {
                        ASSET_DETAIL=new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= "EarnestMoneyCashDepositTowardPurchase",
                            AssetTypeOtherDescription= null,
                            EXTENSION= null
                        },
                    ASSET_HOLDER=new ASSET_HOLDER {
                        NAME=new NAME {
                            FullName= null,
                            FirstName= null,
                            LastName= null,
                            MiddleName= null,
                            SuffixName= null
                        }
                    },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                    , new ASSET
                    {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY
                        {
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "Retain",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= true,
                                OwnedPropertyRentalIncomeGrossAmount= 0,
                                OwnedPropertyRentalIncomeNetAmount= 0
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS=   new List<ADDRESS>
                                {
                                     new ADDRESS{
                                    AddressLineText= "sadadsad #12",
                                    CountryCode= "334",
                                    CountryName= "b",
                                    CityName= "asdad",
                                    PostalCode= "1234",
                                    StateCode=null,
                                    CountyName= null
                                     }
                                 },
                                PROPERTY_DETAIL=new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= null,
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "Other",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }
                   ,new ASSET
                    {
            ASSET_DETAIL= null,
            ASSET_HOLDER= null,
            SequenceNumber= 3,
            Label= "ASSET_3",
            OWNED_PROPERTY= new OWNED_PROPERTY {
                OWNED_PROPERTY_DETAIL= new OWNED_PROPERTY_DETAIL {
                    OwnedPropertyLienUPBAmount= null,
                    OwnedPropertyDispositionStatusType= "Retain",
                    OwnedPropertyMaintenanceExpenseAmount= null,
                    OwnedPropertySubjectIndicator= false,
                    OwnedPropertyRentalIncomeGrossAmount= null,
                    OwnedPropertyRentalIncomeNetAmount= null
                },
                PROPERTY= new PROPERTY {
                    ADDRESS=  new List<ADDRESS>
                    {
                         new ADDRESS
                        {
                             AddressLineText= "sadadsad #12" ,
                            CountryCode= "334",
                            CountryName= "b",
                            CityName= "asdad",
                            PostalCode= "12",
                            StateCode=null,
                            CountyName= null
                        }
                    },
                    PROPERTY_DETAIL=new PROPERTY_DETAIL {
                        PropertyCurrentUsageType= "PrimaryResidence",
                        PropertyEstimatedValueAmount= null,
                        FinancedUnitCount= null,
                        PropertyInProjectIndicator= null,
                        PropertyMixedUsageIndicator= null,
                        PropertyUsageType= "PrimaryResidence",
                        AttachmentType= null,
                        ConstructionMethodType= null,
                        PUDIndicator= null,
                        PropertyAcquiredDate= null,
                        PropertyEstateType= null
                    }
                }
            }
        }
                }


            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[2].OWNED_PROPERTY);
            Assert.NotNull(result.ASSET[2].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL);
            Assert.NotNull(result.ASSET[2].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType);

            Assert.Equal("Retain", result.ASSET[2].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType);
            Assert.False(result.ASSET[2].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertySubjectIndicator);
            var actualJson = JsonConvert.SerializeObject(result.ASSET[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.ASSET[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.ASSET[1]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[1]);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.ASSET[2]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[2]);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestAssetsWhenPropertyInfoAndAddressInfoIsNull()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 568,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 568,
                BusinessUnitId = 568,
                CustomerId = 568,
                LoanOriginatorId = 568,
                LoanGoalId = 568,
                LoanPurposeId = 3,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=568,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=568,
                            BorrowerId=568,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                         BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=568,
                               PropertyInfoId=568,
                                PropertyInfo = null

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=568,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = null,
                    PropertyUsageId = 102
                },



            };
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                               {
                        ASSET_DETAIL=new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= "EarnestMoneyCashDepositTowardPurchase",
                            AssetTypeOtherDescription= null,
                            EXTENSION= null
                        },
                    ASSET_HOLDER=new ASSET_HOLDER {
                        NAME=new NAME {
                            FullName= null,
                            FirstName= null,
                            LastName= null,
                            MiddleName= null,
                            SuffixName= null
                        }
                    },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                    , new ASSET
                    {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY
                        {
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "Retain",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= true,
                                OwnedPropertyRentalIncomeGrossAmount= 0,
                                OwnedPropertyRentalIncomeNetAmount= 0
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS=   new List<ADDRESS>
                                {
                                   null
                                 },
                                PROPERTY_DETAIL=new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= null,
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "Other",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }

                }


            };
            //Act
            var result = service.GetAssets(loanApplication);
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.PROPERTY);
            Assert.Null(result.ASSET[1].OWNED_PROPERTY.PROPERTY.ADDRESS[0]);

            var actualJson = JsonConvert.SerializeObject(result.ASSET[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.ASSET[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.ASSET[1]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[1]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestAssetsWhenPropertyStatusIsEmpty()
        {
            //Arrange
            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 569,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 569,
                BusinessUnitId = 569,
                CustomerId = 569,
                LoanOriginatorId = 569,
                LoanGoalId = 569,
                LoanPurposeId = 100,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=569,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=569,
                            BorrowerId=569,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=569,
                               PropertyInfoId=569,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=569,
                                        CityId=569
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus=""
                                    ,IntentToSellPriorToPurchase= true
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=5691,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 569,
                        CityId = 569,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 569,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "569"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = ""
                   ,
                    IntentToSellPriorToPurchase = true
                },



            };
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                               {
                        ASSET_DETAIL=new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= "EarnestMoneyCashDepositTowardPurchase",
                            AssetTypeOtherDescription= null,
                            EXTENSION= null
                        },
                    ASSET_HOLDER=new ASSET_HOLDER {
                        NAME=new NAME {
                            FullName= null,
                            FirstName= null,
                            LastName= null,
                            MiddleName= null,
                            SuffixName= null
                        }
                    },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                    , new ASSET
                    {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY
                        {
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "PendingSale",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= false,
                                OwnedPropertyRentalIncomeGrossAmount= null,
                                OwnedPropertyRentalIncomeNetAmount=null
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS=   new List<ADDRESS>
                                {
                                         new ADDRESS
                        {
                             AddressLineText= string.Empty,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= null,
                            StateCode=null,
                            CountyName= null
                        }
                                 },
                                PROPERTY_DETAIL=new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= "PrimaryResidence",
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "PrimaryResidence",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }

                }


            };
            //Act
            var result = service.GetAssets(loanApplication);
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType);

            Assert.Equal("PendingSale", result.ASSET[1].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType);

            var actualJson = JsonConvert.SerializeObject(result.ASSET[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.ASSET[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.ASSET[1]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[1]);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestAssetsWhenPropertyStatusIsNotEmpty()
        {
            //Arrange


            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 570,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 570,
                BusinessUnitId = 570,
                CustomerId = 570,
                LoanOriginatorId = 570,
                LoanGoalId = 570,
                LoanPurposeId = 100,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=570,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=570,
                            BorrowerId=570,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=570,
                               PropertyInfoId=570,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=570,
                                        CityId=570
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="sad"
                                    ,IntentToSellPriorToPurchase= true
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=570,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,
                    AddressInfo = new AddressInfo
                    {
                        Id = 570,
                        CityId = 570,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 570,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "570"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "asdad"
                   ,
                    IntentToSellPriorToPurchase = true
                },



            };
            var expected = new ASSETS
            {

                ASSET = new List<ASSET>
                {
                    new ASSET
                               {
                        ASSET_DETAIL=new ASSET_DETAIL {
                            AssetAccountIdentifier= null,
                            AssetCashOrMarketValueAmount= 1000,
                            AssetType= "EarnestMoneyCashDepositTowardPurchase",
                            AssetTypeOtherDescription= null,
                            EXTENSION= null
                        },
                    ASSET_HOLDER=new ASSET_HOLDER {
                        NAME=new NAME {
                            FullName= null,
                            FirstName= null,
                            LastName= null,
                            MiddleName= null,
                            SuffixName= null
                        }
                    },
                        SequenceNumber= 1,
                        Label= "ASSET_1",
                        OWNED_PROPERTY= null
                     }
                    , new ASSET
                    {
                        ASSET_DETAIL= null,
                        ASSET_HOLDER= null,
                        SequenceNumber= 2,
                        Label= "ASSET_2",
                        OWNED_PROPERTY= new OWNED_PROPERTY
                        {
                            OWNED_PROPERTY_DETAIL=new OWNED_PROPERTY_DETAIL {
                                OwnedPropertyLienUPBAmount= null,
                                OwnedPropertyDispositionStatusType= "sad",
                                OwnedPropertyMaintenanceExpenseAmount= null,
                                OwnedPropertySubjectIndicator= false,
                                OwnedPropertyRentalIncomeGrossAmount= null,
                                OwnedPropertyRentalIncomeNetAmount= null
                            },
                            PROPERTY= new PROPERTY {
                                ADDRESS=   new List<ADDRESS>
                                {
                                   new ADDRESS
                                   {
                                       AddressLineText= string.Empty,
                            CountryCode= "US",
                            CountryName= "United States",
                            CityName= null,
                            PostalCode= null,
                            StateCode=null,
                            CountyName= null
                                   }
                                 },
                                PROPERTY_DETAIL=new PROPERTY_DETAIL {
                                    PropertyCurrentUsageType= "PrimaryResidence",
                                    PropertyEstimatedValueAmount= null,
                                    FinancedUnitCount= null,
                                    PropertyInProjectIndicator= null,
                                    PropertyMixedUsageIndicator= null,
                                    PropertyUsageType= "PrimaryResidence",
                                    AttachmentType= null,
                                    ConstructionMethodType= null,
                                    PUDIndicator= null,
                                    PropertyAcquiredDate= null,
                                    PropertyEstateType= null
                                }
                            }
                        }
                    }

                }


            };
            //Act
            var result = service.GetAssets(loanApplication);
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ASSET);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL);
            Assert.NotNull(result.ASSET[1].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType);
            Assert.Equal("sad", result.ASSET[1].OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType);
            var actualJson = JsonConvert.SerializeObject(result.ASSET[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.ASSET[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.ASSET[1]);
            expectedJson = JsonConvert.SerializeObject(expected.ASSET[1]);
            Assert.Equal(expectedJson, actualJson);


        }

        [Fact]
        public void TestSubjectPropertyWhenThreeUnitBuildingExists()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 571,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 571,
                BusinessUnitId = 571,
                CustomerId = 571,
                LoanOriginatorId = 571,
                LoanGoalId = 571,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=571,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=571,
                            BorrowerId=571,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=571,
                               PropertyInfoId=571,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 5,//ThreeUnitBuilding
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=571,
                                        CityId=571
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=571,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 5,//ThreeUnitBuilding
                    AddressInfo = new AddressInfo
                    {
                        Id = 571,
                        CityId = 571,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 571,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "571"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };
            //Act
            var result = service.GetSubjectProperty(loanApplication);
            DateTime? dt = result.PROPERTY_DETAIL.PropertyAcquiredDate;
            var expected = new SUBJECT_PROPERTY
            {


                ADDRESS = new ADDRESS
                {
                    AddressLineText = "asdad #571",
                    CountryCode = null,
                    CountryName = null,
                    CityName = "abc",
                    PostalCode = "abc",
                    StateCode = "Pak",
                    CountyName = "abc"
                }
          ,
                PROJECT = new PROJECT
                {
                    PROJECT_DETAIL = new PROJECT_DETAIL
                    {
                        ProjectLegalStructureType = null
                    }
                }
            ,
                PROPERTY_DETAIL = new PROPERTY_DETAIL
                {
                    PropertyCurrentUsageType = "PrimaryResidence",
                    PropertyEstimatedValueAmount = null,
                    FinancedUnitCount = 3,
                    PropertyInProjectIndicator = null,
                    PropertyMixedUsageIndicator = null,
                    PropertyUsageType = "PrimaryResidence",
                    AttachmentType = null,
                    ConstructionMethodType = null,
                    PUDIndicator = null,
                    PropertyAcquiredDate = dt,
                    PropertyEstateType = "FeeSimple"
                }
            ,
                SALES_CONTRACTS = new SALES_CONTRACTS
                {
                    SALES_CONTRACT = new SALES_CONTRACT
                    {
                        SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL
                        {
                            SalesContractAmount = null
                        },
                        SequenceNumber = null
                    }
                }



            };

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.PROPERTY_DETAIL);
            Assert.NotNull(result.PROPERTY_DETAIL.FinancedUnitCount);

            Assert.Equal(3, result.PROPERTY_DETAIL.FinancedUnitCount);
            var actualJson = JsonConvert.SerializeObject(result.ADDRESS);
            var expectedJson = JsonConvert.SerializeObject(expected.ADDRESS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.PROJECT);
            expectedJson = JsonConvert.SerializeObject(expected.PROJECT);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.PROPERTY_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected.PROPERTY_DETAIL);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestSubjectPropertyWhenFourUnitBuildingExists()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 572,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 572,
                BusinessUnitId = 572,
                CustomerId = 572,
                LoanOriginatorId = 572,
                LoanGoalId = 572,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=572,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=572,
                            BorrowerId=572,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=572,
                               PropertyInfoId=572,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 6,//FourUnitBuilding
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=572,
                                        CityId=572
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=572,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 6,//FourUnitBuilding
                    AddressInfo = new AddressInfo
                    {
                        Id = 572,
                        CityId = 572,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 572,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "572"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.GetSubjectProperty(loanApplication);
            DateTime? dt = result.PROPERTY_DETAIL.PropertyAcquiredDate;
            var expected = new SUBJECT_PROPERTY
            {


                ADDRESS = new ADDRESS
                {
                    AddressLineText = "asdad #572",
                    CountryCode = null,
                    CountryName = null,
                    CityName = "abc",
                    PostalCode = "abc",
                    StateCode = "Pak",
                    CountyName = "abc"
                }
          ,
                PROJECT = new PROJECT
                {
                    PROJECT_DETAIL = new PROJECT_DETAIL
                    {
                        ProjectLegalStructureType = null
                    }
                }
            ,
                PROPERTY_DETAIL = new PROPERTY_DETAIL
                {
                    PropertyCurrentUsageType = "PrimaryResidence",
                    PropertyEstimatedValueAmount = null,
                    FinancedUnitCount = 4,
                    PropertyInProjectIndicator = null,
                    PropertyMixedUsageIndicator = null,
                    PropertyUsageType = "PrimaryResidence",
                    AttachmentType = null,
                    ConstructionMethodType = null,
                    PUDIndicator = null,
                    PropertyAcquiredDate = dt,
                    PropertyEstateType = "FeeSimple"
                }
            ,
                SALES_CONTRACTS = new SALES_CONTRACTS
                {
                    SALES_CONTRACT = new SALES_CONTRACT
                    {
                        SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL
                        {
                            SalesContractAmount = null
                        },
                        SequenceNumber = null
                    }
                }



            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.PROPERTY_DETAIL);
            Assert.NotNull(result.PROPERTY_DETAIL.FinancedUnitCount);
            Assert.Equal(4, result.PROPERTY_DETAIL.FinancedUnitCount);
            var actualJson = JsonConvert.SerializeObject(result.ADDRESS);
            var expectedJson = JsonConvert.SerializeObject(expected.ADDRESS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.PROJECT);
            expectedJson = JsonConvert.SerializeObject(expected.PROJECT);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.PROPERTY_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected.PROPERTY_DETAIL);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestSubjectPropertyWhenSingleFamilyDetachedExist()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 573,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 573,
                BusinessUnitId = 573,
                CustomerId = 573,
                LoanOriginatorId = 573,
                LoanGoalId = 573,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=573,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=573,
                            BorrowerId=573,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=573,
                               PropertyInfoId=573,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 1,//SingleFamilyDetached
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=573,
                                        CityId=573
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=573,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 1,//SingleFamilyDetached
                    AddressInfo = new AddressInfo
                    {
                        Id = 573,
                        CityId = 573,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 573,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "573"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.GetSubjectProperty(loanApplication);
            DateTime? dt = result.PROPERTY_DETAIL.PropertyAcquiredDate;
            var expected = new SUBJECT_PROPERTY
            {


                ADDRESS = new ADDRESS
                {
                    AddressLineText = "asdad #573",
                    CountryCode = null,
                    CountryName = null,
                    CityName = "abc",
                    PostalCode = "abc",
                    StateCode = "Pak",
                    CountyName = "abc"
                }
          ,
                PROJECT = new PROJECT
                {
                    PROJECT_DETAIL = new PROJECT_DETAIL
                    {
                        ProjectLegalStructureType = null
                    }
                }
            ,
                PROPERTY_DETAIL = new PROPERTY_DETAIL
                {
                    PropertyCurrentUsageType = "PrimaryResidence",
                    PropertyEstimatedValueAmount = null,
                    FinancedUnitCount = 1,
                    PropertyInProjectIndicator = false,
                    PropertyMixedUsageIndicator = null,
                    PropertyUsageType = "PrimaryResidence",
                    AttachmentType = "Detached",
                    ConstructionMethodType = "SiteBuilt",
                    PUDIndicator = null,
                    PropertyAcquiredDate = dt,
                    PropertyEstateType = "FeeSimple"
                }
            ,
                SALES_CONTRACTS = new SALES_CONTRACTS
                {
                    SALES_CONTRACT = new SALES_CONTRACT
                    {
                        SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL
                        {
                            SalesContractAmount = null
                        },
                        SequenceNumber = null
                    }
                }



            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.PROPERTY_DETAIL);
            Assert.NotNull(result.PROPERTY_DETAIL.AttachmentType);
            Assert.NotNull(result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.NotNull(result.PROPERTY_DETAIL.PropertyInProjectIndicator);

            Assert.Equal("Detached", result.PROPERTY_DETAIL.AttachmentType);
            Assert.Equal("SiteBuilt", result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.False(result.PROPERTY_DETAIL.PropertyInProjectIndicator);

            var actualJson = JsonConvert.SerializeObject(result.ADDRESS);
            var expectedJson = JsonConvert.SerializeObject(expected.ADDRESS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.PROJECT);
            expectedJson = JsonConvert.SerializeObject(expected.PROJECT);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.PROPERTY_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected.PROPERTY_DETAIL);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestSubjectPropertyWhenCooperativeExists()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 574,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 574,
                BusinessUnitId = 574,
                CustomerId = 574,
                LoanOriginatorId = 574,
                LoanGoalId = 574,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=574,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=574,
                            BorrowerId=574,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=574,
                               PropertyInfoId=574,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 7,//Cooperative
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=574,
                                        CityId=574
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=574,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 7,//Cooperative
                    AddressInfo = new AddressInfo
                    {
                        Id = 574,
                        CityId = 574,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 574,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "574"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.GetSubjectProperty(loanApplication);
            DateTime? dt = result.PROPERTY_DETAIL.PropertyAcquiredDate;
            var expected = new SUBJECT_PROPERTY
            {


                ADDRESS = new ADDRESS
                {
                    AddressLineText = "asdad #574",
                    CountryCode = null,
                    CountryName = null,
                    CityName = "abc",
                    PostalCode = "abc",
                    StateCode = "Pak",
                    CountyName = "abc"
                }
          ,
                PROJECT = new PROJECT
                {
                    PROJECT_DETAIL = new PROJECT_DETAIL
                    {
                        ProjectLegalStructureType = "Cooperative"
                    }
                }
            ,
                PROPERTY_DETAIL = new PROPERTY_DETAIL
                {
                    PropertyCurrentUsageType = "PrimaryResidence",
                    PropertyEstimatedValueAmount = null,
                    FinancedUnitCount = 1,
                    PropertyInProjectIndicator = null,
                    PropertyMixedUsageIndicator = null,
                    PropertyUsageType = "PrimaryResidence",
                    AttachmentType = "Attached",
                    ConstructionMethodType = "SiteBuilt",
                    PUDIndicator = null,
                    PropertyAcquiredDate = dt,
                    PropertyEstateType = "FeeSimple"
                }
            ,
                SALES_CONTRACTS = new SALES_CONTRACTS
                {
                    SALES_CONTRACT = new SALES_CONTRACT
                    {
                        SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL
                        {
                            SalesContractAmount = null
                        },
                        SequenceNumber = null
                    }
                }



            };
            //Assert
            Assert.NotNull(result);

            Assert.NotNull(result.PROPERTY_DETAIL);

            Assert.NotNull(result.PROPERTY_DETAIL.AttachmentType);
            Assert.NotNull(result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.Equal("Attached", result.PROPERTY_DETAIL.AttachmentType);
            Assert.Equal("SiteBuilt", result.PROPERTY_DETAIL.ConstructionMethodType);

            var actualJson = JsonConvert.SerializeObject(result.ADDRESS);
            var expectedJson = JsonConvert.SerializeObject(expected.ADDRESS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.PROJECT);
            expectedJson = JsonConvert.SerializeObject(expected.PROJECT);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.PROPERTY_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected.PROPERTY_DETAIL);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestSubjectWhenPropertyManufacturedExists()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 575,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 575,
                BusinessUnitId = 575,
                CustomerId = 575,
                LoanOriginatorId = 575,
                LoanGoalId = 575,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=575,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=575,
                            BorrowerId=575,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=575,
                               PropertyInfoId=575,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 9,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=575,
                                        CityId=575
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=575,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 9,
                    AddressInfo = new AddressInfo
                    {
                        Id = 575,
                        CityId = 575,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 575,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "575"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.GetSubjectProperty(loanApplication);
            DateTime? dt = result.PROPERTY_DETAIL.PropertyAcquiredDate;
            var expected = new SUBJECT_PROPERTY
            {


                ADDRESS = new ADDRESS
                {
                    AddressLineText = "asdad #575",
                    CountryCode = null,
                    CountryName = null,
                    CityName = "abc",
                    PostalCode = "abc",
                    StateCode = "Pak",
                    CountyName = "abc"
                }
          ,
                PROJECT = new PROJECT
                {
                    PROJECT_DETAIL = new PROJECT_DETAIL
                    {
                        ProjectLegalStructureType = null
                    }
                }
            ,
                PROPERTY_DETAIL = new PROPERTY_DETAIL
                {
                    PropertyCurrentUsageType = "PrimaryResidence",
                    PropertyEstimatedValueAmount = null,
                    FinancedUnitCount = 1,
                    PropertyInProjectIndicator = null,
                    PropertyMixedUsageIndicator = null,
                    PropertyUsageType = "PrimaryResidence",
                    AttachmentType = "Detached",
                    ConstructionMethodType = "Manufactured",
                    PUDIndicator = null,
                    PropertyAcquiredDate = dt,
                    PropertyEstateType = "FeeSimple"
                }
            ,
                SALES_CONTRACTS = new SALES_CONTRACTS
                {
                    SALES_CONTRACT = new SALES_CONTRACT
                    {
                        SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL
                        {
                            SalesContractAmount = null
                        },
                        SequenceNumber = null
                    }
                }



            };
            //Assert

            Assert.NotNull(result);
            Assert.NotNull(result.PROPERTY_DETAIL);
            Assert.NotNull(result.PROPERTY_DETAIL.AttachmentType);
            Assert.NotNull(result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.Equal("Detached", result.PROPERTY_DETAIL.AttachmentType);
            Assert.Equal("Manufactured", result.PROPERTY_DETAIL.ConstructionMethodType);
            var actualJson = JsonConvert.SerializeObject(result.ADDRESS);
            var expectedJson = JsonConvert.SerializeObject(expected.ADDRESS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.PROJECT);
            expectedJson = JsonConvert.SerializeObject(expected.PROJECT);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.PROPERTY_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected.PROPERTY_DETAIL);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestSubjectPropertyWhenManufactured_Condo_PUD_COOPExists()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 576,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 576,
                BusinessUnitId = 576,
                CustomerId = 576,
                LoanOriginatorId = 576,
                LoanGoalId = 576,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=576,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=576,
                            BorrowerId=576,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=576,
                               PropertyInfoId=576,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,//Manufactured_Condo_PUD_COOP
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=576,
                                        CityId=576
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=576,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,//Manufactured_Condo_PUD_COOP
                    AddressInfo = new AddressInfo
                    {
                        Id = 576,
                        CityId = 576,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 576,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "576"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.GetSubjectProperty(loanApplication);
            DateTime? dt = result.PROPERTY_DETAIL.PropertyAcquiredDate;
            var expected = new SUBJECT_PROPERTY
            {


                ADDRESS = new ADDRESS
                {
                    AddressLineText = "asdad #576",
                    CountryCode = null,
                    CountryName = null,
                    CityName = "abc",
                    PostalCode = "abc",
                    StateCode = "Pak",
                    CountyName = "abc"
                }
          ,
                PROJECT = new PROJECT
                {
                    PROJECT_DETAIL = new PROJECT_DETAIL
                    {
                        ProjectLegalStructureType = null
                    }
                }
            ,
                PROPERTY_DETAIL = new PROPERTY_DETAIL
                {
                    PropertyCurrentUsageType = "PrimaryResidence",
                    PropertyEstimatedValueAmount = null,
                    FinancedUnitCount = 1,
                    PropertyInProjectIndicator = true,
                    PropertyMixedUsageIndicator = true,
                    PropertyUsageType = "PrimaryResidence",
                    AttachmentType = "Detached",
                    ConstructionMethodType = "SiteBuilt",
                    PUDIndicator = true,
                    PropertyAcquiredDate = dt,
                    PropertyEstateType = "FeeSimple"
                }
            ,
                SALES_CONTRACTS = new SALES_CONTRACTS
                {
                    SALES_CONTRACT = new SALES_CONTRACT
                    {
                        SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL
                        {
                            SalesContractAmount = null
                        },
                        SequenceNumber = null
                    }
                }



            };


            //Assert
            Assert.NotNull(result);

            Assert.NotNull(result.PROPERTY_DETAIL);
            Assert.NotNull(result.PROPERTY_DETAIL.AttachmentType);
            Assert.NotNull(result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.NotNull(result.PROPERTY_DETAIL.PropertyInProjectIndicator);
            Assert.Equal("Detached", result.PROPERTY_DETAIL.AttachmentType);
            Assert.Equal("SiteBuilt", result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.True(result.PROPERTY_DETAIL.PropertyInProjectIndicator);
            Assert.True(result.PROPERTY_DETAIL.PropertyMixedUsageIndicator);
            Assert.True(result.PROPERTY_DETAIL.PUDIndicator);
            var actualJson = JsonConvert.SerializeObject(result.ADDRESS);
            var expectedJson = JsonConvert.SerializeObject(expected.ADDRESS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.PROJECT);
            expectedJson = JsonConvert.SerializeObject(expected.PROJECT);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.PROPERTY_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected.PROPERTY_DETAIL);
            Assert.Equal(expectedJson, actualJson);

        }

        [Fact]
        public void TestSubjectPropertyWhenCondominiumExists()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 577,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 577,
                BusinessUnitId = 577,
                CustomerId = 577,
                LoanOriginatorId = 577,
                LoanGoalId = 577,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=577,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=577,
                            BorrowerId=577,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=577,
                               PropertyInfoId=577,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,//Condominium TwoUnitBuilding
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=577,
                                        CityId=577
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=577,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 2,//Condominium TwoUnitBuilding
                    AddressInfo = new AddressInfo
                    {
                        Id = 577,
                        CityId = 577,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 577,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "577"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.GetSubjectProperty(loanApplication);

            DateTime? dt = result.PROPERTY_DETAIL.PropertyAcquiredDate;
            var expected = new SUBJECT_PROPERTY
            {


                ADDRESS = new ADDRESS
                {
                    AddressLineText = "asdad #577",
                    CountryCode = null,
                    CountryName = null,
                    CityName = "abc",
                    PostalCode = "abc",
                    StateCode = "Pak",
                    CountyName = "abc"
                }
          ,
                PROJECT = new PROJECT
                {
                    PROJECT_DETAIL = new PROJECT_DETAIL
                    {
                        ProjectLegalStructureType = "Condominium"
                    }
                }
            ,
                PROPERTY_DETAIL = new PROPERTY_DETAIL
                {
                    PropertyCurrentUsageType = "PrimaryResidence",
                    PropertyEstimatedValueAmount = null,
                    FinancedUnitCount = 1,
                    PropertyInProjectIndicator = true,
                    PropertyMixedUsageIndicator = null,
                    PropertyUsageType = "PrimaryResidence",
                    AttachmentType = "Attached",
                    ConstructionMethodType = "SiteBuilt",
                    PUDIndicator = null,
                    PropertyAcquiredDate = dt,
                    PropertyEstateType = "FeeSimple"
                }
            ,
                SALES_CONTRACTS = new SALES_CONTRACTS
                {
                    SALES_CONTRACT = new SALES_CONTRACT
                    {
                        SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL
                        {
                            SalesContractAmount = null
                        },
                        SequenceNumber = null
                    }
                }



            };

            //Assert
            Assert.NotNull(result);

            Assert.NotNull(result.PROPERTY_DETAIL);
            Assert.NotNull(result.PROPERTY_DETAIL.AttachmentType);
            Assert.NotNull(result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.NotNull(result.PROPERTY_DETAIL.PropertyInProjectIndicator);
            Assert.Equal("Attached", result.PROPERTY_DETAIL.AttachmentType);
            Assert.Equal("SiteBuilt", result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.True(result.PROPERTY_DETAIL.PropertyInProjectIndicator);
            Assert.Null(result.PROPERTY_DETAIL.PropertyMixedUsageIndicator);
            Assert.Null(result.PROPERTY_DETAIL.PUDIndicator);
            var actualJson = JsonConvert.SerializeObject(result.ADDRESS);
            var expectedJson = JsonConvert.SerializeObject(expected.ADDRESS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.PROJECT);
            expectedJson = JsonConvert.SerializeObject(expected.PROJECT);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.PROPERTY_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected.PROPERTY_DETAIL);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestSubjectPropertyWhenPropertyUsageIdIsNotExistExists()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 577,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 577,
                BusinessUnitId = 577,
                CustomerId = 577,
                LoanOriginatorId = 577,
                LoanGoalId = 577,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=577,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=577,
                            BorrowerId=577,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=577,
                               PropertyInfoId=577,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=577,
                                        CityId=577
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=577,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 577,
                        CityId = 577,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 577,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "577"
                    },
                    PropertyUsageId = 51,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.GetSubjectProperty(loanApplication);
            DateTime? dt = result.PROPERTY_DETAIL.PropertyAcquiredDate;
            var expected = new SUBJECT_PROPERTY
            {


                ADDRESS = new ADDRESS
                {
                    AddressLineText = "asdad #577",
                    CountryCode = null,
                    CountryName = null,
                    CityName = "abc",
                    PostalCode = "abc",
                    StateCode = "Pak",
                    CountyName = "abc"
                }
          ,
                PROJECT = new PROJECT
                {
                    PROJECT_DETAIL = new PROJECT_DETAIL
                    {
                        ProjectLegalStructureType = null
                    }
                }
            ,
                PROPERTY_DETAIL = new PROPERTY_DETAIL
                {
                    PropertyCurrentUsageType = "Other",
                    PropertyEstimatedValueAmount = null,
                    FinancedUnitCount = 1,
                    PropertyInProjectIndicator = true,
                    PropertyMixedUsageIndicator = true,
                    PropertyUsageType = "Other",
                    AttachmentType = "Detached",
                    ConstructionMethodType = "SiteBuilt",
                    PUDIndicator = true,
                    PropertyAcquiredDate = dt,
                    PropertyEstateType = "FeeSimple"
                }
            ,
                SALES_CONTRACTS = new SALES_CONTRACTS
                {
                    SALES_CONTRACT = new SALES_CONTRACT
                    {
                        SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL
                        {
                            SalesContractAmount = null
                        },
                        SequenceNumber = null
                    }
                }



            };
            //Assert
            Assert.NotNull(result);

            Assert.NotNull(result.PROPERTY_DETAIL);
            Assert.NotNull(result.PROPERTY_DETAIL.AttachmentType);
            Assert.NotNull(result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.NotNull(result.PROPERTY_DETAIL.PropertyInProjectIndicator);
            Assert.Equal("Detached", result.PROPERTY_DETAIL.AttachmentType);
            Assert.Equal("SiteBuilt", result.PROPERTY_DETAIL.ConstructionMethodType);
            Assert.True(result.PROPERTY_DETAIL.PropertyInProjectIndicator);
            var actualJson = JsonConvert.SerializeObject(result.ADDRESS);
            var expectedJson = JsonConvert.SerializeObject(expected.ADDRESS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.PROJECT);
            expectedJson = JsonConvert.SerializeObject(expected.PROJECT);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result.PROPERTY_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected.PROPERTY_DETAIL);
            Assert.Equal(expectedJson, actualJson);

        }

        //---
        [Fact]
        public void TestLoanInfo()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 578,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 578,
                BusinessUnitId = 578,
                CustomerId = 578,
                LoanOriginatorId = 578,
                LoanGoalId = 100,
                LoanPurposeId = 2,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=578,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=578,
                            BorrowerId=578,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=578,
                               PropertyInfoId=578,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=578,
                                        CityId=578
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=578,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=578,
                              CityId=578,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=578,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=578,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=578,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=578,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=578,
                            LoanContactId=578,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=578,
                             BorrowerId=578,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=578
                          ,Phone= "021136957825"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=578,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=578,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=578,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=578,
                            BorrowerId=578,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=578,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=578,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=578,
                                LiabilityTypeId=5
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 578,
                        CityId = 578,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 578,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "578"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = new List<PropertyTaxEscrow>
                    {
                        new PropertyTaxEscrow
                    {
                        Id=578,
                        EscrowEntityTypeId=2
                    },   new PropertyTaxEscrow
                    {
                        Id=578,
                        EscrowEntityTypeId=1
                    }

                    }
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=578,
                         IsFirstMortgage=false,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = 2
                ,
                LoanTypeId = 1

            };

            //Act
            var result = service.GetLoanInfo(loanApplication);
            var expected = new List<LOAN>
            {   new LOAN
                {
                         AMORTIZATION= new AMORTIZATION {
                            AMORTIZATION_RULE= new AMORTIZATION_RULE{
                                AmortizationType= "AdjustableRate",
                                LoanAmortizationPeriodCount= "360",
                                LoanAmortizationPeriodType= "Month"
                            }
                        },
                        CLOSING_INFORMATION= null,
                        CONSTRUCTION= new CONSTRUCTION{
                            LandOriginalCostAmount= null
                        },
                        DOCUMENT_SPECIFIC_DATA_SETS= new DOCUMENT_SPECIFIC_DATA_SETS {
                            DOCUMENT_SPECIFIC_DATA_SET= new DOCUMENT_SPECIFIC_DATA_SET{
                                URLA= new URLA{
                                    URLA_DETAIL= new URLA_DETAIL{
                                        ApplicationSignedByLoanOriginatorDate= null,
                                        EstimatedClosingCostsAmount= 0,
                                        MIAndFundingFeeFinancedAmount= 0,
                                        MIAndFundingFeeTotalAmount= null,
                                        PrepaidItemsEstimatedAmount= 0
                                    }
                                },
                                SequenceNumber= 1
                            }
                        },
                        HMDA_LOAN=new HMDA_LOAN {
                            HMDA_LOAN_DETAIL= new HMDA_LOAN_DETAIL{
                                HMDA_HOEPALoanStatusIndicator= false
                            }
                        }
                        ,HOUSING_EXPENSES= new HOUSING_EXPENSES{
                          HOUSING_EXPENSE= new List<HOUSING_EXPENSE> {
           new  HOUSING_EXPENSE
                {
                    HousingExpensePaymentAmount= 0,
                    HousingExpenseTimingType= "Proposed",
                    HousingExpenseType= "HomeownersInsurance",
                    SequenceNumber= null
                },
                new HOUSING_EXPENSE{
                    HousingExpensePaymentAmount= 0,
                    HousingExpenseTimingType= "Proposed",
                    HousingExpenseType= "RealEstateTax",
                    SequenceNumber= null
                }
            }
        },
                        LOAN_DETAIL= new LOAN_DETAIL{
            ApplicationReceivedDate= null,
            BalloonIndicator= false,
            BelowMarketSubordinateFinancingIndicator= false,
            BuydownTemporarySubsidyFundingIndicator= false,
            ConstructionLoanIndicator= false,
            ConversionOfContractForDeedIndicator= false,
            EnergyRelatedImprovementsIndicator= false,
            InterestOnlyIndicator= false,
            NegativeAmortizationIndicator= false,
            PrepaymentPenaltyIndicator= false,
            RenovationLoanIndicator= false
        },
                        LOAN_IDENTIFIERS= null,
                        LOAN_PRODUCT=new LOAN_PRODUCT {
                            LOAN_PRODUCT_DETAIL= new LOAN_PRODUCT_DETAIL{
                                DiscountPointsTotalAmount= 0
                            }
                        },
                        ORIGINATION_SYSTEMS= null,
                        QUALIFICATION= null,
                        REFINANCE=new REFINANCE {
                            RefinanceCashOutDeterminationType= "CashOut",



                            RefinancePrimaryPurposeType= "AssetAcquisition"
                        },
                        TERMS_OF_LOAN= new TERMS_OF_LOAN{
                            BaseLoanAmount= null,
                            LienPriorityType= "FirstLien",
                            LoanPurposeType= "Refinance",
                            MortgageType= "Conventional"
                        },

                        SequenceNumber= 1,

                        LoanRoleType="SubjectLoan",
                        Label="SUBJECT_LOAN_1"
                }
            };
            //Assert
            Assert.NotNull(result);


            Assert.NotNull(result[0].LOAN_DETAIL);
            Assert.Null(result[0].LOAN_DETAIL.ApplicationReceivedDate);
            Assert.False(result[0].LOAN_DETAIL.BalloonIndicator);
            Assert.False(result[0].LOAN_DETAIL.BelowMarketSubordinateFinancingIndicator);
            Assert.False(result[0].LOAN_DETAIL.BuydownTemporarySubsidyFundingIndicator);
            Assert.False(result[0].LOAN_DETAIL.ConstructionLoanIndicator);
            Assert.False(result[0].LOAN_DETAIL.ConversionOfContractForDeedIndicator);
            Assert.False(result[0].LOAN_DETAIL.EnergyRelatedImprovementsIndicator);
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

        }

        [Fact]
        public void TestLoanInfoWhenFirstMortgageTrue()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 579,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 579,
                BusinessUnitId = 579,
                CustomerId = 579,
                LoanOriginatorId = 579,
                LoanGoalId = 100,
                LoanPurposeId = 2,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=579,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=579,
                            BorrowerId=579,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=579,
                               PropertyInfoId=579,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=579,
                                        CityId=579
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=579,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=579,
                              CityId=579,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=579,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=579,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=579,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=579,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=579,
                            LoanContactId=579,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=579,
                             BorrowerId=579,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=579
                          ,Phone= "021136957925"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=579,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=579,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=579,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=579,
                            BorrowerId=579,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=579,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=579,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=579,
                                LiabilityTypeId=5
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 579,
                        CityId = 579,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 579,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "579"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = new List<PropertyTaxEscrow>
                    {
                        new PropertyTaxEscrow
                    {
                        Id=579,
                        EscrowEntityTypeId=2
                    },   new PropertyTaxEscrow
                    {
                        Id=579,
                        EscrowEntityTypeId=1
                    }

                    }
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=579,
                         IsFirstMortgage=true,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = 2
                ,
                LoanTypeId = 1

            };

            //Act
            var result = service.GetLoanInfo(loanApplication);

            var expected = new List<LOAN>
            {   new LOAN
                {
                         AMORTIZATION= new AMORTIZATION {
                            AMORTIZATION_RULE= new AMORTIZATION_RULE{
                                AmortizationType= "AdjustableRate",
                                LoanAmortizationPeriodCount= "360",
                                LoanAmortizationPeriodType= "Month"
                            }
                        },
                        CLOSING_INFORMATION= null,
                        CONSTRUCTION= new CONSTRUCTION{
                            LandOriginalCostAmount= null
                        },
                        DOCUMENT_SPECIFIC_DATA_SETS= new DOCUMENT_SPECIFIC_DATA_SETS {
                            DOCUMENT_SPECIFIC_DATA_SET= new DOCUMENT_SPECIFIC_DATA_SET{
                                URLA= new URLA{
                                    URLA_DETAIL= new URLA_DETAIL{
                                        ApplicationSignedByLoanOriginatorDate= null,
                                        EstimatedClosingCostsAmount= 0,
                                        MIAndFundingFeeFinancedAmount= 0,
                                        MIAndFundingFeeTotalAmount= null,
                                        PrepaidItemsEstimatedAmount= 0
                                    }
                                },
                                SequenceNumber= 1
                            }
                        },
                        HMDA_LOAN=new HMDA_LOAN {
                            HMDA_LOAN_DETAIL= new HMDA_LOAN_DETAIL{
                                HMDA_HOEPALoanStatusIndicator= false
                            }
                        }
                        ,HOUSING_EXPENSES= new HOUSING_EXPENSES{
                          HOUSING_EXPENSE= new List<HOUSING_EXPENSE> {
           new  HOUSING_EXPENSE
                {
                    HousingExpensePaymentAmount= 0,
                    HousingExpenseTimingType= "Proposed",
                    HousingExpenseType= "HomeownersInsurance",
                    SequenceNumber= null
                },
                new HOUSING_EXPENSE{
                    HousingExpensePaymentAmount= 0,
                    HousingExpenseTimingType= "Proposed",
                    HousingExpenseType= "RealEstateTax",
                    SequenceNumber= null
                },
                new HOUSING_EXPENSE{
                    HousingExpensePaymentAmount= 1000,
                    HousingExpenseTimingType= "Present",
                    HousingExpenseType= "FirstMortgagePrincipalAndInterest",
                    SequenceNumber= null
                }
            }
        },
                        LOAN_DETAIL= new LOAN_DETAIL{
                            ApplicationReceivedDate= null,
                            BalloonIndicator= false,
                            BelowMarketSubordinateFinancingIndicator= false,
                            BuydownTemporarySubsidyFundingIndicator= false,
                            ConstructionLoanIndicator= false,
                            ConversionOfContractForDeedIndicator= false,
                            EnergyRelatedImprovementsIndicator= false,
                            InterestOnlyIndicator= false,
                            NegativeAmortizationIndicator= false,
                            PrepaymentPenaltyIndicator= false,
                            RenovationLoanIndicator= false
                        },
                        LOAN_IDENTIFIERS= null,
                        LOAN_PRODUCT=new LOAN_PRODUCT {
                            LOAN_PRODUCT_DETAIL= new LOAN_PRODUCT_DETAIL{
                                DiscountPointsTotalAmount= 0
                            }
                        },
                        ORIGINATION_SYSTEMS= null,
                        QUALIFICATION= null,
                        REFINANCE=new REFINANCE {
                            RefinanceCashOutDeterminationType= "CashOut",



                            RefinancePrimaryPurposeType= "AssetAcquisition"
                        },
                        TERMS_OF_LOAN= new TERMS_OF_LOAN{
                            BaseLoanAmount= null,
                            LienPriorityType= "FirstLien",
                            LoanPurposeType= "Refinance",
                            MortgageType= "Conventional"
                        },

                        SequenceNumber= 1,

                        LoanRoleType="SubjectLoan",
                        Label="SUBJECT_LOAN_1"
                }
            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].HousingExpensePaymentAmount);
            Assert.NotNull(result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].HousingExpenseTimingType);
            Assert.NotNull(result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].HousingExpenseType);
            Assert.Equal(0, result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].HousingExpensePaymentAmount);
            Assert.Equal("Proposed", result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].HousingExpenseTimingType);
            Assert.Equal("HomeownersInsurance", result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].HousingExpenseType);

            var actualJson = JsonConvert.SerializeObject(result[0].AMORTIZATION);
            var expectedJson = JsonConvert.SerializeObject(expected[0].AMORTIZATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CLOSING_INFORMATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CLOSING_INFORMATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CONSTRUCTION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CONSTRUCTION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].DOCUMENT_SPECIFIC_DATA_SETS);
            expectedJson = JsonConvert.SerializeObject(expected[0].DOCUMENT_SPECIFIC_DATA_SETS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HMDA_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].HMDA_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HOUSING_EXPENSES);
            expectedJson = JsonConvert.SerializeObject(expected[0].HOUSING_EXPENSES);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_DETAIL);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_IDENTIFIERS);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_IDENTIFIERS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_PRODUCT);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_PRODUCT);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].QUALIFICATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].QUALIFICATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].REFINANCE);
            expectedJson = JsonConvert.SerializeObject(expected[0].REFINANCE);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].TERMS_OF_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].TERMS_OF_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestLoanInfoWhenPropertyInfoIsNull()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 580,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 580,
                BusinessUnitId = 580,
                CustomerId = 580,
                LoanOriginatorId = 580,
                LoanGoalId = 100,
                LoanPurposeId = 2,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=580,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=580,
                            BorrowerId=580,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=580,
                               PropertyInfoId=580,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=580,
                                        CityId=580
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=580,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=580,
                              CityId=580,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=580,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=580,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=580,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=580,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=580,
                            LoanContactId=580,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=580,
                             BorrowerId=580,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=580
                          ,Phone= "021136958025"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=580,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=580,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=580,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=580,
                            BorrowerId=580,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=580,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=580,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=580,
                                LiabilityTypeId=5
                            }
                       }
                    }


                },
                PropertyInfo = null,
                ProductAmortizationTypeId = 2,
                LoanTypeId = 1

            };

            //Act
            var result = service.GetLoanInfo(loanApplication);

            var expected = new List<LOAN>
            {   new LOAN
                {
                         AMORTIZATION= new AMORTIZATION {
                            AMORTIZATION_RULE= new AMORTIZATION_RULE{
                                AmortizationType= "AdjustableRate",
                                LoanAmortizationPeriodCount= "360",
                                LoanAmortizationPeriodType= "Month"
                            }
                        },
                        CLOSING_INFORMATION= null,
                        CONSTRUCTION= new CONSTRUCTION{
                            LandOriginalCostAmount= null
                        },
                        DOCUMENT_SPECIFIC_DATA_SETS= new DOCUMENT_SPECIFIC_DATA_SETS {
                            DOCUMENT_SPECIFIC_DATA_SET= new DOCUMENT_SPECIFIC_DATA_SET{
                                URLA= new URLA{
                                    URLA_DETAIL= new URLA_DETAIL{
                                        ApplicationSignedByLoanOriginatorDate= null,
                                        EstimatedClosingCostsAmount= 0,
                                        MIAndFundingFeeFinancedAmount= 0,
                                        MIAndFundingFeeTotalAmount= null,
                                        PrepaidItemsEstimatedAmount= 0
                                    }
                                },
                                SequenceNumber= 1
                            }
                        },
                        HMDA_LOAN=new HMDA_LOAN {
                            HMDA_LOAN_DETAIL= new HMDA_LOAN_DETAIL{
                                HMDA_HOEPALoanStatusIndicator= false
                            }
                        }
                        ,HOUSING_EXPENSES= new HOUSING_EXPENSES{
                          HOUSING_EXPENSE= new List<HOUSING_EXPENSE> {

            }
        },
                        LOAN_DETAIL= new LOAN_DETAIL{
                            ApplicationReceivedDate= null,
                            BalloonIndicator= false,
                            BelowMarketSubordinateFinancingIndicator= false,
                            BuydownTemporarySubsidyFundingIndicator= false,
                            ConstructionLoanIndicator= false,
                            ConversionOfContractForDeedIndicator= false,
                            EnergyRelatedImprovementsIndicator= false,
                            InterestOnlyIndicator= false,
                            NegativeAmortizationIndicator= false,
                            PrepaymentPenaltyIndicator= false,
                            RenovationLoanIndicator= false
                        },
                        LOAN_IDENTIFIERS= null,
                        LOAN_PRODUCT=new LOAN_PRODUCT {
                            LOAN_PRODUCT_DETAIL= new LOAN_PRODUCT_DETAIL{
                                DiscountPointsTotalAmount= 0
                            }
                        },
                        ORIGINATION_SYSTEMS= null,
                        QUALIFICATION= null,
                        REFINANCE=new REFINANCE {
                            RefinanceCashOutDeterminationType= "CashOut",



                            RefinancePrimaryPurposeType= "AssetAcquisition"
                        },
                        TERMS_OF_LOAN= new TERMS_OF_LOAN{
                            BaseLoanAmount= null,
                            LienPriorityType= "FirstLien",
                            LoanPurposeType= "Refinance",
                            MortgageType= "Conventional"
                        },

                        SequenceNumber= 1,

                        LoanRoleType="SubjectLoan",
                        Label="SUBJECT_LOAN_1"
                }
            };
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result[0].HOUSING_EXPENSES);
           


            var actualJson = JsonConvert.SerializeObject(result[0].AMORTIZATION);
            var expectedJson = JsonConvert.SerializeObject(expected[0].AMORTIZATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CLOSING_INFORMATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CLOSING_INFORMATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CONSTRUCTION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CONSTRUCTION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].DOCUMENT_SPECIFIC_DATA_SETS);
            expectedJson = JsonConvert.SerializeObject(expected[0].DOCUMENT_SPECIFIC_DATA_SETS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HMDA_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].HMDA_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HOUSING_EXPENSES);
            expectedJson = JsonConvert.SerializeObject(expected[0].HOUSING_EXPENSES);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_DETAIL);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_IDENTIFIERS);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_IDENTIFIERS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_PRODUCT);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_PRODUCT);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].QUALIFICATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].QUALIFICATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].REFINANCE);
            expectedJson = JsonConvert.SerializeObject(expected[0].REFINANCE);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].TERMS_OF_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].TERMS_OF_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestLoanInfoWhenProductAmortizationTypeIdIsNull()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 581,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 581,
                BusinessUnitId = 581,
                CustomerId = 581,
                LoanOriginatorId = 581,
                LoanGoalId = 100,
                LoanPurposeId = 2,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=581,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=581,
                            BorrowerId=581,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=581,
                               PropertyInfoId=581,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=581,
                                        CityId=581
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=581,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=581,
                              CityId=581,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=581,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=581,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=581,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=581,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=581,
                            LoanContactId=581,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=581,
                             BorrowerId=581,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=581
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=581,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=581,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=581,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=581,
                            BorrowerId=581,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=581,
                                LiabilityTypeId=5
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 581,
                        CityId = 581,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 581,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "581"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = new List<PropertyTaxEscrow>
                    {
                        new PropertyTaxEscrow
                    {
                        Id=581,
                        EscrowEntityTypeId=2
                    },   new PropertyTaxEscrow
                    {
                        Id=581,
                        EscrowEntityTypeId=1
                    }

                    }
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=581,
                         IsFirstMortgage=true,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 1

            };

            //Act
            var result = service.GetLoanInfo(loanApplication);
            #region expected
            var expected = new List<LOAN>
            {   new LOAN
                {
                         AMORTIZATION= new AMORTIZATION {
                            AMORTIZATION_RULE= new AMORTIZATION_RULE{
                                AmortizationType= "Other",
                                LoanAmortizationPeriodCount= "360",
                                LoanAmortizationPeriodType= "Month"
                            }
                        },
                        CLOSING_INFORMATION= null,
                        CONSTRUCTION= new CONSTRUCTION{
                            LandOriginalCostAmount= null
                        },
                        DOCUMENT_SPECIFIC_DATA_SETS= new DOCUMENT_SPECIFIC_DATA_SETS {
                            DOCUMENT_SPECIFIC_DATA_SET= new DOCUMENT_SPECIFIC_DATA_SET{
                                URLA= new URLA{
                                    URLA_DETAIL= new URLA_DETAIL{
                                        ApplicationSignedByLoanOriginatorDate= null,
                                        EstimatedClosingCostsAmount= 0,
                                        MIAndFundingFeeFinancedAmount= 0,
                                        MIAndFundingFeeTotalAmount= null,
                                        PrepaidItemsEstimatedAmount= 0
                                    }
                                },
                                SequenceNumber= 1
                            }
                        },
                        HMDA_LOAN=new HMDA_LOAN {
                            HMDA_LOAN_DETAIL= new HMDA_LOAN_DETAIL{
                                HMDA_HOEPALoanStatusIndicator= false
                            }
                        }
                        ,HOUSING_EXPENSES= new HOUSING_EXPENSES{
                          HOUSING_EXPENSE= new List<HOUSING_EXPENSE> {
                              new HOUSING_EXPENSE
                              {
                                  HousingExpensePaymentAmount= 0,
                                    HousingExpenseTimingType= "Proposed",
                                    HousingExpenseType= "HomeownersInsurance",
                                    SequenceNumber= null
                              }
                              ,  new HOUSING_EXPENSE
                              {
                                  HousingExpensePaymentAmount= 0,
                                    HousingExpenseTimingType= "Proposed",
                                    HousingExpenseType= "RealEstateTax",
                                    SequenceNumber= null
                              }
                              ,  new HOUSING_EXPENSE
                              {
                                  HousingExpensePaymentAmount= 1000,
                                    HousingExpenseTimingType= "Present",
                                    HousingExpenseType= "FirstMortgagePrincipalAndInterest",
                                    SequenceNumber= null
                              }
            }
        },
                        LOAN_DETAIL= new LOAN_DETAIL{
                            ApplicationReceivedDate= null,
                            BalloonIndicator= false,
                            BelowMarketSubordinateFinancingIndicator= false,
                            BuydownTemporarySubsidyFundingIndicator= false,
                            ConstructionLoanIndicator= false,
                            ConversionOfContractForDeedIndicator= false,
                            EnergyRelatedImprovementsIndicator= false,
                            InterestOnlyIndicator= false,
                            NegativeAmortizationIndicator= false,
                            PrepaymentPenaltyIndicator= false,
                            RenovationLoanIndicator= false
                        },
                        LOAN_IDENTIFIERS= null,
                        LOAN_PRODUCT=new LOAN_PRODUCT {
                            LOAN_PRODUCT_DETAIL= new LOAN_PRODUCT_DETAIL{
                                DiscountPointsTotalAmount= 0
                            }
                        },
                        ORIGINATION_SYSTEMS= null,
                        QUALIFICATION= null,
                        REFINANCE=new REFINANCE {
                            RefinanceCashOutDeterminationType= "CashOut",



                            RefinancePrimaryPurposeType= "AssetAcquisition"
                        },
                        TERMS_OF_LOAN= new TERMS_OF_LOAN{
                            BaseLoanAmount= null,
                            LienPriorityType= "FirstLien",
                            LoanPurposeType= "Refinance",
                            MortgageType= "Conventional"
                        },

                        SequenceNumber= 1,

                        LoanRoleType="SubjectLoan",
                        Label="SUBJECT_LOAN_1"
                }
            };
            #endregion
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result[0].AMORTIZATION.AMORTIZATION_RULE.AmortizationType);
            Assert.Equal("Other", result[0].AMORTIZATION.AMORTIZATION_RULE.AmortizationType);
            #region assert
            var actualJson = JsonConvert.SerializeObject(result[0].AMORTIZATION);
            var expectedJson = JsonConvert.SerializeObject(expected[0].AMORTIZATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CLOSING_INFORMATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CLOSING_INFORMATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CONSTRUCTION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CONSTRUCTION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].DOCUMENT_SPECIFIC_DATA_SETS);
            expectedJson = JsonConvert.SerializeObject(expected[0].DOCUMENT_SPECIFIC_DATA_SETS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HMDA_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].HMDA_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HOUSING_EXPENSES);
            expectedJson = JsonConvert.SerializeObject(expected[0].HOUSING_EXPENSES);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_DETAIL);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_IDENTIFIERS);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_IDENTIFIERS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_PRODUCT);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_PRODUCT);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].QUALIFICATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].QUALIFICATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].REFINANCE);
            expectedJson = JsonConvert.SerializeObject(expected[0].REFINANCE);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].TERMS_OF_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].TERMS_OF_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

            #endregion

        }

        [Fact]
        public void TestLoanInfoWhenPropertyTaxEscrowsIsNull()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 581,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 581,
                BusinessUnitId = 581,
                CustomerId = 581,
                LoanOriginatorId = 581,
                LoanGoalId = 100,
                LoanPurposeId = 2,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=581,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=581,
                            BorrowerId=581,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=581,
                               PropertyInfoId=581,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=581,
                                        CityId=581
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=581,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=581,
                              CityId=581,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=581,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=581,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=581,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=581,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=581,
                            LoanContactId=581,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=581,
                             BorrowerId=581,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=581
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=581,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=581,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=581,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=581,
                            BorrowerId=581,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=581,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=581,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=581,
                                LiabilityTypeId=5
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 581,
                        CityId = 581,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 581,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "581"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=581,
                         IsFirstMortgage=true,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 1

            };

            //Act
            var result = service.GetLoanInfo(loanApplication);
            #region expected
            var expected = new List<LOAN>
            {   new LOAN
                {
                         AMORTIZATION= new AMORTIZATION {
                            AMORTIZATION_RULE= new AMORTIZATION_RULE{
                                AmortizationType= "Other",
                                LoanAmortizationPeriodCount= "360",
                                LoanAmortizationPeriodType= "Month"
                            }
                        },
                        CLOSING_INFORMATION= null,
                        CONSTRUCTION= new CONSTRUCTION{
                            LandOriginalCostAmount= null
                        },
                        DOCUMENT_SPECIFIC_DATA_SETS= new DOCUMENT_SPECIFIC_DATA_SETS {
                            DOCUMENT_SPECIFIC_DATA_SET= new DOCUMENT_SPECIFIC_DATA_SET{
                                URLA= new URLA{
                                    URLA_DETAIL= new URLA_DETAIL{
                                        ApplicationSignedByLoanOriginatorDate= null,
                                        EstimatedClosingCostsAmount= 0,
                                        MIAndFundingFeeFinancedAmount= 0,
                                        MIAndFundingFeeTotalAmount= null,
                                        PrepaidItemsEstimatedAmount= 0
                                    }
                                },
                                SequenceNumber= 1
                            }
                        },
                        HMDA_LOAN=new HMDA_LOAN {
                            HMDA_LOAN_DETAIL= new HMDA_LOAN_DETAIL{
                                HMDA_HOEPALoanStatusIndicator= false
                            }
                        }
                        ,HOUSING_EXPENSES= new HOUSING_EXPENSES{
                          HOUSING_EXPENSE= new List<HOUSING_EXPENSE> {
                              new HOUSING_EXPENSE
                              {
                                  HousingExpensePaymentAmount= 1000,
                                    HousingExpenseTimingType= "Present",
                                    HousingExpenseType= "FirstMortgagePrincipalAndInterest",
                                    SequenceNumber= null
                              }

            }
        },
                        LOAN_DETAIL= new LOAN_DETAIL{
                            ApplicationReceivedDate= null,
                            BalloonIndicator= false,
                            BelowMarketSubordinateFinancingIndicator= false,
                            BuydownTemporarySubsidyFundingIndicator= false,
                            ConstructionLoanIndicator= false,
                            ConversionOfContractForDeedIndicator= false,
                            EnergyRelatedImprovementsIndicator= false,
                            InterestOnlyIndicator= false,
                            NegativeAmortizationIndicator= false,
                            PrepaymentPenaltyIndicator= false,
                            RenovationLoanIndicator= false
                        },
                        LOAN_IDENTIFIERS= null,
                        LOAN_PRODUCT=new LOAN_PRODUCT {
                            LOAN_PRODUCT_DETAIL= new LOAN_PRODUCT_DETAIL{
                                DiscountPointsTotalAmount= 0
                            }
                        },
                        ORIGINATION_SYSTEMS= null,
                        QUALIFICATION= null,
                        REFINANCE=new REFINANCE {
                            RefinanceCashOutDeterminationType= "CashOut",



                            RefinancePrimaryPurposeType= "AssetAcquisition"
                        },
                        TERMS_OF_LOAN= new TERMS_OF_LOAN{
                            BaseLoanAmount= null,
                            LienPriorityType= "FirstLien",
                            LoanPurposeType= "Refinance",
                            MortgageType= "Conventional"
                        },

                        SequenceNumber= 1,

                        LoanRoleType="SubjectLoan",
                        Label="SUBJECT_LOAN_1"
                }
            };
            #endregion

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result[0].HOUSING_EXPENSES);
            Assert.NotNull(result[0].HOUSING_EXPENSES.HOUSING_EXPENSE);

            Assert.Equal(1000, result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].HousingExpensePaymentAmount);
            Assert.Equal("Present", result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].HousingExpenseTimingType);
            Assert.Equal("FirstMortgagePrincipalAndInterest", result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].HousingExpenseType);
            Assert.Null(result[0].HOUSING_EXPENSES.HOUSING_EXPENSE[0].SequenceNumber);
            #region assert
            var actualJson = JsonConvert.SerializeObject(result[0].AMORTIZATION);
            var expectedJson = JsonConvert.SerializeObject(expected[0].AMORTIZATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CLOSING_INFORMATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CLOSING_INFORMATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CONSTRUCTION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CONSTRUCTION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].DOCUMENT_SPECIFIC_DATA_SETS);
            expectedJson = JsonConvert.SerializeObject(expected[0].DOCUMENT_SPECIFIC_DATA_SETS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HMDA_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].HMDA_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HOUSING_EXPENSES);
            expectedJson = JsonConvert.SerializeObject(expected[0].HOUSING_EXPENSES);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_DETAIL);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_IDENTIFIERS);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_IDENTIFIERS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_PRODUCT);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_PRODUCT);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].QUALIFICATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].QUALIFICATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].REFINANCE);
            expectedJson = JsonConvert.SerializeObject(expected[0].REFINANCE);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].TERMS_OF_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].TERMS_OF_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

            #endregion
        }

        [Fact]
        public void TestLoanInfoWhenMortgageOnPropertiesIsNull()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication();
            //Act
            var result = service.GetLoanInfo(loanApplication);
            #region expected
            var expected = new List<LOAN>
            {   new LOAN
                {
                         AMORTIZATION= new AMORTIZATION {
                            AMORTIZATION_RULE= new AMORTIZATION_RULE{
                                AmortizationType= "Other",
                                LoanAmortizationPeriodCount= "360",
                                LoanAmortizationPeriodType= "Month"
                            }
                        },
                        CLOSING_INFORMATION= null,
                        CONSTRUCTION= new CONSTRUCTION{
                            LandOriginalCostAmount= null
                        },
                        DOCUMENT_SPECIFIC_DATA_SETS= new DOCUMENT_SPECIFIC_DATA_SETS {
                            DOCUMENT_SPECIFIC_DATA_SET= new DOCUMENT_SPECIFIC_DATA_SET{
                                URLA= new URLA{
                                    URLA_DETAIL= new URLA_DETAIL{
                                        ApplicationSignedByLoanOriginatorDate= null,
                                        EstimatedClosingCostsAmount= 0,
                                        MIAndFundingFeeFinancedAmount= 0,
                                        MIAndFundingFeeTotalAmount= null,
                                        PrepaidItemsEstimatedAmount= 0
                                    }
                                },
                                SequenceNumber= 1
                            }
                        },
                        HMDA_LOAN=new HMDA_LOAN {
                            HMDA_LOAN_DETAIL= new HMDA_LOAN_DETAIL{
                                HMDA_HOEPALoanStatusIndicator= false
                            }
                        }
                        ,HOUSING_EXPENSES= new HOUSING_EXPENSES{
                          HOUSING_EXPENSE= new List<HOUSING_EXPENSE> {


            }
        },
                        LOAN_DETAIL= new LOAN_DETAIL{
                            ApplicationReceivedDate= null,
                            BalloonIndicator= false,
                            BelowMarketSubordinateFinancingIndicator= false,
                            BuydownTemporarySubsidyFundingIndicator= false,
                            ConstructionLoanIndicator= false,
                            ConversionOfContractForDeedIndicator= false,
                            EnergyRelatedImprovementsIndicator= false,
                            InterestOnlyIndicator= false,
                            NegativeAmortizationIndicator= false,
                            PrepaymentPenaltyIndicator= false,
                            RenovationLoanIndicator= false
                        },
                        LOAN_IDENTIFIERS= null,
                        LOAN_PRODUCT=new LOAN_PRODUCT {
                            LOAN_PRODUCT_DETAIL= new LOAN_PRODUCT_DETAIL{
                                DiscountPointsTotalAmount= 0
                            }
                        },
                        ORIGINATION_SYSTEMS= null,
                        QUALIFICATION= null,
                        REFINANCE=null,
                        TERMS_OF_LOAN= new TERMS_OF_LOAN{
                            BaseLoanAmount= null,
                            LienPriorityType= "FirstLien",
                            LoanPurposeType= "Unknown",
                            MortgageType= "Conventional"
                        },

                        SequenceNumber= 1,

                        LoanRoleType="SubjectLoan",
                        Label="SUBJECT_LOAN_1"
                }
            };
            #endregion
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result[0].HOUSING_EXPENSES);
           

            #region assert
            var actualJson = JsonConvert.SerializeObject(result[0].AMORTIZATION);
            var expectedJson = JsonConvert.SerializeObject(expected[0].AMORTIZATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CLOSING_INFORMATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CLOSING_INFORMATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CONSTRUCTION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CONSTRUCTION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].DOCUMENT_SPECIFIC_DATA_SETS);
            expectedJson = JsonConvert.SerializeObject(expected[0].DOCUMENT_SPECIFIC_DATA_SETS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HMDA_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].HMDA_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HOUSING_EXPENSES);
            expectedJson = JsonConvert.SerializeObject(expected[0].HOUSING_EXPENSES);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_DETAIL);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_IDENTIFIERS);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_IDENTIFIERS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_PRODUCT);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_PRODUCT);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].QUALIFICATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].QUALIFICATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].REFINANCE);
            expectedJson = JsonConvert.SerializeObject(expected[0].REFINANCE);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].TERMS_OF_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].TERMS_OF_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

            #endregion
        }

        [Fact]
        public void TestLoanInfoWhenPropertyMortgageOnPropertiesAndIsFirstMortgageIsFalseIsNotNull()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 582,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 582,
                BusinessUnitId = 582,
                CustomerId = 582,
                LoanOriginatorId = 582,
                LoanGoalId = 100,
                LoanPurposeId = 2,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=582,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=582,
                            BorrowerId=582,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=582,
                               PropertyInfoId=582,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=582,
                                        CityId=582
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=582,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=582,
                              CityId=582,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=582,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=582,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=582,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=582,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=582,
                            LoanContactId=582,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=582,
                             BorrowerId=582,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=582
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=582,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=582,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=582,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=582,
                            BorrowerId=582,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=582,
                                LiabilityTypeId=5
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 582,
                        CityId = 582,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 582,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "582"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=582,
                         IsFirstMortgage=false,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 1

            };

            //Act
            var result = service.GetLoanInfo(loanApplication);
            #region expected
            var expected = new List<LOAN>
            {   new LOAN
                {
                         AMORTIZATION= new AMORTIZATION {
                            AMORTIZATION_RULE= new AMORTIZATION_RULE{
                                AmortizationType= "Other",
                                LoanAmortizationPeriodCount= "360",
                                LoanAmortizationPeriodType= "Month"
                            }
                        },
                        CLOSING_INFORMATION= null,
                        CONSTRUCTION= new CONSTRUCTION{
                            LandOriginalCostAmount= null
                        },
                        DOCUMENT_SPECIFIC_DATA_SETS= new DOCUMENT_SPECIFIC_DATA_SETS {
                            DOCUMENT_SPECIFIC_DATA_SET= new DOCUMENT_SPECIFIC_DATA_SET{
                                URLA= new URLA{
                                    URLA_DETAIL= new URLA_DETAIL{
                                        ApplicationSignedByLoanOriginatorDate= null,
                                        EstimatedClosingCostsAmount= 0,
                                        MIAndFundingFeeFinancedAmount= 0,
                                        MIAndFundingFeeTotalAmount= null,
                                        PrepaidItemsEstimatedAmount= 0
                                    }
                                },
                                SequenceNumber= 1
                            }
                        },
                        HMDA_LOAN=new HMDA_LOAN {
                            HMDA_LOAN_DETAIL= new HMDA_LOAN_DETAIL{
                                HMDA_HOEPALoanStatusIndicator= false
                            }
                        }
                        ,HOUSING_EXPENSES= new HOUSING_EXPENSES{
                          HOUSING_EXPENSE= new List<HOUSING_EXPENSE> {


            }
        },
                        LOAN_DETAIL= new LOAN_DETAIL{
                            ApplicationReceivedDate= null,
                            BalloonIndicator= false,
                            BelowMarketSubordinateFinancingIndicator= false,
                            BuydownTemporarySubsidyFundingIndicator= false,
                            ConstructionLoanIndicator= false,
                            ConversionOfContractForDeedIndicator= false,
                            EnergyRelatedImprovementsIndicator= false,
                            InterestOnlyIndicator= false,
                            NegativeAmortizationIndicator= false,
                            PrepaymentPenaltyIndicator= false,
                            RenovationLoanIndicator= false
                        },
                        LOAN_IDENTIFIERS= null,
                        LOAN_PRODUCT=new LOAN_PRODUCT {
                            LOAN_PRODUCT_DETAIL= new LOAN_PRODUCT_DETAIL{
                                DiscountPointsTotalAmount= 0
                            }
                        },
                        ORIGINATION_SYSTEMS= null,
                        QUALIFICATION= null,
                        REFINANCE= new REFINANCE
                        {
                            RefinanceCashOutDeterminationType="CashOut",
                        RefinancePrimaryPurposeType= "AssetAcquisition"
                        },
                        TERMS_OF_LOAN= new TERMS_OF_LOAN{
                            BaseLoanAmount= null,
                            LienPriorityType= "FirstLien",
                            LoanPurposeType= "Refinance",
                            MortgageType= "Conventional"
                        },

                        SequenceNumber= 1,

                        LoanRoleType="SubjectLoan",
                        Label="SUBJECT_LOAN_1"
                }
            };
            #endregion
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result[0].HOUSING_EXPENSES);
            


            #region assert
            var actualJson = JsonConvert.SerializeObject(result[0].AMORTIZATION);
            var expectedJson = JsonConvert.SerializeObject(expected[0].AMORTIZATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CLOSING_INFORMATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CLOSING_INFORMATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CONSTRUCTION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CONSTRUCTION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].DOCUMENT_SPECIFIC_DATA_SETS);
            expectedJson = JsonConvert.SerializeObject(expected[0].DOCUMENT_SPECIFIC_DATA_SETS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HMDA_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].HMDA_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HOUSING_EXPENSES);
            expectedJson = JsonConvert.SerializeObject(expected[0].HOUSING_EXPENSES);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_DETAIL);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_IDENTIFIERS);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_IDENTIFIERS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_PRODUCT);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_PRODUCT);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].QUALIFICATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].QUALIFICATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].REFINANCE);
            expectedJson = JsonConvert.SerializeObject(expected[0].REFINANCE);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].TERMS_OF_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].TERMS_OF_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

            #endregion
        }

        [Fact]
        public void TestLoanInfoWhenPropertyInterestRateReductionIsExists()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 583,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 583,
                BusinessUnitId = 583,
                CustomerId = 583,
                LoanOriginatorId = 583,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=583,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=583,
                            BorrowerId=583,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=583,
                               PropertyInfoId=583,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=583,
                                        CityId=583
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=583,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=583,
                              CityId=583,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=583,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=583,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=583,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=583,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=583,
                            LoanContactId=583,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=583,
                             BorrowerId=583,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=583
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=583,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=583,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=583,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=583,
                            BorrowerId=583,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=583,
                                LiabilityTypeId=5
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 583,
                        CityId = 583,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 583,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "583"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=583,
                         IsFirstMortgage=false,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 0

            };

            //Act

            var result = service.GetLoanInfo(loanApplication);
            #region expected
            var expected = new List<LOAN>
            {   new LOAN
                {
                         AMORTIZATION= new AMORTIZATION {
                            AMORTIZATION_RULE= new AMORTIZATION_RULE{
                                AmortizationType= "Other",
                                LoanAmortizationPeriodCount= "360",
                                LoanAmortizationPeriodType= "Month"
                            }
                        },
                        CLOSING_INFORMATION= null,
                        CONSTRUCTION= new CONSTRUCTION{
                            LandOriginalCostAmount= null
                        },
                        DOCUMENT_SPECIFIC_DATA_SETS= new DOCUMENT_SPECIFIC_DATA_SETS {
                            DOCUMENT_SPECIFIC_DATA_SET= new DOCUMENT_SPECIFIC_DATA_SET{
                                URLA= new URLA{
                                    URLA_DETAIL= new URLA_DETAIL{
                                        ApplicationSignedByLoanOriginatorDate= null,
                                        EstimatedClosingCostsAmount= 0,
                                        MIAndFundingFeeFinancedAmount= 0,
                                        MIAndFundingFeeTotalAmount= null,
                                        PrepaidItemsEstimatedAmount= 0
                                    }
                                },
                                SequenceNumber= 1
                            }
                        },
                        HMDA_LOAN=new HMDA_LOAN {
                            HMDA_LOAN_DETAIL= new HMDA_LOAN_DETAIL{
                                HMDA_HOEPALoanStatusIndicator= false
                            }
                        }
                        ,HOUSING_EXPENSES= new HOUSING_EXPENSES{
                          HOUSING_EXPENSE= new List<HOUSING_EXPENSE> {


            }
        },
                        LOAN_DETAIL= new LOAN_DETAIL{
                            ApplicationReceivedDate= null,
                            BalloonIndicator= false,
                            BelowMarketSubordinateFinancingIndicator= false,
                            BuydownTemporarySubsidyFundingIndicator= false,
                            ConstructionLoanIndicator= false,
                            ConversionOfContractForDeedIndicator= false,
                            EnergyRelatedImprovementsIndicator= false,
                            InterestOnlyIndicator= false,
                            NegativeAmortizationIndicator= false,
                            PrepaymentPenaltyIndicator= false,
                            RenovationLoanIndicator= false
                        },
                        LOAN_IDENTIFIERS= null,
                        LOAN_PRODUCT=new LOAN_PRODUCT {
                            LOAN_PRODUCT_DETAIL= new LOAN_PRODUCT_DETAIL{
                                DiscountPointsTotalAmount= 0
                            }
                        },
                        ORIGINATION_SYSTEMS= null,
                        QUALIFICATION= null,
                        REFINANCE= new REFINANCE
                        {
                            RefinanceCashOutDeterminationType="NoCashOut",
                        RefinancePrimaryPurposeType= "InterestRateReduction"
                        },
                        TERMS_OF_LOAN= new TERMS_OF_LOAN{
                            BaseLoanAmount= null,
                            LienPriorityType= "FirstLien",
                            LoanPurposeType= "Refinance",
                            MortgageType= "Other"
                        },

                        SequenceNumber= 1,

                        LoanRoleType="SubjectLoan",
                        Label="SUBJECT_LOAN_1"
                }
            };
            #endregion
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result[0].REFINANCE);
            Assert.Equal("NoCashOut", result[0].REFINANCE.RefinanceCashOutDeterminationType);
            Assert.Equal("InterestRateReduction", result[0].REFINANCE.RefinancePrimaryPurposeType);
            #region assert
            var actualJson = JsonConvert.SerializeObject(result[0].AMORTIZATION);
            var expectedJson = JsonConvert.SerializeObject(expected[0].AMORTIZATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CLOSING_INFORMATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CLOSING_INFORMATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].CONSTRUCTION);
            expectedJson = JsonConvert.SerializeObject(expected[0].CONSTRUCTION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].DOCUMENT_SPECIFIC_DATA_SETS);
            expectedJson = JsonConvert.SerializeObject(expected[0].DOCUMENT_SPECIFIC_DATA_SETS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HMDA_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].HMDA_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].HOUSING_EXPENSES);
            expectedJson = JsonConvert.SerializeObject(expected[0].HOUSING_EXPENSES);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_DETAIL);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_DETAIL);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_IDENTIFIERS);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_IDENTIFIERS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].LOAN_PRODUCT);
            expectedJson = JsonConvert.SerializeObject(expected[0].LOAN_PRODUCT);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].QUALIFICATION);
            expectedJson = JsonConvert.SerializeObject(expected[0].QUALIFICATION);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].ORIGINATION_SYSTEMS);
            expectedJson = JsonConvert.SerializeObject(expected[0].ORIGINATION_SYSTEMS);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].REFINANCE);
            expectedJson = JsonConvert.SerializeObject(expected[0].REFINANCE);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result[0].TERMS_OF_LOAN);
            expectedJson = JsonConvert.SerializeObject(expected[0].TERMS_OF_LOAN);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

            #endregion
        }

        [Fact]
        public void TestLiabilities()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 584,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 584,
                BusinessUnitId = 584,
                CustomerId = 584,
                LoanOriginatorId = 584,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=584,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=584,
                            BorrowerId=584,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=584,
                               PropertyInfoId=584,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=584,
                                        CityId=584
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=584,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=584,
                              CityId=584,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=584,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=584,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=584,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=584,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=584,
                            LoanContactId=584,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=584,
                             BorrowerId=584,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=584
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=584,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=584,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=584,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=584,
                            BorrowerId=584,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=584,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=584,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=584,
                                LiabilityTypeId=5
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 584,
                        CityId = 584,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 584,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "584"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=584,
                         IsFirstMortgage=true,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 0

            };

            //Act
            var result = service.GetLiabilities(loanApplication);
            //Assert
            #region
            var expected = new LIABILITIES
            {
                LIABILITY = new List<LIABILITY>{
              new LIABILITY
             {
                 LIABILITY_DETAIL= new LIABILITY_DETAIL {
                LiabilityExclusionIndicator= false,
                LiabilityMonthlyPaymentAmount= 1000,
                LiabilityPayoffStatusIndicator= true,
                LiabilityType= "MortgageLoan",
                LiabilityUnpaidBalanceAmount= null,
                LiabilityAccountIdentifier= null,
                LiabilityRemainingTermMonthsCount= null,
                LiabilityTypeOtherDescription= null,
                LiabilityPaymentIncludesTaxesInsuranceIndicator= false
            },
            LIABILITY_HOLDER= new LIABILITY_HOLDER{
                NAME= new NAME{
                    FullName= "First Mortgage",
                    FirstName= null,
                    LastName= null,
                    MiddleName= null,
                    SuffixName= null
                }
            },
            SequenceNumber= 1,
            Label= "LIABILITY_1"

             }

             , new LIABILITY
             {
            LIABILITY_DETAIL= new LIABILITY_DETAIL {
                LiabilityExclusionIndicator= false,
                LiabilityMonthlyPaymentAmount= null,
                LiabilityPayoffStatusIndicator= false,
                LiabilityType= "Other",
                LiabilityUnpaidBalanceAmount= null,
                LiabilityAccountIdentifier= null,
                LiabilityRemainingTermMonthsCount= null,
                LiabilityTypeOtherDescription= null,
                LiabilityPaymentIncludesTaxesInsuranceIndicator= false
            },
            LIABILITY_HOLDER= new LIABILITY_HOLDER {
                NAME= new NAME {
                    FullName= null,
                    FirstName= null,
                    LastName= null,
                    MiddleName= null,
                    SuffixName= null
                }
            },
            SequenceNumber= 2,
            Label= "LIABILITY_2"
             }


            }
            ,
                LIABILITY_SUMMARY = null
            };
            #endregion
            Assert.NotNull(result);
            Assert.NotNull(result.LIABILITY);
            Assert.NotNull(result.LIABILITY[0].LIABILITY_DETAIL);
            Assert.False(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityExclusionIndicator);
            Assert.Equal(1000, result.LIABILITY[0].LIABILITY_DETAIL.LiabilityMonthlyPaymentAmount);
            Assert.True(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityPayoffStatusIndicator);
            Assert.Equal("MortgageLoan", result.LIABILITY[0].LIABILITY_DETAIL.LiabilityType);
            Assert.Null(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityUnpaidBalanceAmount);
            Assert.False(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityPaymentIncludesTaxesInsuranceIndicator);

            #region assert
            var actualJson = JsonConvert.SerializeObject(result.LIABILITY[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.LIABILITY[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.LIABILITY[1]);
            expectedJson = JsonConvert.SerializeObject(expected.LIABILITY[1]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
            #endregion 

        }

        [Fact]
        public void TestLiabilitiesWhenIsFirstMortgageIsTrue()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 585,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 585,
                BusinessUnitId = 585,
                CustomerId = 585,
                LoanOriginatorId = 585,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=585,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=585,
                            BorrowerId=585,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=585,
                               PropertyInfoId=585,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=585,
                                        CityId=585
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=585,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=585,
                              CityId=585,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=585,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=585,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=585,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=585,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=585,
                            LoanContactId=585,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=585,
                             BorrowerId=585,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=585
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=585,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=585,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=585,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=585,
                            BorrowerId=585,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=585,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=585,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=585,
                                LiabilityTypeId=16
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 585,
                        CityId = 585,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 585,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "585"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=585,
                         IsFirstMortgage=true,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 0

            };

            //Act
            var result = service.GetLiabilities(loanApplication);
            #region
            var expected = new LIABILITIES
            {
                LIABILITY = new List<LIABILITY>{
              new LIABILITY
             {
                 LIABILITY_DETAIL= new LIABILITY_DETAIL {
                LiabilityExclusionIndicator= false,
                LiabilityMonthlyPaymentAmount= 1000,
                LiabilityPayoffStatusIndicator= true,
                LiabilityType= "MortgageLoan",
                LiabilityUnpaidBalanceAmount= null,
                LiabilityAccountIdentifier= null,
                LiabilityRemainingTermMonthsCount= null,
                LiabilityTypeOtherDescription= null,
                LiabilityPaymentIncludesTaxesInsuranceIndicator= false
            },
            LIABILITY_HOLDER= new LIABILITY_HOLDER{
                NAME= new NAME{
                    FullName= "First Mortgage",
                    FirstName= null,
                    LastName= null,
                    MiddleName= null,
                    SuffixName= null
                }
            },
            SequenceNumber= 1,
            Label= "LIABILITY_1"

             }

             , new LIABILITY
             {
            LIABILITY_DETAIL= new LIABILITY_DETAIL {
                LiabilityExclusionIndicator= false,
                LiabilityMonthlyPaymentAmount= null,
                LiabilityPayoffStatusIndicator= false,
                LiabilityType= "CollectionsJudgmentsAndLiens",
                LiabilityUnpaidBalanceAmount= null,
                LiabilityAccountIdentifier= null,
                LiabilityRemainingTermMonthsCount= null,
                LiabilityTypeOtherDescription= null,
                LiabilityPaymentIncludesTaxesInsuranceIndicator= false
            },
            LIABILITY_HOLDER= new LIABILITY_HOLDER {
                NAME= new NAME {
                    FullName= null,
                    FirstName= null,
                    LastName= null,
                    MiddleName= null,
                    SuffixName= null
                }
            },
            SequenceNumber= 2,
            Label= "LIABILITY_2"
             }


            }
            ,
                LIABILITY_SUMMARY = null
            };
            #endregion
            //Assert
            Assert.NotNull(result);
            Assert.False(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityExclusionIndicator);
            Assert.Equal(1000, result.LIABILITY[0].LIABILITY_DETAIL.LiabilityMonthlyPaymentAmount);
            Assert.True(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityPayoffStatusIndicator);
            Assert.Equal("MortgageLoan", result.LIABILITY[0].LIABILITY_DETAIL.LiabilityType);
            Assert.Null(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityUnpaidBalanceAmount);


            #region assert
            var actualJson = JsonConvert.SerializeObject(result.LIABILITY[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.LIABILITY[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.LIABILITY[1]);
            expectedJson = JsonConvert.SerializeObject(expected.LIABILITY[1]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
            #endregion 
        }

        [Fact]
        public void TestLiabilitiesWhenIsFirstMortgageIsFalse()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 586,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 586,
                BusinessUnitId = 586,
                CustomerId = 586,
                LoanOriginatorId = 586,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=586,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=586,
                            BorrowerId=586,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=586,
                               PropertyInfoId=586,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=586,
                                        CityId=586
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=586,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=586,
                              CityId=586,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=586,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=586,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=586,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=586,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=586,
                            LoanContactId=586,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=586,
                             BorrowerId=586,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=586
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=586,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=586,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=586,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=586,
                            BorrowerId=586,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=586,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=586,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=586,
                                LiabilityTypeId=16
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 586,
                        CityId = 586,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 586,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "586"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=586,
                         IsFirstMortgage=false,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 0

            };

            //Act
            var result = service.GetLiabilities(loanApplication);
            #region
            var expected = new LIABILITIES
            {
                LIABILITY = new List<LIABILITY>{
              new LIABILITY
             {
                 LIABILITY_DETAIL= new LIABILITY_DETAIL {
                LiabilityExclusionIndicator= false,
                LiabilityMonthlyPaymentAmount= 1000,
                LiabilityPayoffStatusIndicator= false,
                LiabilityType= "MortgageLoan",
                LiabilityUnpaidBalanceAmount= null,
                LiabilityAccountIdentifier= null,
                LiabilityRemainingTermMonthsCount= null,
                LiabilityTypeOtherDescription= null,
                LiabilityPaymentIncludesTaxesInsuranceIndicator= false
            },
            LIABILITY_HOLDER= new LIABILITY_HOLDER{
                NAME= new NAME{
                    FullName= "Second Mortgage",
                    FirstName= null,
                    LastName= null,
                    MiddleName= null,
                    SuffixName= null
                }
            },
            SequenceNumber= 1,
            Label= "LIABILITY_1"

             }

             , new LIABILITY
             {
            LIABILITY_DETAIL= new LIABILITY_DETAIL {
                LiabilityExclusionIndicator= false,
                LiabilityMonthlyPaymentAmount= null,
                LiabilityPayoffStatusIndicator= false,
                LiabilityType= "CollectionsJudgmentsAndLiens",
                LiabilityUnpaidBalanceAmount= null,
                LiabilityAccountIdentifier= null,
                LiabilityRemainingTermMonthsCount= null,
                LiabilityTypeOtherDescription= null,
                LiabilityPaymentIncludesTaxesInsuranceIndicator= false
            },
            LIABILITY_HOLDER= new LIABILITY_HOLDER {
                NAME= new NAME {
                    FullName= null,
                    FirstName= null,
                    LastName= null,
                    MiddleName= null,
                    SuffixName= null
                }
            },
            SequenceNumber= 2,
            Label= "LIABILITY_2"
             }


            }
            ,
                LIABILITY_SUMMARY = null
            };
            #endregion
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.LIABILITY[0].LIABILITY_HOLDER);
            Assert.NotNull(result.LIABILITY[0].LIABILITY_HOLDER.NAME);
            Assert.NotNull(result.LIABILITY[0].LIABILITY_HOLDER.NAME.FullName);
            Assert.Equal("Second Mortgage", result.LIABILITY[0].LIABILITY_HOLDER.NAME.FullName);
            #region assert
            var actualJson = JsonConvert.SerializeObject(result.LIABILITY[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.LIABILITY[0]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result.LIABILITY[1]);
            expectedJson = JsonConvert.SerializeObject(expected.LIABILITY[1]);
            Assert.Equal(expectedJson, actualJson);
            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
            #endregion 
        }

        [Fact]
        public void TestLiabilitiesWhenPropertyInfoIsNull()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 587,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 587,
                BusinessUnitId = 587,
                CustomerId = 587,
                LoanOriginatorId = 587,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=587,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=587,
                            BorrowerId=587,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=587,
                               PropertyInfoId=587,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=587,
                                        CityId=587
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=587,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=587,
                              CityId=587,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=587,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=587,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=587,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=587,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=587,
                            LoanContactId=587,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=587,
                             BorrowerId=587,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=587
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=587,
                            BorrowerId=587,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=587,
                                LiabilityTypeId=16
                            }
                       }
                    }


                },
                PropertyInfo = null
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 0

            };

            //Act
            var result = service.GetLiabilities(loanApplication);
            #region
            var expected = new LIABILITIES
            {
                LIABILITY = new List<LIABILITY>{
              new LIABILITY
             {
                 LIABILITY_DETAIL= new LIABILITY_DETAIL {
                LiabilityExclusionIndicator= false,
                LiabilityMonthlyPaymentAmount= null,
                LiabilityPayoffStatusIndicator= false,
                LiabilityType= "CollectionsJudgmentsAndLiens",
                LiabilityUnpaidBalanceAmount= null,
                LiabilityAccountIdentifier= null,
                LiabilityRemainingTermMonthsCount= null,
                LiabilityTypeOtherDescription= null,
                LiabilityPaymentIncludesTaxesInsuranceIndicator= false
            },
            LIABILITY_HOLDER= new LIABILITY_HOLDER{
                NAME= new NAME{
                    FullName= null,
                    FirstName= null,
                    LastName= null,
                    MiddleName= null,
                    SuffixName= null
                }
            },
            SequenceNumber= 1,
            Label= "LIABILITY_1"

             }




            }
            ,
                LIABILITY_SUMMARY = null
            };
            #endregion

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityAccountIdentifier);
            Assert.False(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityExclusionIndicator);
            Assert.Null(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityMonthlyPaymentAmount);
            Assert.False(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityPayoffStatusIndicator);
            Assert.Null(result.LIABILITY[0].LIABILITY_DETAIL.LiabilityRemainingTermMonthsCount);
            Assert.Equal("CollectionsJudgmentsAndLiens", result.LIABILITY[0].LIABILITY_DETAIL.LiabilityType);
            #region assert
            var actualJson = JsonConvert.SerializeObject(result.LIABILITY[0]);
            var expectedJson = JsonConvert.SerializeObject(expected.LIABILITY[0]);
            Assert.Equal(expectedJson, actualJson);

            actualJson = JsonConvert.SerializeObject(result);
            expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
            #endregion 
        }

        [Fact]
        public void TestExpenseLiabilities()
        {
            List<int?> liabilitiesToSkip = new List<int?>();
            liabilitiesToSkip.Add(16);
            




            var service = new MismoConverter34();
            service.SetLiabilitiesToSkip(liabilitiesToSkip);

            LoanApplication loanApplication = new LoanApplication
            {
                Id = 587,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 587,
                BusinessUnitId = 587,
                CustomerId = 587,
                LoanOriginatorId = 587,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=587,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=587,
                            BorrowerId=587,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=587,
                               PropertyInfoId=587,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=587,
                                        CityId=587
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=587,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=587,
                              CityId=587,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=587,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=587,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=587,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=587,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=587,
                            LoanContactId=587,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=587,
                             BorrowerId=587,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=587
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=587,
                            BorrowerId=587,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=587,
                                LiabilityTypeId=16
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 587,
                        CityId = 587,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 587,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "587"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=587,
                         IsFirstMortgage=false,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 0

            };

            //Act
            var result = service.GetExpenseLiabilities(loanApplication);

            var expected = new EXPENSES
            {
                EXPENSE = new List<EXPENSE>
                {
                    new EXPENSE {
            AlimonyOwedToName= null,
            ExpenseDescription= null,
            ExpenseMonthlyPaymentAmount= null,
            ExpenseRemainingTermMonthsCount= null,
            ExpenseType= "Other",
            ExpenseTypeOtherDescription= null,
            Label= "EXPENSE_1",
            SequenceNumber= 1
                  }
                }
            };

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.EXPENSE[0].ExpenseDescription);
            Assert.Null(result.EXPENSE[0].ExpenseMonthlyPaymentAmount);
            Assert.Equal("EXPENSE_1", result.EXPENSE[0].Label);
            Assert.Equal(1, result.EXPENSE[0].SequenceNumber);
            Assert.Equal("Other", result.EXPENSE[0].ExpenseType);
            #region assert
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

            #endregion
        }

        [Fact]
        public void TestExpenseLiabilitiesWhenLiabilityTypeIdIsNotExist()
        {
            List<int?> liabilitiesToSkip = new List<int?>();
            liabilitiesToSkip.Add(27);
            
            var service = new MismoConverter34();
            service.SetLiabilitiesToSkip(liabilitiesToSkip);

            LoanApplication loanApplication = new LoanApplication
            {
                Id = 587,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 587,
                BusinessUnitId = 587,
                CustomerId = 587,
                LoanOriginatorId = 587,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=587,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=587,
                            BorrowerId=587,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=587,
                               PropertyInfoId=587,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=587,
                                        CityId=587
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=587,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=587,
                              CityId=587,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=587,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=587,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=587,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=587,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=587,
                            LoanContactId=587,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=587,
                             BorrowerId=587,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=587
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=587,
                            BorrowerId=587,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=587,
                                LiabilityTypeId=27
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 587,
                        CityId = 587,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 587,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "587"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=587,
                         IsFirstMortgage=false,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 0

            };

            //Act
            var result = service.GetExpenseLiabilities(loanApplication);
            var expected = new EXPENSES
            {
                EXPENSE = new List<EXPENSE>
                {
                    new EXPENSE {
            AlimonyOwedToName= null,
            ExpenseDescription= null,
            ExpenseMonthlyPaymentAmount= null,
            ExpenseRemainingTermMonthsCount= null,
            ExpenseType= "Alimony",
            ExpenseTypeOtherDescription= null,
            Label= "EXPENSE_1",
            SequenceNumber= 1
                  }
                }
            };
            //Assert
            Assert.NotNull(result);
            Assert.Equal("Alimony", result.EXPENSE[0].ExpenseType);
            Assert.Null(result.EXPENSE[0].ExpenseDescription);
            Assert.Null(result.EXPENSE[0].ExpenseMonthlyPaymentAmount);
            Assert.Equal("EXPENSE_1", result.EXPENSE[0].Label);
            Assert.Equal(1, result.EXPENSE[0].SequenceNumber);
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

        }

        [Fact]
        public void TestExpenseLiabilitiesWhenLiabilityTypeIdChildCareIsExists()
        {
            List<int?> liabilitiesToSkip = new List<int?>();
            liabilitiesToSkip.Add(14);
            
            var service = new MismoConverter34();
            service.SetLiabilitiesToSkip(liabilitiesToSkip);

            LoanApplication loanApplication = new LoanApplication
            {
                Id = 587,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 587,
                BusinessUnitId = 587,
                CustomerId = 587,
                LoanOriginatorId = 587,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=587,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=587,
                            BorrowerId=587,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=587,
                               PropertyInfoId=587,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=587,
                                        CityId=587
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=587,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=587,
                              CityId=587,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=587,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=587,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=587,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=587,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=587,
                            LoanContactId=587,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=587,
                             BorrowerId=587,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=587
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=587,
                            BorrowerId=587,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=587,
                                LiabilityTypeId=14
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 587,
                        CityId = 587,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 587,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "587"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                ,
                    PropertyTaxEscrows = null
                ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=587,
                         IsFirstMortgage=false,
                          MonthlyPayment=1000,

                     }

                     }
                }
            ,
                ProductAmortizationTypeId = null
            ,
                LoanTypeId = 0

            };

            //Act
            var result = service.GetExpenseLiabilities(loanApplication);
            var expected = new EXPENSES
            {
                EXPENSE = new List<EXPENSE>
                {
                    new EXPENSE {
            AlimonyOwedToName= null,
            ExpenseDescription= null,
            ExpenseMonthlyPaymentAmount= null,
            ExpenseRemainingTermMonthsCount= null,
            ExpenseType= "Other",
            ExpenseTypeOtherDescription= "Child Care",
            Label= "EXPENSE_1",
            SequenceNumber= 1
                  }
                }
            };
            //Assert
            Assert.NotNull(result);

            Assert.Equal("Other", result.EXPENSE[0].ExpenseType);
            Assert.Equal("Child Care", result.EXPENSE[0].ExpenseTypeOtherDescription);
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

        }

        [Fact]
        public void TestExpenseLiabilitieWhenBorrowersAndliabilitiesToSkipIsNull()
        {
            List<int?> liabilitiesToSkip = new List<int?>();
            liabilitiesToSkip.Add(27);
            
            var service = new MismoConverter34();
            service.SetLiabilitiesToSkip(null);

            LoanApplication loanApplication = new LoanApplication
            {
                Id = 587,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 587,
                BusinessUnitId = 587,
                CustomerId = 587,
                LoanOriginatorId = 587,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = null,
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 587,
                        CityId = 587,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 587,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "587"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=587,
                         IsFirstMortgage=false,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 0

            };

            //Act
            var result = service.GetExpenseLiabilities(loanApplication);
            #region
            var expected = new EXPENSES
            {
                EXPENSE = new List<EXPENSE>
                { }
            };
            #endregion
            //Assert
            Assert.NotNull(result);

            
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestExpenseLiabilitiesWhenExpenseLiabilitiesIsNull()
        {
            List<int?> liabilitiesToSkip = new List<int?>();
            liabilitiesToSkip.Add(207);
           
            var service = new MismoConverter34();
            service.SetLiabilitiesToSkip(liabilitiesToSkip);

            LoanApplication loanApplication = new LoanApplication
            {
                Id = 587,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 587,
                BusinessUnitId = 587,
                CustomerId = 587,
                LoanOriginatorId = 587,
                LoanGoalId = 5,
                LoanPurposeId = 3,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=587,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=587,
                            BorrowerId=587,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=587,
                               PropertyInfoId=587,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=587,
                                        CityId=587
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=587,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=587,
                              CityId=587,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=null
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                            ,GenderId=1
                            ,LoanContactEthnicityBinders= new List< LoanContactEthnicityBinder>
                            {
                                new LoanContactEthnicityBinder
                                {
                               Id=587,
                               EthnicityId=1
                               , Ethnicity= new Ethnicity
                               {
                                   Id=587,
                                   IsActive=true,
                                   EthnicityDetails= new List<EthnicityDetail>
                                   {  new EthnicityDetail
                                   {
                                       Id=587,
                                       Name="Sli"
                                       ,IsActive=true
                                   }
                                   }
                               }
                               ,EthnicityDetail= new EthnicityDetail
                               {   Id=587,
                                       Name="Sli"
                                       ,IsActive=true

                               }
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=587,
                            LoanContactId=587,
                            Race= new Race
                            { Id=12,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=1,
                                IsActive=true,
                                Name="Abc",
                                RaceId=12
                            }
                        }
                        }
                      ,HomePhone="02136544442"
                           ,CellPhone="0162555555"
                           ,WorkPhone="02136987544"
                           ,ResidencyTypeId=2
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=587,
                             BorrowerId=587,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=587
                          ,Phone= "021136958125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=587,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=587,
                            BorrowerId=587,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=587,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=587,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=587,
                                LiabilityTypeId=27
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 587,
                        CityId = 587,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 587,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "587"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = null
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=587,
                         IsFirstMortgage=false,
                          MonthlyPayment=1000,

                     }

                     }
                }
                ,
                ProductAmortizationTypeId = null
                ,
                LoanTypeId = 0

            };

            //Act11
            var result = service.GetExpenseLiabilities(loanApplication);
            #region
            var expected = new EXPENSES
            {
                EXPENSE = new List<EXPENSE>
                { }
            };
            #endregion
            //Assert
            Assert.NotNull(result);
           
            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);

        }

        [Fact]
        public void TestSubjectPropertyWhenTwoUnitBuildingExists()
        {

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 571,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 571,
                BusinessUnitId = 571,
                CustomerId = 571,
                LoanOriginatorId = 571,
                LoanGoalId = 571,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=571,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=571,
                            BorrowerId=571,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=1
                            }
                           }
                        },
                        BorrowerProperties = new List<BorrowerProperty>
                        {
                           new BorrowerProperty
                           {
                               BorrowerId=571,
                               PropertyInfoId=571,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId =4,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=571,
                                        CityId=571
                                    }
                                    ,PropertyUsageId=1
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                    ,DateAcquired=DateTime.UtcNow
                                    ,PropertyValue=1000
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=571,
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb"
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 4,
                    AddressInfo = new AddressInfo
                    {
                        Id = 571,
                        CityId = 571,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abcd",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 571,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "571"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyValue = 1000
                },



            };

            //Act
            var result = service.GetSubjectProperty(loanApplication);
            #region
            DateTime? dt = result.PROPERTY_DETAIL.PropertyAcquiredDate;
            var expected = new SUBJECT_PROPERTY
            {
                ADDRESS = new ADDRESS
                {
                    AddressLineText = "asdad #571",
                    CountryCode = null,
                    CountryName = null,
                    CityName = "abc",
                    PostalCode = "abcd",
                    StateCode = "Pak",
                    CountyName = "abc"
                },
                PROJECT = new PROJECT
                {
                    PROJECT_DETAIL = new PROJECT_DETAIL
                    {
                        ProjectLegalStructureType = null
                    }
                },
                PROPERTY_DETAIL = new PROPERTY_DETAIL
                {
                    PropertyCurrentUsageType = "PrimaryResidence",
                    PropertyEstimatedValueAmount = 1000,
                    FinancedUnitCount = 2,
                    PropertyInProjectIndicator = null,
                    PropertyMixedUsageIndicator = null,
                    PropertyUsageType = "PrimaryResidence",
                    AttachmentType = null,
                    ConstructionMethodType = null,
                    PUDIndicator = null,
                    PropertyAcquiredDate = dt,
                    PropertyEstateType = "FeeSimple"
                },
                SALES_CONTRACTS = new SALES_CONTRACTS
                {
                    SALES_CONTRACT = new SALES_CONTRACT
                    {
                        SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL
                        {
                            SalesContractAmount = 1000
                        },
                        SequenceNumber = null
                    }
                }
            };
            #endregion
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.PROPERTY_DETAIL);
            Assert.NotNull(result.PROPERTY_DETAIL.FinancedUnitCount);
            Assert.Equal(2, result.PROPERTY_DETAIL.FinancedUnitCount);
            Assert.Equal("PrimaryResidence", result.PROPERTY_DETAIL.PropertyCurrentUsageType);


            var actualJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);
            Assert.Equal(expectedJson, actualJson);
        }

        #region Get Borrower method unit test
        [Fact]
        public void TestMismoWithMinimumBorrower()
        {
            // Arrange
            Borrower primaryBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "MyFirstName",
                    MiddleName = "MyMiddleName",
                    LastName = "MyLastName",
                    Suffix = "MySuffix",
                    MaritalStatusId = 1 // Married
                },
                NoOfDependent = 1,
                DependentAge = "21",
                EmploymentInfoes = null,
                LoanApplication = new LoanApplication()
                {
                    CreatedOnUtc = null
                }
            };
            LoanApplication loanApplicaiton = new LoanApplication()
            {
                CreatedOnUtc = null,
                Borrowers = new List<Borrower>()
                  {
                      primaryBorrower
                  },
            };
            var expected = new List<PARTY>
            {
                new PARTY
                {
                    SequenceNumber = 1,
                    TAXPAYER_IDENTIFIERS = new TAXPAYER_IDENTIFIERS
                    {
                        TAXPAYER_IDENTIFIER = new TAXPAYER_IDENTIFIER
                        {
                            SequenceNumber = 1,
                            TaxpayerIdentifierType = "SocialSecurityNumber"
                        }
                    },
                    INDIVIDUAL = new INDIVIDUAL
                    {
                        CONTACT_POINTS = new CONTACT_POINTS
                        {
                            CONTACT_POINT = new List<CONTACT_POINT>
                            {
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_EMAIL = new CONTACT_POINT_EMAIL
                                    {
                                        ContactPointEmailValue = primaryBorrower.LoanContact.EmailAddress
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue = primaryBorrower.LoanContact.HomePhone
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Home"
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue = primaryBorrower.LoanContact.CellPhone
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Mobile"
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue =
                                            $"{primaryBorrower.LoanContact.WorkPhone}-{primaryBorrower.LoanContact.WorkPhoneExt}"
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Work"
                                    }
                                }
                            }
                        }
                        , NAME = new NAME()
                        {
                            FullName = null,
                            FirstName = primaryBorrower.LoanContact.FirstName,
                            LastName = primaryBorrower.LoanContact.LastName,
                            MiddleName = primaryBorrower.LoanContact.MiddleName,
                            SuffixName = primaryBorrower.LoanContact.Suffix,
                        }
                    },
                    ROLES = new ROLES()
                    {
                        ROLE = new ROLE()
                        {
                            BORROWER = new BORROWER()
                            {
                                BORROWER_DETAIL = new BORROWER_DETAIL()
                                {
                                    CommunityPropertyStateResidentIndicator = false,
                                    SelfDeclaredMilitaryServiceIndicator = false,
                                    DependentCount = primaryBorrower.NoOfDependent,
                                    MaritalStatusType = "Married"
                                },
                                DECLARATION = new DECLARATION()
                                {
                                    DECLARATION_DETAIL = new DECLARATION_DETAIL()
                                },
                                DEPENDENTS = new DEPENDENTS()
                                {
                                    DEPENDENT = new List<DEPENDENT>()
                                    {
                                        new DEPENDENT()
                                        {
                                            DependentAgeYearsCount = primaryBorrower.DependentAge,
                                            SequenceNumber = 1
                                        }
                                    }
                                },
                                EMPLOYERS = new EMPLOYERS()
                                {
                                    EMPLOYER = new List<EMPLOYER>()
                                },
                                GOVERNMENT_MONITORING = new GOVERNMENT_MONITORING()
                                {
                                    GOVERNMENT_MONITORING_DETAIL = new GOVERNMENT_MONITORING_DETAIL()
                                    {
                                        HMDAEthnicityCollectedBasedOnVisualObservationOrSurnameIndicator = false,
                                        HMDAGenderCollectedBasedOnVisualObservationOrNameIndicator = false,
                                        HMDAGenderRefusalIndicator = false,
                                        HMDARaceCollectedBasedOnVisualObservationOrSurnameIndicator = false
                                    },
                                    HMDA_ETHNICITY_ORIGINS = new HMDA_ETHNICITY_ORIGINS()
                                    {
                                        HMDA_ETHNICITY_ORIGIN = new EditableList<HMDA_ETHNICITY_ORIGIN>()
                                    },
                                    HMDA_RACES = new HMDA_RACES()
                                    {
                                        HMDA_RACE =  new EditableList<HMDA_RACE>()
                                    },
                                    EXTENSION = new EXTENSION()
                                    {
                                        OTHER = new OTHER()
                                        {
                                            GOVERNMENT_MONITORING_EXTENSION =  new GOVERNMENT_MONITORING_EXTENSION()
                                            {
                                                HMDA_ETHNICITIES = new HMDA_ETHNICITIES()
                                                {
                                                    HMDA_ETHNICITY = new EditableList<HMDA_ETHNICITY>()
                                                }
                                            }
                                        }
                                    }
                                },
                                RESIDENCES = new RESIDENCES()
                                {
                                    RESIDENCE = new EditableList<RESIDENCE>()
                                },
                                CURRENT_INCOME = new CURRENT_INCOME()
                                {
                                    CURRENT_INCOME_ITEMS = new CURRENT_INCOME_ITEMS()
                                    {
                                        CURRENT_INCOME_ITEM = new EditableList<CURRENT_INCOME_ITEM>()
                                    }
                                }
                            },
                            ROLE_DETAIL = new ROLE_DETAIL()
                            {
                                PartyRoleType = "Borrower"
                            },
                            SequenceNumber = 1,
                            Label = "BORROWER_1",
                        }
                    }
                }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowers(loanApplicaiton);

            // Assert
            Assert.NotNull(results);
            var expectedJson = JsonConvert.SerializeObject(expected[0].INDIVIDUAL.CONTACT_POINTS);
            var actualJson = JsonConvert.SerializeObject(results[0].INDIVIDUAL.CONTACT_POINTS);
            Assert.Equal(expectedJson, actualJson);

            expectedJson = JsonConvert.SerializeObject(expected[0].INDIVIDUAL);
            actualJson = JsonConvert.SerializeObject(results[0].INDIVIDUAL);
            Assert.Equal(expectedJson, actualJson);

            expectedJson = JsonConvert.SerializeObject(expected[0].ROLES);
            actualJson = JsonConvert.SerializeObject(results[0].ROLES);
            Assert.Equal(expectedJson, actualJson);

            expectedJson = JsonConvert.SerializeObject(expected);
            actualJson = JsonConvert.SerializeObject(results);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestMismoWithCoBorrower()
        {
            // Arrange
            Borrower primaryBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "PrimaryMyFirstName",
                    MiddleName = "PrimaryMyMiddleName",
                    LastName = "PrimaryMyLastName",
                    Suffix = "PrimaryMySuffix",
                    MaritalStatusId = 1 // Married
                },
                NoOfDependent = 1,
                DependentAge = "21",
                EmploymentInfoes = new List<EmploymentInfo>(),
                LoanApplication = new LoanApplication()
                {
                    CreatedOnUtc = DateTime.Now.AddDays(-10)
                }
            };
            Borrower coBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "CoBorrowerMyFirstName",
                    MiddleName = "CoBorrowerMyMiddleName",
                    LastName = "CoBorrowerMyLastName",
                    Suffix = "CoBorrowerMySuffix",
                    MaritalStatusId = 2 // Married
                },
                NoOfDependent = 2,
                DependentAge = "12, 24",
                EmploymentInfoes = new List<EmploymentInfo>()
            };
            LoanApplication loanApplicaiton = new LoanApplication()
            {
                CreatedOnUtc = DateTime.UtcNow.AddDays(-10),
                Borrowers = new List<Borrower>()
                  {
                      primaryBorrower,
                      coBorrower
                  },
            };
            var expected = new List<PARTY>
            {
                new PARTY
                {
                    SequenceNumber = 1,
                    TAXPAYER_IDENTIFIERS = new TAXPAYER_IDENTIFIERS
                    {
                        TAXPAYER_IDENTIFIER = new TAXPAYER_IDENTIFIER
                        {
                            SequenceNumber = 1,
                            TaxpayerIdentifierType = "SocialSecurityNumber"
                        }
                    },
                    INDIVIDUAL = new INDIVIDUAL
                    {
                        CONTACT_POINTS = new CONTACT_POINTS
                        {
                            CONTACT_POINT = new List<CONTACT_POINT>
                            {
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_EMAIL = new CONTACT_POINT_EMAIL
                                    {
                                        ContactPointEmailValue = primaryBorrower.LoanContact.EmailAddress
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue = primaryBorrower.LoanContact.HomePhone
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Home"
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue = primaryBorrower.LoanContact.CellPhone
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Mobile"
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue =
                                            $"{primaryBorrower.LoanContact.WorkPhone}-{primaryBorrower.LoanContact.WorkPhoneExt}"
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Work"
                                    }
                                }
                            }
                        }
                        , NAME = new NAME()
                        {
                            FullName = null,
                            FirstName = primaryBorrower.LoanContact.FirstName,
                            LastName = primaryBorrower.LoanContact.LastName,
                            MiddleName = primaryBorrower.LoanContact.MiddleName,
                            SuffixName = primaryBorrower.LoanContact.Suffix,
                        }
                    },
                    ROLES = new ROLES()
                    {
                        ROLE = new ROLE()
                        {
                            BORROWER = new BORROWER()
                            {
                                BORROWER_DETAIL = new BORROWER_DETAIL()
                                {
                                    CommunityPropertyStateResidentIndicator = false,
                                    SelfDeclaredMilitaryServiceIndicator = false,
                                    DependentCount = primaryBorrower.NoOfDependent,
                                    MaritalStatusType = "Married"
                                },
                                DECLARATION = new DECLARATION()
                                {
                                    DECLARATION_DETAIL = new DECLARATION_DETAIL()
                                },
                                DEPENDENTS = new DEPENDENTS()
                                {
                                    DEPENDENT = new List<DEPENDENT>()
                                    {
                                        new DEPENDENT()
                                        {
                                            DependentAgeYearsCount = primaryBorrower.DependentAge,
                                            SequenceNumber = 1
                                        }
                                    }
                                },
                                EMPLOYERS = new EMPLOYERS()
                                {
                                    EMPLOYER = new List<EMPLOYER>()
                                },
                                GOVERNMENT_MONITORING = new GOVERNMENT_MONITORING()
                                {
                                    GOVERNMENT_MONITORING_DETAIL = new GOVERNMENT_MONITORING_DETAIL()
                                    {
                                        HMDAEthnicityCollectedBasedOnVisualObservationOrSurnameIndicator = false,
                                        HMDAGenderCollectedBasedOnVisualObservationOrNameIndicator = false,
                                        HMDAGenderRefusalIndicator = false,
                                        HMDARaceCollectedBasedOnVisualObservationOrSurnameIndicator = false
                                    },
                                    HMDA_ETHNICITY_ORIGINS = new HMDA_ETHNICITY_ORIGINS()
                                    {
                                        HMDA_ETHNICITY_ORIGIN = new EditableList<HMDA_ETHNICITY_ORIGIN>()
                                    },
                                    HMDA_RACES = new HMDA_RACES()
                                    {
                                        HMDA_RACE =  new EditableList<HMDA_RACE>()
                                    },
                                    EXTENSION = new EXTENSION()
                                    {
                                        OTHER = new OTHER()
                                        {
                                            GOVERNMENT_MONITORING_EXTENSION =  new GOVERNMENT_MONITORING_EXTENSION()
                                            {
                                                HMDA_ETHNICITIES = new HMDA_ETHNICITIES()
                                                {
                                                    HMDA_ETHNICITY = new EditableList<HMDA_ETHNICITY>()
                                                }
                                            }
                                        }
                                    }
                                },
                                RESIDENCES = new RESIDENCES()
                                {
                                    RESIDENCE = new EditableList<RESIDENCE>()
                                },
                                CURRENT_INCOME = new CURRENT_INCOME()
                                {
                                    CURRENT_INCOME_ITEMS = new CURRENT_INCOME_ITEMS()
                                    {
                                        CURRENT_INCOME_ITEM = new EditableList<CURRENT_INCOME_ITEM>()
                                    }
                                }
                            },
                            ROLE_DETAIL = new ROLE_DETAIL()
                            {
                                PartyRoleType = "Borrower"
                            },
                            SequenceNumber = 1,
                            Label = "BORROWER_1",
                        }
                    }
                }
                , new PARTY()
                {
                    SequenceNumber = 2,
                    TAXPAYER_IDENTIFIERS = new TAXPAYER_IDENTIFIERS()
                    {
                        TAXPAYER_IDENTIFIER = new TAXPAYER_IDENTIFIER()
                        {
                            SequenceNumber = 1,
                            TaxpayerIdentifierType = "SocialSecurityNumber"
                        }
                    },
                    INDIVIDUAL = new INDIVIDUAL()
                    {
                        CONTACT_POINTS = new CONTACT_POINTS
                        {
                            CONTACT_POINT = new List<CONTACT_POINT>
                            {
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_EMAIL = new CONTACT_POINT_EMAIL
                                    {
                                        ContactPointEmailValue = coBorrower.LoanContact.EmailAddress
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue = coBorrower.LoanContact.HomePhone
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Home"
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue = coBorrower.LoanContact.CellPhone
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Mobile"
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue =
                                            $"{coBorrower.LoanContact.WorkPhone}-{coBorrower.LoanContact.WorkPhoneExt}"
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Work"
                                    }
                                }
                            }
                        }
                        , NAME = new NAME()
                        {
                            FullName = null,
                            FirstName = coBorrower.LoanContact.FirstName,
                            LastName = coBorrower.LoanContact.LastName,
                            MiddleName = coBorrower.LoanContact.MiddleName,
                            SuffixName = coBorrower.LoanContact.Suffix,
                        }
                    },
                    ROLES = new ROLES()
                    {
                        ROLE = new ROLE()
                        {
                            BORROWER = new BORROWER()
                            {
                                BORROWER_DETAIL = new BORROWER_DETAIL()
                                {
                                    CommunityPropertyStateResidentIndicator = false,
                                    SelfDeclaredMilitaryServiceIndicator = false,
                                    DependentCount = coBorrower.NoOfDependent,
                                    MaritalStatusType = "Separated"
                                },
                                DECLARATION = new DECLARATION()
                                {
                                    DECLARATION_DETAIL = new DECLARATION_DETAIL()
                                },
                                DEPENDENTS = new DEPENDENTS()
                                {
                                    DEPENDENT = new List<DEPENDENT>()
                                    {
                                        new DEPENDENT()
                                        {
                                            DependentAgeYearsCount = "12",
                                            SequenceNumber = 1
                                        },
                                        new DEPENDENT()
                                        {
                                            DependentAgeYearsCount = "24",
                                            SequenceNumber = 2
                                        }
                                    }
                                },
                                EMPLOYERS = new EMPLOYERS()
                                {
                                    EMPLOYER = new List<EMPLOYER>()
                                },
                                GOVERNMENT_MONITORING = new GOVERNMENT_MONITORING()
                                {
                                    GOVERNMENT_MONITORING_DETAIL = new GOVERNMENT_MONITORING_DETAIL()
                                    {
                                        HMDAEthnicityCollectedBasedOnVisualObservationOrSurnameIndicator = false,
                                        HMDAGenderCollectedBasedOnVisualObservationOrNameIndicator = false,
                                        HMDAGenderRefusalIndicator = false,
                                        HMDARaceCollectedBasedOnVisualObservationOrSurnameIndicator = false
                                    },
                                    HMDA_ETHNICITY_ORIGINS = new HMDA_ETHNICITY_ORIGINS()
                                    {
                                        HMDA_ETHNICITY_ORIGIN = new EditableList<HMDA_ETHNICITY_ORIGIN>()
                                    },
                                    HMDA_RACES = new HMDA_RACES()
                                    {
                                        HMDA_RACE =  new EditableList<HMDA_RACE>()
                                    },
                                    EXTENSION = new EXTENSION()
                                    {
                                        OTHER = new OTHER()
                                        {
                                            GOVERNMENT_MONITORING_EXTENSION =  new GOVERNMENT_MONITORING_EXTENSION()
                                            {
                                                HMDA_ETHNICITIES = new HMDA_ETHNICITIES()
                                                {
                                                    HMDA_ETHNICITY = new EditableList<HMDA_ETHNICITY>()
                                                }
                                            }
                                        }
                                    }
                                },
                                RESIDENCES = new RESIDENCES()
                                {
                                    RESIDENCE = new EditableList<RESIDENCE>()
                                },
                                CURRENT_INCOME = new CURRENT_INCOME()
                                {
                                    CURRENT_INCOME_ITEMS = new CURRENT_INCOME_ITEMS()
                                    {
                                        CURRENT_INCOME_ITEM = new EditableList<CURRENT_INCOME_ITEM>()
                                    }
                                }
                            },
                            ROLE_DETAIL = new ROLE_DETAIL()
                            {
                                PartyRoleType = "Borrower"
                            },
                            SequenceNumber = 1,
                            Label = "BORROWER_2",
                        }
                    }
                }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowers(loanApplicaiton);
            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(results);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestMismoBorrowerWithSingleEmployer()
        {
            // Arrange
            OtherEmploymentIncome bonusEmploymentIncome = new OtherEmploymentIncome()
            {
                OtherIncomeTypeId = 2, // Bonus
                MonthlyIncome = 123
            };
            OtherEmploymentIncome overtimeEmploymentIncome = new OtherEmploymentIncome()
            {
                OtherIncomeTypeId = 1, // Overtime
                MonthlyIncome = 456
            };
            OtherEmploymentIncome comissionEmploymentIncome = new OtherEmploymentIncome()
            {
                OtherIncomeTypeId = 3, // Comissions
                MonthlyIncome = 789
            };
            EmploymentInfo primaryEmploymentInfo = new EmploymentInfo()
            {
                MonthlyBaseIncome = 12345,
                Phone = "963258741",
                Name = "I am primary employer.",
                AddressInfo = new AddressInfo()
                {
                    CountyName = "PrimaryEmployerCountyName",
                    CityName = "PrimaryEmployerCityName",
                    ZipCode = "PrimaryZipCode",
                    State = new State()
                    {
                        Abbreviation = "TX"
                    },
                    StreetAddress = "Primary Employer Street Address",
                    UnitNo = "123",
                    Country = new Country()
                    {
                        TwoLetterIsoCode = "US"
                    }
                },
                JobTypeId = 1,
                IsSelfEmployed = true,
                StartDate = DateTime.Now.AddYears(-12),
                OwnershipPercentage = 25,
                OtherEmploymentIncomes = new List<OtherEmploymentIncome>()
                      {
                          bonusEmploymentIncome,
                          overtimeEmploymentIncome,
                          comissionEmploymentIncome
                      }
            };
            Borrower primaryBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "MyFirstName",
                    MiddleName = "MyMiddleName",
                    LastName = "MyLastName",
                    Suffix = "MySuffix",
                    MaritalStatusId = 1 // Married
                },
                NoOfDependent = 1,
                DependentAge = "21",
                EmploymentInfoes = new List<EmploymentInfo>()
                {
                    primaryEmploymentInfo
                }
            };
            LoanApplication loanApplication = new LoanApplication()
            {
                Borrowers = new List<Borrower>()
                                                              {
                                                                  primaryBorrower
                                                              }
            };
            var toDate = loanApplication.CreatedOnUtc ?? DateTime.UtcNow;

            var expected = new List<PARTY>
            {
                new PARTY
                {
                    SequenceNumber = 1,
                    TAXPAYER_IDENTIFIERS = this.GetTaxPayerIdentifiers(1),
                    
                    INDIVIDUAL = this.GetMismoIndividual(primaryBorrower),
                    ROLES = new ROLES()
                    {
                        ROLE = new ROLE()
                        {
                            BORROWER = new BORROWER()
                            {
                                BORROWER_DETAIL = new BORROWER_DETAIL()
                                {
                                    CommunityPropertyStateResidentIndicator = false,
                                    SelfDeclaredMilitaryServiceIndicator = false,
                                    DependentCount = primaryBorrower.NoOfDependent,
                                    MaritalStatusType = "Married"
                                },
                                DECLARATION = new DECLARATION()
                                {
                                    DECLARATION_DETAIL = new DECLARATION_DETAIL()
                                },
                                DEPENDENTS = new DEPENDENTS()
                                {
                                    DEPENDENT = new List<DEPENDENT>()
                                    {
                                        new DEPENDENT()
                                        {
                                            DependentAgeYearsCount = primaryBorrower.DependentAge,
                                            SequenceNumber = 1
                                        }
                                    }
                                },
                                EMPLOYERS = new EMPLOYERS()
                                {
                                    EMPLOYER = new List<EMPLOYER>()
                                    {
                                        new EMPLOYER()
                                        {
                                            LEGAL_ENTITY = new LEGAL_ENTITY()
                                            {
                                                CONTACTS = new CONTACTS()
                                                {
                                                    CONTACT = new CONTACT()
                                                    {
                                                        CONTACT_POINTS = new CONTACT_POINTS()
                                                        {
                                                            CONTACT_POINT = new EditableList<CONTACT_POINT>()
                                                            {
                                                                new CONTACT_POINT()
                                                                {
                                                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE()
                                                                    {
                                                                        ContactPointTelephoneValue = primaryEmploymentInfo.Phone
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                },
                                                LEGAL_ENTITY_DETAIL = new LEGAL_ENTITY_DETAIL()
                                                {
                                                    FullName = primaryEmploymentInfo.Name
                                                }
                                            },
                                            ADDRESS = new ADDRESS()
                                            {
                                                AddressLineText = $"{primaryEmploymentInfo.AddressInfo.StreetAddress} #{primaryEmploymentInfo.AddressInfo.UnitNo}",
                                                CountryCode = primaryEmploymentInfo.AddressInfo.Country.TwoLetterIsoCode,
                                                CountryName = null,
                                                CityName = primaryEmploymentInfo.AddressInfo.CityName,
                                                PostalCode = primaryEmploymentInfo.AddressInfo.ZipCode,
                                                CountyName = primaryEmploymentInfo.AddressInfo.CountyName,
                                                StateCode = primaryEmploymentInfo.AddressInfo.State.Abbreviation
                                            },
                                            EMPLOYMENT = new EMPLOYMENT()
                                            {
                                                EmploymentBorrowerSelfEmployedIndicator = primaryEmploymentInfo.IsSelfEmployed == true,
                                                EmploymentClassificationType = "Primary",
                                                EmploymentMonthlyIncomeAmount = null,
                                                EmploymentPositionDescription = null,
                                                EmploymentStartDate = primaryEmploymentInfo.StartDate.Value.ToString("yyyy-MM-dd"),
                                                EmploymentStatusType = "Current",
                                                EmploymentTimeInLineOfWorkYearsCount = (decimal?)Math.Round(Math.Abs((primaryEmploymentInfo.StartDate.Value - toDate).TotalDays / 365), 2),
                                                EmploymentTimeInLineOfWorkMonthsCount = ((toDate.Year - primaryEmploymentInfo.StartDate.Value.Year) * 12) + (toDate.Month - primaryEmploymentInfo.StartDate.Value.Month),
                                                OwnershipInterestType = Convert.ToString(OwnershipInterestBase.GreaterThanOrEqualTo25Percent),
                                                SpecialBorrowerEmployerRelationshipIndicator = false,
                                                EmploymentEndDate = null
                                            },
                                            SequenceNumber = 1
                                            , Label = "EMPLOYER_1"
                                        },
                                    }
                                },
                                GOVERNMENT_MONITORING = new GOVERNMENT_MONITORING()
                                {
                                    GOVERNMENT_MONITORING_DETAIL = new GOVERNMENT_MONITORING_DETAIL()
                                    {
                                        HMDAEthnicityCollectedBasedOnVisualObservationOrSurnameIndicator = false,
                                        HMDAGenderCollectedBasedOnVisualObservationOrNameIndicator = false,
                                        HMDAGenderRefusalIndicator = false,
                                        HMDARaceCollectedBasedOnVisualObservationOrSurnameIndicator = false
                                    },
                                    HMDA_ETHNICITY_ORIGINS = new HMDA_ETHNICITY_ORIGINS()
                                    {
                                        HMDA_ETHNICITY_ORIGIN = new EditableList<HMDA_ETHNICITY_ORIGIN>()
                                    },
                                    HMDA_RACES = new HMDA_RACES()
                                    {
                                        HMDA_RACE =  new EditableList<HMDA_RACE>()
                                    },
                                    EXTENSION = new EXTENSION()
                                    {
                                        OTHER = new OTHER()
                                        {
                                            GOVERNMENT_MONITORING_EXTENSION =  new GOVERNMENT_MONITORING_EXTENSION()
                                            {
                                                HMDA_ETHNICITIES = new HMDA_ETHNICITIES()
                                                {
                                                    HMDA_ETHNICITY = new EditableList<HMDA_ETHNICITY>()
                                                }
                                            }
                                        }
                                    }
                                },
                                RESIDENCES = new RESIDENCES()
                                {
                                    RESIDENCE = new EditableList<RESIDENCE>()
                                },
                                CURRENT_INCOME = new CURRENT_INCOME()
                                {
                                    CURRENT_INCOME_ITEMS = new CURRENT_INCOME_ITEMS()
                                    {
                                        CURRENT_INCOME_ITEM = new EditableList<CURRENT_INCOME_ITEM>()
                                        {
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = primaryEmploymentInfo.MonthlyBaseIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = Convert.ToString(IncomeBase.Base)
                                                },
                                                SequenceNumber = 1,
                                                Label = "CURRENT_INCOME_ITEM_1"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = bonusEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Bonus",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 2,
                                                Label = "CURRENT_INCOME_ITEM_2"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = comissionEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Commissions",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 3,
                                                Label = "CURRENT_INCOME_ITEM_3"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = overtimeEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Overtime",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 4,
                                                Label = "CURRENT_INCOME_ITEM_4"
                                            }
                                        }
                                    }
                                }
                            },
                            ROLE_DETAIL = new ROLE_DETAIL()
                            {
                                PartyRoleType = "Borrower"
                            },
                            SequenceNumber = 1,
                            Label = "BORROWER_1",
                        }
                    }
                }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowers(loanApplication);
            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(results);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestMismoBorrowerWithMultipleEmployers()
        {
            // Arrange
            OtherEmploymentIncome bonusEmploymentIncome = new OtherEmploymentIncome()
            {
                OtherIncomeTypeId = 2, // Bonus
                MonthlyIncome = 123
            };
            OtherEmploymentIncome overtimeEmploymentIncome = new OtherEmploymentIncome()
            {
                OtherIncomeTypeId = 1, // Overtime
                MonthlyIncome = 456
            };
            OtherEmploymentIncome comissionEmploymentIncome = new OtherEmploymentIncome()
            {
                OtherIncomeTypeId = 3, // Comissions
                MonthlyIncome = 789
            };
            EmploymentInfo primaryEmploymentInfo = new EmploymentInfo()
            {
                MonthlyBaseIncome = 12345,
                Phone = "963258741",
                Name = "I am primary employer.",
                AddressInfo = new AddressInfo()
                {
                    CountyName = "PrimaryEmployerCountyName",
                    CityName = "PrimaryEmployerCityName",
                    ZipCode = "PrimaryZipCode",
                    State = new State()
                    {
                        Abbreviation = "TX"
                    },
                    StreetAddress = "Primary Employer Street Address",
                    UnitNo = "123",
                    Country = new Country()
                    {
                        TwoLetterIsoCode = "US"
                    }
                },
                JobTypeId = 1,
                IsSelfEmployed = true,
                StartDate = DateTime.Now.AddYears(-1),
                OwnershipPercentage = 25,
                OtherEmploymentIncomes = new List<OtherEmploymentIncome>()
                      {
                          bonusEmploymentIncome,
                          overtimeEmploymentIncome,
                          comissionEmploymentIncome
                      }
            };
            EmploymentInfo secondaryEmploymentInfo = new EmploymentInfo()
            {
                MonthlyBaseIncome = 12345,
                Phone = "963258741",
                Name = "I am secondary employer.",
                AddressInfo = null,
                JobTypeId = 1,
                IsSelfEmployed = true,
                StartDate = DateTime.Now.AddYears(-2),
                OwnershipPercentage = 24,
                OtherEmploymentIncomes = new List<OtherEmploymentIncome>()
                {
                    bonusEmploymentIncome,
                    overtimeEmploymentIncome,
                    comissionEmploymentIncome
                }
            };
            EmploymentInfo employmentInfoWithCountryObjectNull = new EmploymentInfo()
            {
                MonthlyBaseIncome = 12345,
                Phone = "963258741",
                Name = "I am third employer.",
                AddressInfo = new AddressInfo()
                {
                    CountyName = "ThirdEmployerCountyName",
                    CityName = "ThirdEmployerCityName",
                    ZipCode = "ThirdZipCode",
                    State = null,
                    StreetAddress = "Third Employer Street Address",
                    UnitNo = "123",
                    Country = null
                },
                JobTypeId = 1,
                IsSelfEmployed = true,
                StartDate = DateTime.Now.AddYears(-3),
                OwnershipPercentage = 24,
                OtherEmploymentIncomes = new List<OtherEmploymentIncome>()
                                                                              {
                                                                                  bonusEmploymentIncome,
                                                                                  overtimeEmploymentIncome,
                                                                                  comissionEmploymentIncome
                                                                              }
            };
            Borrower primaryBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "MyFirstName",
                    MiddleName = "MyMiddleName",
                    LastName = "MyLastName",
                    Suffix = "MySuffix",
                    MaritalStatusId = 1 // Married
                },
                NoOfDependent = 1,
                DependentAge = "21",
                EmploymentInfoes = new List<EmploymentInfo>()
                {
                    primaryEmploymentInfo,
                    secondaryEmploymentInfo,
                    employmentInfoWithCountryObjectNull
                }
            };
            LoanApplication loanApplication = new LoanApplication()
            {
                Borrowers = new List<Borrower>()
                                                              {
                                                                  primaryBorrower
                                                              }
            };
            var toDate = loanApplication.CreatedOnUtc ?? DateTime.UtcNow;
            int totalCurrentIncomesCount = 1;
            var expected = new List<PARTY>
            {
                new PARTY
                {
                    SequenceNumber = 1,
                    TAXPAYER_IDENTIFIERS = new TAXPAYER_IDENTIFIERS
                    {
                        TAXPAYER_IDENTIFIER = new TAXPAYER_IDENTIFIER
                        {
                            SequenceNumber = 1,
                            TaxpayerIdentifierType = "SocialSecurityNumber"
                        }
                    },
                    INDIVIDUAL = new INDIVIDUAL
                    {
                        CONTACT_POINTS = new CONTACT_POINTS
                        {
                            CONTACT_POINT = new List<CONTACT_POINT>
                            {
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_EMAIL = new CONTACT_POINT_EMAIL
                                    {
                                        ContactPointEmailValue = primaryBorrower.LoanContact.EmailAddress
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue = primaryBorrower.LoanContact.HomePhone
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Home"
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue = primaryBorrower.LoanContact.CellPhone
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Mobile"
                                    }
                                },
                                new CONTACT_POINT
                                {
                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                                    {
                                        ContactPointTelephoneValue =
                                            $"{primaryBorrower.LoanContact.WorkPhone}-{primaryBorrower.LoanContact.WorkPhoneExt}"
                                    },
                                    CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                                    {
                                        ContactPointRoleType = "Work"
                                    }
                                }
                            }
                        }
                        , NAME = new NAME()
                        {
                            FullName = null,
                            FirstName = primaryBorrower.LoanContact.FirstName,
                            LastName = primaryBorrower.LoanContact.LastName,
                            MiddleName = primaryBorrower.LoanContact.MiddleName,
                            SuffixName = primaryBorrower.LoanContact.Suffix,
                        }
                    },
                    ROLES = new ROLES()
                    {
                        ROLE = new ROLE()
                        {
                            BORROWER = new BORROWER()
                            {
                                BORROWER_DETAIL = this.GetBorrowerDetail(primaryBorrower),
                                DECLARATION = new DECLARATION()
                                {
                                    DECLARATION_DETAIL = new DECLARATION_DETAIL()
                                },
                                
                                DEPENDENTS = this.GetBorrowerDependents(primaryBorrower, 1),
                                EMPLOYERS = new EMPLOYERS()
                                {
                                    EMPLOYER = new List<EMPLOYER>()
                                    {
                                        #region Primary Employer
                                        this.GetMismoEmployer(primaryEmploymentInfo, EmploymentClassificationBase.Primary, EmploymentStatusBase.Current, 1, 1),
                                        #endregion
                                        this.GetMismoEmployer(secondaryEmploymentInfo, EmploymentClassificationBase.Secondary, EmploymentStatusBase.Current, 1, 2),
                                        this.GetMismoEmployer(employmentInfoWithCountryObjectNull, EmploymentClassificationBase.Secondary, EmploymentStatusBase.Current, 1, 3)
                                    }
                                },
                                GOVERNMENT_MONITORING = new GOVERNMENT_MONITORING()
                                {
                                    GOVERNMENT_MONITORING_DETAIL = new GOVERNMENT_MONITORING_DETAIL()
                                    {
                                        HMDAEthnicityCollectedBasedOnVisualObservationOrSurnameIndicator = false,
                                        HMDAGenderCollectedBasedOnVisualObservationOrNameIndicator = false,
                                        HMDAGenderRefusalIndicator = false,
                                        HMDARaceCollectedBasedOnVisualObservationOrSurnameIndicator = false
                                    },
                                    HMDA_ETHNICITY_ORIGINS = new HMDA_ETHNICITY_ORIGINS()
                                    {
                                        HMDA_ETHNICITY_ORIGIN = new EditableList<HMDA_ETHNICITY_ORIGIN>()
                                    },
                                    HMDA_RACES = new HMDA_RACES()
                                    {
                                        HMDA_RACE =  new EditableList<HMDA_RACE>()
                                    },
                                    EXTENSION = new EXTENSION()
                                    {
                                        OTHER = new OTHER()
                                        {
                                            GOVERNMENT_MONITORING_EXTENSION =  new GOVERNMENT_MONITORING_EXTENSION()
                                            {
                                                HMDA_ETHNICITIES = new HMDA_ETHNICITIES()
                                                {
                                                    HMDA_ETHNICITY = new EditableList<HMDA_ETHNICITY>()
                                                }
                                            }
                                        }
                                    }
                                },
                                RESIDENCES = new RESIDENCES()
                                {
                                    RESIDENCE = new EditableList<RESIDENCE>()
                                },
                                CURRENT_INCOME = new CURRENT_INCOME()
                                {
                                    CURRENT_INCOME_ITEMS = new CURRENT_INCOME_ITEMS()
                                    {
                                        CURRENT_INCOME_ITEM = new EditableList<CURRENT_INCOME_ITEM>()
                                        {
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = primaryEmploymentInfo.MonthlyBaseIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Base",
                                                },
                                                SequenceNumber = 1,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = bonusEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Bonus",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 2,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = comissionEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Commissions",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 3,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = overtimeEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Overtime",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 4,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = secondaryEmploymentInfo.MonthlyBaseIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Base",
                                                },
                                                SequenceNumber = 5,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = bonusEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Bonus",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 6,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = comissionEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Commissions",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 7,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = overtimeEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Overtime",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 8,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = employmentInfoWithCountryObjectNull.MonthlyBaseIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Base",
                                                },
                                                SequenceNumber = 9,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = bonusEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Bonus",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 10,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = comissionEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Commissions",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 11,
                                                Label = "CURRENT_INCOME_ITEM_11"
                                            },
                                            new CURRENT_INCOME_ITEM()
                                            {
                                                CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                                {
                                                    CurrentIncomeMonthlyTotalAmount = overtimeEmploymentIncome.MonthlyIncome,
                                                    EmploymentIncomeIndicator = true,
                                                    IncomeType = "Overtime",
                                                    SeasonalIncomeIndicator = false
                                                },
                                                SequenceNumber = 12,
                                                Label = $"CURRENT_INCOME_ITEM_{totalCurrentIncomesCount++}"
                                            }
                                        }
                                    }
                                }
                            },
                            ROLE_DETAIL = new ROLE_DETAIL()
                            {
                                PartyRoleType = "Borrower"
                            },
                            SequenceNumber = 1,
                            Label = "BORROWER_1",
                        }
                    }
                }
            };


            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowers(loanApplication);



            // Assert
            Assert.NotNull(results);

            var expectedJson = JsonConvert.SerializeObject(expected[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER[0]);
            var actualJson = JsonConvert.SerializeObject(results[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER[0]);
            Assert.Equal(expectedJson, actualJson);

            expectedJson = JsonConvert.SerializeObject(expected[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER[1]);
            actualJson = JsonConvert.SerializeObject(results[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER[1]);
            Assert.Equal(expectedJson, actualJson);

            expectedJson = JsonConvert.SerializeObject(expected[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER[2]);
            actualJson = JsonConvert.SerializeObject(results[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER[2]);
            Assert.Equal(expectedJson, actualJson);

            expectedJson = JsonConvert.SerializeObject(expected);
            actualJson = JsonConvert.SerializeObject(results);
        }

        [Fact]
        public void TestMismoBorrowerWithPreviousEmployers()
        {
            // Arrange
            EmploymentInfo previousEmploymentInfo1 = new EmploymentInfo()
            {
                MonthlyBaseIncome = 12345,
                Phone = "963258741",
                Name = "I am primary employer.",
                AddressInfo = new AddressInfo()
                {
                    CountyName = "PrimaryEmployerCountyName",
                    CityName = "PrimaryEmployerCityName",
                    ZipCode = "PrimaryZipCode",
                    State = new State()
                    {
                        Abbreviation = "TX"
                    },
                    StreetAddress = "Primary Employer Street Address",
                    UnitNo = "123",
                    Country = new Country()
                    {
                        TwoLetterIsoCode = "US"
                    }
                },
                JobTypeId = 1,
                StartDate = DateTime.Now.AddYears(-12),
                EndDate = DateTime.UtcNow.AddYears(-10)
            };
            EmploymentInfo previousEmploymentInfo2 = new EmploymentInfo()
            {
                MonthlyBaseIncome = 12345,
                Phone = "963258741",
                Name = "I am previous employer.",
                AddressInfo = new AddressInfo()
                {
                    CountyName = "PreviousEmployerCountyName",
                    CityName = "PreviousEmployerCityName",
                    ZipCode = "PreviousZipCode",
                    State = new State()
                    {
                        Abbreviation = "TX"
                    },
                    StreetAddress = "Previous Employer Street Address",
                    UnitNo = "123",
                    Country = new Country()
                    {
                        TwoLetterIsoCode = "US"
                    }
                },
                JobTypeId = 1,
                StartDate = DateTime.Now.AddYears(-12),
                EndDate = DateTime.UtcNow.AddYears(-10),
                OwnershipPercentage = 25
            };
            EmploymentInfo previousEmploymentInfo3 = new EmploymentInfo()
            {
                MonthlyBaseIncome = 12345,
                Phone = "963258741",
                Name = "I am Previous employer 2.",
                AddressInfo = new AddressInfo()
                {
                    CountyName = "PreviousEmployerCountyName2",
                    CityName = "PreviousEmployerCityName2",
                    ZipCode = "PreviousZipCode2",
                    State = new State()
                    {
                        Abbreviation = "TX"
                    },
                    StreetAddress = "Previous Employer Street Address 2",
                    UnitNo = "123",
                    Country = new Country()
                    {
                        TwoLetterIsoCode = "US"
                    }
                },
                JobTypeId = 1,
                StartDate = DateTime.Now.AddYears(-12),
                EndDate = DateTime.UtcNow.AddYears(-10),
                OwnershipPercentage = 24
            };
            Borrower primaryBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "MyFirstName",
                    MiddleName = "MyMiddleName",
                    LastName = "MyLastName",
                    Suffix = "MySuffix",
                    MaritalStatusId = 1 // Married
                },
                NoOfDependent = 1,
                DependentAge = "21",
                EmploymentInfoes = new List<EmploymentInfo>()
                {
                    previousEmploymentInfo1,
                    previousEmploymentInfo2,
                    previousEmploymentInfo3
                }
            };

            LoanApplication loanApplicaiton = new LoanApplication()
            {
                Borrowers = new List<Borrower>()
                  {
                      primaryBorrower
                  }
            };

            List<PARTY> expectedObj = new EditableList<PARTY>()
            {
                new PARTY()
                {
                    TAXPAYER_IDENTIFIERS = this.GetTaxPayerIdentifiers(1),
                    INDIVIDUAL = this.GetMismoIndividual(primaryBorrower),
                    ROLES = new ROLES()
                    {
                        ROLE = new ROLE()
                        {
                            BORROWER = new BORROWER()
                            {
                                BORROWER_DETAIL = this.GetBorrowerDetail(primaryBorrower),
                                DECLARATION = new DECLARATION()
                                {
                                    DECLARATION_DETAIL = new DECLARATION_DETAIL()
                                },
                                DEPENDENTS = this.GetBorrowerDependents(primaryBorrower, 1),
                                EMPLOYERS = new EMPLOYERS()
                                {
                                    EMPLOYER = new EditableList<EMPLOYER>()
                                    {
                                        this.GetMismoEmployer(previousEmploymentInfo1, null, EmploymentStatusBase.Previous, 1, 1),
                                        this.GetMismoEmployer(previousEmploymentInfo2, null, EmploymentStatusBase.Previous, 2, 2),
                                        this.GetMismoEmployer(previousEmploymentInfo3, null, EmploymentStatusBase.Previous, 3, 3),
                                    }
                                },
                                GOVERNMENT_MONITORING = this.GetEmptyGovernmentMonitoring(),
                                RESIDENCES = new RESIDENCES()
                                {
                                    RESIDENCE = new EditableList<RESIDENCE>()
                                }
                            },
                        }
                    }
                }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowers(loanApplicaiton);

            // Assert
            Assert.NotNull(results);
            Assert.NotNull(results[0].ROLES);
            Assert.NotNull(results[0].ROLES.ROLE);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.EMPLOYERS);
            Assert.Equal(expectedObj[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER.Count, results[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER.Count);

            var expectedJson = JsonConvert.SerializeObject(expectedObj[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER);
            var actualJson = JsonConvert.SerializeObject(results[0].ROLES.ROLE.BORROWER.EMPLOYERS.EMPLOYER);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void TestMismoMultipleBorrowersWithQuestionResponses()
        {
            // Arrange
            Borrower primaryBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "MyFirstName",
                    MiddleName = "MyMiddleName",
                    LastName = "MyLastName",
                    Suffix = "MySuffix",
                    MaritalStatusId = 1 // Married
                },
                BorrowerQuestionResponses = new List<BorrowerQuestionResponse>()
                {
                    new BorrowerQuestionResponse()
                        {QuestionId = 36, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}}, // LoanForeclosureOrJudgementIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 37, QuestionResponse = new QuestionResponse() {AnswerText = "1"}}, // Bankruptcy
                    new BorrowerQuestionResponse()
                        {QuestionId = 38, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}}, // PropertyForeclosedPastSevenYearsIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 39, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}}, // PartyToLawsuitIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 40, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}}, // OutstandingJudgementsIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 41, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}}, // PresentlyDelinquentIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 42, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}}, // BorrowedDownPaymentIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 43, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}}, // UndisclosedComakerOfNoteIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 44, QuestionResponse = new QuestionResponse() {AnswerText = "1"}}, // DeclarationsJIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 45, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}}, // IntentToOccupyIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 46, QuestionResponse = new QuestionResponse() {AnswerText = "1"}}, // AlimonyChildSupportObligationIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 47, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}}, // HomeownerPastThreeYearsIndicator
                    new BorrowerQuestionResponse()
                        {QuestionId = 49, QuestionResponse = new QuestionResponse() {AnswerText = null}}, // PriorPropertyUsageType
                    new BorrowerQuestionResponse()
                        {QuestionId = 50, QuestionResponse = new QuestionResponse() {AnswerText = "1"}}, // PriorPropertyTitleType
                    new BorrowerQuestionResponse()
                        {QuestionId = 54, QuestionResponse = new QuestionResponse() {AnswerText = "1"}}, // DeclarationsKIndicator
                },
                AssetBorrowerBinders = new List<AssetBorrowerBinder>()
                {
                    new AssetBorrowerBinder()
                    {
                        BorrowerAsset = new BorrowerAsset()
                        {
                            AssetTypeId = 11,
                            UseForDownpayment = 1234
                        }
                    }
                }
            };
            Borrower coBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "coborrower@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "MyFirstName",
                    MiddleName = "MyMiddleName",
                    LastName = "MyLastName",
                    Suffix = "MySuffix",
                    MaritalStatusId = 1 // Married
                },
                BorrowerQuestionResponses = new List<BorrowerQuestionResponse>()
                {
                    new BorrowerQuestionResponse()
                        {QuestionId = 36, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 37, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 38, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 39, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 40, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 41, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 42, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 43, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 44, QuestionResponse = new QuestionResponse() {AnswerText = "0"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 45, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 46, QuestionResponse = new QuestionResponse() {AnswerText = "1"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 47, QuestionResponse = new QuestionResponse() {AnswerText = "Yes"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 49, QuestionResponse = new QuestionResponse() {AnswerText = "1"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 50, QuestionResponse = new QuestionResponse() {AnswerText = "1"}},
                    new BorrowerQuestionResponse()
                        {QuestionId = 54, QuestionResponse = new QuestionResponse() {AnswerText = "1"}},
                }
            };
            LoanApplication loanApplicaiton = new LoanApplication()
            {
                Borrowers = new List<Borrower>()
                  {
                      primaryBorrower,
                      coBorrower
                  }
            };



            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowers(loanApplicaiton);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(2, results.Count);

            Assert.NotNull(results[0].ROLES);
            Assert.NotNull(results[0].ROLES.ROLE);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.DECLARATION);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL);
            Assert.True(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.BankruptcyIndicator);
            Assert.Equal(Convert.ToString(CitizenshipResidencyBase.USCitizen), results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CitizenshipResidencyType);
            Assert.Equal("No", results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.HomeownerPastThreeYearsType);
            Assert.Equal("No", results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.IntentToOccupyType);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.OutstandingJudgmentsIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PartyToLawsuitIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PresentlyDelinquentIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyForeclosureCompletedIndicator);
            Assert.Equal(Convert.ToString(PriorPropertyTitleBase.Sole), results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyTitleType);
            Assert.Equal(Convert.ToString(PriorPropertyUsageBase.PrimaryResidence), results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyUsageType);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedComakerOfNoteIndicator);
            Assert.True(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.AlimonyChildSupportObligationIndicator);
            Assert.Null(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CoMakerEndorserOfNoteIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.LoanForeclosureOrJudgmentIndicator);
            Assert.Equal(1234, results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsAmount); // Downpayment asset

            Assert.NotNull(results[1].ROLES);
            Assert.NotNull(results[1].ROLES.ROLE);
            Assert.NotNull(results[1].ROLES.ROLE.BORROWER);
            Assert.NotNull(results[1].ROLES.ROLE.BORROWER.DECLARATION);
            Assert.NotNull(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.BankruptcyIndicator);
            Assert.Equal(Convert.ToString(CitizenshipResidencyBase.PermanentResidentAlien), results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CitizenshipResidencyType);
            Assert.Equal("No", results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.HomeownerPastThreeYearsType);
            Assert.Equal("No", results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.IntentToOccupyType);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.OutstandingJudgmentsIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PartyToLawsuitIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PresentlyDelinquentIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyForeclosureCompletedIndicator);
            Assert.Equal(Convert.ToString(PriorPropertyTitleBase.Sole), results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyTitleType);
            Assert.Equal(Convert.ToString(PriorPropertyUsageBase.PrimaryResidence), results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyUsageType);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedComakerOfNoteIndicator);
            Assert.True(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.AlimonyChildSupportObligationIndicator);
            Assert.Null(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CoMakerEndorserOfNoteIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.LoanForeclosureOrJudgmentIndicator);
            Assert.Null(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsAmount); // Downpayment asset
        }

        [Fact]
        public void TestQuestionResponsesWhenNonResidentAlien()
        {
            // Arrange
            LoanApplication loanApplicaiton = new LoanApplication()
            {
                Borrowers = new List<Borrower>()
                  {
                      new Borrower()
                      {
                          LoanContact = new LoanContact()
                                        {
                                            EmailAddress = "abc@xyz.com",
                                            HomePhone = "123456789",
                                            CellPhone = "987654321",
                                            WorkPhone = "147852369",
                                            FirstName = "MyFirstName",
                                            MiddleName = "MyMiddleName",
                                            LastName = "MyLastName",
                                            Suffix = "MySuffix",
                                            MaritalStatusId = 1 // Married
                                        },
                          BorrowerQuestionResponses = new List<BorrowerQuestionResponse>()
                                                      {
                                                          new BorrowerQuestionResponse() { QuestionId = 36, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 37, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 38, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 39, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 40, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 41, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 42, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 43, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 44, QuestionResponse = new QuestionResponse() { AnswerText = "0"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 45, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 46, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 47, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 49, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 50, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 54, QuestionResponse = new QuestionResponse() { AnswerText = "0"}},
                                                      }
                      }
                  }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowers(loanApplicaiton);

            // Assert
            Assert.NotNull(results);
           

            Assert.NotNull(results[0].ROLES);
            Assert.NotNull(results[0].ROLES.ROLE);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.DECLARATION);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL);
            Assert.True(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.BankruptcyIndicator);
            Assert.Equal(Convert.ToString(CitizenshipResidencyBase.NonResidentAlien), results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CitizenshipResidencyType);
            Assert.Equal("No", results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.HomeownerPastThreeYearsType);
            Assert.Equal("No", results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.IntentToOccupyType);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.OutstandingJudgmentsIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PartyToLawsuitIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PresentlyDelinquentIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyForeclosureCompletedIndicator);
            Assert.Equal(Convert.ToString(PriorPropertyTitleBase.Sole), results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyTitleType);
            Assert.Equal(Convert.ToString(PriorPropertyUsageBase.PrimaryResidence), results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyUsageType);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedComakerOfNoteIndicator);
            Assert.True(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.AlimonyChildSupportObligationIndicator);
            Assert.Null(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CoMakerEndorserOfNoteIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.LoanForeclosureOrJudgmentIndicator);
            Assert.Null(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsAmount); // Downpayment asset
        }

        [Fact]
        public void TestQuestionResponsesWhenPermanentResidentAlien()
        {
            // Arrange
            LoanApplication loanApplicaiton = new LoanApplication()
            {
                Borrowers = new List<Borrower>()
                  {
                      new Borrower()
                      {
                          LoanContact = new LoanContact()
                                        {
                                            EmailAddress = "abc@xyz.com",
                                            HomePhone = "123456789",
                                            CellPhone = "987654321",
                                            WorkPhone = "147852369",
                                            FirstName = "MyFirstName",
                                            MiddleName = "MyMiddleName",
                                            LastName = "MyLastName",
                                            Suffix = "MySuffix",
                                            MaritalStatusId = 1 // Married
                                        },
                          BorrowerQuestionResponses = new List<BorrowerQuestionResponse>()
                                                      {
                                                          new BorrowerQuestionResponse() { QuestionId = 36, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 37, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 38, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 39, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 40, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 41, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 42, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 43, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 44, QuestionResponse = new QuestionResponse() { AnswerText = "0"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 45, QuestionResponse = new QuestionResponse() { AnswerText = "0"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 46, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 47, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 49, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 50, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 54, QuestionResponse = new QuestionResponse() { AnswerText = "0"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 57, QuestionResponse = new QuestionResponse() { AnswerText = "3"}},
                                                      }
                      },
                      new Borrower()
                      {
                          LoanContact = new LoanContact()
                                        {
                                            EmailAddress = "abc@xyz.com",
                                            HomePhone = "123456789",
                                            CellPhone = "987654321",
                                            WorkPhone = "147852369",
                                            FirstName = "MyFirstName",
                                            MiddleName = "MyMiddleName",
                                            LastName = "MyLastName",
                                            Suffix = "MySuffix",
                                            MaritalStatusId = 1 // Married
                                        },
                          BorrowerQuestionResponses = new List<BorrowerQuestionResponse>()
                                                      {
                                                          new BorrowerQuestionResponse() { QuestionId = 36, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 37, QuestionResponse = new QuestionResponse() { AnswerText = "0"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 38, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 39, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 40, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 41, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 42, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 43, QuestionResponse = new QuestionResponse() { AnswerText = "Yes"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 44, QuestionResponse = new QuestionResponse() { AnswerText = "0"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 45, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 46, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 47, QuestionResponse = new QuestionResponse() { AnswerText = "0"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 49, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 50, QuestionResponse = new QuestionResponse() { AnswerText = "1"}},
                                                          new BorrowerQuestionResponse() { QuestionId = 54, QuestionResponse = new QuestionResponse() { AnswerText = "1"}}, // PermanentResidentAlien
                                                          new BorrowerQuestionResponse() { QuestionId = 57, QuestionResponse = new QuestionResponse() { AnswerText = "5"}},
                                                      }
                      }
                  }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowers(loanApplicaiton);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(loanApplicaiton.Borrowers.Count, results.Count);

            Assert.NotNull(results[0].ROLES);
            Assert.NotNull(results[0].ROLES.ROLE);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.DECLARATION);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL);
            Assert.True(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.BankruptcyIndicator);
            Assert.Equal(Convert.ToString(CitizenshipResidencyBase.NonPermanentResidentAlien), results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CitizenshipResidencyType);
            Assert.Equal("Yes", results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.HomeownerPastThreeYearsType);
            Assert.Equal("No", results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.IntentToOccupyType);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.OutstandingJudgmentsIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PartyToLawsuitIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PresentlyDelinquentIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyForeclosureCompletedIndicator);
            Assert.Equal(Convert.ToString(PriorPropertyTitleBase.Sole), results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyTitleType);
            Assert.Equal(Convert.ToString(PriorPropertyUsageBase.PrimaryResidence), results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyUsageType);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedComakerOfNoteIndicator);
            Assert.True(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.AlimonyChildSupportObligationIndicator);
            Assert.Null(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CoMakerEndorserOfNoteIndicator);
            Assert.False(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.LoanForeclosureOrJudgmentIndicator);
            Assert.Null(results[0].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsAmount); // Downpayment asset

            Assert.NotNull(results[1].ROLES);
            Assert.NotNull(results[1].ROLES.ROLE);
            Assert.NotNull(results[1].ROLES.ROLE.BORROWER);
            Assert.NotNull(results[1].ROLES.ROLE.BORROWER.DECLARATION);
            Assert.NotNull(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.BankruptcyIndicator);
            Assert.Equal(Convert.ToString(CitizenshipResidencyBase.PermanentResidentAlien), results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CitizenshipResidencyType);
            Assert.Equal("No", results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.HomeownerPastThreeYearsType);
            Assert.Equal("Yes", results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.IntentToOccupyType);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.OutstandingJudgmentsIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PartyToLawsuitIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PresentlyDelinquentIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyForeclosureCompletedIndicator);
            Assert.Equal(Convert.ToString(PriorPropertyTitleBase.Sole), results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyTitleType);
            Assert.Equal(Convert.ToString(PriorPropertyUsageBase.PrimaryResidence), results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.PriorPropertyUsageType);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedComakerOfNoteIndicator);
            Assert.True(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.AlimonyChildSupportObligationIndicator);
            Assert.Null(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.CoMakerEndorserOfNoteIndicator);
            Assert.False(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.LoanForeclosureOrJudgmentIndicator);
            Assert.Null(results[1].ROLES.ROLE.BORROWER.DECLARATION.DECLARATION_DETAIL.UndisclosedBorrowedFundsAmount); // Downpayment asset
        }

        [Fact]
        public void TestBorrowerWithOtherIncome()
        {
            // Arrange
            OtherIncome autoMobileIncome = new OtherIncome()
            {
                MonthlyAmount = 123,
                IncomeTypeId = 2 // AutomobileAllowance
            };
            OtherIncome unIdentifiedIncome = new OtherIncome()
            {
                MonthlyAmount = 123,
                IncomeTypeId = 999 // For unidentified type
            };
            OtherIncome seasonalIncome = new OtherIncome()
            {
                MonthlyAmount = 123,
                IncomeTypeId = 25 // Seasonal Income
            };

            Borrower primaryBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "MyFirstName",
                    MiddleName = "MyMiddleName",
                    LastName = "MyLastName",
                    Suffix = "MySuffix",
                    MaritalStatusId = 1 // Married
                },
                OtherIncomes = new List<OtherIncome>()
                {
                    autoMobileIncome,
                    unIdentifiedIncome,
                    seasonalIncome
                }
            };
            LoanApplication loanApplicaiton = new LoanApplication()
            {
                Borrowers = new List<Borrower>()
                {
                      primaryBorrower
                }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowers(loanApplicaiton);


            // Assert
            Assert.NotNull(results);

            // Automobile Income
            Assert.NotNull(results[0].ROLES);
            Assert.NotNull(results[0].ROLES.ROLE);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME.CURRENT_INCOME_ITEMS);
            Assert.NotNull(results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME.CURRENT_INCOME_ITEMS.CURRENT_INCOME_ITEM);
            Assert.Equal(primaryBorrower.OtherIncomes.Count, results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME.CURRENT_INCOME_ITEMS.CURRENT_INCOME_ITEM.Count);
            Assert.Equal(autoMobileIncome.MonthlyAmount, results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME.CURRENT_INCOME_ITEMS.CURRENT_INCOME_ITEM[0].CURRENT_INCOME_ITEM_DETAIL.CurrentIncomeMonthlyTotalAmount);
            Assert.Equal(Convert.ToString(IncomeBase.AutomobileAllowance), results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME.CURRENT_INCOME_ITEMS.CURRENT_INCOME_ITEM[0].CURRENT_INCOME_ITEM_DETAIL.IncomeType);

            // Unmapped income
            Assert.Equal(unIdentifiedIncome.MonthlyAmount, results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME.CURRENT_INCOME_ITEMS.CURRENT_INCOME_ITEM[1].CURRENT_INCOME_ITEM_DETAIL.CurrentIncomeMonthlyTotalAmount);
            Assert.Equal(Convert.ToString(IncomeBase.Other), results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME.CURRENT_INCOME_ITEMS.CURRENT_INCOME_ITEM[1].CURRENT_INCOME_ITEM_DETAIL.IncomeType);

            // Seasonal income
            Assert.Equal(seasonalIncome.MonthlyAmount, results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME.CURRENT_INCOME_ITEMS.CURRENT_INCOME_ITEM[2].CURRENT_INCOME_ITEM_DETAIL.CurrentIncomeMonthlyTotalAmount);
            Assert.Equal("Seasonal Income", results[0].ROLES.ROLE.BORROWER.CURRENT_INCOME.CURRENT_INCOME_ITEMS.CURRENT_INCOME_ITEM[2].CURRENT_INCOME_ITEM_DETAIL.IncomeTypeOtherDescription);
        }

        [Fact]
        public void TestGovernmentMonitoringWithEthnicityRefusalAndGender()
        {
            Race asianRace = new Race()
            {
                Id = 2, // Asian
                Name = "Asian",
            };
            RaceDetail japneseRaceDetail = new RaceDetail()
            {
                RaceId = 2,
                Id = 4,
                Name = "Japanese"
            };
            Borrower rmBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "MyFirstName",
                    MiddleName = "MyMiddleName",
                    LastName = "MyLastName",
                    Suffix = "MySuffix",
                    MaritalStatusId = 1, // Married
                    GenderId = 2,
                    LoanContactEthnicityBinders = new List<LoanContactEthnicityBinder>()
                                                                                      {
                                                                                          new LoanContactEthnicityBinder()
                                                                                          {
                                                                                              EthnicityId = 3, // Do not wish to provide ethnicity detail
                                                                                          },
                                                                                      },
                    LoanContactRaceBinders = new List<LoanContactRaceBinder>()
                                                                                 {
                                                                                     new LoanContactRaceBinder()
                                                                                     {
                                                                                         RaceId = 6, // Do not wish to provide race detail
                                                                                         Race = asianRace,
                                                                                         RaceDetail = japneseRaceDetail
                                                                                     },
                                                                                 }
                }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetGovernmentMonitoring(rmBorrower);

            // Assert
            Assert.NotNull(results);
            Assert.NotNull(results.GOVERNMENT_MONITORING_DETAIL);
            Assert.True(results.GOVERNMENT_MONITORING_DETAIL.HMDAEthnicityRefusalIndicator); // Ethnicity Refusal
            Assert.True(results.GOVERNMENT_MONITORING_DETAIL.HMDARaceRefusalIndicator); // Race Refusal
            Assert.NotNull(results.GOVERNMENT_MONITORING_DETAIL.EXTENSION);
            Assert.NotNull(results.GOVERNMENT_MONITORING_DETAIL.EXTENSION.OTHER);
            Assert.NotNull(results.GOVERNMENT_MONITORING_DETAIL.EXTENSION.OTHER.GOVERNMENT_MONITORING_DETAIL_EXTENSION);
            Assert.Equal(Convert.ToString(GenderBase.Male), results.GOVERNMENT_MONITORING_DETAIL.EXTENSION.OTHER.GOVERNMENT_MONITORING_DETAIL_EXTENSION.HMDAGenderType);
            Assert.NotNull(results.HMDA_RACES);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE);
            Assert.NotEmpty(results.HMDA_RACES.HMDA_RACE);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0]);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION);
            Assert.NotEmpty(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION.OTHER);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION);
            Assert.Equal(japneseRaceDetail.Name, results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION.HMDARaceDesignationType);
        }

        [Fact]
        public void TestGovernmentMonitoringWhenNativeOrOtherPacificIslander()
        {
            EthnicityDetail puertoRicanEthDetail = new EthnicityDetail()
            {
                Id = 2,
                Name = "Puerto Rican",
            };

            Race asianRace = new Race()
            {
                Id = 2, // Asian
                Name = "Asian",
            };
            RaceDetail japanseRaceDetail = new RaceDetail()
            {
                RaceId = 2,
                Id = 4,
                Name = "Japanese"
            };
            Race nativeHawRace = new Race()
            {
                Id = 4, // Native Hawaiian or Other Pacific Islander
                Name = "Native Hawaiian or Other Pacific Islander",
            };
            RaceDetail nativeHawRaceDetail = new RaceDetail()
            {
                Id = 11,
                RaceId = 4,
                Name = "Other Pacific Islander"
            };

            Borrower rmBorrower = new Borrower()
            {
                LoanContact = new LoanContact()
                {
                    EmailAddress = "abc@xyz.com",
                    HomePhone = "123456789",
                    CellPhone = "987654321",
                    WorkPhone = "147852369",
                    FirstName = "MyFirstName",
                    MiddleName = "MyMiddleName",
                    LastName = "MyLastName",
                    Suffix = "MySuffix",
                    MaritalStatusId = 1, // Married
                    LoanContactEthnicityBinders = new List<LoanContactEthnicityBinder>()
                                                                                      {
                                                                                          new LoanContactEthnicityBinder()
                                                                                          {
                                                                                              EthnicityId = 1, // Hispanic Or Latino
                                                                                              Ethnicity = new Ethnicity()
                                                                                                          {
                                                                                                              Id = 1
                                                                                                          },
                                                                                              EthnicityDetailId = 2,
                                                                                              EthnicityDetail = puertoRicanEthDetail
                                                                                          },

                                                                                      },
                    LoanContactRaceBinders = new List<LoanContactRaceBinder>()
                                                                                 {
                                                                                     new LoanContactRaceBinder()
                                                                                     {
                                                                                         RaceId = 2, // Do not wish to provide race detail
                                                                                         Race = asianRace,
                                                                                         RaceDetail = japanseRaceDetail
                                                                                     },
                                                                                     new LoanContactRaceBinder()
                                                                                     {
                                                                                         RaceId = 4, // Native Hawaiian or Other Pacific Islander
                                                                                         Race = nativeHawRace,
                                                                                         RaceDetail = nativeHawRaceDetail
                                                                                     }
                                                                                 }
                }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetGovernmentMonitoring(rmBorrower);

           
            // Assert
            Assert.NotNull(results);
            Assert.NotNull(results.GOVERNMENT_MONITORING_DETAIL);
            Assert.NotNull(results.HMDA_ETHNICITY_ORIGINS);
            Assert.NotNull(results.HMDA_ETHNICITY_ORIGINS.HMDA_ETHNICITY_ORIGIN);
            Assert.NotEmpty(results.HMDA_ETHNICITY_ORIGINS.HMDA_ETHNICITY_ORIGIN);
            Assert.Equal(Convert.ToString(HMDAEthnicityOriginBase.PuertoRican), results.HMDA_ETHNICITY_ORIGINS.HMDA_ETHNICITY_ORIGIN[0].HMDAEthnicityOriginType); // Match Ethnicity Origin

            Assert.NotNull(results.HMDA_RACES);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE);
            Assert.NotEmpty(results.HMDA_RACES.HMDA_RACE);

            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0]);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION);
            Assert.NotEmpty(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0]);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION.OTHER);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION);
            Assert.Equal(Convert.ToString(HMDARaceDesignationBase.Japanese), results.HMDA_RACES.HMDA_RACE[0].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION.HMDARaceDesignationType);

            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[1]);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DESIGNATIONS);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION);
            Assert.NotEmpty(results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0]);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION.OTHER);
            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION);
            Assert.Equal("OtherPacificIslander", results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION[0].EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION.HMDARaceDesignationType);

            Assert.NotNull(results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DETAIL);
            Assert.Equal("NativeHawaiianorOtherPacificIslander", results.HMDA_RACES.HMDA_RACE[1].HMDA_RACE_DETAIL.HMDARaceType);

            Assert.NotNull(results.EXTENSION);
            Assert.NotNull(results.EXTENSION.OTHER);
            Assert.NotNull(results.EXTENSION.OTHER.GOVERNMENT_MONITORING_EXTENSION);
            Assert.NotNull(results.EXTENSION.OTHER.GOVERNMENT_MONITORING_EXTENSION.HMDA_ETHNICITIES);
            Assert.NotNull(results.EXTENSION.OTHER.GOVERNMENT_MONITORING_EXTENSION.HMDA_ETHNICITIES.HMDA_ETHNICITY);
            Assert.NotEmpty(results.EXTENSION.OTHER.GOVERNMENT_MONITORING_EXTENSION.HMDA_ETHNICITIES.HMDA_ETHNICITY);
            Assert.Equal(Convert.ToString(HMDAEthnicityBase.HispanicOrLatino), results.EXTENSION.OTHER.GOVERNMENT_MONITORING_EXTENSION.HMDA_ETHNICITIES.HMDA_ETHNICITY[0].HMDAEthnicityType);
        }

        [Fact]
        public void TestGetBorrowerResidenciesWithOwnershipAndRental()
        {
            BorrowerResidence ownershipResidence = new BorrowerResidence()
            {
                OwnershipTypeId = 1, // Own
                FromDate = DateTime.Now.AddYears(-3),
                LoanAddress = new AddressInfo()
                {
                    CityName = "OwnCityName",
                    ZipCode = "123",
                    State = new State()
                    {
                        Abbreviation = "WA"
                    },
                    CountyName = "OwnCountyName",
                    StreetAddress = "Own Some street",
                    UnitNo = "C5"
                }
            };
            BorrowerResidence rentalResidence = new BorrowerResidence()
            {
                OwnershipTypeId = 2, // Rent
                MonthlyRent = 1999,
                FromDate = DateTime.Now.AddYears(-10),
                ToDate = DateTime.Now.AddYears(-5),
                LoanAddress = new AddressInfo()
                {
                    CityName = "CityName",
                    ZipCode = "123",
                    State = new State()
                    {
                        Abbreviation = "TX"
                    },
                    CountyName = "CountyName",
                    StreetAddress = "Some street",
                    UnitNo = "C4"
                }
            };

            Borrower rmBorrower = new Borrower()
            {
                BorrowerResidences = new List<BorrowerResidence>()
                                                           {
                                                               ownershipResidence,
                                                               rentalResidence
                                                           }
            };

            // Act
            MismoConverter34 converter = new MismoConverter34();
            var results = converter.GetBorrowerResidences(rmBorrower);

            // Assert
           
            Assert.NotNull(results);

            Assert.NotEmpty(results);
            Assert.Equal(rmBorrower.BorrowerResidences.Count, results.Count);
            Assert.NotNull(results[0]);
            Assert.NotNull(results[0].RESIDENCE_DETAIL);
            Assert.Equal(Convert.ToString(BorrowerResidencyBasisBase.Own), results[0].RESIDENCE_DETAIL.BorrowerResidencyBasisType);
            Assert.Equal(Convert.ToString(BorrowerResidencyBase.Current), results[0].RESIDENCE_DETAIL.BorrowerResidencyType);

            Assert.NotNull(results[1]);
            Assert.NotNull(results[1].LANDLORD);
            Assert.NotNull(results[1].LANDLORD.LANDLORD_DETAIL);
            Assert.Equal(rentalResidence.MonthlyRent, results[1].LANDLORD.LANDLORD_DETAIL.MonthlyRentAmount);
            Assert.NotNull(results[1].RESIDENCE_DETAIL);
            Assert.Equal(Convert.ToString(BorrowerResidencyBasisBase.Rent), results[1].RESIDENCE_DETAIL.BorrowerResidencyBasisType);
            Assert.Equal(Convert.ToString(BorrowerResidencyBase.Prior), results[1].RESIDENCE_DETAIL.BorrowerResidencyType);
        }
        #endregion

        private EMPLOYER GetMismoEmployer(EmploymentInfo rmEmployer, EmploymentClassificationBase? classification, EmploymentStatusBase empStatus, int sequenceIndex, int employerIndex)
        {
            if (rmEmployer != null)
            {
                var toDate = DateTime.UtcNow;
                if (empStatus == EmploymentStatusBase.Previous)
                {
                    toDate = rmEmployer.EndDate.Value;
                }
                var mismoEmployer = new EMPLOYER()
                {
                    LEGAL_ENTITY = new LEGAL_ENTITY()
                    {
                        CONTACTS = new CONTACTS()
                        {
                            CONTACT = new CONTACT()
                            {
                                CONTACT_POINTS = new CONTACT_POINTS()
                                {
                                    CONTACT_POINT = new EditableList<CONTACT_POINT>()
                                                            {
                                                                new CONTACT_POINT()
                                                                {
                                                                    CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE()
                                                                    {
                                                                        ContactPointTelephoneValue = rmEmployer.Phone
                                                                    }
                                                                }
                                                            }
                                }
                            }
                        },
                        LEGAL_ENTITY_DETAIL = new LEGAL_ENTITY_DETAIL()
                        {
                            FullName = rmEmployer.Name
                        }
                    },
                    EMPLOYMENT = new EMPLOYMENT()
                    {
                        EmploymentBorrowerSelfEmployedIndicator = rmEmployer.IsSelfEmployed == true,
                        EmploymentClassificationType = classification == null ? null : Convert.ToString(classification),
                        EmploymentMonthlyIncomeAmount = empStatus == EmploymentStatusBase.Previous ? rmEmployer.MonthlyBaseIncome : null,
                        EmploymentPositionDescription = null,
                        EmploymentStartDate = rmEmployer.StartDate.Value.ToString("yyyy-MM-dd"),
                        EmploymentStatusType = Convert.ToString(empStatus),
                        EmploymentTimeInLineOfWorkYearsCount = (decimal?)Math.Round(Math.Abs((rmEmployer.StartDate.Value - toDate).TotalDays / 365), 2),
                        EmploymentTimeInLineOfWorkMonthsCount = ((toDate.Year - rmEmployer.StartDate.Value.Year) * 12) + (toDate.Month - rmEmployer.StartDate.Value.Month),
                        SpecialBorrowerEmployerRelationshipIndicator = false,
                        EmploymentEndDate = null
                    },
                    SequenceNumber = sequenceIndex,
                    Label = $"EMPLOYER_{employerIndex}"
                };
                if (rmEmployer.OwnershipPercentage.HasValue)
                {
                    mismoEmployer.EMPLOYMENT.OwnershipInterestType = rmEmployer.OwnershipPercentage >= 25
                        ? Convert.ToString(OwnershipInterestBase.GreaterThanOrEqualTo25Percent)
                        : Convert.ToString(OwnershipInterestBase.LessThan25Percent);
                }

                if (empStatus == EmploymentStatusBase.Previous)
                {
                    mismoEmployer.EMPLOYMENT.EmploymentTimeInLineOfWorkYearsCount = (decimal?)Math.Abs((rmEmployer.StartDate.Value - toDate).TotalDays / 365);
                    mismoEmployer.EMPLOYMENT.EmploymentTimeInLineOfWorkMonthsCount = 0;
                    mismoEmployer.EMPLOYMENT.SpecialBorrowerEmployerRelationshipIndicator = null;
                    if (rmEmployer.EndDate.HasValue)
                    {
                        mismoEmployer.EMPLOYMENT.EmploymentEndDate = rmEmployer.EndDate.Value.ToString("yyyy-MM-dd");
                    }
                }

                if (rmEmployer.AddressInfo != null)
                {
                    mismoEmployer.ADDRESS = new ADDRESS()
                    {
                        AddressLineText = $"{rmEmployer.AddressInfo.StreetAddress} #{rmEmployer.AddressInfo.UnitNo}",
                        CountryCode = rmEmployer.AddressInfo.Country == null ? "US" : rmEmployer.AddressInfo.Country.TwoLetterIsoCode,
                        CountryName = rmEmployer.AddressInfo.Country == null ? "United States" : rmEmployer.AddressInfo.Country.Name,
                        CityName = rmEmployer.AddressInfo.CityName,
                        PostalCode = rmEmployer.AddressInfo.ZipCode,
                        CountyName = rmEmployer.AddressInfo.CountyName,
                        StateCode = rmEmployer.AddressInfo?.State?.Abbreviation
                    };
                }

                return mismoEmployer;
            }
            return null;
        }

        private TAXPAYER_IDENTIFIERS GetTaxPayerIdentifiers(int sequenceIndex)
        {
            return new TAXPAYER_IDENTIFIERS()
            {
                TAXPAYER_IDENTIFIER = new TAXPAYER_IDENTIFIER()
                {
                    SequenceNumber = 1,
                    TaxpayerIdentifierType = "SocialSecurityNumber"
                }
            };
        }

        private INDIVIDUAL GetMismoIndividual(Borrower rmBorrower)
        {
            INDIVIDUAL individual = null;
            if (rmBorrower != null)
            {
                List<CONTACT_POINT> contactPointList = new EditableList<CONTACT_POINT>();
                individual = new INDIVIDUAL()
                {
                    CONTACT_POINTS = new CONTACT_POINTS()
                    {
                        CONTACT_POINT = contactPointList
                    }
                };
                if (!string.IsNullOrEmpty(rmBorrower.LoanContact?.EmailAddress))
                {
                    contactPointList.Add(new CONTACT_POINT()
                    {
                        CONTACT_POINT_EMAIL = new CONTACT_POINT_EMAIL
                        {
                            ContactPointEmailValue = rmBorrower.LoanContact.EmailAddress
                        }
                    });
                }

                if (!string.IsNullOrEmpty(rmBorrower.LoanContact?.HomePhone))
                {
                    contactPointList.Add(new CONTACT_POINT()
                    {
                        CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                        {
                            ContactPointTelephoneValue = rmBorrower.LoanContact.HomePhone
                        },
                        CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                        {
                            ContactPointRoleType = "Home"
                        }
                    });
                }

                if (!string.IsNullOrEmpty(rmBorrower.LoanContact?.CellPhone))
                {
                    contactPointList.Add(new CONTACT_POINT()
                    {
                        CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                        {
                            ContactPointTelephoneValue = rmBorrower.LoanContact.CellPhone
                        },
                        CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                        {
                            ContactPointRoleType = "Mobile"
                        }
                    });
                }

                if (!string.IsNullOrEmpty(rmBorrower.LoanContact?.WorkPhone))
                {
                    contactPointList.Add(new CONTACT_POINT()
                    {
                        CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE
                        {
                            ContactPointTelephoneValue =
                                $"{rmBorrower.LoanContact.WorkPhone}-{rmBorrower.LoanContact.WorkPhoneExt}"
                        },
                        CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL
                        {
                            ContactPointRoleType = "Work"
                        }

                    });
                }

                individual.NAME = new NAME()
                {
                    FullName = null,
                    FirstName = rmBorrower.LoanContact?.FirstName,
                    LastName = rmBorrower.LoanContact?.LastName,
                    MiddleName = rmBorrower.LoanContact?.MiddleName,
                    SuffixName = rmBorrower.LoanContact?.Suffix,
                };
            }
            return individual;
        }

        private BORROWER_DETAIL GetBorrowerDetail(Borrower rmBorrower)
        {
            BORROWER_DETAIL mismoBorrowerDetail = null;
            if (rmBorrower != null)
            {
                mismoBorrowerDetail = new BORROWER_DETAIL()
                {
                    DependentCount = rmBorrower.NoOfDependent,
                    CommunityPropertyStateResidentIndicator = false,
                    SelfDeclaredMilitaryServiceIndicator = false
                };
                if (Enum.IsDefined(typeof(MaritalStatusBase),
                    (MaritalStatusBase)rmBorrower.LoanContact.MaritalStatusId))
                {
                    mismoBorrowerDetail.MaritalStatusType = Convert.ToString((MaritalStatusBase)rmBorrower.LoanContact.MaritalStatusId);
                }
            }
            return mismoBorrowerDetail;
        }

        private DEPENDENTS GetBorrowerDependents(Borrower rmBorrower, int sequenceIndex)
        {
            DEPENDENTS dependents = null;
            if (rmBorrower != null)
            {
                List<DEPENDENT> dependentList = new EditableList<DEPENDENT>();
                dependents = new DEPENDENTS()
                {
                    DEPENDENT = dependentList
                };
                int dependentIndex = 1;
                var ages = rmBorrower.DependentAge.Split(',');
                foreach (var age in ages)
                {
                    if (!string.IsNullOrEmpty(age))
                    {
                        dependentList.Add(new DEPENDENT()
                        {
                            SequenceNumber = dependentIndex++,
                            DependentAgeYearsCount = age.Trim()
                        });
                    }
                }
            }
            return dependents;
        }

        private GOVERNMENT_MONITORING GetEmptyGovernmentMonitoring()
        {
            return new GOVERNMENT_MONITORING()
            {
                GOVERNMENT_MONITORING_DETAIL = new GOVERNMENT_MONITORING_DETAIL()
                {

                },
                HMDA_ETHNICITY_ORIGINS = new HMDA_ETHNICITY_ORIGINS()
                {
                    HMDA_ETHNICITY_ORIGIN = new EditableList<HMDA_ETHNICITY_ORIGIN>()
                },
                HMDA_RACES = new HMDA_RACES()
                {
                    HMDA_RACE = new EditableList<HMDA_RACE>()
                },
                EXTENSION = new EXTENSION()
                {
                    OTHER = new OTHER()
                    {
                        GOVERNMENT_MONITORING_EXTENSION = new GOVERNMENT_MONITORING_EXTENSION()
                        {
                            HMDA_ETHNICITIES = new HMDA_ETHNICITIES()
                            {
                                HMDA_ETHNICITY = new EditableList<HMDA_ETHNICITY>()
                            }
                        }
                    }
                }
            };
        }
    }
}
