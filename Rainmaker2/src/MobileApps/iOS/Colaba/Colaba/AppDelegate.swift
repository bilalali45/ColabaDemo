//
//  AppDelegate.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit
import IQKeyboardManagerSwift
import DropDown

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
        return true
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
        let vc = Utility.getBorrowerInformationVC()
        //let vc = Utility.getAddResidenceVC()
        self.window?.rootViewController = vc
    }
    
    func loadLoginViewController(){
        let vc = Utility.getLoginNavigationVC()
        self.window?.rootViewController = vc
    }
    
    func loadFingerPrintViewController(){
        let vc = Utility.getFingerPrintVC()
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        self.window?.rootViewController = navVC
    }
    
    func loadFaceLockViewController(){
        let vc = Utility.getFaceRecognitionVC()
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        self.window?.rootViewController = navVC
    }
}

