//
//  AddBankAccountViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 07/09/2021.
//

import UIKit

class AddBankAccountViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldAccountType: ColabaTextField!
    @IBOutlet weak var txtfieldFinancialInstitution: ColabaTextField!
    @IBOutlet weak var txtfieldAccountNumber: ColabaTextField!
    @IBOutlet weak var txtfieldAnnualBaseSalary: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isShowAccountNumber = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Account Type Text Field
        txtfieldAccountType.setTextField(placeholder: "Account Type")
        txtfieldAccountType.setDelegates(controller: self)
        txtfieldAccountType.setValidation(validationType: .required)
        txtfieldAccountType.type = .dropdown
        txtfieldAccountType.setDropDownDataSource(kAccountTypeArray)
        
        ///Financial Institution Text Field
        txtfieldFinancialInstitution.setTextField(placeholder: "Financial Institution")
        txtfieldFinancialInstitution.setDelegates(controller: self)
        txtfieldFinancialInstitution.setValidation(validationType: .required)
        
        ///Account Number Text Field
        txtfieldAccountNumber.setTextField(placeholder: "Account Number")
        txtfieldAccountNumber.setDelegates(controller: self)
        txtfieldAccountNumber.type = .password
        txtfieldAccountNumber.setValidation(validationType: .required)
        txtfieldAccountNumber.setTextField(keyboardType: .numberPad)
        
        ///Annual Base Salary Text Field
        txtfieldAnnualBaseSalary.setTextField(placeholder: "Annual Base Salary")
        txtfieldAnnualBaseSalary.setDelegates(controller: self)
        txtfieldAnnualBaseSalary.setTextField(keyboardType: .numberPad)
        txtfieldAnnualBaseSalary.setValidation(validationType: .required)
        txtfieldAnnualBaseSalary.type = .amount
        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            if (txtfieldAccountType.text != "" && txtfieldFinancialInstitution.text != "" && txtfieldAccountNumber.text != "" && txtfieldAnnualBaseSalary.text != ""){
                self.dismissVC()
            }
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldAccountType.validate()
        isValidate = txtfieldFinancialInstitution.validate()
        isValidate = txtfieldAccountNumber.validate()
        isValidate = txtfieldAnnualBaseSalary.validate()
        return isValidate
    }
}
