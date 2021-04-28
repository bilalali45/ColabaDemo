using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using ByteWebConnector.Model.Models.OwnModels;
using ByteWebConnector.Model.Models.OwnModels.Settings;
using ByteWebConnector.Model.Models.ServiceRequestModels.ByteApi;
using ByteWebConnector.Model.Models.ServiceRequestModels.Los;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;
using ByteWebConnector.Service.DbServices;
using Extensions.ExtensionClasses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Borrower = ByteWebConnector.Model.Models.ActionModels.LoanFile.Borrower;

namespace ByteWebConnector.Service.ExternalServices
{
    public class ByteProService : IByteProService
    {
        private readonly string _baseApiUrl;
        private readonly ByteProSettings _byteProSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ByteProService> _logger;
        private readonly IMapper _mapper;

        private string _byteProSession;


        public ByteProService(ILogger<ByteProService> logger,
                              ISettingService settingService,
                              HttpClient httpClient,
                              IMapper mapper)
        {
            _logger = logger;
            _httpClient = httpClient;
            _byteProSettings = settingService.GetByteProSettings();
            _baseApiUrl = _byteProSettings.ByteApiUrl;
            _mapper = mapper;
        }


        private string ByteProSession
        {
            get
            {
                if (string.IsNullOrEmpty(value: _byteProSession)) _byteProSession = GetByteProSession().Result;
                return _byteProSession;
            }
        }


