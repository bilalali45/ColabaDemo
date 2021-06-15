//
//  DashboardViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 02/06/2021.
//

import UIKit
import RealmSwift
import CarbonKit

class DashboardViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var logo: UIImageView!
    @IBOutlet weak var btnSearch: UIButton!
    @IBOutlet weak var tabView: UIView!
    @IBOutlet weak var btnNew: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        //refreshAccessTokenWithRequest()
        setTopTabBar()
        btnNew.roundButtonWithShadow()
    }
    
    //MARK:- Methods and Actions
    
    func setTopTabBar(){
        let tabItems = ["All Loans", "Active Loans", "Inactive Loans"]
        let carbonTabSwipeNavigation = CarbonTabSwipeNavigation(items: tabItems, delegate: self)
        carbonTabSwipeNavigation.setIndicatorColor(Theme.getButtonBlueColor())
        carbonTabSwipeNavigation.setNormalColor(Theme.getAppGreyColor(), font: Theme.getRubikRegularFont(size: 15))
        carbonTabSwipeNavigation.setSelectedColor(Theme.getButtonBlueColor(), font: Theme.getRubikRegularFont(size: 15))
//        carbonTabSwipeNavigation.toolbar.isTranslucent = false
        carbonTabSwipeNavigation.carbonSegmentedControl?.tintColor = .clear
        carbonTabSwipeNavigation.carbonSegmentedControl?.backgroundColor = .clear
        carbonTabSwipeNavigation.carbonTabSwipeScrollView.backgroundColor = .clear
        carbonTabSwipeNavigation.carbonTabSwipeScrollView.tintColor = .clear
        carbonTabSwipeNavigation.carbonSegmentedControl?.imageNormalColor = .clear
        carbonTabSwipeNavigation.carbonSegmentedControl?.imageSelectedColor = .clear
        let segmentWidth = tabView.frame.width / 3
        carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 0)
        carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 1)
        carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 2)
        carbonTabSwipeNavigation.insert(intoRootViewController: self, andTargetView: tabView)
    }
    
    @IBAction func btnSearchTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnNewTapped(_ sender: UIButton){
        
    }
    
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
    
    func refreshAccessTokenWithRequest(){
        
        let params = ["Token": Utility.getUserAccessToken(),
                      "RefreshToken": Utility.getUserRefreshToken()]

        APIRouter.sharedInstance.executeAPI(type: .refreshAccessToken, method: .post, params: params) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    
                    let realm = try! Realm()
                    realm.beginWrite()
                    realm.deleteAll()
                    let model = UserModel()
                    model.updateModelWithJSON(json: result["data"])
                    realm.add(model)
                    try! realm.commitWrite()
                    //Dashboard API.
                    
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
    
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

extension DashboardViewController: CarbonTabSwipeNavigationDelegate{
    
    func carbonTabSwipeNavigation(_ carbonTabSwipeNavigation: CarbonTabSwipeNavigation, viewControllerAt index: UInt) -> UIViewController {
        return Utility.getPipelineVC()
    }
    
}
