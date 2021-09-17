//
//  ChildSupportFollowupQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 17/09/2021.
//

import UIKit

class ChildSupportFollowupQuestionsViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var childSupportView: UIView!
    @IBOutlet weak var childSupportViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var childSupportStackView: UIStackView!
    @IBOutlet weak var btnChildSupport: UIButton!
    @IBOutlet weak var lblChildSupport: UILabel!
    @IBOutlet weak var txtfieldChildSupportPaymentsRemaining: ColabaTextField!
    @IBOutlet weak var txtfieldChildSupportMonthlyPayment: ColabaTextField!
    @IBOutlet weak var txtfieldChildSupportPaymentRecipient: ColabaTextField!
    @IBOutlet weak var alimonyView: UIView!
    @IBOutlet weak var alimonyViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var alimonySupportStackView: UIStackView!
    @IBOutlet weak var btnAlimony: UIButton!
    @IBOutlet weak var lblAlimony: UILabel!
    @IBOutlet weak var txtfieldAlimonyPaymentsRemaining: ColabaTextField!
    @IBOutlet weak var txtfieldAlimonyMonthlyPayment: ColabaTextField!
    @IBOutlet weak var txtfieldAlimonyPaymentRecipient: ColabaTextField!
    @IBOutlet weak var separateMaintainanceView: UIView!
    @IBOutlet weak var separateMaintainanceViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var separateMaintainanceStackView: UIStackView!
    @IBOutlet weak var btnSeparateMaintainance: UIButton!
    @IBOutlet weak var lblSeparateMaintainance: UILabel!
    @IBOutlet weak var txtfieldSeparateMaintainancePaymentsRemaining: ColabaTextField!
    @IBOutlet weak var txtfieldSeparateMaintainanceMonthlyPayment: ColabaTextField!
    @IBOutlet weak var txtfieldSeparateMaintainancePaymentRecipient: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var isChildSupport = false
    var isAlimony = false
    var isSeparateMaintainance = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextfields()
        childSupportStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(childSupportStackViewTapped)))
        alimonySupportStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(alimonyStackViewTapped)))
        separateMaintainanceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(separateMaintainanceStackViewTapped)))
    }

    //MARK:- Methods and Action
    
    func setupTextfields(){
        
        txtfieldChildSupportPaymentsRemaining.setTextField(placeholder: "Payments remaining")
        txtfieldChildSupportPaymentsRemaining.setDelegates(controller: self)
        txtfieldChildSupportPaymentsRemaining.setValidation(validationType: .required)
        txtfieldChildSupportPaymentsRemaining.setTextField(keyboardType: .asciiCapable)
        txtfieldChildSupportPaymentsRemaining.setIsValidateOnEndEditing(validate: true)
        txtfieldChildSupportPaymentsRemaining.type = .dropdown
        
        txtfieldChildSupportMonthlyPayment.setTextField(placeholder: "Monthly Payment")
        txtfieldChildSupportMonthlyPayment.setDelegates(controller: self)
        txtfieldChildSupportMonthlyPayment.setTextField(keyboardType: .numberPad)
        txtfieldChildSupportMonthlyPayment.setIsValidateOnEndEditing(validate: true)
        txtfieldChildSupportMonthlyPayment.setValidation(validationType: .required)
        txtfieldChildSupportMonthlyPayment.type = .amount
        
        txtfieldChildSupportPaymentRecipient.setTextField(placeholder: "Payment Recipient")
        txtfieldChildSupportPaymentRecipient.setDelegates(controller: self)
        txtfieldChildSupportPaymentRecipient.setValidation(validationType: .required)
        txtfieldChildSupportPaymentRecipient.setTextField(keyboardType: .asciiCapable)
        txtfieldChildSupportPaymentRecipient.setIsValidateOnEndEditing(validate: true)
        
        txtfieldAlimonyPaymentsRemaining.setTextField(placeholder: "Payments remaining")
        txtfieldAlimonyPaymentsRemaining.setDelegates(controller: self)
        txtfieldAlimonyPaymentsRemaining.setValidation(validationType: .required)
        txtfieldAlimonyPaymentsRemaining.setTextField(keyboardType: .asciiCapable)
        txtfieldAlimonyPaymentsRemaining.setIsValidateOnEndEditing(validate: true)
        txtfieldAlimonyPaymentsRemaining.type = .dropdown
        
        txtfieldAlimonyMonthlyPayment.setTextField(placeholder: "Monthly Payment")
        txtfieldAlimonyMonthlyPayment.setDelegates(controller: self)
        txtfieldAlimonyMonthlyPayment.setTextField(keyboardType: .numberPad)
        txtfieldAlimonyMonthlyPayment.setIsValidateOnEndEditing(validate: true)
        txtfieldAlimonyMonthlyPayment.setValidation(validationType: .required)
        txtfieldAlimonyMonthlyPayment.type = .amount
        
        txtfieldAlimonyPaymentRecipient.setTextField(placeholder: "Payment Recipient")
        txtfieldAlimonyPaymentRecipient.setDelegates(controller: self)
        txtfieldAlimonyPaymentRecipient.setValidation(validationType: .required)
        txtfieldAlimonyPaymentRecipient.setTextField(keyboardType: .asciiCapable)
        txtfieldAlimonyPaymentRecipient.setIsValidateOnEndEditing(validate: true)
        
        txtfieldSeparateMaintainancePaymentsRemaining.setTextField(placeholder: "Payments remaining")
        txtfieldSeparateMaintainancePaymentsRemaining.setDelegates(controller: self)
        txtfieldSeparateMaintainancePaymentsRemaining.setValidation(validationType: .required)
        txtfieldSeparateMaintainancePaymentsRemaining.setTextField(keyboardType: .asciiCapable)
        txtfieldSeparateMaintainancePaymentsRemaining.setIsValidateOnEndEditing(validate: true)
        txtfieldSeparateMaintainancePaymentsRemaining.type = .dropdown
        
        txtfieldSeparateMaintainanceMonthlyPayment.setTextField(placeholder: "Monthly Payment")
        txtfieldSeparateMaintainanceMonthlyPayment.setDelegates(controller: self)
        txtfieldSeparateMaintainanceMonthlyPayment.setTextField(keyboardType: .numberPad)
        txtfieldSeparateMaintainanceMonthlyPayment.setIsValidateOnEndEditing(validate: true)
        txtfieldSeparateMaintainanceMonthlyPayment.setValidation(validationType: .required)
        txtfieldSeparateMaintainanceMonthlyPayment.type = .amount
        
        txtfieldSeparateMaintainancePaymentRecipient.setTextField(placeholder: "Payment Recipient")
        txtfieldSeparateMaintainancePaymentRecipient.setDelegates(controller: self)
        txtfieldSeparateMaintainancePaymentRecipient.setValidation(validationType: .required)
        txtfieldSeparateMaintainancePaymentRecipient.setTextField(keyboardType: .asciiCapable)
        txtfieldSeparateMaintainancePaymentRecipient.setIsValidateOnEndEditing(validate: true)
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
    }
    
    func setScreenHeight(){
        
        let childSupportViewHeight = childSupportView.frame.height
        let alimonyViewHeight = alimonyView.frame.height
        let separateMaintainanceViewHeight = separateMaintainanceView.frame.height
        
        let totalHeight = childSupportViewHeight + alimonyViewHeight + separateMaintainanceViewHeight + 200
        self.mainViewHeightConstraint.constant = totalHeight
        UIView.animate(withDuration: 0.5) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func childSupportStackViewTapped(){
        isChildSupport = !isChildSupport
        btnChildSupport.setImage(UIImage(named: isChildSupport ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblChildSupport.font = isChildSupport ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        childSupportViewHeightConstraint.constant = isChildSupport ? 250 : 40
        txtfieldChildSupportPaymentsRemaining.isHidden = !isChildSupport
        txtfieldChildSupportMonthlyPayment.isHidden = !isChildSupport
        txtfieldChildSupportPaymentRecipient.isHidden = !isChildSupport
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func alimonyStackViewTapped(){
        isAlimony = !isAlimony
        btnAlimony.setImage(UIImage(named: isAlimony ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblAlimony.font = isAlimony ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        alimonyViewHeightConstraint.constant = isAlimony ? 250 : 40
        txtfieldAlimonyPaymentsRemaining.isHidden = !isAlimony
        txtfieldAlimonyMonthlyPayment.isHidden = !isAlimony
        txtfieldAlimonyPaymentRecipient.isHidden = !isAlimony
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func separateMaintainanceStackViewTapped(){
        isSeparateMaintainance = !isSeparateMaintainance
        btnSeparateMaintainance.setImage(UIImage(named: isSeparateMaintainance ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblSeparateMaintainance.font = isSeparateMaintainance ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        separateMaintainanceViewHeightConstraint.constant = isSeparateMaintainance ? 250 : 40
        txtfieldSeparateMaintainancePaymentsRemaining.isHidden = !isSeparateMaintainance
        txtfieldSeparateMaintainanceMonthlyPayment.isHidden = !isSeparateMaintainance
        txtfieldSeparateMaintainancePaymentRecipient.isHidden = !isSeparateMaintainance
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
}
