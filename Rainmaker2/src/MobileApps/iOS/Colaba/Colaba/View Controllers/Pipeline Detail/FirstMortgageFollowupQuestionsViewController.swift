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
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldMortgagePayment: TextField!
    @IBOutlet weak var mortgagePaymentDollarView: UIView!
    @IBOutlet weak var txtfieldMortgageBalance: TextField!
    @IBOutlet weak var mortgageBalanceDollarView: UIView!
    @IBOutlet weak var accountPaymentsView: UIView!
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
    @IBOutlet weak var txtfieldCreditLimit: TextField!
    @IBOutlet weak var creditLimitDollarView: UIView!
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
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldMortgagePayment, txtfieldMortgageBalance, txtfieldCreditLimit])
    }
    
    //MARK:- Methods and Actions
    
    func setMaterialTextFieldsAndViews(textfields: [TextField]){
        for textfield in textfields{
            textfield.dividerActiveColor = Theme.getButtonBlueColor()
            textfield.dividerColor = Theme.getSeparatorNormalColor()
            textfield.placeholderActiveColor = Theme.getAppGreyColor()
            textfield.delegate = self
            textfield.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
            textfield.detailLabel.font = Theme.getRubikRegularFont(size: 12)
            textfield.detailColor = .red
            textfield.detailVerticalOffset = 4
            textfield.placeholderVerticalOffset = 8
            textfield.textColor = Theme.getAppBlackColor()
        }
        
        txtfieldMortgagePayment.addTarget(self, action: #selector(txtfieldMortgagePaymentChanged), for: .editingChanged)
        txtfieldMortgageBalance.addTarget(self, action: #selector(txtfieldMortgageBalanceChanged), for: .editingChanged)
        txtfieldCreditLimit.addTarget(self, action: #selector(txtfieldCreditChanged), for: .editingChanged)
        
        annualFloodInsuranceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualFloodInsuranceStackViewTapped)))
        annualPropertyTaxesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualPropertyTaxStackViewTapped)))
        annualHomeownerInsuranceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualHomeownerInsuranceStackViewTapped)))
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
    }
    
    func setPlaceholderLabelColorAfterTextFilled(selectedTextField: UITextField, allTextFields: [TextField]){
        for allTextField in allTextFields{
            if (allTextField == selectedTextField){
                if (allTextField.text == ""){
                    allTextField.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                }
                else{
                    allTextField.placeholderLabel.textColor = Theme.getAppGreyColor()
                }
            }
        }
    }
    
    @objc func txtfieldMortgagePaymentChanged(){
        if let amount = Int(txtfieldMortgagePayment.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldMortgagePayment.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }

    @objc func txtfieldMortgageBalanceChanged(){
        if let amount = Int(txtfieldMortgageBalance.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldMortgageBalance.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldCreditChanged(){
        if let amount = Int(txtfieldCreditLimit.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldCreditLimit.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
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
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
}

extension FirstMortgageFollowupQuestionsViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldMortgagePayment){
            txtfieldMortgagePayment.textInsetsPreset = .horizontally5
            txtfieldMortgagePayment.placeholderHorizontalOffset = -24
            mortgagePaymentDollarView.isHidden = false
        }
        
        if (textField == txtfieldMortgageBalance){
            txtfieldMortgageBalance.textInsetsPreset = .horizontally5
            txtfieldMortgageBalance.placeholderHorizontalOffset = -24
            mortgageBalanceDollarView.isHidden = false
        }
        
        if (textField == txtfieldCreditLimit){
            txtfieldCreditLimit.textInsetsPreset = .horizontally5
            txtfieldCreditLimit.placeholderHorizontalOffset = -24
            creditLimitDollarView.isHidden = false
        }
        
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldMortgagePayment && txtfieldMortgagePayment.text == ""){
            txtfieldMortgagePayment.textInsetsPreset = .none
            txtfieldMortgagePayment.placeholderHorizontalOffset = 0
            mortgagePaymentDollarView.isHidden = true
        }
        
        if (textField == txtfieldMortgageBalance && txtfieldMortgageBalance.text == ""){
            txtfieldMortgageBalance.textInsetsPreset = .none
            txtfieldMortgageBalance.placeholderHorizontalOffset = 0
            mortgageBalanceDollarView.isHidden = true
        }
        
        if (textField == txtfieldCreditLimit && txtfieldCreditLimit.text == ""){
            txtfieldCreditLimit.textInsetsPreset = .none
            txtfieldCreditLimit.placeholderHorizontalOffset = 0
            creditLimitDollarView.isHidden = true
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldMortgagePayment, txtfieldMortgageBalance, txtfieldCreditLimit])
    }
    
}
