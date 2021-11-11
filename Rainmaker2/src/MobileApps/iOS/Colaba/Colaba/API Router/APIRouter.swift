//
//  APIRouter.swift
//  Colaba
//
//  Created by Murtaza on 25/05/2021.
//

import UIKit
import Alamofire
import SwiftyJSON
import NVActivityIndicatorView

typealias completionBlock = (StatusCodes,JSON,String) -> ()
typealias fileCompletionBlock = (StatusCodes, Data?, String) -> ()

enum EndPoint:String{
    
    case getMCUConfiguration = "identity/mcuaccount/GetMcuTenantTwoFaValues"
    case login = "identity/mcuaccount/signin"
    case forgotPassword = "identity/mcuaccount/ForgotPasswordRequest"
    case skip2FA = "identity/mcuaccount/SkipTwoFa"
    case dontAskFor2FA = "identity/mcuaccount/DontAskTwoFa"
    case send2FA = "identity/mcuaccount/SendTwoFa"
    case send2FAToPhoneNo = "identity/mcuaccount/SendTwoFaToNumber?PhoneNumber="
    case verify2FA = "identity/mcuaccount/VerifyTwoFa"
    case refreshAccessToken = "identity/mcuaccount/RefreshAccessToken"
    case get2FASettings = "identity/mcuaccount/GetTwoFaSettings"
    case getPipelineList = "loanapplication/loan/GetListForPipeline?"
    case getNewPipelineRecords = "loanapplication/loan/AreNewRecordsAvailable"
    case searchLoans = "loanapplication/loan/search?"
    case getNotificationCount = "Notification/notification/getcount"
    case getNotificationsList = "Notification/notification/GetPaged?"
    case readNotification = "Notification/notification/Read"
    case seenNotification = "Notification/notification/Seen"
    case deleteNotifications = "Notification/notification/Delete"
    case getLoanInformation = "loanapplication/loan/getloaninfo?"
    case getLoanDocuments = "documentmanagement/mcudocument/getdocuments?"
    case viewLoanDocument = "documentmanagement/mcudocument/View?"
    case getLoanApplicationData = "loanapplication/Loan/GetLoanApplicationSummary?"
    case getAllPropertyTypeDrowpDown = "loanapplication/Loan/GetAllPropertyTypeDropDown"
    case getAllOccupancyTypeDropDown = "loanapplication/Loan/GetPropertyUsageDropDown"
    case getCoBorrowersOccupancyStatus = "loanapplication/SubjectProperty/GetCoBorrowersOccupancyStatus?"
    case getPurchaseSubjectPropertyDetail = "loanapplication/SubjectProperty/GetSubjectPropertyDetails?"
    case getAllCountries = "loanapplication/Loan/GetCountries"
    case getAllStates = "loanapplication/Loan/GetStates"
    case getAllCounties = "loanapplication/Loan/GetCounties"
    case getRefinanceSubjectPropertyDetail = "loanapplication/SubjectProperty/GetRefinanceSubjectPropertyDetail?"
    case getLoanInfoDetail = "loanapplication/Loan/GetLoanInfoDetails?"
    case getLoanGoals = "loanapplication/Loan/GetAllLoanGoals?"
    case getAllPropertyStatus = "loanapplication/Loan/GetAllPropertyStatusDropDown"
    case getRealEstateDetail = "loanapplication/RealEstate/GetRealEstateDetails?"
    case getFirstMortgageDetail = "loanapplication/RealEstate/GetFirstMortgageDetails?"
    case getSecondMortgageDetail = "loanapplication/RealEstate/GetSecondMortgageDetails?"
    case getIncomeDetail = "loanapplication/Assets/GetIncomeDetails?"
    case getAssetsDetail = "loanapplication/Assets/GetAssetsDetails?"
    case getGovernmentQuestions = "loanapplication/GovtQuestions/GetGovernmentQuestions?"
    case getAllRaceList = "loanapplication/Loan/GetAllRaceList"
    case getAllEthnicityList = "loanapplication/Loan/GetAllEthnicityList"
    case getAllGenderList = "loanapplication/Loan/GetGenderList"
    case getDemographicInformation = "loanapplication/GovtQuestions/GetDemographicInformation?"
    case getMaritalStatusList = "loanapplication/Loan/GetMaritalStatusList"
    case getHousingStatus = "loanapplication/Loan/GetHousingStatus"
    case getRelationshipType = "loanapplication/Loan/GetRelationshipTypes"
    case getCitizenShip = "loanapplication/Loan/GetCitizenship"
    case getVisaStatus = "loanapplication/Loan/GetVisaStatus?"
    case getMilitartyAffiliation = "loanapplication/Loan/GetMilitaryAffiliation"
    case getBorrowerDetail = "loanapplication/Borrower/GetBorrowerDetails?"
    case getAllBankAccountType = "loanapplication/Assets/GetBankAccountType"
    case getBankAccountDetail = "loanapplication/Assets/GetBankAccountDetails?"
    case getRetirementAccountDetail = "loanapplication/Assets/GetRetirementAccountDetails?"
    case getAllFinancialAssets = "loanapplication/Assets/GetAllFinancialAsset"
    case getFinancialAccountDetail = "loanapplication/Assets/GetFinancialAssetDetails?"
    case getAssetsTypes = "loanapplication/Assets/GetAssetTypesByCategory?"
    case getOtherAssetsDetail = "loanapplication/Assets/GetOtherAssetDetails?"
    case getGiftSources = "loanapplication/Assets/GetAllGiftSources"
    case getGiftAssetsDetail = "loanapplication/Assets/GetGiftAssetDetails?"
    case getProceedsFromNonRealEstateDetail = "loanapplication/Assets/GetFromLoanNonRealStateDetails?"
    case getProceedsFromRealEstateDetail = "loanapplication/Assets/GetFromLoanRealStateDetails?"
    case getProceedsFromLoan = "loanapplication/Assets/GetProceedsfromloanDetails?"
    case getEmploymentDetail = "loanapplication/Assets/GetEmploymentDetail?"
    case getSelfEmploymentDetail = "loanapplication/Assets/GetSelfBusinessIncome?"
    case getAllBusinessType = "loanapplication/Assets/GetAllBusinessTypes"
    case getBusinessIncomeDetail = "loanapplication/Assets/GetBusinessIncome?"
    case getMilitaryPayDetail = "loanapplication/Assets/GetMilitaryIncome?"
    case getAllRetirementTypes = "loanapplication/Assets/GetRetirementIncomeTypes"
    case getRetirementIncomeDetail = "loanapplication/Assets/GetRetirementIncomeInfo?"
    case getOtherIncomeType = "loanapplication/Assets/GetOtherIncomeTypes"
    case getOtherIncomeDetail = "loanapplication/Assets/GetOtherIncomeInfo?"
    case updatePurchaseSubjectProperty = "loanapplication/SubjectProperty/AddOrUpdateSubjectPropertyDetail"
    case updateRefinanceSubjectProperty = "loanapplication/SubjectProperty/AddOrUpdateRefinanceSubjectPropertyDetail"
    case updateCoBorrowerOccupancyStatus = "loanapplication/SubjectProperty/AddOrUpdateCoBorrowerOccupancyStatus"
    case updateLoanInformation = "loanapplication/Loan/AddOrUpdateLoanInformation"
    case deleteAsset = "loanapplication/Assets/DeleteAsset?"
    case addUpdateBankAccount = "loanapplication/Assets/AddOrUpdateBankAccount"
    case addUpdateRetirementAccount = "loanapplication/Assets/AddOrUpdateRetirementAccount"
    case addUpdateFinancialAccount = "loanapplication/Assets/AddOrUpdateFinancialAsset"
    case addUpdateProceedsFromLoan = "loanapplication/Assets/AddOrUpdateProceedsfromloan"
    case addUpdateProceedFromLoanOther = "loanapplication/Assets/AddOrUpdateProceedsfromloanOther"
    case addUpdateProceedFromRealState = "loanapplication/Assets/AddOrUpdateAssestsRealState"
    case addUpdateProceedFromNonRealState = "loanapplication/Assets/AddOrUpdateAssestsNonRealState"
    case addUpdateGiftFunds = "loanapplication/Assets/AddOrUpdateGiftAsset"
    case addUpdateOtherAssets = "loanapplication/Assets/AddOrUpdateOtherAssetsInfo"
    case deleteIncome = "loanapplication/Assets/DeleteIncome?"
    case addUpdateCurrentEmployment = "loanapplication/Assets/AddOrUpdateCurrentEmploymentDetail"
    case addUpdatePreviousEmployment = "loanapplication/Assets/AddOrUpdatePreviousEmploymentDetail"
    case addUpdateSelfEmployment = "loanapplication/Assets/AddOrUpdateSelfBusiness"
    case addUpdateBusiness = "loanapplication/Assets/AddOrUpdateBusiness"
    case addUpdateMilitaryPay = "loanapplication/Assets/AddOrUpdateMilitaryIncome"
    case logout = "identity/mcuaccount/Logout"
    
}

