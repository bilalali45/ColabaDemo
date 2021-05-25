using ByteWebConnector.SDK.Mismo;
using ByteWebConnector.SDK.Models.ControllerModels.Document.Response;
using ByteWebConnector.SDK.Models.ControllerModels.Loan;
using ByteWebConnector.SDK.Models.Rainmaker;
using LOSAutomation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ByteWebConnector.SDK.Models;

namespace ByteWebConnector.SDK.Abstraction
{
    public interface IByteSDKHelper
    {
        ApiResponse<SendSdkLoanApplicationResponse> CreateByteFile(SdkSendLoanApplicationRequest request);
        ApiResponse<string> GetLoanStatusName(SdkLoanApplicationStatusNameRequest request);
    }

    public class ByteSDKHelper : IByteSDKHelper
    {
        private const string DEFAULT_COUNTRY_NAME = "United States";

        private readonly IMismoConverter _mismoConverter;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ByteSDKHelper> _logger;
        private readonly ITextFileWriter _fileWriter;

        private List<int?> _expenseTypeLiabilities = new List<int?>()
                                                     {
                                                         27, // Alimony
                                                         14, // Child Care
                                                         15, // Child Support
                                                         24 // Separate Maintenance Expense
                                                     };

        private List<int?> _otherAssetTypes = new List<int?>()
                                              {
                                                  30, // Cash On Hand
                                                  19, // Pending Net Sale Proceeds From RealEstate Assets
                                                  18, // Other Liquid Assets
                                                  32, // Other Non Liquid Assets
                                                  28, // Secured Borrowed Funds Not Deposited
                                              };

        private List<int?> _liabilityTypes = new List<int?>()
                                             {
                                                 23, // Revolving
                                                 18, // Installment
                                                 //20, // Mortgage Loan
                                                 //17, // HELOC
                                                 16, // Collections Judgements And Liens
                                                 19, // Lease Payments
                                                 21, // Open 30 Day Charge Account
                                                 26, // Taxes
                                                 22 // Other Liability
                                             };

        private List<int?> _giftOfCashTypes = new List<int?>()
                                              {
                                                  25, // Gift Not Deposited
                                                  26 // Gift Of Equity
                                              };

        private string MismoExportPath
        {
            get
            {
                string exportDir = this._configuration[key: "MismoExportPath"];
                if (!Directory.Exists(exportDir))
                {
                    Directory.CreateDirectory(exportDir);
                }
                return exportDir;
            }
        }

        private string MismoExportFileName
        {
            get
            {
                string fileName = Guid.NewGuid().ToString();
                string path = $"{this.MismoExportPath}\\{fileName}.xml";
                return path;
            }
        }

        public ByteSDKHelper(IMismoConverter mismoConverter, IConfiguration configuration, ILogger<ByteSDKHelper> logger, ITextFileWriter fileWriter)
        {
            this._mismoConverter = mismoConverter;
            this._configuration = configuration;
            this._logger = logger;
            this._fileWriter = fileWriter;
        }

