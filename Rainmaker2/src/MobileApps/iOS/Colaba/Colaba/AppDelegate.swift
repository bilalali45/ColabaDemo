//
//  AppDelegate.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit
import IQKeyboardManagerSwift
import DropDown
import GooglePlaces
import GoogleMaps

@main
class AppDelegate: UIResponder, UIApplicationDelegate {

    var window: UIWindow?
    
    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        // Override point for customization after application launch.
        self.window = UIWindow(frame: UIScreen.main.bounds)
        IQKeyboardManager.shared.enable = true
        IQKeyboardManager.shared.shouldResignOnTouchOutside = true
        self.showInitialViewController()
        DropDown.startListeningToKeyboard()
        
        GMSPlacesClient.provideAPIKey(kGoogleAPIKey)
        GMSServices.provideAPIKey(kGoogleAPIKey)
        
        return true
    }
    
    func applicationWillEnterForeground(_ application: UIApplication) {
        if (UserModel.getCurrentUser() != nil && (self.window?.rootViewController is MainTabBarViewController)){
            
            if (self.window?.rootViewController?.presentedViewController == nil){
                isAppOpenFromBackground = true
                self.window?.rootViewController?.present(getBioMetricOrLoginController(), animated: false, completion: nil)
            }
            else{
                if (!(self.window?.rootViewController?.presentedViewController is FaceRecognitionViewController) && !(self.window?.rootViewController?.presentedViewController is FingerPrintViewController) && !(self.window?.rootViewController?.presentedViewController is LoginNavigationViewController)) {
                    
                    let keyWindow = UIApplication.shared.windows.filter {$0.isKeyWindow}.first

                    if var topController = keyWindow?.rootViewController {
                        while let presentedViewController = topController.presentedViewController {
                            topController = presentedViewController
                        }
                        if (!(topController is FaceLockNavigationViewController) && !(topController is FingerPrintNavigationViewController) && !(topController is LoginNavigationViewController)){
                            isAppOpenFromBackground = true
                            topController.present(getBioMetricOrLoginController(), animated: false, completion: nil)
                        }
                    }
                }
            }
        }
    }
    
    func getBioMetricOrLoginController() -> UIViewController{
        let faceNavVC = Utility.getFaceLockNavigationVC()
        faceNavVC.modalPresentationStyle = .overFullScreen
        
        let fingerNavVC = Utility.getFingerPrintNavigationVC()
        fingerNavVC.modalPresentationStyle = .overFullScreen
        
        let loginNavVC = Utility.getLoginNavigationVC()
        loginNavVC.modalPresentationStyle = .overFullScreen
        
        var isAlreadyRegisteredWithBiometric = ""
        if let isBiometricRegistered = UserDefaults.standard.value(forKey: kIsUserRegisteredWithBiometric){
            isAlreadyRegisteredWithBiometric = isBiometricRegistered as! String
        }

        if (isAlreadyRegisteredWithBiometric == kYes && UserModel.getCurrentUser() != nil){
            if (Utility.checkDeviceAuthType() == kTouchID){
                return fingerNavVC
            }
            else if (Utility.checkDeviceAuthType() == kFaceID){
                return faceNavVC
            }
            else{
                return loginNavVC
            }
        }
        else{
            return loginNavVC
        }
        
    }

    func showInitialViewController(){
            
//        var isAlreadyRegisteredWithBiometric = ""
//        if let isBiometricRegistered = UserDefaults.standard.value(forKey: kIsUserRegisteredWithBiometric){
//            isAlreadyRegisteredWithBiometric = isBiometricRegistered as! String
//        }
//
//        if (isAlreadyRegisteredWithBiometric == kYes && UserModel.getCurrentUser() != nil){
//            if (Utility.checkDeviceAuthType() == kTouchID){
//                loadFingerPrintViewController()
//            }
//            else if (Utility.checkDeviceAuthType() == kFaceID){
//                loadFaceLockViewController()
//            }
//            else{
//                loadLoginViewController()
//            }
//        }
//        else{
//            loadLoginViewController()
//        }
        self.loadBorrowerInfoController()
        self.window?.makeKeyAndVisible()
    }

    func loadDashboardViewController(){
        let vc = Utility.getDashboardNavVC()
        self.window?.rootViewController = vc
    }
    
    func loadBorrowerInfoController(){
//        let vc = Utility.getSendDocumentRequestVC()
//        //let vc = Utility.getTestVC()
//        self.window?.rootViewController = vc
        
        let vc = Utility.getLoanDetailVC()
        vc.loanApplicationId = 5
        vc.borrowerName = "Quentin Finley"
        vc.loanPurpose = "Purchase"
        vc.phoneNumber = ""
        vc.email = ""
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        navVC.modalPresentationStyle = .fullScreen
        self.window?.rootViewController = navVC
    }
    
    func loadLoginViewController(){
        let vc = Utility.getLoginNavigationVC()
        self.window?.rootViewController = vc
    }
    
    func loadFingerPrintViewController(){
        let vc = Utility.getFingerPrintNavigationVC()
        self.window?.rootViewController = vc
    }
    
    func loadFaceLockViewController(){
        let vc = Utility.getFaceLockNavigationVC()
        self.window?.rootViewController = vc
    }
}
