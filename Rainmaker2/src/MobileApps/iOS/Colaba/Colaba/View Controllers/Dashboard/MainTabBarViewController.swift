//
//  MainTabBarViewController.swift
//  Colaba
//
//  Created by Murtaza on 11/06/2021.
//

import UIKit

class MainTabBarViewController: UITabBarController {

    var newButton = UIButton()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.tabBar.tintColor = Theme.getButtonBlueColor()
        
        NotificationCenter.default.addObserver(self, selector: #selector(popupViewCloseTapped), name: NSNotification.Name(rawValue: kNotificationPopupViewCloseTapped), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(getNotificationCount), name: NSNotification.Name(rawValue: kNotificationRefreshCounter), object: nil)
        
        let centerPoint = (self.view.frame.width / 2) - 30
        let centerPoint2 = (self.view.frame.width / 2) - 45
        newButton = UIButton(frame: CGRect(x: centerPoint, y: -30, width: 60, height: 60))
        newButton.backgroundColor = Theme.getButtonBlueColor()
        newButton.setImage(UIImage(named: "PlusButton"), for: .normal)
        newButton.roundButtonWithShadow(shadowColor: UIColor.black.withAlphaComponent(0.3).cgColor)
        newButton.addTarget(self, action: #selector(newButtonTapped), for: .touchUpInside)
        self.tabBar.addSubview(newButton)
        let hidedButton = UIButton(frame: CGRect(x: centerPoint2, y: -30, width: 90, height: 90))
        hidedButton.backgroundColor = .clear
        hidedButton.roundAllCorners(radius: 45)
        hidedButton.addTarget(self, action: #selector(newButtonTapped), for: .touchUpInside)
        self.tabBar.addSubview(hidedButton)
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(true)
        getNotificationCount()
    }
    
    //MARK:- Methods and Actions
    @objc func newButtonTapped(){
        UIView.transition(with: self.newButton, duration: 0.5, options: .transitionCrossDissolve, animations: {
            self.newButton.isHidden = true
        })
        let vc = Utility.getCreateNewPopupVC()
        self.present(vc, animated: false, completion: nil)
    }
    
    @objc func popupViewCloseTapped(){
        UIView.transition(with: self.newButton, duration: 0.5, options: .transitionCrossDissolve, animations: {
            self.newButton.isHidden = false
        })
    }
   
    //MARK:- API's
    
    @objc func getNotificationCount(){
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getNotificationCount, method: .get, params: nil) { (status, result, message) in
            
            DispatchQueue.main.async {
                if (status == .success){
                    self.tabBar.items![1].badgeValue = result["count"].intValue == 0 ? nil : "\(result["count"].intValue)"
                }
                else{
                    self.tabBar.items![1].badgeValue = nil
                }
            }
            
        }
        
    }
}
