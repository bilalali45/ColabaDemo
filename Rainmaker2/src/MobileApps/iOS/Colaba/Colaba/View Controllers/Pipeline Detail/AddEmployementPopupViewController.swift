//
//  AddEmployementPopupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/09/2021.
//

import UIKit

class AddEmployementPopupViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewBottomConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var currentEmployementStackView: UIStackView!
    @IBOutlet weak var previousEmployementStackView: UIStackView!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        mainView.roundOnlyTopCorners(radius: 20)
        currentEmployementStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(currentEmployementTapped)))
        previousEmployementStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(previousEmployementTapped)))
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
    
    //MARK:- Actions and Methods
    
    func dismissPopup(){
        
        self.mainViewBottomConstraint.constant = -206
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30) {
            self.view.backgroundColor = .clear
        } completion: { _ in
            self.dismissVC()
        }
        
    }
    
    @objc func currentEmployementTapped(){
        dismissPopup()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.31) {
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationAddCurrentEmployement), object: nil)
        }
    }
    
    @objc func previousEmployementTapped(){
        dismissPopup()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.31) {
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationAddPreviousEmployement), object: nil)
        }
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        dismissPopup()
    }
    
}