enum StatusCodes{
    
    case success
    case failure
    case blockByAdmin
    case authError
    case internetError
    
}

class APIRouter: NSObject {
    
    static let sharedInstance = APIRouter()
    
    private override init() {
        
    }
    
    func executeAPI(type: EndPoint,method: HTTPMethod, params: Parameters? = nil, extraHeaderKey: String? = nil, extraHeaderValue: String? = nil, extraData: String? = nil, completion: completionBlock?){
    
        URLCache.shared.removeAllCachedResponses()
       
        var request: DataRequest!
        
        var headers = HTTPHeaders()
        
        if let exHeaderKey = extraHeaderKey, let exHeaderValue = extraHeaderValue{
            headers = ["Accept":"application/json",
                       exHeaderKey: exHeaderValue]
        }
        else if (type == .login){
            
            var dontAskValue = ""
            
            if let dontAsk2FAValue = UserDefaults.standard.value(forKey: kDontAsk2FAValue){
                dontAskValue = dontAsk2FAValue as! String
            }
            
            headers = ["dontAskTwoFaIdentifier": dontAskValue,
                       "Accept":"application/json"]
        }
        else if (type != .getMCUConfiguration && type != .login && type != .forgotPassword && type != .skip2FA && type != .send2FA && type != .send2FAToPhoneNo && type != .verify2FA && type != .get2FASettings && type != .refreshAccessToken){
            
            if let user = UserModel.getCurrentUser(){
                headers = ["Accept":"application/json",
                           "Authorization": "Bearer \(user.token)"]
            }
            else{
                headers = ["Accept":"application/json"]
            }
            
        }
        else{
            headers = ["Accept":"application/json"]
        }
        
        var endPoint = ""
        if let extraValueAfterEndPoint = extraData{
            endPoint = "\(type.rawValue)\(extraValueAfterEndPoint)"
        }
        else{
            endPoint = type.rawValue
        }
        
        request = AF.request(BASEURL + endPoint, method: method, parameters: params, encoding: method == .post ? JSONEncoding.default : URLEncoding.queryString, headers:headers)
        
        request.responseJSON { response in
            
            if response.response?.statusCode == 401{
                completion?(.authError,JSON.null,"UnAuthorized User")
                return
            }
            
            if let error = response.error{
                var errorDescription = error.localizedDescription
                if errorDescription == "The Internet connection appears to be offline." || errorDescription == "URLSessionTask failed with error: A data connection is not currently allowed." || errorDescription == "URLSessionTask failed with error: The Internet connection appears to be offline."{
                    errorDescription = "No Internet Connection"
                    completion?(.internetError,JSON.null,errorDescription)
                }
                else{
                    completion?(.failure,JSON.null,errorDescription)
                }
                
                return
            }
            
            if let value = response.value {
                let json = JSON(value)
                
                if (response.response?.statusCode == 200){
                    completion?(.success,json,json["message"].stringValue)
                }
                else if (response.response?.statusCode == 400){
                    completion?(.failure,json,json["message"].stringValue)
                }
                else if (response.response?.statusCode == 500 || response.response?.statusCode == 501 || response.response?.statusCode == 502 || response.response?.statusCode == 503){
                    completion?(.failure,json,json["Message"].stringValue)
                }
                else{
                    let errorMessage = json["message"].stringValue
                    completion?(.failure,json, errorMessage == "" ? "Something went wrong" : errorMessage)
                }
                
                return
            }
            
            completion?(.failure,JSON.null,NSLocalizedString("Something went wrong, please try again!", comment: ""))
        }
    
    }
    
