using System;
using System.Collections.Generic;
using System.Text;
using ByteWebConnector.SDK.Mismo;
using ByteWebConnector.SDK.Models.Rainmaker;
using Xunit;

namespace ByteWebConnectorTest
{
    public class TestMismo34Borrowers
    {
        [Fact]
        public void TestMismoBorrowersWithSingleBorrower()
        {
            // Arrange
            LoanApplication loanApplication = new LoanApplication()
            {
                Borrowers = new List<Borrower>()
                            {
                                new Borrower()
                                {
                                    LoanContact = new LoanContact()
                                                  {
                                                      EmailAddress = "abc@xyz.com",
                                                      HomePhone = "12345678",
                                                      CellPhone = "87654321",
                                                      WorkPhone = "14785236",
                                                      FirstName = "MyFirstName",
                                                      LastName = "MyLastName",
                                                      MiddleName = "MyMiddleName",
                                                      Suffix = "MySuffix",
                                                      MaritalStatusId = 1, // Married
                                                      GenderId = 2 // Male
                                                  },
                                    NoOfDependent = 1,
                                    DependentAge = "21",
                                    EmploymentInfoes = new List<EmploymentInfo>()
                                }
                            }
            };

            // Act
            MismoConverter34 mismoConverter = new MismoConverter34();
            var mismoAssets = mismoConverter.GetAssets(loanApplication);
            
            // Assert
            Assert.NotNull(mismoAssets);
        }
    }
}
