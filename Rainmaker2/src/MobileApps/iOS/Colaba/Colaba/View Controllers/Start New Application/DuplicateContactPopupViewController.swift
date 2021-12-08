//
//  DuplicateContactPopupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 28/09/2021.
//

import UIKit

protocol DuplicateContactPopupViewControllerDelegate: AnyObject {
    func createLoanApplicationWithExistingContact()
}

class DuplicateContactPopupViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewBottomConstraint: NSLayoutConstraint!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var loanOfficerView: UIView!
    @IBOutlet weak var lblName: UILabel!
    @IBOutlet weak var lblEmailPhoneNumber: UILabel!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var btnYes: UIButton!
    
    var selectedContact = BorrowerContactModel()
    weak var delegate: DuplicateContactPopupViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
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
    
    func setupViews(){
        mainView.roundOnlyTopCorners(radius: 20)
        btnYes.layer.cornerRadius = 5
        btnYes.layer.borderWidth = 2
        btnYes.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnNo.layer.cornerRadius = 5
        loanOfficerView.layer.cornerRadius = 5
        loanOfficerView.layer.borderWidth = 1
        loanOfficerView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        
        lblName.text = "\(selectedContact.firstName.capitalized) \(selectedContact.lastName.capitalized)"
        
        let phoneNumber = formatNumber(with: "(XXX) XXX-XXXX", number: selectedContact.mobileNumber)
        let loanOfficerEmailAndPhone = "\(selectedContact.emailAddress)  ·  \(phoneNumber)"
        let loanOfficerEmailAndPhoneAttributedString = NSMutableAttributedString(string: loanOfficerEmailAndPhone)
        let range1 = loanOfficerEmailAndPhone.range(of: "·")
        loanOfficerEmailAndPhoneAttributedString.addAttribute(NSAttributedString.Key.font, value: Theme.getRubikBoldFont(size: 15), range: loanOfficerEmailAndPhone.nsRange(from: range1!))
        self.lblEmailPhoneNumber.attributedText = loanOfficerEmailAndPhoneAttributedString
    }
    
    func dismissPopup(){
        
        self.mainViewBottomConstraint.constant = -360
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
    
    @IBAction func btnNoTapped(_ sender: UIButton) {
        self.dismissPopup()
    }
    
    @IBAction func btnYesTapped(_ sender: UIButton) {
        self.delegate?.createLoanApplicationWithExistingContact()
        self.dismissPopup()
    }
    
    
}