    func executeDashboardAPIs(type: EndPoint,method: HTTPMethod, params: Parameters? = nil, extraHeaderKey: String? = nil, extraHeaderValue: String? = nil, extraData: String? = nil, completion: completionBlock?){
    
        URLCache.shared.removeAllCachedResponses()
       
        var request: DataRequest!
        
        var headers = HTTPHeaders()
        
        if let user = UserModel.getCurrentUser(){
            headers = ["Accept":"application/json",
                       "Authorization": "Bearer \(user.token)"]
        }
        else{
            headers = ["Accept":"application/json"]
        }
        
        var endPoint = ""
        if let extraValueAfterEndPoint = extraData{
            endPoint = "\(type.rawValue)\(extraValueAfterEndPoint)"
        }
        else{
            endPoint = type.rawValue
        }
        
        request = AF.request(BASEURL + endPoint, method: method, parameters: params, encoding: method == .get ? URLEncoding.queryString : JSONEncoding.default, headers:headers)
        
        request.responseJSON { response in
            
            if response.response?.statusCode == 401{
                completion?(.authError,JSON.null,"UnAuthorized User")
                return
            }
            
            if let error = response.error{
                var errorDescription = error.localizedDescription
                if errorDescription == "The Internet connection appears to be offline." || errorDescription == "URLSessionTask failed with error: A data connection is not currently allowed." || errorDescription == "URLSessionTask failed with error: The Internet connection appears to be offline."{
                    errorDescription = "No Internet Connection"
                    completion?(.internetError,JSON.null,errorDescription)
                }
                else{
                    completion?(.failure,JSON.null,errorDescription)
                }
                
                return
            }
            
            if let value = response.value {
                let json = JSON(value)
                
                if (response.response?.statusCode == 200){
                    completion?(.success,json,json["message"].stringValue)
                }
                else if (response.response?.statusCode == 400){
                    completion?(.failure,json,json["message"].stringValue)
                }
                else if (response.response?.statusCode == 500 || response.response?.statusCode == 501 || response.response?.statusCode == 502 || response.response?.statusCode == 503){
                    completion?(.failure,json,json["Message"].stringValue)
                }
                else{
                    let errorMessage = json["message"].stringValue
                    completion?(.failure,json, errorMessage == "" ? "Something went wrong" : errorMessage)
                }
                
                return
            }
            
            completion?(.failure,JSON.null,NSLocalizedString("Something went wrong, please try again!", comment: ""))
        }
    
    }
    
