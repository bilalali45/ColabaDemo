//
//  DeleteDocumentPopupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/10/2021.
//

import UIKit

class DeleteDocumentPopupViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewBottomConstraint: NSLayoutConstraint!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var lblDeleteTitle: UILabel!
    @IBOutlet weak var lblDocumentName: UILabel!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var btnYes: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        mainView.roundOnlyTopCorners(radius: 20)
        btnYes.layer.cornerRadius = 5
        btnYes.layer.borderWidth = 2
        btnYes.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnNo.layer.cornerRadius = 5
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
        
        self.mainViewBottomConstraint.constant = -298
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
        dismissPopup()
    }
    
    @IBAction func btnNoTapped(_ sender: UIButton) {
        dismissPopup()
    }
    
    @IBAction func btnYesTapped(_ sender: UIButton) {
        dismissPopup()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.30) {
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationDeleteDocument), object: nil)
        }
    }
    
}
