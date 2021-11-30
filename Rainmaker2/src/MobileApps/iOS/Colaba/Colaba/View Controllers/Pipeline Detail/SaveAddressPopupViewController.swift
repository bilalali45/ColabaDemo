//
//  SaveAddressPopupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit

class SaveAddressPopupViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewBottomConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var stackViewSave: UIStackView!
    @IBOutlet weak var stackViewDiscard: UIStackView!
    var isForPrevAddress = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        mainView.roundOnlyTopCorners(radius: 20)
        stackViewSave.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(saveTapped)))
        stackViewDiscard.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(discardTapped)))
        lblTitle.text = isForPrevAddress ? "Save Previous Residence?" : "Save Current Residence?"
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
    
    //MARK:- Methods and Actions
    
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
    
    @objc func saveTapped(){
        dismissPopup()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.35) {
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationSaveAddressAndDismiss), object: nil)
        }
    }
    
    @objc func discardTapped(){
        dismissPopup()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.35) {
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationDiscardAddressChanges), object: nil)
        }
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        self.dismissPopup()
    }
}
