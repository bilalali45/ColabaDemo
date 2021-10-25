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
    @IBOutlet weak var lblError: UILabel!
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
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isChildSupport = false
    var isAlimony = false
    var isSeparateMaintainance = false
    var questionModel = GovernmentQuestionModel()
    var borrowerName = ""
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextfields()
        childSupportStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(childSupportStackViewTapped)))
        alimonySupportStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(alimonyStackViewTapped)))
        separateMaintainanceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(separateMaintainanceStackViewTapped)))
        setQuestionData()
    }

    //MARK:- Methods and Action
    
    func setupTextfields(){
        
        txtfieldChildSupportPaymentsRemaining.setTextField(placeholder: "Payments remaining", controller: self, validationType: .required)
        txtfieldChildSupportPaymentsRemaining.type = .dropdown
        txtfieldChildSupportPaymentsRemaining.setDropDownDataSource(kPaymentsRemainingArray)
        
        txtfieldChildSupportMonthlyPayment.setTextField(placeholder: "Monthly Payment", controller: self, validationType: .required)
        txtfieldChildSupportMonthlyPayment.type = .amount
        
        txtfieldChildSupportPaymentRecipient.setTextField(placeholder: "Payment Recipient", controller: self, validationType: .required)
            
        txtfieldAlimonyPaymentsRemaining.setTextField(placeholder: "Payments remaining", controller: self, validationType: .required)
        txtfieldAlimonyPaymentsRemaining.type = .dropdown
        txtfieldAlimonyPaymentsRemaining.setDropDownDataSource(kPaymentsRemainingArray)
        
        txtfieldAlimonyMonthlyPayment.setTextField(placeholder: "Monthly Payment", controller: self, validationType: .required)
        txtfieldAlimonyMonthlyPayment.type = .amount
            
        txtfieldAlimonyPaymentRecipient.setTextField(placeholder: "Payment Recipient", controller: self, validationType: .required)
        
        txtfieldSeparateMaintainancePaymentsRemaining.setTextField(placeholder: "Payments remaining", controller: self, validationType: .required)
        txtfieldSeparateMaintainancePaymentsRemaining.type = .dropdown
        txtfieldSeparateMaintainancePaymentsRemaining.setDropDownDataSource(kPaymentsRemainingArray)
        
        txtfieldSeparateMaintainanceMonthlyPayment.setTextField(placeholder: "Monthly Payment", controller: self, validationType: .required)
        txtfieldSeparateMaintainanceMonthlyPayment.type = .amount
        
        txtfieldSeparateMaintainancePaymentRecipient.setTextField(placeholder: "Payment Recipient", controller: self, validationType: .required)
    }
    
    func setQuestionData(){
        lblUsername.text = borrowerName.uppercased()
    }
    
    func setScreenHeight(){
        
        let childSupportViewHeight = childSupportView.frame.height
        let alimonyViewHeight = alimonyView.frame.height
        let separateMaintainanceViewHeight = separateMaintainanceView.frame.height
        
        let totalHeight = childSupportViewHeight + alimonyViewHeight + separateMaintainanceViewHeight + 200
        self.mainViewHeightConstraint.constant = totalHeight
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func childSupportStackViewTapped(){
        lblError.text = ""
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
        lblError.text = ""
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
        lblError.text = ""
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
    
    func validate() -> Bool{
        
        if (!isChildSupport && !isAlimony && !isSeparateMaintainance){
            lblError.text = "Choose one or more options."
            return false
        }
        else{
            lblError.text = ""
            if (isChildSupport){
                if (!txtfieldChildSupportPaymentsRemaining.validate()){
                    return false
                }
                if (!txtfieldChildSupportMonthlyPayment.validate()){
                    return false
                }
                if (!txtfieldChildSupportPaymentRecipient.validate()){
                    return false
                }
            }
            if (isAlimony){
                if (!txtfieldAlimonyPaymentsRemaining.validate()){
                    return false
                }
                if (!txtfieldAlimonyMonthlyPayment.validate()){
                    return false
                }
                if (!txtfieldAlimonyPaymentRecipient.validate()){
                    return false
                }
            }
            if (isSeparateMaintainance){
                if (!txtfieldSeparateMaintainancePaymentsRemaining.validate()){
                    return false
                }
                if (!txtfieldSeparateMaintainanceMonthlyPayment.validate()){
                    return false
                }
                if (!txtfieldSeparateMaintainancePaymentRecipient.validate()){
                    return false
                }
            }
            return true
        }
        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if (isChildSupport){
            txtfieldChildSupportPaymentsRemaining.validate()
            txtfieldChildSupportMonthlyPayment.validate()
            txtfieldChildSupportPaymentRecipient.validate()
        }
        if (isAlimony){
            txtfieldAlimonyPaymentsRemaining.validate()
            txtfieldAlimonyMonthlyPayment.validate()
            txtfieldAlimonyPaymentRecipient.validate()
        }
        if (isSeparateMaintainance){
            txtfieldSeparateMaintainancePaymentsRemaining.validate()
            txtfieldSeparateMaintainanceMonthlyPayment.validate()
            txtfieldSeparateMaintainancePaymentRecipient.validate()
        }
        
        if (validate()){
            self.dismissVC()
        }
    }
    
}