        public async Task<string> GetByteProSession()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriString: _baseApiUrl + "auth/ "),
                Method = HttpMethod.Get,
                //Content = new StringContent(content: "")
            };
            request.Headers.Add(name: "authorizationKey",
                                value: _byteProSettings.ByteApiAuthKey);
            request.Headers.Add(name: "username",
                                value: _byteProSettings.ByteApiUserName);
            request.Headers.Add(name: "password",
                                value: _byteProSettings.ByteApiPassword);
            request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            request.Content = new StringContent(content: "");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");

            var response = await _httpClient.SendAsync(request: request).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            _logger.LogInformation(message: "ByteFile EnsureSuccessStatusCode");
            var resp = await response.Content.ReadAsStringAsync();
            return resp.Replace(oldValue: "\"",
                                newValue: "");
        }


        public ApiResponse<DocumentUploadResponse> SendDocumentToByte(DocumentUploadRequest documentUploadRequest,
                                                                      string session)
        {
            var apiResponse = new ApiResponse<DocumentUploadResponse>();
            try
            {
                var input = JsonConvert.SerializeObject(value: documentUploadRequest,
                                                        formatting: Formatting.None,
                                                        settings: new JsonSerializerSettings
                                                                  {
                                                                      NullValueHandling = NullValueHandling.Ignore
                                                                  });

                var documentResponse = Send(output: input,
                                            session: session);
                var settings = new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore,
                                   MissingMemberHandling = MissingMemberHandling.Ignore
                               };

                _logger.LogInformation(message: $"byteDocumentResponse= {documentResponse.Result}");
                var document =
                    JsonConvert.DeserializeObject<DocumentUploadResponse>(value: documentResponse.Result,
                                                                          settings: settings);
                if (document != null) document.ExtOriginatorId = 1;

                apiResponse.Status = ApiResponseStatus.Success;
                apiResponse.Data = document;
            }
            catch (Exception ex)
            {
                apiResponse.Status = ApiResponseStatus.Fail;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }


        public List<EmbeddedDoc> GetAllByteDocuments(string session,
                                                     int fileDataId)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUriString: _baseApiUrl + "Document/" + fileDataId);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add(name: "Session",
                                value: session);
            request.Accept = "application/json";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(stream: dataStream ?? throw new InvalidOperationException());
                var responseString = reader.ReadToEnd();

                var embeddedDocList =
                    JsonConvert.DeserializeObject<List<EmbeddedDoc>>(value: responseString);
                reader.Close();
                dataStream.Close();
                return embeddedDocList;
            }
        }


        public async Task<string> Send(string output,
                                     string session)
        {

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriString: _baseApiUrl + "Document/"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: output,
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");
            request.Headers.Add(name: "Session",
                                value: session);
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            var response = await _httpClient.SendAsync(request: request)
                                       .ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            return resp;
        }


        public EmbeddedDoc GetEmbeddedDocData(string byteProSession,
                                              int documentId,
                                              int fileDataId)
        {
            var request =
                (HttpWebRequest)WebRequest.Create(requestUriString: $"{_baseApiUrl}Document/{fileDataId}/{documentId}");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add(name: "Session",
                                value: byteProSession);
            request.Accept = "application/json";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(stream: dataStream ?? throw new InvalidOperationException());
                var responseString = reader.ReadToEnd();

                var embeddedDoc =
                    JsonConvert.DeserializeObject<EmbeddedDoc>(value: responseString);
                reader.Close();
                dataStream.Close();
                return embeddedDoc;
            }
        }


        public async Task<bool> ValidateByteSessionAsync(string byteSession)
        {
            _logger.LogInformation(message: $"DocSync byteSession = {byteSession}");
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriString: _baseApiUrl + "organization/ "),
                Method = HttpMethod.Get,
                //Content = new StringContent(content: "")
            };
            request.Headers.Add(name: "Session",
                          value: byteSession);
            request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            request.Content = new StringContent(content: "");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");

            var response = await _httpClient.SendAsync(request: request).ConfigureAwait(continueOnCapturedContext: false);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }


        public async Task<FileDataResponse> GetFileDataAsync(string byteSession,
                                           string fileDataId)
        {
            _logger.LogInformation(message: "GetFileData Start");
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriString: _baseApiUrl + "FileData/" + fileDataId),
                Method = HttpMethod.Get,
            };
            request.Headers.Add(name: "Session",
                          value: byteSession);
            request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            request.Content = new StringContent(content: "");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");

            var response = await _httpClient.SendAsync(request: request).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var fileData = JsonConvert.DeserializeObject<FileDataResponse>(value: responseString);
            _logger.LogInformation(message: "GetFileData End");
            return fileData;

        }


        public ByteFile GetByteLoanFile(string loanApplicationByteLoanNumber)
        {
            throw new NotImplementedException();
        }


        public ByteFile SendFile(LoanFileRequest loanFileRequest)
        {
            //var newFile = ByteNewFile.Create(loanApplication: loanFileRequest.LoanApplication);
            var newFile = new ByteNewFile(loanApplication: loanFileRequest.LoanApplication);

            _logger.LogInformation(message: "ByteFile SendFile Start");
            var newFileResponse = CreateLoanApplicationInByte(newFile: newFile);
            _logger.LogInformation(message: $"ByteFile newFileResponse Content: {newFileResponse.ToJson()}");

            var byteFile = GetLoanApplicationFileFromByte(fileDataId: newFileResponse.FileDataId);

            #region UpdateByteFileAndFirstApplication

            byteFile.Loan.Update(loanApplication: loanFileRequest.LoanApplication,
                                 loanRequest: loanFileRequest.LoanRequest,
                                 thirdPartyCodeList: loanFileRequest.ThirdPartyCodeList);

            var party = new Party
                        {
                            FileDataId = byteFile.FileDataID
                        };
            party.Update(owner: loanFileRequest.LoanApplication.Opportunity.Owner);
            byteFile.Parties.Add(item: party);

            byteFile.CustomField = CustomField.Create(loanApplication: loanFileRequest.LoanApplication,
                                                      byteFileFileDataId: byteFile.FileDataID);

            byteFile.Status.Update(loanApplication: loanFileRequest.LoanApplication);

            byteFile.FileData.Update(loanApplication: loanFileRequest.LoanApplication,
                                     thirdPartyCodeList: loanFileRequest.ThirdPartyCodeList);

            byteFile.SubProp.Update(loanApplication: loanFileRequest.LoanApplication,
                                    thirdPartyCodeList: loanFileRequest.ThirdPartyCodeList);

            byteFile.Applications.First().FileDataId = byteFile.FileDataID;

        

            decimal? lifeInsuranceEstimatedMonthlyAmount = null;
            var propertyTaxEscrows = loanFileRequest.LoanApplication.PropertyInfo.PropertyTaxEscrows;
            if (propertyTaxEscrows != null)
            {
                var propertyTaxes = propertyTaxEscrows.SingleOrDefault(predicate: pte => pte.EscrowEntityTypeId == 1);
                if (propertyTaxes != null)
                {
                    var propertyTaxesItem = PrepaidItem.Create(propertyInsuranceAnnuallyPayment: propertyTaxes.AnnuallyPayment,
                                                               itemType: 3);
                    lifeInsuranceEstimatedMonthlyAmount = propertyTaxes?.AnnuallyPayment / 12;
                    propertyTaxesItem.FileDataId = byteFile.FileDataID;
                    //byteFile.PrepaidItems[3] = propertyTaxesItem; todo later
                }

                var propertyInsurance = propertyTaxEscrows.SingleOrDefault(predicate: pte => pte.EscrowEntityTypeId == 2);
                if (propertyInsurance != null)
                {
                    var hazardInsuranceItem = PrepaidItem.Create(propertyInsuranceAnnuallyPayment: propertyInsurance.AnnuallyPayment,
                                                                 itemType: 0);
                    hazardInsuranceItem.FileDataId = byteFile.FileDataID;
                    //byteFile.PrepaidItems[0] = hazardInsuranceItem; todo later
                }
            }

            byteFile.Applications.First().Update(lifeInsuranceEstimatedMonthlyAmount: lifeInsuranceEstimatedMonthlyAmount,
                                                 propertyInfoHoaDues: loanFileRequest.LoanApplication.PropertyInfo.HoaDues);

            #endregion 


            #region Creating All Borrowers

            for (var rmBorrowerIndex = 0; rmBorrowerIndex < loanFileRequest.LoanApplication.Borrowers.ToList().Count; rmBorrowerIndex++)
            {
                var rmBorrower = loanFileRequest.LoanApplication.Borrowers.ToList()[index: rmBorrowerIndex];

                var byteApplicationIndex = (int) Math.Floor(d: rmBorrowerIndex / 2m);

                if (byteFile.Applications.ElementAtOrDefault(index: byteApplicationIndex) == null)
                {
                    var application = new Application
                                      {
                                          FileDataId = byteFile.FileDataID
                                      };
                    byteFile.Applications.Add(item: application);
                }

                var byteApplication = byteFile.Applications[index: byteApplicationIndex];

                if (rmBorrowerIndex % 2 == 0)
                {
                    if (byteApplication.Borrower == null)
                    {
                        byteApplication.Borrower = ByteBorrower.Create(rmBorrower: rmBorrower,
                                                                       thirdPartyCodeList: loanFileRequest.ThirdPartyCodeList,
                                                                       byteFileDataId: byteFile.FileDataID);
                    }
                    else
                    {
                        ByteBorrower.Update(byteBorrower: byteApplication.Borrower,
                                            rmBorrower: rmBorrower,
                                            thirdPartyCodeList: loanFileRequest.ThirdPartyCodeList);
                    }
                }
                else
                {
                    if (byteApplication.CoBorrower == null)
                    {
                        byteApplication.CoBorrower = ByteBorrower.Create(rmBorrower: rmBorrower,
                                                                         thirdPartyCodeList: loanFileRequest.ThirdPartyCodeList,
                                                                         byteFileDataId: byteFile.FileDataID);
                    }
                    else
                    {
                        ByteBorrower.Update(byteBorrower: byteApplication.CoBorrower,
                                            rmBorrower: rmBorrower,
                                            thirdPartyCodeList: loanFileRequest.ThirdPartyCodeList);
                    }
                }

            }

            #endregion


            UpdateLoanFileToByte(byteFile: ref byteFile);

            SetAllBorrowers(loanApplication: loanFileRequest.LoanApplication,
                                    byteFile: ref byteFile,
                                    lifeInsuranceEstimatedMonthlyAmount: lifeInsuranceEstimatedMonthlyAmount,
                                    thirdPartyCodeList: loanFileRequest.ThirdPartyCodeList
                                   );

            _logger.LogInformation(message: "ByteFile SendFile End");
            return byteFile;
        }


        public void UpdateLoanFileToByte(ref ByteFile byteFile)
        {
            _logger.LogInformation(message: "ByteFile update call start");

            var updateFileAsyncResponse = UpdateFileAsync(input: byteFile.ToJson(),
                                                          endPoint: "LoanFile/",
                                                          byteProSession: ByteProSession).Result;
            var settings = new JsonSerializerSettings
                           {
                               NullValueHandling = NullValueHandling.Ignore,
                               MissingMemberHandling = MissingMemberHandling.Ignore
                           };
            byteFile = JsonConvert.DeserializeObject<ByteFile>(value: updateFileAsyncResponse,
                                                               settings: settings);
            

            _logger.LogInformation(message: "ByteFile update call end");
        }


        public async Task<string> UpdateFileAsync(string input,
                                                  string endPoint,
                                                  string byteProSession)
        {
            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri(uriString: _baseApiUrl + endPoint),
                              Method = HttpMethod.Post,
                              Content = new StringContent(content: input,
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
                          };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");
            request.Headers.Add(name: "Session",
                                value: ByteProSession);
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            var response = await _httpClient.SendAsync(request: request).ConfigureAwait(continueOnCapturedContext: false);
            if (response.StatusCode == HttpStatusCode.Unauthorized) byteProSession = null;
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            return resp;
        }


        public ByteNewFileResponse CreateLoanApplicationInByte(ByteNewFile newFile)
        {
            _logger.LogInformation(message: "ByteFile CreateLoanApplicationAsync Start");

            var response = CreateFileAsync(newFileContent: newFile.ToJson());

            return JsonConvert.DeserializeObject<ByteNewFileResponse>(value: response.Result);
        }


        public async Task<string> CreateFileAsync(string newFileContent)
        {
            _logger.LogInformation(message: "ByteFile CreateFileAsync start");

            //using (var client = new HttpClient())
            //{

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri(uriString: _baseApiUrl + "NewFile/"),
                              Method = HttpMethod.Post,
                              Content = new StringContent(content: newFileContent,
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
                          };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");
            request.Headers.Add(name: "Session",
                                value: ByteProSession);
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            var response = await _httpClient.SendAsync(request: request).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            _logger.LogInformation(message: "ByteFile EnsureSuccessStatusCode");
            var resp = await response.Content.ReadAsStringAsync();
            try
            {
                var exceptionResponses = JsonConvert.DeserializeObject<List<ExceptionResponse>>(value: resp);

                if (exceptionResponses[index: 0].Type.ToLower() == "exception") throw new ArgumentException(message: exceptionResponses[index: 0].Message);
            }
            catch (JsonSerializationException)
            {
                _logger.LogInformation(message: "ByteFile Didn't DeserializeObject in ExceptionResponse");
            }

            return resp;
            //}
        }

        public async Task<short> GetLoanStatusAsync(int fileDataId)
        {
            short loanStatus = 0;
            var loanStatusResponse = await this.ExecuteByteProGetRequest<StatusResponse>($"Status/{fileDataId}");
            if (loanStatusResponse != null)
            {
                loanStatus = loanStatusResponse.LoanStatus;
            }
            return loanStatus;
        }

        private async Task<TResponse> ExecuteByteProGetRequest<TResponse>(string postFixUrl)
        {
            if (string.IsNullOrEmpty(postFixUrl))
            {
                throw new Exception("Postfix URL cannot be null or empty.");
            }
            else
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(uriString: $"{_baseApiUrl}{postFixUrl}"),
                    Method = HttpMethod.Get,
                };
                request.Content = new StringContent(content: "");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");
                request.Headers.Add(name: "Session",
                                    value: ByteProSession);
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var response = await _httpClient.SendAsync(request: request).ConfigureAwait(continueOnCapturedContext: false);
                response.EnsureSuccessStatusCode();
                var responseString = response.Content.ReadAsStringAsync();
                var embeddedDoc =
                    JsonConvert.DeserializeObject<TResponse>(value: responseString.Result);
                return embeddedDoc;
            }
        }

        private ByteFile GetLoanApplicationFileFromByte(int fileDataId)
        {
            _logger.LogInformation(message: "ByteFile GetLoanApplicationFileFromByte Start");

            var fileData = GetFile(fileDataId: fileDataId).Result;
            var settings = new JsonSerializerSettings
                           {
                               NullValueHandling = NullValueHandling.Ignore,
                               MissingMemberHandling = MissingMemberHandling.Ignore
                           };
            var file = JsonConvert.DeserializeObject<ByteFile>(value: fileData,
                                                               settings: settings);
            _logger.LogInformation(message: "ByteFile GetLoanApplicationFileFromByte End");
            return file;
        }


        private async Task<string> GetFile(int fileDataId)
        {
            _logger.LogInformation(message: "ByteFile GetFileAsync Start");

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriString: _baseApiUrl + "loanfile/" + fileDataId),
                Method = HttpMethod.Get,
                //Content = new StringContent(content: "")
            };
            request.Headers.Add(name: "Session",
                                value: ByteProSession);
            request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            request.Content = new StringContent(content: "");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");

            var response = await _httpClient.SendAsync(request: request).ConfigureAwait(continueOnCapturedContext: false);
            response.EnsureSuccessStatusCode();
            _logger.LogInformation(message: "ByteFile EnsureSuccessStatusCode");
            var resp = await response.Content.ReadAsStringAsync();

            
            return resp;
        }

        #region private Methods



        private void SetAllBorrowers(LoanApplication loanApplication,
                                             ref ByteFile byteFile,
                                             decimal? lifeInsuranceEstimatedMonthlyAmount,
                                             ThirdPartyCodeList thirdPartyCodeList)
        {
            loanApplication.Borrowers.ForEach(action: b => { b.LoanApplication = loanApplication; });

            byteFile = GetLoanApplicationFileFromByte(fileDataId: byteFile.FileDataID);

            for (var rmBorrowerIndex = 0; rmBorrowerIndex < loanApplication.Borrowers.ToList().Count; rmBorrowerIndex++)
            {
                var rmBorrower = loanApplication.Borrowers.ToList()[index: rmBorrowerIndex];
                var byteApplicationIndex = (int) Math.Floor(d: rmBorrowerIndex / 2m);
                var byteApplication = byteFile.Applications[index: byteApplicationIndex];
                
                var byteBorrower = rmBorrowerIndex % 2 == 0 ? byteApplication.Borrower : byteApplication.CoBorrower;

                FillBorrower(byteBorrower: ref byteBorrower,
                             rmBorrower: rmBorrower,
                             fileDataId: byteFile.FileDataID,
                             thirdPartyCodeList: thirdPartyCodeList,
                             byteApplication: byteApplication);

             
                
            }

            var byteFileBeforeUpdate = byteFile;

            UpdateLoanFileToByte(byteFile: ref byteFile);

            for (var rmBorrowerIndex = 0; rmBorrowerIndex < loanApplication.Borrowers.ToList().Count; rmBorrowerIndex++)
            {
                var rmBorrower = loanApplication.Borrowers.ToList()[index: rmBorrowerIndex];
                var byteApplicationIndex = (int)Math.Floor(d: rmBorrowerIndex / 2m);
                var byteApplicationBeforeUpdate = byteFileBeforeUpdate.Applications[index: byteApplicationIndex];
                var byteBorrowerBeforeUpdate = rmBorrowerIndex % 2 == 0 ? byteApplicationBeforeUpdate.Borrower : byteApplicationBeforeUpdate.CoBorrower;

                var employerWithRmId = byteBorrowerBeforeUpdate.Employers.ToList();
                var incomesWithRmId = byteBorrowerBeforeUpdate.Incomes.ToList();


                var byteApplicationAfterUpdate = byteFile.Applications[index: byteApplicationIndex];
                var byteBorrowerAfterUpdate = rmBorrowerIndex % 2 == 0 ? byteApplicationAfterUpdate.Borrower : byteApplicationAfterUpdate.CoBorrower;

                #region Income Employee Mapping

                var config = new MapperConfiguration(configure: cfg =>
                {
                    cfg.CreateMap<Employer, Employer>()
                       .ForMember(destinationMember: dest => dest.RmEmploymentInfoid,
                                  memberOptions: options => options.Ignore()); // Do not overwrite RMEmploymentInfoId as we need to preserve.
                    cfg.CreateMap<Income, Income>()
                       .ForMember(destinationMember: dest => dest.RMEmploymentInfoId,
                                  memberOptions: options => options.Ignore()); // Do not overwrite RMEmploymentInfoId as we need to preserve.
                });

                var mapper = config.CreateMapper();
                //mapper.Map<List<Income>, List<Income>>(byteFile.Applications[0].Borrower.Incomes, incomesWithRMId);
                var employerCount = employerWithRmId.Count();
                for (var i = 0; i < employerCount; i++)
                    mapper.Map(source: byteBorrowerAfterUpdate.Employers[index: i],
                               destination: employerWithRmId[index: i]);
                var incomeCount = incomesWithRmId.Count();
                for (var i = 0; i < incomeCount; i++)
                    mapper.Map(source: byteBorrowerAfterUpdate.Incomes[index: i],
                               destination: incomesWithRmId[index: i]);

                byteBorrowerAfterUpdate.Employers = employerWithRmId;
                byteBorrowerAfterUpdate.Incomes = incomesWithRmId;

                foreach (var emp in byteBorrowerAfterUpdate.Employers)
                {
                    var employerIncomes = byteBorrowerAfterUpdate.Incomes
                                                                 .Where(predicate: inc => inc.RMEmploymentInfoId == emp.RmEmploymentInfoid)
                                                                 .ToList();
                    foreach (var inc in employerIncomes) inc.EmployerId = emp.EmployerId;
                }
            #endregion

            }

            UpdateLoanFileToByte(byteFile: ref byteFile);
        }


        private void FillBorrower(ref ByteBorrower byteBorrower,
                                  Borrower rmBorrower,
                                  int fileDataId,
                                  ThirdPartyCodeList thirdPartyCodeList,
                                  Application byteApplication)
        {
            //byteBorrower.Residences = Residence.Create(borrowerResidences: rmBorrower.BorrowerResidences.ToList());

            rmBorrower.BorrowerResidences = rmBorrower.BorrowerResidences.OrderByDescending(keySelector: borrowerResidence => borrowerResidence.FromDate).ToList();

            if (rmBorrower.BorrowerResidences.HasValue())
                for (var i = 0; i < rmBorrower.BorrowerResidences.Count; i++)
                {
                    if (!(byteBorrower.Residences.Count >= i + 1)) byteBorrower.Residences.Add(item: new Residence());
                    byteBorrower.Residences[index: i].Update(rmBorrowerResidence: rmBorrower.BorrowerResidences[index: i],
                                                             current: i == 0,
                                                             fileDataId: fileDataId);
                }

            byteBorrower.Employers = Employer.Create(rmBorrower: rmBorrower,
                                                     byteEmployers: byteBorrower.Employers,
                                                     fileDataId: fileDataId);


            byteBorrower.Incomes = Income.Create(borrower: rmBorrower,
                                                 fileDataId: fileDataId,
                                                 thirdPartyCodeList: thirdPartyCodeList);

            byteBorrower.Debts = Debt.Create(rmBorrower: rmBorrower,
                                             fileDataId: fileDataId,
                                             thirdPartyCodeList: thirdPartyCodeList);
            byteBorrower.Assets = Asset.Create(rmBorrower: rmBorrower,
                                               thirdPartyCodeList: thirdPartyCodeList,
                                               application: byteApplication,
                                               fileDataId: fileDataId);
            byteBorrower.REOs = Reo.Create(rmBorrower: rmBorrower,
                                           fileDataId: fileDataId,
                                           thirdPartyCodeList: thirdPartyCodeList);

            byteBorrower.Borrower.SetDeclaration(rmBorrower: rmBorrower,
                                                 thirdPartyCodeList: thirdPartyCodeList);
            byteBorrower.Borrower.SetGovernmentQuestion(rmBorrower: rmBorrower);
        }

        

        #endregion
		
	
		
		 
    }
}