        public ApiResponse<SendSdkLoanApplicationResponse> CreateByteFile(SdkSendLoanApplicationRequest request)
        {
            string xml = null;
            var response = new ApiResponse<SendSdkLoanApplicationResponse>();
            try
            {
                _mismoConverter.SetLiabilitiesToSkip(this._expenseTypeLiabilities);
                xml = _mismoConverter.ConvertToMismo(request.LoanFileRequest.LoanApplication);
                string filePath = this._fileWriter.CreateFile(this.MismoExportFileName,
                                                              xml);
                var byteSession = ByteProSession.GetInstance(request.ByteProSettings);
                var byteApplication = byteSession.GetApplication();
                _logger.LogInformation($"Loan applicaiton ID : {request.LoanFileRequest.LoanApplication.Id} Byte Organization Code : {request.LoanFileRequest.LoanApplication.BusinessUnit?.ByteOrganizationCode}");
                var options = new SDKNewFileOptions()
                              {
                                  SubPropState = request.LoanFileRequest.LoanApplication.PropertyInfo.AddressInfo.State.Abbreviation,
                                  LoanOfficerUserName = request.LoanFileRequest.LoanApplication.Opportunity?.Owner?.UserProfile?.UserName ?? "" // "Tanner.Holland"
                                  ,
                                  ImportFileFormat = SDKModule.SDKImportFileFormat.MISMO,
                                  ImportFileName = filePath,
                                  OrganizationCode = request.LoanFileRequest.LoanApplication.BusinessUnit?.ByteOrganizationCode
                              };
                SDKFile byteFile = byteApplication.CreateFile(options);
                PopulateMissingFields(byteFile,
                                      request.LoanFileRequest.LoanApplication,
                                      request.LoanFileRequest.ThirdPartyCodeList);
                byteFile.Save();

                var fileData = byteFile.GetChildObject("FileData",
                                                       false);
                string byteFileName = fileData.GetFieldValue("FileName");
                string fileDataId = fileData.GetFieldValue("FileDataID");
                response.Data = new SendSdkLoanApplicationResponse();
                response.Data.FileName = byteFileName;
                int fileId = 0;
                int.TryParse(fileDataId,
                             out fileId);
                response.Data.FileDataId = fileId;
                response.Status = ApiResponseStatus.Success;
            }
            catch (ByteBase.FilerException fe)
            {
                ByteProSession.ResetSession();
                response.Message = fe.Message;
                response.Code = ApiResponseStatus.Error;
                _logger.LogInformation(fe.Message);
                _logger.LogInformation(fe.StackTrace);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Code = ApiResponseStatus.Error;
                _logger.LogInformation(e.Message);
                _logger.LogInformation(e.StackTrace);
            }
            
            return response;
        }


        public ApiResponse<string> GetLoanStatusName(SdkLoanApplicationStatusNameRequest request)
        {
            string statusName = null;
            ApiResponse<string> response = new ApiResponse<string>();
            if (string.IsNullOrEmpty(request.ByteFileName))
            {
                throw new Exception("Byte file name is required.");
            }
            else
            {
                try
                {
                    var byteSession = ByteProSession.GetInstance(request.ByteProSettings);
                    var byteApplication = byteSession.GetApplication();

                    var byteFile = byteApplication.OpenFile(request.ByteFileName, true);
                    var status = byteFile.GetChildObject("Status", false);
                    statusName = status.GetFieldValue("LoanStatusName");
                    response.Status = ApiResponseStatus.Success;
                }
                catch (Exception e)
                {
                    response.Message = e.Message;
                    response.Code = ApiResponseStatus.Error;
                    _logger.LogInformation(e.Message);
                    _logger.LogInformation(e.StackTrace);
                    response.Data = null;
                }
            }
            response.Data = statusName;
            return response;
        }

