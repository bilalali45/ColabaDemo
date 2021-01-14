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
        private readonly IMismoConverter _mismoConverter;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ByteSDKHelper> _logger;
        private readonly ITextFileWriter _fileWriter;

        private List<int?> _liabilitiesToSkip = new List<int?>()
                                                {
                                                    27 // Alimony
                                                    //14, // Child Care
                                                    //15 // Child Support
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
                _mismoConverter.SetLiabilitiesToSkip(this._liabilitiesToSkip);
                xml = _mismoConverter.ConvertToMismo(request.LoanFileRequest.LoanApplication);
                string filePath = this._fileWriter.CreateFile(this.MismoExportFileName,
                                                              xml);
                var byteSession = ByteProSession.GetInstance(request.ByteProSettings);
                var byteApplication = byteSession.GetApplication();
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
            this.SyncSkippedLiabilities(byteFile, loanApplication);
            this.SyncReoType(byteFile, loanApplication, thirdPartyCodeList);
            this.SyncYearOnJob(byteFile, loanApplication);

            byteFile.Save(false);
        }

        private void SyncSkippedLiabilities(SDKFile byteFile,
                                            LoanApplication loanApplication)
        {
            if (this._liabilitiesToSkip != null && this._liabilitiesToSkip.Count > 0)
            {
                if (byteFile != null && loanApplication != null)
                {
                    decimal? liabilitiesSum = 0;
                    foreach (var rmBorrower in loanApplication.Borrowers)
                    {
                        var liabilitiesToSync = rmBorrower.BorrowerLiabilities?.Where(liab => this._liabilitiesToSkip.Contains(liab.LiabilityTypeId))
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
                var rmBorrowers = this._mismoConverter.SortRainMakerBorrowers(loanApplication);
                if (rmBorrowers != null)
                {
                    int reoIndex = 1;
                    foreach (var rmBorrower in rmBorrowers)
                    {
                        if (rmBorrower.BorrowerProperties != null)
                        {
                            foreach (var reo in rmBorrower.BorrowerProperties)
                            {
                                if (reo.PropertyInfo != null)
                                {
                                    var reoObj = byteFile.GetCollectionObject("REO", reoIndex++);
                                    var reoType = thirdPartyCodeList.GetByteProValue("REOType", reo.PropertyInfo.PropertyTypeId);
                                    var reoByteValue = reoType.FindEnumIndex(typeof(REOTypeEnum));
                                    reoObj.SetFieldValue("REOType", reoByteValue.ToString());
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
                var rmBorrowers = loanApplication.Borrowers.ToList();
                foreach (var rmBorrower in rmBorrowers)
                {
                    int borrowerIndex = rmBorrowers.IndexOf(rmBorrower);
                    SDKObject sdkBorrower = byteFile.GetChildObject($"Bor{(borrowerIndex + 1)}",
                                                                    false);
                    if (rmBorrower.EmploymentInfoes != null)
                    {
                        var rmBorrowerEmployers = rmBorrower.EmploymentInfoes.ToList();
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
    }

}
