//
//  PurchaseLoanInfoViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 30/08/2021.
//

import UIKit

class PurchaseLoanInfoViewController: BaseViewController {
    
    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLoanStage: ColabaTextField!
    @IBOutlet weak var txtfieldPurchasePrice: ColabaTextField!
    @IBOutlet weak var txtfieldLoanAmount: ColabaTextField!
    @IBOutlet weak var txtfieldDownPayment: ColabaTextField!
    @IBOutlet weak var txtfieldPercentage: ColabaTextField!
    @IBOutlet weak var txtfieldClosingDate: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isDownPaymentPercentageChanged = false
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Loan Stage Text Field
        txtfieldLoanStage.setTextField(placeholder: "Loan Stage")
        txtfieldLoanStage.setDelegates(controller: self)
        txtfieldLoanStage.setValidation(validationType: .required)
        txtfieldLoanStage.type = .dropdown
        txtfieldLoanStage.setDropDownDataSource(kLoanStageArray)
        
        ///Purchase Price Text Field
        txtfieldPurchasePrice.setTextField(placeholder: "Purchase Price")
        txtfieldPurchasePrice.setDelegates(controller: self)
        txtfieldPurchasePrice.setTextField(keyboardType: .numberPad)
        txtfieldPurchasePrice.setValidation(validationType: .purchasePrice)
        txtfieldPurchasePrice.type = .amount
        
        ///Loan Amount Text Field
        txtfieldLoanAmount.setTextField(placeholder: "Loan Amount")
        txtfieldLoanAmount.setDelegates(controller: self)
        txtfieldLoanAmount.setTextField(keyboardType: .numberPad)
        txtfieldLoanAmount.setValidation(validationType: .required)
        txtfieldLoanAmount.type = .amount
        
        ///Down Payment Salary Text Field
        txtfieldDownPayment.setTextField(placeholder: "Down Payment")
        txtfieldDownPayment.setDelegates(controller: self)
        txtfieldDownPayment.setTextField(keyboardType: .numberPad)
        txtfieldDownPayment.setValidation(validationType: .required)
        txtfieldDownPayment.type = .amount
        
        ///Down Payment Salary Text Field
        txtfieldPercentage.setTextField(placeholder: "")
        txtfieldPercentage.setDelegates(controller: self)
        txtfieldPercentage.setTextField(keyboardType: .numberPad)
        txtfieldPercentage.setValidation(validationType: .required)
        txtfieldPercentage.type = .percentage
        
        ///Estimated Closing Date Salary Text Field
        txtfieldClosingDate.setTextField(placeholder: "Estimated Closing Date")
        txtfieldClosingDate.setDelegates(controller: self)
        txtfieldClosingDate.setValidation(validationType: .required)
        txtfieldClosingDate.type = .monthlyDatePicker
        
    }
    
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
        if validate() {
            if (txtfieldLoanStage.text != "" && txtfieldPurchasePrice.text != "" && txtfieldLoanAmount.text != "" && txtfieldDownPayment.text != "" && txtfieldPercentage.text != "" && txtfieldClosingDate.text != ""){
                self.goBack()
            }
        }
        
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldLoanStage.validate()
        isValidate = txtfieldPurchasePrice.validate() && isValidate
        isValidate = txtfieldLoanAmount.validate() && isValidate
        isValidate = txtfieldDownPayment.validate() && isValidate
        isValidate = txtfieldPercentage.validate() && isValidate
        isValidate = txtfieldClosingDate.validate() && isValidate
        return isValidate
    }
}

extension PurchaseLoanInfoViewController: ColabaTextFieldDelegate {
    func textFieldDidChange(_ textField: ColabaTextField) {
        if textField == txtfieldPurchasePrice {
            if !isDownPaymentPercentageChanged {
                txtfieldPercentage.attributedText = createAttributedTextWithPrefix(prefix: PrefixType.percentage.rawValue, string: "20")
                isDownPaymentPercentageChanged = true
            }
            calculateDownPayment()
        }
        if textField == txtfieldPercentage {
            isDownPaymentPercentageChanged = true
            calculateDownPayment()
        }
        
        if textField == txtfieldDownPayment {
            isDownPaymentPercentageChanged = true
            calculatePercentage()
        }
    }
    
    func calculateDownPayment() {
        let percentage = Double(cleanString(string: txtfieldPercentage.text ?? "0.0", replaceCharacters: [PrefixType.percentage.rawValue], replaceWith: "")) ?? 0.0
        
        if let purchaseAmount = Double(cleanString(string: txtfieldPurchasePrice.text!, replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) {
            let downPaymentPercentage = percentage / 100
            let downPayment = Int(round(purchaseAmount * downPaymentPercentage))
            let downPaymentString = cleanString(string: downPayment.withCommas(), replaceCharacters: ["$",".00"], replaceWith: "")
            txtfieldDownPayment.attributedText = createAttributedTextWithPrefix(prefix: PrefixType.amount.rawValue, string: downPaymentString)
        }
    }
    
    func calculatePercentage() {
        let downPayment = Double(cleanString(string: txtfieldDownPayment.text ?? "0.0", replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) ?? 0.0
        
        if let purchaseAmount = Double(cleanString(string: txtfieldPurchasePrice.text!, replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) {
            let percentage = Int(round(downPayment / purchaseAmount * 100))
            txtfieldPercentage.attributedText = createAttributedTextWithPrefix(prefix: PrefixType.percentage.rawValue, string: percentage.description)
        }
    }
}

