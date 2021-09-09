//
//  ExtensionOfViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import Foundation
import UIKit

extension UIViewController{
    
    func pushToVC(vc: UIViewController){
        self.navigationController?.pushViewController(vc, animated: true)
    }
    
    func goBack(){
        self.navigationController?.popViewController(animated: true)
    }
    
    func goToRoot(){
        self.navigationController?.popToRootViewController(animated: true)
    }
    
    func presentVC(vc: UIViewController){
        self.present(vc, animated: true, completion: nil)
    }
    
    func dismissVC(){
        self.dismiss(animated: true, completion: nil)
    }
    
    func goToLogin(){
        let vc = Utility.getLoginNavigationVC()
        UIApplication.shared.windows.first?.rootViewController = vc
        UIApplication.shared.windows.first?.makeKeyAndVisible()
    }
    
    func goToDashboard(){
        //let vc = Utility.getDummyDashboardVC()
        if (isAppOpenFromBackground){
            self.dismissVC()
        }
        else{
            let vc = Utility.getMainTabBarVC()
            UIApplication.shared.windows.first?.rootViewController = vc
            UIApplication.shared.windows.first?.makeKeyAndVisible()
        }
        
    }
    
    func presentPopup(message: String){
        let alert = UIAlertController(title: "Error", message: message, preferredStyle: .alert)
        let okAction = UIAlertAction(title: "Ok", style: .default, handler: nil)
        alert.addAction(okAction)
        self.presentVC(vc: alert)
    }
    
    func showPopup(message: String, popupState: Loaf.State, popupDuration: Loaf.Duration, completionHandler: Loaf.LoafCompletionHandler){
        Loaf(message, state: popupState, location: .bottom, presentingDirection: .vertical, dismissingDirection: .vertical, sender: self).show(popupDuration, completionHandler: completionHandler)
        
    }
    
}
