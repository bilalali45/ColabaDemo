//
//  AssignLoanOfficerPopupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 27/09/2021.
//

import UIKit

class AssignLoanOfficerPopupViewController: BaseViewController {
    
    @IBOutlet weak var backgroundView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewBottomConstraint: NSLayoutConstraint!
    @IBOutlet weak var containerView: UIView!
    
    var loanOfficerMainVC = LoanOfficerMainViewController()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        mainView.roundOnlyTopCorners(radius: 20)
        loanOfficerMainVC = Utility.getLoanOfficerMainVC()
        loanOfficerMainVC.isForPopup = true
        add(viewController: loanOfficerMainVC)
        self.backgroundView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(backgroundViewTapped)))
        NotificationCenter.default.addObserver(self, selector: #selector(seeMoreTapped), name: NSNotification.Name(rawValue: kNotificationLoanOfficerSeeMoreTapped), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(loanOfficerSelected), name: NSNotification.Name(rawValue: kNotificationLoanOfficerSelected), object: nil)
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
        self.mainViewBottomConstraint.constant = 0
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30, animations: {
            self.view.backgroundColor = UIColor(red: 0, green: 0, blue: 0, alpha: 0.08)
            
        }, completion: nil)
        
    }
    
    //MARK:- Methods
    
    func add(viewController: UIViewController){
        addChild(viewController)
        containerView.addSubview(viewController.view)
        viewController.view.frame = containerView.bounds
        viewController.view.autoresizingMask = [.flexibleHeight, .flexibleWidth]
        viewController.didMove(toParent: self)
    }
    
    @objc func backgroundViewTapped(){
        dismissPopup(shouldAnimate: true)
    }
    
    @objc func dismissPopup(shouldAnimate: Bool){
        
        self.mainViewBottomConstraint.constant = -350
        UIView.animate(withDuration: shouldAnimate ? 0.3 : 0.0) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: shouldAnimate ? 0.30 : 0.0) {
            self.view.backgroundColor = .clear
        } completion: { _ in
            self.dismissVC()
        }
        
    }
    
    @objc func seeMoreTapped(){
        dismissPopup(shouldAnimate: true)
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.4) {
            NotificationCenter.default.removeObserver(self, name: NSNotification.Name(rawValue: kNotificationLoanOfficerSeeMoreTapped), object: nil)
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationLoanOfficerSeeMoreTapped), object: nil)
        }
    }
    
    @objc func loanOfficerSelected(){
        dismissPopup(shouldAnimate: true)
        NotificationCenter.default.removeObserver(self, name: NSNotification.Name(rawValue: kNotificationLoanOfficerSelected), object: nil)
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationLoanOfficerSelected), object: nil)
    }
}
