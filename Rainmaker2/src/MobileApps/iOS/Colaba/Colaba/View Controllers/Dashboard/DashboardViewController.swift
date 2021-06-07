//
//  DashboardViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 02/06/2021.
//

import UIKit
import RealmSwift

class DashboardViewController: BaseViewController {

    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    }
    
    //MARK:- Methods and Actions
    
    @IBAction func btnLogoutTapped(_ sender: UIButton) {
        
        let alert = UIAlertController(title: "Logout", message: "Are you sure you want to logout?", preferredStyle: .alert)
        let yesAction = UIAlertAction(title: "Yes", style: .default) { ation in
            DispatchQueue.main.async {
                self.logoutWithRequest()
            }
        }
        let noAction = UIAlertAction(title: "No", style: .destructive, handler: nil)
        alert.addAction(yesAction)
        alert.addAction(noAction)
        self.present(alert, animated: true, completion: nil)
    }
    
    //MARK:- API's
    
    func logoutWithRequest(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeAPI(type: .logout, method: .post, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    let realm = try! Realm()
                    realm.beginWrite()
                    realm.deleteAll()
                    try! realm.commitWrite()
                    self.goToLogin()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
}