    func downloadFileFromRequest(type: EndPoint,method: HTTPMethod, params: Parameters? = nil, extraData: String? = nil, completion: fileCompletionBlock?){
        
        var request: DataRequest!
        var headers = HTTPHeaders()
        headers = ["Accept":"application/json",
                   "Authorization": "Bearer \(Utility.getUserAccessToken())"]
        
        var endPoint = ""
        if let extraValueAfterEndPoint = extraData{
            endPoint = "\(type.rawValue)\(extraValueAfterEndPoint)"
        }
        else{
            endPoint = type.rawValue
        }
        
        request = AF.request(BASEURL + endPoint, method: .get, parameters: nil, encoding: URLEncoding.queryString, headers:headers)
        request.downloadProgress(closure: { progress in
            let progressPercentage = progress.fractionCompleted * 100
            print("PROGRESS PERCENTAGE ISSSSSS \(progressPercentage)")
            
            let activityData = ActivityData(size: CGSize(width: 50, height: 50), message: String(format: "%.0f%%", progressPercentage), messageFont: Theme.getRubikMediumFont(size: 15), messageSpacing: nil, type: .circleStrokeSpin, color: UIColor.init(red: 0.0, green: 0.0, blue: 0.0, alpha: 0.40), padding: nil, displayTimeThreshold: nil, minimumDisplayTime: 0, backgroundColor: .clear, textColor: Theme.getAppGreyColor())
            NVActivityIndicatorPresenter.sharedInstance.startAnimating(activityData)
            NVActivityIndicatorPresenter.sharedInstance.setMessage(String(format: "%.0f%%", progressPercentage))
                
        }).responseData { response in
            NVActivityIndicatorPresenter.sharedInstance.stopAnimating()
            if response.response?.statusCode == 401{
                completion?(.authError,nil,"UnAuthorized User")
                return
            }
            
            if let error = response.error{
                var errorDescription = error.localizedDescription
                if errorDescription == "The Internet connection appears to be offline." || errorDescription == "URLSessionTask failed with error: A data connection is not currently allowed." || errorDescription == "URLSessionTask failed with error: The Internet connection appears to be offline."{
                    errorDescription = "No Internet Connection"
                    completion?(.internetError,nil,errorDescription)
                }
                else{
                    completion?(.failure,nil,"Fail to download file")
                }
                
                return
            }
            if let data = response.value{
                
                if (response.response?.statusCode == 200){
                    completion?(.success,data,"file downloaded")
                }
                else if (response.response?.statusCode == 400){
                    completion?(.failure,nil,"Fail to download file")
                }
                else if (response.response?.statusCode == 500 || response.response?.statusCode == 501 || response.response?.statusCode == 502 || response.response?.statusCode == 503){
                    completion?(.failure,nil,"Internal server error")
                }
                else{
                    completion?(.failure,nil,"Something went wrong")
                }
                
                return
            }
        }
        
    }
        
}
