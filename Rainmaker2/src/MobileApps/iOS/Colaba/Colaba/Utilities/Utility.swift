//
//  Utility.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import Foundation
import UIKit
import NVActivityIndicatorView

struct Utility {
    
    static let authStoryboard = UIStoryboard.init(name: "Authentication", bundle: nil)
    static let mainStoryboard = UIStoryboard.init(name: "Main", bundle: nil)
    
    static func getLoginNavigationVC() -> UINavigationController{
        return authStoryboard.instantiateViewController(identifier: "LoginNavigation")
    }
    
    static func getFaceRecognitionVC() -> FaceRecognitionViewController{
        return authStoryboard.instantiateViewController(identifier: "FaceRecognitionViewController") as! FaceRecognitionViewController
    }
    
    static func getFingerPrintVC() -> FingerPrintViewController{
        return authStoryboard.instantiateViewController(identifier: "FingerPrintViewController") as! FingerPrintViewController
    }
    
    static func getLoginVC() -> LoginViewController{
        return authStoryboard.instantiateViewController(identifier: "LoginViewController") as! LoginViewController
    }
    
    static func getForgotPasswordVC() -> ForgotPasswordViewController{
        return authStoryboard.instantiateViewController(identifier: "ForgotPasswordViewController") as! ForgotPasswordViewController
    }
    
    static func getResetPasswordSuccessfullVC() -> ResetPasswordSuccessfullViewController{
        return authStoryboard.instantiateViewController(identifier: "ResetPasswordSuccessfullViewController") as! ResetPasswordSuccessfullViewController
    }
    
    static func getPhoneNumberVC() -> PhoneNumberViewController{
        return authStoryboard.instantiateViewController(identifier: "PhoneNumberViewController") as! PhoneNumberViewController
    }
    
    static func getCodeVC() -> CodeViewController{
        return authStoryboard.instantiateViewController(identifier: "CodeViewController") as! CodeViewController
    }
    
    static func getDashboardVC() -> DashboardViewController{
        return mainStoryboard.instantiateViewController(identifier: "DashboardViewController") as! DashboardViewController
    }
    
    static func getDashboardNavVC() -> UINavigationController{
        return mainStoryboard.instantiateViewController(identifier: "DashboardNavigation")
    }
    
    
    static func checkDeviceAuthType() -> String {
         let authType = LocalAuthManager.shared.biometricType
            switch authType {
            case .none:
                return "Device not registered with TouchID/FaceID"
            case .touchID:
                return kTouchID
            case .faceID:
                return kFaceID
            }
     }

    static func showOrHideLoader(shouldShow: Bool){
    
        let activityData = ActivityData(size: CGSize(width: 50, height: 50), message: "", messageFont: nil, messageSpacing: nil, type: .circleStrokeSpin, color: UIColor.init(red: 0.0, green: 0.0, blue: 0.0, alpha: 0.40), padding: nil, displayTimeThreshold: nil, minimumDisplayTime: 2, backgroundColor: .clear, textColor: nil)
        if (shouldShow){
            NVActivityIndicatorPresenter.sharedInstance.startAnimating(activityData)
        }
        else{
            NVActivityIndicatorPresenter.sharedInstance.stopAnimating()
        }
    }
    
    static func getUserAccessToken() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.token
        }
        return ""
    }
    
    static func getUserRefreshToken() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.refreshToken
        }
        return ""
    }
}
