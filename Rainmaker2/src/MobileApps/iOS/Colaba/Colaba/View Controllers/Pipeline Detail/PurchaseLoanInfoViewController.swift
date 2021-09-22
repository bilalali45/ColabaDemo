//
//  PurchaseLoanInfoViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 30/08/2021.
//

import UIKit
import Material
import MonthYearPicker
import DropDown

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
        isValidate = txtfieldPurchasePrice.validate()
        isValidate = txtfieldLoanAmount.validate()
        isValidate = txtfieldDownPayment.validate()
        isValidate = txtfieldPercentage.validate()
        isValidate = txtfieldClosingDate.validate()
        return isValidate
    }
}

/*
 
 if let percentage = Double(txtfieldPercentage.text!), let purchaseAmount = Double(txtfieldPurchasePrice.text!.replacingOccurrences(of: ",", with: "")){
 let downPaymentPercentage = percentage / 100
 let downPayment = Int(purchaseAmount * downPaymentPercentage)
 self.txtfieldDownPayment.text = downPayment.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
 self.txtfieldDownPayment.dividerColor = Theme.getSeparatorNormalColor()
 self.txtfieldDownPayment.detail = ""
 */
