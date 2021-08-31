//
//  PurchaseLoanInfoViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 30/08/2021.
//

import UIKit
import Material
import MonthYearPicker

class PurchaseLoanInfoViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLoanStage: TextField!
    @IBOutlet weak var txtfieldPurchasePrice: TextField!
    @IBOutlet weak var purchasePriceDollarView: UIView!
    @IBOutlet weak var txtfieldLoanAmount: TextField!
    @IBOutlet weak var loanAmountDollarView: UIView!
    @IBOutlet weak var txtfieldDownPayment: TextField!
    @IBOutlet weak var downPaymentDollarView: UIView!
    @IBOutlet weak var txtfieldPercentage: TextField!
    @IBOutlet weak var percentageView: UIView!
    @IBOutlet weak var txtfieldClosingDate: TextField!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let closingDateFormatter = DateFormatter()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldLoanStage, txtfieldPurchasePrice, txtfieldLoanAmount, txtfieldDownPayment, txtfieldPercentage, txtfieldClosingDate])
        
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
        
        txtfieldPurchasePrice.addTarget(self, action: #selector(txtfieldPurchasePriceChanged), for: .editingChanged)
        txtfieldLoanAmount.addTarget(self, action: #selector(txtfieldLoanAmountChanged), for: .editingChanged)
        txtfieldDownPayment.addTarget(self, action: #selector(txtfieldDownPaymentChanged), for: .editingChanged)
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
        closingDateFormatter.dateStyle = .medium
        closingDateFormatter.dateFormat = "MM/yyyy"
        txtfieldClosingDate.addInputViewMonthYearDatePicker(target: self, selector: #selector(dateChanged))
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
    
    @objc func dateChanged() {
        if let  datePicker = self.txtfieldClosingDate.inputView as? MonthYearPickerView {
            self.txtfieldClosingDate.text = closingDateFormatter.string(from: datePicker.date)
        }
    }
    
    @objc func txtfieldPurchasePriceChanged(){
        if let amount = Int(txtfieldPurchasePrice.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldPurchasePrice.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldLoanAmountChanged(){
        if let amount = Int(txtfieldLoanAmount.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldLoanAmount.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldDownPaymentChanged(){
        if let amount = Int(txtfieldDownPayment.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldDownPayment.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.goBack()
    }
    
}

extension PurchaseLoanInfoViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        if (textField == txtfieldPurchasePrice){
            txtfieldPurchasePrice.textInsetsPreset = .horizontally5
            txtfieldPurchasePrice.placeholderHorizontalOffset = -24
            purchasePriceDollarView.isHidden = false
        }
        
        if (textField == txtfieldLoanAmount){
            txtfieldLoanAmount.textInsetsPreset = .horizontally5
            txtfieldLoanAmount.placeholderHorizontalOffset = -24
            loanAmountDollarView.isHidden = false
        }
        
        if (textField == txtfieldDownPayment){
            txtfieldDownPayment.textInsetsPreset = .horizontally5
            txtfieldDownPayment.placeholderHorizontalOffset = -24
            downPaymentDollarView.isHidden = false
        }
        
        if (textField == txtfieldPercentage){
            txtfieldPercentage.textInsetsPreset = .horizontally5
            txtfieldPercentage.placeholderHorizontalOffset = -24
            percentageView.isHidden = false
        }
        
        if (textField == txtfieldClosingDate){
            dateChanged()
        }

    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldPurchasePrice && txtfieldPurchasePrice.text == ""){
            txtfieldPurchasePrice.textInsetsPreset = .none
            txtfieldPurchasePrice.placeholderHorizontalOffset = 0
            purchasePriceDollarView.isHidden = true
        }
        
        if (textField == txtfieldLoanAmount && txtfieldLoanAmount.text == ""){
            txtfieldLoanAmount.textInsetsPreset = .none
            txtfieldLoanAmount.placeholderHorizontalOffset = 0
            loanAmountDollarView.isHidden = true
        }
        
        if (textField == txtfieldDownPayment && txtfieldDownPayment.text == ""){
            txtfieldDownPayment.textInsetsPreset = .none
            txtfieldDownPayment.placeholderHorizontalOffset = 0
            downPaymentDollarView.isHidden = true
        }
        
        if (textField == txtfieldPercentage && txtfieldPercentage.text == ""){
            txtfieldPercentage.textInsetsPreset = .none
            txtfieldPercentage.placeholderHorizontalOffset = 0
            percentageView.isHidden = true
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldLoanStage, txtfieldPurchasePrice, txtfieldLoanAmount, txtfieldDownPayment, txtfieldPercentage, txtfieldClosingDate])
    }
}
