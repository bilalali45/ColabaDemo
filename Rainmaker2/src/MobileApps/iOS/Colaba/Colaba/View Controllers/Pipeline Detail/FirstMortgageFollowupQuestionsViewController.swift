//
//  FirstMortgageFollowupQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/09/2021.
//

import UIKit
import Material

class FirstMortgageFollowupQuestionsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldMortgagePayment: ColabaTextField!
    @IBOutlet weak var txtfieldMortgageBalance: ColabaTextField!
    @IBOutlet weak var accountPaymentsView: UIView!
    @IBOutlet weak var accountPaymentViewHeightConstraint: NSLayoutConstraint! // 337 or 248
    @IBOutlet weak var lblAccountPaymentQuestion: UILabel!
    @IBOutlet weak var annualFloodInsuranceStackView: UIStackView!
    @IBOutlet weak var btnAnnualFloodInsurance: UIButton!
    @IBOutlet weak var lblAnnualFloodInsurance: UILabel!
    @IBOutlet weak var annualPropertyTaxesStackView: UIStackView!
    @IBOutlet weak var btnAnnualTaxes: UIButton!
    @IBOutlet weak var lblAnnualTaxes: UILabel!
    @IBOutlet weak var annualHomeownerInsuranceStackView: UIStackView!
    @IBOutlet weak var btnAnnualHomeownerInsurance: UIButton!
    @IBOutlet weak var lblAnnualHomeownerInsurance: UILabel!
    @IBOutlet weak var homeEquityStackView: UIStackView!
    @IBOutlet weak var switchHomeEquity: UISwitch!
    @IBOutlet weak var lblHomeEquity: UILabel!
    @IBOutlet weak var txtfieldCreditLimit: ColabaTextField!
    @IBOutlet weak var mortgagePaidOffView: UIView!
    @IBOutlet weak var lblMortgagePaidOffQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var isAnnualFloodInsurance = true
    var isAnnualPropertyTax = true
    var isAnnualHomeownerInsurance = false
    var isMortgagePaidOff = true
    
    var isForRealEstate = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews()
        if (isForRealEstate){
            lblUsername.text = "5919 TRUSSVILLE CROSSINGS PKWY"
        }
    }
    
    //MARK:- Methods and Actions
    
    func setMaterialTextFieldsAndViews(){

        annualFloodInsuranceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualFloodInsuranceStackViewTapped)))
        annualPropertyTaxesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualPropertyTaxStackViewTapped)))
        annualHomeownerInsuranceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualHomeownerInsuranceStackViewTapped)))
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
        setTextFields()
    }
    
    func setTextFields() {
        ///First Mortgage Payment Text Field
        txtfieldMortgagePayment.setTextField(placeholder: "First Mortgage Payment")
        txtfieldMortgagePayment.setDelegates(controller: self)
        txtfieldMortgagePayment.setValidation(validationType: .noValidation)
        txtfieldMortgagePayment.type = .amount
        
        ///Unpaid First Mortgage Balance Text Field
        txtfieldMortgageBalance.setTextField(placeholder: "Unpaid First Mortgage Balance")
        txtfieldMortgageBalance.setDelegates(controller: self)
        txtfieldMortgageBalance.setValidation(validationType: .noValidation)
        txtfieldMortgageBalance.type = .amount
        
        ///Credit Limit Text Field
        txtfieldCreditLimit.setTextField(placeholder: "Credit Limit")
        txtfieldCreditLimit.setDelegates(controller: self)
        txtfieldCreditLimit.setValidation(validationType: .noValidation)
        txtfieldCreditLimit.type = .amount
    }
    
    @objc func annualFloodInsuranceStackViewTapped(){
        isAnnualFloodInsurance = !isAnnualFloodInsurance
        btnAnnualFloodInsurance.setImage(UIImage(named: isAnnualFloodInsurance ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblAnnualFloodInsurance.font = isAnnualFloodInsurance ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @objc func annualPropertyTaxStackViewTapped(){
        isAnnualPropertyTax = !isAnnualPropertyTax
        btnAnnualTaxes.setImage(UIImage(named: isAnnualPropertyTax ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblAnnualTaxes.font = isAnnualPropertyTax ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @objc func annualHomeownerInsuranceStackViewTapped(){
        isAnnualHomeownerInsurance = !isAnnualHomeownerInsurance
        btnAnnualHomeownerInsurance.setImage(UIImage(named: isAnnualHomeownerInsurance ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblAnnualHomeownerInsurance.font = isAnnualHomeownerInsurance ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @objc func yesStackViewTapped(){
        isMortgagePaidOff = true
        changeMortgagePaidOffStatus()
    }
    
    @objc func noStackViewTapped(){
        isMortgagePaidOff = false
        changeMortgagePaidOffStatus()
    }
    
    func changeMortgagePaidOffStatus(){
        btnYes.setImage(UIImage(named: isMortgagePaidOff ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isMortgagePaidOff ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: isMortgagePaidOff ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
        lblNo.font = isMortgagePaidOff ?  Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func switchHomeEquityChanged(_ sender: UISwitch) {
        lblHomeEquity.font = sender.isOn ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        
        txtfieldCreditLimit.text = ""
        txtfieldCreditLimit.isHidden = !sender.isOn
        accountPaymentViewHeightConstraint.constant = sender.isOn ? 337 : 248
        UIView.animate(withDuration: 0.5) {
            self.view.layoutIfNeeded()
        }
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.dismissVC()
    }
}
