//
//  SecondMortgageFollowupQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/09/2021.
//

import UIKit
import Material

class SecondMortgageFollowupQuestionsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldMortgagePayment: TextField!
    @IBOutlet weak var mortgagePaymentDollarView: UIView!
    @IBOutlet weak var txtfieldMortgageBalance: TextField!
    @IBOutlet weak var mortgageBalanceDollarView: UIView!
    @IBOutlet weak var homeEquityStackView: UIStackView!
    @IBOutlet weak var switchHomeEquity: UISwitch!
    @IBOutlet weak var lblHomeEquity: UILabel!
    @IBOutlet weak var txtfieldCreditLimit: TextField!
    @IBOutlet weak var creditLimitDollarView: UIView!
    @IBOutlet weak var mortgageCombinedView: UIView!
    @IBOutlet weak var mortgageCombinedViewTopConstraint: NSLayoutConstraint! //140 or 50
    @IBOutlet weak var mortgageCombinedViewHeightConstarint: NSLayoutConstraint!
    @IBOutlet weak var lblMortgageCombinedQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var propertyPurchaseView: UIView!
    @IBOutlet weak var propertyPurchaseViewTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblPropertyPurchaseQuestion: UILabel!
    @IBOutlet weak var yesStackView2: UIStackView!
    @IBOutlet weak var btnYes2: UIButton!
    @IBOutlet weak var lblYes2: UILabel!
    @IBOutlet weak var noStackView2: UIStackView!
    @IBOutlet weak var btnNo2: UIButton!
    @IBOutlet weak var lblNo2: UILabel!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var isMortgageCombine = true
    var isMortgageTakenWhenPropertyPurchase = true
    
    var isForRealEstate = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldMortgagePayment, txtfieldMortgageBalance, txtfieldCreditLimit])
        if (isForRealEstate){
            lblUsername.text = "5919 TRUSSVILLE CROSSINGS PKWY"
            mortgageCombinedView.isHidden = true
            mortgageCombinedViewHeightConstarint.constant = 0
            propertyPurchaseViewTopConstraint.constant = 0
        }
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
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        yesStackView2.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackView2Tapped)))
        noStackView2.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackView2Tapped)))
        
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
    
    @objc func yesStackViewTapped(){
        isMortgageCombine = true
        changeMortgageCombineStatus()
    }
    
    @objc func noStackViewTapped(){
        isMortgageCombine = false
        changeMortgageCombineStatus()
    }
    
    func changeMortgageCombineStatus(){
        btnYes.setImage(UIImage(named: isMortgageCombine ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isMortgageCombine ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: isMortgageCombine ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
        lblNo.font = isMortgageCombine ?  Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
    }
    
    @objc func yesStackView2Tapped(){
        isMortgageTakenWhenPropertyPurchase = true
        changeMortgageTakenWhenPropertyPurchaseStatus()
    }
    
    @objc func noStackView2Tapped(){
        isMortgageTakenWhenPropertyPurchase = false
        changeMortgageTakenWhenPropertyPurchaseStatus()
    }
    
    func changeMortgageTakenWhenPropertyPurchaseStatus(){
        btnYes2.setImage(UIImage(named: isMortgageTakenWhenPropertyPurchase ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes2.font = isMortgageTakenWhenPropertyPurchase ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo2.setImage(UIImage(named: isMortgageTakenWhenPropertyPurchase ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
        lblNo2.font = isMortgageTakenWhenPropertyPurchase ?  Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func switchHomeEquityChanged(_ sender: UISwitch) {
        lblHomeEquity.font = sender.isOn ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        
        txtfieldCreditLimit.text = ""
        txtfieldCreditLimit.textInsetsPreset = .none
        txtfieldCreditLimit.placeholderHorizontalOffset = 0
        creditLimitDollarView.isHidden = true
        txtfieldCreditLimit.isHidden = !sender.isOn
        mortgageCombinedViewTopConstraint.constant = sender.isOn ? 140 : 50
        UIView.animate(withDuration: 0.5) {
            self.view.layoutIfNeeded()
        }
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
}

extension SecondMortgageFollowupQuestionsViewController: UITextFieldDelegate{
    
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

