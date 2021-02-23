using ByteWebConnector.SDK.Mismo;
using ByteWebConnector.SDK.Models.Rainmaker;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ByteWebConnectorTest
{
    public class MismoService
    {
        ////[Fact]
        public void ConvertToMismo()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=1,
                            BorrowerId=1,
                            BorrowerAccount= new BorrowerAccount
                            {
                                Balance=1000 ,
                                AccountTypeId=33
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
                                        CityId=1
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoOtherNonLiquidAssets()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                AccountTypeId=32
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
                                        CityId=555
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoOtherLiquidAssets()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                AccountTypeId=18
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
                                        CityId=556
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }


        //[Fact]
        public void ConvertToMismoGiftNotDeposited()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                AccountTypeId=25
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
                                        CityId=557
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }


        //[Fact]
        public void ConvertToMismoGiftOfEquity()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                AccountTypeId=26
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
                                        CityId=558
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoSecuredBorrowedFundsNotDeposited()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                AccountTypeId=28
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
                                        CityId=559
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoCashDepositOnSalesContract()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                AccountTypeId=29
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
                                        CityId=560
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoGiftsTotal()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                AccountTypeId=24
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
                                        CityId=561
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoDefault()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                        CityId=562
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoDefaultAccountTypeIdIsNotZero()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                        CityId=563
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoDefaultLoanPurposeIdThree()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=564,
                                        CityId=564
                                    }
                                    ,PropertyUsageId=1
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
                        State = new State
                        {
                            Abbreviation = "Pak"
                        }
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoDefaultAddressIsNotNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=565,
                                        CityId=565
                                    }
                                    ,PropertyUsageId=1
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
                    AddressInfo = new AddressInfo
                    {
                        Id = 565,
                        CityId = 565,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 565,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "565"
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoDefaultLoanPurposeIdIsNotThree()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanPurposeId = 100,
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
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=566,
                                        CityId=566
                                    }
                                    ,PropertyUsageId=1
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
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 566,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "566"
                    },
                    PropertyUsageId = 1
                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }

        //[Fact]
        public void ConvertToMismoDefaultPropertyStatusIsEmpty()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanPurposeId = 100,
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
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=567,
                                        CityId=567
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
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 567,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "567"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = ""
                   ,
                    IntentToSellPriorToPurchase = true
                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoDefaultPropertyStatusIsNotEmpty()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanPurposeId = 100,
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
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 2,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=568,
                                        CityId=568
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
                    AddressInfo = new AddressInfo
                    {
                        Id = 568,
                        CityId = 568,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 568,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "568"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "asdad"
                   ,
                    IntentToSellPriorToPurchase = true
                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoDefaultPropertyStatusIsrental()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                    ,PropertyStatus="rental"
                                    ,IntentToSellPriorToPurchase= true
                                }

                           }
                        },
                        BorrowerResidences= new  List< BorrowerResidence>
                        {
                            new BorrowerResidence
                        {
                            LoanAddressId=569,
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
                    PropertyStatus = "rental"
                   ,
                    IntentToSellPriorToPurchase = true
                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }

        //[Fact]
        public void ConvertToMismoCreateDateIsNotNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                CreatedOnUtc = DateTime.UtcNow,
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
                                    PropertyTypeId = 4,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=570,
                                        CityId=570
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
                    PropertyTypeId = 4,
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
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }


        //[Fact]
        public void ConvertToMismoThreeUnitBuilding()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                    PropertyTypeId = 5,
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
                    PropertyTypeId = 5,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoFourUnitBuilding()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                    PropertyTypeId = 6,
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
                    PropertyTypeId = 6,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoSingleFamilyDetached()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                    PropertyTypeId = 1,
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
                    PropertyTypeId = 1,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoCooperative()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                    PropertyTypeId = 7,
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
                    PropertyTypeId = 7,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoManufactured()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoManufactured_Condo_PUD_COOP()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                                    PropertyTypeId = 13,
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
                    PropertyTypeId = 13,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoGetBorrowerDetail()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                            EmailAddress="ancb",
                            MaritalStatusId=4
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
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoGetBorrowerResidences()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 578,
                LoanPurposeId = 100,
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
                            OwnershipTypeId=2
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
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

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoGetBorrowerResidencesOwnershipTypeIdIsNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 579,
                LoanPurposeId = 100,
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

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoGetGovernmentMonitoringGenderIdIsNotNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 580,
                LoanPurposeId = 100,
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
                            { Id=580,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=580,
                                IsActive=true,
                                Name="Abc",
                                RaceId=580
                            }
                        }
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
                        Id = 580,
                        CityId = 580,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 580,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "580"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoGetGovernmentMonitoringraceDetailOther()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 581,
                LoanPurposeId = 100,
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
                            { Id=11,
                            Name="abc"
                            },
                            RaceDetail= new RaceDetail
                            {
                                Id=11 ,
                                IsActive=true,
                                Name="Abc",
                                RaceId=11
                            }
                        }
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

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoSetBorrowers()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 582,
                LoanPurposeId = 100,
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
                        }
                       ,EmploymentInfoes= new List<EmploymentInfo>

                       {
                         new EmploymentInfo
                         {
                             Id=582,
                             BorrowerId=582,
                          EndDate=DateTime.UtcNow,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=582
                          ,Phone= "021136958525"
                         ,OwnershipPercentage=25
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
                                    AnswerText="1"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=582,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=582,
                                    AnswerText="4"
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
                                    AnswerText="4"
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

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoSetBorrowersEndDateNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 583,
                LoanPurposeId = 100,
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
                          ,Phone= "021136958525"
                         ,OwnershipPercentage=25
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
                                    AnswerText="1"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=583,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=583,
                                    AnswerText="4"
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
                                    AnswerText="4"
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

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoSetBorrowersOwnershipPercentageLessThen25()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 584,
                LoanPurposeId = 100,
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
                          ,Phone= "021136958525"
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

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoSetLoanInfo()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 100,
                LoanPurposeId = 2,
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
                          ,Phone= "021136958525"
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
                    PropertyTaxEscrows = new List<PropertyTaxEscrow>
                    {
                        new PropertyTaxEscrow
                    {
                        Id=585,
                        EscrowEntityTypeId=2
                    },   new PropertyTaxEscrow
                    {
                        Id=585,
                        EscrowEntityTypeId=1
                    }

                    }
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=585,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoSetLoanInfoIsFirstMortgageTrue()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 100,
                LoanPurposeId = 2,
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
                          ,Phone= "021136958625"
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
                    PropertyTaxEscrows = new List<PropertyTaxEscrow>
                    {
                        new PropertyTaxEscrow
                    {
                        Id=586,
                        EscrowEntityTypeId=2
                    },   new PropertyTaxEscrow
                    {
                        Id=586,
                        EscrowEntityTypeId=1
                    }

                    }
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=586,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoLoanAddressIsNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

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
                LoanGoalId = 100,
                LoanPurposeId = 2,
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
                              ZipCode=null
                             , CityName=null
                             ,State= new State
                             {
                                 Id=587,
                                 Name=""
                             }
                               ,Country= new Country
                             {
                                 Id=587,
                                 Name=""
                             }

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
                          ,Phone= "021136958725"
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
                    PropertyTaxEscrows = new List<PropertyTaxEscrow>
                    {
                        new PropertyTaxEscrow
                    {
                        Id=587,
                        EscrowEntityTypeId=2
                    },   new PropertyTaxEscrow
                    {
                        Id=587,
                        EscrowEntityTypeId=1
                    }

                    }
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=587,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoGetBorrowerResidencesToDateIsNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 588,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 588,
                BusinessUnitId = 588,
                CustomerId = 588,
                LoanOriginatorId = 588,
                LoanGoalId = 588,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=588,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=588,
                            BorrowerId=588,
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
                               BorrowerId=588,
                               PropertyInfoId=588,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=588,
                                        CityId=588
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
                            LoanAddressId=588,
                            FromDate=DateTime.UtcNow
                            ,ToDate=null
                           , LoanAddress= new AddressInfo
                           {
                               Id=588,
                              CityId=588,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=2
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 588,
                        CityId = 588,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 588,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "588"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoGetBorrowerResidencesToDateIsNotNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 589,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 589,
                BusinessUnitId = 589,
                CustomerId = 589,
                LoanOriginatorId = 589,
                LoanGoalId = 589,
                LoanPurposeId = 100,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=589,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=589,
                            BorrowerId=589,
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
                               BorrowerId=589,
                               PropertyInfoId=589,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=589,
                                        CityId=589
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
                            LoanAddressId=589,
                            FromDate=DateTime.UtcNow
                            ,ToDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=589,
                              CityId=589,
                              CityName="Karachi"
                           },
                            OwnershipTypeId=2
                            }
                        }
                        ,LoanContact= new LoanContact
                        {
                            EmailAddress="ancb",
                            MaritalStatusId=4
                        }
                    }

                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = new AddressInfo
                    {
                        Id = 589,
                        CityId = 589,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 589,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "589"
                    },
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow

                },



            };

            //Act
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoGetGovernmentMonitoringEthnicityIsNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 590,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 590,
                BusinessUnitId = 590,
                CustomerId = 590,
                LoanOriginatorId = 590,
                LoanGoalId = 100,
                LoanPurposeId = 2,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=590,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=590,
                            BorrowerId=590,
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
                               BorrowerId=590,
                               PropertyInfoId=590,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                    {
                                        Id=590,
                                        CityId=590
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
                            LoanAddressId=590,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=590,
                              CityId=590,
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
                               Id=590,
                               EthnicityId=1
                               , Ethnicity= null
                               ,EthnicityDetail= null
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=590,
                            LoanContactId=590,
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
                             Id=590,
                             BorrowerId=590,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=590
                          ,Phone= "021136959025"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=590,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=590,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=590,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=590,
                            BorrowerId=590,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=590,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=590,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=590,
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
                        Id = 590,
                        CityId = 590,
                        State = new State
                        {
                            Abbreviation = "Pak"
                        },
                        CityName = "abc",
                        ZipCode = "abc",

                        CountyName = "abc",
                        Country = new Country
                        {
                            Id = 590,
                            TwoLetterIsoCode = "3444"
                        },
                        StreetAddress = "asdad",
                        UnitNo = "590"
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
                        Id=590,
                        EscrowEntityTypeId=2
                    },   new PropertyTaxEscrow
                    {
                        Id=590,
                        EscrowEntityTypeId=1
                    }

                    }
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=590,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        public void ConvertToMismoSetAssetsADDRESSIsNull()
        {
            //Arrange
            Mock<IMismoConverter> mock = new Mock<IMismoConverter>();
            mock.Setup(x => x.ConvertToMismo(It.IsAny<LoanApplication>())).Returns(string.Empty);

            var service = new MismoConverter34();
            LoanApplication loanApplication = new LoanApplication
            {
                Id = 591,
                LoanNumber = "00091",
                AgencyNumber = "00091",
                VisitorId = 1,
                OpportunityId = 591,
                BusinessUnitId = 591,
                CustomerId = 591,
                LoanOriginatorId = 591,
                LoanGoalId = 100,
                LoanPurposeId = 2,
                CreatedOnUtc = DateTime.UtcNow,
                Borrowers = new List<Borrower>
                {
                    new Borrower
                {
                        Id=591,
                        BorrowerAccountBinders= new List<  BorrowerAccountBinder>
                        {
                            new  BorrowerAccountBinder
                            {
                            BorrowerAccountId=591,
                            BorrowerId=591,
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
                               BorrowerId=591,
                               PropertyInfoId=591,
                                PropertyInfo = new PropertyInfo
                                {
                                    FirstMortgageBalance = 1000,
                                    PropertyTypeId = 13,
                                    AddressInfo= new AddressInfo
                                   {
                               Id=591,
                              CityId=591,
                              ZipCode=null
                             , CityName=null
                             ,State= new State
                             {
                                 Id=591,
                                 Name=""
                             }
                               ,Country= new Country
                             {
                                 Id=591,
                                 Name=""
                             }

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
                            LoanAddressId=591,
                            FromDate=DateTime.UtcNow
                           , LoanAddress= new AddressInfo
                           {
                               Id=591,
                              CityId=591,
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
                               Id=591,
                               EthnicityId=1
                               , Ethnicity= null
                               ,EthnicityDetail= null
                            }

                            }
                        ,LoanContactRaceBinders= new List<LoanContactRaceBinder>
                        {
                          new LoanContactRaceBinder
                        {
                            Id=591,
                            LoanContactId=591,
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
                             Id=591,
                             BorrowerId=591,
                          EndDate=null,
                          StartDate=DateTime.UtcNow,
                          JobTypeId=591
                          ,Phone= "021136959125"
                         ,OwnershipPercentage=20
                         ,OtherEmploymentIncomes= new  List<OtherEmploymentIncome>
                         {
                             new OtherEmploymentIncome
                         {
                             Id=591,
                             OtherIncomeTypeId=2,
                             MonthlyIncome=10000
                         }, new OtherEmploymentIncome
                         {
                             Id=591,
                             OtherIncomeTypeId=3,
                             MonthlyIncome=10000
                         },  new OtherEmploymentIncome
                         {
                             Id=591,
                             OtherIncomeTypeId=1,
                             MonthlyIncome=10000
                         }
                         }
                         }
                       }
                       ,OtherIncomes=new List<OtherIncome>
                        {  new OtherIncome
                        {
                            Id=591,
                            BorrowerId=591,
                            Description="ABBCBC",
                            MonthlyAmount=1000,
                            IncomeTypeId=22
                        }
                        }
                       ,BorrowerQuestionResponses= new List<BorrowerQuestionResponse>
                       {
                            new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=36,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="abc"
                                }
                            }
                            , new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=37,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            },
                             new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=38,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            },
                              new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=39,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            },
                               new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=40,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            },
                                new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=41,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            },
                                 new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=42,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            },
                                  new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=43,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=44,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="11"
                                }
                            },
                                    new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=45,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=46,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=47,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="1"
                                }
                            },
                                     new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=48,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            },     new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=49,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            }
                               ,  new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=50,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            }
                             ,  new BorrowerQuestionResponse
                            {
                                Id=591,
                                QuestionId=54,
                                QuestionResponse= new QuestionResponse
                                {
                                    Id=591,
                                    AnswerText="4"
                                }
                            }
                       }
                       ,DependentAge="25,33"

                       ,BorrowerLiabilities= new List<BorrowerLiability>
                       {
                            new BorrowerLiability
                            {
                                Id=591,
                                LiabilityTypeId=5
                            }
                       }
                    }


                },
                PropertyInfo = new PropertyInfo
                {
                    FirstMortgageBalance = 1000,
                    PropertyTypeId = 13,
                    AddressInfo = null,
                    PropertyUsageId = 1,
                    PropertyStatus = "rental",
                    IntentToSellPriorToPurchase = true,
                    DateAcquired = DateTime.UtcNow
                    ,
                    PropertyTaxEscrows = new List<PropertyTaxEscrow>
                    {
                        new PropertyTaxEscrow
                    {
                        Id=591,
                        EscrowEntityTypeId=2
                    },   new PropertyTaxEscrow
                    {
                        Id=591,
                        EscrowEntityTypeId=1
                    }

                    }
                    ,
                    MortgageOnProperties = new List<MortgageOnProperty>
                     { new MortgageOnProperty
                     {
                         Id=591,
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
            var result = service.ConvertToMismo(loanApplication);
            //Assert
            Assert.NotNull(result);
        }
       
    }
}




