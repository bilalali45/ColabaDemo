//
//  DeleteAddressPopupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit

protocol DeleteAddressPopupViewControllerDelegate: AnyObject {
    func deleteAddress(indexPath: IndexPath)
}

class DeleteAddressPopupViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewBottomConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var btnNo: UIButton!
    
    var popupTitle = ""
    var screenType = 1 // 1 for main Screen, 2 for Address Screen, 3 for Mailing Screen // 4 for Asset Income Real Estate
    var indexPath = IndexPath()
    weak var delegate: DeleteAddressPopupViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        mainView.roundOnlyTopCorners(radius: 20)
        btnYes.layer.cornerRadius = 5
        btnYes.layer.borderWidth = 2
        btnYes.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnNo.layer.cornerRadius = 5
        lblTitle.text = popupTitle
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
        
        self.mainViewBottomConstraint.constant = -227
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30) {
            self.view.backgroundColor = .clear
        } completion: { _ in
            self.dismissVC()
        }
        
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        self.dismissPopup()
    }
    
    @IBAction func btnYesTapped(_ sender: UIButton) {
        self.dismissPopup()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.35) {
            if (self.screenType == 1){
                self.delegate?.deleteAddress(indexPath: self.indexPath)
            }
            else if (self.screenType == 2){
                NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationSaveAddressAndDismiss), object: nil)
                self.delegate?.deleteAddress(indexPath: self.indexPath)
            }
            else if (self.screenType == 3){
                NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationDeleteMailingAddressAndDismiss), object: nil)
            }
            else if (self.screenType == 4){
                self.delegate?.deleteAddress(indexPath: self.indexPath)
            }
        }
    }
    
    @IBAction func btnNoTapped(_ sender: UIButton) {
        self.dismissPopup()
    }
    
}