        private void PopulateMissingFields(SDKFile byteFile,
                                           LoanApplication loanApplication, ThirdPartyCodeList thirdPartyCodeList)
        {
            //Application HOD
            SDKObject application = byteFile.GetCollectionObject("Application", 1);
            if (loanApplication.PropertyInfo.HoaDues.HasValue)
            {
                var hoaDues = loanApplication.PropertyInfo.HoaDues / 12;
                application.SetFieldValue("PresentHOD", hoaDues.ToString());
            }

            //subject Property
            SDKObject propertyInfo = byteFile.GetChildObject("SubProp", true);
            var rAmtExLiens = loanApplication.PropertyInfo.MortgageOnProperties?.Sum(mortgageOnProperty => mortgageOnProperty.MortgageBalance);
            if (rAmtExLiens.HasValue)
            {
                propertyInfo.SetFieldValue("RAmtExLiens", rAmtExLiens.ToString());// Re
            }

            propertyInfo.SetFieldValue("RYearLotAcq", loanApplication.PropertyInfo?.DateAcquired?.Year.ToString());
            propertyInfo.SetFieldValue("LotAcquiredDate", loanApplication.PropertyInfo?.DateAcquired.ToString());
            propertyInfo.SetFieldValue("ROrigCost", loanApplication.PropertyInfo.OriginalPurchasePrice.ToString());
            propertyInfo.SetFieldValue("CPresValLot", loanApplication.PropertyInfo.PropertyValue != null ? Convert.ToString(loanApplication.PropertyInfo.PropertyValue) : ""); //

            //Status
            var status = byteFile.GetChildObject("Status", false);
            if (loanApplication.ExpectedClosingDate.HasValue)
            {
                status.SetFieldValue("SchedClosingDate", loanApplication.ExpectedClosingDate.ToString());
            }

            //Asset NetEquity

            for (var rmBorrowerIndex = 0; rmBorrowerIndex < loanApplication.Borrowers.ToList().Count; rmBorrowerIndex++)
            {
                var rmBorrower = loanApplication.Borrowers.ToList()[index: rmBorrowerIndex];
                var byteApplicationIndex = (int)Math.Floor(d: rmBorrowerIndex / 2m) + 1;
                SDKObject applicationInner = byteFile.GetCollectionObject("Application", byteApplicationIndex);


                var netEquity = rmBorrower.BorrowerAccountBinders?.FirstOrDefault(b => b.BorrowerAccount.AccountTypeId == 33 /*NetEquity*/)?.BorrowerAccount?.Balance;
                //var byteBorrower;
                var fieldIndex = (rmBorrowerIndex % 2 == 0) ? 1 : 2;

                if (netEquity != null)
                {
                    application.SetFieldValue($"StockBondDesc{fieldIndex }", "Net Equity");
                    application.SetFieldValue($"StockBondValue{fieldIndex }", netEquity.ToString());
                }
            }
            //this.SyncSkippedLiabilities(byteFile, loanApplication);
            this.SyncReoType(byteFile, loanApplication, thirdPartyCodeList);
            //this.SyncYearOnJob(byteFile, loanApplication);
            this.SetNotApplicableFlag(byteFile, loanApplication);
            this.SetCountryNameInAddresses(byteFile, loanApplication);
            this.FixBorrwerQuestions(byteFile, loanApplication);

            byteFile.GetChildObject("DOT", false).SetFieldValue("BorrowerClosingCostsOV", null);

            byteFile.Save(false);
        }

        private void SyncSkippedLiabilities(SDKFile byteFile,
                                            LoanApplication loanApplication)
        {
            if (this._expenseTypeLiabilities != null && this._expenseTypeLiabilities.Count > 0)
            {
                if (byteFile != null && loanApplication != null)
                {
                    decimal? liabilitiesSum = 0;
                    foreach (var rmBorrower in loanApplication.Borrowers)
                    {
                        var liabilitiesToSync = rmBorrower.BorrowerLiabilities?.Where(liab => this._expenseTypeLiabilities.Contains(liab.LiabilityTypeId))
                                                          .ToList();
                        if (liabilitiesToSync != null && liabilitiesToSync.Count > 0)
                        {
                            liabilitiesSum += Convert.ToDecimal(liabilitiesToSync.Sum(liab => liab.MonthlyPayment));
                        }
                    }

                    if (liabilitiesSum > 0)
                    {
                        SDKObject application = byteFile.GetCollectionObject("Application", 1);
                        application.SetFieldValue("OtherExpenseType", "1");
                        application.SetFieldValue("OtherExpenseAmount", liabilitiesSum.ToString());
                    }
                }
            }
        }

