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
    
    @IBOutlet weak var navigationView: UIView!
    @IBOutlet weak var navigationViewTopConstraint: NSLayoutConstraint!
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
        NotificationCenter.default.addObserver(self, selector: #selector(hidesNavigationBar), name: NSNotification.Name(rawValue: kNotificationHidesHomeNavigationBar), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(showNavigationBar), name: NSNotification.Name(rawValue: kNotificationShowHomeNavigationBar), object: nil)
//        userIcon.isUserInteractionEnabled = true
//        userIcon.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(userImageViewTapped)))
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(true)
        lblUsername.text = "\(Utility.getGreetingMessage()), \(Utility.getUserFirstName())"
    }
    
    //MARK:- Methods and Actions
    
    func setTopTabBar(){
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.03) {
            let tabItems = ["All Loans", "Active Loans"/*, "Inactive Loans"*/]
            let carbonTabSwipeNavigation = CarbonTabSwipeNavigation(items: tabItems, delegate: self)
            
            let headerView = UIView(frame: CGRect(x: 0, y: (carbonTabSwipeNavigation.carbonSegmentedControl?.frame.origin.y)! + 41, width: self.view.bounds.width, height: 59))
            headerView.backgroundColor = .clear
            
            let nib = Bundle.main.loadNibNamed("DashboardHeaderView", owner: self, options: nil)
            if let contentView = nib?.first as? UIView{
                contentView.frame = headerView.bounds
                contentView.autoresizingMask = [.flexibleWidth, .flexibleHeight]
                headerView.addSubview(contentView)
            }
            
            carbonTabSwipeNavigation.carbonSegmentedControl?.addSubview(headerView)
            carbonTabSwipeNavigation.carbonSegmentedControl?.bringSubviewToFront(headerView)
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
            
            var segmentWidth: CGFloat = 0.0
            
            if (UIDevice.current.screenType == .iPhones_4_4S || UIDevice.current.screenType == .iPhones_5_5s_5c_SE || UIDevice.current.screenType == .iPhones_6_6s_7_8){
                segmentWidth = (self.tabView.frame.width / 2)
            }
            else{
                segmentWidth = (self.tabView.frame.width / 2)
            }
            
            let indicator = carbonTabSwipeNavigation.carbonSegmentedControl?.indicator
            let subView = UIView()
            subView.backgroundColor = Theme.getButtonBlueColor()
            subView.roundOnlyTopCorners(radius: 4)
            indicator?.addSubview(subView)
            subView.translatesAutoresizingMaskIntoConstraints = false
            subView.widthAnchor.constraint(equalToConstant: segmentWidth * 0.9).isActive = true
            subView.centerXAnchor.constraint(equalTo: indicator!.centerXAnchor, constant: 0).isActive = true
            subView.topAnchor.constraint(equalTo: indicator!.topAnchor, constant: 0).isActive = true
            subView.bottomAnchor.constraint(equalTo: indicator!.bottomAnchor, constant: 0).isActive = true
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 0)
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 1)
            //carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 2)
            carbonTabSwipeNavigation.insert(intoRootViewController: self, andTargetView: self.tabView)
        }
        
    }
    
    @objc func hidesNavigationBar(){
        
        DispatchQueue.main.async {
            self.navigationViewTopConstraint.constant = -70
            self.navigationView.isHidden = true
            UIView.animate(withDuration: 0.3) {
                self.view.layoutIfNeeded()
            }
        }
        
    }
    
    @objc func showNavigationBar(){
        
        DispatchQueue.main.async {
            self.navigationViewTopConstraint.constant = 0
            self.navigationView.isHidden = false
            UIView.animate(withDuration: 0.3) {
                self.view.layoutIfNeeded()
            }
        }
        
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
    
    @objc func userImageViewTapped(){
        
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
