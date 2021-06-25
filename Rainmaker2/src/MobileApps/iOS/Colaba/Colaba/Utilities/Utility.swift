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
        return authStoryboard.instantiateViewController(withIdentifier: "LoginNavigation")  as! UINavigationController
    }
    
    static func getFaceRecognitionVC() -> FaceRecognitionViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "FaceRecognitionViewController") as! FaceRecognitionViewController
    }
    
    static func getFingerPrintVC() -> FingerPrintViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "FingerPrintViewController") as! FingerPrintViewController
    }
    
    static func getLoginVC() -> LoginViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "LoginViewController") as! LoginViewController
    }
    
    static func getForgotPasswordVC() -> ForgotPasswordViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "ForgotPasswordViewController") as! ForgotPasswordViewController
    }
    
    static func getResetPasswordSuccessfullVC() -> ResetPasswordSuccessfullViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "ResetPasswordSuccessfullViewController") as! ResetPasswordSuccessfullViewController
    }
    
    static func getPhoneNumberVC() -> PhoneNumberViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "PhoneNumberViewController") as! PhoneNumberViewController
    }
    
    static func getCodeVC() -> CodeViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "CodeViewController") as! CodeViewController
    }
    
    static func getMainTabBarVC() -> MainTabBarViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "MainTabBarViewController") as! MainTabBarViewController
    }
    
    static func getDashboardVC() -> DashboardViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "DashboardViewController") as! DashboardViewController
    }
    
    static func getDummyDashboardVC() -> DummyDashboardViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "DummyDashboardViewController") as! DummyDashboardViewController
    }
    
    static func getDashboardNavVC() -> UINavigationController{
        return mainStoryboard.instantiateViewController(withIdentifier: "DashboardNavigation") as! UINavigationController
    }
    
    static func getPipelineVC() -> PipelineViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "PipelineViewController") as! PipelineViewController
    }
    
    static func getPipelineMoreVC() -> PipelineMoreViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "PipelineMoreViewController") as! PipelineMoreViewController
    }
    
    static func getFiltersVC() -> FiltersViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "FiltersViewController") as! FiltersViewController
    }
    
    static func getSearchVC() -> SearchViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "SearchViewController") as! SearchViewController
    }
    
    static func getCreateNewPopupVC() -> CreateNewPopupViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "CreateNewPopupViewController") as! CreateNewPopupViewController
    }
    
    static func checkDeviceAuthType() -> String {
         let authType = LocalAuthManager.shared.biometricType
            switch authType {
            case .none:
                return kDeviceNotRegistered
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
    
    static func getUserFirstName() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.firstName
        }
        return ""
    }
    
    static func getUserLastName() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.lastName
        }
        return ""
    }
    
    static func getUserFullName() -> String{
        if let user = UserModel.getCurrentUser(){
            return "\(user.firstName) \(user.lastName)"
        }
        return ""
    }
    
//    static func getAddress() -> String{
//        let address = street unit city state zip country
//        return ""
//    }
}
