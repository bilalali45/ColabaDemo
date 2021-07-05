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
    
    @IBOutlet weak var userIcon: UIImageView!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnSearch: UIButton!
    @IBOutlet weak var tabView: UIView!
    @IBOutlet weak var floatingView: UIView!
    @IBOutlet weak var floatingApplicationView: UIView!
    @IBOutlet weak var floatingContactView: UIView!
    
    var isFloatingButtonActive = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        //refreshAccessTokenWithRequest()
        setTopTabBar()
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(true)
        lblUsername.text = "\(Utility.getGreetingMessage()), \(Utility.getUserFirstName())"
    }
    
    //MARK:- Methods and Actions
    
    func setTopTabBar(){
        let tabItems = ["All Loans", "Active Loans", "Inactive Loans"]
        let carbonTabSwipeNavigation = CarbonTabSwipeNavigation(items: tabItems, delegate: self)
        carbonTabSwipeNavigation.setIndicatorColor(nil)
        carbonTabSwipeNavigation.setIndicatorHeight(4)
        carbonTabSwipeNavigation.setNormalColor(Theme.getAppGreyColor(), font: Theme.getRubikRegularFont(size: 15))
        carbonTabSwipeNavigation.setSelectedColor(Theme.getButtonBlueColor(), font: Theme.getRubikRegularFont(size: 15))
        //carbonTabSwipeNavigation.toolbar.isTranslucent = false
        carbonTabSwipeNavigation.carbonSegmentedControl?.tintColor = .clear
        carbonTabSwipeNavigation.carbonSegmentedControl?.backgroundColor = .clear
        carbonTabSwipeNavigation.carbonTabSwipeScrollView.backgroundColor = .clear
        carbonTabSwipeNavigation.carbonTabSwipeScrollView.tintColor = .clear
        carbonTabSwipeNavigation.carbonSegmentedControl?.imageNormalColor = .clear
        carbonTabSwipeNavigation.carbonSegmentedControl?.imageSelectedColor = .clear
        let segmentWidth = tabView.frame.width / 3
        let indicator = carbonTabSwipeNavigation.carbonSegmentedControl?.indicator
        let subView = UIView()
        subView.backgroundColor = Theme.getButtonBlueColor()
        subView.layer.cornerRadius = 2
        indicator?.addSubview(subView)
        subView.translatesAutoresizingMaskIntoConstraints = false
        subView.widthAnchor.constraint(equalToConstant: segmentWidth * 0.8).isActive = true
        subView.centerXAnchor.constraint(equalTo: indicator!.centerXAnchor, constant: 0).isActive = true
        subView.topAnchor.constraint(equalTo: indicator!.topAnchor, constant: 0).isActive = true
        subView.bottomAnchor.constraint(equalTo: indicator!.bottomAnchor, constant: 0).isActive = true
        carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 0)
        carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 1)
        carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 2)
        carbonTabSwipeNavigation.insert(intoRootViewController: self, andTargetView: tabView)
        carbonTabSwipeNavigation.carbonSegmentedControl?.frame = CGRect(x: Double(tabView.frame.origin.x), y: Double(tabView.frame.origin.y), width: Double(tabView.frame.width) * 0.8, height: Double(tabView.frame.height))
    }
    
    @IBAction func btnSearchTapped(_ sender: UIButton) {
        let vc = Utility.getSearchVC()
        vc.hidesBottomBarWhenPushed = true
        self.presentVC(vc: vc)
    }
    
//    @IBAction func btnNewTapped(_ sender: UIButton){
////        isFloatingButtonActive = !isFloatingButtonActive
////        btnNew.rotate(angle: 45)
//        UIView.transition(with: self.floatingView, duration: 0.5, options: .transitionCrossDissolve, animations: {
//            self.floatingView.isHidden = !self.isFloatingButtonActive
//        })
//    }
    
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

extension DashboardViewController: CarbonTabSwipeNavigationDelegate{
    
    func carbonTabSwipeNavigation(_ carbonTabSwipeNavigation: CarbonTabSwipeNavigation, viewControllerAt index: UInt) -> UIViewController {
        
        if (index == 0){
            return Utility.getPipelineVC()
        }
        else if (index == 1){
            return Utility.getActivePipelineVC()
        }
        else{
            return Utility.getInActivePipelineVC()
        }
        
    }
    
}
