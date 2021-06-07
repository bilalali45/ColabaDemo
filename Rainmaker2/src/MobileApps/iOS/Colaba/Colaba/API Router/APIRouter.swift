//
//  APIRouter.swift
//  Colaba
//
//  Created by Murtaza on 25/05/2021.
//

import UIKit
import Alamofire
import SwiftyJSON

typealias completionBlock = (StatusCodes,JSON,String) -> ()

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
        else if (type != .getMCUConfiguration && type != .login && type != .forgotPassword && type != .skip2FA && type != .send2FA && type != .send2FAToPhoneNo && type != .verify2FA && type != .get2FASettings){
            
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
        
        request = AF.request(BASEURL + endPoint, method: method, parameters: params, encoding: method == .get ? URLEncoding.queryString : JSONEncoding.default, headers:headers)
        
        request.responseJSON { response in
            
            if response.response?.statusCode == 401{
                completion?(.authError,JSON.null,"UnAuthorized User")
                return
            }
            
            if let error = response.error{
                var errorDescription = error.localizedDescription
                if errorDescription == "The Internet connection appears to be offline."{
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
                    completion?(.success,JSON(value),json["message"].stringValue)
                }
                else if (response.response?.statusCode == 400){
                    completion?(.failure,JSON(value),json["message"].stringValue)
                }
                else if (response.response?.statusCode == 503){
                    completion?(.failure,JSON(value),"Server Error")
                }
                else{
                    let errorMessage = json["message"].stringValue
                    completion?(.failure,JSON(value), errorMessage == "" ? "Something went wrong" : errorMessage)
                }
                
                return
            }
            
            completion?(.failure,JSON.null,NSLocalizedString("Something went wrong, please try again!", comment: ""))
        }
    
    }
        
}