        private void SyncReoType(SDKFile byteFile,
                                 LoanApplication loanApplication, ThirdPartyCodeList thirdPartyCodeList)
        {
            if (byteFile != null && loanApplication != null)
            {
                int reoIndex = 1;
                // Set REO type when loan purpose is Refinance OR Cashout
                if ((loanApplication.LoanPurposeId) == 2 || (loanApplication.LoanPurposeId == 3)) // If 2 = refinance or 3 = cash out
                {
                    if (loanApplication.PropertyInfo.PropertyTypeId.HasValue)
                    {
                        try
                        {
                            var reoObj = byteFile.GetCollectionObject("REO", reoIndex++);
                            var reoType = thirdPartyCodeList.GetByteProValue("REOType", loanApplication.PropertyInfo.PropertyTypeId);
                            var reoByteValue = reoType.FindEnumIndex(typeof(REOTypeEnum));
                            reoObj.SetFieldValue("REOType", reoByteValue.ToString());
                            bool hasMortgage = loanApplication.PropertyInfo.MortgageOnProperties != null && loanApplication.PropertyInfo.MortgageOnProperties.Count > 0;
                            reoObj.SetFieldValue("MortgagesDNADesired", Convert.ToString(!hasMortgage).ToLower());
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                }
                var rmBorrowers = this._mismoConverter.SortRainMakerBorrowers(loanApplication);
                if (rmBorrowers != null)
                {
                    foreach (var rmBorrower in rmBorrowers)
                    {
                        if (rmBorrower.BorrowerProperties != null)
                        {
                            foreach (var reo in rmBorrower.BorrowerProperties)
                            {
                                if (reo.PropertyInfo != null && (!this._mismoConverter.IsSameAddress(loanApplication.PropertyInfo.AddressInfo, reo.PropertyInfo.AddressInfo)))
                                {
                                    var reoObj = byteFile.GetCollectionObject("REO", reoIndex++);
                                    var reoType = thirdPartyCodeList.GetByteProValue("REOType", reo.PropertyInfo.PropertyTypeId);
                                    var reoByteValue = reoType.FindEnumIndex(typeof(REOTypeEnum));
                                    reoObj.SetFieldValue("REOType", reoByteValue.ToString());
                                    reoObj.SetFieldValue("MortgagesDNADesired", "true");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SyncYearOnJob(SDKFile byteFile,
                                   LoanApplication loanApplication)
        {
            try
            {
                var rmBorrowers = this._mismoConverter.SortRainMakerBorrowers(loanApplication);// loanApplication.Borrowers.ToList();
                foreach (var rmBorrower in rmBorrowers)
                {
                    int borrowerIndex = rmBorrowers.IndexOf(rmBorrower);
                    SDKObject sdkBorrower = byteFile.GetChildObject($"Bor{(borrowerIndex + 1)}",
                                                                    false);
                    if (rmBorrower.EmploymentInfoes != null)
                    {
                        var currentEmploymentInfos = rmBorrower.EmploymentInfoes?.Where(ei => ei.EndDate == null).OrderByDescending(ei => ei.StartDate).ThenBy(emp => emp.JobTypeId).ToList();
                        var previousEmploymentInfos = rmBorrower.EmploymentInfoes?.Where(ei => ei.EndDate != null).ToList();

                        List<EmploymentInfo> rmBorrowerEmployers = new List<EmploymentInfo>();
                        rmBorrowerEmployers.AddRange(currentEmploymentInfos);
                        rmBorrowerEmployers.AddRange(previousEmploymentInfos);


                        foreach (var rmBorrowerEmployer in rmBorrowerEmployers)
                        {
                            int employerIndex = rmBorrowerEmployers.IndexOf(rmBorrowerEmployer);
                            var endDate = rmBorrowerEmployer.EndDate == null ? DateTime.Now : rmBorrowerEmployer.EndDate;

                            bool isPrimary = employerIndex == 0;
                            SDKObject sdkEmployer = null;
                            string sdkEmployerStatus = "Primary";
                            if (isPrimary)
                            {
                                sdkEmployer = sdkBorrower.GetChildObject("PrimaryEmployer",
                                                                         false);
                            }
                            else
                            {
                                var formerCount = sdkBorrower.GetCollectionCount("FormerEmployer");
                                if (formerCount > 0)
                                {
                                    sdkEmployer = sdkBorrower.GetCollectionObject("FormerEmployer",
                                                                                  employerIndex);
                                    sdkEmployerStatus = "SecondaryCurrent";
                                }
                            }

                            if (sdkEmployer != null)
                            {
                                var yearsOnJob = (decimal?)Math.Abs((rmBorrowerEmployer.StartDate.Value - endDate.Value).TotalDays / 365);
                                sdkEmployer.SetFieldValue("YearsOnJob", Convert.ToString(yearsOnJob));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void SetNotApplicableFlag(SDKFile byteFile,
                                           LoanApplication loanApplication)
        {
            try
            {
                var rmBorrowers = loanApplication.Borrowers.ToList();
                bool hasOtherAssets = false;
                bool hasAnyRealState = false;
                bool hasAdditionalRealState = false;
                bool giftCashAlreadyFound = false;
                List<int?> giftCashFoundList = new List<int?>();
                foreach (var rmBorrower in rmBorrowers)
                {
                    int borrowerIndex = rmBorrowers.IndexOf(rmBorrower);
                    SDKObject sdkBorrower = byteFile.GetChildObject($"Bor{(borrowerIndex + 1)}",
                                                                    false);
                    var currentEmploymentInfos = rmBorrower.EmploymentInfoes?.Where(ei => ei.EndDate == null).OrderByDescending(ei => ei.StartDate).ThenBy(emp => emp.JobTypeId).ToList();
                    var previousEmploymentInfos = rmBorrower.EmploymentInfoes?.Where(ei => ei.EndDate != null).ToList();

                    bool primaryEmployerExists = currentEmploymentInfos == null || currentEmploymentInfos.Count == 0;
                    sdkBorrower.SetFieldValue("PrimaryEmployerDNADesired", Convert.ToString(primaryEmployerExists).ToLower());

                    bool secondaryEmployerExists = currentEmploymentInfos != null && currentEmploymentInfos.Count > 1;
                    sdkBorrower.SetFieldValue("SecondaryEmployersDNADesired", Convert.ToString(!secondaryEmployerExists).ToLower());

                    var previousEmployerExists = previousEmploymentInfos == null || previousEmploymentInfos.Count == 0;
                    sdkBorrower.SetFieldValue("FormerEmployersDNADesired", Convert.ToString(previousEmployerExists).ToLower());

                    #region Other Income

                    bool hasOtherIncomes = rmBorrower.OtherIncomes != null && rmBorrower.OtherIncomes.Count > 0;
                    hasOtherIncomes = hasOtherIncomes || (rmBorrower.OtherIncomes != null && rmBorrower.OtherIncomes.Any());
                    bool otherEmployerIncomeFound = false;
                    if (currentEmploymentInfos != null)
                    {
                        otherEmployerIncomeFound = currentEmploymentInfos.Any(emp => emp.OtherEmploymentIncomes != null);
                    }
                    //OtherIncomeDNADesired
                    sdkBorrower.SetFieldValue("OtherIncomeDNADesired", Convert.ToString(hasOtherIncomes == false && otherEmployerIncomeFound == false).ToLower());
                    #endregion

                    bool formerResidenceExists = rmBorrower.BorrowerResidences != null && rmBorrower.BorrowerResidences.Count > 1;
                    sdkBorrower.SetFieldValue("FormerResidencesDNADesired",
                                              Convert.ToString(!formerResidenceExists).ToLower());

                    sdkBorrower.SetFieldValue("MailingAddressDNADesired", "true");


                    bool hasExpenseLiabilities = rmBorrower.BorrowerLiabilities != null && rmBorrower.BorrowerLiabilities.Any(liab => this._expenseTypeLiabilities.Contains(liab.LiabilityTypeId));
                    sdkBorrower.SetFieldValue("ExpensesDNADesired",
                                              Convert.ToString(!hasExpenseLiabilities).ToLower());

                    //hasOtherAssets = hasOtherAssets || rmBorrower.BorrowerAccountBinders != null && rmBorrower.BorrowerAccountBinders.Any(bab => bab.BorrowerAccount != null && this._otherAssetTypes.Contains(bab.BorrowerAccount.AccountTypeId));
                    hasOtherAssets = hasOtherAssets || rmBorrower.BorrowerAccountBinders != null && rmBorrower.BorrowerAccountBinders.Any();
                    sdkBorrower.SetFieldValue("OtherAssetsDNADesired",
                                              Convert.ToString(!hasOtherAssets).ToLower());

                    //bool hasLiabilities = rmBorrower.BorrowerLiabilities != null && rmBorrower.BorrowerLiabilities.Any(liab => !this._expenseTypeLiabilities.Contains(liab.LiabilityTypeId));
                    //hasLiabilities = hasLiabilities || (loanApplication.PropertyInfo.MortgageOnProperties != null && loanApplication.PropertyInfo.MortgageOnProperties.Any());
                    //sdkBorrower.SetFieldValue("DebtsDNADesired",
                    //                          Convert.ToString(!hasLiabilities).ToLower());

                    bool hasLiabilityOtherThanExpense = rmBorrower.BorrowerLiabilities != null
                                                        //&& rmBorrower.BorrowerLiabilities.Where(liab => this._liabilityTypes.Contains(liab.LiabilityTypeId));
                                                        && (rmBorrower.BorrowerLiabilities.Where(liab => this._liabilityTypes.Contains(liab.LiabilityTypeId)).Count() > 0)
                        ;
                    sdkBorrower.SetFieldValue("DebtsDNADesired",
                                              Convert.ToString(!hasLiabilityOtherThanExpense).ToLower());

                    if (loanApplication.LoanPurposeId == 1) // Purchase
                    {
                        hasAnyRealState = hasAnyRealState || (rmBorrower.BorrowerProperties != null && rmBorrower.BorrowerProperties.Any());
                    }
                    else
                    {
                        hasAnyRealState = true;
                    }
                    hasAdditionalRealState = hasAdditionalRealState || (rmBorrower.BorrowerProperties != null && rmBorrower.BorrowerProperties.Count() > 1);
                    sdkBorrower.SetFieldValue("DoNotOwnAnyRealEstateDesired",
                                              Convert.ToString(!hasAnyRealState).ToLower());
                    sdkBorrower.SetFieldValue("AdditionalREOsDNADesired",
                                              Convert.ToString(!hasAdditionalRealState).ToLower());

                    //var hasGiftCash = hasOtherAssets && rmBorrower.BorrowerAccountBinders.Any(bab => bab.BorrowerAccount != null && this._giftOfCashTypes.Contains(bab.BorrowerAccount.AccountTypeId));
                    //giftCashAlreadyFound = !hasGiftCash;

                    //if (!giftCashAlreadyFound)
                    //{
                    //    sdkBorrower.SetFieldValue("GiftsDNADesired", (!hasGiftCash).ToString().ToLower());
                    //}
                    if (giftCashFoundList.Count == 0)
                    {
                        if (rmBorrower.BorrowerAccountBinders != null)
                        {
                            giftCashFoundList = rmBorrower.BorrowerAccountBinders.Where(
                                    bab => bab.BorrowerAccount != null
                                           && this._giftOfCashTypes.Contains(bab.BorrowerAccount.AccountTypeId))
                                .Select(bab => bab.BorrowerAccount.AccountTypeId)
                                .ToList();
                            var hasGiftCash = hasOtherAssets && giftCashFoundList != null &&
                                              giftCashFoundList.Count > 0;
                            sdkBorrower.SetFieldValue("GiftsDNADesired", (!hasGiftCash).ToString().ToLower());
                        }
                    }
                }


                var subjectProperty = byteFile.GetChildObject("SubProp",
                                                          false);
                if (subjectProperty != null)
                {
                    subjectProperty.SetFieldValue("OtherLoansDNADesired", "true");
                    subjectProperty.SetFieldValue("NetCashFlowDNADesired", "true");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private void FixBorrwerQuestions(SDKFile byteFile, LoanApplication loanApplication)
        {
            if (loanApplication != null && loanApplication.Borrowers != null)
            {
                int intentToOccupyId = 45;
                var rmBorrowers = loanApplication.Borrowers.ToList();
                foreach (var rmBorrower in rmBorrowers)
                {
                    int borrowerIndex = rmBorrowers.IndexOf(rmBorrower);
                    SDKObject sdkBorrower = byteFile.GetChildObject($"Bor{(borrowerIndex + 1)}",
                        false);
                    bool intentToOccupy = rmBorrower.BorrowerQuestionResponses.Any(bqr => bqr.QuestionId == 45)
                                          && rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 45).QuestionResponse != null
                                          && rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 45).QuestionResponse.AnswerText == "1";
                    if (!intentToOccupy)
                    {

                        sdkBorrower.SetFieldValue("PropertyType", "0");
                    }
                    var PriorPropertyUsageType = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 49)?.QuestionResponse.AnswerText;
                    if (PriorPropertyUsageType == "2") // FHA Secondary Residence
                    {
                        sdkBorrower.SetFieldValue("PropertyType", "4"); // Equivalent property value in Byte
                    }
                }
            }
            //rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 37)?.QuestionResponse.AnswerText;
        }

        private void SetCountryNameInAddresses(SDKFile byteFile,
                                               LoanApplication loanApplication)
        {
            var rmBorrowers = loanApplication.Borrowers.ToList();
            foreach (var rmBorrower in rmBorrowers)
            {
                int borrowerIndex = rmBorrowers.IndexOf(rmBorrower);
                SDKObject sdkBorrower = byteFile.GetChildObject($"Bor{(borrowerIndex + 1)}",
                                                                false);

                //var sdkAssetsCount = sdkBorrower.GetCollectionCount("Asset");
                //for (int i = 1; i <= sdkAssetsCount; i++)
                //{
                //    var sdkAsset = sdkBorrower.GetCollectionObject("Asset",
                //                                                   i);
                //}

                var sdREOsCount = sdkBorrower.GetCollectionCount("REO");
                for (int i = 1; i <= sdREOsCount; i++)
                {
                    var sdkREO = sdkBorrower.GetCollectionObject("REO",
                                                                   i);
                    this.SetCountry(sdkREO);
                    //var sdkREOCountryName = sdkREO.GetFieldValue("Country");
                    //if (string.IsNullOrEmpty(sdkREOCountryName) || (sdkREOCountryName == "US"))
                    //{
                    //    sdkREO.SetFieldValue("Country", DEFAULT_COUNTRY_NAME);
                    //}
                }

                var formerEmployersCount = sdkBorrower.GetCollectionCount("FormerEmployer");
                for (int i = 1; i <= formerEmployersCount; i++)
                {
                    var sdkEmployer = sdkBorrower.GetCollectionObject("FormerEmployer",
                                                                  i);
                    this.SetCountry(sdkEmployer);
                }
                this.SetCountry(sdkBorrower.GetChildObject("PrimaryEmployer",
                                                           false));
                this.SetCountry(sdkBorrower.GetChildObject("CurrentResidence",
                                                           false));

                var formerResidencesCount = sdkBorrower.GetCollectionCount("FormerResidence");
                if (formerResidencesCount > 0)
                {
                    for (int i = 1; i <= formerResidencesCount; i++)
                    {
                        var formerResidence = sdkBorrower.GetCollectionObject("FormerResidence",
                                                                              i);
                        this.SetCountry(formerResidence);
                    }
                }
            }
        }

        private void SetCountry(SDKObject obj,
                                string countryFieldName = "Country")
        {
            var fieldValue = obj.GetFieldValue(countryFieldName);
            if (string.IsNullOrEmpty(fieldValue) || fieldValue.Equals("US",
                                                                      StringComparison.InvariantCultureIgnoreCase))
            {
                obj.SetFieldValue(countryFieldName, DEFAULT_COUNTRY_NAME);
            }
        }
    }

}
